import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { TotalExpenditureData } from "src/views/compare-your-costs/partials";
import {
  ChartDimensionContext,
  CustomDataContext,
  PhaseContext,
} from "src/contexts";
import {
  CostCategories,
  PoundsPerPupil,
  ChartDimensions,
  PercentageExpenditure,
} from "src/components";
import {
  HorizontalBarChartWrapper,
  HorizontalBarChartWrapperData,
} from "src/composed/horizontal-bar-chart-wrapper";
import { ExpenditureApi, TotalExpenditureExpenditure } from "src/services";
import { CompareYourCostsProps } from "./accordion-sections/types";
import { ErrorBanner } from "src/components/error-banner";

export const TotalExpenditure: React.FC<CompareYourCostsProps> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<TotalExpenditureExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<TotalExpenditureExpenditure>(
      type,
      id,
      dimension.value,
      "TotalExpenditure",
      phase,
      customDataId
    );
  }, [id, dimension, type, phase, customDataId]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const chartData: HorizontalBarChartWrapperData<TotalExpenditureData> =
    useMemo(() => {
      const tableHeadings = [
        "School name",
        "Local Authority",
        "School type",
        "Number of pupils",
        dimension.heading,
      ];

      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.totalExpenditure,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [dimension, data]);

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    const dimension =
      CostCategories.find((x) => x.value === event.target.value) ??
      PoundsPerPupil;
    setDimension(dimension);
  };

  const hasNoData = data?.length === 0;

  return hasNoData ? (
    <ErrorBanner
      isRendered={hasNoData}
      message="There isn't enough information available to create a set of similar schools."
    />
  ) : (
    <ChartDimensionContext.Provider value={dimension}>
      <HorizontalBarChartWrapper data={chartData} chartName="total expenditure">
        <h2 className="govuk-heading-m">Total expenditure</h2>
        <ChartDimensions
          dimensions={CostCategories.filter(function (category) {
            return category !== PercentageExpenditure;
          })}
          handleChange={handleSelectChange}
          elementId="total-expenditure"
          value={dimension.value}
        />
      </HorizontalBarChartWrapper>
    </ChartDimensionContext.Provider>
  );
};
