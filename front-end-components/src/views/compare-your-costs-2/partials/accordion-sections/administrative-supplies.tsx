import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  AdministrativeSuppliesData,
  CompareYourCosts2Props,
} from "src/views/compare-your-costs-2/partials/accordion-sections/types";
import { CostCategories, PoundsPerPupil } from "src/components";
import {
  PhaseContext,
  CustomDataContext,
  useProgressIndicatorsContext,
} from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import {
  ExpenditureApi,
  AdministrativeSuppliesExpenditure,
} from "src/services";
import { AccordionSection } from "src/composed/accordion-section";
import { useAbort } from "src/hooks/useAbort";

export const AdministrativeSupplies: React.FC<CompareYourCosts2Props> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [expenditureData, setExpenditureData] = useState<
    AdministrativeSuppliesExpenditure[] | null
  >();
  const [data, setData] = useState<AdministrativeSuppliesData[] | null>();
  const { abort, signal } = useAbort();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<AdministrativeSuppliesExpenditure>(
      type,
      id,
      dimension.value,
      "AdministrationSupplies",
      phase,
      customDataId,
      [signal]
    );
  }, [type, id, dimension.value, phase, customDataId, signal]);
  const { progressIndicators } = useProgressIndicatorsContext();

  useEffect(() => {
    getData().then((result) => {
      setExpenditureData(result);
    });
  }, [getData]);

  useEffect(() => {
    const merged = expenditureData
      ? expenditureData.reduce<AdministrativeSuppliesData[]>(
          (
            acc: AdministrativeSuppliesData[],
            curr: AdministrativeSuppliesExpenditure
          ) => {
            acc.push({
              ...curr,
              progressBanding: progressIndicators[curr.urn],
            });
            return acc;
          },
          []
        )
      : null;

    setData(merged);
  }, [expenditureData, progressIndicators]);

  const handleDimensionChange = (value: string) => {
    abort();

    const dimension =
      CostCategories.find((x) => x.value === value) ?? PoundsPerPupil;
    setDimension(dimension);
  };

  const administrativeSuppliesBarData: HorizontalBarChartWrapperData<AdministrativeSuppliesData> =
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
              value: school.administrativeSuppliesNonEducationalCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, dimension]);

  return (
    <AccordionSection
      charts={[
        {
          data: administrativeSuppliesBarData,
          title: "Administrative supplies (Non-educational)",
        },
      ]}
      dimension={dimension}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      index={7}
      showCopyImageButton
      title="Administrative supplies"
    />
  );
};
