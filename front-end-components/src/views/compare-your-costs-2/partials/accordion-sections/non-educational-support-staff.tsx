import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  CompareYourCosts2Props,
  NonEducationalSupportStaffData,
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
  NonEducationalSupportStaffExpenditure,
} from "src/services";
import { AccordionSection } from "src/composed/accordion-section";
import { useAbort } from "src/hooks/useAbort";

export const NonEducationalSupportStaff: React.FC<CompareYourCosts2Props> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<NonEducationalSupportStaffData[] | null>();
  const { abort, signal } = useAbort();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<NonEducationalSupportStaffExpenditure>(
      type,
      id,
      dimension.value,
      "NonEducationalSupportStaff",
      phase,
      customDataId,
      [signal]
    );
  }, [type, id, dimension.value, phase, customDataId, signal]);
  const { data: progressIndicators } = useProgressIndicatorsContext();

  useEffect(() => {
    getData().then((result) => {
      const merged = result
        ? result.reduce<NonEducationalSupportStaffData[]>(
            (
              acc: NonEducationalSupportStaffData[],
              curr: NonEducationalSupportStaffExpenditure
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
    });
  }, [getData, progressIndicators]);

  const tableHeadings = useMemo(
    () => [
      "School name",
      "Local Authority",
      "School type",
      "Number of pupils",
      dimension.heading,
    ],
    [dimension]
  );

  const handleDimensionChange = (value: string) => {
    abort();

    const dimension =
      CostCategories.find((x) => x.value === value) ?? PoundsPerPupil;
    setDimension(dimension);
  };

  const administrativeClericalBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.administrativeClericalStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const totalNonEducationalBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.totalNonEducationalSupportStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const auditorsCostsBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.auditorsCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const otherStaffCostsBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.otherStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const professionalServicesBarData: HorizontalBarChartWrapperData<NonEducationalSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.professionalServicesNonCurriculumCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  return (
    <AccordionSection
      charts={[
        {
          data: totalNonEducationalBarData,
          title: "Total non-educational support staff costs",
        },
        {
          data: administrativeClericalBarData,
          title: "Administrative and clerical staff costs",
        },
        {
          data: auditorsCostsBarData,
          title: "Auditors costs",
        },
        {
          data: otherStaffCostsBarData,
          title: "Other staff costs",
        },
        {
          data: professionalServicesBarData,
          title: "Professional services (non-curriculum) costs",
        },
      ]}
      dimension={dimension}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      index={2}
      showCopyImageButton
      title="Non-educational support staff and services"
    />
  );
};
