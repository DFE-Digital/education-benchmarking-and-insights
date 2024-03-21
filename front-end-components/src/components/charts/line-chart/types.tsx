import { CurveType } from "recharts/types/shape/Curve";
import {
  ChartDataSeries,
  ChartProps,
  ValueFormatterProps,
} from "src/components";

export interface LineChartProps<TData extends ChartDataSeries>
  extends ChartProps<TData>,
    ValueFormatterProps {
  curveType?: CurveType;
}
