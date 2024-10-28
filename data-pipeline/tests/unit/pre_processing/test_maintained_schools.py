import datetime

import numpy as np
import pandas as pd
import pytest

import src.pipeline.maintained_schools as maintained_schools


def test_create_master_list(
    maintained_schools_master_list: pd.DataFrame,
    prepared_schools_data: pd.DataFrame,
    prepared_sen_data_df: pd.DataFrame,
    prepared_census_data: pd.DataFrame,
    prepared_cdc_data_df: pd.DataFrame,
    prepared_ks2_data: pd.DataFrame,
    prepared_ks4_data: pd.DataFrame,
):
    actual = maintained_schools.create_master_list(
        maintained_schools_master_list,
        prepared_schools_data,
        prepared_sen_data_df,
        prepared_census_data,
        prepared_cdc_data_df,
        prepared_ks2_data,
        prepared_ks4_data,
    )

    assert actual["URN"].isin([100150, 100152, 100153]).all()
    # 100154 is absent from `schools` data.
    assert not actual["URN"].isin([100154]).any()
    # Asserting one column from each of the joined datasets
    assert {
        "Percentage SEN",
        "Number of pupils",
        "Age Average Score",
        "Ks2Progress",
        "Progress8Measure",
    }.issubset(list(actual.columns))
    # Assert DNS is replaced
    assert len(actual[actual.eq("DNS").any(axis=1)]) == 0


def test_maps_pfi_correctly():
    df = pd.DataFrame({"PFI": ["Y", "N"]})

    actual = maintained_schools.map_pfi(df)

    assert actual.iloc[0]["Is PFI"] == True
    assert actual.iloc[1]["Is PFI"] == False


def test_maps_submission_attributes():
    df = pd.DataFrame(
        {
            "Period covered by return (months)": [11, 8, 12],
            "Did Not Supply flag": [0, 0, 0],
        }
    )

    actual = maintained_schools.map_submission_attrs(df)

    assert actual.iloc[0]["Did Not Submit"] == False
    assert actual.iloc[2]["Partial Years Present"] == False
    assert actual.iloc[0]["Partial Years Present"] == True


def test_map_school_attrs():
    df = pd.DataFrame(
        {
            "OfstedLastInsp": ["01/08/2023", "01/09/2023", "01/10/2023"],
            "London Weighting": ["Inner", "Outer", pd.NA],
        }
    )

    actual = maintained_schools.map_school_attrs(df)

    assert actual.iloc[0]["OfstedLastInsp"] == datetime.datetime(2023, 8, 1)
    assert len(actual[actual["London Weighting"] == pd.NA]) == 0
    assert {"Email", "HeadEmail", "Trust Name"}.issubset(list(actual.columns))


def test_school_type_attrs():
    df = pd.DataFrame(
        {
            "Overall Phase": ["Primary", "Secondary"],
            "TypeOfEstablishment (code)": [38, 33],
            "PhaseOfEducation (code)": [0, 0],
        }
    )

    actual = maintained_schools.map_school_type_attrs(df)

    assert (actual["Finance Type"] == "Maintained").all(0)
    assert list(actual["SchoolPhaseType"]) == ["Alternative Provision", "Special"]


@pytest.mark.parametrize(
    "column,expected",
    [
        ("Income_Total grant funding", 3000),
        ("Income_Pre Post 16", 2000),
        ("In year balance", 500),
        ("Financial Position", "Surplus"),
    ],
)
def test_calc_base_financials(
    maintained_schools_master_list: pd.DataFrame, column, expected
):
    actual = maintained_schools.calc_base_financials(
        maintained_schools_master_list
    ).iloc[0]
    assert actual[column] == expected


def test_map_cost_income_categories(maintained_schools_master_list: pd.DataFrame):
    income_mappings = {"I03  SEN funding": "Income_SEN"}

    cost_mappings = {"E06 Catering staff": "Catering staff and supplies_Catering staff"}

    actual = maintained_schools.map_cost_income_categories(
        maintained_schools_master_list, cost_mappings, income_mappings
    )

    assert actual.columns.isin(["Income_SEN"]).any()
    assert actual.columns.isin(["Catering staff and supplies_Catering staff"]).any()


def test_calc_rag_cost_series(
    maintained_schools_master_list: pd.DataFrame,
    prepared_schools_data: pd.DataFrame,
    prepared_sen_data_df: pd.DataFrame,
    prepared_census_data: pd.DataFrame,
    prepared_cdc_data_df: pd.DataFrame,
    prepared_ks2_data: pd.DataFrame,
    prepared_ks4_data: pd.DataFrame,
):

    rag_category_settings = {
        "Teaching and Teaching support staff": {"type": "Pupil"},
        "Premises staff and services": {"type": "Building"},
    }

    merged_data = maintained_schools.create_master_list(
        maintained_schools_master_list,
        prepared_schools_data,
        prepared_sen_data_df,
        prepared_census_data,
        prepared_cdc_data_df,
        prepared_ks2_data,
        prepared_ks4_data,
    )

    merged_data.rename(
        columns={
            "E01  Teaching Staff": "Teaching and Teaching support staff_Teaching staff",
            "E12  Building maintenance and improvement": "Premises staff and services_Maintenance of premises",
        },
        inplace=True,
    )

    actual = maintained_schools.calc_rag_cost_series(merged_data, rag_category_settings)

    assert actual.columns.isin(
        ["Teaching and Teaching support staff_Teaching staff_Per Unit"]
    ).any()
    assert actual.columns.isin(["Teaching and Teaching support staff_Total"]).any()
    assert actual.columns.isin(
        ["Premises staff and services_Maintenance of premises_Per Unit"]
    ).any()
    assert actual.columns.isin(["Premises staff and services_Total"]).any()

    value_row = actual.iloc[0]

    assert value_row["Teaching and Teaching support staff_Total"] == 1000
    assert value_row["Teaching and Teaching support staff_Teaching staff_Per Unit"] == (
        1000 / 320
    )
    assert value_row["Premises staff and services_Total"] == 1000
    assert value_row[
        "Premises staff and services_Maintenance of premises_Per Unit"
    ] == (1000 / 2803.0)


def test_calc_catering_net_costs():
    df = pd.DataFrame(
        {
            "Income_Catering services": [1000, 1100, 1200],
            "Catering staff and supplies_Total": [500, 550, 600],
        }
    )

    actual = maintained_schools.calc_catering_net_costs(df).iloc[0]

    assert actual["Catering staff and supplies_Net Costs"] == -500


def test_federation_mapping(
    maintained_schools_master_list: pd.DataFrame,
    prepared_schools_data: pd.DataFrame,
    prepared_sen_data_df: pd.DataFrame,
    prepared_census_data: pd.DataFrame,
    prepared_cdc_data_df: pd.DataFrame,
    prepared_ks2_data: pd.DataFrame,
    prepared_ks4_data: pd.DataFrame,
):
    master_list = maintained_schools.create_master_list(
        maintained_schools_master_list,
        prepared_schools_data,
        prepared_sen_data_df,
        prepared_census_data,
        prepared_cdc_data_df,
        prepared_ks2_data,
        prepared_ks4_data,
    )
    actual = maintained_schools.join_federations(master_list)

    # Beware: `nan != nan`; 100154 removed as per `test_create_master_list()`.
    assert list(actual["Federation Name"].fillna("")) == ["A", "", "A"]
    assert list(actual["Federation Lead School URN"].fillna(0)) == [100150, 0, 100150]


def test_federation_lead_school_agg_index():
    df = pd.DataFrame(
        {
            "Percentage Free school meals": [25.0, 25.0, 25.0, 25.0],
            "Percentage SEN": [50.0, 50.0, 50.0, 50.0],
            "Number of pupils": [1_000, 1_000, 1_000, 1_000],
            "Lead school in federation": ["10000", "10001", "10002", "10000"],
            "Total Internal Floor Area": [1_000, 1_000, 1_000, 1_000],
            "Building Age": [1990, 1990, 1990, 2000],
        }
    )

    actual = maintained_schools._federation_lead_school_agg(df)

    assert len(actual.index) == 3
    assert actual.index.name == "Federation LAEstab"


def test_federation_lead_school_agg_pupils():
    df = pd.DataFrame(
        {
            "Percentage Free school meals": [25.0, 25.0, 25.0, 25.0],
            "Percentage SEN": [50.0, 50.0, 50.0, 50.0],
            "Number of pupils": [1_000, 1_000, 1_000, 1_000],
            "Lead school in federation": ["10000", "10001", "10002", "10000"],
            "Total Internal Floor Area": [1_000, 1_000, 1_000, 1_000],
            "Building Age": [1990, 1990, 1990, 2000],
        }
    )

    actual = maintained_schools._federation_lead_school_agg(df)

    assert actual.loc["10000", "Number of pupils"] == 2_000
    assert actual.loc["10001", "Number of pupils"] == 1_000
    assert actual.loc["10002", "Number of pupils"] == 1_000


def test_federation_lead_school_agg_fsm():
    df = pd.DataFrame(
        {
            "Percentage Free school meals": [10.0, 25.0, 25.0, 50.0],
            "Percentage SEN": [50.0, 50.0, 50.0, 50.0],
            "Number of pupils": [1_000, 1_000, 1_000, 1_000],
            "Lead school in federation": ["10000", "10001", "10002", "10000"],
            "Total Internal Floor Area": [1_000, 1_000, 1_000, 1_000],
            "Building Age": [1990, 1990, 1990, 2000],
        }
    )

    actual = maintained_schools._federation_lead_school_agg(df)

    assert actual.loc["10000", "Percentage Free school meals"] == 30.0
    assert actual.loc["10001", "Percentage Free school meals"] == 25.0
    assert actual.loc["10002", "Percentage Free school meals"] == 25.0


def test_federation_lead_school_agg_sen():
    df = pd.DataFrame(
        {
            "Percentage Free school meals": [25.0, 25.0, 25.0, 25.0],
            "Percentage SEN": [10.0, 25.0, 25.0, 50.0],
            "Number of pupils": [1_000, 1_000, 1_000, 1_000],
            "Lead school in federation": ["10000", "10001", "10002", "10000"],
            "Total Internal Floor Area": [1_000, 1_000, 1_000, 1_000],
            "Building Age": [1990, 1990, 1990, 2000],
        }
    )

    actual = maintained_schools._federation_lead_school_agg(df)

    assert actual.loc["10000", "Percentage SEN"] == 30.0
    assert actual.loc["10001", "Percentage SEN"] == 25.0
    assert actual.loc["10002", "Percentage SEN"] == 25.0


def test_federation_lead_school_agg_building_age():
    df = pd.DataFrame(
        {
            "Percentage Free school meals": [25.0, 25.0, 25.0, 25.0],
            "Percentage SEN": [50.0, 50.0, 50.0, 50.0],
            "Number of pupils": [1_000, 1_000, 1_000, 1_000],
            "Lead school in federation": ["10000", "10001", "10002", "10000"],
            "Total Internal Floor Area": [1_000, 1_000, 1_000, 1_000],
            "Building Age": [1990, 1990, 1990, 2000],
        }
    )

    actual = maintained_schools._federation_lead_school_agg(df)

    assert actual.loc["10000", "Building Age"] == 1995.0
    assert actual.loc["10001", "Building Age"] == 1990.0
    assert actual.loc["10002", "Building Age"] == 1990.0
