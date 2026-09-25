from pipeline.input_schemas import evolve_schema


def test_evolve_schema_removals():
    base = {"a": "1", "b": "2", "c": "3"}
    res = evolve_schema(base, removals=["b"])
    assert res == {"a": "1", "c": "3"}
    assert list(res.keys()) == ["a", "c"]


def test_evolve_schema_renames_in_place():
    base = {"a": "1", "b": "2", "c": "3"}
    res = evolve_schema(base, renames={"b": "beta"})
    assert res == {"a": "1", "beta": "2", "c": "3"}
    assert list(res.keys()) == ["a", "beta", "c"]


def test_evolve_schema_tuple_renames():
    base = {"a": "1", "b": "2"}
    res = evolve_schema(base, renames={"a": ("alpha", "100")})
    assert res == {"alpha": "100", "b": "2"}


def test_evolve_schema_additions():
    base = {"a": "1", "b": "2"}
    res = evolve_schema(base, additions={"c": "3", "d": "4"})
    assert res == {"a": "1", "b": "2", "c": "3", "d": "4"}
    assert list(res.keys()) == ["a", "b", "c", "d"]


def test_evolve_schema_all_operations_combined():
    base = {"col_a": "float", "col_b": "int", "col_c": "string"}
    res = evolve_schema(
        base,
        removals=["col_b"],
        renames={"col_a": "col_alpha"},
        additions={"col_d": "boolean"},
    )
    assert res == {"col_alpha": "float", "col_c": "string", "col_d": "boolean"}
    assert list(res.keys()) == ["col_alpha", "col_c", "col_d"]
