import { CurveType } from "recharts/types/shape/Curve";
import { ChartDataSeries, ChartProps } from "src/components";

export interface LineChartProps<TData extends ChartDataSeries>
  extends ChartProps<TData> {
  curveType?: CurveType;
}
