import React, {
  useCallback,
  useEffect,
  useMemo,
  useRef,
  useState,
} from "react";
import { BalanceApi, BudgetForecastReturn } from "src/services";
import { BudgetForecastData } from "./types";
import {
  Actual,
  ChartDimensions,
  ChartHandler,
  ForecastCategories,
  PoundsPerPupil,
} from "src/components";
import { BfrChart } from "./bfr-chart";
import { BfrTable } from "./bfr-table";

export const YearEnd: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(Actual);
  const [data, setData] = useState<BudgetForecastReturn[] | null>();
  const [imageLoading, setImageLoading] = useState<boolean>();
  const chartRef = useRef<ChartHandler>(null);

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
        ? data.map(
            ({
              year,
              forecast,
              actual,
              forecastTotalPupils,
              actualTotalPupils,
            }) => {
              let forecastValue = forecast;
              let actualValue = actual;

              if (dimension === PoundsPerPupil) {
                forecastValue = forecast
                  ? forecastTotalPupils
                    ? forecast / forecastTotalPupils
                    : undefined
                  : forecast;
                actualValue = actual
                  ? actualTotalPupils
                    ? actual / actualTotalPupils
                    : undefined
                  : actual;
              }

              return {
                periodEndDate: new Date(year, 7, 31).toISOString(),
                forecast: forecastValue,
                actual: actualValue,
              };
            }
          )
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
    <div className="govuk-grid-row govuk-!-margin-top-5">
      <div className="govuk-grid-column-one-half">
        <h2 className="govuk-heading-m">Year-end revenue reserves</h2>
      </div>
      <div className="govuk-grid-column-one-half">
        <div>
          <button
            className="govuk-button govuk-button--secondary"
            data-module="govuk-button"
            disabled={imageLoading}
            aria-disabled={imageLoading}
            onClick={() => chartRef?.current?.download()}
          >
            Save{" "}
            <span className="govuk-visually-hidden">
              year-end revenue reserves
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
      <BfrChart
        data={chartData}
        ref={chartRef}
        onImageLoading={setImageLoading}
      />
      <BfrTable data={data} />
    </div>
  );
};
