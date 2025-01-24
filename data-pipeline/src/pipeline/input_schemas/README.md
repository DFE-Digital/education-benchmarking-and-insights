# Input Schemas

This package contains modules which define:

- the expected format of each dataset (which may vary between years)
- any additional mapping and/or processing to derive additional data

Broadly, each module must define some or all of the following:

- a single value for the index column
- a `dict` defining the expected columns for use in Pandas' `usecols` and
  `dtype` parameters
  - this must have a `default` key to be used where no other keys match
  - additional keys must be `int` values corresponding to years which deviate
    from the values in `default`
  - values must be a `dict` mapping the expected column name to its Pandas
    data type (e.g. `float`)
- a `dict` which defines mappings for column names (i.e. to be used in a
  Pandas `rename()` call):
  - this must have a `default` key to be used where no other keys match
  - additional keys must be `int` values corresponding to years which deviate
    from the values in `default`
- a `dict` which defines additional columns to be calculated via Pandas'
  `eval()`:
  - this must have a `default` key to be used where no other keys match
  - additional keys must be `int` values corresponding to years which deviate
    from the values in `default`
