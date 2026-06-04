import numpy as np
import pandas as pd

INCOME_RAW = [
    "I01", "I02", "I03", "I04", "I05", "I06", "I07", "I08a", "I08b", "I09", "I10", 
    "I11", "I12", "I13", "I15", "I16", "I17", "I18c", "I18d",
]
EXPENSE_RAW = [
    "E01", "E02", "E03", "E04", "E05", "E06", "E07", "E08", "E09", "E10", "E11",
    "E12", "E13", "E14", "E15", "E16", "E17", "E18", "E19", "E20A", "E20B", "E20C",
    "E20D", "E20E", "E20F", "E20G", "E21", "E22", "E23", "E24", "E25", "E26", "E27",
    "E28a", "E28b", "E29", "E31", "E32",
]
CAPITAL_RAW = [
    "OB01", "OB02", "OB03", "CI01", "CI03", "CI04", "CE01", "CE02", "CE03", "CE04A",
    "CE04B", "CE04C", "CE04D", "CE04E", "B01", "B02", "B03", "B05", "B06", "B07",
]


def add_financials(merged: pd.DataFrame) -> pd.DataFrame:
    m = merged.copy()

    # # Pre-cast all financial columns to numeric
    # for c in INCOME_RAW + EXPENSE_RAW + CAPITAL_RAW + ["E30"]:
    #     m[c] = pd.to_numeric(m[c], errors="coerce")

    m["I01 Pre-16 Funding"] = m["I01"]
    m["I02 Post-16 Funding"] = m["I02"]
    m["I01/2 Pre and Post-16 Funding"] = m["I01"] + m["I02"]
    m["I03 SEN"] = m["I03"]
    m["I04 Funding for minority ethnic pupils"] = m["I04"]
    m["I05 Pupil Premium"] = m["I05"]
    m["I06 Other DfE/EFA Revenue Grants"] = m["I06"]
    m["I07 Other income (LA & other Government grants)"] = m["I07"]
    m["I08a Income from lettings"] = m["I08a"]
    m["I08b Income from facilities and services"] = m["I08b"]
    m["I08a/b Income from facilities and services"] = m["I08a"] + m["I08b"]
    m["I09 Income from catering"] = m["I09"]
    m["I10 Receipts from supply teacher insurance claims"] = m["I10"]
    m["I11 Receipts from other insurance claims"] = m["I11"]
    m["I12 Income from contributions to visits etc"] = m["I12"]
    m["I13 Donations and/or voluntary funds"] = m["I13"]
    m["I15 Pupil focussed extended school funding and/or grants"] = m["I15"]
    m["I16 Community focussed school funding and/or grants"] = m["I16"]
    m["I17 Community focused school facilities income"] = m["I17"]
    m[
        "I18c Income from the £1bn COVID-19 catch-up package announced on 20 July 2020"
    ] = m["I18c"]
    m["I18d Income from other additional grants"] = m["I18d"]
    m["I18 Total additional grant for schools"] = m["I18c"] + m["I18d"]

    m["E01 Teaching staff"] = m["E01"]
    m["E02 Supply teaching staff"] = m["E02"]
    m["E03 Education support staff"] = m["E03"]
    m["E04 Premises staff"] = m["E04"]
    m["E05 Administrative and clerical staff"] = m["E05"]
    m["E06 Catering staff"] = m["E06"]
    m["E07 Other staff"] = m["E07"]
    m["E08 Indirect employee expenses"] = m["E08"]
    m["E09 Staff development and training"] = m["E09"]
    m["E10 Supply teacher insurance"] = m["E10"]
    m["E11 Staff-related insurance"] = m["E11"]
    m["E12 Building maintenance and improvement"] = m["E12"]
    m["E13 Grounds maintenance and improvement"] = m["E13"]
    m["E14 Cleaning and caretaking"] = m["E14"]
    m["E15 Water and sewerage"] = m["E15"]
    m["E16 Energy"] = m["E16"]
    m["E17 Rates"] = m["E17"]
    m["E18 Other occupation costs"] = m["E18"]
    m["E19 Learning resources (not ICT equipment)"] = m["E19"]
    m["E20A Connectivity"] = m["E20A"]
    m["E20B Onsite servers"] = m["E20B"]
    m["E20C IT learning resources"] = m["E20C"]
    m["E20D Administration software and systems"] = m["E20D"]
    m["E20E Laptops, desktops and tablets"] = m["E20E"]
    m["E20F Other hardware"] = m["E20F"]
    m["E20G IT support"] = m["E20G"]
    m["E21 Examination fees"] = m["E21"]
    m["E22 Administrative supplies - non educational"] = m["E22"]
    m["E23 Other insurance premiums"] = m["E23"]
    m["E24 Special facilities"] = m["E24"]
    m["E25 Catering supplies"] = m["E25"]
    m["E26 Agency supply teaching staff"] = m["E26"]
    m["E27 Educational Consultancy"] = m["E27"]
    m["E28a Bought in professional services, other, not PFI"] = m["E28a"]
    m["E28b Bought in professional services, other, PFI"] = m["E28b"]
    m["E28a/b Bought in professional services"] = m["E28a"] + m["E28b"]
    m["E29 Interest charges for Loan and Bank"] = m["E29"]
    m["E30 Direct revenue financing (Revenue contributions to capital)"] = m["E30"]
    m["E31 Community focused school staff"] = m["E31"]
    m["E32 Community focused school costs"] = m["E32"]

    m["OB01 Opening pupil-focused revenue balance"] = m["OB01"]
    m["OB02 Opening community-focused revenue balance"] = m["OB02"]
    m["OB03 Opening capital balance"] = m["OB03"]
    m["CI01 Capital income"] = m["CI01"]
    m["CI03 Voluntary or private income"] = m["CI03"]
    m["CI04 Direct revenue financing"] = m["CI04"]
    m["CE01 Acquisition of land and existing buildings"] = m["CE01"]
    m["CE02 New construction, conversion and renovation"] = m["CE02"]
    m["CE03 Vehicles, plant, equipment and machinery"] = m["CE03"]
    m["CE04 Information and communication technology"] = m[
        ["CE04A", "CE04B", "CE04C", "CE04D", "CE04E"]].sum(axis=1)
    m["CE04A Connectivity"] = m["CE04A"]
    m["CE04B Onsite servers"] = m["CE04B"]
    m["CE04C Administration software and systems"] = m["CE04C"]
    m["CE04D Laptops desktops and tablets"] = m["CE04D"]
    m["CE04E Other hardware"] = m["CE04E"]
    m["B01 Committed revenue balances"] = m["B01"]
    m["B02 Uncommitted revenue balances"] = m["B02"]
    m["B03 Devolved formula capital balance"] = m["B03"]
    m["B05 Other capital balances"] = m["B05"]
    m["B06 Community-focused school revenue balances"] = m["B06"]
    m["B07 Outstanding balance on capital loans to school"] = m["B07"]
    m["Revenue Reserve: B01 + B02 + B06"] = m["B01"] + m["B02"] + m["B06"]

    # Aggregated Financial Totals
    income_core = (
        m["I01"] + m["I02"] + m["I03"] + m["I04"] + m["I05"] + m["I06"]
        + m["I07"] + m["I08a"] + m["I08b"]
    )
    income_full = (
        income_core + m["I09"] + m["I10"] + m["I11"] + m["I12"] + m["I13"]
        + m["I15"] + m["I16"] + m["I17"] + m["I18c"] + m["I18d"]
    )
    expense_total = m[EXPENSE_RAW].sum(axis=1)
    expense_net = expense_total - (m["I09"] + m["I10"] + m["I16"] + m["I17"])

    m["Total Income: I01 to I08, I11 to I15, I18 minus E30"] = (
        income_core + m["I11"] + m["I12"] + m["I13"] + m["I15"] + m["I18c"]
        + m["I18d"] - m["E30"]
    )
    m["Total Income: I01 to I18 minus E30"] = income_full - m["E30"]
    m["Total Expenditure:E01 to E29 and E31 to E32 minus I9, I10, I16 and I17"] = (
        expense_net
    )
    m["Total Expenditure:E01 to E29 and E31 to E32"] = expense_total
    m["In-year Balance"] = (income_full - m["E30"]) - expense_total
    m["Grant Funding: (I01:I07) + I15 + I16 + I18a/b/c/d"] = (
        m["I01"] + m["I02"] + m["I03"] + m["I04"] + m["I05"] + m["I06"] + m["I07"]
        + m["I15"] + m["I16"] + m["I18c"] + m["I18d"]
    )
    m["Direct Grants: I01:I02 + I06:I07"] = m["I01"] + m["I02"] + m["I06"] + m["I07"]
    m["Community Grants: I16+I18"] = m["I16"] + m["I18c"] + m["I18d"]
    m["Targetted Grants: I03:I05 + I15"] = m["I03"] + m["I04"] + m["I05"] + m["I15"]
    m["Self Generated Funding: (I08a/b:I13) + I17"] = (
        m["I08a"] + m["I08b"] + m["I09"] + m["I10"] + m["I11"] + m["I12"] + m["I13"]
        + m["I17"]
    )
    m["Total income"] = m["Total Income: I01 to I18 minus E30"]
    m["Teaching Staff: E01"] = m["E01"]
    m["Supply Staff: E02 + E10 + E26"] = m["E02"] + m["E10"] + m["E26"]
    m["Education support staff: E03"] = m["E03"]
    m["Other Staff Costs: (E07:E09) + E11"] = (
        m["E07"] + m["E08"] + m["E09"] + m["E11"]
    )
    m["Staff Total: (E01:E03) + E05 + (E07: E11) + E26"] = (
        m["E01"] + m["E02"] + m["E03"] + m["E05"] + m["E07"] + m["E08"] + m["E09"]
        + m["E10"] + m["E11"] + m["E26"]
    )
    m["Maintenance & Improvement: E12 + E13"] = m["E12"] + m["E13"]
    m["Premises: (E12:E14) + E04 + E28b"] = (
        m["E12"] + m["E13"] + m["E14"] + m["E04"] + m["E28b"]
    )
    m["Catering Expenses: E06 + E25"] = m["E06"] + m["E25"]
    m["Occupation: E06 + (E15:E18) + E23 + E25"] = (
        m["E06"] + m["E15"] + m["E16"] + m["E17"] + m["E18"] + m["E23"] + m["E25"]
    )
    m["Supplies and Services: (E19:E22) + (E27:E28b)"] = (
        m["E19"] + m["E20A"] + m["E20B"] + m["E20C"] + m["E20D"] + m["E20E"] + m["E20F"]
        + m["E20G"] + m["E21"] + m["E22"] + m["E27"] + m["E28a"] + m["E28b"]
    )
    m["Educational Supplies: (E19:E21)"] = (
        m["E19"] + m["E20A"] + m["E20B"] + m["E20C"] + m["E20D"] + m["E20E"] + m["E20F"]
        + m["E20G"] + m["E21"]
    )
    m["Brought in Professional Sevices: (E27 + E28a)"] = m["E27"] + m["E28a"]
    m["Community Exp: E31 + E32"] = m["E31"] + m["E32"]
    m["Total Expenditure excluding E30"] = expense_total

    return m
