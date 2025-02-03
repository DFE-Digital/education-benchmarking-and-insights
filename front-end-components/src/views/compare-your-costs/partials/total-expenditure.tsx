import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import { TotalExpenditureData } from "src/views/compare-your-costs/partials";
import { CustomDataContext, PhaseContext } from "src/contexts";
import {
  CostCategories,
  PoundsPerPupil,
  PercentageExpenditure,
} from "src/components";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { ExpenditureApi, TotalExpenditureExpenditure } from "src/services";
import { CompareYourCostsProps } from "./accordion-sections/types";
import { DimensionedChart } from "src/composed/dimensioned-chart";

export const TotalExpenditure: React.FC<CompareYourCostsProps> = ({
  id,
  onFetching,
  type,
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

  useEffect(() => {
    if (onFetching) {
      onFetching(!data);
    }
  }, [data, onFetching]);

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

  const handleDimensionChange = (value: string) => {
    const dimension =
      CostCategories.find((x) => x.value === value) ?? PoundsPerPupil;
    setDimension(dimension);
  };

  return (
    <DimensionedChart
      charts={[{ data: chartData, title: "Total expenditure" }]}
      dimension={dimension}
      dimensions={CostCategories.filter(function (category) {
        return category !== PercentageExpenditure;
      })}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      showCopyImageButton
      topLevel
    />
  );
};
