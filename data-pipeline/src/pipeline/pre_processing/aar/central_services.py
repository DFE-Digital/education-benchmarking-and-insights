import logging

import pandas as pd

import pipeline.config as config
import pipeline.input_schemas as input_schemas
import pipeline.pre_processing.common.mappings as mappings

logger = logging.getLogger("fbit-data-pipeline")


def prepare_central_services_data(cs_path, year: int):
    central_services_financial = pd.read_csv(
        cs_path,
        encoding="utf-8",
        usecols=input_schemas.aar_central_services.get(
            year, input_schemas.aar_central_services["default"]
        ).keys(),
        dtype=input_schemas.aar_central_services.get(
            year, input_schemas.aar_central_services["default"]
        ),
    ).rename(
        columns=input_schemas.aar_central_services_column_mappings.get(
            year, input_schemas.aar_central_services_column_mappings["default"]
        ),
    )
    logger.info(
        f"Central Services Data raw {year=} shape: {central_services_financial.shape}"
    )
    central_services_financial = central_services_financial.dropna(
        subset=[input_schemas.aar_central_services_index_col]
    )

    for column, eval_ in input_schemas.aar_central_services_column_eval.get(
        year, input_schemas.aar_central_services_column_eval["default"]
    ).items():
        central_services_financial[column] = central_services_financial.eval(eval_)

    central_services_financial["Income_Direct revenue finance"] = (
        central_services_financial[
            "BNCH21707 (Direct revenue financing (Revenue contributions to capital))"
        ]
    )

    central_services_financial["Income_Total grant funding"] = (
        central_services_financial["BNCH11110T (EFA Revenue Grants)"]
        + central_services_financial["BNCH11131 (DfE Family Revenue Grants)"]
        + central_services_financial["BNCH11141 (SEN)"]
        + central_services_financial["BNCH11142 (Other Revenue)"]
        + central_services_financial["BNCH11151 (Other Government Revenue Grants)"]
        + central_services_financial["BNCH11161 (Government source (non-grant))"]
        + central_services_financial["BNCH11162 (Academies)"]
        + central_services_financial["BNCH11163 (Non- Government)"]
        + central_services_financial[
            "BNCH11123-BTI011-A (MAT Central services - Income)"
        ]
    )

    central_services_financial["Income_Total self generated funding"] = (
        central_services_financial["BNCH11201 (Income from facilities and services)"]
        + central_services_financial["BNCH11202 (Income from catering)"]
        + central_services_financial[
            "BNCH11203 (Receipts from supply teacher insurance claims)"
        ]
        + central_services_financial["BNCH11300T (Voluntary income)"]
        + central_services_financial["BNCH11204 (Other income - revenue)"]
        + central_services_financial[
            "BNCH11205 (Other Income from facilities and services)"
        ]
        + central_services_financial["BNCH11400T (Investment income)"]
    )

    central_services_financial["Income_Direct grants"] = (
        central_services_financial["BNCH11110T (EFA Revenue Grants)"]
        + central_services_financial["BNCH11131 (DfE Family Revenue Grants)"]
        + central_services_financial["BNCH11142 (Other Revenue)"]
        + central_services_financial["BNCH11151 (Other Government Revenue Grants)"]
        + central_services_financial[
            "BNCH11123-BTI011-A (MAT Central services - Income)"
        ]
    )

    central_services_financial["Income_Pre Post 16"] = (
        central_services_financial["BNCH11110T (EFA Revenue Grants)"]
        + central_services_financial["BNCH11131 (DfE Family Revenue Grants)"]
        + central_services_financial[
            "BNCH11123-BTI011-A (MAT Central services - Income)"
        ]
    )

    central_services_financial["Income_Other Revenue Income"] = (
        central_services_financial["BNCH11162 (Academies)"]
        + central_services_financial["BNCH11163 (Non- Government)"]
    )

    central_services_financial["Income_Facilities and services"] = (
        central_services_financial["BNCH11201 (Income from facilities and services)"]
        + central_services_financial[
            "BNCH11205 (Other Income from facilities and services)"
        ]
    )

    central_services_financial["Total Expenditure"] = (
        central_services_financial["BNCH21101 (Teaching staff)"]
        + central_services_financial[
            "BNCH21102 (Supply teaching staff - extra note in guidance)"
        ]
        + central_services_financial["BNCH21103 (Education support staff)"]
        + central_services_financial["BNCH21104 (Administrative and clerical staff)"]
        + central_services_financial["BNCH21105 (Premises staff)"]
        + central_services_financial["BNCH21106 (Catering staff)"]
        + central_services_financial["BNCH21107 (Other staff)"]
        + central_services_financial["BNCH21201 (Indirect employee expenses)"]
        + central_services_financial["BNCH21202 (Staff development and training)"]
        + central_services_financial["BNCH21203 (Staff-related insurance)"]
        + central_services_financial["BNCH21204 (Supply teacher insurance)"]
        + central_services_financial["BNCH21301 (Maintenance of premises)"]
        + central_services_financial["BNCH21405 (Grounds maintenance)"]
        + central_services_financial["BNCH21401 (Cleaning and caretaking)"]
        + central_services_financial["BNCH21402 (Water and sewerage)"]
        + central_services_financial["BNCH21403 (Energy)"]
        + central_services_financial["BNCH21404 (Rent and rates)"]
        + central_services_financial["BNCH21406 (Other occupation costs)"]
        + central_services_financial["BNCH21501 (Special facilities)"]
        + central_services_financial[
            "BNCH21601 (Learning resources (not ICT equipment))"
        ]
        + central_services_financial["BNCH21602 (ICT learning resources)"]
        + central_services_financial["BNCH21603 (Examination fees)"]
        + central_services_financial["BNCH21604 (Educational Consultancy)"]
        + central_services_financial[
            "BNCH21706 (Administrative supplies - non educational)"
        ]
        + central_services_financial["BNCH21606 (Agency supply teaching staff)"]
        + central_services_financial["BNCH21701 (Catering supplies)"]
        + central_services_financial["BNCH21705 (Other insurance premiums)"]
        + central_services_financial[
            "BNCH21702 (Professional Services - non-curriculum)"
        ]
        + central_services_financial["BNCH21703 (Auditor costs)"]
        + central_services_financial["BNCH21801 (Interest charges for Loan and bank)"]
        + central_services_financial["BNCH21802 (PFI Charges)"]
    )

    central_services_financial["Total Income"] = (
        central_services_financial["Income_Total grant funding"]
        + central_services_financial["Income_Total self generated funding"]
        - central_services_financial["Income_Direct revenue finance"]
    )

    central_services_financial["In year balance"] = (
        central_services_financial["Total Income"]
        - central_services_financial["Total Expenditure"]
    )

    central_services_financial.rename(
        columns={
            "Lead_UPIN": "Trust UPIN",
            "Company_Number": "Company Registration Number",
        }
        | config.nonaggregated_cost_category_map["central_services"]
        | config.nonaggregated_income_category_map["central_services"],
        inplace=True,
    )

    central_services_financial["Financial Position"] = central_services_financial[
        "In year balance"
    ].map(mappings.map_is_surplus_deficit)

    return central_services_financial.set_index("Trust UPIN")
