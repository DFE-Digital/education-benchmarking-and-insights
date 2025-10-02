bfr_sofa_cols = {
    "default": {
        "TrustUPIN": "Int64",
        "Title": "string",
        "EFALineNo": "Int64",
        "Y1P1": "float",
        "Y1P2": "float",
        "Y2P1": "float",
        "Y2P2": "float",
    },
    2025: {
        "TrustUPIN": "Int64",
        "Title": "string",
        "EFALineNo": "Int64",
        "Y1P1": "float",
        "Y1P2": "float",
        "Y2P1": "float",
        "Y2P2": "float",
        "Y3P1": "float",
        "Y3P2": "float",
    },
}

bfr_3y_cols = {
    "TrustUPIN": "Int64",
    "EFALineNo": "Int64",
    "Y2": "float",
    "Y3": "float",
    "Y4": "float",
}


# BFR SOFA
SOFA_SELF_GENERATED_INCOME_EFALINES = [211, 220]
SOFA_PUPIL_NUMBER_EFALINE = 999
SOFA_GRANT_FUNDING_EFALINES = [199, 200, 205, 210]
SOFA_TRUST_REVENUE_RESERVE_EFALINE = 430
SOFA_SUBTOTAL_INCOME_EFALINE = 980
SOFA_OTHER_COSTS_EFALINE = 335
SOFA_TOTAL_REVENUE_INCOME = 298
SOFA_TOTAL_REVENUE_EXPENDITURE = 380
SOFA_IT_SPEND_LINES = [336, 337, 338, 339, 340, 341, 342]


def get_sofa_year_cols(year: int) -> list[str]:
    """Dynamically returns the SOFA year columns based on the pipeline run year."""
    return list(
        bfr_sofa_cols.get(year, bfr_sofa_cols["default"]).keys()
        - {"TrustUPIN", "Title", "EFALineNo"}
    )


# BFA 3Y
SUBTOTAL_INCOME_EFALINE = 2980
REVENUE_RESERVE_BALANCE_EFALINE = 4300
SUBTOTAL_COSTS_EFALINE = 3800
ESTIMASTED_PUPIL_NUMBERS_EFALINE = 9000
OTHER_COSTS_EFALINE = 335
BFR_3Y_TO_SOFA_MAPPINGS = {
    SUBTOTAL_INCOME_EFALINE: SOFA_TOTAL_REVENUE_INCOME,
    REVENUE_RESERVE_BALANCE_EFALINE: SOFA_TRUST_REVENUE_RESERVE_EFALINE,
    SUBTOTAL_COSTS_EFALINE: SOFA_TOTAL_REVENUE_EXPENDITURE,
    ESTIMASTED_PUPIL_NUMBERS_EFALINE: SOFA_PUPIL_NUMBER_EFALINE,
}
THREE_YEAR_PROJECTION_COLS = ["Y2", "Y3", "Y4"]

BFR_CATEGORY_MAPPINGS = {
    "Balance c/f to next period ": "Revenue reserve",
    "Pupil numbers (actual and estimated)": "Pupil numbers",
    "Total revenue expenditure": "Total expenditure",
    "Total revenue income": "Total income",
    "Total staff costs": "Staff costs",
}
