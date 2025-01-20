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

gias_index_col = "URN"
gias = {
    "URN": "Int64",
    "UKPRN": "Int64",
    "LA (code)": "Int64",
    "LA (name)": "string",
    "EstablishmentNumber": "Int64",
    "EstablishmentName": "string",
    "TypeOfEstablishment (code)": "Int64",
    "TypeOfEstablishment (name)": "string",
    "OpenDate": "string",
    "CloseDate": "string",
    "PhaseOfEducation (code)": "Int64",
    "PhaseOfEducation (name)": "string",
    "Boarders (code)": "Int64",
    "Boarders (name)": "string",
    "NurseryProvision (name)": "string",
    "OfficialSixthForm (code)": "Int64",
    "OfficialSixthForm (name)": "string",
    "AdmissionsPolicy (code)": "Int64",
    "AdmissionsPolicy (name)": "string",
    "OfstedLastInsp": "string",
    "Postcode": "string",
    "SchoolWebsite": "string",
    "TelephoneNum": "string",
    "GOR (name)": "string",
    "OfstedRating (name)": "string",
    "MSOA (code)": "string",
    "LSOA (code)": "string",
    "StatutoryLowAge": "Int64",
    "StatutoryHighAge": "Int64",
    "Street": "string",
    "Locality": "string",
    "Address3": "string",
    "Town": "string",
    "County (name)": "string",
}

gias_links_index_col = "URN"
gias_links = {
    "URN": "Int64",
    "LinkURN": "Int64",
    "LinkName": "string",
    "LinkType": "string",
    "LinkEstablishedDate": "string",
}

maintained_schools_master_list_index_col = "URN"
maintained_schools_master_list = {
    "URN": "Int64",
    "School Name": "string",
    "LAEstab": "string",
    "Phase": "string",
    "Overall Phase": "string",
    "Type": "string",
    "Period covered by return (months)": "Int64",
    "Did Not Supply flag": "string",
    "Lead school in federation": "string",
    "London Weighting": "string",
    "PFI": "string",
    "I01  Funds delegated by the LA": "float",
    "I02  Funding for 6th form students": "float",
    "I06  Other government grants": "float",
    "I07  Other grants and payments": "float",
    "I08  Income from facilities and services": "float",
    "I09  Income from catering": "float",
    "I10  Receipts from supply teacher insurance claims": "float",
    "I13  Donations and or private funds": "float",
    "Total Income   I01 to I18": "float",
    "CI04 Direct revenue financing": "float",
    "E01  Teaching Staff": "float",
    "E02  Supply teaching staff": "float",
    "E03 Education support staff": "float",
    "E04  Premises staff": "float",
    "E05 Administrative and clerical staff": "float",
    "E06 Catering staff": "float",
    "E07  Cost of other staff": "float",
    "E08  Indirect employee expenses": "float",
    "E09  Development and training": "float",
    "E10  Supply teacher insurance": "float",
    "E11  Staff related insurance": "float",
    "E12  Building maintenance and improvement": "float",
    "E13  Grounds maintenance and improvement": "float",
    "E14  Cleaning and caretaking": "float",
    "E15  Water and sewerage": "float",
    "E16  Energy": "float",
    "E17  Rates": "float",
    "E18  Other occupation costs": "float",
    "E19  Learning resources (not ICT equipment)": "float",
    "E20  ICT learning resources": "float",
    "E21  Exam fees": "float",
    "E22 Administrative supplies": "float",
    "E23  Other insurance premiums": "float",
    "E24  Special facilities ": "float",
    "E25  Catering supplies": "float",
    "E26 Agency supply teaching staff": "float",
    "E27  Bought in professional services - curriculum": "float",
    "E28a  Bought in professional services - other (except PFI)": "float",
    "E28b Bought in professional services - other (PFI)": "float",
    "E29  Loan interest": "float",
    "E30 Direct revenue financing (revenue contributions to capital)": "float",
    "E31  Community focused school staff": "float",
    "E32 Community focused school costs": "float",
    "Total Expenditure  E01 to E32": "float",
    "Revenue Reserve   B01 plus B02 plus B06": "float",
    "Direct Grant": "float",
    "Targeted Grants": "float",
    "Community Grants": "float",
    "Self Generated Funding": "float",
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
