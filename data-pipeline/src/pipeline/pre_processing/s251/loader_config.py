"""Loader configuration for Section 251 source files."""

la_budget_reader_config = {
    "default": {
        "file_format": "csv",
        "encoding": "utf-8",
        "year_digits": 6,
    },
    2026: {
        "file_format": "excel",
        "engine": "calamine",
        "sheet_name": "Planned_expenditure",
        "header": 1,
        "year_digits": 4,
    },
}

la_outturn_reader_config = {
    "default": {
        "file_format": "csv",
        "encoding": "cp1252",
        "year_digits": 6,
    },
    2026: {
        "file_format": "excel",
        "engine": "calamine",
        "sheet_name": "LA_expenditure_by_year",
        "header": 0,
        "year_digits": 6,
    },
}
