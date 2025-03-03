import random

import pandas as pd
import pytest


@pytest.fixture
def la_expenditure() -> pd.DataFrame:
    """
    Local Authority expenditure/budget data.
    """
    category_of_planned_expenditure = [
        "1.0.2 High needs place funding within Individual Schools Budget, including all pre- and post-16 place funding for maintained schools and academies",
        "1.2.1 Top-up funding – maintained schools",
        "1.2.2 Top-up funding – academies, free schools and colleges",
        "1.2.3 Top-up and other funding – non-maintained and independent providers",
        "1.2.4 Additional high needs targeted funding for mainstream schools and academies",
        "1.2.5 SEN support service",
        "1.2.6 Hospital education services",
        "1.2.7 Other alternative provision services",
        "1.2.8 Support for inclusion",
        "1.2.9 Special schools and PRUs in financial difficulty",
        "1.2.10 PFI/ BSF costs at special schools, AP/ PRUs and Post 16 institutions only",
        "1.2.11 Direct payments (SEN and disability)",
        "1.2.13 Therapies and other health related services",
    ]

    def _get_str(value: str) -> list[str]:
        return [value] * len(category_of_planned_expenditure)

    def _get_float() -> list[float]:
        return [
            random.uniform(0.0, 1000.0)
            for _ in range(len(category_of_planned_expenditure))
        ]

    la_ex = {
        "time_period": _get_str("202324"),
        "time_identifier": _get_str("Financial year"),
        "geographic_level": _get_str("Local authority"),
        "country_name": _get_str("England"),
        "country_code": _get_str("E92000001"),
        "region_name": _get_str("Yorkshire and The Humber"),
        "region_code": _get_str("E13000001"),
        "la_name": _get_str("West Yorkshire"),
        "old_la_code": _get_str("101"),
        "new_la_code": _get_str("E10000000"),
        "main_category_planned_expenditure": _get_str("Section A: Schools"),
        "category_of_planned_expenditure": category_of_planned_expenditure,
        "early_years_establishments": _get_float(),
        "primary_schools": _get_float(),
        "secondary_schools": _get_float(),
        "sen_and_special_schools": _get_float(),
        "pupil_referral_units_and_alt_provision": _get_float(),
        "post_16": _get_float(),
        "gross_planned_expenditure": _get_float(),
        "income": _get_float(),
        "net_planned_expenditure": _get_float(),
        "net_per_capita_planned_expenditure": _get_float(),
    }

    return pd.DataFrame(la_ex)
