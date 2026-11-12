from .utils import evolve_schema

la_section_251_index_column = ["new_la_code", "old_la_code"]
la_budget_index_column = {
    "default": la_section_251_index_column,
    2026: ["old_la_code"],
}
la_outturn_index_column = {
    "default": la_section_251_index_column,
}
la_budget_filename = {
    "default": "plannedexpenditure_schools_other_education_la_unrounded_data.csv",
    2026: "Local_authority_planned_expenditure_on_education_and_children_s_services_2015_to_2016_financial_year_to_2025_to_2026_financial_year.ods",
}
la_budget = {
    "default": {
        "time_period": "string",
        "time_identifier": "string",
        "geographic_level": "string",
        "country_name": "string",
        "country_code": "string",
        "region_name": "string",
        "region_code": "string",
        "la_name": "string",
        "old_la_code": "Int64",
        "new_la_code": "string",
        "category_of_planned_expenditure": "string",
        "early_years_establishments": "float",
        "primary_schools": "float",
        "secondary_schools": "float",
        "sen_and_special_schools": "float",
        "pupil_referral_units_and_alt_provision": "float",
        "post_16": "float",
        "gross_planned_expenditure": "float",
        "income": "float",
        "net_planned_expenditure": "float",
        "net_per_capita_planned_expenditure": "float",
    },
    2026: {
        "Financial year": "string",
        "Local authority number": "Int64",
        "Local authority name": "string",
        "Description": "string",
        "Early years": "float",
        "Primaries": "float",
        "Secondary": "float",
        "Special educational needs (SEN) special": "float",
        "Alternative Provision(AP)/ Pupil referral unit (PRU)": "float",
        "Post school": "float",
        "Gross": "float",
        "Income": "float",
        "Net": "float",
    },
}
la_budget_raw_column_mappings = {
    "default": {},
    2026: {
        "Financial year": "time_period",
        "Local authority number": "old_la_code",
        "Local authority name": "la_name",
        "Description": "category_of_planned_expenditure",
        "Early years": "early_years_establishments",
        "Primaries": "primary_schools",
        "Secondary": "secondary_schools",
        "Special educational needs (SEN) special": "sen_and_special_schools",
        "Alternative Provision(AP)/ Pupil referral unit (PRU)": "pupil_referral_units_and_alt_provision",
        "Post school": "post_16",
        "Gross": "gross_planned_expenditure",
        "Income": "income",
        "Net": "net_planned_expenditure",
    },
}
la_outturn_filename = {
    "default": "s251_alleducation_la_regional_national.csv",
    2026: "outturn-return-local-authority-expenditure-on-schools-other-education-and-community-for-financial-year-2015-to-2016-to-financial-year-2025-to-2026.xlsx",
}
la_outturn = {
    "default": {
        "time_period": "string",
        "time_identifier": "string",
        "geographic_level": "string",
        "country_name": "string",
        "country_code": "string",
        "region_name": "string",
        "region_code": "string",
        "la_name": "string",
        "old_la_code": "Int64",
        "new_la_code": "string",
        "category_of_expenditure": "string",
        "early_years_establishments": "float",
        "primary_schools": "float",
        "secondary_schools": "float",
        "sen_and_special_schools": "float",
        "pupil_referral_units_and_alt_provision": "float",
        "post_16": "float",
        "gross_expenditure": "float",
        "income": "float",
        "net_expenditure": "float",
        "net_per_capita_expenditure": "float",
    },
    2026: {
        "time_period": "string",
        "time_identifier": "string",
        "geographic_level": "string",
        "country_name": "string",
        "country_code": "string",
        "region_name": "string",
        "region_code": "string",
        "la_name": "string",
        "old_la_code": "Int64",
        "new_la_code": "string",
        "Description": "string",
        "EarlyYears": "float",
        "Primary": "float",
        "Secondary": "float",
        "SenSpecial": "float",
        "APPru": "float",
        "PostSchool": "float",
        "Gross": "float",
        "Income": "float",
        "Net": "float",
    },
}
la_outturn_raw_column_mappings = {
    "default": {},
    2026: {
        "Description": "category_of_expenditure",
        "EarlyYears": "early_years_establishments",
        "Primary": "primary_schools",
        "Secondary": "secondary_schools",
        "SenSpecial": "sen_and_special_schools",
        "APPru": "pupil_referral_units_and_alt_provision",
        "PostSchool": "post_16",
        "Gross": "gross_expenditure",
        "Income": "income",
        "Net": "net_expenditure",
    },
}
la_section_251_na_values = {"default": ["c", "x", "z", ":", "u"]}
la_section_251_category_prefixes = {
    "default": (
        "1.0.2 ",
        "1.2.1 ",
        "1.2.10 ",
        "1.2.11 ",
        "1.2.13 ",
        "1.2.2 ",
        "1.2.3 ",
        "1.2.4 ",
        "1.2.5 ",
        "1.2.6 ",
        "1.2.7 ",
        "1.2.8 ",
        "1.2.8 ",
        "1.2.9 ",
        "1.4.11 ",
        "1.9.3 ",
        "2.1.4 ",
        "2.1.6 ",
        "2.1.7 ",
        "2.1.1 ",
        "2.1.2 ",
    ),
}
la_budget_pivot = {
    "default": {
        "index": [
            "time_period",
            "time_identifier",
            "geographic_level",
            "country_name",
            "country_code",
            "region_name",
            "region_code",
            "la_name",
            "old_la_code",
            "new_la_code",
        ],
        "columns": ["category_of_planned_expenditure"],
        "values": [
            "early_years_establishments",
            "primary_schools",
            "secondary_schools",
            "sen_and_special_schools",
            "pupil_referral_units_and_alt_provision",
            "post_16",
            "gross_planned_expenditure",
            "income",
            "net_planned_expenditure",
            "net_per_capita_planned_expenditure",
        ],
    },
    2026: {
        "index": [
            "time_period",
            "la_name",
            "old_la_code",
        ],
        "columns": ["category_of_planned_expenditure"],
        "values": [
            "early_years_establishments",
            "primary_schools",
            "secondary_schools",
            "sen_and_special_schools",
            "pupil_referral_units_and_alt_provision",
            "post_16",
            "gross_planned_expenditure",
            "income",
            "net_planned_expenditure",
        ],
    },
}
la_outturn_pivot = {
    "default": {
        "index": [
            "time_period",
            "time_identifier",
            "geographic_level",
            "country_name",
            "country_code",
            "region_name",
            "region_code",
            "la_name",
            "old_la_code",
            "new_la_code",
        ],
        "columns": ["category_of_expenditure"],
        "values": [
            "early_years_establishments",
            "primary_schools",
            "secondary_schools",
            "sen_and_special_schools",
            "pupil_referral_units_and_alt_provision",
            "post_16",
            "gross_expenditure",
            "income",
            "net_expenditure",
            "net_per_capita_expenditure",
        ],
    },
    2026: {
        "index": [
            "time_period",
            "time_identifier",
            "geographic_level",
            "country_name",
            "country_code",
            "region_name",
            "region_code",
            "la_name",
            "old_la_code",
            "new_la_code",
        ],
        "columns": ["category_of_expenditure"],
        "values": [
            "early_years_establishments",
            "primary_schools",
            "secondary_schools",
            "sen_and_special_schools",
            "pupil_referral_units_and_alt_provision",
            "post_16",
            "gross_expenditure",
            "income",
            "net_expenditure",
        ],
    },
}

# Used to standardise the names of budget and outturn lines
_la_budget_default_column_mappings = {
    "1.0.2 High needs place funding within Individual Schools Budget, including all pre- and post-16 place funding for maintained schools and academies__early_years_establishments": "1.0.2 EarlyYears",
    "1.0.2 High needs place funding within Individual Schools Budget, including all pre- and post-16 place funding for maintained schools and academies__primary_schools": "1.0.2 Primaries",
    "1.0.2 High needs place funding within Individual Schools Budget, including all pre- and post-16 place funding for maintained schools and academies__secondary_schools": "1.0.2 Secondary",
    "1.0.2 High needs place funding within Individual Schools Budget, including all pre- and post-16 place funding for maintained schools and academies__sen_and_special_schools": "1.0.2 SENSpecial",
    "1.0.2 High needs place funding within Individual Schools Budget, including all pre- and post-16 place funding for maintained schools and academies__pupil_referral_units_and_alt_provision": "1.0.2 APPRU",
    "1.0.2 High needs place funding within Individual Schools Budget, including all pre- and post-16 place funding for maintained schools and academies__post_16": "1.0.2 PostSchool",
    "1.0.2 High needs place funding within Individual Schools Budget, including all pre- and post-16 place funding for maintained schools and academies__net_planned_expenditure": "1.0.2 Net total",
    "1.2.3 Top-up and other funding – non-maintained and independent providers__net_planned_expenditure": "1.2.3 Net total",
    "1.2.3 Top-up and other funding – non-maintained and independent providers__early_years_establishments": "1.2.3 EarlyYears",
    "1.2.3 Top-up and other funding – non-maintained and independent providers__primary_schools": "1.2.3 Primaries",
    "1.2.3 Top-up and other funding – non-maintained and independent providers__secondary_schools": "1.2.3 Secondary",
    "1.2.3 Top-up and other funding – non-maintained and independent providers__sen_and_special_schools": "1.2.3 SENSpecial",
    "1.2.3 Top-up and other funding – non-maintained and independent providers__pupil_referral_units_and_alt_provision": "1.2.3 APPRU",
    "1.2.3 Top-up and other funding – non-maintained and independent providers__post_16": "1.2.3 PostSchool",
    "1.2.3 Top-up and other funding – non-maintained and independent providers__income": "1.2.3 Income",
    "1.2.6 Hospital education services__net_planned_expenditure": "1.2.6 Net total",
    "1.2.7 Other alternative provision services__net_planned_expenditure": "1.2.7 Net total",
    "1.2.13 Therapies and other health related services__net_planned_expenditure": "1.2.13 Net total",
    # note: below renamed to replace non-ASCII characters.
    "1.2.1 Top-up funding – maintained schools__early_years_establishments": "1.2.1 Top-up funding - maintained schools__early_years_establishments",
    "1.2.1 Top-up funding – maintained schools__primary_schools": "1.2.1 Top-up funding - maintained schools__primary_schools",
    "1.2.1 Top-up funding – maintained schools__secondary_schools": "1.2.1 Top-up funding - maintained schools__secondary_schools",
    "1.2.1 Top-up funding – maintained schools__sen_and_special_schools": "1.2.1 Top-up funding - maintained schools__sen_and_special_schools",
    "1.2.1 Top-up funding – maintained schools__pupil_referral_units_and_alt_provision": "1.2.1 Top-up funding - maintained schools__pupil_referral_units_and_alt_provision",
    "1.2.1 Top-up funding – maintained schools__post_16": "1.2.1 Top-up funding - maintained schools__post_16",
    "1.2.1 Top-up funding – maintained schools__gross_planned_expenditure": "1.2.1 Top-up funding - maintained schools__gross_planned_expenditure",
    "1.2.1 Top-up funding – maintained schools__income": "1.2.1 Top-up funding - maintained schools__income",
    "1.2.1 Top-up funding – maintained schools__net_planned_expenditure": "1.2.1 Top-up funding - maintained schools__net_planned_expenditure",
    "1.2.1 Top-up funding – maintained schools__net_per_capita_planned_expenditure": "1.2.1 Top-up funding - maintained schools__net_per_capita_planned_expenditure",
    "1.2.2 Top-up funding – academies, free schools and colleges__early_years_establishments": "1.2.2 Top-up funding - academies, free schools and colleges__early_years_establishments",
    "1.2.2 Top-up funding – academies, free schools and colleges__primary_schools": "1.2.2 Top-up funding - academies, free schools and colleges__primary_schools",
    "1.2.2 Top-up funding – academies, free schools and colleges__secondary_schools": "1.2.2 Top-up funding - academies, free schools and colleges__secondary_schools",
    "1.2.2 Top-up funding – academies, free schools and colleges__sen_and_special_schools": "1.2.2 Top-up funding - academies, free schools and colleges__sen_and_special_schools",
    "1.2.2 Top-up funding – academies, free schools and colleges__pupil_referral_units_and_alt_provision": "1.2.2 Top-up funding - academies, free schools and colleges__pupil_referral_units_and_alt_provision",
    "1.2.2 Top-up funding – academies, free schools and colleges__post_16": "1.2.2 Top-up funding - academies, free schools and colleges__post_16",
    "1.2.2 Top-up funding – academies, free schools and colleges__gross_planned_expenditure": "1.2.2 Top-up funding - academies, free schools and colleges__gross_planned_expenditure",
    "1.2.2 Top-up funding – academies, free schools and colleges__income": "1.2.2 Top-up funding - academies, free schools and colleges__income",
    "1.2.2 Top-up funding – academies, free schools and colleges__net_planned_expenditure": "1.2.2 Top-up funding - academies, free schools and colleges__net_planned_expenditure",
    "1.2.2 Top-up funding – academies, free schools and colleges__net_per_capita_planned_expenditure": "1.2.2 Top-up funding - academies, free schools and colleges__net_per_capita_planned_expenditure",
    "1.4.11 SEN transport__net_planned_expenditure": "1.4.11 SEN transport Net total",
    "1.9.3 Dedicated Schools Grant carried forward to next year__gross_planned_expenditure": "1.9.3 DSG Carried Forward",
    "2.1.4 Home to school transport (pre 16): SEN transport expenditure__net_planned_expenditure": "2.1.4 Home to school transport (pre-16)",
    "2.1.6 Home to post-16 provision: SEN/ LLDD transport expenditure (aged 16-18)__net_planned_expenditure": "2.1.6 Home to school transport (16-18)",
    "2.1.7 Home to post-16 provision: SEN/ LLDD transport expenditure (aged 19-25)__net_planned_expenditure": "2.1.7 Home to school transport (19-25)",
    "2.1.1 Educational psychology service__net_planned_expenditure": "2.1.1 Educational psychology service",
    "2.1.2 SEN administration, assessment and coordination and monitoring__net_planned_expenditure": "2.1.2 SEN Admin",
}
la_budget_column_mappings = {
    "default": _la_budget_default_column_mappings,
    2026: evolve_schema(
        _la_budget_default_column_mappings,
        removals=[
            "1.2.1 Top-up funding – maintained schools__net_per_capita_planned_expenditure",
            "1.2.2 Top-up funding – academies, free schools and colleges__net_per_capita_planned_expenditure",
        ],
        renames={
            "1.0.2 High needs place funding within Individual Schools Budget, including all pre- and post-16 place funding for maintained schools and academies__early_years_establishments": "1.0.2 High needs place funding within Individual Schools Budget (before academy recoupment), excluding funding for 16-19 academies and free schools and FE colleges and independent learning providers__early_years_establishments",
            "1.0.2 High needs place funding within Individual Schools Budget, including all pre- and post-16 place funding for maintained schools and academies__primary_schools": "1.0.2 High needs place funding within Individual Schools Budget (before academy recoupment), excluding funding for 16-19 academies and free schools and FE colleges and independent learning providers__primary_schools",
            "1.0.2 High needs place funding within Individual Schools Budget, including all pre- and post-16 place funding for maintained schools and academies__secondary_schools": "1.0.2 High needs place funding within Individual Schools Budget (before academy recoupment), excluding funding for 16-19 academies and free schools and FE colleges and independent learning providers__secondary_schools",
            "1.0.2 High needs place funding within Individual Schools Budget, including all pre- and post-16 place funding for maintained schools and academies__sen_and_special_schools": "1.0.2 High needs place funding within Individual Schools Budget (before academy recoupment), excluding funding for 16-19 academies and free schools and FE colleges and independent learning providers__sen_and_special_schools",
            "1.0.2 High needs place funding within Individual Schools Budget, including all pre- and post-16 place funding for maintained schools and academies__pupil_referral_units_and_alt_provision": "1.0.2 High needs place funding within Individual Schools Budget (before academy recoupment), excluding funding for 16-19 academies and free schools and FE colleges and independent learning providers__pupil_referral_units_and_alt_provision",
            "1.0.2 High needs place funding within Individual Schools Budget, including all pre- and post-16 place funding for maintained schools and academies__post_16": "1.0.2 High needs place funding within Individual Schools Budget (before academy recoupment), excluding funding for 16-19 academies and free schools and FE colleges and independent learning providers__post_16",
            "1.0.2 High needs place funding within Individual Schools Budget, including all pre- and post-16 place funding for maintained schools and academies__net_planned_expenditure": "1.0.2 High needs place funding within Individual Schools Budget (before academy recoupment), excluding funding for 16-19 academies and free schools and FE colleges and independent learning providers__net_planned_expenditure",
            "1.9.3 Dedicated Schools Grant carried forward to next year__gross_planned_expenditure": "1.9.3 Dedicated Schools Grant carry forward to 2026-27__gross_planned_expenditure",
        },
        additions={
            "1.2.5 SEN support services__net_planned_expenditure": "1.2.5 SEN support service__net_planned_expenditure",
        },
    ),
}
_la_outturn_2025_column_mappings = {
    "1.0.2 High needs place funding within Individual Schools Budget__early_years_establishments": "1.0.2 EarlyYears",
    "1.0.2 High needs place funding within Individual Schools Budget__primary_schools": "1.0.2 Primaries",
    "1.0.2 High needs place funding within Individual Schools Budget__secondary_schools": "1.0.2 Secondary",
    "1.0.2 High needs place funding within Individual Schools Budget__sen_and_special_schools": "1.0.2 SENSpecial",
    "1.0.2 High needs place funding within Individual Schools Budget__pupil_referral_units_and_alt_provision": "1.0.2 APPRU",
    "1.0.2 High needs place funding within Individual Schools Budget__post_16": "1.0.2 PostSchool",
    "1.0.2 High needs place funding within Individual Schools Budget__net_expenditure": "1.0.2 Net total",
    "1.2.3 Top-up and other funding – non-maintained and independent providers__net_expenditure": "1.2.3 Net total",
    "1.2.3 Top-up and other funding – non-maintained and independent providers__early_years_establishments": "1.2.3 EarlyYears",
    "1.2.3 Top-up and other funding – non-maintained and independent providers__primary_schools": "1.2.3 Primaries",
    "1.2.3 Top-up and other funding – non-maintained and independent providers__secondary_schools": "1.2.3 Secondary",
    "1.2.3 Top-up and other funding – non-maintained and independent providers__sen_and_special_schools": "1.2.3 SENSpecial",
    "1.2.3 Top-up and other funding – non-maintained and independent providers__pupil_referral_units_and_alt_provision": "1.2.3 APPRU",
    "1.2.3 Top-up and other funding – non-maintained and independent providers__post_16": "1.2.3 PostSchool",
    "1.2.3 Top-up and other funding – non-maintained and independent providers__income": "1.2.3 Income",
    "1.2.6 Hospital education services__net_expenditure": "1.2.6 Net total",
    "1.2.7 Other alternative provision services__net_expenditure": "1.2.7 Net total",
    "1.2.13 Therapies and other health related services__net_expenditure": "1.2.13 Net total",
    # note: below renamed to replace non-ASCII characters.
    "1.2.1 Top-up funding – maintained schools__early_years_establishments": "1.2.1 Top-up funding - maintained schools__early_years_establishments",
    "1.2.1 Top-up funding – maintained schools__primary_schools": "1.2.1 Top-up funding - maintained schools__primary_schools",
    "1.2.1 Top-up funding – maintained schools__secondary_schools": "1.2.1 Top-up funding - maintained schools__secondary_schools",
    "1.2.1 Top-up funding – maintained schools__sen_and_special_schools": "1.2.1 Top-up funding - maintained schools__sen_and_special_schools",
    "1.2.1 Top-up funding – maintained schools__pupil_referral_units_and_alt_provision": "1.2.1 Top-up funding - maintained schools__pupil_referral_units_and_alt_provision",
    "1.2.1 Top-up funding – maintained schools__post_16": "1.2.1 Top-up funding - maintained schools__post_16",
    "1.2.1 Top-up funding – maintained schools__gross_expenditure": "1.2.1 Top-up funding - maintained schools__gross_expenditure",
    "1.2.1 Top-up funding – maintained schools__income": "1.2.1 Top-up funding - maintained schools__income",
    "1.2.1 Top-up funding – maintained schools__net_expenditure": "1.2.1 Top-up funding - maintained schools__net_expenditure",
    "1.2.1 Top-up funding – maintained schools__net_per_capita_expenditure": "1.2.1 Top-up funding - maintained schools__net_per_capita_expenditure",
    "1.2.2 Top-up funding – academies, free schools and colleges__early_years_establishments": "1.2.2 Top-up funding - academies, free schools and colleges__early_years_establishments",
    "1.2.2 Top-up funding – academies, free schools and colleges__primary_schools": "1.2.2 Top-up funding - academies, free schools and colleges__primary_schools",
    "1.2.2 Top-up funding – academies, free schools and colleges__secondary_schools": "1.2.2 Top-up funding - academies, free schools and colleges__secondary_schools",
    "1.2.2 Top-up funding – academies, free schools and colleges__sen_and_special_schools": "1.2.2 Top-up funding - academies, free schools and colleges__sen_and_special_schools",
    "1.2.2 Top-up funding – academies, free schools and colleges__pupil_referral_units_and_alt_provision": "1.2.2 Top-up funding - academies, free schools and colleges__pupil_referral_units_and_alt_provision",
    "1.2.2 Top-up funding – academies, free schools and colleges__post_16": "1.2.2 Top-up funding - academies, free schools and colleges__post_16",
    "1.2.2 Top-up funding – academies, free schools and colleges__gross_expenditure": "1.2.2 Top-up funding - academies, free schools and colleges__gross_expenditure",
    "1.2.2 Top-up funding – academies, free schools and colleges__income": "1.2.2 Top-up funding - academies, free schools and colleges__income",
    "1.2.2 Top-up funding – academies, free schools and colleges__net_expenditure": "1.2.2 Top-up funding - academies, free schools and colleges__net_expenditure",
    "1.2.2 Top-up funding – academies, free schools and colleges__net_per_capita_expenditure": "1.2.2 Top-up funding - academies, free schools and colleges__net_per_capita_expenditure",
    "1.2.5 SEN support services__net_expenditure": "1.2.5 SEN support service__net_expenditure",
    "1.2.5 SEN support services__early_years_establishments": "1.2.5 SEN support service__early_years_establishments",
    "1.2.5 SEN support services__primary_schools": "1.2.5 SEN support service__primary_schools",
    "1.2.5 SEN support services__secondary_schools": "1.2.5 SEN support service__secondary_schools",
    "1.2.5 SEN support services__sen_and_special_schools": "1.2.5 SEN support service__sen_and_special_schools",
    "1.2.5 SEN support services__pupil_referral_units_and_alt_provision": "1.2.5 SEN support service__pupil_referral_units_and_alt_provision",
    "1.2.5 SEN support services__post_16": "1.2.5 SEN support service__post_16",
    "1.2.5 SEN support services__income": "1.2.5 SEN support service__income",
    "1.4.11 SEN transport__net_expenditure": "1.4.11 SEN transport Net total",
    "1.9.3 Dedicated Schools Grant carried forward to next year__gross_expenditure": "1.9.3 DSG Carried Forward",
    "2.1.4 Home to school transport (pre 16): SEN transport expenditure__net_expenditure": "2.1.4 Home to school transport (pre-16)",
    "2.1.6 Home to post-16 provision: SEN/ LLDD transport expenditure (aged 16-18)__net_expenditure": "2.1.6 Home to school transport (16-18)",
    "2.1.7 Home to post-16 provision: SEN/ LLDD transport expenditure (aged 19-25)__net_expenditure": "2.1.7 Home to school transport (19-25)",
    "2.1.1 Educational psychology service__net_expenditure": "2.1.1 Educational psychology service",
    "2.1.2 SEN administration, assessment and coordination and monitoring__net_expenditure": "2.1.2 SEN Admin",
}
la_outturn_column_mappings = {
    "default": {
        "1.0.2 High needs place funding within Individual Schools Budget__early_years_establishments": "1.0.2 EarlyYears",
        "1.0.2 High needs place funding within Individual Schools Budget__primary_schools": "1.0.2 Primaries",
        "1.0.2 High needs place funding within Individual Schools Budget__secondary_schools": "1.0.2 Secondary",
        "1.0.2 High needs place funding within Individual Schools Budget__sen_and_special_schools": "1.0.2 SENSpecial",
        "1.0.2 High needs place funding within Individual Schools Budget__pupil_referral_units_and_alt_provision": "1.0.2 APPRU",
        "1.0.2 High needs place funding within Individual Schools Budget__post_16": "1.0.2 PostSchool",
        "1.0.2 High needs place funding within Individual Schools Budget__net_expenditure": "1.0.2 Net total",
        "1.2.3 Top-up and other funding – non-maintained and independent providers__net_expenditure": "1.2.3 Net total",
        "1.2.3 Top-up and other funding – non-maintained and independent providers__early_years_establishments": "1.2.3 EarlyYears",
        "1.2.3 Top-up and other funding – non-maintained and independent providers__primary_schools": "1.2.3 Primaries",
        "1.2.3 Top-up and other funding – non-maintained and independent providers__secondary_schools": "1.2.3 Secondary",
        "1.2.3 Top-up and other funding – non-maintained and independent providers__sen_and_special_schools": "1.2.3 SENSpecial",
        "1.2.3 Top-up and other funding – non-maintained and independent providers__pupil_referral_units_and_alt_provision": "1.2.3 APPRU",
        "1.2.3 Top-up and other funding – non-maintained and independent providers__post_16": "1.2.3 PostSchool",
        "1.2.3 Top-up and other funding – non-maintained and independent providers__income": "1.2.3 Income",
        "1.2.6 Hospital education services__net_expenditure": "1.2.6 Net total",
        "1.2.7 Other alternative provision services__net_expenditure": "1.2.7 Net total",
        "1.2.13 Therapies and other health related services__net_expenditure": "1.2.13 Net total",
        # note: below renamed to replace non-ASCII characters.
        "1.2.1 Top-up funding – maintained schools__early_years_establishments": "1.2.1 Top-up funding - maintained schools__early_years_establishments",
        "1.2.1 Top-up funding – maintained schools__primary_schools": "1.2.1 Top-up funding - maintained schools__primary_schools",
        "1.2.1 Top-up funding – maintained schools__secondary_schools": "1.2.1 Top-up funding - maintained schools__secondary_schools",
        "1.2.1 Top-up funding – maintained schools__sen_and_special_schools": "1.2.1 Top-up funding - maintained schools__sen_and_special_schools",
        "1.2.1 Top-up funding – maintained schools__pupil_referral_units_and_alt_provision": "1.2.1 Top-up funding - maintained schools__pupil_referral_units_and_alt_provision",
        "1.2.1 Top-up funding – maintained schools__post_16": "1.2.1 Top-up funding - maintained schools__post_16",
        "1.2.1 Top-up funding – maintained schools__gross_expenditure": "1.2.1 Top-up funding - maintained schools__gross_expenditure",
        "1.2.1 Top-up funding – maintained schools__income": "1.2.1 Top-up funding - maintained schools__income",
        "1.2.1 Top-up funding – maintained schools__net_expenditure": "1.2.1 Top-up funding - maintained schools__net_expenditure",
        "1.2.1 Top-up funding – maintained schools__net_per_capita_expenditure": "1.2.1 Top-up funding - maintained schools__net_per_capita_expenditure",
        "1.2.2 Top-up funding – academies, free schools and colleges__early_years_establishments": "1.2.2 Top-up funding - academies, free schools and colleges__early_years_establishments",
        "1.2.2 Top-up funding – academies, free schools and colleges__primary_schools": "1.2.2 Top-up funding - academies, free schools and colleges__primary_schools",
        "1.2.2 Top-up funding – academies, free schools and colleges__secondary_schools": "1.2.2 Top-up funding - academies, free schools and colleges__secondary_schools",
        "1.2.2 Top-up funding – academies, free schools and colleges__sen_and_special_schools": "1.2.2 Top-up funding - academies, free schools and colleges__sen_and_special_schools",
        "1.2.2 Top-up funding – academies, free schools and colleges__pupil_referral_units_and_alt_provision": "1.2.2 Top-up funding - academies, free schools and colleges__pupil_referral_units_and_alt_provision",
        "1.2.2 Top-up funding – academies, free schools and colleges__post_16": "1.2.2 Top-up funding - academies, free schools and colleges__post_16",
        "1.2.2 Top-up funding – academies, free schools and colleges__gross_expenditure": "1.2.2 Top-up funding - academies, free schools and colleges__gross_expenditure",
        "1.2.2 Top-up funding – academies, free schools and colleges__income": "1.2.2 Top-up funding - academies, free schools and colleges__income",
        "1.2.2 Top-up funding – academies, free schools and colleges__net_expenditure": "1.2.2 Top-up funding - academies, free schools and colleges__net_expenditure",
        "1.2.2 Top-up funding – academies, free schools and colleges__net_per_capita_expenditure": "1.2.2 Top-up funding - academies, free schools and colleges__net_per_capita_expenditure",
        "1.4.11 SEN transport__net_expenditure": "1.4.11 SEN transport Net total",
        "1.9.3 Dedicated Schools Grant carried forward to next year__gross_expenditure": "1.9.3 DSG Carried Forward",
        "2.1.4 Home to school transport (pre 16): SEN transport expenditure__net_expenditure": "2.1.4 Home to school transport (pre-16)",
        "2.1.6 Home to post-16 provision: SEN/ LLDD transport expenditure (aged 16-18)__net_expenditure": "2.1.6 Home to school transport (16-18)",
        "2.1.7 Home to post-16 provision: SEN/ LLDD transport expenditure (aged 19-25)__net_expenditure": "2.1.7 Home to school transport (19-25)",
        "2.1.1 Educational psychology service__net_expenditure": "2.1.1 Educational psychology service",
        "2.1.2 SEN administration, assessment and coordination and monitoring__net_expenditure": "2.1.2 SEN Admin",
    },
    2025: _la_outturn_2025_column_mappings,
    2026: evolve_schema(
        _la_outturn_2025_column_mappings,
        removals=[
            "1.2.1 Top-up funding – maintained schools__net_per_capita_expenditure",
            "1.2.2 Top-up funding – academies, free schools and colleges__net_per_capita_expenditure",
        ],
        renames={
            "1.0.2 High needs place funding within Individual Schools Budget__early_years_establishments": "1.0.2 High needs place funding within Individual Schools Budget (after academies recoupment), including all pre- and post-16 place funding for maintained schools__early_years_establishments",
            "1.0.2 High needs place funding within Individual Schools Budget__primary_schools": "1.0.2 High needs place funding within Individual Schools Budget (after academies recoupment), including all pre- and post-16 place funding for maintained schools__primary_schools",
            "1.0.2 High needs place funding within Individual Schools Budget__secondary_schools": "1.0.2 High needs place funding within Individual Schools Budget (after academies recoupment), including all pre- and post-16 place funding for maintained schools__secondary_schools",
            "1.0.2 High needs place funding within Individual Schools Budget__sen_and_special_schools": "1.0.2 High needs place funding within Individual Schools Budget (after academies recoupment), including all pre- and post-16 place funding for maintained schools__sen_and_special_schools",
            "1.0.2 High needs place funding within Individual Schools Budget__pupil_referral_units_and_alt_provision": "1.0.2 High needs place funding within Individual Schools Budget (after academies recoupment), including all pre- and post-16 place funding for maintained schools__pupil_referral_units_and_alt_provision",
            "1.0.2 High needs place funding within Individual Schools Budget__post_16": "1.0.2 High needs place funding within Individual Schools Budget (after academies recoupment), including all pre- and post-16 place funding for maintained schools__post_16",
            "1.0.2 High needs place funding within Individual Schools Budget__net_expenditure": "1.0.2 High needs place funding within Individual Schools Budget (after academies recoupment), including all pre- and post-16 place funding for maintained schools__net_expenditure",
            "1.2.1 Top-up funding – maintained schools__early_years_establishments": "1.2.1 Top up funding - maintained schools__early_years_establishments",
            "1.2.1 Top-up funding – maintained schools__primary_schools": "1.2.1 Top up funding - maintained schools__primary_schools",
            "1.2.1 Top-up funding – maintained schools__secondary_schools": "1.2.1 Top up funding - maintained schools__secondary_schools",
            "1.2.1 Top-up funding – maintained schools__sen_and_special_schools": "1.2.1 Top up funding - maintained schools__sen_and_special_schools",
            "1.2.1 Top-up funding – maintained schools__pupil_referral_units_and_alt_provision": "1.2.1 Top up funding - maintained schools__pupil_referral_units_and_alt_provision",
            "1.2.1 Top-up funding – maintained schools__post_16": "1.2.1 Top up funding - maintained schools__post_16",
            "1.2.1 Top-up funding – maintained schools__gross_expenditure": "1.2.1 Top up funding - maintained schools__gross_expenditure",
            "1.2.1 Top-up funding – maintained schools__income": "1.2.1 Top up funding - maintained schools__income",
            "1.2.1 Top-up funding – maintained schools__net_expenditure": "1.2.1 Top up funding - maintained schools__net_expenditure",
            "1.9.3 Dedicated Schools Grant carried forward to next year__gross_expenditure": "1.9.3 Dedicated Schools Grant carry forward to 2025-26__gross_expenditure",
            "2.1.6 Home to post-16 provision: SEN/ LLDD transport expenditure (aged 16-18)__net_expenditure": "2.1.6 Home to post-16 provision: SEN/LLDD transport expenditure (aged 16-18)__net_expenditure",
            "2.1.7 Home to post-16 provision: SEN/ LLDD transport expenditure (aged 19-25)__net_expenditure": "2.1.7 Home to post-16 provision: SEN/LLDD transport expenditure (aged 19-25)__net_expenditure",
        },
    ),
}

# Define aggregated columns
la_budget_column_eval = {
    "default": {
        "1.0.2 SENSpecial and APPRU": "`1.0.2 SENSpecial`.fillna(0.0) + `1.0.2 APPRU`.fillna(0.0)",
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 Net total": (
            "`1.2.1 Top-up funding - maintained schools__net_planned_expenditure`.fillna(0.0) + "
            "`1.2.11 Direct payments (SEN and disability)__net_planned_expenditure`.fillna(0.0) + "
            "`1.2.2 Top-up funding - academies, free schools and colleges__net_planned_expenditure`.fillna(0.0) + "
            "`1.2.4 Additional high needs targeted funding for mainstream schools and academies__net_planned_expenditure`.fillna(0.0)"
        ),
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 EarlyYears": (
            "`1.2.1 Top-up funding - maintained schools__early_years_establishments`.fillna(0.0) + "
            "`1.2.11 Direct payments (SEN and disability)__early_years_establishments`.fillna(0.0) + "
            "`1.2.2 Top-up funding - academies, free schools and colleges__early_years_establishments`.fillna(0.0) + "
            "`1.2.4 Additional high needs targeted funding for mainstream schools and academies__early_years_establishments`.fillna(0.0)"
        ),
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 Primaries": (
            "`1.2.1 Top-up funding - maintained schools__primary_schools`.fillna(0.0) + "
            "`1.2.11 Direct payments (SEN and disability)__primary_schools`.fillna(0.0) + "
            "`1.2.2 Top-up funding - academies, free schools and colleges__primary_schools`.fillna(0.0) + "
            "`1.2.4 Additional high needs targeted funding for mainstream schools and academies__primary_schools`.fillna(0.0)"
        ),
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 Secondary": (
            "`1.2.1 Top-up funding - maintained schools__secondary_schools`.fillna(0.0) + "
            "`1.2.11 Direct payments (SEN and disability)__secondary_schools`.fillna(0.0) + "
            "`1.2.2 Top-up funding - academies, free schools and colleges__secondary_schools`.fillna(0.0) + "
            "`1.2.4 Additional high needs targeted funding for mainstream schools and academies__secondary_schools`.fillna(0.0)"
        ),
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 SENSpecial": (
            "`1.2.1 Top-up funding - maintained schools__sen_and_special_schools`.fillna(0.0) + "
            "`1.2.11 Direct payments (SEN and disability)__sen_and_special_schools`.fillna(0.0) + "
            "`1.2.2 Top-up funding - academies, free schools and colleges__sen_and_special_schools`.fillna(0.0) + "
            "`1.2.4 Additional high needs targeted funding for mainstream schools and academies__sen_and_special_schools`.fillna(0.0)"
        ),
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 APPRU": (
            "`1.2.1 Top-up funding - maintained schools__pupil_referral_units_and_alt_provision`.fillna(0.0) + "
            "`1.2.11 Direct payments (SEN and disability)__pupil_referral_units_and_alt_provision`.fillna(0.0) + "
            "`1.2.2 Top-up funding - academies, free schools and colleges__pupil_referral_units_and_alt_provision`.fillna(0.0) + "
            "`1.2.4 Additional high needs targeted funding for mainstream schools and academies__pupil_referral_units_and_alt_provision`.fillna(0.0)"
        ),
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 PostSchool": (
            "`1.2.1 Top-up funding - maintained schools__post_16`.fillna(0.0) + "
            "`1.2.11 Direct payments (SEN and disability)__post_16`.fillna(0.0) + "
            "`1.2.2 Top-up funding - academies, free schools and colleges__post_16`.fillna(0.0) + "
            "`1.2.4 Additional high needs targeted funding for mainstream schools and academies__post_16`.fillna(0.0)"
        ),
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 Income": (
            "`1.2.1 Top-up funding - maintained schools__income`.fillna(0.0) + "
            "`1.2.11 Direct payments (SEN and disability)__income`.fillna(0.0) + "
            "`1.2.2 Top-up funding - academies, free schools and colleges__income`.fillna(0.0) + "
            "`1.2.4 Additional high needs targeted funding for mainstream schools and academies__income`.fillna(0.0)"
        ),
        "1.2.5 + 1.2.8 + 1.2.9 Net total": (
            "`1.2.5 SEN support service__net_planned_expenditure`.fillna(0.0) + "
            "`1.2.8 Support for inclusion__net_planned_expenditure`.fillna(0.0) + "
            "`1.2.9 Special schools and PRUs in financial difficulty__net_planned_expenditure`.fillna(0.0)"
        ),
    },
}
la_outturn_column_eval = {
    "default": {
        "1.0.2 SENSpecial and APPRU": "`1.0.2 SENSpecial`.fillna(0.0) + `1.0.2 APPRU`.fillna(0.0)",
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 Net total": (
            "`1.2.1 Top-up funding - maintained schools__net_expenditure`.fillna(0.0) + "
            "`1.2.11 Direct payments (SEN and disability)__net_expenditure`.fillna(0.0) + "
            "`1.2.2 Top-up funding - academies, free schools and colleges__net_expenditure`.fillna(0.0) + "
            "`1.2.4 Additional high needs targeted funding for mainstream schools and academies__net_expenditure`.fillna(0.0)"
        ),
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 EarlyYears": (
            "`1.2.1 Top-up funding - maintained schools__early_years_establishments`.fillna(0.0) + "
            "`1.2.11 Direct payments (SEN and disability)__early_years_establishments`.fillna(0.0) + "
            "`1.2.2 Top-up funding - academies, free schools and colleges__early_years_establishments`.fillna(0.0) + "
            "`1.2.4 Additional high needs targeted funding for mainstream schools and academies__early_years_establishments`.fillna(0.0)"
        ),
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 Primaries": (
            "`1.2.1 Top-up funding - maintained schools__primary_schools`.fillna(0.0) + "
            "`1.2.11 Direct payments (SEN and disability)__primary_schools`.fillna(0.0) + "
            "`1.2.2 Top-up funding - academies, free schools and colleges__primary_schools`.fillna(0.0) + "
            "`1.2.4 Additional high needs targeted funding for mainstream schools and academies__primary_schools`.fillna(0.0)"
        ),
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 Secondary": (
            "`1.2.1 Top-up funding - maintained schools__secondary_schools`.fillna(0.0) + "
            "`1.2.11 Direct payments (SEN and disability)__secondary_schools`.fillna(0.0) + "
            "`1.2.2 Top-up funding - academies, free schools and colleges__secondary_schools`.fillna(0.0) + "
            "`1.2.4 Additional high needs targeted funding for mainstream schools and academies__secondary_schools`.fillna(0.0)"
        ),
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 SENSpecial": (
            "`1.2.1 Top-up funding - maintained schools__sen_and_special_schools`.fillna(0.0) + "
            "`1.2.11 Direct payments (SEN and disability)__sen_and_special_schools`.fillna(0.0) + "
            "`1.2.2 Top-up funding - academies, free schools and colleges__sen_and_special_schools`.fillna(0.0) + "
            "`1.2.4 Additional high needs targeted funding for mainstream schools and academies__sen_and_special_schools`.fillna(0.0)"
        ),
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 APPRU": (
            "`1.2.1 Top-up funding - maintained schools__pupil_referral_units_and_alt_provision`.fillna(0.0) + "
            "`1.2.11 Direct payments (SEN and disability)__pupil_referral_units_and_alt_provision`.fillna(0.0) + "
            "`1.2.2 Top-up funding - academies, free schools and colleges__pupil_referral_units_and_alt_provision`.fillna(0.0) + "
            "`1.2.4 Additional high needs targeted funding for mainstream schools and academies__pupil_referral_units_and_alt_provision`.fillna(0.0)"
        ),
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 PostSchool": (
            "`1.2.1 Top-up funding - maintained schools__post_16`.fillna(0.0) + "
            "`1.2.11 Direct payments (SEN and disability)__post_16`.fillna(0.0) + "
            "`1.2.2 Top-up funding - academies, free schools and colleges__post_16`.fillna(0.0) + "
            "`1.2.4 Additional high needs targeted funding for mainstream schools and academies__post_16`.fillna(0.0)"
        ),
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 Income": (
            "`1.2.1 Top-up funding - maintained schools__income`.fillna(0.0) + "
            "`1.2.11 Direct payments (SEN and disability)__income`.fillna(0.0) + "
            "`1.2.2 Top-up funding - academies, free schools and colleges__income`.fillna(0.0) + "
            "`1.2.4 Additional high needs targeted funding for mainstream schools and academies__income`.fillna(0.0)"
        ),
        "1.2.5 + 1.2.8 + 1.2.9 Net total": (
            "`1.2.5 SEN support service__net_expenditure`.fillna(0.0) + "
            "`1.2.8 Support for inclusion__net_expenditure`.fillna(0.0) + "
            "`1.2.9 Special schools and PRUs in financial difficulty__net_expenditure`.fillna(0.0)"
        ),
    },
}

# Convert standardised budget/outturn lines to db friendly strings
la_section_251_column_mappings = {
    "default": {
        "1.0.2 SENSpecial and APPRU": "TotalPlaceFunding",
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 Net total": "TotalTopUpFundingMaintained",
        "1.2.3 Net total": "TotalTopUpFundingNonMaintained",
        "1.2.5 + 1.2.8 + 1.2.9 Net total": "TotalSenServices",
        "1.2.7 Net total": "TotalAlternativeProvisionServices",
        "1.2.6 Net total": "TotalHospitalServices",
        "1.2.13 Net total": "TotalOtherHealthServices",
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 EarlyYears": "TopFundingMaintainedEarlyYears",
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 Primaries": "TopFundingMaintainedPrimary",
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 Secondary": "TopFundingMaintainedSecondary",
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 SENSpecial": "TopFundingMaintainedSpecial",
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 APPRU": "TopFundingMaintainedAlternativeProvision",
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 PostSchool": "TopFundingMaintainedPostSchool",
        "1.2.1 + 1.2.2 + 1.2.4 + 1.2.11 Income": "TopFundingMaintainedIncome",
        "1.2.3 EarlyYears": "TopFundingNonMaintainedEarlyYears",
        "1.2.3 Primaries": "TopFundingNonMaintainedPrimary",
        "1.2.3 Secondary": "TopFundingNonMaintainedSecondary",
        "1.2.3 SENSpecial": "TopFundingNonMaintainedSpecial",
        "1.2.3 APPRU": "TopFundingNonMaintainedAlternativeProvision",
        "1.2.3 PostSchool": "TopFundingNonMaintainedPostSchool",
        "1.2.3 Income": "TopFundingNonMaintainedIncome",
        "1.0.2 Primaries": "PlaceFundingPrimary",
        "1.0.2 Secondary": "PlaceFundingSecondary",
        "1.0.2 SENSpecial": "PlaceFundingSpecial",
        "1.0.2 APPRU": "PlaceFundingAlternativeProvision",
        "1.9.3 DSG Carried Forward": "DSGCarriedForward",
        "1.4.11 SEN transport Net total": "SENTransportDSG",
        "2.1.4 Home to school transport (pre-16)": "HometoSchoolTransportPre16",
        "2.1.6 Home to school transport (16-18)": "HometoSchoolTransport1618",
        "2.1.7 Home to school transport (19-25)": "HometoSchoolTransport1925",
        "2.1.1 Educational psychology service": "EdPsychologyService",
        "2.1.2 SEN Admin": "SENAdmin",
    },
}

# This aggregated field is the same for budget/outturn
la_section_251_column_eval = {
    "default": {
        "TotalHighNeeds": (
            "TotalTopUpFundingMaintained + "
            "TotalTopUpFundingNonMaintained + "
            "TotalSenServices + "
            "TotalAlternativeProvisionServices + "
            "TotalHospitalServices + "
            "TotalOtherHealthServices + "
            "`1.0.2 Net total` + "
            "SENTransportDSG"
        ),
    }
}

la_section_251_columns = {
    "default": [
        "TotalHighNeeds",
        "TotalPlaceFunding",
        "TotalTopUpFundingMaintained",
        "TotalTopUpFundingNonMaintained",
        "TotalSenServices",
        "TotalAlternativeProvisionServices",
        "TotalHospitalServices",
        "TotalOtherHealthServices",
        "TopFundingMaintainedEarlyYears",
        "TopFundingMaintainedPrimary",
        "TopFundingMaintainedSecondary",
        "TopFundingMaintainedSpecial",
        "TopFundingMaintainedAlternativeProvision",
        "TopFundingMaintainedPostSchool",
        "TopFundingMaintainedIncome",
        "TopFundingNonMaintainedEarlyYears",
        "TopFundingNonMaintainedPrimary",
        "TopFundingNonMaintainedSecondary",
        "TopFundingNonMaintainedSpecial",
        "TopFundingNonMaintainedAlternativeProvision",
        "TopFundingNonMaintainedPostSchool",
        "TopFundingNonMaintainedIncome",
        "PlaceFundingPrimary",
        "PlaceFundingSecondary",
        "PlaceFundingSpecial",
        "PlaceFundingAlternativeProvision",
        "DSGCarriedForward",
        "SENTransportDSG",
        "HometoSchoolTransportPre16",
        "HometoSchoolTransport1618",
        "HometoSchoolTransport1925",
        "EdPsychologyService",
        "SENAdmin",
    ]
}

# The raw data used to process the statistical neighbours
# contains duplicate column names.
# When reading from the CSV, pandas appends a number
# (e.g., SN1.1) to make them unique.
# The input schema and mappings below rename these columns as needed.
la_statistical_neighbours_index_col = "LA number"
la_statistical_neighbours = {
    "default": {
        "LA number": "Int64",
        "SN1": "string",
        "SN1.1": "string",
        "SN2": "string",
        "SN2.1": "string",
        "SN3": "string",
        "SN3.1": "string",
        "SN4": "string",
        "SN4.1": "string",
        "SN5": "string",
        "SN5.1": "string",
        "SN6": "string",
        "SN6.1": "string",
        "SN7": "string",
        "SN7.1": "string",
        "SN8": "string",
        "SN8.1": "string",
        "SN9": "string",
        "SN9.1": "string",
        "SN10": "string",
        "SN10.1": "string",
        "SN1.2": "Int64",
        "SN2.2": "Int64",
        "SN3.2": "Int64",
        "SN4.2": "Int64",
        "SN5.2": "Int64",
        "SN6.2": "Int64",
        "SN7.2": "Int64",
        "SN8.2": "Int64",
        "SN9.2": "Int64",
        "SN10.2": "Int64",
    }
}

la_statistical_neighbours_column_mappings = {
    "default": {
        "SN1.1": "SN1Prox",
        "SN2.1": "SN2Prox",
        "SN3.1": "SN3Prox",
        "SN4.1": "SN4Prox",
        "SN5.1": "SN5Prox",
        "SN6.1": "SN6Prox",
        "SN7.1": "SN7Prox",
        "SN8.1": "SN8Prox",
        "SN9.1": "SN9Prox",
        "SN10.1": "SN10Prox",
        "SN1.2": "SN1Code",
        "SN2.2": "SN2Code",
        "SN3.2": "SN3Code",
        "SN4.2": "SN4Code",
        "SN5.2": "SN5Code",
        "SN6.2": "SN6Code",
        "SN7.2": "SN7Code",
        "SN8.2": "SN8Code",
        "SN9.2": "SN9Code",
        "SN10.2": "SN10Code",
    },
}

la_ons_population_index_column = "AREA_CODE"
la_ons_population = {
    "default": {
        "AREA_CODE": "string",
        "AGE_GROUP": "string",
    }
}

la_sen2_index_column = ["new_la_code", "old_la_code"]
la_sen2_filenames = {
    "default": "sen2_estab_caseload.csv",
    2025: "caseload.csv",
    2026: "caseload.csv",
}
la_sen2_caseload_drop_columns = {
    "default": [
        "breakdown_topic",
        "mainstream_total_pc",
        "special_total_pc",
        "fe_total_pc",
        "ed_elsewhere_pc",
        "nm_early_years_pc",
        "placement_unknown_pc",
        "ap_pru_total_pc",
    ],
    2025: [
        "breakdown_topic",
        "mainstream_total_pc",
        "special_total_pc",
        "fe_total_pc",
        "ed_elsewhere_pc",
        "nm_early_years_pc",
        "placement_unknown_pc",
        "AP_PRU_total_pc",
    ],
    2026: [
        "breakdown_topic",
        "mainstream_total_pc",
        "special_total_pc",
        "fe_total_pc",
        "ed_elsewhere_pc",
        "nm_early_years_pc",
        "placement_unknown_pc",
        "ap_pru_total_pc",
    ],
}
la_sen2_caseload_na_columns = {
    "default": [
        "Sixth form college",
        "Other FE",
        "Hospital School",
    ],
}
_la_sen2_caseload_column_mappings_2025 = {
    "ehcplans": "Total",
    "mainstream_la_maintained": "Mainstream - LA maintained",
    "mainstream_academy": "Mainstream - Academy",
    "mainstream_la_maintained_resourced_provision": "Mainstream - LA maintained - Resourced provision",
    "ap_pru_academy": "AP/PRU - Academy",
    "ap_pru_free_school": "AP/PRU - Free school",
    "ap_pru_la_maintained": "AP/PRU - LA maintained",
    "general_fe_tertiary_colleges": "General FE and tertiary colleges/HE",
    "mainstream_la_maintained_senunit": "Mainstream - LA maintained - SEN unit",
    "mainstream_academy_senunit": "Mainstream - Academy - SEN unit",
    "mainstream_academy_resourced_provision": "Mainstream - Academy - Resourced Provision",
    "mainstream_free_school_senunit": "Mainstream - Free school - SEN unit",
    "mainstream_free_school_resourced_provision": "Mainstream - Free school - Resourced provision",
    "mainstream_free_school": "Mainstream - Free school",
    "mainstream_independent": "Mainstream - Independent",
    "special_independent": "Special - Independent",
    "special_non_maintained": "Special - Non-maintained",
    "special_la_maintained": "Special - LA maintained",
    "special_academy_free": "Special - Academy/free",
    "specialist_post_16_institutions": "Specialist post-16 institutions",
}
la_sen2_caseload_column_mappings = {
    2025: _la_sen2_caseload_column_mappings_2025,
    2026: evolve_schema(
        _la_sen2_caseload_column_mappings_2025,
        removals=["special_academy_free"],
        additions={
            "special_academy": "Special - Academy/free",
            "special_free_school": "Special - Academy/free",
        },
    ),
}
la_sen2_caseload_id_vars = {
    "default": [
        "time_period",
        "time_identifier",
        "geographic_level",
        "country_code",
        "country_name",
        "region_code",
        "region_name",
        "new_la_code",
        "old_la_code",
        "la_name",
        "breakdown",
    ],
}
la_sen2_caseload_group_rules = {
    "default": {
        "Total": "Total",
        "Mainstream - LA maintained": "Mainstream school",
        "Mainstream - Academy": "Mainstream school",
        "Mainstream - LA maintained - Resourced provision": "Mainstream school",
        "Mainstream - LA maintained - SEN unit": "Mainstream school",
        "Mainstream - Academy - SEN unit": "Mainstream school",
        "Mainstream - Academy - Resourced Provision": "Mainstream school",
        "Mainstream - Free school - SEN unit": "Mainstream school",
        "Mainstream - Free school - Resourced provision": "Mainstream school",
        "Mainstream - Free school": "Mainstream school",
        "Mainstream - Independent": "Mainstream school",
        "Special - Independent": "Special school",
        "Special - Non-maintained": "Special school",
        "Special - LA maintained": "Special school",
        "Special - Academy/free": "Special school",
        "Hospital School": "Special school",
        "AP/PRU - Academy": "Alternative provision/Pupil referral unit",
        "AP/PRU - Free school": "Alternative provision/Pupil referral unit",
        "AP/PRU - LA maintained": "Alternative provision/Pupil referral unit",
        "General FE and tertiary colleges/HE": "Further education",
        "Specialist post-16 institutions": "Further education",
        "Sixth form college": "Further education",
        "Other FE": "Further education",
    },
}
la_sen2 = {
    "default": {
        "time_period": "string",
        "new_la_code": "string",
        "old_la_code": "Int64",
        "establishment_group": "string",
        "establishment_type": "string",
        "ehcp_or_statement": "string",
        "num_caseload": "Int64",
    }
}
la_sen2_na_values = {
    "default": {
        "num_caseload": ["x", "z"],
        "pc_caseload": ["x", "z"],
    }
}
la_sen2_pivot = {
    "default": {
        "index": [
            "new_la_code",
            "old_la_code",
        ],
        "columns": [
            "establishment_group",
            "establishment_type",
        ],
        "values": [
            "num_caseload",
        ],
    }
}
la_sen2_eval = {
    2021: {
        "EHCPTotal": "`Total__Total`",
        "EHCPMainstream": (
            "`Mainstream school__Mainstream - LA maintained` + "
            "`Mainstream school__Mainstream - Free school` + "
            "`Mainstream school__Mainstream - Academy`"
        ),
        "EHCPResourced": (
            "`Mainstream school__Mainstream - Academy - Resourced Provision` + "
            "`Mainstream school__Mainstream - Academy - SEN unit` + "
            "`Mainstream school__Mainstream - Free school - Resourced provision` + "
            "`Mainstream school__Mainstream - Free school - SEN unit` + "
            "`Mainstream school__Mainstream - LA maintained - Resourced provision` + "
            "`Mainstream school__Mainstream - LA maintained - SEN unit`"
        ),
        "EHCPSpecial": (
            "`Special school__Special - LA maintained` + "
            "`Special school__Special - Academy/free`"
        ),
        "EHCPIndependent": (
            "`Mainstream school__Mainstream - Independent` + "
            "`Special school__Special - Independent` + "
            "`Special school__Special - Non-maintained`"
        ),
        "EHCPHospital": (
            "`Alternative provision/Pupil referral unit__AP/PRU - Academy` + "
            "`Alternative provision/Pupil referral unit__AP/PRU - Free school` + "
            "`Alternative provision/Pupil referral unit__AP/PRU - LA maintained` + "
            "`Special school__Hospital School`"
        ),
        "EHCPPost16": (
            "`Further education__General FE and tertiary colleges/HE` + "
            "`Further education__Other FE` + "
            "`Further education__Sixth form college` + "
            "`Further education__Specialist post-16 institutions`"
        ),
        "EHCPOther": (
            "`EHCPTotal` - "
            "`EHCPMainstream` - "
            "`EHCPResourced` - "
            "`EHCPSpecial` - "
            "`EHCPIndependent` - "
            "`EHCPHospital` - "
            "`EHCPPost16`"
        ),
    },
    2022: {
        "EHCPTotal": "`Total__Total`",
        "EHCPMainstream": (
            "`Mainstream school__Mainstream - LA maintained` + "
            "`Mainstream school__Mainstream - Free school` + "
            "`Mainstream school__Mainstream - Academy`"
        ),
        "EHCPResourced": (
            "`Mainstream school__Mainstream - Academy - Resourced Provision` + "
            "`Mainstream school__Mainstream - Academy - SEN unit` + "
            "`Mainstream school__Mainstream - Free school - Resourced provision` + "
            "`Mainstream school__Mainstream - Free school - SEN unit` + "
            "`Mainstream school__Mainstream - LA maintained - Resourced provision` + "
            "`Mainstream school__Mainstream - LA maintained - SEN unit`"
        ),
        "EHCPSpecial": (
            "`Special school__Special - LA maintained` + "
            "`Special school__Special - Academy/free`"
        ),
        "EHCPIndependent": (
            "`Mainstream school__Mainstream - Independent` + "
            "`Special school__Special - Independent` + "
            "`Special school__Special - Non-maintained`"
        ),
        "EHCPHospital": (
            "`Alternative provision/Pupil referral unit__AP/PRU - Academy` + "
            "`Alternative provision/Pupil referral unit__AP/PRU - Free school` + "
            "`Alternative provision/Pupil referral unit__AP/PRU - LA maintained` + "
            "`Special school__Hospital School`"
        ),
        "EHCPPost16": (
            "`Further education__General FE and tertiary colleges/HE` + "
            "`Further education__Other FE` + "
            "`Further education__Sixth form college` + "
            "`Further education__Specialist post-16 institutions`"
        ),
        "EHCPOther": (
            "`EHCPTotal` - "
            "`EHCPMainstream` - "
            "`EHCPResourced` - "
            "`EHCPSpecial` - "
            "`EHCPIndependent` - "
            "`EHCPHospital` - "
            "`EHCPPost16`"
        ),
    },
    "default": {
        "EHCPTotal": "`Total__Total`",
        "EHCPMainstream": (
            "`Mainstream school__Mainstream - LA maintained` + "
            "`Mainstream school__Mainstream - Free school` + "
            "`Mainstream school__Mainstream - Academy`"
        ),
        "EHCPResourced": (
            "`Mainstream school__Mainstream - Academy - Resourced Provision` + "
            "`Mainstream school__Mainstream - Academy - SEN unit` + "
            "`Mainstream school__Mainstream - Free school - Resourced provision` + "
            "`Mainstream school__Mainstream - Free school - SEN unit` + "
            "`Mainstream school__Mainstream - LA maintained - Resourced provision` + "
            "`Mainstream school__Mainstream - LA maintained - SEN unit`"
        ),
        "EHCPSpecial": (
            "`Special school__Special - LA maintained` + "
            "`Special school__Special - Academy/free`"
        ),
        "EHCPIndependent": (
            "`Mainstream school__Mainstream - Independent` + "
            "`Special school__Special - Independent` + "
            "`Special school__Special - Non-maintained`"
        ),
        "EHCPHospital": (
            "`Alternative provision/Pupil referral unit__AP/PRU - Academy` + "
            "`Alternative provision/Pupil referral unit__AP/PRU - Free school` + "
            "`Alternative provision/Pupil referral unit__AP/PRU - LA maintained` + "
            "`Special school__Hospital School`"
        ),
        "EHCPPost16": (
            "`Further education__General FE and tertiary colleges/HE` + "
            "`Further education__Other FE` + "
            "`Further education__Sixth form college` + "
            "`Further education__Specialist post-16 institutions`"
        ),
        "EHCPOther": (
            "`EHCPTotal` - "
            "`EHCPMainstream` - "
            "`EHCPResourced` - "
            "`EHCPSpecial` - "
            "`EHCPIndependent` - "
            "`EHCPHospital` - "
            "`EHCPPost16`"
        ),
    },
    2025: (
        _la_sen2_eval_2025 := {
            "EHCPTotal": "`Total__Total`",
            "EHCPMainstream": (
                "`Mainstream school__Mainstream - LA maintained` + "
                "`Mainstream school__Mainstream - Free school` + "
                "`Mainstream school__Mainstream - Academy`"
            ),
            "EHCPResourced": (
                "`Mainstream school__Mainstream - Academy - Resourced Provision` + "
                "`Mainstream school__Mainstream - Academy - SEN unit` + "
                "`Mainstream school__Mainstream - Free school - Resourced provision` + "
                "`Mainstream school__Mainstream - Free school - SEN unit` + "
                "`Mainstream school__Mainstream - LA maintained - Resourced provision` + "
                "`Mainstream school__Mainstream - LA maintained - SEN unit`"
            ),
            "EHCPSpecial": (
                "`Special school__Special - LA maintained` + "
                "`Special school__Special - Academy/free`"
            ),
            "EHCPIndependent": (
                "`Mainstream school__Mainstream - Independent` + "
                "`Special school__Special - Independent` + "
                "`Special school__Special - Non-maintained`"
            ),
            "EHCPHospital": (
                "`Alternative provision/Pupil referral unit__AP/PRU - Academy` + "
                "`Alternative provision/Pupil referral unit__AP/PRU - Free school` + "
                "`Alternative provision/Pupil referral unit__AP/PRU - LA maintained`"
            ),
            "EHCPPost16": (
                "`Further education__General FE and tertiary colleges/HE` + "
                "`Further education__Specialist post-16 institutions`"
            ),
            "EHCPOther": (
                "`EHCPTotal` - "
                "`EHCPMainstream` - "
                "`EHCPResourced` - "
                "`EHCPSpecial` - "
                "`EHCPIndependent` - "
                "`EHCPHospital` - "
                "`EHCPPost16`"
            ),
        }
    ),
    2026: _la_sen2_eval_2025,
}
la_sen2_columns = {k: list(v.keys()) for k, v in la_sen2_eval.items()}
