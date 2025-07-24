from .aar_academies import (  # noqa
    aar_academies,
    aar_academies_column_eval,
    aar_academies_column_mappings,
    aar_academies_index_col,
)
from .aar_central_services import (  # noqa
    aar_central_services,
    aar_central_services_column_eval,
    aar_central_services_column_mappings,
    aar_central_services_index_col,
)
from .census_pupils import (  # noqa
    pupil_census,
    pupil_census_column_mappings,
    pupil_census_index_col,
)
from .census_workforce import (  # noqa
    workforce_census,
    workforce_census_column_eval,
    workforce_census_column_mappings,
    workforce_census_header_row,
    workforce_census_index_col,
)
from .cfo import cfo, cfo_column_eval, cfo_column_mappings  # noqa
from .gias import gias, gias_index_col  # noqa
from .high_exec_pay import high_exec_pay, high_exec_pay_column_mappings  # noqa
from .ilr import (
    ilr_column_mappings,
    ilr_ehcp,
    ilr_ehcp_column_eval,
    ilr_ehcp_header,
    ilr_ehcp_sheet_name,
    ilr_fsm,
    ilr_fsm_column_eval,
    ilr_fsm_header,
    ilr_fsm_sheet_name,
    ilr_index,
)
from .local_authority import (  # noqa
    la_budget,
    la_budget_column_eval,
    la_budget_column_mappings,
    la_budget_pivot,
    la_ons_population,
    la_ons_population_index_column,
    la_outturn,
    la_outturn_column_eval,
    la_outturn_column_mappings,
    la_outturn_pivot,
    la_section_251_category_prefixes,
    la_section_251_column_eval,
    la_section_251_column_mappings,
    la_section_251_columns,
    la_section_251_index_column,
    la_section_251_na_values,
    la_sen2,
    la_sen2_columns,
    la_sen2_eval,
    la_sen2_index_column,
    la_sen2_na_values,
    la_sen2_pivot,
    la_statistical_neighbours,
    la_statistical_neighbours_column_mappings,
    la_statistical_neighbours_index_col,
)
from .maintained_schools_master_list import (
    maintained_schools_master_list_cols,
    maintained_schools_master_list_index_col,
    maintained_schools_master_list_column_eval
)

gias_links_index_col = "URN"
gias_links = {
    "URN": "Int64",
    "LinkURN": "Int64",
    "LinkName": "string",
    "LinkType": "string",
    "LinkEstablishedDate": "string",
}

cdc_index_col = "URN"
cdc = {"URN": "Int64", "GIFA": "float", "Block Age": "string"}

sen_index_col = "URN"
sen = {
    "URN": "Int64",
    "Total pupils": "float",
    "EHC plan": "float",
    "SEN support": "float",
    "EHC_Primary_need_spld": "float",
    "SUP_Primary_need_spld": "float",
    "EHC_Primary_need_mld": "float",
    "SUP_Primary_need_mld": "float",
    "EHC_Primary_need_sld": "float",
    "SUP_Primary_need_sld": "float",
    "EHC_Primary_need_pmld": "float",
    "SUP_Primary_need_pmld": "float",
    "EHC_Primary_need_semh": "float",
    "SUP_Primary_need_semh": "float",
    "EHC_Primary_need_slcn": "float",
    "SUP_Primary_need_slcn": "float",
    "EHC_Primary_need_hi": "float",
    "SUP_Primary_need_hi": "float",
    "EHC_Primary_need_vi": "float",
    "SUP_Primary_need_vi": "float",
    "EHC_Primary_need_msi": "float",
    "SUP_Primary_need_msi": "float",
    "EHC_Primary_need_pd": "float",
    "SUP_Primary_need_pd": "float",
    "EHC_Primary_need_asd": "float",
    "SUP_Primary_need_asd": "float",
    "EHC_Primary_need_oth": "float",
    "SUP_Primary_need_oth": "float",
}

cfr_index_col = "URN"
cfr = {"URN": "Int64"}

ks2_index_col = "URN"
ks2 = {
    "URN": "Int64",
    "READPROG": "string",
    "WRITPROG": "string",
    "MATPROG": "string",
}

ks4_index_col = "URN"
ks4 = {"URN": "Int64", "ATT8SCR": "float", "P8MEA": "float", "P8_BANDING": "string"}

bfr_sofa_cols = {
    "TrustUPIN": "Int64",
    "Title": "string",
    "EFALineNo": "Int64",
    "Y1P1": "float",
    "Y1P2": "float",
    "Y2P1": "float",
    "Y2P2": "float",
}

bfr_3y_cols = {
    "TrustUPIN": "Int64",
    "EFALineNo": "Int64",
    "Y2": "float",
    "Y3": "float",
    "Y4": "float",
}
