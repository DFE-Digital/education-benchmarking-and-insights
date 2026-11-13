import pandas as pd


def calculate_transparency_file_rollups(df: pd.DataFrame) -> pd.DataFrame:
    df["Community Grants"] = df[
        ["Income_Academies", "BNCH11163 (Non- Government)"]
    ].sum(axis=1)
    df["Supply Staff Costs"] = df[
        [
            "Teaching and Teaching support staff_Supply teaching staff",
            "Other costs_Supply teacher insurance",
            "Teaching and Teaching support staff_Agency supply teaching staff",
        ]
    ].sum(axis=1)
    df["Other Staff Costs"] = df[
        [
            "Non-educational support staff and services_Other staff",
            "Other costs_Indirect employee expenses",
            "Other costs_Staff development and training",
            "Other costs_Staff-related insurance",
        ]
    ].sum(axis=1)
    df["Total Staff Costs"] = df[
        [
            "Teaching and Teaching support staff_Teaching staff",
            "Teaching and Teaching support staff_Education support staff",
            "Non-educational support staff and services_Administrative and clerical staff",
            "Non-educational support staff and services_Other staff",  # Other Staff Costs
            "Other costs_Indirect employee expenses",
            "Other costs_Staff development and training",
            "Other costs_Staff-related insurance",  # \Other Staff Costs
            "Teaching and Teaching support staff_Supply teaching staff",  # Supply Staff Costs
            "Other costs_Supply teacher insurance",
            "Teaching and Teaching support staff_Agency supply teaching staff",  # \Supply Staff Costs
        ]
    ].sum(axis=1)
    df["Maintenance & Improvement Costs"] = df[
        [
            "Premises staff and services_Maintenance of premises",
            "Other costs_Grounds maintenance",
        ]
    ].sum(axis=1)
    df["Premises Costs"] = df[
        [
            "Premises staff and services_Maintenance of premises",
            "Other costs_Grounds maintenance",
            "Premises staff and services_Premises staff",
            "Premises staff and services_Cleaning and caretaking",
            "Other costs_PFI charges",
        ]
    ].sum(axis=1)
    df["Catering Expenses"] = df[
        [
            "Catering staff and supplies_Catering staff",  # BNCH21106
            "Catering staff and supplies_Catering supplies",  # BNCH21701
        ]
    ].sum(axis=1)
    df["Occupation Costs"] = df[
        [
            "Catering staff and supplies_Catering staff",  # BNCH21106
            "Catering staff and supplies_Catering supplies",  # BNCH21701
            "Utilities_Water and sewerage",  # BNCH21402
            "Utilities_Energy",  # BNCH21403
            "Other costs_Rent and rates",  # BNCH21404
            "Premises staff and services_Other occupation costs",  # BNCH21406
            "Other costs_Other insurance premiums",  # BNCH21705
        ]
    ].sum(axis=1)

    df["Total Costs of Supplies and Services"] = df[
        [
            "Educational supplies_Learning resources (not ICT equipment)",  # BNCH21601
            "Educational ICT_ICT learning resources",  # BNCH21602
            "Educational supplies_Examination fees",  # BNCH21603
            "Teaching and Teaching support staff_Educational consultancy",  # BNCH21604
            "Administrative supplies_Administrative supplies (non educational)",  # BNCH21706
            "Non-educational support staff and services_Professional services (non-curriculum)",  # BNCH21702
            "Non-educational support staff and services_Audit cost",  # BNCH21703
        ]
    ].sum(axis=1)

    df["Total Costs of Educational Supplies"] = df[
        [
            "Educational supplies_Learning resources (not ICT equipment)",  # BNCH21601
            "Educational ICT_ICT learning resources",  # BNCH21602
            "Educational supplies_Examination fees",  # BNCH21603
        ]
    ].sum(axis=1)

    df["Costs of Brought in Professional Services"] = df[
        [
            "Teaching and Teaching support staff_Educational consultancy",  # BNCH21604
            "Non-educational support staff and services_Professional services (non-curriculum)",  # BNCH21702
            "Non-educational support staff and services_Audit cost",  # BNCH21703
        ]
    ].sum(axis=1)

    df["DFE/EFA Revenue grants"] = df[
        [
            "BNCH11110T (EFA Revenue Grants)",
            "BNCH11131 (DfE Family Revenue Grants)",
            "BNCH11123-BAI011-A (Academies - Income)",
        ]
    ].sum(axis=1)

    return df


def calculate_cs_transparency_file_rollups(cs_df: pd.DataFrame) -> pd.DataFrame:
    cs_df["Maintenance & Improvement"] = cs_df[
        [
            "Premises staff and services_Maintenance of premises",
            "Other costs_Grounds maintenance",
        ]
    ].sum(axis=1)
    cs_df["Premises"] = cs_df[
        [
            "Premises staff and services_Maintenance of premises",
            "Other costs_Grounds maintenance",
            "Premises staff and services_Premises staff",
            "Premises staff and services_Cleaning and caretaking",
            "Other costs_PFI charges",
        ]
    ].sum(axis=1)
    cs_df["Occupation"] = cs_df[
        [
            "Catering staff and supplies_Catering staff",  # BNCH21106
            "Catering staff and supplies_Catering supplies",  # BNCH21701
            "Utilities_Water and sewerage",  # BNCH21402
            "Utilities_Energy",  # BNCH21403
            "Other costs_Rent and rates",  # BNCH21404
            "Premises staff and services_Other occupation costs",  # BNCH21406
            "Other costs_Other insurance premiums",  # BNCH21705
        ]
    ].sum(axis=1)
    cs_df["Supplies and Services"] = cs_df[
        [
            "Educational supplies_Learning resources (not ICT equipment)",  # BNCH21601
            "Educational ICT_ICT learning resources",  # BNCH21602
            "Educational supplies_Examination fees",  # BNCH21603
            "Teaching and Teaching support staff_Educational consultancy",  # BNCH21604
            "Administrative supplies_Administrative supplies (non educational)",  # BNCH21706
            "Non-educational support staff and services_Professional services (non-curriculum)",  # BNCH21702
            "Non-educational support staff and services_Audit cost",  # BNCH21703
        ]
    ].sum(axis=1)
    cs_df["Brought in Professional Services"] = cs_df[
        [
            "Teaching and Teaching support staff_Educational consultancy",
            "Non-educational support staff and services_Professional services (non-curriculum)",
            "Non-educational support staff and services_Audit cost",
        ]
    ].sum(axis=1)
    cs_df["Catering Exp"] = cs_df[
        [
            "Catering staff and supplies_Catering staff",  # BNCH21106
            "Catering staff and supplies_Catering supplies",  # BNCH21701
        ]
    ].sum(axis=1)
    cs_df["Educational Supplies"] = cs_df[
        [
            "Educational supplies_Learning resources (not ICT equipment)",  # BNCH21601
            "Educational ICT_ICT learning resources",  # BNCH21602
            "Educational supplies_Examination fees",  # BNCH21603
        ]
    ].sum(axis=1)
    cs_df["Staff Total"] = cs_df[
        [
            "Teaching and Teaching support staff_Teaching staff",
            "Teaching and Teaching support staff_Education support staff",
            "Non-educational support staff and services_Administrative and clerical staff",
            "Non-educational support staff and services_Other staff",  # Other Staff Costs
            "Other costs_Indirect employee expenses",
            "Other costs_Staff development and training",
            "Other costs_Staff-related insurance",  # \Other Staff Costs
            "Teaching and Teaching support staff_Supply teaching staff",  # Supply Staff Costs
            "Other costs_Supply teacher insurance",
            "Teaching and Teaching support staff_Agency supply teaching staff",  # \Supply Staff Costs
        ]
    ].sum(axis=1)
    cs_df["Supply Staff"] = cs_df[
        [
            "Teaching and Teaching support staff_Supply teaching staff",
            "Other costs_Supply teacher insurance",
            "Teaching and Teaching support staff_Agency supply teaching staff",
        ]
    ].sum(axis=1)
    cs_df["Other Staff Costs"] = cs_df[
        [
            "Non-educational support staff and services_Other staff",
            "Other costs_Indirect employee expenses",
            "Other costs_Staff development and training",
            "Other costs_Staff-related insurance",
        ]
    ].sum(axis=1)

    return cs_df
