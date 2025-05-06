import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  CompareYourCostsProps,
  TeachingSupportStaffData,
} from "src/views/compare-your-costs/partials/accordion-sections/types";
import { CostCategories, PoundsPerPupil } from "src/components";
import { PhaseContext, CustomDataContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { ExpenditureApi, TeachingSupportStaffExpenditure } from "src/services";
import { AccordionSection } from "src/composed/accordion-section";
import { useAbort } from "src/hooks/useAbort";

export const TeachingSupportStaff: React.FC<CompareYourCostsProps> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<TeachingSupportStaffExpenditure[] | null>();
  const { abort, signal } = useAbort();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<TeachingSupportStaffExpenditure>(
      type,
      id,
      dimension.value,
      "TeachingTeachingSupportStaff",
      phase,
      customDataId,
      [signal]
    );
  }, [type, id, dimension.value, phase, customDataId, signal]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

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

  const totalTeachingBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.totalTeachingSupportStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const teachingStaffBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.teachingStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const supplyTeachingBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.supplyTeachingStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const educationalConsultancyBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.educationalConsultancyCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const educationSupportStaffBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.educationSupportStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const agencySupplyBarData: HorizontalBarChartWrapperData<TeachingSupportStaffData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.agencySupplyTeachingStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  return (
    <AccordionSection
      charts={[
        {
          data: totalTeachingBarData,
          title: "Total teaching and teaching support staff costs",
        },
        {
          data: teachingStaffBarData,
          title: "Teaching staff costs",
        },
        {
          data: supplyTeachingBarData,
          title: "Supply teaching staff costs",
        },
        {
          data: educationalConsultancyBarData,
          title: "Educational consultancy costs",
        },
        {
          data: educationSupportStaffBarData,
          title: "Educational support staff costs",
        },
        {
          data: agencySupplyBarData,
          title: "Agency supply teaching staff costs",
        },
      ]}
      dimension={dimension}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      index={1}
      showCopyImageButton
      title="Teaching and teaching support staff"
    />
  );
};
