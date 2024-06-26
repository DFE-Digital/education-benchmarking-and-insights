import pandas as pd
import numpy as np


def calculate_metrics(bfr: pd.DataFrame) -> pd.DataFrame:
    df = bfr[["Company Registration Number", "Category", "Y1P2"]].pivot_table(index=["Company Registration Number"],
                                                          columns='Category', values='Y1P2')

    df['Revenue reserve as percentage of income'] = (df['Revenue reserve'] / df['Total income']) * 100
    df['Staff costs as percentage of income'] = (df['Staff costs'] / df['Total income']) * 100
    df['Expenditure as percentage of income'] = (df['Total expenditure'] / df['Total income']) * 100
    df['Self generated income as percentage of income'] = (df['Self-generated income'] / df['Total income']) * 100
    df['Grant funding as percentage of income'] = 100 - df['Self generated income as percentage of income']

    return (df.reset_index().melt(id_vars=["Company Registration Number"],
                                  value_vars=[
                                     "Revenue reserve as percentage of income",
                                     "Staff costs as percentage of income",
                                     'Expenditure as percentage of income',
                                     'Self generated income as percentage of income',
                                     'Grant funding as percentage of income'],
                                  value_name="Value")
            .set_index("Company Registration Number")
            .replace([np.inf, -np.inf, np.nan], 0.0)
    )


def calculate_slopes(matrix):
    x = np.array([1, 2, 3, 4, 5, 6])
    x_bar = 3.5
    x_x_bar = x - x_bar
    y_bar = np.nanmean(matrix, axis=1)
    y_y_bar = matrix - np.vstack(y_bar)
    slope_array = np.nansum(x_x_bar * y_y_bar, axis=1) / np.nansum(x_x_bar ** 2)
    return slope_array


def assign_slope_flag(df):
    percentile_10 = np.nanpercentile(df['Slope'].values, 10)
    percentile_90 = np.nanpercentile(df['Slope'].values, 90)
    df['Slope flag'] = 0
    df.loc[df['Slope'] < percentile_10, 'Slope flag'] = -1
    df.loc[df['Slope'] > percentile_90, 'Slope flag'] = 1
    return df


def slope_analysis(bfr):
    year_columns = ["Y-2", "Y-1", "Y1", "Y2", "Y3", "Y4"]
    df = bfr.copy()

    # convert to matrix
    matrix_revenue_reserves = df[year_columns].fillna(0.0).values.astype(float)

    # determine associated slopes
    df['Slope'] = calculate_slopes(matrix_revenue_reserves)

    # flag top 10% and bottom 90% percent of slopes with -1 and 1 respectively
    df = assign_slope_flag(df)
    return (df[["Company Registration Number", "Trust UPIN", "Slope", "Slope flag"]]
            .melt(id_vars=["Company Registration Number", 'Trust UPIN'], value_vars=['Slope', 'Slope flag'])
            .rename(columns={"variable": "Category"})
            .set_index("Company Registration Number")
            )


