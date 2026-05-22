import pandas as pd
import numpy as np
import geopandas as gpd
import contextily as cx
import matplotlib.pyplot as plt
import seaborn as sns
from ast import literal_eval

def load_comparator_data(file_path):
    return pd.read_csv(file_path, converters={"Pupil": literal_eval, "Building": literal_eval})

def prepare_features(academy_path, maintained_path):
    ac = pd.read_parquet(academy_path).reset_index()
    ma = pd.read_parquet(maintained_path).reset_index()
    combined = pd.concat([ac, ma], ignore_index=True)
    drop_cols = ["Pupil", "Building", "Partial Years Present", "Financial Data Present",
                 "Pupil Comparator Data Present", "Building Comparator Data Present", "Did Not Submit"]
    return combined.drop(columns=[c for c in drop_cols if c in combined.columns])

def explode_comparators(comp_df, comp_group, feature_df, school_data):
    # For each school, the pupil and buildings comparators groups exist as lists of URNs in the relevant columns
    # This takes one of those columns and makes it long - creating a new row for each URN in the comparator group
    # Then adding in some contextual data
    df = comp_df[["URN", comp_group]].copy().explode(comp_group)
    # Add in contextual data - features used in comparator group calculation
    df = df.merge(feature_df, left_on=comp_group, right_on="URN", how="left")
    df = df.rename(columns={"URN_x": "target_URN", comp_group: "comparator_URN"}).drop(columns=["URN_y"])
    # Add in further contextual data, not used in comparator group calculation
    school_cols = ["URN", "SchoolName", "LAName", "FinanceType", "OverallPhase"]
    df = df.merge(school_data[school_cols], left_on="comparator_URN", right_on="URN", how="left")
    # Add a field to identify the target school within the comparator group
    df["target"] = df["target_URN"] == df["comparator_URN"]
    # Add a field for the calculated geographic distance in km from the target school
    # Note that this dataset contains all schools, so care is needed to make sure the refernece eastings and northings
    # Are linked to the relevant school, and not just to the first school in the dataframe
    coordinate_lookup = feature_df.set_index("URN")[['Easting', 'Northing']]
    ref_eastings = df['target_URN'].map(coordinate_lookup['Easting'])
    ref_northings = df['target_URN'].map(coordinate_lookup['Northing'])
    df['distance_km'] = np.sqrt((df['Easting'] - ref_eastings)**2 + (df['Northing'] - ref_northings)**2)/1000
    return df

def plot_comparator_map(df, target_urn, output_path=None):
    """Generates the geospatial plot."""
    comparator_group = df[df["target_URN"] == target_urn].copy()
    if comparator_group.empty:
        return None
    
    # Create geopandas dataframe
    # coordinates are initially in British National Grid format (EPSG: 27700), and it needs informing of this (the crs argument)
    # but they then need to be converted to Web Mercator format (EPSG: 3857) because that's what OpenStreetMap etc use
    # and without the conversion, the plotted points and the map tiles will not line up
    # note that you have to tell gepandas the original coordinate system, then once that's set convert it - you can't jump straight to the conversion
    gdf = gpd.GeoDataFrame(
        comparator_group,
        geometry=gpd.points_from_xy(comparator_group['Easting'], comparator_group['Northing']),
        crs="EPSG:27700"
    ).to_crs(epsg=3857)

    # Create a plot with all the comparator schools on it as points
    fig, ax = plt.subplots(figsize=(10, 10))
    gdf.plot(ax=ax, alpha=0.5, edgecolor='k', markersize=50)
    
    # Label the points with their URNs
    for x, y, label in zip(gdf.geometry.x, gdf.geometry.y, gdf['comparator_URN']):
        ax.annotate(label,
                    xy=(x,y),
                    xytext=(5,5),
                    textcoords="offset points",
                    fontsize=10)

    # Highlight Target school in red
    gdf[gdf['target']].plot(ax=ax, color='red', markersize=60, edgecolor='k')
    
    # Underlay a geographical reference map - defaulting here to the standard OpenStreetMap
    # Lower detail and black and white alternatives are available, e.g. CartoDB.Positron
    cx.add_basemap(ax, source=cx.providers.OpenStreetMap.Mapnik, alpha=0.5)
    ax.set_axis_off()
    
    if output_path:
        plt.savefig(output_path)
    return fig

def comp_barplot(df, target_urn, metric, threshold='none'):
    # Makes a horizontal barplot showing the distribution of a given feature across the comparator group
    subset = df[df["target_URN"] == target_urn].copy()
    if subset.empty: return None
    # Display the bars from largest value at the top to smallest value at the bottom
    bar_order = subset.sort_values(by=metric, ascending=False)['comparator_URN']
    # Optionally add reference lines at a given threshold above and below the value of the feature for the target school
    target_val = subset[subset["target"] == True][metric].iloc[0]
    
    if threshold != 'none':
        lower, upper = target_val * (1-threshold), target_val * (1+threshold)
    
    g = sns.catplot(data=subset, x=metric, y='comparator_URN', 
                    hue='target', 
                    kind="bar", order=bar_order, orient='h', legend=False)
    if threshold != 'none':
        g.refline(x=lower, color='red', linestyle='--')
        g.refline(x=upper, color='red', linestyle='--')
    g.set_axis_labels(metric, 'comparator_URN',fontsize=20)
    g.set_xticklabels(fontsize=16)
    g.set_yticklabels(fontsize=16)

    return g

def overall_summary_dataframe(df, target_urn, comp_type, features):
    """
    Generate overall summary table across different algorithm choices and comparator groups for a given school
    Used to create a dataframe which can then be embedded as a table in a powerpoint or PDF report summary page
    Expects a dataframe with columns 'algorithm' and 'comparator_type', one row per school, and other columns for the features of interest
    Provided, for example, by the current project by flat_results_df in the main.py script
    """

    #Filter to given comparator type because the features of interest are different between the pupil and the building groups
    #So it makes more sense to present these as separate tables in outputs
    comps = df[(df['target_URN'] == target_urn) & (df['comparator_type'] == comp_type)].copy()
    if comps.empty:
        return pd.DataFrame()
    
    target_vals = comps[comps['target'] == True][features].iloc[0]
    target_vals['algorithm'] = 'target'
    target_vals['distance_km'] = 0
    

    summary_df = comps[comps['target'] == False][['algorithm'] + features]\
        .groupby('algorithm').median().reset_index()
    
    summary_df = pd.concat([target_vals.to_frame().T, summary_df], ignore_index=True)
    summary_df = summary_df[['algorithm'] + features]
    
    return summary_df

