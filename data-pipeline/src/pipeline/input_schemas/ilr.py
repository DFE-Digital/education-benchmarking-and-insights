ilr_index = {
    "default": "UKPRN Current",
}

ilr_fsm_header = {
    "default": [9, 11],
}

ilr_ehcp_header = {
    "default": [9, 11],
}

ilr_fsm_sheet_name = {
    "default": "FSM",
}

ilr_ehcp_sheet_name = {
    "default": "EHCP",
}

ilr_fsm = {
    "default": {
        "UKPRN Current": "Int64",
        "16-19 year old learner is eligible for and in receipt of free meals": "float",
        "Grand Total": "float",
    }
}

ilr_ehcp = {
    "default": {
        "UKPRN Current": "Int64",
        "Learner has an Education Health Care plan": "float",
        "Grand Total": "float",
    }
}

ilr_fsm_column_eval = {
    "default": {
        "Percentage Free school meals": "(`16-19 year old learner is eligible for and in receipt of free meals` * 100) / `Grand Total`",
    }
}

ilr_ehcp_column_eval = {
    "default": {
        "Percentage SEN": "(`Learner has an Education Health Care plan` * 100) / `Grand Total`",
    }
}

ilr_column_mappings = {
    "default": {
        "URN": "URN",
        "UKPRN": "UKPRN",
        "Percentage Free school meals": "Percentage Free school meals",
        "Percentage SEN": "Percentage SEN",
        "Grand Total": "Number of pupils",
    },
}
