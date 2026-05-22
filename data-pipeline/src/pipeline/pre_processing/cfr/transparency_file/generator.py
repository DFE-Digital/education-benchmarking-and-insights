import pandas as pd
import io

from .ancillary_data import build_federation_context
from .financials import add_financials
from .formatting import (
    build_maintained_schools_download_file,
    build_maintained_schools_master_list,
    build_sfb_maintained,
)


def build_transparency_files(
    cfr_raw_blob: io.StringIO | io.BytesIO,
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
) -> tuple[pd.DataFrame, pd.DataFrame]:
    """
    Coordinates the generation of the CFR transparency files (Master List and Download File).
    Replaces the legacy My_Step1.sql through My_Step5.sql execution.
    """
    
    # 1. Read Raw CFR Data
    cfr_raw = pd.read_csv(cfr_raw_blob)
    
    # 2. Build Context (Steps 1 & 2)
    cfr_federations_with_context = build_federation_context(
        cfr_raw=cfr_raw,
        gias=gias,
        pru=pru,
        hospital_schools=hospital_schools,
        sen=sen,
        census=census,
        lookup_la=lookup_la,
        census_last_year=census_last_year,
        sen_last_year=sen_last_year,
        pru_last_year=pru_last_year,
        hospital_schools_last_year=hospital_schools_last_year,
        year=year
    )
    
    # 3. Add Financials (Step 3 partial)
    cfr_raw_fin = cfr_raw.copy().rename(columns={"LEAEstab": "LAEstab"})
    merged = cfr_federations_with_context.merge(
        cfr_raw_fin, on="LAEstab", how="left", suffixes=("", "_raw")
    )
    mergedworking = add_financials(merged)
    
    # 4. Format Outputs (Steps 3 final, 4 & 5)
    sfb_maintained = build_sfb_maintained(mergedworking, lookup_la)
    master_list = build_maintained_schools_master_list(sfb_maintained)
    download_file = build_maintained_schools_download_file(sfb_maintained)
    
    return master_list, download_file
