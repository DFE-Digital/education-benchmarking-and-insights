# Automated Testing for CSV to DataFrame Pipeline

## Objective

To implement automated testing for validating the transformation of input CSV files into DataFrames. The goal is to ensure data integrity, schema consistency, and correctness of derived columns before further processing in the data pipeline.

## Scope

- Validate CSV to DataFrame transformation.
- Ensure schema consistency and data integrity.
- Verify correctness of computed/derived columns.

## Testing Approach

### 1. Schema Validation

- Ensure all expected columns from the CSV are present in the DataFrame.
- Validate that data types in the DataFrame match expectations.

### 2. Data Integrity Checks

- Verify that no data is lost during transformation.
- Ensure we have the expected row count in the DataFrame.

### 3. Transformation Validation

- Validate that derived columns (e.g., sums of multiple columns) are correctly calculated for a sample of the data.
- For large datasets, perform calculations on a representative sample (e.g., 100 rows out of 10,000+).
- Ensure missing/null values are correctly handled.

### 4. Range Validation

- Ensure columns with float data types do not contain all null values.
- Define and validate acceptable ranges for every column.

## Implementation Plan

### 1. Test Framework Setup

- Use **Pytest + [Pandera](https://pandera.readthedocs.io/)** for validation.
- Organise test cases in a structured `tests/` directory.
- Ensure sample test data is available in the `data/` directory or implement a mechanism to read directly from **Azure Storage Explorer** to use the latest files.

### 2. Writing Automated Test Cases

- **Schema Validation:** Check column names and data types.
- **Row Count Validation:** Ensure the number of rows matches.
- **Column Sum Validation:** Verify if any column is derived as a sum of multiple CSV columns.
- **Null Handling Validation:** Ensure missing values are correctly processed.
- **Range Validation:** Validate that column values fall within expected ranges.
- **Data Integrity Check:** Compare values of non-derived columns to ensure they remain unchanged.

## Expected Outcomes

- Improved confidence in data integrity and transformations.
- Automated detection of schema mismatches and data errors.
- Faster validation cycles, reducing manual checks.
- Seamless integration into CI/CD workflows.

## Next Steps

1. Set up the test framework and sample test data.
2. Write initial test cases for schema, row counts, and derived columns.
3. Implement integration with **Azure Storage Explorer** for real-time test data access.
4. Run tests on local development setup.
5. Integrate into CI/CD for automated validation.
6. Review results and extend the tests to other input files.
