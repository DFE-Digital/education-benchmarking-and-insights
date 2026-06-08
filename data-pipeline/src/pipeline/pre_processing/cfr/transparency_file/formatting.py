import numpy as np
import pandas as pd


def format_dns(sfb: pd.DataFrame, series: pd.Series, decimals: int = 0) -> pd.Series:
    """Helper function to format numeric columns and apply DNS masking."""
    formatted = (
        series.round(decimals).astype(str)
        if decimals > 0
        else series.round(decimals).astype("Int64", errors="ignore").astype(str)
    )
    formatted = formatted.replace("<NA>", np.nan)
    return np.where(
        sfb["Did Not Supply flag"] == "DNS",
        "DNS",
        np.where(formatted.isna() | (formatted == "nan"), "DNS", formatted),
    )


def build_sfb_maintained(
    mergedworking: pd.DataFrame, lookup_la: pd.DataFrame
) -> pd.DataFrame:
    """Final mapping to SFB_Maintained schema (Step 3 SQL: [dbo].[SFB_Maintained_2024-25])."""
    # Merge LA Name from lookup
    a = mergedworking.merge(lookup_la, left_on="LA", right_on="old_la_code", how="left")

    out = pd.DataFrame()
    out["LA"] = pd.to_numeric(a["LA"], errors="coerce").astype("Int64")
    out["LA Name"] = a["LA (name)"]
    out["Region"] = a["Region"]
    out["London Borough"] = a["London Borough"]
    out["Estab"] = pd.to_numeric(a["Estab"], errors="coerce").astype("Int64")
    out["LAEstab"] = a["LAEstab"]
    out["URN"] = a["URN"]
    out["School Name"] = a["EstablishmentName"]
    out["Phase"] = a["SFB Phase"]
    out["Overall Phase"] = a["Overall Phase"]
    out["Lowest age of pupils"] = a["StatutoryLowAge"]
    out["Highest age of pupils"] = a["StatutoryHighAge"]
    out["Type"] = a["TypeOfEstablishment (name)"]
    out["UrbanRural"] = a["UrbanRural (name)"]

    out["Period covered by return"] = a["Period Covered"]
    out["Did Not Supply flag"] = a["DNS"].map(
        {"n/a": "N", "LeadSchool": "N", "DNS": "DNS"}
    )
    out["FederatedSubmission"] = a["DNS"].map(
        {"n/a": "No", "LeadSchool": "Lead school", "DNS": "Non returning school"}
    )
    out["Lead school in federation"] = np.select(
        [a["DNS"] == "LeadSchool", a["DNS"] == "DNS"],
        [a["LAEstab"], a["Lead_school"]],
        default=0,
    )
    out["General Hospital School Indicator"] = np.where(
        (a["GHSIndicator"] == "GHS").fillna(False), "Y", "N"
    )

    out["IndividualPupilsFTE"] = a["Ind. Pupils FTE"]
    out["AggregatedPupilsFTE"] = a["Aggregated Pupils FTE"]
    out["IndTeachers_FTE"] = a.get("Teachers FTE_ind", 0)
    out["AggregatedTeachersFTE"] = a.get("Teachers FTE_agg", 0)
    out["Gender"] = a["Gender (name)"]
    out["London Weighting"] = a["London Borough"]

    out["Ind_PC_FSM"] = a.get("% of pupils eligible for FSM_ind", np.nan)
    out["Aggregated_PC_FSM"] = a.get("% of pupils eligible for FSM_agg", np.nan)
    out["Ind_PC_EHCP"] = a.get("% of pupils with EHCP_ind", np.nan)
    out["Aggregated_PC_EHCP"] = a.get("% of pupils with EHCP_agg", np.nan)
    out["Ind_PC_SEN_Support"] = a.get("% of pupils with SEN Support_ind", np.nan)
    out["Aggregated_PC_SEN_Support"] = a.get("% of pupils with SEN Support_agg", np.nan)
    out["Ind_PC_EAL"] = a.get("% of pupils with EAL_ind", np.nan)
    out["Aggregated_PC_EAL"] = a.get("% of pupils with EAL_agg", np.nan)
    out["Ind_PC_Boarders"] = a.get("% of pupils who are Boarders_ind", np.nan)
    out["Aggregated_PC_Boarders"] = a.get("% of pupils who are Boarders_agg", np.nan)

    out["Admissions_Policy"] = a["AdmissionsPolicy (name)"]
    out["PFI"] = a["PFI Funding"]
    out["Has a 6th form"] = a["OfficialSixthForm (name)"]
    out["Ind_VIthForm"] = a.get("No of pupils in 6th form_ind", 0)
    out["Aggregated_VIthForm"] = a.get("No of pupils in 6th form_agg", 0)
    out["Ind_TA_FTE"] = a.get("FTE of Teaching Assistants_ind", 0)
    out["Aggregated_TA_FTE"] = a.get("FTE of Teaching Assistants_agg", 0)
    out["Teachers_PC_QTS"] = a.get("Teachers_PC_QTS", np.nan)

    # Financial columns
    fin_cols = [
        "I01 Pre-16 Funding", "I02 Post-16 Funding", "I01/2 Pre and Post-16 Funding",
        "I03 SEN", "I04 Funding for minority ethnic pupils", "I05 Pupil Premium",
        "I06 Other DfE/EFA Revenue Grants", "I07 Other income (LA & other Government grants)",
        "I08a Income from lettings", "I08b Income from facilities and services",
        "I08a/b Income from facilities and services", "I09 Income from catering",
        "I10 Receipts from supply teacher insurance claims", "I11 Receipts from other insurance claims",
        "I12 Income from contributions to visits etc", "I13 Donations and/or voluntary funds",
        "I15 Pupil focussed extended school funding and/or grants",
        "I16 Community focussed school funding and/or grants",
        "I17 Community focused school facilities income",
        "I18c Income from the £1bn COVID-19 catch-up package announced on 20 July 2020",
        "I18d Income from other additional grants", "I18 Total additional grant for schools",
        "Total Income: I01 to I08, I11 to I15, I18 minus E30", "Total Income: I01 to I18 minus E30",
        "E01 Teaching staff", "E02 Supply teaching staff", "E03 Education support staff",
        "E04 Premises staff", "E05 Administrative and clerical staff", "E06 Catering staff",
        "E07 Other staff", "E08 Indirect employee expenses", "E09 Staff development and training",
        "E10 Supply teacher insurance", "E11 Staff-related insurance",
        "E12 Building maintenance and improvement", "E13 Grounds maintenance and improvement",
        "E14 Cleaning and caretaking", "E15 Water and sewerage", "E16 Energy", "E17 Rates",
        "E18 Other occupation costs", "E19 Learning resources (not ICT equipment)",
        "E20A Connectivity", "E20B Onsite servers", "E20C IT learning resources",
        "E20D Administration software and systems", "E20E Laptops, desktops and tablets",
        "E20F Other hardware", "E20G IT support", "E21 Examination fees",
        "E22 Administrative supplies - non educational", "E23 Other insurance premiums",
        "E24 Special facilities", "E25 Catering supplies", "E26 Agency supply teaching staff",
        "E27 Educational Consultancy", "E28a Bought in professional services, other, not PFI",
        "E28b Bought in professional services, other, PFI", "E28a/b Bought in professional services",
        "E29 Interest charges for Loan and Bank",
        "E30 Direct revenue financing (Revenue contributions to capital)",
        "E31 Community focused school staff", "E32 Community focused school costs",
        "Total Expenditure:E01 to E29 and E31 to E32 minus I9, I10, I16 and I17",
        "Total Expenditure:E01 to E29 and E31 to E32",
        "OB01 Opening pupil-focused revenue balance", "OB02 Opening community-focused revenue balance",
        "OB03 Opening capital balance", "CI01 Capital income", "CI03 Voluntary or private income",
        "CI04 Direct revenue financing", "CE01 Acquisition of land and existing buildings",
        "CE02 New construction, conversion and renovation", "CE03 Vehicles, plant, equipment and machinery",
        "CE04 Information and communication technology", "CE04A Connectivity", "CE04B Onsite servers",
        "CE04C Administration software and systems", "CE04D Laptops desktops and tablets",
        "CE04E Other hardware", "B01 Committed revenue balances", "B02 Uncommitted revenue balances",
        "B03 Devolved formula capital balance", "B05 Other capital balances",
        "B06 Community-focused school revenue balances", "B07 Outstanding balance on capital loans to school",
        "Revenue Reserve: B01 + B02 + B06", "In-year Balance",
        "Grant Funding: (I01:I07) + I15 + I16 + I18a/b/c/d", "Direct Grants: I01:I02 + I06:I07",
        "Community Grants: I16+I18", "Targetted Grants: I03:I05 + I15",
        "Self Generated Funding: (I08a/b:I13) + I17", "Total income", "Teaching Staff: E01",
        "Supply Staff: E02 + E10 + E26", "Education support staff: E03",
        "Other Staff Costs: (E07:E09) + E11", "Staff Total: (E01:E03) + E05 + (E07: E11) + E26",
        "Maintenance & Improvement: E12 + E13", "Premises: (E12:E14) + E04 + E28b",
        "Catering Expenses: E06 + E25", "Occupation: E06 + (E15:E18) + E23 + E25",
        "Supplies and Services: (E19:E22) + (E27:E28b)", "Educational Supplies: (E19:E21)",
        "Brought in Professional Sevices: (E27 + E28a)", "Community Exp: E31 + E32",
        "Total Expenditure excluding E30"
    ]
    for col in fin_cols:
        out[col] = a.get(col, 0)

    return out


def build_maintained_schools_master_list(sfb: pd.DataFrame) -> pd.DataFrame:
    """Replicates My_Step4.sql: final formatting for the master list."""
    out = pd.DataFrame()

    out["LA"] = sfb["LA"]
    out["LA Name"] = sfb["LA Name"]
    out["Region"] = sfb["Region"]
    out["London Borough"] = sfb["London Borough"]
    out["Estab"] = sfb["Estab"]
    out["LAEstab"] = sfb["LAEstab"]
    out["URN"] = sfb["URN"]
    out["School Name"] = sfb["School Name"]
    out["Phase"] = sfb["Phase"]
    out["Overall Phase"] = sfb["Overall Phase"]
    out["Lowest age of pupils"] = sfb["Lowest age of pupils"]
    out["Highest age of pupils"] = sfb["Highest age of pupils"]
    out["Type"] = sfb["Type"]

    out["No Pupils"] = sfb["IndividualPupilsFTE"].round(1)
    out["% of pupils eligible for FSM"] = sfb["Ind_PC_FSM"].round(1)

    out["Period covered by return (months)"] = sfb["Period covered by return"].abs()
    out["Did Not Supply flag"] = sfb["Did Not Supply flag"]
    out["Federation"] = sfb["FederatedSubmission"]
    out["Lead school in federation"] = sfb["Lead school in federation"]

    out["No Teachers"] = sfb["IndTeachers_FTE"].round(1)
    out["Gender"] = sfb["Gender"]
    out["Urban  Rural"] = sfb["UrbanRural"]
    out["London Weighting"] = sfb["London Weighting"]

    out["% of pupils with SEN Statement"] = sfb["Ind_PC_EHCP"].round(1)
    out["% of SEN pupils without Statement"] = sfb["Ind_PC_SEN_Support"].round(1)
    out["% of pupils with EAL"] = sfb["Ind_PC_EAL"].round(1)
    out["% of pupils who are Boarders"] = sfb["Ind_PC_Boarders"].round(1)
    out["Admissions policy"] = sfb["Admissions_Policy"]
    out["PFI"] = sfb["PFI"]
    out["Has a 6th form"] = sfb["Has a 6th form"]

    out["No of pupils in 6th form"] = sfb["Ind_VIthForm"].round(0)

    out["% of teachers with QTS"] = format_dns(sfb, sfb["Teachers_PC_QTS"])
    out["FTE of Teaching Assistants"] = format_dns(sfb, sfb["Ind_TA_FTE"])
    out["FTE of Support Staff"] = format_dns(sfb, pd.Series(0, index=sfb.index))
    out["FTE of Admin Staff"] = format_dns(sfb, pd.Series(0, index=sfb.index))

    fin_mappings = {
        "I01  Funds delegated by the LA": "I01 Pre-16 Funding",
        "I02  Funding for 6th form students": "I02 Post-16 Funding",
        "I03  SEN funding": "I03 SEN",
        "I04  Funding for minority ethnic pupils": "I04 Funding for minority ethnic pupils",
        "I05  Pupil Premium": "I05 Pupil Premium",
        "I06  Other government grants": "I06 Other DfE/EFA Revenue Grants",
        "I07  Other grants and payments": "I07 Other income (LA & other Government grants)",
        "I08  Income from facilities and services": "I08a/b Income from facilities and services",
        "I09  Income from catering": "I09 Income from catering",
        "I10  Receipts from supply teacher insurance claims": "I10 Receipts from supply teacher insurance claims",
        "I11  Receipts from other insurance claims": "I11 Receipts from other insurance claims",
        "I12  Income from contributions to visits etc ": "I12 Income from contributions to visits etc",
        "I13  Donations and or private funds": "I13 Donations and/or voluntary funds",
        "I15  Pupil focussed extended school funding and   or grants": "I15 Pupil focussed extended school funding and/or grants",
        "I16  Community focussed school funding and   or grants": "I16 Community focussed school funding and/or grants",
        "I17  Community focused school facilities income": "I17 Community focused school facilities income",
        "I18  Additional grant for schools": "I18 Total additional grant for schools",
        "Total Income   I01 to I08, I11 to I15, I18": "Total Income: I01 to I08, I11 to I15, I18 minus E30",
        "Total Income   I01 to I18": "Total Income: I01 to I18 minus E30",
        "E01  Teaching Staff": "E01 Teaching staff",
        "E02  Supply teaching staff": "E02 Supply teaching staff",
        "E03 Education support staff": "E03 Education support staff",
        "E04  Premises staff": "E04 Premises staff",
        "E05 Administrative and clerical staff": "E05 Administrative and clerical staff",
        "E06 Catering staff": "E06 Catering staff",
        "E07  Cost of other staff": "E07 Other staff",
        "E08  Indirect employee expenses": "E08 Indirect employee expenses",
        "E09  Development and training": "E09 Staff development and training",
        "E10  Supply teacher insurance": "E10 Supply teacher insurance",
        "E11  Staff related insurance": "E11 Staff-related insurance",
        "E12  Building maintenance and improvement": "E12 Building maintenance and improvement",
        "E13  Grounds maintenance and improvement": "E13 Grounds maintenance and improvement",
        "E14  Cleaning and caretaking": "E14 Cleaning and caretaking",
        "E15  Water and sewerage": "E15 Water and sewerage",
        "E16  Energy": "E16 Energy",
        "E17  Rates": "E17 Rates",
        "E18  Other occupation costs": "E18 Other occupation costs",
        "E19  Learning resources (not ICT equipment)": "E19 Learning resources (not ICT equipment)",
        "E20A  Connectivity": "E20A Connectivity",
        "E20B  Onsite servers": "E20B Onsite servers",
        "E20C  IT learning resources": "E20C IT learning resources",
        "E20D  Administration software and systems": "E20D Administration software and systems",
        "E20E  Laptops, desktops and tablets": "E20E Laptops, desktops and tablets",
        "E20F  Other hardware": "E20F Other hardware",
        "E20G  IT support": "E20G IT support",
        "E21  Exam fees": "E21 Examination fees",
        "E22 Administrative supplies": "E22 Administrative supplies - non educational",
        "E23  Other insurance premiums": "E23 Other insurance premiums",
        "E24  Special facilities ": "E24 Special facilities",
        "E25  Catering supplies": "E25 Catering supplies",
        "E26 Agency supply teaching staff": "E26 Agency supply teaching staff",
        "E27  Bought in professional services - curriculum": "E27 Educational Consultancy",
        "E28a  Bought in professional services - other (except PFI)": "E28a Bought in professional services, other, not PFI",
        "E28b Bought in professional services - other (PFI)": "E28b Bought in professional services, other, PFI",
        "E29  Loan interest": "E29 Interest charges for Loan and Bank",
        "E30 Direct revenue financing (revenue contributions to capital)": "E30 Direct revenue financing (Revenue contributions to capital)",
        "E31  Community focused school staff": "E31 Community focused school staff",
        "E32 Community focused school costs": "E32 Community focused school costs",
        "Total Expenditure  E01 to E29 and E31 to E32 minus I9, I10, I16 and I17": "Total Expenditure:E01 to E29 and E31 to E32 minus I9, I10, I16 and I17",
        "Total Expenditure  E01 to E32": "Total Expenditure:E01 to E29 and E31 to E32",
        "OB01  Opening pupil-focused revenue balance": "OB01 Opening pupil-focused revenue balance",
        "OB02 Opening community-focused revenue balance": "OB02 Opening community-focused revenue balance",
        "OB03 Opening capital balance": "OB03 Opening capital balance",
        "CI01 Capital income": "CI01 Capital income",
        "CI03 Voluntary or private income": "CI03 Voluntary or private income",
        "CI04 Direct revenue financing": "CI04 Direct revenue financing",
        "CE01 Acquisition of land and existing buildings": "CE01 Acquisition of land and existing buildings",
        "CE02 New construction, conversion and renovation": "CE02 New construction, conversion and renovation",
        "CE03 Vehicles, plant, equipment and machinery": "CE03 Vehicles, plant, equipment and machinery",
        "CE04 Information and communication technology": "CE04 Information and communication technology",
        "B01 Committed revenue balances": "B01 Committed revenue balances",
        "B02 Uncommitted revenue balances": "B02 Uncommitted revenue balances",
        "B03 Devolved formula capital balance": "B03 Devolved formula capital balance",
        "B05 Other capital balances": "B05 Other capital balances",
        "B06 Community-focused school revenue balances": "B06 Community-focused school revenue balances",
        "Revenue Reserve   B01 plus B02 plus B06": "Revenue Reserve: B01 + B02 + B06",
        "In-year Balance   Total Income (I01 to I18) minus Total Expenditure (E01 to E32)": "In-year Balance",
        "Grant Funding": "Grant Funding: (I01:I07) + I15 + I16 + I18a/b/c/d",
        "Direct Grant": "Direct Grants: I01:I02 + I06:I07",
        "Community Grants": "Community Grants: I16+I18",
        "Targeted Grants": "Targetted Grants: I03:I05 + I15",
        "Self Generated Funding": "Self Generated Funding: (I08a/b:I13) + I17",
        "Total Income ": "Total income",
        "Supply Staff": "Supply Staff: E02 + E10 + E26",
        "Other Staff Costs": "Other Staff Costs: (E07:E09) + E11",
        "Staff Total ": "Staff Total: (E01:E03) + E05 + (E07: E11) + E26",
        "Maintenance & Improvement": "Maintenance & Improvement: E12 + E13",
        "Premises": "Premises: (E12:E14) + E04 + E28b",
        "Catering Exp": "Catering Expenses: E06 + E25",
        "Occupation": "Occupation: E06 + (E15:E18) + E23 + E25",
        "Supplies and Services": "Supplies and Services: (E19:E22) + (E27:E28b)",
        "Educational Supplies": "Educational Supplies: (E19:E21)",
        "Brought in Professional Sevices": "Brought in Professional Sevices: (E27 + E28a)",
        "Cost of Finance": None,
        "Community Exp": "Community Exp: E31 + E32",
        "Total Expenditure": "Total Expenditure excluding E30",
    }

    for target_col, source_col in fin_mappings.items():
        if source_col is None:
            out[target_col] = format_dns(sfb, pd.Series(0, index=sfb.index))
        else:
            out[target_col] = format_dns(
                sfb, pd.to_numeric(sfb[source_col], errors="coerce")
            )

    return out


def build_maintained_schools_download_file(sfb: pd.DataFrame) -> pd.DataFrame:
    """Replicates My_Step5.sql: final formatting for the download file."""
    out = pd.DataFrame()

    out["LA"] = sfb["LA"]
    out["LA Name"] = sfb["LA Name"]
    out["Estab"] = sfb["Estab"]
    out["LAEstab"] = sfb["LAEstab"]
    out["Did Not Supply flag"] = sfb["Did Not Supply flag"].str.replace("DNS", "Y")
    out["Lead school in federation"] = sfb["Lead school in federation"]
    out["London Weighting"] = sfb["London Weighting"]
    out["No pupils (including dual registrations)"] = sfb["AggregatedPupilsFTE"].round(1)
    out["Overall Phase"] = sfb["Overall Phase"]
    out["% of pupils eligible for FSM"] = sfb["Aggregated_PC_FSM"].round(1)
    out["% of pupils with SEN support"] = sfb["Aggregated_PC_SEN_Support"].round(1)
    out["% of pupils with EHCP"] = sfb["Aggregated_PC_EHCP"].round(1)
    out["School Name"] = sfb["School Name"]
    out["FTE Number of teachers"] = sfb["AggregatedTeachersFTE"].round(1)
    out["Number of pupils in 6th form"] = sfb["Aggregated_VIthForm"].round(0)
    out["Type"] = sfb["Type"]
    out["URN"] = sfb["URN"]
    out["Admissions policy"] = sfb["Admissions_Policy"]
    out["Period covered by return"] = sfb["Period covered by return"]
    out["Federated submission"] = sfb["FederatedSubmission"]

    fin_mappings = {
        "I01 Funds delegated by the LA": "I01 Pre-16 Funding",
        "I02 Funding for 6th form students": "I02 Post-16 Funding",
        "I01/2 Total pre and Post-16 Funding": "I01/2 Pre and Post-16 Funding",
        "I03 SEN funding": "I03 SEN",
        "I04 Funding for minority ethnic pupils": "I04 Funding for minority ethnic pupils",
        "I05 Pupil premium": "I05 Pupil Premium",
        "I06 Other government grants": "I06 Other DfE/EFA Revenue Grants",
        "I07 Other grants and payments": "I07 Other income (LA & other Government grants)",
        "I08a Income from lettings": "I08a Income from lettings",
        "I08b Income from facilities and services": "I08b Income from facilities and services",
        "I08a/b Total income from facilities and services": "I08a/b Income from facilities and services",
        "I09 Income from catering": "I09 Income from catering",
        "I10 Receipts from supply teacher insurance claims": "I10 Receipts from supply teacher insurance claims",
        "I11 Receipts from other insurance claims": "I11 Receipts from other insurance claims",
        "I12 Income from contributions to visits etc.": "I12 Income from contributions to visits etc",
        "I13 Donations and/or private funds": "I13 Donations and/or voluntary funds",
        "I15 Pupil focussed extended school funding and / or grants": "I15 Pupil focussed extended school funding and/or grants",
        "I16 Community focussed school funding and / or grants": "I16 Community focussed school funding and/or grants",
        "I17 Community focused school facilities income": "I17 Community focused school facilities income",
        "I18c Income from the £1bn COVID-19 catch-up package announced on 20 July 2020": "I18c Income from the £1bn COVID-19 catch-up package announced on 20 July 2020",
        "I18d Income from other additional grants": "I18d Income from other additional grants",
        "I18 Total additional grant for schools": "I18 Total additional grant for schools",
        "E01 Teaching staff": "E01 Teaching staff",
        "E02 Supply teaching staff": "E02 Supply teaching staff",
        "E03 Education support staff": "E03 Education support staff",
        "E04 Premises staff": "E04 Premises staff",
        "E05 Administrative and clerical staff": "E05 Administrative and clerical staff",
        "E06 Catering staff": "E06 Catering staff",
        "E07 Cost of other staff": "E07 Other staff",
        "E08 Indirect employee expenses": "E08 Indirect employee expenses",
        "E09 Development and training": "E09 Staff development and training",
        "E10 Supply teacher insurance": "E10 Supply teacher insurance",
        "E11 Staff related insurance": "E11 Staff-related insurance",
        "E12 Building maintenance and improvement": "E12 Building maintenance and improvement",
        "E13 Grounds maintenance and improvement": "E13 Grounds maintenance and improvement",
        "E14 Cleaning and caretaking": "E14 Cleaning and caretaking",
        "E15 Water and sewerage": "E15 Water and sewerage",
        "E16 Energy": "E16 Energy",
        "E17 Rates": "E17 Rates",
        "E18 Other occupation costs": "E18 Other occupation costs",
        "E19 Learning resources (not ICT equipment)": "E19 Learning resources (not ICT equipment)",
        "E20A Connectivity": "E20A Connectivity",
        "E20B Onsite servers": "E20B Onsite servers",
        "E20C IT learning resources": "E20C IT learning resources",
        "E20D Administration software and systems": "E20D Administration software and systems",
        "E20E Laptops, desktops and tablets": "E20E Laptops, desktops and tablets",
        "E20F Other hardware": "E20F Other hardware",
        "E20G IT support": "E20G IT support",
        "E21 Exam fees": "E21 Examination fees",
        "E22 Administrative supplies": "E22 Administrative supplies - non educational",
        "E23 Other insurance premiums": "E23 Other insurance premiums",
        "E24 Special facilities": "E24 Special facilities",
        "E25 Catering supplies": "E25 Catering supplies",
        "E26 Agency supply teaching staff": "E26 Agency supply teaching staff",
        "E27 Bought in professional services – curriculum": "E27 Educational Consultancy",
        "E28a Bought in professional services, other, not PFI": "E28a Bought in professional services, other, not PFI",
        "E28b Bought in professional services, other, PFI": "E28b Bought in professional services, other, PFI",
        "E28a/b Total bought in professional services": "E28a/b Bought in professional services",
        "E29 Loan interest": "E29 Interest charges for Loan and Bank",
        "E30 Direct revenue financing (Revenue contributions to capital)": "E30 Direct revenue financing (Revenue contributions to capital)",
        "E31 Community focused school staff": "E31 Community focused school staff",
        "E32 Community focused school costs": "E32 Community focused school costs",
        "OB01 Opening pupil-focused revenue balance": "OB01 Opening pupil-focused revenue balance",
        "OB02 Opening community-focused revenue balance": "OB02 Opening community-focused revenue balance",
        "OB03 Opening capital balance": "OB03 Opening capital balance",
        "CI01 Capital income": "CI01 Capital income",
        "CI03 Voluntary or private income": "CI03 Voluntary or private income",
        "CI04 Direct revenue financing": "CI04 Direct revenue financing",
        "CE01 Acquisition of land and existing buildings": "CE01 Acquisition of land and existing buildings",
        "CE02 New construction, conversion and renovation": "CE02 New construction, conversion and renovation",
        "CE03 Vehicles, plant, equipment and machinery": "CE03 Vehicles, plant, equipment and machinery",
        "CE04A Connectivity": "CE04A Connectivity",
        "CE04B Onsite servers": "CE04B Onsite servers",
        "CE04C Administration software and systems": "CE04C Administration software and systems",
        "CE04D Laptops desktops and tablets": "CE04D Laptops desktops and tablets",
        "CE04E Other hardware": "CE04E Other hardware",
        "B01 Committed revenue balances": "B01 Committed revenue balances",
        "B02 Uncommitted revenue balances": "B02 Uncommitted revenue balances",
        "B03 Devolved formula capital balance": "B03 Devolved formula capital balance",
        "B05 Other capital balances": "B05 Other capital balances",
        "B06 Community-focused school revenue balances": "B06 Community-focused school revenue balances",
        "B07 Outstanding balance on capital loans to school": "B07 Outstanding balance on capital loans to school",
        "Revenue Reserve: B01 + B02 + B06": "Revenue Reserve: B01 + B02 + B06",
        "In-year Balance: Total Income (I01:I18 - E30) - Total Expenditure (E01:E29 + E31 + E32)": "In-year Balance",
        "Grant Funding: (I01:I07) + I15 + I16 + I18a/b/c/d": "Grant Funding: (I01:I07) + I15 + I16 + I18a/b/c/d",
        "Direct Grants: I01:I02 + I06:I07": "Direct Grants: I01:I02 + I06:I07",
        "Community Grants: I16+I18": "Community Grants: I16+I18",
        "Targetted Grants: I03:I05 + I15": "Targetted Grants: I03:I05 + I15",
        "Self Generated Funding: (I08a/b:I13) + I17": "Self Generated Funding: (I08a/b:I13) + I17",
        "Total Income: I01:I18 - E30": "Total income",
        "Teaching Staff E01": "Teaching Staff: E01",
        "Supply Staff: E02 + E10 + E26": "Supply Staff: E02 + E10 + E26",
        "Education support staff: E03": "Education support staff: E03",
        "Other Staff Costs: (E07:E09) + E11": "Other Staff Costs: (E07:E09) + E11",
        "Staff Total: (E01:E03) + E05 + (E07: E11) + E26": "Staff Total: (E01:E03) + E05 + (E07: E11) + E26",
        "Maintenance & Improvement: E12 + E13": "Maintenance & Improvement: E12 + E13",
        "Premises: (E12:E14) + E04 + E28b": "Premises: (E12:E14) + E04 + E28b",
        "Catering Expenses: E06 + E25": "Catering Expenses: E06 + E25",
        "Occupation: E06 + (E15:E18) + E23 + E25": "Occupation: E06 + (E15:E18) + E23 + E25",
        "Supplies and Services: (E19:E22) + (E27:E28b)": "Supplies and Services: (E19:E22) + (E27:E28b)",
        "Educational Supplies: (E19:E21)": "Educational Supplies: (E19:E21)",
        "Brought in Professional Services: (E27 + E28a)": "Brought in Professional Sevices: (E27 + E28a)",
        "Community Exp: E31 + E32": "Community Exp: E31 + E32",
        "Total Expenditure: (E01:E29 + E31 + E32)": "Total Expenditure excluding E30",
    }

    for target_col, source_col in fin_mappings.items():
        out[target_col] = format_dns(
            sfb, pd.to_numeric(sfb[source_col], errors="coerce")
        )

    out_sorted = out.sort_values(by=["LA", "LAEstab"])

    return out_sorted
