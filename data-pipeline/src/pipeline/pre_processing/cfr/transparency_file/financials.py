import numpy as np
import pandas as pd

# CFR income/expenditure columns used in sums (fill NaN with 0 for SQL-like arithmetic)
INCOME_RAW = [
    "I01", "I02", "I03", "I04", "I05", "I06", "I07", "I08a", "I08b",
    "I09", "I10", "I11", "I12", "I13", "I15", "I16", "I17", "I18c", "I18d",
]
EXPENSE_RAW = [
    "E01", "E02", "E03", "E04", "E05", "E06", "E07", "E08", "E09", "E10", "E11",
    "E12", "E13", "E14", "E15", "E16", "E17", "E18", "E19",
    "E20A", "E20B", "E20C", "E20D", "E20E", "E20F", "E20G",
    "E21", "E22", "E23", "E24", "E25", "E26", "E27", "E28a", "E28b", "E29",
    "E31", "E32",
]
CAPITAL_RAW = ["OB01", "OB02", "OB03", "CI01", "CI03", "CI04",
               "CE01", "CE02", "CE03", "CE04A", "CE04B", "CE04C", "CE04D", "CE04E",
               "B01", "B02", "B03", "B05", "B06", "B07"]


def _sum_cols(df: pd.DataFrame, cols: list[str]) -> pd.Series:
    present = [c for c in cols if c in df.columns]
    return df[present].fillna(0).sum(axis=1)


def add_financials(merged: pd.DataFrame) -> pd.DataFrame:
    m = merged.copy()
    
    # Pre-cast all financial columns to numeric
    for c in INCOME_RAW + EXPENSE_RAW + CAPITAL_RAW + ["E30"]:
        if c in m.columns:
            m[c] = pd.to_numeric(m[c], errors="coerce").fillna(0)

    def get_val(col):
        return m[col] if col in m.columns else 0

    m["I01 Pre-16 Funding"] = get_val("I01")
    m["I02 Post-16 Funding"] = get_val("I02")
    m["I01/2 Pre and Post-16 Funding"] = get_val("I01") + get_val("I02")
    m["I03 SEN"] = get_val("I03")
    m["I04 Funding for minority ethnic pupils"] = get_val("I04")
    m["I05 Pupil Premium"] = get_val("I05")
    m["I06 Other DfE/EFA Revenue Grants"] = get_val("I06")
    m["I07 Other income (LA & other Government grants)"] = get_val("I07")
    m["I08a Income from lettings"] = get_val("I08a")
    m["I08b Income from facilities and services"] = get_val("I08b")
    m["I08a/b Income from facilities and services"] = get_val("I08a") + get_val("I08b")
    m["I09 Income from catering"] = get_val("I09")
    m["I10 Receipts from supply teacher insurance claims"] = get_val("I10")
    m["I11 Receipts from other insurance claims"] = get_val("I11")
    m["I12 Income from contributions to visits etc"] = get_val("I12")
    m["I13 Donations and/or voluntary funds"] = get_val("I13")
    m["I15 Pupil focussed extended school funding and/or grants"] = get_val("I15")
    m["I16 Community focussed school funding and/or grants"] = get_val("I16")
    m["I17 Community focused school facilities income"] = get_val("I17")
    m["I18c Income from the £1bn COVID-19 catch-up package announced on 20 July 2020"] = get_val("I18c")
    m["I18d Income from other additional grants"] = get_val("I18d")
    m["I18 Total additional grant for schools"] = get_val("I18c") + get_val("I18d")

    m["E01 Teaching staff"] = get_val("E01")
    m["E02 Supply teaching staff"] = get_val("E02")
    m["E03 Education support staff"] = get_val("E03")
    m["E04 Premises staff"] = get_val("E04")
    m["E05 Administrative and clerical staff"] = get_val("E05")
    m["E06 Catering staff"] = get_val("E06")
    m["E07 Other staff"] = get_val("E07")
    m["E08 Indirect employee expenses"] = get_val("E08")
    m["E09 Staff development and training"] = get_val("E09")
    m["E10 Supply teacher insurance"] = get_val("E10")
    m["E11 Staff-related insurance"] = get_val("E11")
    m["E12 Building maintenance and improvement"] = get_val("E12")
    m["E13 Grounds maintenance and improvement"] = get_val("E13")
    m["E14 Cleaning and caretaking"] = get_val("E14")
    m["E15 Water and sewerage"] = get_val("E15")
    m["E16 Energy"] = get_val("E16")
    m["E17 Rates"] = get_val("E17")
    m["E18 Other occupation costs"] = get_val("E18")
    m["E19 Learning resources (not ICT equipment)"] = get_val("E19")
    m["E20A Connectivity"] = get_val("E20A")
    m["E20B Onsite servers"] = get_val("E20B")
    m["E20C IT learning resources"] = get_val("E20C")
    m["E20D Administration software and systems"] = get_val("E20D")
    m["E20E Laptops, desktops and tablets"] = get_val("E20E")
    m["E20F Other hardware"] = get_val("E20F")
    m["E20G IT support"] = get_val("E20G")
    m["E21 Examination fees"] = get_val("E21")
    m["E22 Administrative supplies - non educational"] = get_val("E22")
    m["E23 Other insurance premiums"] = get_val("E23")
    m["E24 Special facilities"] = get_val("E24")
    m["E25 Catering supplies"] = get_val("E25")
    m["E26 Agency supply teaching staff"] = get_val("E26")
    m["E27 Educational Consultancy"] = get_val("E27")
    m["E28a Bought in professional services, other, not PFI"] = get_val("E28a")
    m["E28b Bought in professional services, other, PFI"] = get_val("E28b")
    m["E28a/b Bought in professional services"] = get_val("E28a") + get_val("E28b")
    m["E29 Interest charges for Loan and Bank"] = get_val("E29")
    m["E30 Direct revenue financing (Revenue contributions to capital)"] = get_val("E30")
    m["E31 Community focused school staff"] = get_val("E31")
    m["E32 Community focused school costs"] = get_val("E32")

    m["OB01 Opening pupil-focused revenue balance"] = get_val("OB01")
    m["OB02 Opening community-focused revenue balance"] = get_val("OB02")
    m["OB03 Opening capital balance"] = get_val("OB03")
    m["CI01 Capital income"] = get_val("CI01")
    m["CI03 Voluntary or private income"] = get_val("CI03")
    m["CI04 Direct revenue financing"] = get_val("CI04")
    m["CE01 Acquisition of land and existing buildings"] = get_val("CE01")
    m["CE02 New construction, conversion and renovation"] = get_val("CE02")
    m["CE03 Vehicles, plant, equipment and machinery"] = get_val("CE03")
    m["CE04 Information and communication technology"] = (
        m[["CE04A", "CE04B", "CE04C", "CE04D", "CE04E"]].fillna(0).sum(axis=1) if all(c in m.columns for c in ["CE04A", "CE04B", "CE04C", "CE04D", "CE04E"]) else 0
    )
    m["CE04A Connectivity"] = get_val("CE04A")
    m["CE04B Onsite servers"] = get_val("CE04B")
    m["CE04C Administration software and systems"] = get_val("CE04C")
    m["CE04D Laptops desktops and tablets"] = get_val("CE04D")
    m["CE04E Other hardware"] = get_val("CE04E")
    m["B01 Committed revenue balances"] = get_val("B01")
    m["B02 Uncommitted revenue balances"] = get_val("B02")
    m["B03 Devolved formula capital balance"] = get_val("B03")
    m["B05 Other capital balances"] = get_val("B05")
    m["B06 Community-focused school revenue balances"] = get_val("B06")
    m["B07 Outstanding balance on capital loans to school"] = get_val("B07")
    m["Revenue Reserve: B01 + B02 + B06"] = get_val("B01") + get_val("B02") + get_val("B06")

    # Aggregated Financial Totals
    income_core = get_val("I01") + get_val("I02") + get_val("I03") + get_val("I04") + get_val("I05") + get_val("I06") + get_val("I07") + get_val("I08a") + get_val("I08b")
    income_full = income_core + get_val("I09") + get_val("I10") + get_val("I11") + get_val("I12") + get_val("I13") + get_val("I15") + get_val("I16") + get_val("I17") + get_val("I18c") + get_val("I18d")
    expense_total = _sum_cols(m, EXPENSE_RAW)
    expense_net = expense_total - (get_val("I09") + get_val("I10") + get_val("I16") + get_val("I17"))

    m["Total Income: I01 to I08, I11 to I15, I18 minus E30"] = (
        income_core + get_val("I11") + get_val("I12") + get_val("I13") + get_val("I15") + get_val("I18c") + get_val("I18d") - get_val("E30")
    )
    m["Total Income: I01 to I18 minus E30"] = income_full - get_val("E30")
    m["Total Expenditure:E01 to E29 and E31 to E32 minus I9, I10, I16 and I17"] = expense_net
    m["Total Expenditure:E01 to E29 and E31 to E32"] = expense_total
    m["In-year Balance"] = (income_full - get_val("E30")) - expense_total
    m["Grant Funding: (I01:I07) + I15 + I16 + I18a/b/c/d"] = (
        get_val("I01") + get_val("I02") + get_val("I03") + get_val("I04") + get_val("I05") + get_val("I06") + get_val("I07")
        + get_val("I15") + get_val("I16") + get_val("I18c") + get_val("I18d")
    )
    m["Direct Grants: I01:I02 + I06:I07"] = get_val("I01") + get_val("I02") + get_val("I06") + get_val("I07")
    m["Community Grants: I16+I18"] = get_val("I16") + get_val("I18c") + get_val("I18d")
    m["Targetted Grants: I03:I05 + I15"] = get_val("I03") + get_val("I04") + get_val("I05") + get_val("I15")
    m["Self Generated Funding: (I08a/b:I13) + I17"] = (
        get_val("I08a") + get_val("I08b") + get_val("I09") + get_val("I10") + get_val("I11") + get_val("I12") + get_val("I13") + get_val("I17")
    )
    m["Total income"] = m["Total Income: I01 to I18 minus E30"]
    m["Teaching Staff: E01"] = get_val("E01")
    m["Supply Staff: E02 + E10 + E26"] = get_val("E02") + get_val("E10") + get_val("E26")
    m["Education support staff: E03"] = get_val("E03")
    m["Other Staff Costs: (E07:E09) + E11"] = get_val("E07") + get_val("E08") + get_val("E09") + get_val("E11")
    m["Staff Total: (E01:E03) + E05 + (E07: E11) + E26"] = (
        get_val("E01") + get_val("E02") + get_val("E03") + get_val("E05") + get_val("E07") + get_val("E08") + get_val("E09") + get_val("E10") + get_val("E11") + get_val("E26")
    )
    m["Maintenance & Improvement: E12 + E13"] = get_val("E12") + get_val("E13")
    m["Premises: (E12:E14) + E04 + E28b"] = get_val("E12") + get_val("E13") + get_val("E14") + get_val("E04") + get_val("E28b")
    m["Catering Expenses: E06 + E25"] = get_val("E06") + get_val("E25")
    m["Occupation: E06 + (E15:E18) + E23 + E25"] = (
        get_val("E06") + get_val("E15") + get_val("E16") + get_val("E17") + get_val("E18") + get_val("E23") + get_val("E25")
    )
    m["Supplies and Services: (E19:E22) + (E27:E28b)"] = (
        get_val("E19") + get_val("E20A") + get_val("E20B") + get_val("E20C") + get_val("E20D") + get_val("E20E") + get_val("E20F") + get_val("E20G")
        + get_val("E21") + get_val("E22") + get_val("E27") + get_val("E28a") + get_val("E28b")
    )
    m["Educational Supplies: (E19:E21)"] = (
        get_val("E19") + get_val("E20A") + get_val("E20B") + get_val("E20C") + get_val("E20D") + get_val("E20E") + get_val("E20F") + get_val("E20G") + get_val("E21")
    )
    m["Brought in Professional Sevices: (E27 + E28a)"] = get_val("E27") + get_val("E28a")
    m["Community Exp: E31 + E32"] = get_val("E31") + get_val("E32")
    m["Total Expenditure excluding E30"] = expense_total
    
    return m
