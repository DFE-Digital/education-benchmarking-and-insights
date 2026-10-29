from .utils import evolve_schema

ks4_index_col = "URN"

_ks4_base = {
    "URN": "Int64",
    "ATT8SCR": "float",
    "P8MEA": "float",
    "P8_BANDING": "string",
    "RECTYPE": "Int64",
    "LEA": "Int64"
}


ks4 = {"default": _ks4_base}

_ks4_column_mappings_base = {
    "ATT8SCR": "AverageAttainment",
    "P8MEA": "Progress8Measure",
    "P8_BANDING": "Progress8Banding",
}

ks4_column_mappings = {"default": _ks4_column_mappings_base}
