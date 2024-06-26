from io import BytesIO, StringIO

import numpy as np
import pandas as pd
import pytest

from src.pipeline.pre_processing import (
    build_bfr_data,
    prepare_aar_data,
    prepare_cdc_data,
    prepare_census_data,
    prepare_central_services_data,
    prepare_ks2_data,
    prepare_ks4_data,
    prepare_schools_data,
    prepare_sen_data,
)


@pytest.fixture
def cdc_data() -> pd.DataFrame:
    return pd.DataFrame(
        {
            "Site": ["Site A", "Site A", "Site A", "Site A", "Site B"],
            "URN": [100150, 100150, 100150, 100150, 100162],
            "SurveySectionSectionType": [
                "ANCILLARY",
                "BLOCK",
                "BLOCK",
                "BLOCK",
                "ANCILLARY",
            ],
            "GIFA": [89, 2221, 436, 57, 131],
            "Block Age": ["NULL", "1961-1970", "2001-2010", "1991-2000", "NULL"],
        }
    )


@pytest.fixture
def prepared_cdc_data(cdc_data: pd.DataFrame) -> dict:
    return (
        prepare_cdc_data(StringIO(cdc_data.to_csv()), 2022)
        .reset_index()
        .to_dict("records")[0]
    )


@pytest.fixture
def sen_data() -> pd.DataFrame:
    return pd.DataFrame(
        {
            "URN": [100150, 100151],
            "EHC plan": [50, 100],
            "SEN support": [20, 20],
            "Total pupils": [100, 100],
            "EHC_Primary_need_spld": [1, 50],
            "SUP_Primary_need_spld": [1, 50],
            "EHC_Primary_need_mld": [1, np.nan],
            "SUP_Primary_need_mld": [2, np.nan],
            "EHC_Primary_need_sld": [1, np.nan],
            "SUP_Primary_need_sld": [3, np.nan],
            "EHC_Primary_need_pmld": [1, np.nan],
            "SUP_Primary_need_pmld": [4, np.nan],
            "EHC_Primary_need_semh": [1, np.nan],
            "SUP_Primary_need_semh": [5, np.nan],
            "EHC_Primary_need_slcn": [1, np.nan],
            "SUP_Primary_need_slcn": [6, np.nan],
            "EHC_Primary_need_hi": [1, np.nan],
            "SUP_Primary_need_hi": [7, np.nan],
            "EHC_Primary_need_vi": [1, np.nan],
            "SUP_Primary_need_vi": [8, np.nan],
            "EHC_Primary_need_msi": [1, np.nan],
            "SUP_Primary_need_msi": [9, np.nan],
            "EHC_Primary_need_pd": [1, np.nan],
            "SUP_Primary_need_pd": [10, np.nan],
            "EHC_Primary_need_asd": [1, np.nan],
            "SUP_Primary_need_asd": [11, np.nan],
            "EHC_Primary_need_oth": [1, np.nan],
            "SUP_Primary_need_oth": [12, np.nan],
        }
    )


@pytest.fixture
def prepared_sen_data(sen_data: pd.DataFrame) -> dict:
    return (
        prepare_sen_data(StringIO(sen_data.to_csv()))
        .reset_index()
        .to_dict("records")[0]
    )


@pytest.fixture
def prepared_sen_data_with_nans(sen_data: pd.DataFrame) -> dict:
    return (
        prepare_sen_data(StringIO(sen_data.to_csv()))
        .reset_index()
        .to_dict("records")[1]
    )


@pytest.fixture
def ks2_data() -> pd.DataFrame:
    return pd.DataFrame(
        {
            "URN": [100150, 100152, 100153],
            "READPROG": ["0.1", "SUPP", "LOWCOV"],
            "WRITPROG": ["0.1", "SUPP", "LOWCOV"],
            "MATPROG": ["0.1", "SUPP", "LOWCOV"],
        }
    )


@pytest.fixture
def prepared_ks2_data(ks2_data: pd.DataFrame):
    output = BytesIO()
    writer = pd.ExcelWriter(output)
    ks2_data.to_excel(writer, index=False)
    writer.close()
    output.seek(0)

    return prepare_ks2_data(output)


@pytest.fixture
def ks4_data() -> pd.DataFrame:
    return pd.DataFrame(
        {
            "URN": [100150, 100152, 100153],
            "ATT8SCR": ["0.1", "SUPP", "NE"],
            "P8MEA": ["0.1", "SUPP", "NE"],
            "P8_BANDING": ["Average", "Average", "Average"],
        }
    )


@pytest.fixture
def prepared_ks4_data(ks4_data: pd.DataFrame):
    output = BytesIO()
    writer = pd.ExcelWriter(output)
    ks4_data.to_excel(writer, index=False)
    writer.close()
    output.seek(0)

    return prepare_ks4_data(output)


@pytest.fixture
def aar_data() -> pd.DataFrame:
    return pd.DataFrame(
        {
            "URN": [100150, 100152, 100153],
            "Academy UPIN": [111443, 111451, 111453],
            "In year balance": [1000, 1001, -1002],
            "PFI": ["Not part of PFI", "Not part of PFI", "Part of PFI"],
            "Lead UPIN": [137157, 137157, 135112],
            "DFE/EFA Revenue grants (includes Coronavirus Government Funding": [
                1000,
                1001,
                1002,
            ],
            "of which: Coronavirus Government Funding": [1000, 1001, 1002],
            "SEN funding": [1000, 1001, 1002],
            "Other DfE/EFA Revenue Grants": [1000, 1001, 1002],
            "Other income - LA & other Government grants": [1000, 1001, 1002],
            "Government source, non-grant": [1000, 1001, 1002],
            "Academies": [1000, 1001, 1002],
            "Non-Government": [1000, 1001, 1002],
            "All income from facilities and services": [1000, 1001, 1002],
            "Income from catering": [1000, 1001, 1002],
            "Receipts from supply teacher insurance claims": [1000, 1001, 1002],
            "Donations and/or voluntary funds": [1000, 1001, 1002],
            "Other self-generated income": [1000, 1001, 1002],
            "Investment income": [1000, 1001, 1002],
            "Teaching staff": [1000, 1001, 1002],
            "Supply teaching staff": [1000, 1001, 1002],
            "Education support staff": [1000, 1001, 1002],
            "Administrative and clerical staff": [1000, 1001, 1002],
            "Premises staff": [1000, 1001, 1002],
            "Catering staff": [1000, 1001, 1002],
            "Other staff": [1000, 1001, 1002],
            "Indirect employee expenses": [1000, 1001, 1002],
            "Staff development and training": [1000, 1001, 1002],
            "Staff-related insurance": [1000, 1001, 1002],
            "Supply teacher insurance": [1000, 1001, 1002],
            "Building and Grounds maintenance and improvement": [1000, 1001, 1002],
            "Cleaning and caretaking": [1000, 1001, 1002],
            "Water and sewerage": [1000, 1001, 1002],
            "Energy": [1000, 1001, 1002],
            "Rent and Rates": [1000, 1001, 1002],
            "Other occupation costs": [1000, 1001, 1002],
            "Special facilities": [1000, 1001, 1002],
            "Learning resources (not ICT equipment)": [1000, 1001, 1002],
            "ICT learning resources": [1000, 1001, 1002],
            "Examination fees": [1000, 1001, 1002],
            "Educational Consultancy": [1000, 1001, 1002],
            "Administrative supplies - non educational": [1000, 1001, 1002],
            "Agency supply teaching staff": [1000, 1001, 1002],
            "Catering supplies": [1000, 1001, 1002],
            "Other insurance premiums": [1000, 1001, 1002],
            "Legal & Professionalservices": [1000, 1001, 1002],
            "Auditor costs": [1000, 1001, 1002],
            "Interest charges for Loan and Bank": [1000, 1001, 1002],
            "Direct revenue financing - Revenue contributions to capital": [
                1000,
                1001,
                1002,
            ],
            "PFI Charges": [1000, 1001, 1002],
            "Revenue reserve": [1000, 1001, 1002],
            "Total Grant Funding": [1000, 1001, 1002],
            "Direct Grants": [1000, 1001, 1002],
            "Community Grants": [1000, 1001, 1002],
            "Targeted Grants": [1000, 1001, 1002],
            "Total Self Generated Funding": [1000, 1001, 1002],
            "Total Income": [1000, 1001, 1002],
            "Supply Staff Costs": [1000, 1001, 1002],
            "Other Staff Costs": [1000, 1001, 1002],
            "Total Staff Costs": [1000, 1001, 1002],
            "Maintenance & Improvement Costs": [1000, 1001, 1002],
            "Premises Costs": [1000, 1001, 1002],
            "Catering Expenses": [1000, 1001, 1002],
            "Occupation Costs": [1000, 1001, 1002],
            "Total Costs of Supplies and Services": [1000, 1001, 1002],
            "Total Costs of Educational Supplies": [1000, 1001, 1002],
            "Costs of Brought in Professional Services": [1000, 1001, 1002],
            "Total Expenditure": [1000, 1001, 1002],
            "Share of Revenue Reserve, distributed on per pupil basis\n": [
                1000,
                1001,
                1002,
            ],
            "London Weighting": ["Neither", "Outer", "Inner"],
            "Date joined or opened if in period": [None, "01/05/2023", None],
            "Date left or closed if in period": [None, "01/05/2023", None],
        }
    )


@pytest.fixture
def aar_central_services_data() -> pd.DataFrame:
    return pd.DataFrame(
        {
            "Lead UPIN": [137157, 137157, 135112],
            "In Year Balance": [1000, 1001, 1002],
            "DFE/EFA Revenue grants (includes Coronavirus Government Funding": [
                1000,
                1001,
                1002,
            ],
            "SEN": [1000, 1001, 1002],
            "Other DfE/EFA Revenue Grants": [1000, 1001, 1002],
            "Other income (LA & other Government grants)": [1000, 1001, 1002],
            "Government source (non-grant)": [1000, 1001, 1002],
            "Academies": [1000, 1001, 1002],
            "Non- Government": [1000, 1001, 1002],
            "Income from facilities and services": [1000, 1001, 1002],
            "Income from catering": [1000, 1001, 1002],
            "Receipts from supply teacher insurance claims": [1000, 1001, 1002],
            "Donations and/or voluntary funds": [1000, 1001, 1002],
            "Other self-generated income": [1000, 1001, 1002],
            "Investment income": [1000, 1001, 1002],
            "Teaching staff": [1000, 1001, 1002],
            "Supply teaching staff": [1000, 1001, 1002],
            "Education support staff": [1000, 1001, 1002],
            "Administrative and clerical staff": [1000, 1001, 1002],
            "Premises staff": [1000, 1001, 1002],
            "Catering staff": [1000, 1001, 1002],
            "Other staff": [1000, 1001, 1002],
            "Indirect employee expenses": [1000, 1001, 1002],
            "Staff development and training": [1000, 1001, 1002],
            "Staff-related insurance": [1000, 1001, 1002],
            "Supply teacher insurance": [1000, 1001, 1002],
            "Building and Grounds maintenance and improvement": [1000, 1001, 1002],
            "Cleaning and caretaking": [1000, 1001, 1002],
            "Water and sewerage": [1000, 1001, 1002],
            "Energy": [1000, 1001, 1002],
            "Rent and Rates": [1000, 1001, 1002],
            "Other occupation costs": [1000, 1001, 1002],
            "Special facilities": [1000, 1001, 1002],
            "Learning resources (not ICT equipment)": [1000, 1001, 1002],
            "ICT learning resources": [1000, 1001, 1002],
            "Examination fees": [1000, 1001, 1002],
            "Educational Consultancy": [1000, 1001, 1002],
            "Administrative supplies - non educational": [1000, 1001, 1002],
            "Agency supply teaching staff": [1000, 1001, 1002],
            "Catering supplies": [1000, 1001, 1002],
            "Other insurance premiums": [1000, 1001, 1002],
            "Legal & Professional": [1000, 1001, 1002],
            "Auditor costs": [1000, 1001, 1002],
            "Interest charges for Loan and Bank": [1000, 1001, 1002],
            "Direct revenue financing (Revenue contributions to capital)": [
                1000,
                1001,
                1002,
            ],
            "PFI Charges": [1000, 1001, 1002],
            "Revenue Reserve": [1000, 1001, 1002],
            "Grant Funding": [1000, 1001, 1002],
            "Direct Grant": [1000, 1001, 1002],
            "Community Grants": [1000, 1001, 1002],
            "Targeted Grants": [1000, 1001, 1002],
            "Self Generated Funding": [1000, 1001, 1002],
            "Total Income ": [1000, 1001, 1002],
            "Supply Staff": [1000, 1001, 1002],
            "Other Staff Costs": [1000, 1001, 1002],
            "Staff Total ": [1000, 1001, 1002],
            "Maintenance & Improvement": [1000, 1001, 1002],
            "Premises": [1000, 1001, 1002],
            "Catering Exp": [1000, 1001, 1002],
            "Occupation": [1000, 1001, 1002],
            "Supplies and Services": [1000, 1001, 1002],
            "Educational Supplies": [1000, 1001, 1002],
            "Brought in Professional Services": [1000, 1001, 1002],
            "Total Expenditure": [1000, 1001, 1002],
        }
    )


def _aar_data(
    aar_data: pd.DataFrame, aar_central_services_data: pd.DataFrame
) -> BytesIO:
    output = BytesIO()
    writer = pd.ExcelWriter(output)
    aar_data.to_excel(writer, sheet_name="Academies", index=False)
    aar_central_services_data.to_excel(
        writer, sheet_name="CentralServices", index=False
    )
    writer.close()
    output.seek(0)
    return output


@pytest.fixture
def prepared_central_services_data(
    aar_data: pd.DataFrame, aar_central_services_data: pd.DataFrame
) -> pd.DataFrame:
    return prepare_central_services_data(_aar_data(aar_data, aar_central_services_data))


@pytest.fixture
def prepared_aar_data(
    aar_data: pd.DataFrame, aar_central_services_data: pd.DataFrame
) -> pd.DataFrame:
    return prepare_aar_data(_aar_data(aar_data, aar_central_services_data))


@pytest.fixture
def workforce_census_data() -> pd.DataFrame:
    return pd.DataFrame(
        {
            "URN": [100150, 100152, 100153],
            "Total Number of Non-Classroom-based School Support Staff, (Other school support staff plus Administrative staff plus Technicians and excluding Auxiliary staff (Full-Time Equivalent)": [
                6.9,
                7.9,
                10,
            ],
            "Total Number of Non Classroom-based School Support Staff, Excluding Auxiliary Staff (Headcount)": [
                13,
                14,
                15,
            ],
            "Teachers with Qualified Teacher Status (%) (Headcount)": [80, 80, 100],
            "Total Number of Teaching Assistants (Full-Time Equivalent)": [
                14.8,
                15.8,
                16.8,
            ],
            "Total Number of Teaching Assistants (Headcount)": [20, 21, 22],
            "Total School Workforce (Full-Time Equivalent)": [75, 100, 125],
            "Total Number of Teachers (Full-Time Equivalent)": [15.6, 9.8, 15.8],
            "Total Number of Teachers (Headcount)": [25, 10, 18],
            "Total Number of Teachers in the Leadership Group (Headcount)": [4, 5, 2],
            "Total Number of Teachers in the Leadership Group (Full-time Equivalent)": [
                2.6,
                3.6,
                2,
            ],
            "Total Number of Auxiliary Staff (Full-Time Equivalent)": [2.3, 2.3, 5.4],
            "Total Number of Auxiliary Staff (Headcount)": [3, 3, 9],
            "Total School Workforce (Headcount)": [61, 62, 50],
        }
    )


@pytest.fixture
def pupil_census_data() -> pd.DataFrame:
    return pd.DataFrame(
        {
            "URN": [100150, 100152, 100153],
            "% of pupils known to be eligible for and claiming free school me": [
                33.8,
                23.4,
                33.2,
            ],
            "% of pupils known to be eligible for free school meals (Performa": [
                52.3,
                60.3,
                47.7,
            ],
            "number of pupils whose first language is known or believed to be other than English": [
                93,
                236,
                127,
            ],
            "headcount of pupils": [325, 599, 465],
            "fte pupils": [320, 590, 460],
            "ward_name": ["Bilborough", "Aspley", "Bilborough"],
            "district_administrative_name": ["Nottingham", "Nottingham", "Nottingham"],
            "region_name": ["East Midlands", "East Midlands", "East Midlands"],
            "Number of early year pupils (years E1 and E2)": [10, 20, 30],
            "Number of nursery pupils (years N1 and N2)": [10, 20, 30],
            "Full time boys Year group 12": [10, 20, 30],
            "Full time girls Year group 12": [10, 20, 30],
            "Full time boys Year group 13": [10, 20, 30],
            "Full time girls Year group 13": [10, 20, 30],
            "number_of_dual_subsidiary_registrations": ["z", 29, 0],
        }
    )


@pytest.fixture
def prepared_census_data(
    workforce_census_data: pd.DataFrame, pupil_census_data: pd.DataFrame
) -> dict:
    output = BytesIO()
    writer = pd.ExcelWriter(output)
    workforce_census_data.to_excel(
        writer, startrow=5, sheet_name="Schools 2022", index=False
    )
    writer.close()
    output.seek(0)

    return prepare_census_data(output, StringIO(pupil_census_data.to_csv()))


@pytest.fixture
def gias_data() -> pd.DataFrame:
    return pd.DataFrame(
        {
            "URN": [100150, 100152, 100153],
            "UKPRN": [10079319, 10013279, 10018890],
            "LA (code)": [201, 202, 202],
            "LA (name)": ["City of London", "Camden", "Camden"],
            "EstablishmentNumber": [3614, 6005, 6006],
            "EstablishmentName": ["School A", "School B", "School C"],
            "TypeOfEstablishment (code)": [2, 11, 15],
            "TypeOfEstablishment (name)": [
                "Voluntary aided school",
                "Other independent school",
                "Local authority nursery school",
            ],
            "EstablishmentStatus (code)": [4, 3, 4],
            "EstablishmentStatus (name)": [
                "Local authority maintained schools",
                "Independent schools",
                "Local authority maintained schools",
            ],
            "OpenDate": [None, "01/01/1920", "01/01/1939"],
            "CloseDate": [None, None, None],
            "PhaseOfEducation (code)": [2, 0, 1],
            "PhaseOfEducation (name)": ["Primary", "Not applicable", "Nursery"],
            "Boarders (code)": [1, 0, 3],
            "Boarders (name)": ["No boarders", "Not applicable", "Boarding school"],
            "NurseryProvision (name)": [
                "No Nursery Classes",
                "No Nursery Classes",
                "Has Nursery Classes",
            ],
            "OfficialSixthForm (code)": [2, 1, 0],
            "OfficialSixthForm (name)": [
                "Does not have a sixth form",
                "Has a sixth form",
                "Not applicable",
            ],
            "Gender (code)": [2, 1, 3],
            "Gender (name)": ["Girls", "Boys", "Mixed"],
            "AdmissionsPolicy (code)": [0, 0, 2],
            "AdmissionsPolicy (name)": [
                "Not applicable",
                "Not applicable",
                "Selective",
            ],
            "CensusDate": ["19/01/2023", "19/01/2023", "20/01/2023"],
            "SchoolCapacity": [271, 820, 285],
            "NumberOfPupils": [271, 739, 269],
            "NumberOfBoys": [0, 739, 164],
            "NumberOfGirls": [271, 0, 105],
            "OfstedLastInsp": ["19/04/2013", "06/03/2019", None],
            "LastChangedDate": ["09/02/2024", "20/12/2023", "02/01/2024"],
            "Postcode": ["EC3A 5DE", "EC2Y 8BB", "EC4M 9AD"],
            "SchoolWebsite": [
                "www.schoola.co.uk",
                "www.schoolb.co.uk",
                "www.schoolc.co.uk",
            ],
            "TelephoneNum": ["02072831147", "02072831147", "02072831147"],
            "HeadTitle (name)": ["Miss", "Mr", "Mrs"],
            "HeadFirstName": ["A", "B", "C"],
            "HeadLastName": ["HeadA", "HeadB", "HeadC"],
            "HeadPreferredJobTitle": ["Headteacher", "Head", "Executive Head"],
            "GOR (name)": ["London", "London", "London"],
            "UrbanRural (name)": ["A1", "A1", "A1"],
            "BoardingEstablishment (name)": [
                "Does not have boarders",
                "Does not have boarders",
                "Has boarders",
            ],
            "PreviousLA (code)": [999, 999, 999],
            "PreviousLA (name)": [None, None, None],
            "PreviousEstablishmentNumber": [None, None, None],
            "OfstedRating (name)": [
                "Outstanding",
                "Special Measures",
                "Serious Weaknesses",
            ],
            "MSOA (code)": [
                "City of London 001F",
                "City of London 001F",
                "Camden 007D",
            ],
            "LSOA (code)": [
                "City of London 001F",
                "City of London 001F",
                "Camden 007D",
            ],
            "StatutoryLowAge": [3, 7, 3],
            "StatutoryHighAge": [11, 18, 5],
            "Street": ["St James's Passage", "St Giles' Terrace", "2 New Change"],
            "Locality": ["Dukes Place", "Barbican", "Victoria"],
            "Address3": [None, None, None],
            "Town": ["London", "Coventry", "Leeds"],
            "County (name)": [None, None, None],
        }
    )


@pytest.fixture
def gias_links():
    return pd.DataFrame(
        {
            "URN": [100154, 100155, 100156],
            "LinkURN": [100153, 100153, 100153],
            "LinkName": ["School C", "School C", "School C"],
            "LinkType": ["Predecessor", "Successor", "Successor"],
            "LinkEstablishedDate": ["31/08/2021", "15/09/2021", None],
        }
    )


@pytest.fixture
def prepared_schools_data(gias_data: pd.DataFrame, gias_links: pd.DataFrame) -> dict:
    return prepare_schools_data(
        StringIO(gias_data.to_csv()), StringIO(gias_links.to_csv())
    )


@pytest.fixture
def bfr_sofa_data():
    return pd.DataFrame(
        {
            "TrustUPIN": [
                140992,
                140992,
                140992,
                140992,
                140992,
                140992,
                140992,
                140992,
                140992,
                140992,
            ],
            "Title": [
                "Income from trading activities",
                "Local authority revenue income",
                "Balance c/f to next period ",
                "Pupil numbers (actual and estimated)",
                "Total staff costs",
                "Other government grants",
                "Other income, including investment income",
                "Total DfE/ESFA revenue income",
                "Total revenue expenditure",
                "Total revenue income",
            ],
            "EFALineNo": [211, 200, 430, 999, 335, 205, 220, 199, 380, 298],
            "Y1P1": [296, 654, 1388, 2228, 5993, 0, 403, 6477, 8042, 7834],
            "Y1P2": [279, 444, 1152, 2197, 4458, 0, 240, 4777, 5582, 5346],
            "Y2P1": [399, 654, 1017, 0, 6349, 1, 404, 6647, 8107, 7972],
            "Y2P2": [293, 539, 1098, 0, 4785, 9, 267, 5252, 6187, 6268],
        }
    )


@pytest.fixture
def bfr_3y_data():
    return pd.DataFrame(
        {
            "TrustUPIN": [140992, 140992, 140992, 140992],
            "EFALineNo": [2980, 4300, 3800, 9000],
            "Y2": [14381, 1132, 14347, 2168],
            "Y3": [14325, 655, 14802, 2174],
            "Y4": [14491, -93, 15239, 2158],
        }
    )


@pytest.fixture
def academies():
    return pd.DataFrame(
        {
            "Trust UPIN": [140992, 140993],
            "Company Registration Number": ['06666666', '01111111'],
            "Total pupils in trust": [100, 200],
            "Trust Revenue reserve": [10000, 10000],
        }
    )

@pytest.fixture
def academies_y1():
    return pd.DataFrame(
        {
            "Trust UPIN": [140992, 140993],
            "Company Registration Number": ['06666666', '01111111'],
            "Total pupils in trust": [90, 190],
            "Trust Revenue reserve": [9800, 98000],
        }
    )


@pytest.fixture
def academies_y2():
    return pd.DataFrame(
        {
            "Trust UPIN": [140992, 140993],
            "Company Registration Number": ['06666666', '01111111'],
            "Total pupils in trust": [80, 180],
            "Trust Revenue reserve": [9400, 9400],
        }
    )


@pytest.fixture
def prepared_bfr_data(
    bfr_sofa_data: pd.DataFrame,
    bfr_3y_data: pd.DataFrame,
    academies_y2: pd.DataFrame,
    academies_y1: pd.DataFrame,
    academies: pd.DataFrame,
) -> list:
    return build_bfr_data(
        2023,
        StringIO(bfr_sofa_data.to_csv()),
        StringIO(bfr_3y_data.to_csv()),
        academies_y2,
        academies_y1,
        academies,
    )
