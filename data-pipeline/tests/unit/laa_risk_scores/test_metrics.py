import numpy as np
import pandas as pd
from pipeline.laa_risk_scores.config import DEFAULT_RISK_CONFIG


def test_pupil_change_metrics_inf_handling():
    # Arrange: previous year/4-year pupil number is 0, which would cause division by zero
    df = pd.DataFrame(
        {
            "URN": [1001, 1002, 1003],
            "Number of pupils": [50, 50, 0],
            "Number of pupils_y_minus_one": [0, 10, 0],
            "Number of pupils_y_minus_four": [0, 10, 0],
        }
    )

    # Find the metrics from DEFAULT_RISK_CONFIG
    metric_4yr = next(
        m for m in DEFAULT_RISK_CONFIG if m.name == "PupilChangeOver4Years"
    )
    metric_1yr = next(
        m for m in DEFAULT_RISK_CONFIG if m.name == "PupilChangeOver1Year"
    )

    # Act
    metric_4yr.execute(df)
    metric_1yr.execute(df)

    # Assert
    # The derived values should not contain inf or -inf
    assert not np.isinf(df["PupilChangeOver4Years"]).any()
    assert not np.isinf(df["PupilChangeOver1Year"]).any()

    # The division-by-zero case should resolve to NaN and result in max score / Major risk
    assert pd.isna(df.loc[0, "PupilChangeOver4Years"])
    assert df.loc[0, "PupilChangeOver4Years_Risk"] == "Major"
    assert df.loc[0, "PupilChangeOver4Years_Score"] == 1.5

    assert pd.isna(df.loc[0, "PupilChangeOver1Year"])
    assert df.loc[0, "PupilChangeOver1Year_Risk"] == "Major"
    assert df.loc[0, "PupilChangeOver1Year_Score"] == 1.5
