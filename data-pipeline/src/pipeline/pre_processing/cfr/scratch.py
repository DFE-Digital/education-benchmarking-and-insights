import pandas as pd
import numpy as np

def recreate_ms_file(cfr_raw_blob, gias, pru, hospital_schools, sen, census,
        census_last_year, sen_last_year, pru_last_year, hospital_schools_last_year):
    """Recreate the logic to handle federations from the raw CFR data"""
    cfr_raw = pd.read_csv(cfr_raw_blob)
    REFERENCE_DATE = pd.Timestamp("2024-04-01")
    is_open_status = ['Open', 'Open, but proposed to close', 'Proposed to open']
    is_special_type = ["Local authority maintained schools", "Community special school", "Foundation special school"]

    closed_schools = gias[
        (gias["EstablishmentStatus (name)"] == "Closed") & 
        (pd.to_datetime(gias["CloseDate"]) >= REFERENCE_DATE)
    ]
    open_schools = gias[
        gias["EstablishmentStatus (name)"].isin(is_open_status) & 
        gias["EstablishmentTypeGroup (name)"].isin(is_special_type)  # | gias["Type"].isin(is_special_type))
    ]
    gias_subset = pd.concat([closed_schools, open_schools])
    gias_subset = gias_subset.dropna(subset=["LA (code)", "EstablishmentNumber"])
    gias_subset["LAEstab"] = (gias_subset["LA (code)"].astype(str) + gias_subset["EstablishmentNumber"].astype(str)).astype(int)

    cfr_cols = [
        "LANumber",
        "Estab",
        "LEAEstab",
        "Federated Flag",
        *[f"LAEstab of School in Federation {i}" for i in range(1, 11)],
    ]
    cfr = (
        cfr_raw[cfr_cols]
        .drop_duplicates()
        .rename(columns={
            "LEAEstab": "LAEstab",
            "LANumber": "LA"
        })
    )

    fed_lead = pd.DataFrame({
        "LA": cfr["LA"],
        "LAEstab": cfr["LAEstab"],
        "Did_Not_Submit": np.where(
            cfr["Federated Flag"].eq("No"), "Not federated",
            np.where(cfr["Federated Flag"].eq("Yes"), "Lead", None),
        ),
        "Lead_school": np.where(cfr["Federated Flag"].eq("Yes"), cfr["LAEstab"], 0),
        "Index_number": np.where(
            cfr["Federated Flag"].eq("Yes"),
            "A" + cfr["LA"].astype(str) + cfr["Estab"].astype(str),
            "Z",
        ),
    })

    fed_member_frames = []
    for n in range(1, 11):
        fed_col = f"LAEstab of School in Federation {n}"
        mask = (cfr[fed_col] > 1) & (cfr["LAEstab"] != cfr[fed_col])
        slot = cfr.loc[mask].copy()
        if slot.empty:
            continue
        cond = slot["Federated Flag"].eq("Yes") & (slot[fed_col] > 1) & (slot["LAEstab"] != slot[fed_col])
        fed_member_frames.append(pd.DataFrame({
            "LA": slot["LA"],
            "LAEstab": np.where(cond, slot[fed_col].astype(int), np.nan),
            "Did_Not_Submit": "DNS",
            "Lead_school": np.where(cond, slot["LAEstab"].astype(int), np.nan),
            "Index_number": np.where(
                slot["Federated Flag"].eq("Yes"),
                "A" + slot["LA"].astype(str) + slot["Estab"].astype(str),
                "Z",
            ),
        }))

    all_federations = pd.concat([fed_lead, *fed_member_frames], ignore_index=True)

    fedmatched = (
        all_federations
        .merge(gias_subset.reset_index(), on="LAEstab", how="left")
        .merge(pru, on="LAEstab", how="left")
        .merge(census, on="URN", how="left")
        .merge(sen, on="URN", how="left")
    )

    fedmatched["FTE"] = fedmatched["Number of pupils"]
    if fedmatched["EstablishmentTypeGroup (name)"].eq("Pupil referral unit").any():
        fedmatched["FTE"] = np.where(
            fedmatched["EstablishmentTypeGroup (name)"].eq("Pupil referral unit"),
            fedmatched["PRU_Headcount"],
            fedmatched["Number of pupils"],
        )
    fedmatched["VIthForm"] = 0
    fedmatched["NonClassroomSupport_HC"] = 0
    fedmatched["NonClassroomSupport_FTE"] = 0

    sfb_federations_individual_schools_202425 = fedmatched[
        fedmatched["Did_Not_Submit"].isin(["DNS", "Lead"])
    ]

    agg_cols = [
        "Number of pupils (headcount)", "FTE", "total boarders", "PRU_Headcount", 
        "number of pupils known to be eligible for free school meals", 
        "number of pupils whose first language is known or believed to be other than English",
        "Total pupils", "SEN support", "EHC plan", "VIthForm",
        "Total School Workforce (Headcount)", 
        # "Classroom_Teachers_HC", 
        "Total Number of Teachers in the Leadership Group (Headcount)",
        "Total Number of Teachers (Headcount)", 
        "Total Number of Teaching Assistants (Headcount)", 
        "NonClassroomSupportStaffHeadcount", 
        "Total Number of Auxiliary Staff (Headcount)",
        "Total School Workforce (Full-Time Equivalent)", 
        # "Classroom_Teachers_FTE", 
        "Total Number of Teachers in the Leadership Group (Full-time Equivalent)",
        "Total Number of Teachers (Full-Time Equivalent)", 
        "Total Number of Teaching Assistants (Full-Time Equivalent)", 
        "NonClassroomSupportStaffFTE", 
        "Total Number of Auxiliary Staff (Full-Time Equivalent)",
    ]
    # Exclude schools with a lead 0
    lead_base = fedmatched[fedmatched["Lead_school"] > 99].copy()
    lead_base["NumberEligibleFSM"] = pd.to_numeric(
        lead_base["number of pupils known to be eligible for free school meals"], errors="coerce")
    lead_base["Pupils_EAL"] = pd.to_numeric(
        lead_base["number of pupils whose first language is known or believed to be other than English"], errors="coerce")
    fed_cols_on_cfr = [f"LAEstab of School in Federation {i}" for i in range(1, 11)]
    group_keys = ["LA", "Index_number", "Lead_school", *fed_cols_on_cfr]
    leadschools = (
        lead_base.merge(
            cfr[["LAEstab", *fed_cols_on_cfr]],
            left_on="Lead_school",
            right_on="LAEstab",
            how="left",
            suffixes=("", "_cfr"),
        ).groupby(group_keys, dropna=False, as_index=False)[agg_cols].sum(min_count=1)
    )

    def _pct(numerator, denominator) -> pd.Series:
        return (numerator / denominator.replace(0, np.nan) * 100).round(2)
    
    cfr_federations_aggregated = leadschools.assign(
        PC_Boarders=_pct(leadschools["total boarders"], leadschools["Number of pupils (headcount)"]),
        PC_FSM=_pct(leadschools["number of pupils known to be eligible for free school meals"], leadschools["Number of pupils (headcount)"]),
        PC_EAL=_pct(leadschools["number of pupils whose first language is known or believed to be other than English"], leadschools["Number of pupils (headcount)"]),
        PC_SENSupport=_pct(leadschools["SEN support"], leadschools["Total pupils"]),
        PC_EHCP=_pct(leadschools["EHC plan"], leadschools["Total pupils"]),
    )

#---------------------------
    # 1. Prepare GIAS subset (Assumes GIAS is already filtered/unioned from Step 1)
    REF_START = pd.Timestamp('2024-04-01')
    REF_PREV_END = pd.Timestamp('2024-03-31')

    # 2. #CFRWorkingGIAS Logic
    # Calculate Period Covered (Months)
    def calc_months(row):
        if pd.notnull(row['CloseDate']):
            diff = (row['CloseDate'].year - REF_PREV_END.year) * 12 + row['CloseDate'].month - REF_PREV_END.month
            if 0 < diff < 12: return diff
        if pd.notnull(row['OpenDate']):
            diff = (REF_START.year - row['OpenDate'].year) * 12 + REF_START.month - row['OpenDate'].month
            if 0 < diff < 12: return diff
        return 12

    # Initial Merge
    working = cfr_raw[['LEAEstab', 'LANumber', 'Estab', 'E28b']].rename(
        columns={'LEAEstab': 'LAEstab', 'LANumber': 'LA'})
    
    # working = working.merge(lookup_la, left_on='LA', right_on='old_la_code', how='left')
    working = working.merge(gias_subset, on='LAEstab', how='left')
    working = working.merge(hospital_schools[['LAEstab', 'GHSIndicator', "TotalHeadcount"]], on='LAEstab', how='left')
    working = working.merge(cfr_federations_aggregated['Lead_school'].astype(int),
                            left_on='LAEstab', right_on='Lead_school', how='left', indicator='is_fed')

    working['Period Covered'] = working.apply(calc_months, axis=1)
    working['DNS'] = np.where(working['is_fed'] == 'both', 'LeadSchool', 'n/a')
    working['NewFedFlag'] = np.where(working['is_fed'] == 'both', 'Y', 'N')
    
    # SFB Phase Logic
    special_types = ['Community special school', 'Foundation special school']
    cond_pru = (working['PhaseOfEducation (name)'].fillna('') == 'Not applicable') & \
           (working['EstablishmentTypeGroup (name)'].fillna('') == 'Pupil referral unit')

    cond_special = (working['PhaseOfEducation (name)'].fillna('') == 'Not applicable') & \
                (working['EstablishmentTypeGroup (name)'].fillna('').isin(special_types))

    cond_all_through = (working['PhaseOfEducation (name)'].fillna('') == 'All-through')
    cond_primary = (working['PhaseOfEducation (name)'].fillna('').isin(['Middle deemed primary', 'Primary']))
    cond_secondary = (working['PhaseOfEducation (name)'].fillna('').isin(['Middle deemed secondary', '16 plus', 'Secondary']))
    cond_nursery = (working['PhaseOfEducation (name)'].fillna('') == 'Nursery')
    working['Overall Phase'] = np.select(
        [cond_pru, cond_special, cond_all_through, cond_primary, cond_secondary, cond_nursery],
        ['Pupil referral unit', 'Special', 'All-through', 'Primary', 'Secondary', 'Nursery'],
        default=None
    )

    cond_infant = (working['StatutoryHighAge'] == 7).fillna(False)
    cond_junior = (working['StatutoryHighAge'] == 11).fillna(False) & (working['StatutoryLowAge'] >= 7).fillna(False)
    working['SFB Phase'] = np.where(
        working['Overall Phase'].isin(['Primary']),
        np.select(
            [cond_infant, cond_junior],
            ['Infant', 'Junior'],
            default='Infant and junior'
        ),
        working['Overall Phase']
    )

    # Regional Logic
    # working['Region'] = np.where(working['region_name'].str.contains('London', na=False), 'London', working['region_name'])
    # working['London Borough'] = np.where(working['region_name'] == 'Inner London', 'Inner', 
    #                                      np.where(working['region_name'] == 'Outer London', 'Outer', 'Neither'))
    working['PFI Funding'] = np.where(working['E28b'] > 0, 'Y', 'N')

    # 3. Add Pupil Numbers 2025 (#CFRWorkingGIASPupils)
    working = working.merge(gias_subset.reset_index()[["URN", "LAEstab"]], on="LAEstab", how="left")
    working = working.merge(census, left_on='URN', right_index=True, how='left')
    working = working.merge(sen, left_on='URN', right_index=True, how='left')
    working = working.merge(pru, on='LAEstab', how='left')

    cond_pru = (working['EstablishmentTypeGroup (name)'] == 'Pupil referral unit').fillna(False)
    cond_hospital_school = (working['GHSIndicator'] == 'GHS').fillna(False)
    working['IndPupils_FTE25'] = np.select(
        [cond_pru, cond_hospital_school],
        [working['PRU_Headcount'], working['TotalHeadcount']], # TotalHeadcount from Hospital schools
        default=working['Number of pupils']
    )

    # 4. Add Pupil Numbers 2024 (#CFRWorkingGIASPupils24)
    working = working.merge(census_last_year, left_on='URN', right_index=True, how='left', suffixes=('', '_24'))
    working = working.merge(sen_last_year, left_on='URN', right_index=True, how='left', suffixes=('', '_24'))
    working = working.merge(pru_last_year, on='LAEstab', how='left', suffixes=('', '_24'))
    working = working.merge(hospital_schools_last_year[['LAESTAB', 'TotalHeadcount']], 
                            left_on='LAEstab', right_on="LAESTAB", how='left', suffixes=('', '_ghs24'))

    working['IndPupils_FTE24'] = np.select(
        [cond_pru, cond_hospital_school],
        [working['PRU_Headcount_24'], working['TotalHeadcount_ghs24']],
        default=working['Number of pupils_24']
    )

    # 5. Final Join with Workforce and Aggregated Federations
    working = working.merge(cfr_federations_aggregated, 
                            left_on='LAEstab', right_on="Lead_school", how='left', suffixes=('', '_fed'))
    # working = working.merge(sw_25, on='LAEstab', how='left')
    # working = working.merge(sw_24, on='LAEstab', how='left', suffixes=('_sw25', '_sw24'))

    # 6. Final Selection & SFB Calculation Logic
    final = pd.DataFrame()
    final['LAEstab'] = working['LAEstab']
    final['School_Name'] = working['EstablishmentName']
    final['DNS'] = working['DNS']
    
    def fallback(df, col25, col24):
        return np.where(df[col25].isna() & (df[col24] > 0), df[col24], df[col25])

    final['Ind. Pupils FTE'] = fallback(working, 'IndPupils_FTE25', 'IndPupils_FTE24')
    
    # Aggregated Pupils FTE logic
    final['Aggregated Pupils FTE'] = np.where(
        final['DNS'] == 'LeadSchool', working['FTE_fed'],
        np.where((final['DNS'] == 'n/a') & working['IndPupils_FTE25'].isna() & (working['IndPupils_FTE24'] > 0), 
                 working['IndPupils_FTE24'], working['IndPupils_FTE25'])
    )

    # SEN Aggregation Logic
    def calc_sen_pct(df, ehc_col, tot_col):
        return (df[ehc_col] / df[tot_col] * 100).round(2)

    final['% of pupils with EHCP_agg'] = np.where(
        final['DNS'] == 'LeadSchool', 
        working['PC_EHCP'],
        np.where(working['IndPupils_FTE25'].isna() & (working['Total pupils_24'] > 0), 
                 calc_sen_pct(working, 'EHC plan_24', 'Total pupils_24'),
                 calc_sen_pct(working, 'EHC plan', 'Total pupils'))
    )

    # Workforce Fallbacks
    final['Teachers FTE_agg'] = np.where(
        final['DNS'] == 'LeadSchool', 
        working['Total Number of Teachers (Full-Time Equivalent)_fed'],
        fallback(working, 'Total Number of Teachers (Full-Time Equivalent)', 'Total Number of Teachers (Full-Time Equivalent)_24')
    )

    final
    return cfr_federations_aggregated