import React, {
  useCallback,
  useEffect,
  useMemo,
  useRef,
  useState,
} from "react";
import { BalanceApi, BudgetForecastReturn } from "src/services";
import { LineChart } from "src/components/charts/line-chart";
import { BudgetForecastData } from "./types";
import { format } from "date-fns";
import {
  Actual,
  ChartDimensions,
  ChartHandler,
  ForecastCategories,
  PoundsPerPupil,
} from "src/components";
import { shortValueFormatter } from "src/components/charts/utils";
import { Loading } from "src/components/loading";

export const YearEnd: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(Actual);
  const [data, setData] = useState<BudgetForecastReturn[] | null>();
  const [imageLoading, setImageLoading] = useState<boolean>();
  const lineChart2SeriesRef = useRef<ChartHandler>(null);

  const getData = useCallback(async () => {
    setData(null);
    return await BalanceApi.budgetForecastReturns(id);
  }, [id]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const chartData: BudgetForecastData[] = useMemo(
    () =>
      data && Array.isArray(data)
        ? data.map(({ year, forecast, actual, totalPupils }) => {
            let forecastValue = forecast;
            let actualValue = actual;

            if (dimension === PoundsPerPupil) {
              forecastValue = forecast
                ? totalPupils
                  ? forecast / totalPupils
                  : undefined
                : forecast;
              actualValue = actual
                ? totalPupils
                  ? actual / totalPupils
                  : undefined
                : actual;
            }

            return {
              periodEndDate: new Date(year, 7, 31).toISOString(),
              forecast: forecastValue,
              actual: actualValue,
            };
          })
        : [],
    [data, dimension]
  );

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    const dimension =
      ForecastCategories.find((x) => x.value === event.target.value) ?? Actual;
    setDimension(dimension);
  };

  return (
    <div className="govuk-grid-row">
      <div className="govuk-grid-column-one-half">
        <h2 className="govuk-heading-m">Year-end usable reserves</h2>
      </div>
      <div className="govuk-grid-column-one-half">
        <div>
          <button
            className="govuk-button govuk-button--secondary"
            data-module="govuk-button"
            disabled={imageLoading}
            aria-disabled={imageLoading}
            onClick={() => lineChart2SeriesRef?.current?.download()}
          >
            Save{" "}
            <span className="govuk-visually-hidden">
              year-end usable reserves
            </span>{" "}
            as image
          </button>
        </div>
        <div>
          <ChartDimensions
            dimensions={ForecastCategories}
            handleChange={handleSelectChange}
            elementId="year-end-reserves"
            value={dimension.value}
            label="View chart as"
          />
        </div>
      </div>
      <div className="govuk-grid-column-full" style={{ height: 400 }}>
        {chartData.length > 0 ? (
          <LineChart
            chartName="Year-end usable reserves"
            data={chartData}
            grid
            highlightActive
            keyField="periodEndDate"
            margin={20}
            onImageLoading={setImageLoading}
            ref={lineChart2SeriesRef}
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
    </div>
  );
};