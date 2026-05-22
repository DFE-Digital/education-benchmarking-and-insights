# School Level Comparator Group Analysis

## What is this for?
This analysis visualises the effect of changing the algorithm used to construct comparator groups for FBIT at an individual school level.

For a given choice of algorithm options, it takes the different comparator groups returned for the school for each of those, reads in some contextual information, and produces a Powerpoint slide deck containting geospatial maps to show the geographic distribution of each group, and bar plots to show the distribution of key features within the group. It also includes tables comparing the median values of those features for each group, alongside the actual values for the target school which the comparator groups have been created for.

The slide decks also contain two slides of generic explanatory text which remains the same regardless of comparator group.

The purpose of the slide decks are to allow members of the FBIT team - and potentially stakeholders outside DfE (e.g. school finance staff, local authorities) to visually inspect what "similar" means in the context of each comparator group algorithm choice, and make a judgement about which of these comprise "good" comparator groups. Note that this is a subjective exercise, as no objective target measures are used for similarity.

## Setup
The data required to run the code is output by the FBIT data pipeline.

Because each time the pipeline is run, it persists only one version of the comparator groups (uses one algorithm) in the SQL database, a pragmatic choice was made to save down CSV file copies of the comparator group table output by individual runs for use in this code. So to compare, say, four algorithm choices, run the pipeline for the first, then save a copy of the ComparatorSet table with an appropriate name into the /data folder of this analysis project.

Contextual data is also used. You will need a copy of the "School" table from the SQL database output by the data pipeline, and copies of the "academy_comparators.parquet" and "maintained_schools_comparators.parquet" files which are output to blob storage in the pipeline Azurite container (accessed via Microsoft Azure File Explorer). You will not need to update these after every run - but you will need to update them if you alter the pipeline to include different input features (or change the definitions/processing of input features) etc.

The functions.py file contains the code to create all of the formatted visuals in the powerpoint. These are then called in the report_generators.py script. To change the format or content of the output, change the content of the call to generate_pptx in that script. (Note that multiple library imports are done in the report_generators and functions scripts. It might be cleaner to move these to main.)

This analysis uses some packages/libraries not used by the pipeline (e.g. numpy, contextily, python-pptx). It may be necessary to install them. They are listed in the requirements.txt file.

## Running the code
1. Specify the names of the comparator algorithm choices and the corresponding filepaths in the settings.yaml file, and set the "enabled" value to true if they are needed for the current slide deck.
2. Specify the URN of the target school which the slide deck is to be produced for as the "default_target_urn" in the settings.yaml file.
3. Run the main.py file.

## Outputs
Slide decks will be output to the /outputs folder of this project.

## Future developments
The code currently outputs only Powerpoint format files. It may be relevant to include options for HTML and PDF as well.