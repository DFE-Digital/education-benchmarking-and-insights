from .config import SOFA_IT_SPEND_LINES, SOFA_PUPIL_NUMBER_EFALINE


def melt_it_spend_rows_from_bfr(bfr, current_year):
    it_spend_melted_rows = (
        bfr[bfr["EFALineNo"].isin(SOFA_IT_SPEND_LINES)]
        .assign(
            Y1P_Total=lambda x: x["Y1P1"] + x["Y1P2"],
            Y2P_Total=lambda x: x["Y2P1"] + x["Y2P2"],
            Y3P_Total=lambda x: x["Y3P1"] + x["Y3P2"],
        )
        .melt(
            id_vars=["Category", "Company Registration Number"],
            value_vars=["Y1P_Total", "Y2P_Total", "Y3P_Total"],
            var_name="Year",
            value_name="Value",
        )
        .replace(
            {
                "Y1P_Total": current_year - 1,
                "Y2P_Total": current_year,
                "Y3P_Total": current_year + 1,
            }
        )
        .set_index("Company Registration Number")
        .sort_values(by=["Company Registration Number", "Year"])
    )
    return it_spend_melted_rows


def melt_it_spend_pupil_numbers_from_bfr(bfr, current_year):
    it_spend_pupil_numbers_melted_rows = (
        bfr[bfr["EFALineNo"].isin([SOFA_PUPIL_NUMBER_EFALINE])]
        .assign(
            Y1P1_scaled=lambda x: x["Y1P1"] / 1000,
            Y1P2_scaled=lambda x: x["Y1P2"] / 1000,
            Y2P1_scaled=lambda x: x["Y2P1"] / 1000,
        )
        .melt(
            id_vars=["Company Registration Number"],
            value_vars=["Y1P1_scaled", "Y1P2_scaled", "Y2P1_scaled"],
            var_name="Year",
            value_name="Pupils",
        )
        .replace(
            {
                "Y1P1_scaled": current_year - 1,
                "Y1P2_scaled": current_year,
                "Y2P1_scaled": current_year + 1,
            }
        )
        .set_index("Company Registration Number")
        .sort_values(by=["Company Registration Number", "Year"])
    )
    return it_spend_pupil_numbers_melted_rows


def get_bfr_it_spend_rows(bfr_final_wide, current_year):
    bfr_it_spend_melted_rows = melt_it_spend_rows_from_bfr(bfr_final_wide, current_year)
    bfr_it_spend_pupil_numbers = melt_it_spend_pupil_numbers_from_bfr(
        bfr_final_wide, current_year
    )
    bfr_it_spend_final = bfr_it_spend_melted_rows.merge(
        bfr_it_spend_pupil_numbers,
        how="left",
        on=("Company Registration Number", "Year"),
    )
    return bfr_it_spend_final
