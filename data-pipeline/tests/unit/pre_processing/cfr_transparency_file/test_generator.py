import io

import numpy as np
import pandas as pd
import pytest

from pipeline.pre_processing.cfr.transparency_file.generator import (
    build_transparency_files,
)


def test_build_transparency_files_structure():
    # 1. Setup Mock Inputs
    year = 2025

    # This schema is defined in input_schemas.cfr_raw_cols["default"]
    all_cols = [
        "LANumber",
        "LEAEstab",
        "Estab",
        "Federated Flag",
        "LAEstab of School in Federation 1",
        "LAEstab of School in Federation 2",
        "LAEstab of School in Federation 3",
        "LAEstab of School in Federation 4",
        "LAEstab of School in Federation 5",
        "LAEstab of School in Federation 6",
        "LAEstab of School in Federation 7",
        "LAEstab of School in Federation 8",
        "LAEstab of School in Federation 9",
        "LAEstab of School in Federation 10",
        "I01",
        "I02",
        "I03",
        "I04",
        "I05",
        "I06",
        "I07",
        "I08a",
        "I08b",
        "I09",
        "I10",
        "I11",
        "I12",
        "I13",
        "I15",
        "I16",
        "I17",
        "I18c",
        "I18d",
        "E01",
        "E02",
        "E03",
        "E04",
        "E05",
        "E06",
        "E07",
        "E08",
        "E09",
        "E10",
        "E11",
        "E12",
        "E13",
        "E14",
        "E15",
        "E16",
        "E17",
        "E18",
        "E19",
        "E20A",
        "E20B",
        "E20C",
        "E20D",
        "E20E",
        "E20F",
        "E20G",
        "E21",
        "E22",
        "E23",
        "E24",
        "E25",
        "E26",
        "E27",
        "E28a",
        "E28b",
        "E29",
        "E30",
        "E31",
        "E32",
        "OB01",
        "OB02",
        "OB03",
        "CI01",
        "CI03",
        "CI04",
        "CE01",
        "CE02",
        "CE03",
        "CE04A",
        "CE04B",
        "CE04C",
        "CE04D",
        "CE04E",
        "B01",
        "B02",
        "B03",
        "B05",
        "B06",
        "B07",
    ]

    header = ",".join(all_cols)
    row1 = (
        ["100", "1001001", "1001", "No"] + ["0"] * 10 + ["1000"] * (len(all_cols) - 14)
    )
    row2 = (
        ["100", "1001002", "1002", "Yes"]
        + ["1001003"]
        + ["0"] * 9
        + ["2000"] * (len(all_cols) - 14)
    )

    full_cfr_csv = header + "\n" + ",".join(row1) + "\n" + ",".join(row2)
    cfr_raw_blob = io.StringIO(full_cfr_csv)

    gias = pd.DataFrame(
        {
            "LA (code)": [100, 100, 100],
            "LA (name)": ["Test LA", "Test LA", "Test LA"],
            "EstablishmentNumber": [1001, 1002, 1003],
            "LAEstab": [1001001, 1001002, 1001003],
            "URN": [1, 2, 3],
            "EstablishmentName": ["School 1", "School 2", "School 3"],
            "EstablishmentStatus (name)": ["Open", "Open", "Open"],
            "EstablishmentTypeGroup (name)": ["Local authority maintained schools"] * 3,
            "TypeOfEstablishment (name)": ["Community school"] * 3,
            "PhaseOfEducation (name)": ["Primary"] * 3,
            "StatutoryLowAge": [5] * 3,
            "StatutoryHighAge": [11] * 3,
            "Gender (name)": ["Mixed"] * 3,
            "AdmissionsPolicy (name)": ["Non-selective"] * 3,
            "OfficialSixthForm (name)": ["Has no sixth form"] * 3,
            "UrbanRural (name)": ["Urban"] * 3,
            "CloseDate": [np.nan] * 3,
            "OpenDate": ["2000-01-01"] * 3,
        }
    )

    pru = pd.DataFrame(columns=["LAEstab", "Headcount"]).astype(
        {"LAEstab": "Int64", "Headcount": float}
    )
    hospital_schools = pd.DataFrame(
        columns=["LAEstab", "GHSIndicator", "TotalHeadcount"]
    ).astype({"LAEstab": "Int64", "GHSIndicator": "string", "TotalHeadcount": float})
    sen = pd.DataFrame(
        {
            "URN": [1, 2, 3],
            "Total pupils": [100, 200, 50],
            "SEN support": [10, 20, 5],
            "EHC plan": [2, 4, 1],
        }
    ).astype({"Total pupils": float, "SEN support": float, "EHC plan": float})

    census = (
        pd.DataFrame(
            {
                "URN": [1, 2, 3],
                "total boarders": [0, 0, 0],
                "number of pupils known to be eligible for free school meals": [
                    20,
                    40,
                    10,
                ],
                "number of pupils whose first language is known or believed to be other than English": [
                    5,
                    10,
                    2,
                ],
                "Percentage Free school meals": [20.0, 20.0, 20.0],
                "% of pupils whose first language is known or believed to be other than English": [
                    5.0,
                    5.0,
                    4.0,
                ],
                "Total Number of Teachers (Full-Time Equivalent)": [5.0, 10.0, 2.5],
                "Total Number of Teaching Assistants (Full-Time Equivalent)": [
                    2.0,
                    4.0,
                    1.0,
                ],
                "Teachers with Qualified Teacher Status (%) (Headcount)": [
                    90.0,
                    95.0,
                    100.0,
                ],
            }
        )
        .astype(
            {
                "total boarders": float,
                "number of pupils known to be eligible for free school meals": float,
                "number of pupils whose first language is known or believed to be other than English": float,
                "Percentage Free school meals": float,
                "% of pupils whose first language is known or believed to be other than English": float,
                "Total Number of Teachers (Full-Time Equivalent)": float,
                "Total Number of Teaching Assistants (Full-Time Equivalent)": float,
                "Teachers with Qualified Teacher Status (%) (Headcount)": float,
            }
        )
        .set_index("URN")
    )

    lookup_la = pd.DataFrame(
        {
            "old_la_code": [100],
            "LA (name)": ["Test LA"],
            "region_name": ["Inner London"],
        }
    )

    # Last year mocks
    census_ly = census.copy()
    sen_ly = sen.set_index("URN").copy()
    pru_ly = pru.copy()
    hospital_ly = hospital_schools.copy()

    # 2. Execute
    master_list, transparency_file = build_transparency_files(
        cfr_raw_blob=cfr_raw_blob,
        gias=gias,
        pru=pru,
        hospital_schools=hospital_schools,
        sen=sen,
        census=census,
        lookup_la=lookup_la,
        census_last_year=census_ly,
        sen_last_year=sen_ly,
        pru_last_year=pru_ly,
        hospital_schools_last_year=hospital_ly,
        year=year,
    )

    # 3. Assertions
    assert isinstance(master_list, pd.DataFrame)
    assert isinstance(transparency_file, pd.DataFrame)

    # Check DNS logic for School 3 (member of federation 1001002)
    school_3 = transparency_file[transparency_file["LAEstab"] == 1001003].iloc[0]
    # Check that Did Not Supply flag is correct
    assert school_3["Did Not Supply flag"] == "Y"
    assert school_3["Federated submission"] == "Non returning school"
    # Ensure financials are masked for DNS school
    assert school_3["I01 Funds delegated by the LA"] == "DNS"

    # Check normal school
    school_1 = transparency_file[transparency_file["LAEstab"] == 1001001].iloc[0]
    assert school_1["Did Not Supply flag"] == "N"
    assert school_1["Federated submission"] == "No"
    assert school_1["I01 Funds delegated by the LA"] != "DNS"

    # Check Lead school
    school_2 = transparency_file[transparency_file["LAEstab"] == 1001002].iloc[0]
    assert school_2["Did Not Supply flag"] == "N"
    assert school_2["Federated submission"] == "Lead school"

    # Verify Column Ordering in Transparency File
    expected_order = [
        "LA",
        "LA Name",
        "Estab",
        "LAEstab",
        "Did Not Supply flag",
        "Lead school in federation",
        "London Weighting",
        "No pupils",
        "Overall Phase",
        "% of pupils eligible for FSM",
        "% of pupils with SEN support",
        "% of pupils with EHCP",
        "School Name",
        "FTE Number of teachers",
        "Number of pupils in 6th form",
        "Type",
        "URN",
        "Admissions policy",
        "Period covered by return",
        "Federated submission",
        "I01 Funds delegated by the LA",
        "I02 Funding for 6th form students",
        "I01/2 Total pre and Post-16 Funding",
        "I03 SEN funding",
        "I04 Funding for minority ethnic pupils",
        "I05 Pupil premium",
        "I06 Other government grants",
        "I07 Other grants and payments",
        "I08a Income from lettings",
        "I08b Income from facilities and services",
        "I08a/b Total income from facilities and services",
        "I09 Income from catering",
        "I10 Receipts from supply teacher insurance claims",
        "I11 Receipts from other insurance claims",
        "I12 Income from contributions to visits etc.",
        "I13 Donations and/or private funds",
        "I15 Pupil focussed extended school funding and / or grants",
        "I16 Community focussed school funding and / or grants",
        "I17 Community focused school facilities income",
        "I18c Income from the £1bn COVID-19 catch-up package announced on 20 July 2020",
        "I18d Income from other additional grants",
        "I18 Total additional grant for schools",
        "E01 Teaching staff",
        "E02 Supply teaching staff",
        "E03 Education support staff",
        "E04 Premises staff",
        "E05 Administrative and clerical staff",
        "E06 Catering staff",
        "E07 Cost of other staff",
        "E08 Indirect employee expenses",
        "E09 Development and training",
        "E10 Supply teacher insurance",
        "E11 Staff related insurance",
        "E12 Building maintenance and improvement",
        "E13 Grounds maintenance and improvement",
        "E14 Cleaning and caretaking",
        "E15 Water and sewerage",
        "E16 Energy",
        "E17 Rates",
        "E18 Other occupation costs",
        "E19 Learning resources (not ICT equipment)",
        "E20A Connectivity",
        "E20B Onsite servers",
        "E20C IT learning resources",
        "E20D Administration software and systems",
        "E20E Laptops, desktops and tablets",
        "E20F Other hardware",
        "E20G IT support",
        "E21 Exam fees",
        "E22 Administrative supplies",
        "E23 Other insurance premiums",
        "E24 Special facilities",
        "E25 Catering supplies",
        "E26 Agency supply teaching staff",
        "E27 Bought in professional services – curriculum",
        "E28a Bought in professional services, other, not PFI",
        "E28b Bought in professional services, other, PFI",
        "E28a/b Total bought in professional services",
        "E29 Loan interest",
        "E30 Direct revenue financing (Revenue contributions to capital)",
        "E31 Community focused school staff",
        "E32 Community focused school costs",
        "OB01 Opening pupil-focused revenue balance",
        "OB02 Opening community-focused revenue balance",
        "OB03 Opening capital balance",
        "CI01 Capital income",
        "CI03 Voluntary or private income",
        "CI04 Direct revenue financing",
        "CE01 Acquisition of land and existing buildings",
        "CE02 New construction, conversion and renovation",
        "CE03 Vehicles, plant, equipment and machinery",
        "CE04A Connectivity",
        "CE04B Onsite servers",
        "CE04C Administration software and systems",
        "CE04D Laptops desktops and tablets",
        "CE04E Other hardware",
        "B01 Committed revenue balances",
        "B02 Uncommitted revenue balances",
        "B03 Devolved formula capital balance",
        "B05 Other capital balances",
        "B06 Community-focused school revenue balances",
        "B07 Outstanding balance on capital loans to school",
        "Revenue Reserve: B01 + B02 + B06",
        "In-year Balance: Total Income (I01:I18 - E30) - Total Expenditure (E01:E29 + E31 + E32)",
        "Grant Funding: (I01:I07) + I15 + I16 + I18a/b/c/d",
        "Direct Grants: I01:I02 + I06:I07",
        "Community Grants: I16+I18",
        "Targetted Grants: I03:I05 + I15",
        "Self Generated Funding: (I08a/b:I13) + I17",
        "Total Income: I01:I18 - E30",
        "Teaching Staff E01",
        "Supply Staff: E02 + E10 + E26",
        "Education support staff: E03",
        "Other Staff Costs: (E07:E09) + E11",
        "Staff Total: (E01:E03) + E05 + (E07: E11) + E26",
        "Maintenance & Improvement: E12 + E13",
        "Premises: (E12:E14) + E04 + E28b",
        "Catering Expenses: E06 + E25",
        "Occupation: E06 + (E15:E18) + E23 + E25",
        "Supplies and Services: (E19:E22) + (E27:E28b)",
        "Educational Supplies: (E19:E21)",
        "Brought in Professional Services: (E27 + E28a)",
        "Community Exp: E31 + E32",
        "Total Expenditure: (E01:E29 + E31 + E32)",
    ]

    assert list(transparency_file.columns) == expected_order
