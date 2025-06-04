import pandas as pd
from pandas._typing import FilePath, ReadCsvBuffer

from pipeline import input_schemas, log
from pipeline.part_year.common import map_has_pupil_comparator_data

from . import gias

logger = log.setup_logger(__name__)


def _build_ilr_fsm_data(
    filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    year: int,
) -> pd.DataFrame:
    """
    Build the ILR-FSM dataset.

    Specifically, this uses the `FSM` worksheet of the ILR Excel file.

    Note: the headers are split across 3 different lines and as such,
    require some manipulation.

    :param filepath_or_buffer: source for ILR data
    :param year: financial year in question
    :return: ILR-FSM data
    """
    ilr_fsm = pd.read_excel(
        filepath_or_buffer,
        header=input_schemas.ilr_fsm_header.get(
            year,
            input_schemas.ilr_fsm_header["default"],
        ),
        sheet_name=input_schemas.ilr_fsm_sheet_name.get(
            year,
            input_schemas.ilr_fsm_sheet_name["default"],
        ),
        engine="calamine",
    )
    # fix the multi-index columns.
    ilr_fsm.columns = [
        b if not b.startswith("Unnamed") else a for a, b in ilr_fsm.columns
    ]
    dtypes = input_schemas.ilr_fsm.get(
        year,
        input_schemas.ilr_fsm["default"],
    )
    index = input_schemas.ilr_index.get(
        year,
        input_schemas.ilr_index["default"],
    )
    ilr_fsm = (
        ilr_fsm[dtypes.keys()].astype(dtypes).dropna(subset=[index]).set_index(index)
    )

    for column, eval_ in input_schemas.ilr_fsm_column_eval.get(
        year,
        input_schemas.ilr_fsm_column_eval["default"],
    ).items():
        ilr_fsm[column] = ilr_fsm.eval(eval_)

    return ilr_fsm


def _build_ilr_ehcp_data(
    filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    year: int,
) -> pd.DataFrame:
    """
    Build the ILR-EHCP dataset.

    Specifically, this uses the `EHCP` worksheet of the ILR Excel file.

    Note: the headers are split across 3 different lines and as such,
    require some manipulation.

    :param filepath_or_buffer: source for ILR data
    :param year: financial year in question
    :return: ILR-EHCP data
    """
    ilr_ehcp = pd.read_excel(
        filepath_or_buffer,
        header=input_schemas.ilr_ehcp_header.get(
            year,
            input_schemas.ilr_ehcp_header["default"],
        ),
        sheet_name=input_schemas.ilr_ehcp_sheet_name.get(
            year,
            input_schemas.ilr_ehcp_sheet_name["default"],
        ),
        engine="calamine",
    )
    # fix the multi-index columns.
    ilr_ehcp.columns = [
        b if not b.startswith("Unnamed") else a for a, b in ilr_ehcp.columns
    ]
    dtypes = input_schemas.ilr_ehcp.get(
        year,
        input_schemas.ilr_ehcp["default"],
    )
    index = input_schemas.ilr_index.get(
        year,
        input_schemas.ilr_index["default"],
    )
    ilr_ehcp = (
        ilr_ehcp[dtypes.keys()].astype(dtypes).dropna(subset=[index]).set_index(index)
    )

    for column, eval_ in input_schemas.ilr_ehcp_column_eval.get(
        year,
        input_schemas.ilr_ehcp_column_eval["default"],
    ).items():
        ilr_ehcp[column] = ilr_ehcp.eval(eval_)

    return ilr_ehcp


def build_ilr_data(
    filepath_or_buffer: FilePath | ReadCsvBuffer[bytes] | ReadCsvBuffer[str],
    schools: pd.DataFrame,
    year: int,
) -> pd.DataFrame:
    """
    Build the ILR dataset.

    - derives its initial data from the FSM and EHCP components of
      the ILR data
    - provide a UKPRN/URN mapping via the `schools` data

    :param filepath_or_buffer: source for ILR data
    :param schools: schools data for UKPRN/URN resolution
    :param year: financial year in question
    :return: ILR data
    """
    columns = input_schemas.ilr_column_mappings.get(
        year, input_schemas.ilr_column_mappings["default"]
    )

    return (
        _build_ilr_fsm_data(filepath_or_buffer, year)
        .join(
            _build_ilr_ehcp_data(filepath_or_buffer, year),
            how="inner",
            lsuffix="",
            rsuffix="_ehcp",
        )
        .reset_index()
        .merge(
            schools.reset_index()[["URN", "UKPRN"]],
            how="inner",
            left_on="UKPRN Current",
            right_on="UKPRN",
        )
        .rename(columns=columns)[columns.values()]
    )


def patch_missing_sixth_form_data(
    df: pd.DataFrame,
    ilr: pd.DataFrame,
    gias_links: pd.DataFrame,
):
    """
    Patch any missing sixth-form census data with ILR.

    Note: the value of `Pupil Comparator Data Present` is recalculated
    due to the update.

    :param df: dataset to be patched (Academy or Maintained)
    :param ilr: ILR data
    :param gias_links: GIAS-links data for predecessor lookups
    :return: data with missing sixth-form data patched where possible
    """
    sixth_form_schools = df[
        df["SchoolPhaseType"].isin(
            [
                "Post-16",
                "University Technical College",
            ]
        )
    ]
    logger.info(
        f"{len(sixth_form_schools.index)} sixth-form orgs. in {len(df.index)} records."
    )
    sixth_form_schools = sixth_form_schools[
        (sixth_form_schools["Number of pupils"].isna())
        | (sixth_form_schools["Percentage Free school meals"].isna())
        | (sixth_form_schools["Percentage SEN"].isna())
    ]
    logger.info(
        f"{len(sixth_form_schools.index)} sixth-form orgs. have missing census data."
    )

    ilr_linked = gias.link_data(
        sixth_form_schools,
        ilr,
        gias_links,
    )
    sixth_form_schools.update(ilr_linked.set_index("URN"), overwrite=False)

    return map_has_pupil_comparator_data(df.combine_first(sixth_form_schools))
