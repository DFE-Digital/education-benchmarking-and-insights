import numpy as np
import pandas as pd

# --- constants (2024-25 reporting year) ---
REF_START = pd.Timestamp("2024-04-01")
REF_PREV_END = pd.Timestamp("2024-03-31")
REF_CLOSE_ANCHOR = pd.Timestamp("2021-03-31")
REF_YEAR_END = pd.Timestamp("2025-03-31")
REF_OPEN_END = pd.Timestamp("2025-04-01")
EXCLUDED_LAEESTAB = 8153238

# CFR income/expenditure columns used in sums (fill NaN with 0 for SQL-like arithmetic)
I_COLS = [f"I{i:02d}" if i < 10 else f"I{i}" for i in range(1, 19)]  # simplified below
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


def months_diff(end: pd.Series, start: pd.Timestamp | pd.Series) -> pd.Series:
    """Approximate SQL DATEDIFF(month, start, end)."""
    end = pd.to_datetime(end)
    if isinstance(start, pd.Series):
        start = pd.to_datetime(start)
    else:
        start = pd.Timestamp(start)
    return (end.dt.year - start.year) * 12 + (end.dt.month - start.month)


def _sum_cols(df: pd.DataFrame, cols: list[str]) -> pd.Series:
    present = [c for c in cols if c in df.columns]
    return df[present].fillna(0).sum(axis=1)


def calc_period_covered(gias: pd.DataFrame) -> pd.Series:
    close_m = months_diff(gias["CloseDate"], REF_CLOSE_ANCHOR)
    close_m2 = months_diff(gias["CloseDate"], REF_PREV_END)
    open_m = months_diff(REF_START, gias["OpenDate"])

    return np.select(
        [
            (close_m > 0) & (close_m2 < 12),
            (open_m > 0) & (open_m < 12),
        ],
        [
            months_diff(REF_YEAR_END, gias["CloseDate"]),
            months_diff(gias["OpenDate"], REF_OPEN_END),
        ],
        default=12,
    )


def map_overall_phase(gias: pd.DataFrame) -> pd.Series:
    phase = gias["PhaseOfEducation (name)"].fillna("")
    typ = gias["Type"].fillna("")
    return np.select(
        [
            (phase == "Not applicable") & (typ == "Pupil referral unit"),
            (phase == "Not applicable") & typ.isin(["Community special school", "Foundation special school"]),
            phase == "All-through",
            phase.isin(["Middle deemed primary", "Primary"]),
            phase.isin(["Middle deemed secondary", "16 plus", "Secondary"]),
            phase == "Nursery",
        ],
        ["Pupil referral unit", "Special", "All-through", "Primary", "Secondary", "Nursery"],
        default=None,
    )


def map_sfb_phase(gias: pd.DataFrame, overall_phase: pd.Series) -> pd.Series:
    phase = gias["PhaseOfEducation (name)"].fillna("")
    typ = gias["Type"].fillna("")
    high = gias["StatutoryHighAge"]
    low = gias["StatutoryLowAge"]

    base = np.select(
        [
            (phase == "Not applicable") & (typ == "Pupil referral unit"),
            (phase == "Not applicable") & typ.isin(["Community special school", "Foundation special school"]),
            phase == "All-through",
            phase.isin(["Middle deemed secondary", "16 plus", "Secondary"]),
        ],
        ["Pupil referral unit", "Special", "All-through", "Secondary"],
        default=phase,
    )
    primary_sub = np.select(
        [high == 7, (high == 11) & (low >= 7)],
        ["Infant", "Junior"],
        default="Infant and junior",
    )
    return np.where(
        phase.isin(["Primary", "Middle deemed primary"]),
        primary_sub,
        base,
    )


URBAN_RURAL_MAP = {
    "(England/Wales) Rural hamlet and isolated dwellings": "Rural hamlet and isolated dwellings",
    "(England/Wales) Rural hamlet and isolated dwellings in a sparse setting": "Rural hamlet and isolated dwellings in a sparse setting",
    "(England/Wales) Rural town and fringe": "Rural town and fringe",
    "(England/Wales) Rural town and fringe in a sparse setting": "Rural town and fringe in a sparse setting",
    "(England/Wales) Rural village": "Rural village",
    "(England/Wales) Rural village in a sparse setting": "Rural village in a sparse setting",
    "(England/Wales) Urban city and town": "Urban city and town",
    "(England/Wales) Urban city and town in a sparse setting": "Urban city and town in a sparse setting",
    "(England/Wales) Urban major conurbation": "Urban major conurbation",
    "(England/Wales) Urban minor conurbation": "Urban minor conurbation",
}


def map_region(la_region: pd.Series) -> pd.Series:
    return np.where(la_region.isin(["Inner London", "Outer London"]), "London", la_region)


def map_london_bucket(la_region: pd.Series) -> pd.Series:
    return np.select(
        [la_region == "Inner London", la_region == "Outer London"],
        ["Inner", "Outer"],
        default="Neither",
    )


def build_tempfed(
    fed: pd.DataFrame,
    gias: pd.DataFrame,
    hospital_schools: pd.DataFrame,
    lookup_la: pd.DataFrame,
) -> pd.DataFrame:
    fed = fed.rename(columns={"LEAEstab": "LAEstab"})
    dns = fed[(fed["Did_Not_Submit"] == "DNS") & (fed["LAEstab"] != EXCLUDED_LAEESTAB)].copy()

    g = gias.rename(columns={
        "EstablishmentName": "School_Name",
        "FederationFlag (name)": "GIAS_Federation",
        "Federations (name)": "GIAS_Federation Name",
        "Gender (name)": "Gender",
        "Type_group": "TypeGroup",
        "AdmissionsPolicy (name)": "Admissions_Policy",
        "OfficialSixthForm (name)": "Has a 6th Form",
        "StatutoryLowAge": "Lowest age of pupils",
        "StatutoryHighAge": "Highest age of pupils",
        "EstablishmentStatus (name)": "EstablishmentStatus (name)",  # kept for union
    })

    df = (
        dns
        .merge(g, on="LAEstab", how="left")
        .merge(
            hospital_schools.rename(columns={"GHSIndicator": "Hospital School"}),
            on="LAEstab",
            how="left",
        )
        .merge(lookup_la, left_on="LA", right_on="old_la_code", how="left")
    )

    df["Period Covered"] = calc_period_covered(df)
    df["DNS"] = "DNS"
    df["NewFedFlag"] = "Y"
    df["Estab"] = df["EstablishmentNumber"]
    df["Overall Phase"] = map_overall_phase(df)
    df["SFB Phase"] = map_sfb_phase(df, df["Overall Phase"])
    df["UrbanRural"] = df["UrbanRural (name)"].map(URBAN_RURAL_MAP)
    df["Region"] = map_region(df["region_name"])
    df["London Borough"] = map_london_bucket(df["region_name"])
    df["London Weighting"] = df["London Borough"]
    df["PFI Funding"] = "DNS"
    df["Admissions_Policy"] = df["Admissions_Policy"]
    df["Ind. Pupils FTE"] = df["FTE"]
    df["Aggregated Pupils FTE"] = df["FTE"]
    df["PRU pupil nums"] = df["PRU_Headcount"]
    df["Ind. Pupils Headcount"] = df["Headcount"]

    def pct(num, den):
        return (num / den.replace(0, np.nan) * 100).round(2)

    df["% of pupils with EHCP_agg"] = pct(df["EHC_plan"], df["SEN_pupilTotal"])
    df["% of pupils with EHCP_ind"] = df["% of pupils with EHCP_agg"]
    df["% of pupils with SEN Support_agg"] = pct(df["SEN_Support"], df["SEN_pupilTotal"])
    df["% of pupils with SEN Support_ind"] = df["% of pupils with SEN Support_agg"]
    df["% of pupils eligible for FSM_agg"] = df["PC_FSMEligible"]
    df["% of pupils eligible for FSM_ind"] = df["PC_FSMEligible"]
    df["% of pupils with EAL_agg"] = df["PC_EAL"]
    df["% of pupils with EAL_ind"] = df["PC_EAL"]
    df["% of pupils who are Boarders_agg"] = pct(df["Boarders"], df["FTE"])
    df["% of pupils who are Boarders_ind"] = df["% of pupils who are Boarders_agg"]
    df["No of pupils in 6th form_agg"] = df["VIthForm"]
    df["No of pupils in 6th form_ind"] = df["VIthForm"]
    df["Teachers FTE_agg"] = df["AllTeachers_FTE"]
    df["Teachers FTE_ind"] = df["AllTeachers_FTE"]
    df["FTE of Teaching Assistants_agg"] = df["TAs_FTE"]
    df["FTE of Teaching Assistants_ind"] = df["TAs_FTE"]

    return df


ALLSCHOOLS_COLS = [
    "LAEstab", "LA", "Estab", "URN", "School_Name", "Period Covered", "DNS",
    "NewFedFlag", "Lead_school", "GIAS_Federation", "GIAS_Federation_Name",
    "Gender", "Overall Phase", "SFB Phase", "Type", "TypeGroup", "UrbanRural",
    "Region", "London Borough", "London Weighting", "Easting", "Northing",
    "Admissions_Policy", "Has a 6th Form", "Lowest age of pupils", "Highest age of pupils",
    "PFI Funding", "Open_Closed", "OpenDate", "CloseDate", "Hospital School",
    "Ind. Pupils FTE", "Aggregated Pupils FTE", "PRU pupil nums", "Ind. Pupils Headcount",
    "% of pupils with EHCP_agg", "% of pupils with EHCP_ind",
    "% of pupils with SEN Support_agg", "% of pupils with SEN Support_ind",
    "% of pupils eligible for FSM_agg", "% of pupils eligible for FSM_ind",
    "% of pupils with EAL_agg", "% of pupils with EAL_ind",
    "% of pupils who are Boarders_agg", "% of pupils who are Boarders_ind",
    "No of pupils in 6th form_agg", "No of pupils in 6th form_ind",
    "Teachers FTE_agg", "Teachers FTE_ind", "% of teachers with QTS",
    "FTE of Teaching Assistants_agg", "FTE of Teaching Assistants_ind",
]


def build_allschools(sfb_context: pd.DataFrame, tempfed: pd.DataFrame) -> pd.DataFrame:
    context = sfb_context.copy()
    context["Lead_school"] = 0

    fed_part = tempfed.rename(columns={
        "EstablishmentNumber": "Estab",
        "GIAS_Federation Name": "GIAS_Federation_Name",
        "EstablishmentStatus (name)": "Open_Closed",
    })
    fed_part["Lead_school"] = fed_part["Lead_school"]  # from federation table

    return pd.concat(
        [context[ALLSCHOOLS_COLS], fed_part[ALLSCHOOLS_COLS]],
        ignore_index=True,
    )


def add_financials(merged: pd.DataFrame) -> pd.DataFrame:
    """#mergedworking calculated columns (lines 332-450)."""
    m = merged.copy()
    for c in INCOME_RAW + EXPENSE_RAW + CAPITAL_RAW:
        if c in m.columns:
            m[c] = pd.to_numeric(m[c], errors="coerce")

    i = m[INCOME_RAW].fillna(0)
    e = m[EXPENSE_RAW].fillna(0)

    m["I01 Pre-16 Funding"] = m["I01"]
    m["I02 Post-16 Funding"] = m["I02"]
    m["I01/2 Pre and Post-16 Funding"] = i["I01"] + i["I02"]
    # ... map remaining I/E renames as in SQL (I03..E32, OB..B07) ...
    m["I08a/b Income from facilities and services"] = i["I08a"] + i["I08b"]
    m["I18 Total additional grant for schools"] = i["I18c"] + i["I18d"]
    m["E28a/b Bought in professional services"] = e["E28a"] + e["E28b"]
    m["CE04 Information and communication technology"] = (
        m[["CE04A", "CE04B", "CE04C", "CE04D", "CE04E"]].fillna(0).sum(axis=1)
    )
    m["Revenue Reserve: B01 + B02 + B06"] = m["B01"].fillna(0) + m["B02"].fillna(0) + m["B06"].fillna(0)

    income_core = i["I01"] + i["I02"] + i["I03"] + i["I04"] + i["I05"] + i["I06"] + i["I07"] + i["I08a"] + i["I08b"]
    income_full = income_core + i["I09"] + i["I10"] + i["I11"] + i["I12"] + i["I13"] + i["I15"] + i["I16"] + i["I17"] + i["I18c"] + i["I18d"]
    expense_total = _sum_cols(m, EXPENSE_RAW)
    expense_net = expense_total - (i["I09"] + i["I10"] + i["I16"] + i["I17"])

    m["Total Income: I01 to I08, I11 to I15, I18 minus E30"] = (
        income_core + i["I11"] + i["I12"] + i["I13"] + i["I15"] + i["I18c"] + i["I18d"] - m["E30"].fillna(0)
    )
    m["Total Income: I01 to I18 minus E30"] = income_full - m["E30"].fillna(0)
    m["Total Expenditure:E01 to E29 and E31 to E32 minus I9, I10, I16 and I17"] = expense_net
    m["Total Expenditure:E01 to E29 and E31 to E32"] = expense_total
    m["In-year Balance"] = (income_full - m["E30"].fillna(0)) - expense_total
    m["Grant Funding: (I01:I07) + I15 + I16 + I18a/b/c/d"] = (
        i["I01"] + i["I02"] + i["I03"] + i["I04"] + i["I05"] + i["I06"] + i["I07"]
        + i["I15"] + i["I16"] + i["I18c"] + i["I18d"]
    )
    m["Direct Grants: I01:I02 + I06:I07"] = i["I01"] + i["I02"] + i["I06"] + i["I07"]
    m["Community Grants: I16+I18"] = i["I16"] + i["I18c"] + i["I18d"]
    m["Targetted Grants: I03:I05 + I15"] = i["I03"] + i["I04"] + i["I05"] + i["I15"]
    m["Self Generated Funding: (I08a/b:I13) + I17"] = (
        i["I08a"] + i["I08b"] + i["I09"] + i["I10"] + i["I11"] + i["I12"] + i["I13"] + i["I17"]
    )
    m["Total income"] = m["Total Income: I01 to I18 minus E30"]
    m["Teaching Staff: E01"] = e["E01"]
    m["Supply Staff: E02 + E10 + E26"] = e["E02"] + e["E10"] + e["E26"]
    m["Education support staff: E03"] = e["E03"]
    m["Other Staff Costs: (E07:E9) + E11"] = e["E07"] + e["E08"] + e["E09"] + e["E11"]
    m["Staff Total: (E01:E03) + E05 + (E07: E11) + E26"] = (
        e["E01"] + e["E02"] + e["E03"] + e["E05"] + e["E07"] + e["E08"] + e["E09"] + e["E10"] + e["E11"] + e["E26"]
    )
    m["Maintenance & Improvement: E12 + E13"] = e["E12"] + e["E13"]
    m["Premises: (E12:E14) + E04 + E28b"] = e["E12"] + e["E13"] + e["E14"] + e["E04"] + e["E28b"]
    m["Catering Expenses: E06 + E25"] = e["E06"] + e["E25"]
    m["Occupation: E06 + (E15:E18) + E23 + E25"] = (
        e["E06"] + e["E15"] + e["E16"] + e["E17"] + e["E18"] + e["E23"] + e["E25"]
    )
    m["Supplies and Services: (E19:E22) + (E27:E28b)"] = (
        e["E19"] + e["E20A"] + e["E20B"] + e["E20C"] + e["E20D"] + e["E20E"] + e["E20F"] + e["E20G"]
        + e["E21"] + e["E22"] + e["E27"] + e["E28a"] + e["E28b"]
    )
    m["Educational Supplies: (E19:E21)"] = (
        e["E19"] + e["E20A"] + e["E20B"] + e["E20C"] + e["E20D"] + e["E20E"] + e["E20F"] + e["E20G"] + e["E21"]
    )
    m["Brought in Professional Sevices: (E27 + E28a)"] = e["E27"] + e["E28a"]
    m["Community Exp: E31 + E32"] = e["E31"] + e["E32"]
    m["Total Expenditure excluding E30"] = expense_total
    return m


def build_sfb_maintained(mergedworking: pd.DataFrame, lookup_la: pd.DataFrame) -> pd.DataFrame:
    """Final SELECT INTO SFB_Maintained_2024-25 (lines 469-641)."""
    a = mergedworking.merge(lookup_la, left_on="LA", right_on="old_la_code", how="left")

    out = pd.DataFrame()
    out["LAEstab"] = a["LAEstab"]
    out["LA"] = a["LA"]
    out["LA Name"] = a["LA Name"]
    out["Estab"] = a["Estab"]
    out["URN"] = a["URN"]
    out["School Name"] = a["School_Name"]
    out["Period covered by return"] = a["Period Covered"]
    out["Did Not Supply flag"] = a["DNS"].map({"n/a": "0", "LeadSchool": "0", "DNS": "DNS"})
    out["FederatedSubmission"] = a["DNS"].map({
        "n/a": "No", "LeadSchool": "Lead school", "DNS": "Non returning school"
    })
    out["Lead school in federation"] = np.select(
        [a["DNS"] == "LeadSchool", a["DNS"] == "DNS"],
        [a["LAEstab"], a["Lead_school"]],
        default=0,
    )
    out["General Hospital School Indicator"] = np.where(a["Hospital School"] == "GHS", "Y", "N")
    out["IndividualPupilsFTE"] = a["Ind. Pupils FTE"]
    out["AggregatedPupilsFTE"] = a["Aggregated Pupils FTE"]
    # ... copy all financial + dimension columns from `a` using SQL aliases ...
    return out


def run_step3(gias_subset, fed, hospital_schools, lookup_la, sfb_context, cfr) -> pd.DataFrame:
    tempfed = build_tempfed(fed, gias_subset, hospital_schools, lookup_la)
    allschools = build_allschools(sfb_context, tempfed)
    merged = allschools.merge(
        cfr.rename(columns={"LEAEstab": "LAEstab"}),
        on="LAEstab",
        how="left",
    )
    mergedworking = add_financials(merged)
    return build_sfb_maintained(mergedworking, lookup_la)