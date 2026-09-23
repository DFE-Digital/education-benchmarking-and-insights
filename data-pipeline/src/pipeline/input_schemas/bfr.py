base_sofa_cols = {
    "TrustUPIN": "Int64",
    "Title": "string",
    "EFALineNo": "Int64",
    "Y1P1": "float",
    "Y1P2": "float",
    "Y2P1": "float",
    "Y2P2": "float",
}

_2025_sofa_cols = base_sofa_cols.copy()
_2025_sofa_cols["Y3P1"] = 'float'
_2025_sofa_cols["Y3P2"] = 'float'

bfr_sofa_cols = {
    "default": base_sofa_cols,
    2025: _2025_sofa_cols,
    2026: _2025_sofa_cols,
}

bfr_3y_cols = {
    "TrustUPIN": "Int64",
    "EFALineNo": "Int64",
    "Y2": "float",
    "Y3": "float",
    "Y4": "float",
}
