
gias_index_col = 'URN'
gias = {
    'URN': 'Int64',
    'UKPRN': 'Int64',
    'LA (code)': 'string',
    'LA (name)': 'string',
    'EstablishmentNumber': 'Int64',
    'EstablishmentName': 'string',
    'TypeOfEstablishment (code)': 'Int64',
    'TypeOfEstablishment (name)': 'string',
    'EstablishmentStatus (code)': 'Int64',
    'EstablishmentStatus (name)': 'string',
    'OpenDate': 'string',
    'CloseDate': 'string',
    'PhaseOfEducation (code)': 'Int64',
    'PhaseOfEducation (name)': 'string',
    'Boarders (code)': 'Int64',
    'Boarders (name)': 'string',
    'NurseryProvision (name)': 'string',
    'OfficialSixthForm (code)': 'Int64',
    'OfficialSixthForm (name)': 'string',
    'Gender (code)': 'Int64',
    'Gender (name)': 'string',
    'AdmissionsPolicy (code)': 'Int64',
    'AdmissionsPolicy (name)': 'string',
    'CensusDate': 'string',
    'SchoolCapacity': 'Int64',
    'NumberOfPupils': 'Int64',
    'NumberOfBoys': 'Int64',
    'NumberOfGirls': 'Int64',
    'OfstedLastInsp': 'string',
    'LastChangedDate': 'string',
    'Postcode': 'string',
    'SchoolWebsite': 'string',
    'TelephoneNum': 'string',
    'HeadTitle (name)': 'string',
    'HeadFirstName': 'string',
    'HeadLastName': 'string',
    'HeadPreferredJobTitle': 'string',
    'GOR (name)': 'string',
    'UrbanRural (name)': 'string',
    'BoardingEstablishment (name)': 'string',
    'PreviousLA (code)': 'Int64',
    'PreviousLA (name)': 'string',
    'PreviousEstablishmentNumber': 'Int64',
    'OfstedRating (name)': 'string',
    'MSOA (code)': 'string',
    'LSOA (code)': 'string'
}

gias_links_index_col = 'URN'
gias_links = {
    'URN': 'Int64',
    'LinkURN': 'Int64',
    'LinkName': 'string',
    'LinkType': 'string',
    'LinkEstablishedDate': 'string'
}

academy_master_list_index_col = 'LA Establishment Number'
academy_master_list = {
    'Company Registration Number': 'string',
    'Incorporation Date': 'string',
    'Academy Trust UPIN': 'Int64',
    'UKPRN': 'Int64',
    'Academy Trust Name': 'string',
    'Academy Name': 'string',
    'Academy UPIN': 'Int64',
    'Trust Type': 'string',
    'LA Establishment Number': 'string',
    'Date Opened': 'string',
    'LA Name': 'string',
    'Type of Provision - Phase': 'string',
    'Regional School Commissioner': 'string',
    'Valid From': 'string',
    'Valid to': 'string',
    'Date joined or opened if in period': 'string',
    'Date left or closed if in period': 'string',
    'Territory': 'string',
    'Academy Status': 'string',
    'Academy Trust Status': 'string',
    'Number of Academies in Trust': 'Int64',
    'Number of Pupils': 'Int64'
}

maintained_schools_master_list_index_col = 'URN'
maintained_schools_master_list = {
    'URN': 'Int64',
    # Following columns should probably come from GIAS?
    # 'LA': 'string',
    # 'LA Name': 'string',
    # 'Region': 'string',
    # 'Estab': 'string',
    'LAEstab': 'string',
    # 'School Name': 'string',
    # 'Admissions policy': 'string',
    # 'Has a 6th form': 'string',
    # 'Gender': 'string',
    'Phase': 'string',
    'Overall Phase': 'string',
    'Lowest age of pupils': 'float',
    'Highest age of pupils': 'float',
    'Type': 'string',
    'No Pupils': 'float',
    '% of pupils eligible for FSM': 'float',
    'Period covered by return (months)': 'Int64',
    'Did Not Supply flag': 'string',
    'Federation': 'string',
    'Lead school in federation': 'string',
    'No Teachers': 'float',
    'Urban  Rural': 'string',
    'London Weighting': 'string',
    # '% of pupils with SEN Statement': 'float',
    # '% of SEN pupils without Statement': 'float',
    '% of pupils with EAL': 'float',
    '% of pupils who are Boarders': 'float',
    'PFI': 'string',
    'No of pupils in 6th form': 'float',
    '% of teachers with QTS': 'float',
    'FTE of Teaching Assistants': 'float',
    'FTE of Support Staff': 'float',
    'FTE of Admin Staff': 'float',
    'I01  Funds delegated by the LA': 'float',
    'I02  Funding for 6th form students': 'float',
    'I03  SEN funding': 'float',
    'I04  Funding for minority ethnic pupils': 'float',
    'I05  Pupil Premium': 'float',
    'I06  Other government grants': 'float',
    'I07  Other grants and payments': 'float',
    'I08  Income from facilities and services': 'float',
    'I09  Income from catering': 'float',
    'I10  Receipts from supply teacher insurance claims': 'float',
    'I11  Receipts from other insurance claims': 'float',
    'I12  Income from contributions to visits etc ': 'float',
    'I13  Donations and or private funds': 'float',
    'I15  Pupil focussed extended school funding and   or grants': 'float',
    'I16  Community focussed school funding and   or grants': 'float',
    'I17  Community focused school facilities income': 'float',
    'I18  Additional grant for schools': 'float',
    'Total Income   I01 to I08, I11 to I15, I18': 'float',
    'Total Income   I01 to I18': 'float',
    'E01  Teaching Staff': 'float',
    'E02  Supply teaching staff': 'float',
    'E03 Education support staff': 'float',
    'E04  Premises staff': 'float',
    'E05 Administrative and clerical staff': 'float',
    'E06 Catering staff': 'float',
    'E07  Cost of other staff': 'float',
    'E08  Indirect employee expenses': 'float',
    'E09  Development and training': 'float',
    'E10  Supply teacher insurance': 'float',
    'E11  Staff related insurance': 'float',
    'E12  Building maintenance and improvement': 'float',
    'E13  Grounds maintenance and improvement': 'float',
    'E14  Cleaning and caretaking': 'float',
    'E15  Water and sewerage': 'float',
    'E16  Energy': 'float',
    'E17  Rates': 'float',
    'E18  Other occupation costs': 'float',
    'E19  Learning resources (not ICT equipment)': 'float',
    'E20  ICT learning resources': 'float',
    'E21  Exam fees': 'float',
    'E22 Administrative supplies': 'float',
    'E23  Other insurance premiums': 'float',
    'E24  Special facilities ': 'float',
    'E25  Catering supplies': 'float',
    'E26 Agency supply teaching staff': 'float',
    'E27  Bought in professional services - curriculum': 'float',
    'E28a  Bought in professional services - other (except PFI)': 'float',
    'E28b Bought in professional services - other (PFI)': 'float',
    'E29  Loan interest': 'float',
    'E30 Direct revenue financing (revenue contributions to capital)': 'float',
    'E31  Community focused school staff': 'float',
    'E32 Community focused school costs': 'float',
    'Total Expenditure  E01 to E29 and E31 to E32 minus I9, I10, I16 and I17': 'float',
    'Total Expenditure  E01 to E32': 'float',
    'Revenue Reserve   B01 plus B02 plus B06': 'float',
    'In-year Balance   Total Income (I01 to I18) minus Total Expenditure (E01 to E32)': 'float'
}

cdc_index_col = 'URN'
cdc = {
    'URN': 'Int64',
    'GIFA': 'float',
    'Block Age': 'string'
}

sen_index_col = 'URN'
sen = {
    'URN': 'Int64',
    'Total pupils': 'Int64',
    'SEN support': 'Int64',
    'EHC plan': 'Int64',
    'EHC_Primary_need_spld': 'Int64',
    'SUP_Primary_need_spld': 'Int64',
    'EHC_Primary_need_mld': 'Int64',
    'SUP_Primary_need_mld': 'Int64',
    'EHC_Primary_need_sld': 'Int64',
    'SUP_Primary_need_sld': 'Int64',
    'EHC_Primary_need_pmld': 'Int64',
    'SUP_Primary_need_pmld': 'Int64',
    'EHC_Primary_need_semh': 'Int64',
    'SUP_Primary_need_semh': 'Int64',
    'EHC_Primary_need_slcn': 'Int64',
    'SUP_Primary_need_slcn': 'Int64',
    'EHC_Primary_need_hi': 'Int64',
    'SUP_Primary_need_hi': 'Int64',
    'EHC_Primary_need_vi': 'Int64',
    'SUP_Primary_need_vi': 'Int64',
    'EHC_Primary_need_msi': 'Int64',
    'SUP_Primary_need_msi': 'Int64',
    'EHC_Primary_need_pd': 'Int64',
    'SUP_Primary_need_pd': 'Int64',
    'EHC_Primary_need_asd': 'Int64',
    'SUP_Primary_need_asd': 'Int64',
    'EHC_Primary_need_oth': 'Int64',
    'SUP_Primary_need_oth': 'Int64',
    'EHC_Primary_need_nsa': 'Int64',
    'SUP_Primary_need_nsa': 'Int64',
    'Prov_SPLD': 'float',
    'Prov_MLD': 'float',
    'Prov_SLD': 'float',
    'Prov_PMLD': 'float',
    'Prov_SEMH': 'float',
    'prov_slcn': 'float',
    'prov_hi': 'float',
    'prov_vi': 'float',
    'prov_msi': 'float',
    'prov_pd': 'float',
    'prov_asd': 'float',
    'prov_oth': 'float'
}

workforce_census_index_col = 'URN'
workforce_census = {
    'URN': 'Int64',
    'Total School Workforce (Full-Time Equivalent)': 'float',
    'Number of Vacant Teacher Posts': 'float',
    'Total Number of Teachers (Full-Time Equivalent)': 'float',
    'Pupil: Teacher Ratio (Full-Time Equivalent of qualified and unqualified teachers)': 'float',
    'Total Number of Teaching Assistants (Headcount)': 'float',
    'All Teachers on the Leadership Pay Range (%)':'float'
}


pupil_census_index_col = 'URN'
pupil_census = {
    'URN': 'Int64',
    'headcount of pupils': 'float',
    '% of pupils known to be eligible for and claiming free school me': 'float',
    '% of pupils known to be eligible for free school meals (Performa': 'float',
    'number of pupils whose first language is known or believed to be other than English': 'float',
    'full time pupils': 'float'
}

cfr_index_col = 'URN'
cfr = {
    'URN': 'Int64'
}

ar_cell_mapping_index_col = 'cell'
ar_cell_mapping = {
    'cell': 'string',
    'Description L1': 'string',
    'Metric': 'string',
    'Metric ID': 'Int64',
    'Cost Pool': 'string',
    'Presentation name': 'string',
    'Cost Pool ID': 'Int64'
}

ar_index_col = 'academyupin'
ar = {
    'academyupin': 'Int64',
    'trustupin': 'Int64',
    'companynumber': 'Int64',
    'aruniquereference': 'string',
    'value': 'float'
}


groups_index_col = 'URN'
groups = {
    'URN':'Int64',
    'Group UID':'Int64',
	'Group ID':'string',
    'Group Name':'string',
	'Companies House Number':'string',
    'Group Type (code)':'string',
    'Group Type':'string',
    'Closed Date':'string',
    'Open date':'string',
    'Incorporated on (open date)':'string',
    'Group Status (code)':'string',
    'Group Status':'string',
    'Joined date':'string',
    'EstablishmentName':'string',
    'TypeOfEstablishment (code)':'string',	
    'TypeOfEstablishment (name)':'string',	
    'PhaseOfEducation (code)':'Int64',
    'PhaseOfEducation (name)':'string',	
    'LA (code)':'Int64',	
    'LA (name)':'string'
}


