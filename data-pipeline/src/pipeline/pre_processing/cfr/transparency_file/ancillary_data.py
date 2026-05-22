import pandas as pd
import numpy as np


def _get_financial_year_anchors(year: int):
    """Derive temporal anchors from the reporting year (e.g., '2025' for 2024/25)."""
    return {
        "ref_date": pd.Timestamp(f"{year - 1}-04-01"),
        "ref_start": pd.Timestamp(f"{year - 1}-04-01"),
        "ref_prev_end": pd.Timestamp(f"{year - 1}-03-31")
    }


def _filter_gias(gias: pd.DataFrame, ref_date: pd.Timestamp) -> pd.DataFrame:
    """Filter GIAS for open schools and recently closed schools."""
    gias = gias.dropna(subset=["LA (code)", "EstablishmentNumber"]).copy()
    gias["LAEstab"] = (
        gias["LA (code)"].astype(str).str.zfill(3) + 
        gias["EstablishmentNumber"].astype(str).str.zfill(4)
    ).astype(int)
    
    is_open_status = ['Open', 'Open, but proposed to close', 'Proposed to open']
    
    closed_schools = gias[
        (gias["EstablishmentStatus (name)"] == "Closed") & 
        (pd.to_datetime(gias["CloseDate"]) >= ref_date)
    ]
    
    open_schools = gias[
        gias["EstablishmentStatus (name)"].isin(is_open_status) & 
        (
            (gias["EstablishmentTypeGroup (name)"] == "Local authority maintained schools") |
            (gias["TypeOfEstablishment (name)"] == "Community special school") |
            (gias["TypeOfEstablishment (name)"] == "Foundation special school")
        )
    ]
    return pd.concat([closed_schools, open_schools])


def _extract_federation_topology(cfr_raw: pd.DataFrame) -> pd.DataFrame:
    """Extract the lead and member school mappings from CFR data."""
    fed_lead = pd.DataFrame({
        "LA": cfr_raw["LANumber"].astype(int),
        "LAEstab": cfr_raw["LEAEstab"],
        "Estab": cfr_raw["Estab"].astype(int),
        "Did_Not_Submit": np.where(
            cfr_raw["Federated Flag"].eq("No"), "Not federated",
            np.where(cfr_raw["Federated Flag"].eq("Yes"), "Lead", None),
        ),
        "Lead_school": np.where(cfr_raw["Federated Flag"].eq("Yes"), cfr_raw["LEAEstab"], 0),
        "Index_number": np.where(
            cfr_raw["Federated Flag"].eq("Yes"),
            "A" + cfr_raw["LANumber"].astype(str) + cfr_raw["Estab"].astype(str),
            "Z",
        ),
    })

    fed_member_frames = []
    for n in range(1, 11):
        fed_col = f"LAEstab of School in Federation {n}"
        mask = (cfr_raw[fed_col] > 1) & (cfr_raw["LEAEstab"] != cfr_raw[fed_col])
        fed_school_slot = cfr_raw.loc[mask].copy()
        if not fed_school_slot.empty:
            fed_member_frames.append(pd.DataFrame({
                "LA": fed_school_slot["LANumber"],
                "LAEstab": fed_school_slot[fed_col].astype(int),
                "Estab": (fed_school_slot[fed_col].astype(int) % 10000),
                "Did_Not_Submit": "DNS",
                "Lead_school": fed_school_slot["LEAEstab"].astype(int),
                "Index_number": "A" + fed_school_slot["LANumber"].astype(str) + fed_school_slot["Estab"].astype(str),
            }))

    all_federations = pd.concat([fed_lead, *fed_member_frames], ignore_index=True)
    all_federations["Estab"] = all_federations["Estab"].astype("Int64")
    return all_federations.drop_duplicates(subset=["LAEstab", "Lead_school"])


def _calculate_period_covered(df: pd.DataFrame, ref_start: pd.Timestamp, ref_prev_end: pd.Timestamp) -> pd.Series:
    """Vectorized calculation for the months covered by a return."""
    months = pd.Series(12.0, index=df.index)
    
    close_dates = pd.to_datetime(df['CloseDate'], errors='coerce')
    close_mask = close_dates.notnull()
    if close_mask.any():
        diff = (close_dates.dt.year - ref_prev_end.year) * 12 + close_dates.dt.month - ref_prev_end.month
        valid_diff_mask = (diff > 0) & (diff < 12)
        months.loc[close_mask & valid_diff_mask] = diff[close_mask & valid_diff_mask]

    open_dates = pd.to_datetime(df['OpenDate'], errors='coerce')
    open_mask = open_dates.notnull() & ~close_mask
    if open_mask.any():
        diff = (ref_start.year - open_dates.dt.year) * 12 + ref_start.month - open_dates.dt.month
        valid_diff_mask = (diff > 0) & (diff < 12)
        months.loc[open_mask & valid_diff_mask] = diff[open_mask & valid_diff_mask]

    return months


def _pct(num, den):
    with np.errstate(divide='ignore', invalid='ignore'):
        res = (num / np.where(den == 0, np.nan, den)) * 100
    return np.round(res, 2)


def _fallback(this_v, last_v):
    return np.where(this_v.isna() & (last_v > 0), last_v, this_v)


def build_federation_context(
    cfr_raw: pd.DataFrame, 
    gias: pd.DataFrame, 
    pru: pd.DataFrame, 
    hospital_schools: pd.DataFrame, 
    sen: pd.DataFrame, 
    census: pd.DataFrame, 
    lookup_la: pd.DataFrame,
    census_last_year: pd.DataFrame, 
    sen_last_year: pd.DataFrame, 
    pru_last_year: pd.DataFrame, 
    hospital_schools_last_year: pd.DataFrame, 
    year: int,
) -> pd.DataFrame:
    """
    Build the base context dataframe merging GIAS, Census, SEN, and PRU data 
    onto the extracted federation topology. (Step 1 and 2 SQL logic).
    """
    anchors = _get_financial_year_anchors(year)
    
    # Ensure PRU columns match expected names for logic below
    pru = pru.rename(columns={"Headcount": "PRU_Headcount"})
    pru_last_year = pru_last_year.rename(columns={"Headcount": "PRU_Headcount"})

    # 1 & 2: Topology and Base Context Merge
    gias_subset = _filter_gias(gias, anchors["ref_date"])
    all_federations = _extract_federation_topology(cfr_raw)

    fedmatched = (
        all_federations
        .merge(gias_subset.reset_index(), on="LAEstab", how="left")
        .merge(pru, on="LAEstab", how="left")
        .merge(census.reset_index(), on="URN", how="left")
        .merge(sen, on="URN", how="left")
        .merge(hospital_schools[['LAEstab', 'GHSIndicator', "TotalHeadcount"]], on="LAEstab", how="left")
        .merge(lookup_la, left_on='LA', right_on='old_la_code', how='left')
    )
    
    # Calculate individual FTE in fedmatched for aggregation
    fedmatched["FTE"] = np.where(
        fedmatched["TypeOfEstablishment (name)"].eq("Pupil referral unit").fillna(False),
        fedmatched["PRU_Headcount"],
        fedmatched["Total pupils"]
    )
    fedmatched["VIthForm"] = 0 # Match SQL's '0 as VIthForm'

    # 3. Aggregate lead schools
    agg_cols = [
        "FTE", "total boarders", "PRU_Headcount", 
        "number of pupils known to be eligible for free school meals", 
        "number of pupils whose first language is known or believed to be other than English",
        "Total pupils", "SEN support", "EHC plan", "VIthForm",
        "Total Number of Teachers (Full-Time Equivalent)", 
        "Total Number of Teaching Assistants (Full-Time Equivalent)", 
    ]
    
    leadschools_agg = (
        fedmatched[fedmatched["Lead_school"] > 99]
        .groupby("Lead_school", as_index=False)[agg_cols].sum(min_count=1)
    )

    cfr_federations_aggregated = leadschools_agg.assign(
        Federation_FTE=leadschools_agg["FTE"],
        Federation_boarders=_pct(leadschools_agg["total boarders"], leadschools_agg["FTE"]),
        Federation_FSM=_pct(leadschools_agg["number of pupils known to be eligible for free school meals"], leadschools_agg["FTE"]),
        Federations_EAL=_pct(leadschools_agg["number of pupils whose first language is known or believed to be other than English"], leadschools_agg["FTE"]),
        Federation_SEN=_pct(leadschools_agg["SEN support"], leadschools_agg["Total pupils"]),
        Federation_EHCP=_pct(leadschools_agg["EHC plan"], leadschools_agg["Total pupils"]),
        Federation_VIthForm=leadschools_agg["VIthForm"],
        Federation_FTETeachers=leadschools_agg["Total Number of Teachers (Full-Time Equivalent)"],
        Federation_FTE_TAs=leadschools_agg["Total Number of Teaching Assistants (Full-Time Equivalent)"],
    )

    # 4. Prepare Working Data
    working = fedmatched.copy()
    working = working.merge(cfr_raw[['LEAEstab', 'E28b']], left_on='LAEstab', right_on='LEAEstab', how='left')
    
    # Vectorized Period Covered
    working['Period Covered'] = _calculate_period_covered(working, anchors["ref_start"], anchors["ref_prev_end"])

    working['DNS'] = np.where(working['Did_Not_Submit'] == 'DNS', 'DNS', 
                              np.where(working['Lead_school'] > 0, 'LeadSchool', 'n/a'))
    working['London Borough'] = np.where(working['region_name'] == 'Inner London', 'Inner', 
                                        np.where(working['region_name'] == 'Outer London', 'Outer', 'Neither'))
    working['Region'] = np.where(working['region_name'].str.contains('London', na=False), 'London', working['region_name'])
    working['PFI Funding'] = np.where(working['E28b'] > 0, 'Y', 'N')

    # SFB Phase Logic
    special_types = ['Community special school', 'Foundation special school']
    cond_pru = (working['PhaseOfEducation (name)'].fillna('') == 'Not applicable') & \
            ((working["EstablishmentTypeGroup (name)"].fillna('') == 'Pupil referral unit') | (working["TypeOfEstablishment (name)"].fillna('') == 'Pupil referral unit'))
    cond_special = (working['PhaseOfEducation (name)'].fillna('') == 'Not applicable') & \
            (working['EstablishmentTypeGroup (name)'].fillna('').isin(special_types) | working["TypeOfEstablishment (name)"].isin(special_types))
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
        np.select([cond_infant, cond_junior], ['Infant', 'Junior'], default='Infant and junior'),
        working['Overall Phase']
    )

    # Merge fallbacks
    working = working.merge(census_last_year, left_on='URN', right_index=True, how='left', suffixes=('', '_24'))
    working = working.merge(sen_last_year, left_on='URN', right_index=True, how='left', suffixes=('', '_24'))
    working = working.merge(pru_last_year, on='LAEstab', how='left', suffixes=('', '_24'))
    working = working.merge(hospital_schools_last_year[['LAESTAB', 'TotalHeadcount']], 
                            left_on='LAEstab', right_on="LAESTAB", how='left', suffixes=('', '_ghs24'))

    def ind_pupils(df, suffix=''):
        c_pru = (df['TypeOfEstablishment (name)'] == 'Pupil referral unit').fillna(False)
        c_ghs = (df['GHSIndicator'] == 'GHS').fillna(False)
        pru_hc = 'PRU_Headcount' + suffix
        ghs_hc = ('TotalHeadcount' if suffix == '' else 'TotalHeadcount_ghs24')
        pup_fte = ('Total pupils' if suffix == '' else 'Total pupils_24')
        return np.select([c_pru, c_ghs], [df[pru_hc], df[ghs_hc]], default=df[pup_fte])

    working['IndPupils_FTE25'] = working['FTE']
    working['IndPupils_FTE24'] = ind_pupils(working, '_24')

    # Join Aggregated Federation Data
    working = working.merge(cfr_federations_aggregated.drop(columns=agg_cols), 
                            left_on='Lead_school', right_on="Lead_school", how='left')

    working['Ind. Pupils FTE'] = _fallback(working['IndPupils_FTE25'], working['IndPupils_FTE24'])
    working['Aggregated Pupils FTE'] = np.where(working['DNS'] == 'LeadSchool', working['Federation_FTE'], working['Ind. Pupils FTE'])
    
    # Workforce Aggregation
    working['Teachers FTE_agg'] = np.where(
        working['Lead_school'] > 0, 
        working['Federation_FTETeachers'],
        _fallback(working['Total Number of Teachers (Full-Time Equivalent)'], working['Total Number of Teachers (Full-Time Equivalent)_24'])
    )

    # Percentage Derivations
    fsm_num = _fallback(
        working["number of pupils known to be eligible for free school meals"], 
        working.get("number of pupils known to be eligible for free school meals_24", np.nan))
    fsm_den = _fallback(working["FTE"], working.get("Total pupils_24", np.nan))
    working["% of pupils eligible for FSM_ind"] = _pct(fsm_num, fsm_den)
    working["% of pupils eligible for FSM_agg"] = np.where(working["DNS"] == "LeadSchool", working["Federation_FSM"], working["% of pupils eligible for FSM_ind"])

    ehcp_num = _fallback(working["EHC plan"], working.get("EHC plan_24", np.nan))
    ehcp_den = _fallback(working["Total pupils"], working.get("Total pupils_24", np.nan))
    working["% of pupils with EHCP_ind"] = _pct(ehcp_num, ehcp_den)
    working["% of pupils with EHCP_agg"] = np.where(
        working["DNS"] == "LeadSchool", working["Federation_EHCP"], working["% of pupils with EHCP_ind"])

    sen_num = _fallback(working["SEN support"], working.get("SEN support_24", np.nan))
    working["% of pupils with SEN Support_ind"] = _pct(sen_num, ehcp_den)
    working["% of pupils with SEN Support_agg"] = np.where(
        working["DNS"] == "LeadSchool", working["Federation_SEN"], working["% of pupils with SEN Support_ind"])

    eal_num = _fallback(
        working["number of pupils whose first language is known or believed to be other than English"], 
        working.get("number of pupils whose first language is known or believed to be other than English_24", 
        np.nan)
    )
    working["% of pupils with EAL_ind"] = _pct(eal_num, fsm_den)
    working["% of pupils with EAL_agg"] = np.where(working["DNS"] == "LeadSchool", working["Federations_EAL"], working["% of pupils with EAL_ind"])

    boarders_num = _fallback(working["total boarders"], working.get("total boarders_24", np.nan))
    working["% of pupils who are Boarders_ind"] = _pct(boarders_num, fsm_den)
    working["% of pupils who are Boarders_agg"] = np.where(
        working["DNS"] == "LeadSchool", working["Federation_boarders"], working["% of pupils who are Boarders_ind"])

    qts_col = "Teachers with Qualified Teacher Status (%) (Headcount)"
    qts_25 = working.get(qts_col, pd.Series(np.nan, index=working.index))
    qts_24 = working.get(f"{qts_col}_24", pd.Series(np.nan, index=working.index))
    working["Teachers_PC_QTS"] = _fallback(qts_25, qts_24)

    return working.copy()
