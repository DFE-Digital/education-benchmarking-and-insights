import {
  NameType,
  ValueType,
} from "recharts/types/component/DefaultTooltipContent";
import { ChartDataSeries } from "src/components";
import { LineChartTooltipProps } from "src/components/charts/line-chart-tooltip";

export function LineChartTooltip<
  TData extends ChartDataSeries,
  TValue extends ValueType,
  TName extends NameType,
>(props: LineChartTooltipProps<TData, TValue, TName>) {
  const { active, payload, valueFormatter, valueUnit } = props;
  if (active && payload && payload.length) {
    return (
      <div className="chart-active-tooltip">
        <p className="govuk-body-s">
          {valueFormatter
            ? valueFormatter(payload[0].value, { valueUnit })
            : String(payload[0].value)}
        </p>
      </div>
    );
  }

  return null;
}
