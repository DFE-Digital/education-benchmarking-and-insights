import yaml
from src.functions import *
from src.report_generators import generate_pptx

def main():
    with open("config/settings.yaml", "r") as f:
        config = yaml.safe_load(f)
    
    # Load contextual data
    school_data = pd.read_csv(config['context']['schools'], encoding="latin-1")
    features = prepare_features(config['context']['academy_pq'], config['context']['maintained_pq'])
    
    # Process enabled datasets
    # This reads in an processes the data for all schools each time it's run for one school
    # Could be made more efficient by only doing for the target school(s)

    # This reads in and returns the results as a nested dictionary, which is iterated through to produce individual slides
    # Algorithm at the top level, comparator type (pupil, building) at the next level, and the datasets underneath those
    # Useful for iterating through for the individual slides - one slide per algorithm/comparator group type
    results = {}
    for name, info in config['datasets'].items():
        if info['enabled']:
            raw = load_comparator_data(info['path'])
            results[name] = {
                "pupil": explode_comparators(raw, "Pupil", features, school_data),
                "building": explode_comparators(raw, "Building", features, school_data)
            }

    # This produces a flattened version of the dictionary
    flat_results = {
        (algo, comp): df
        for algo, algo_df in results.items()
        for comp, df in algo_df.items()
    }

    # Then turns the flattened dictionary into a dataframe with columns for the algo name and comparator group type
    # Which is useful for producing overall summary comparing different algorithms
    flat_results_df = pd.concat(flat_results.copy()).reset_index()\
        .rename(columns={'level_0': 'algorithm', 'level_1': 'comparator_type'})\
            .drop('level_2', axis=1)
    
    #TO DO: Change this, and the settings file, to loop through a list of URNs instead of single one

    target = config['reporting']['default_target_urn']
    target_name = school_data[school_data['URN'] == target]['SchoolName'].iloc[0]
    #put all of the header info about the target school here and pass to the output?
    
    generate_pptx(target, target_name, results, flat_results_df, f"outputs/{target}_{target_name}.pptx")

if __name__ == "__main__":
    main()
