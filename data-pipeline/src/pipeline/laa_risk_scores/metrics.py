from dataclasses import dataclass, field
from enum import Enum
from typing import List, Optional, Tuple

import numpy as np
import pandas as pd


class RiskGroup(Enum):
    FINANCIAL = "Financial"
    EDUCATIONAL_PERFORMANCE = "Educational Performance"
    SCHOOL_CHARACTERISTICS = "School Characteristics"


class RiskFlag(str, Enum):
    NONE = "None"
    MINOR = "Minor"
    MAJOR = "Major"
    NO_RISK = "No Risk"
    MINOR_RISK = "Minor Risk"


@dataclass
class GradeThreshold:
    grade: str
    max_score: float
    max_risks: int
    max_major_risks: int
    min_parental_pref: Optional[float] = None
    min_attainment: Optional[float] = None


@dataclass
class MetricRule:
    lower: float
    upper: float
    score: float
    risk: str
    inclusive: str = "both"


@dataclass
class BaseRiskMetric:
    name: str
    risk_group: RiskGroup
    risk_score_maximum: float

    @property
    def value_column(self) -> str:
        return self.name

    @property
    def score_column(self) -> str:
        return f"{self.name}_Score"

    @property
    def flag_column(self) -> str:
        return f"{self.name}_Risk"

    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        """Derives the raw value of the metric from the input DataFrame.
        Override in subclasses for custom derivations.
        """
        return df[self.value_column]

    def derive_risk_score(
        self, df: pd.DataFrame, value_series: pd.Series
    ) -> Tuple[pd.Series, pd.Series]:
        """Derives the risk score and risk flag (string) from the derived value.
        Override in subclasses to implement specific scoring logic.
        """
        raise NotImplementedError

    def execute(self, df: pd.DataFrame) -> None:
        """Orchestrates the metric lifecycle: derives the value, score, and flag,
        and saves them onto the DataFrame.
        """
        value_series = self.derive_value(df)
        if not isinstance(value_series, pd.Series):
            value_series = pd.Series(value_series, index=df.index)
        df[self.value_column] = value_series
        score_series, flag_series = self.derive_risk_score(df, value_series)

        is_missing = value_series.isna()
        if is_missing.any():
            score_series = score_series.mask(is_missing, self.risk_score_maximum)
            flag_series = flag_series.mask(is_missing, RiskFlag.MAJOR.value)

        df[self.score_column] = score_series
        df[self.flag_column] = flag_series


@dataclass
class RangeRiskMetric(BaseRiskMetric):
    default_score: Optional[float] = None
    default_risk: Optional[str] = None
    rules: List[MetricRule] = field(default_factory=list)

    def __post_init__(self):
        if self.default_score is None:
            self.default_score = self.risk_score_maximum
        if self.default_risk is None:
            self.default_risk = RiskFlag.MAJOR.value

    def derive_risk_score(
        self, df: pd.DataFrame, value_series: pd.Series
    ) -> Tuple[pd.Series, pd.Series]:
        conditions = [
            value_series.between(r.lower, r.upper, inclusive=r.inclusive)
            for r in self.rules
        ]
        score_series = pd.Series(
            np.select(
                conditions, [r.score for r in self.rules], default=self.default_score
            ),
            index=df.index,
        )
        flag_series = pd.Series(
            np.select(
                conditions, [r.risk for r in self.rules], default=self.default_risk
            ),
            index=df.index,
        )
        return score_series, flag_series


@dataclass
class EndYearBalanceMetric(RangeRiskMetric):
    prev_year_column: str = "EndYearBalanceAsPercentageIncome_y_minus_one"

    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        return df["Revenue reserve"] / df["Total Income"]

    def derive_prev_year_value(self, df: pd.DataFrame) -> pd.Series:
        return df["Revenue reserve_y_minus_one"] / df["Total Income_y_minus_one"]

    def execute(self, df: pd.DataFrame) -> None:
        super().execute(df)
        prev_series = self.derive_prev_year_value(df)
        df[self.prev_year_column] = prev_series

        conditions = [
            prev_series.between(r.lower, r.upper, inclusive=r.inclusive)
            for r in self.rules
        ]
        df[f"{self.prev_year_column}_Score"] = np.select(
            conditions, [r.score for r in self.rules], default=self.default_score
        )
        df[f"{self.prev_year_column}_Risk"] = np.select(
            conditions, [r.risk for r in self.rules], default=self.default_risk
        )

        # 3. Blend previous year score with current year score (weighted)
        df[self.score_column] = (
            df[self.score_column] + df[f"{self.prev_year_column}_Score"] / 2
        ).clip(upper=self.risk_score_maximum)


@dataclass
class BinaryRiskMetric(BaseRiskMetric):
    score_when_1: float
    risk_when_1: str

    def derive_risk_score(
        self, df: pd.DataFrame, value_series: pd.Series
    ) -> Tuple[pd.Series, pd.Series]:
        score_series = pd.Series(
            np.where(value_series, float(self.score_when_1), 0.0), index=df.index
        )
        flag_series = pd.Series(
            np.where(value_series, self.risk_when_1, RiskFlag.NONE.value), index=df.index
        )
        return score_series, flag_series


@dataclass
class ConditionalRiskMetric(BaseRiskMetric):
    condition_column: str
    special_values: List[int]
    standard_rules: List[MetricRule]
    special_rules: List[MetricRule]

    def derive_risk_score(
        self, df: pd.DataFrame, value_series: pd.Series
    ) -> Tuple[pd.Series, pd.Series]:
        is_special = df[self.condition_column].isin(self.special_values)

        # Standard scoring
        conds_std = [
            value_series.between(r.lower, r.upper, inclusive=r.inclusive)
            for r in self.standard_rules
        ]
        score_std = np.select(
            conds_std, [r.score for r in self.standard_rules], default=0.0
        )
        risk_std = np.select(
            conds_std, [r.risk for r in self.standard_rules], default=RiskFlag.NONE.value
        )

        # Special scoring
        conds_spec = [
            value_series.between(r.lower, r.upper, inclusive=r.inclusive)
            for r in self.special_rules
        ]
        score_spec = np.select(
            conds_spec, [r.score for r in self.special_rules], default=0.0
        )
        risk_spec = np.select(
            conds_spec, [r.risk for r in self.special_rules], default=RiskFlag.NONE.value
        )

        score_series = pd.Series(
            np.where(is_special, score_spec, score_std), index=df.index
        )
        flag_series = pd.Series(
            np.where(is_special, risk_spec, risk_std), index=df.index
        )
        return score_series, flag_series


@dataclass
class InterestOnLoanFlagMetric(BinaryRiskMetric):
    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        return df["Other costs_Interest charges for loan and bank"] > 0


@dataclass
class PercentExpenditureOnPremisesMetric(RangeRiskMetric):
    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        return df["LAAPremisesExpenditureRollup"] / df["NetExpenditure"]


@dataclass
class PercentExpenditureOnStaffMetric(RangeRiskMetric):
    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        return df["LAAStaffExpenditureRollup"] / df["NetExpenditure"]


@dataclass
class ChangeInExpenditureOver4YearsMetric(RangeRiskMetric):
    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        return (df["Total Expenditure"] - df["Total Expenditure_y_minus_four"]) / df[
            "Total Expenditure"
        ]


@dataclass
class DeficitInLast4YearsFlagMetric(BinaryRiskMetric):
    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        return (
            df[
                [
                    "Revenue reserve",
                    "Revenue reserve_y_minus_one",
                    "Revenue reserve_y_minus_two",
                    "Revenue reserve_y_minus_three",
                    "Revenue reserve_y_minus_four",
                ]
            ]
            < 0
        ).any(axis=1)


@dataclass
class CurrentLongTermSurplusABoveThresholdFor5YearsMetric(BinaryRiskMetric):
    threshold: float = 0.15

    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        t = self.threshold
        return (
            ((df["Revenue reserve"] / df["Total Income"]) >= t)
            & (
                (df["Revenue reserve_y_minus_one"] / df["Total Income_y_minus_one"])
                >= t
            )
            & (
                (df["Revenue reserve_y_minus_two"] / df["Total Income_y_minus_two"])
                >= t
            )
            & (
                (df["Revenue reserve_y_minus_three"] / df["Total Income_y_minus_three"])
                >= t
            )
            & (
                (df["Revenue reserve_y_minus_four"] / df["Total Income_y_minus_four"])
                >= t
            )
        )


@dataclass
class PreviousLongTermBalanceDeficitMetric(BinaryRiskMetric):
    threshold: float = 0.04

    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        return (df["Revenue reserve"] / df["Total Income"]) >= self.threshold


@dataclass
class OverspendMetric(RangeRiskMetric):
    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        return (df["Total Income"] - df["Total Expenditure"]) / df["Total Income"]


@dataclass
class LargeDecreaseInBalanceMetric(BinaryRiskMetric):
    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        op1 = df["Revenue reserve"] / df["Total Income"]
        op1_prev = df["Revenue reserve_y_minus_one"] / df["Total Income_y_minus_one"]
        op2 = op1 - op1_prev
        return (op1 < 0.15) & (op2 <= -0.1)


@dataclass
class PupilNumberVarianceFromCapacityMetric(RangeRiskMetric):
    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        return df["Number of pupils"] / df["school_places"]


@dataclass
class PupilChangeOver1YearMetric(RangeRiskMetric):
    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        return (df["Number of pupils"] - df["Number of pupils_y_minus_one"]) / df[
            "Number of pupils_y_minus_one"
        ]


@dataclass
class PupilChangeOver4YearsMetric(RangeRiskMetric):
    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        return (df["Number of pupils"] - df["Number of pupils_y_minus_four"]) / df[
            "Number of pupils_y_minus_four"
        ]


@dataclass
class TotalPupilsSixthFormMetric(RangeRiskMetric):
    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        return df["TotalPupilsSixthForm"]


@dataclass
class PupilAbsenceMetric(ConditionalRiskMetric):
    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        return df["sess_overall_percent"]


@dataclass
class ParentalPreferenceMetric(RangeRiskMetric):
    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        return df["proportion_1stprefs_v_totaloffers"]


@dataclass
class PerformanceTablesProgressScoreMetric(RangeRiskMetric):
    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        primary_secondary_condition = [
            df["Overall Phase"] == "Primary",
            df["Overall Phase"] == "Secondary",
        ]
        progress_score_choices = [(df["Ks2Progress"] / 3), df["Progress8Measure"]]
        result = np.select(
            primary_secondary_condition, progress_score_choices, default=np.nan
        )
        return pd.Series(result, index=df.index)


@dataclass
class PerformanceTablesAchievementScoreMetric(RangeRiskMetric):
    def derive_value(self, df: pd.DataFrame) -> pd.Series:
        primary_secondary_condition = [
            df["Overall Phase"] == "Primary",
            df["Overall Phase"] == "Secondary",
        ]
        achievement_score_choices = [df["PTRWM_EXP"], df["AverageAttainment"]]
        result = np.select(
            primary_secondary_condition, achievement_score_choices, default=np.nan
        )
        return pd.Series(result, index=df.index)
