dsg_filenames = {
    2023: "dedicated-schools-grant_2022-to-2023_published-20-07-2023.ods",
    2024: "dedicated-schools-grant_2023-to-2024_published-17-07-2024.ods",
    2025: "dedicated-schools-grant_2024-to-2025_published-22-07-2025.ods",
}

def get_six_and_ten_k_cols(year):
    return (
        f"{year-1} to {year} mainstream academies and free schools - pre-16 SEN Unit/RP Places (@£6k)",
        f"{year-1} to {year} mainstream academies and free schools - pre-16 SEN Unit/RP Places (@£10k)"
    )

primary = "Primary"
secondary = "Secondary"

PRIMARY_PLACES_6K = "PrimaryPlaces6000"
PRIMARY_PLACES_10K = "PrimaryPlaces10000"
SECONDARY_PLACES_6K = "SecondaryPlaces6000"
SECONDARY_PLACES_10K = "SecondaryPlaces10000"