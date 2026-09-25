from pipeline.input_schemas import evolve_schema
from pipeline.pre_processing.cfr.transparency_file.transparency_file_schema import (
    get_transparency_file_schema,
)


def test_evolve_schema_basic():
    base = {"a": "1", "b": "2", "c": "3", "d": "4"}
    res = evolve_schema(
        base,
        removals=["b"],
        renames={"c": "charlie"},
        additions={"e": "5"},
    )

    assert res == {"a": "1", "charlie": "3", "d": "4", "e": "5"}
    assert list(res.keys()) == [
        "a",
        "charlie",
        "d",
        "e",
    ]  # Preserves existing order + appends additions


def test_evolve_schema_tuple_renames():
    base = {"a": "val_a", "b": "val_b"}
    res = evolve_schema(
        base,
        renames={"a": ("new_a", "new_val_a")},
    )

    assert res == {"new_a": "new_val_a", "b": "val_b"}


def test_get_transparency_file_schema_2026():
    schema_2025 = get_transparency_file_schema(2025)
    schema_2026 = get_transparency_file_schema(2026)

    # 1. Dropped I18 columns
    assert (
        "I18c Income from the £1bn COVID-19 catch-up package announced on 20 July 2020"
        in schema_2025
    )
    assert (
        "I18c Income from the £1bn COVID-19 catch-up package announced on 20 July 2020"
        not in schema_2026
    )
    assert "I18d Income from other additional grants" not in schema_2026
    assert "I18 Total additional grant for schools" not in schema_2026

    # 2. Renamed keys
    assert "Grant Funding: (I01:I07) + I15 + I16 + I18a/b/c/d" in schema_2025
    assert "Grant Funding: (I01:I07) + I15 + I16 + I18a/b/c/d" not in schema_2026
    assert "Grant Funding: (I01:I07) + I15 + I16" in schema_2026

    assert "Total Income: I01:I18 - E30" in schema_2025
    assert "Total Income: I01:I18 - E30" not in schema_2026
    assert "Total Income: I01:I17 - E30" in schema_2026

    assert (
        "In-year Balance: Total Income (I01:I18 - E30) - Total Expenditure (E01:E29 + E31 + E32)"
        in schema_2025
    )
    assert (
        "In-year Balance: Total Income (I01:I18 - E30) - Total Expenditure (E01:E29 + E31 + E32)"
        not in schema_2026
    )
    assert (
        "In-year Balance: Total Income (I01:I17 - E30) - Total Expenditure (E01:E29 + E31 + E32)"
        in schema_2026
    )
