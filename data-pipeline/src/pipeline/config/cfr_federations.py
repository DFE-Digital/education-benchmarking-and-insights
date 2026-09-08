aggregations_default = {
    "Number of pupils": "sum",
    "_Number of pupils FSM": "sum",
    "_Number of pupils SEN": "sum",
    "Total Internal Floor Area": "sum",
    "Building Age": "mean",
    "Total School Workforce (Headcount)": "sum",
    "Total School Workforce (Full-Time Equivalent)": "sum",
    "Total Number of Teachers (Headcount)": "sum",
    "Total Number of Teachers (Full-Time Equivalent)": "sum",
    "SeniorLeadershipHeadcount": "sum",
    "hc_head_teachers": "sum",
    "hc_deputy_head_teachers": "sum",
    "hc_assistant_head_teachers": "sum",
    "SeniorLeadershipFTE": "sum",
    "fte_head_teachers": "sum",
    "fte_deputy_head_teachers": "sum",
    "fte_assistant_head_teachers": "sum",
    "Total Number of Teaching Assistants (Headcount)": "sum",
    "Total Number of Teaching Assistants (Full-Time Equivalent)": "sum",
    "NonClassroomSupportStaffHeadcount": "sum",
    "NonClassroomSupportStaffFTE": "sum",
    "Total Number of Auxiliary Staff (Headcount)": "sum",
    "Total Number of Auxiliary Staff (Full-Time Equivalent)": "sum",
    "_Teachers with QTS (Headcount)": "sum",
}

aggregations_2024 = {
    **aggregations_default,
    "Total Number of Leadership Non-Teachers (Headcount)": "sum",
    "Total Number of Leadership Non-Teachers (FTE)": "sum",
}


def get_federation_lead_school_aggregations(year: int | None) -> dict:
    match year:
        case 2024 | 2025 | 2026 :
            return aggregations_2024
        case _:
            return aggregations_default
