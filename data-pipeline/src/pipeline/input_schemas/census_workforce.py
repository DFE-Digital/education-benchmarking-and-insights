workforce_census_header_row = {
    "default": 5,
    2024: 8,
    2025: 8,
}

workforce_census_index_col = "URN"

workforce_census = {
    "default": {
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
    },
    2024: {
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
    },
    2025: {
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
    },
}

workforce_census_column_mappings = {
    "default": {},
    2024: {
        "Total Number of Teaching Assistants (FTE)": "Total Number of Teaching Assistants (Full-Time Equivalent)",
        "Total School Workforce (FTE)": "Total School Workforce (Full-Time Equivalent)",
        "Total Number of Teachers (FTE)": "Total Number of Teachers (Full-Time Equivalent)",
        "Total Number of Teachers in the Leadership Group (FTE)": "Total Number of Teachers in the Leadership Group (Full-time Equivalent)",
        "Total Number of Auxiliary Staff (FTE)": "Total Number of Auxiliary Staff (Full-Time Equivalent)",
    },
    2025: {
        "Total Number of Teaching Assistants (FTE)": "Total Number of Teaching Assistants (Full-Time Equivalent)",
        "Total School Workforce (FTE)": "Total School Workforce (Full-Time Equivalent)",
        "Total Number of Teachers (FTE)": "Total Number of Teachers (Full-Time Equivalent)",
        "Total Number of Teachers in the Leadership Group (FTE)": "Total Number of Teachers in the Leadership Group (Full-time Equivalent)",
        "Total Number of Auxiliary Staff (FTE)": "Total Number of Auxiliary Staff (Full-Time Equivalent)",
    },
}

workforce_census_column_eval = {
    "default": {},
    2024: {
        "Total Number of Non-Classroom-based School Support Staff, (Other school support staff plus Administrative staff plus Technicians and excluding Auxiliary staff (Full-Time Equivalent)": "`Total Number of Other School Support Staff (FTE)` + `Total Number of Technicians (FTE)` + `Total Number of Leadership Non-Teachers (FTE)` + `Total Number of School Business Professionals (FTE)` + `Total Number of Administrative Staff (FTE)`",
        "Total Number of Non Classroom-based School Support Staff, Excluding Auxiliary Staff (Headcount)": "`Total Number of Other School Support Staff (Headcount)` + `Total Number of Technicians (Headcount)` + `Total Number of Leadership Non-Teachers (Headcount)` + `Total Number of School Business Professionals (Headcount)` + `Total Number of Administrative Staff (Headcount)`",
        "SeniorLeadershipHeadcount": "`Total Number of Teachers in the Leadership Group (Headcount)` + `Total Number of Leadership Non-Teachers (Headcount)`",
        "SeniorLeadershipFTE": "`Total Number of Teachers in the Leadership Group (Full-time Equivalent)` + `Total Number of Leadership Non-Teachers (FTE)`"
    },
    2025: {
        "Total Number of Non-Classroom-based School Support Staff, (Other school support staff plus Administrative staff plus Technicians and excluding Auxiliary staff (Full-Time Equivalent)": "`Total Number of Other School Support Staff (FTE)` + `Total Number of Technicians (FTE)` + `Total Number of Leadership Non-Teachers (FTE)` + `Total Number of School Business Professionals (FTE)` + `Total Number of Administrative Staff (FTE)`",
        "Total Number of Non Classroom-based School Support Staff, Excluding Auxiliary Staff (Headcount)": "`Total Number of Other School Support Staff (Headcount)` + `Total Number of Technicians (Headcount)` + `Total Number of Leadership Non-Teachers (Headcount)` + `Total Number of School Business Professionals (Headcount)` + `Total Number of Administrative Staff (Headcount)`",
        "SeniorLeadershipHeadcount": "`Total Number of Teachers in the Leadership Group (Headcount)` + `Total Number of Leadership Non-Teachers (Headcount)`",
        "SeniorLeadershipFTE": "`Total Number of Teachers in the Leadership Group (Full-time Equivalent)` + `Total Number of Leadership Non-Teachers (FTE)`"
    },
}
