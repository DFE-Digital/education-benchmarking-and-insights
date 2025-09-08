import pandas as pd

import pipeline.config as config
import pipeline.input_schemas as input_schemas

from ..common.part_year import (
    map_has_building_comparator_data,
    map_has_pupil_comparator_data,
)
from .calculations import (
    calc_base_financials,
    calc_catering_net_costs,
    calc_rag_cost_series,
    create_master_list,
    ensure_it_spend_breakdown_columns_are_present,
    eval_cost_income_categories,
    join_federations,
    map_cost_income_categories,
    map_pfi,
    map_school_attrs,
    map_school_type_attrs,
    map_submission_attrs,
)
from .part_year import map_has_financial_data


def build_maintained_school_data(
    maintained_schools_data_path,
    year,
    gias,
    census,
    sen,
    cdc,
    ks2,
    ks4,
    gias_links,
    ilr,
):
    maintained_school_schema = input_schemas.maintained_schools_master_list_cols.get(
        year, input_schemas.maintained_schools_master_list_cols["default"]
    )
    maintained_schools_list = pd.read_csv(
        maintained_schools_data_path,
        encoding="unicode-escape",
        # TODO: explicit schema as per below?
        # index_col=input_schemas.maintained_schools_master_list_cols_index_col,
        # dtype=maintained_school_schema,
        usecols=maintained_school_schema.keys(),
    )

    maintained_schools = create_master_list(
        maintained_schools_list, gias, sen, census, cdc, ks2, ks4, year
    )

    maintained_schools = ensure_it_spend_breakdown_columns_are_present(
        maintained_schools
    )
    maintained_schools = map_pfi(maintained_schools)
    maintained_schools = map_submission_attrs(maintained_schools)
    maintained_schools = map_school_attrs(maintained_schools)
    maintained_schools = map_school_type_attrs(maintained_schools)
    maintained_schools = calc_base_financials(maintained_schools)
    maintained_schools = map_cost_income_categories(
        maintained_schools,
        config.nonaggregated_cost_category_map["maintained_schools"],
        config.nonaggregated_income_category_map["maintained_schools"],
    )

    maintained_schools_column_eval = (
        input_schemas.maintained_schools_master_list_column_eval.get(
            year, input_schemas.maintained_schools_master_list_column_eval["default"]
        )
    )
    maintained_schools = eval_cost_income_categories(
        maintained_schools, maintained_schools_column_eval
    )

    maintained_schools_column_eval = (
        input_schemas.maintained_schools_master_list_column_eval.get(
            year, input_schemas.maintained_schools_master_list_column_eval["default"]
        )
    )
    maintained_schools = eval_cost_income_categories(
        maintained_schools, maintained_schools_column_eval
    )

    maintained_schools = join_federations(maintained_schools)

    maintained_schools = calc_rag_cost_series(
        maintained_schools, config.rag_category_settings
    )
    maintained_schools = calc_catering_net_costs(maintained_schools)
    maintained_schools = maintained_schools[maintained_schools.index.notnull()]

    # partial-year checks…
    maintained_schools = map_has_financial_data(maintained_schools)
    maintained_schools = map_has_pupil_comparator_data(maintained_schools)
    maintained_schools = map_has_building_comparator_data(maintained_schools)

    return maintained_schools.set_index("URN")
