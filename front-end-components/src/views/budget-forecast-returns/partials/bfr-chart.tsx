import { LineChart } from "src/components/charts/line-chart";
import { BfrChartProps } from "./types";
import { format } from "date-fns";
import { shortValueFormatter } from "src/components/charts/utils";
import { Loading } from "src/components/loading";
import { forwardRef } from "react";
import { ChartHandler } from "src/components";

export const BfrChart = forwardRef<ChartHandler, BfrChartProps>(
  ({ data, onImageCopied, onImageLoading }, ref) => {
    return (
      <div className="govuk-grid-column-full" style={{ height: 400 }}>
        {data.length > 0 ? (
          <LineChart
            chartTitle="Year-end revenue reserves"
            data={data}
            grid
            highlightActive
            keyField="periodEndDate"
            margin={20}
            onImageCopied={onImageCopied}
            onImageLoading={onImageLoading}
            ref={ref}
            seriesConfig={{
              actual: {
                label: "Accounts return balance",
                visible: true,
              },
              forecast: {
                label: "Budget forecast return balance",
                visible: true,
                style: "dashed",
              },
            }}
            seriesLabelField="periodEndDate"
            seriesFormatter={(value: unknown) =>
              format(new Date(value as string), "d MMM yyyy")
            }
            valueFormatter={(v) =>
              shortValueFormatter(v, { valueUnit: "currency" })
            }
            valueUnit="currency"
            legend
            labels
          />
        ) : (
          <Loading />
        )}
      </div>
    );
  }
);
