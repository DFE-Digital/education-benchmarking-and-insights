workforce_census_header_row = {
    "default": 5,
    2024: 8,
    2025: 8,
    2026: 0,
}

workforce_census_index_col = "URN"

_workforce_base_cols = {
    "URN": "Int64",
    "Total Number of Other School Support Staff (FTE)": "float",
    "Total Number of Other School Support Staff (Headcount)": "float",
    "Total Number of Technicians (FTE)": "float",
    "Total Number of Technicians (Headcount)": "float",
    "Total Number of Leadership Non-Teachers (FTE)": "float",
    "Total Number of Leadership Non-Teachers (Headcount)": "float",
    "Total Number of School Business Professionals (FTE)": "float",
    "Total Number of School Business Professionals (Headcount)": "float",
    "Total Number of Administrative Staff (FTE)": "float",
    "Total Number of Administrative Staff (Headcount)": "float",
    "Teachers with Qualified Teacher Status (%) (Headcount)": "float",
    "Total Number of Teaching Assistants (FTE)": "float",
    "Total Number of Teaching Assistants (Headcount)": "float",
    "Total School Workforce (FTE)": "float",
    "Total School Workforce (Headcount)": "float",
    "Total Number of Teachers (FTE)": "float",
    "Total Number of Teachers (Headcount)": "float",
    "Total Number of Teachers in the Leadership Group (FTE)": "float",
    "Total Number of Teachers in the Leadership Group (Headcount)": "float",
    "Total Number of Auxiliary Staff (FTE)": "float",
    "Total Number of Auxiliary Staff (Headcount)": "float",
}

_workforce_default = {
    "URN": "Int64",
    "Total Number of Non-Classroom-based School Support Staff, (Other school support staff plus Administrative staff plus Technicians and excluding Auxiliary staff (Full-Time Equivalent)": "float",
    "Total Number of Non Classroom-based School Support Staff, Excluding Auxiliary Staff (Headcount)": "float",
    "Teachers with Qualified Teacher Status (%) (Headcount)": "float",
    "Total Number of Teaching Assistants (Full-Time Equivalent)": "float",
    "Total Number of Teaching Assistants (Headcount)": "float",
    "Total School Workforce (Full-Time Equivalent)": "float",
    "Total Number of Teachers (Full-Time Equivalent)": "float",
    "Total Number of Teachers (Headcount)": "float",
    "Total Number of Teachers in the Leadership Group (Headcount)": "float",
    "Total Number of Teachers in the Leadership Group (Full-time Equivalent)": "float",
    "Total Number of Auxiliary Staff (Full-Time Equivalent)": "float",
    "Total Number of Auxiliary Staff (Headcount)": "float",
    "Total School Workforce (Headcount)": "float",
}

workforce_census = {
    "default": _workforce_default,
    2024: _workforce_base_cols,
    2025: _workforce_base_cols,
    2026: _workforce_base_cols,
}

_workforce_base_mapping = {
    "Total Number of Teaching Assistants (FTE)": "Total Number of Teaching Assistants (Full-Time Equivalent)",
    "Total School Workforce (FTE)": "Total School Workforce (Full-Time Equivalent)",
    "Total Number of Teachers (FTE)": "Total Number of Teachers (Full-Time Equivalent)",
    "Total Number of Teachers in the Leadership Group (FTE)": "Total Number of Teachers in the Leadership Group (Full-time Equivalent)",
    "Total Number of Auxiliary Staff (FTE)": "Total Number of Auxiliary Staff (Full-Time Equivalent)",
}

workforce_census_column_mappings = {
    "default": {},
    2024: _workforce_base_mapping,
    2025: _workforce_base_mapping,
    2026: _workforce_base_mapping,
}

_workforce_base_eval = {
    "Total Number of Non-Classroom-based School Support Staff, (Other school support staff plus Administrative staff plus Technicians and excluding Auxiliary staff (Full-Time Equivalent)": "`Total Number of Other School Support Staff (FTE)` + `Total Number of Technicians (FTE)` + `Total Number of Leadership Non-Teachers (FTE)` + `Total Number of School Business Professionals (FTE)` + `Total Number of Administrative Staff (FTE)`",
    "Total Number of Non Classroom-based School Support Staff, Excluding Auxiliary Staff (Headcount)": "`Total Number of Other School Support Staff (Headcount)` + `Total Number of Technicians (Headcount)` + `Total Number of Leadership Non-Teachers (Headcount)` + `Total Number of School Business Professionals (Headcount)` + `Total Number of Administrative Staff (Headcount)`",
    "SeniorLeadershipHeadcount": "`Total Number of Teachers in the Leadership Group (Headcount)` + `Total Number of Leadership Non-Teachers (Headcount)`",
    "SeniorLeadershipFTE": "`Total Number of Teachers in the Leadership Group (Full-time Equivalent)` + `Total Number of Leadership Non-Teachers (FTE)`",
}

workforce_census_column_eval = {
    "default": {
        "SeniorLeadershipHeadcount": "`Total Number of Teachers in the Leadership Group (Headcount)`",
        "SeniorLeadershipFTE": "`Total Number of Teachers in the Leadership Group (Full-time Equivalent)`",
    },
    2024: _workforce_base_eval,
    2025: _workforce_base_eval,
    2026: _workforce_base_eval,
}


# Pre 2025, we use the historical data from the 2025 file
head_teacher_breakdowns_filenames = {
    2021: "Workforce_2010_2024_fte_hc_nat_reg_la_sch.csv",
    2022: "Workforce_2010_2024_fte_hc_nat_reg_la_sch.csv",
    2023: "Workforce_2010_2024_fte_hc_nat_reg_la_sch.csv",
    2024: "Workforce_2010_2024_fte_hc_nat_reg_la_sch.csv",
    2025: "Workforce_2010_2024_fte_hc_nat_reg_la_sch.csv",
    2026: "Workforce_2010_2025_fte_hc_sch.csv",
}

head_teacher_breakdowns_file_encodings = {"default": "latin-1", 2026: None}

head_teacher_breakdowns = {
    "default": {
        "time_period": "Int64",
        "school_urn": "Int64",
        "fte_head_teachers": "float",
        "fte_deputy_head_teachers": "float",
        "fte_assistant_head_teachers": "float",
        "hc_head_teachers": "float",
        "hc_deputy_head_teachers": "float",
        "hc_assistant_head_teachers": "float",
    }
}
