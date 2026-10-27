from io import BytesIO

import numpy as np
import pandas as pd
import pytest

from pipeline.pre_processing.ancillary.ks4 import prepare_ks4_data, prepare_la_ks4_data


def _make_ks4_excel(rows: list[dict]) -> BytesIO:
    default_row = {
        "RECTYPE": 1,
        "LEA": 201,
        "URN": 100150,
        "ATT8SCR": "48.5",
        "P8MEA": "0.1",
        "P8_BANDING": "Average",
    }
    df = pd.DataFrame([{**default_row, **row} for row in rows])
    buffer = BytesIO()
    with pd.ExcelWriter(buffer) as writer:
        df.to_excel(writer, index=False)
    buffer.seek(0)
    return buffer


def test_prepare_ks4_data_has_correct_output_columns(prepared_ks4_data: pd.DataFrame):
    assert list(prepared_ks4_data.columns) == [
        "AverageAttainment",
        "Progress8Measure",
        "Progress8Banding",
    ]


def test_ks4_computes_average_attainment(prepared_ks4_data: pd.DataFrame):
    assert prepared_ks4_data["AverageAttainment"].iloc[0] == 0.1


def test_ks4_progress_eight_measure(prepared_ks4_data: pd.DataFrame):
    assert prepared_ks4_data["Progress8Measure"].iloc[0] == 0.1


def test_ks4_progress_eight_banding(prepared_ks4_data: pd.DataFrame):
    assert prepared_ks4_data["Progress8Banding"].iloc[0] == "Average"


def test_ks4_replaces_average_attainment_supp_with_zero(
    prepared_ks4_data: pd.DataFrame,
):
    assert prepared_ks4_data["AverageAttainment"].iloc[1] == 0


def test_ks4_replaces_average_attainment_ne_with_zero(prepared_ks4_data: pd.DataFrame):
    assert prepared_ks4_data["AverageAttainment"].iloc[2] == 0


def test_prepare_ks4_data_drops_rows_without_urn():
    ks4_excel = _make_ks4_excel(
        [
            {"RECTYPE": 1, "URN": 100150, "P8_BANDING": "SUPP"},
            {"RECTYPE": 4, "URN": np.nan},
            {"RECTYPE": 5, "URN": np.nan},
        ]
    )

    result = prepare_ks4_data(ks4_excel)

    assert list(result.index) == [100150]


def test_prepare_ks4_data_with_none_path_returns_empty_dataframe():
    result = prepare_ks4_data(None)

    assert result.empty


def test_prepare_la_ks4_data_extracts_la_average_attainment():
    ks4_excel = _make_ks4_excel(
        [
            {"RECTYPE": 1, "LEA": 201, "ATT8SCR": "50.1"},
            {"RECTYPE": 4, "LEA": 201, "ATT8SCR": "46.2"},
            {"RECTYPE": 4, "LEA": 202, "ATT8SCR": "44.8"},
        ]
    )

    la_df, _ = prepare_la_ks4_data(ks4_excel, year=2026)

    assert la_df.loc[201, "LocalAuthorityAvgAttainment8"] == pytest.approx(46.2)
    assert la_df.loc[202, "LocalAuthorityAvgAttainment8"] == pytest.approx(44.8)


@pytest.mark.parametrize("suppressed_value", ["NP", "NE", "SUPP", "LOWCOV"])
def test_prepare_la_ks4_data_replaces_suppressed_la_attainment_with_zero(
    suppressed_value: str,
):
    ks4_excel = _make_ks4_excel(
        [{"RECTYPE": 4, "LEA": 201, "ATT8SCR": suppressed_value}]
    )

    la_df, _ = prepare_la_ks4_data(ks4_excel, year=2026)

    assert la_df.loc[201, "LocalAuthorityAvgAttainment8"] == 0.0


def test_prepare_la_ks4_data_extracts_national_average_attainment():
    ks4_excel = _make_ks4_excel(
        [
            {"RECTYPE": 4, "LEA": 201, "ATT8SCR": "46.2"},
            {"RECTYPE": 5, "LEA": np.nan, "ATT8SCR": "45.9"},
        ]
    )

    _, national_avg = prepare_la_ks4_data(ks4_excel, year=2026)

    assert national_avg == pytest.approx(45.9)


def test_prepare_la_ks4_data_without_national_row_returns_nan():
    ks4_excel = _make_ks4_excel([{"RECTYPE": 4, "LEA": 201, "ATT8SCR": "46.2"}])

    _, national_avg = prepare_la_ks4_data(ks4_excel, year=2026)

    assert np.isnan(national_avg)


def test_prepare_la_ks4_data_with_none_path_returns_empty_dataframe_and_nan():
    la_df, national_avg = prepare_la_ks4_data(None, year=2026)

    assert la_df.empty
    assert np.isnan(national_avg)
