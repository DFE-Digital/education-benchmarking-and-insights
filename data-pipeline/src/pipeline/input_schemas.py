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
    "EstablishmentStatus (code)": "Int64",
    "EstablishmentStatus (name)": "string",
    "OpenDate": "string",
    "CloseDate": "string",
    "PhaseOfEducation (code)": "Int64",
    "PhaseOfEducation (name)": "string",
    "Boarders (code)": "Int64",
    "Boarders (name)": "string",
    "NurseryProvision (name)": "string",
    "OfficialSixthForm (code)": "Int64",
    "OfficialSixthForm (name)": "string",
    "Gender (code)": "Int64",
    "Gender (name)": "string",
    "AdmissionsPolicy (code)": "Int64",
    "AdmissionsPolicy (name)": "string",
    "CensusDate": "string",
    "SchoolCapacity": "Int64",
    "OfstedLastInsp": "string",
    "LastChangedDate": "string",
    "Postcode": "string",
    "SchoolWebsite": "string",
    "TelephoneNum": "string",
    "GOR (name)": "string",
    "UrbanRural (name)": "string",
    "BoardingEstablishment (name)": "string",
    "PreviousLA (code)": "Int64",
    "PreviousLA (name)": "string",
    "PreviousEstablishmentNumber": "Int64",
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

academy_master_list_index_col = "LA Establishment Number"
academy_master_list = {
    "Company Registration Number": "string",
    "Incorporation Date": "string",
    "Academy Trust UPIN": "Int64",
    "Academy Trust Name": "string",
    "Academy Name": "string",
    "Academy UPIN": "Int64",
    "Trust Type": "string",
    "LA Establishment Number": "string",
    "Date Opened": "string",
    "Type of Provision - Phase": "string",
    "Regional School Commissioner": "string",
    "Valid From": "string",
    "Valid to": "string",
    "Territory": "string",
    "Academy Status": "string",
    "Academy Trust Status": "string",
    "Number of Academies in Trust": "Int64",
}

maintained_schools_master_list_index_col = "URN"
maintained_schools_master_list = {
    "URN": "Int64",
    "LAEstab": "string",
    "Phase": "string",
    "Overall Phase": "string",
    "Type": "string",
    "Period covered by return (months)": "Int64",
    "Did Not Supply flag": "string",
    "Federation": "string",
    "Lead school in federation": "string",
    "Urban  Rural": "string",
    "London Weighting": "string",
    "PFI": "string",
    "I01  Funds delegated by the LA": "float",
    "I02  Funding for 6th form students": "float",
    "I03  SEN funding": "float",
    "I04  Funding for minority ethnic pupils": "float",
    "I05  Pupil Premium": "float",
    "I06  Other government grants": "float",
    "I07  Other grants and payments": "float",
    "I08  Income from facilities and services": "float",
    "I09  Income from catering": "float",
    "I10  Receipts from supply teacher insurance claims": "float",
    "I11  Receipts from other insurance claims": "float",
    "I12  Income from contributions to visits etc ": "float",
    "I13  Donations and or private funds": "float",
    "I15  Pupil focussed extended school funding and   or grants": "float",
    "I16  Community focussed school funding and   or grants": "float",
    "I17  Community focused school facilities income": "float",
    "I18  Additional grant for schools": "float",
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

workforce_census_index_col = "URN"
workforce_census = {
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

pupil_census_index_col = "URN"
pupil_census = {
    "URN": "Int64",
    "% of pupils known to be eligible for and claiming free school me": "float",
    "% of pupils known to be eligible for free school meals (Performa": "float",
    "headcount of pupils": "float",
    "fte pupils": "float",
    "ward_name": "string",
    "district_administrative_name": "string",
    "region_name": "string",
    "Number of early year pupils (years E1 and E2)": "float",
    "Number of nursery pupils (years N1 and N2)": "float",
    "Full time boys Year group 12": "float",
    "Full time girls Year group 12": "float",
    "Full time boys Year group 13": "float",
    "Full time girls Year group 13": "float",
    "number_of_dual_subsidiary_registrations": "float",
}

cfr_index_col = "URN"
cfr = {"URN": "Int64"}

groups_index_col = "URN"
groups = {
    "URN": "Int64",
    "Group UID": "Int64",
    "Group ID": "string",
    "Group Name": "string",
    "Companies House Number": "string",
    "Group Type (code)": "string",
    "Group Type": "string",
    "Closed Date": "string",
    "Open date": "string",
    "Incorporated on (open date)": "string",
    "Group Status (code)": "string",
    "Group Status": "string",
    "Joined date": "string",
    "EstablishmentName": "string",
    "TypeOfEstablishment (code)": "string",
    "TypeOfEstablishment (name)": "string",
    "PhaseOfEducation (code)": "Int64",
    "PhaseOfEducation (name)": "string",
    "LA (code)": "Int64",
    "LA (name)": "string",
}

ks2_index_col = "URN"
ks2 = {
    "URN": "Int64",
    "READPROG": "string",
    "WRITPROG": "string",
    "MATPROG": "string",
}

ks4_index_col = "URN"
ks4 = {"URN": "Int64", "ATT8SCR": "float", "P8MEA": "float", "P8_BANDING": "string"}

aar_academies_index_col = "Academy UPIN"
aar_academies = {
    "URN": "Int64",
    "Academy UPIN": "Int64",
    "In year balance": "float",
    "PFI": "string",
    "Lead UPIN": "Int64",
    "DFE/EFA Revenue grants (includes Coronavirus Government Funding": "float",
    "SEN funding": "float",
    "Other DfE/EFA Revenue Grants": "float",
    "Other income - LA & other Government grants": "float",
    "Government source, non-grant": "float",
    "Academies": "float",
    "Non-Government": "float",
    "All income from facilities and services": "float",
    "Income from catering": "float",
    "Receipts from supply teacher insurance claims": "float",
    "Donations and/or voluntary funds": "float",
    "Other self-generated income": "float",
    "Investment income": "float",
    "Teaching staff": "float",
    "Supply teaching staff": "float",
    "Education support staff": "float",
    "Administrative and clerical staff": "float",
    "Premises staff": "float",
    "Catering staff": "float",
    "Other staff": "float",
    "Indirect employee expenses": "float",
    "Staff development and training": "float",
    "Staff-related insurance": "float",
    "Supply teacher insurance": "float",
    "Building and Grounds maintenance and improvement": "float",
    "Cleaning and caretaking": "float",
    "Water and sewerage": "float",
    "Energy": "float",
    "Rent and Rates": "float",
    "Other occupation costs": "float",
    "Special facilities": "float",
    "Learning resources (not ICT equipment)": "float",
    "ICT learning resources": "float",
    "Examination fees": "float",
    "Educational Consultancy": "float",
    "Administrative supplies - non educational": "float",
    "Agency supply teaching staff": "float",
    "Catering supplies": "float",
    "Other insurance premiums": "float",
    "Legal & Professionalservices": "float",
    "Auditor costs": "float",
    "Interest charges for Loan and Bank": "float",
    "Direct revenue financing - Revenue contributions to capital": "float",
    "PFI Charges": "float",
    "Revenue reserve": "float",
    "Total Grant Funding": "float",
    "Direct Grants": "float",
    "Community Grants": "float",
    "Targeted Grants": "float",
    "Total Self Generated Funding": "float",
    "Total Income": "float",
    "Supply Staff Costs": "float",
    "Other Staff Costs": "float",
    "Total Staff Costs": "float",
    "Maintenance & Improvement Costs": "float",
    "Premises Costs": "float",
    "Catering Expenses": "float",
    "Occupation Costs": "float",
    "Total Costs of Supplies and Services": "float",
    "Total Costs of Educational Supplies": "float",
    "Costs of Brought in Professional Services": "float",
    "Total Expenditure": "float",
    "London Weighting": "string",
    "Date joined or opened if in period": "string",
    "Date left or closed if in period": "string",
}

aar_central_services_index_col = "Lead UPIN"
aar_central_services = {
    "Lead UPIN": "Int64",
    "In Year Balance": "float",
    "DFE/EFA Revenue grants (includes Coronavirus Government Funding": "float",
    "SEN": "float",
    "Other DfE/EFA Revenue Grants": "float",
    "Other income (LA & other Government grants)": "float",
    "Government source (non-grant)": "float",
    "Academies": "float",
    "Non- Government": "float",
    "Income from facilities and services": "float",
    "Income from catering": "float",
    "Receipts from supply teacher insurance claims": "float",
    "Donations and/or voluntary funds": "float",
    "Other self-generated income": "float",
    "Investment income": "float",
    "Teaching staff": "float",
    "Supply teaching staff": "float",
    "Education support staff": "float",
    "Administrative and clerical staff": "float",
    "Premises staff": "float",
    "Catering staff": "float",
    "Other staff": "float",
    "Indirect employee expenses": "float",
    "Staff development and training": "float",
    "Staff-related insurance": "float",
    "Supply teacher insurance": "float",
    "Building and Grounds maintenance and improvement": "float",
    "Cleaning and caretaking": "float",
    "Water and sewerage": "float",
    "Energy": "float",
    "Rent and Rates": "float",
    "Other occupation costs": "float",
    "Special facilities": "float",
    "Learning resources (not ICT equipment)": "float",
    "ICT learning resources": "float",
    "Examination fees": "float",
    "Educational Consultancy": "float",
    "Administrative supplies - non educational": "float",
    "Agency supply teaching staff": "float",
    "Catering supplies": "float",
    "Other insurance premiums": "float",
    "Legal & Professional": "float",
    "Auditor costs": "float",
    "Interest charges for Loan and Bank": "float",
    "Direct revenue financing (Revenue contributions to capital)": "float",
    "PFI Charges": "float",
    "Revenue Reserve": "float",
    "Grant Funding": "float",
    "Direct Grant": "float",
    "Community Grants": "float",
    "Targeted Grants": "float",
    "Self Generated Funding": "float",
    "Total Income ": "float",
    "Supply Staff": "float",
    "Other Staff Costs": "float",
    "Staff Total ": "float",
    "Maintenance & Improvement": "float",
    "Premises": "float",
    "Catering Exp": "float",
    "Occupation": "float",
    "Supplies and Services": "float",
    "Educational Supplies": "float",
    "Brought in Professional Services": "float",
    "Total Expenditure": "float",
}

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
    "Y2": "Int64",
    "Y3": "Int64",
    "Y4": "Int64",
}
