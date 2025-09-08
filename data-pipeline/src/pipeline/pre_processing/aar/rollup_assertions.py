# Test that Total Expenditure_CS rolls up correctly to central services totals per trust
def test_trust_rollup(academies, central_services, tolerance=0.001):
    """Test that academy CS totals roll up to central services totals per trust."""

    # Academy rollup by trust (only for academies that have central services data)
    academy_cs_totals = (
        academies[
            academies["Total Expenditure_CS"].notna()
        ]  # Only academies with CS data
        .groupby("Trust Name")
        .agg({"Total Expenditure_CS": "sum"})
        .reset_index()
        .rename(columns={"Total Expenditure_CS": "Academy_CS_Total"})
    )

    # Central services totals by trust
    cs_totals = central_services[["Company_Name", "Total Expenditure"]].rename(
        columns={"Company_Name": "Trust Name"}
    )

    # Merge and compare (left join to only test trusts that joined to academies)
    comparison = academy_cs_totals.merge(cs_totals, on="Trust Name", how="left")
    comparison["Difference"] = (
        comparison["Academy_CS_Total"] - comparison["Total Expenditure"]
    )
    comparison["Within_Tolerance"] = comparison["Difference"] <= tolerance

    all_within_tolerance = comparison["Within_Tolerance"].all()

    if not all_within_tolerance:
        print(f"\nTrusts outside tolerance:", flush=True)
        outliers = comparison[~comparison["Within_Tolerance"]]
        print(
            outliers[
                ["Trust Name", "Academy_CS_Total", "Total Expenditure", "Difference"]
            ].to_string(index=False)
        )

    return all_within_tolerance


def test_academies_rollup(academies, aar, tolerance=0.01):
    comparison_academies = (
        academies[["URN"]]
        .assign(
            Derived_No_CS=academies["Total Expenditure"]
            - academies["Total Expenditure_CS"]
        )
        .merge(aar[["Total Expenditure"]], left_index=True, right_index=True)
        .rename(columns={"Total Expenditure": "Original_AAR"})
    )

    comparison_academies["Within_Tolerance"] = (
        comparison_academies["Derived_No_CS"] - comparison_academies["Original_AAR"]
    ) <= tolerance

    return comparison_academies["Within_Tolerance"].all()
