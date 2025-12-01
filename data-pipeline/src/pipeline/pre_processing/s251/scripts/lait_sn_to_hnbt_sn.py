# STANDALONE SCRIPT
# This file pivots 2025 statistical neighbours (SN) data from the LAIT https://www.gov.uk/government/publications/local-authority-interactive-tool-lait
# To a format like is expected by the FBIT data pipeline - a wide format seen on the high needs benchmarking tool SN data.
# This generates an xlsx, but the pipeline expects and xlsm. Change the extention manually before starting the pipeline.

import pandas as pd

df_original = pd.read_csv("~/Documents/dfe-fbit/2025/Stat_Neighbours_2025.csv")

# Pivot SN cleartext name
df_names = df_original.pivot(
   index=['old_la_code'],
   columns='SN',
   values='SN_name'
)
# Rename columns to SN1, SN2, etc. (names were 1, 2, 3...)
df_names.columns = [f'SN{col}' for col in df_names.columns]
df_names = df_names.reset_index()

df_prox = df_original.pivot(
    index=['old_la_code'],
    columns='SN',
    values='SN_prox'
)
# Rename columns for SN1_prox, SN2_prox, etc. (names were 1, 2, 3...)
df_prox.columns = [f'SN{col}.1' for col in df_prox.columns]
df_prox = df_prox.reset_index()

df_codes = df_original.pivot(
    index=['old_la_code'],
    columns='SN',
    values='old_la_code2'
)
# Rename columns for SN1_code, SN2_code, etc. (names were 1, 2, 3...)
df_codes.columns = [f'SN{col}.2' for col in df_codes.columns]
df_codes = df_codes.reset_index()

# Merge names and proximity data
df_merged = pd.merge(df_names, df_prox, on=['old_la_code'], how='left')
# Merge in the codes
df_final = pd.merge(df_merged, df_codes, on=['old_la_code'], how='left')

# Get a list of the LA columns
la_cols = ['old_la_code']
# Get a list of the SN Name and Proximity columns, ordered by SN (up to SN4 in this example)
sn_name_prox_cols = [col for i in range(1, 11) for col in [f'SN{i}', f'SN{i}.1']]
# Get a list of the SN Code columns
sn_code_cols = [f'SN{i}.2' for i in range(1, 11)]

# Combine all desired columns in the final order: LA info, then interleaved Name/Prox, then Code list
final_columns_order = la_cols + sn_name_prox_cols + sn_code_cols

# Select and reorder the columns
df_final = df_final[final_columns_order]

# Rename the key LA columns
df_final = df_final.rename(columns={'old_la_code': 'LA number'})
print(df_final.shape)
df_final.to_excel("~/Documents/dfe-fbit/2025/High-needs-local-authority-benchmarking-tool.xlsx", index=False, sheet_name="SNsWithNewDorsetBCP", engine='xlsxwriter')