import {
  CompareYourCosts,
  CompareYourWorkforce,
  SchoolHistory,
} from "src/views";
import { VerticalBarChart } from "./components/charts/vertical-bar-chart";
import { LineChart } from "./components/charts/line-chart";
import { Stat } from "./components/charts/stat";
import { DeploymentPlan } from "src/views/deployment-plan";
import { ComparisonChartSummary } from "./composed/comparison-chart-summary";
import { ResolvedStat } from "./components/charts/resolved-stat";

export const components = {
  CompareYourCosts,
  CompareYourWorkforce,
  ComparisonChartSummary,
  DeploymentPlan,
  SchoolHistory,
  VerticalBarChart,
  LineChart,
  ResolvedStat,
  Stat,
};
