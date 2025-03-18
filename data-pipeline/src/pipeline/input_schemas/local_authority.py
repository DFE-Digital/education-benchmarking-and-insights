la_section_251_index_column = ["new_la_code", "old_la_code"]
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
        "main_category_planned_expenditure": "string",
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
    }
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
        "main_category": "string",
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
    }
}
la_section_251_na_values = {"default": ["c", "x", "z", ":"]}
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
    )
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
            "main_category_planned_expenditure",
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
    }
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
            "main_category",
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
    }
}

la_budget_column_mappings = {
    "default": {
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
    }
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
    }
}

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
    },
}

la_section_251_column_eval = {
    "default": {
        "TotalHighNeeds": (
            "TotalTopUpFundingMaintained + "
            "TotalTopUpFundingNonMaintained + "
            "TotalSenServices + "
            "TotalAlternativeProvisionServices + "
            "TotalHospitalServices + "
            "TotalOtherHealthServices + "
            "`1.0.2 Net total`"
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
        "GOInd": "Int64",
        "GOReg": "string",
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
la_sen2 = {
    "default": {
        "time_period": "string",
        # "time_identifier": "string",
        # "geographic_level": "string",
        # "country_code": "string",
        # "country_name": "string",
        # "region_code": "string",
        # "region_name": "string",
        "new_la_code": "string",
        "old_la_code": "Int64",
        # "la_name": "string",
        "establishment_group": "string",
        "establishment_type": "string",
        "ehcp_or_statement": "string",
        "num_caseload": "Int64",
        # "pc_caseload": "float",
    },
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
            "`Mainstream school__Mainstream - LA maintained - SEN unit` + "
            "`Mainstream school__Total`"
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
            "`Further education__Independent specialist providers` + "
            "`Further education__Other FE` + "
            "`Further education__Sixth form college`"
        ),
        "EHCPOther": (
            "`Alternative provision/Pupil referral unit__Total` + "
            "`Educated elsewhere__Awaiting provision - above compulsory school age and in education` + "
            "`Educated elsewhere__Awaiting provision - above compulsory school age and not in education` + "
            "`Educated elsewhere__Awaiting provision - below compulsory school age and in education` + "
            "`Educated elsewhere__Awaiting provision - below compulsory school age and not in education` + "
            "`Educated elsewhere__Awaiting provision - compulsory school age and in education` + "
            "`Educated elsewhere__Awaiting provision - compulsory school age and not in education` + "
            "`Educated elsewhere__Awaiting provision - total` + "
            "`Educated elsewhere__Elective home education` + "
            "`Educated elsewhere__Not in education or training - notice to cease issued` + "
            "`Educated elsewhere__Not in education or training - other` + "
            "`Educated elsewhere__Not in education or training - other - compulsory school age` + "
            "`Educated elsewhere__Other arrangements by local authority` + "
            "`Educated elsewhere__Other arrangements by parents` + "
            "`Educated elsewhere__Permanently excluded` + "
            "`Educated elsewhere__Total` + "
            "`Further education__Specialist post-16 institutions` + "
            "`Further education__Total` + "
            "`NEET__NEET` + "
            "`NEET__Total` + "
            "`Non-maintained early years__Non-maintained early years` + "
            "`Non-maintained early years__Total` + "
            "`Other__Other` + "
            "`Other__Total` + "
            "`Other types__Other types` + "
            "`Other types__Total` + "
            "`Special school__Special - Academy` + "
            "`Special school__Special - Free school` + "
            "`Special school__Total` + "
            "`UKRLP provider__Total` + "
            "`UKRLP provider__UKRLP provider` + "
            "`Unknown__Apprenticeship` + "
            "`Unknown__Supported Internship` + "
            "`Unknown__Total` + "
            "`Unknown__Traineeship` + "
            "`Unknown__Unknown` + "
            "`Unknown__Work-based learning`"
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
            "`Mainstream school__Mainstream - LA maintained - SEN unit` + "
            "`Mainstream school__Total`"
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
            "`Further education__Independent specialist providers` + "
            "`Further education__Other FE` + "
            "`Further education__Sixth form college`"
        ),
        "EHCPOther": (
            "`Alternative provision/Pupil referral unit__Total` + "
            "`Educated elsewhere__Awaiting provision - above compulsory school age and in education` + "
            "`Educated elsewhere__Awaiting provision - above compulsory school age and not in education` + "
            "`Educated elsewhere__Awaiting provision - below compulsory school age and in education` + "
            "`Educated elsewhere__Awaiting provision - below compulsory school age and not in education` + "
            "`Educated elsewhere__Awaiting provision - compulsory school age and in education` + "
            "`Educated elsewhere__Awaiting provision - compulsory school age and not in education` + "
            "`Educated elsewhere__Awaiting provision - total` + "
            "`Educated elsewhere__Elective home education` + "
            "`Educated elsewhere__Not in education or training - notice to cease issued` + "
            "`Educated elsewhere__Not in education or training - other` + "
            "`Educated elsewhere__Not in education or training - other - compulsory school age` + "
            "`Educated elsewhere__Other arrangements by local authority` + "
            "`Educated elsewhere__Other arrangements by parents` + "
            "`Educated elsewhere__Permanently excluded` + "
            "`Educated elsewhere__Total` + "
            "`Further education__Specialist post-16 institutions` + "
            "`Further education__Total` + "
            "`NEET__NEET` + "
            "`NEET__Total` + "
            "`Non-maintained early years__Non-maintained early years` + "
            "`Non-maintained early years__Total` + "
            "`Other__Other` + "
            "`Other__Total` + "
            "`Other types__Other types` + "
            "`Other types__Total` + "
            "`Special school__Special - Academy` + "
            "`Special school__Special - Free school` + "
            "`Special school__Total` + "
            "`UKRLP provider__Total` + "
            "`UKRLP provider__UKRLP provider` + "
            "`Unknown__Apprenticeship` + "
            "`Unknown__Supported Internship` + "
            "`Unknown__Total` + "
            "`Unknown__Traineeship` + "
            "`Unknown__Unknown` + "
            "`Unknown__Work-based learning`"
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
            "`Mainstream school__Mainstream - LA maintained - SEN unit` + "
            "`Mainstream school__Total`"
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
            "`Further education__Independent specialist providers` + "
            "`Further education__Other FE` + "
            "`Further education__Sixth form college`"
        ),
        "EHCPOther": (
            "`Alternative provision/Pupil referral unit__Total` + "
            "`Educated elsewhere__Awaiting provision - above compulsory school age and in education` + "
            "`Educated elsewhere__Awaiting provision - above compulsory school age and not in education` + "
            "`Educated elsewhere__Awaiting provision - below compulsory school age and in education` + "
            "`Educated elsewhere__Awaiting provision - below compulsory school age and not in education` + "
            "`Educated elsewhere__Awaiting provision - compulsory school age and in education` + "
            "`Educated elsewhere__Awaiting provision - compulsory school age and not in education` + "
            "`Educated elsewhere__Awaiting provision - total` + "
            "`Educated elsewhere__Elective home education` + "
            "`Educated elsewhere__Not in education or training - notice to cease issued` + "
            "`Educated elsewhere__Not in education or training - other` + "
            "`Educated elsewhere__Not in education or training - other - compulsory school age` + "
            "`Educated elsewhere__Other arrangements by local authority` + "
            "`Educated elsewhere__Other arrangements by parents` + "
            "`Educated elsewhere__Permanently excluded` + "
            "`Educated elsewhere__Total` + "
            "`Further education__Specialist post-16 institutions` + "
            "`Further education__Total` + "
            "`Further education__UKRLP provider` + "
            "`NEET__NEET` + "
            "`NEET__Total` + "
            "`Non-maintained early years__Non-maintained early years` + "
            "`Non-maintained early years__Total` + "
            "`Other__Other` + "
            "`Other__Total` + "
            "`Special school__Special - Academy` + "
            "`Special school__Special - Free school` + "
            "`Special school__Total` + "
            "`Unknown__Apprenticeship` + "
            "`Unknown__Supported Internship` + "
            "`Unknown__Total` + "
            "`Unknown__Traineeship` + "
            "`Unknown__Unknown` + "
            "`Unknown__Work-based learning`"
        ),
    },
}
la_sen2_columns = {k: list(v.keys()) for k, v in la_sen2_eval.items()}
