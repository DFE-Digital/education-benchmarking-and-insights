# from dataclasses import dataclass
# from typing import Optional


# @dataclass
# class FinancialReportDataField:
#     field_name: str
#     dtype: str
#     db_projection: str


# @dataclass
# class FinancialReportDataSchema:
#     schema: FinancialReportDataField


# @dataclass
# class FinancialReportDataColumnEvals:
#     column_evals: dict[str, str]


# @dataclass
# class FinancialReportData:
#     year: int
#     filepath: str
#     schema: FinancialReportDataSchema
#     column_evals: Optional[FinancialReportDataColumnEvals]


# domain_heirarchy: dict = {
#     "financial_data": {
#         "academies": {
#             "AAR": {
#                 "ancillary_data": {
#                     "cdc": {},
#                     "census_pupil": {},
#                     "csnsus_workforce": {},
#                     "gias": {},
#                     "cfo": {},
#                     "ks2": {},
#                     "ks4": {},
#                     "sen": {},
#                     "HExP": {},
#                 }
#             },
#             "BFR": {
#                 "ancillary_data": {}
#             }
#         },
#         "la_maintained": {
#             "CFR": {
#                  "ancillary_data": {
#                     "cdc": {},
#                     "census_pupil": {},
#                     "csnsus_workforce": {},
#                     "gias": {},
#                     "ks2": {},
#                     "ks4": {},
#                     "sen": {},
#                     "ilr": {},
#                  }
#             },
#             "S251": {
#                  "ancillary_data": {
#                     "sen": {},
#                     "ehcp": {},
#                     "SNPP population": {}
#                 }
#             },
#         }
#     }
# }

# class FBITPipelineRunSchemas:
#     """Holds the required schemas for a pipeline run"""
#     def __init__(self, input_json: dict) -> None:
#         self.pipeline_run_id = input_json["runId"]
#         aar_year: int = input_json["year"]["aar"]
#         bfr_year: int = input_json["year"]["bfr"]
#         cfr_year: int = input_json["year"]["cfr"]
#         s251_year: int = input_json["year"]["s251"]
