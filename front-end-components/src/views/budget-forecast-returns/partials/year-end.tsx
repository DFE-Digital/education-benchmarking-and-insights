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
import {
  fullValueFormatter,
  shortValueFormatter,
} from "src/components/charts/utils";
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
    <div className="govuk-grid-row govuk-!-margin-top-5">
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
      <div className="govuk-grid-column-full govuk-!-margin-top-5">
        <h2 className="govuk-heading-m">
          Year-end balances and forecasting variance
        </h2>
        <p className="govuk-body">
          The analysis in the table indicates forecasting accuracy. To assess
          the accuracy of your forecasting, the forecasted revenue reserve
          balances from the budget forecast return are compared with the actual
          closing balances from the accounts return for the same period.
        </p>
        <details className="govuk-details">
          <summary className="govuk-details__summary">
            <span className="govuk-details__summary-text">
              Help with variance status
            </span>
          </summary>
          <div className="govuk-details__text">
            <p className="govuk-body">
              We compare the most recent forecast for any given period. You'll
              see a variance status indicated whether you're forecasting is
              under or over.
            </p>
            <p className="govuk-body">
              The thresholds for the variance between AR and BFR are:
            </p>
            <ul className="govuk-list">
              <li>Below -5%: AR below forecast</li>
              <li>Between -5% and 5%: stable forecast</li>
              <li>Between 5% and 10%: AR above forecast</li>
              <li>Above 10%: AR significantly above forecast</li>
            </ul>
          </div>
        </details>
        {data && Array.isArray(data) ? (
          <table className="govuk-table">
            <thead className="govuk-table__head">
              <tr className="govuk-table__row">
                <th className="govuk-table__header">Period end date</th>
                <th className="govuk-table__header">Forecast reserves</th>
                <th className="govuk-table__header">Actual reserves</th>
                <th className="govuk-table__header">Difference</th>
                <th className="govuk-table__header">Variance %</th>
                <th className="govuk-table__header">Variance status</th>
              </tr>
            </thead>
            <tbody className="govuk-table__body">
              {data.map((item) => {
                let status = "";
                if (item.percentVariance !== undefined) {
                  if (item.percentVariance < -5) {
                    status = "AR below forecast";
                  }
                  if (item.percentVariance >= -5 && item.percentVariance < 5) {
                    status = "Stable forecast";
                  }
                  if (item.percentVariance >= 5 && item.percentVariance < 10) {
                    status = "AR above forecast";
                  }
                  if (item.percentVariance >= 10) {
                    status = "AR significantly above forecast";
                  }
                }
                return (
                  <tr className="govuk-table__row">
                    <td className="govuk-table__cell">
                      {format(new Date(item.year, 7, 31), "d MMM yyyy")}
                    </td>
                    <td className="govuk-table__cell">
                      {item.forecast &&
                        fullValueFormatter(item.forecast, {
                          valueUnit: "currency",
                        })}
                    </td>
                    <td className="govuk-table__cell">
                      {item.actual &&
                        fullValueFormatter(item.actual, {
                          valueUnit: "currency",
                        })}
                    </td>
                    <td className="govuk-table__cell">
                      {item.forecast &&
                        item.actual &&
                        fullValueFormatter(
                          Math.abs(item.forecast - item.actual),
                          {
                            valueUnit: "currency",
                          }
                        )}
                    </td>
                    <td className="govuk-table__cell">
                      {item.percentVariance &&
                        shortValueFormatter(item.percentVariance, {
                          valueUnit: "%",
                        })}
                    </td>
                    <td className="govuk-table__cell">{status}</td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        ) : (
          <Loading />
        )}
      </div>
    </div>
  );
};
