import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  CompareYourCostsProps,
  NonEducationalSupportStaffData,
} from "src/views/compare-your-costs/partials/accordion-sections/types";
import { CostCategories, PoundsPerPupil } from "src/components";
import { PhaseContext, CustomDataContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import {
  ExpenditureApi,
  NonEducationalSupportStaffExpenditure,
} from "src/services";
import { AccordionSection } from "src/composed/accordion-section";

export const NonEducationalSupportStaff: React.FC<CompareYourCostsProps> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<
    NonEducationalSupportStaffExpenditure[] | null
  >();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<NonEducationalSupportStaffExpenditure>(
      type,
      id,
      dimension.value,
      "NonEducationalSupportStaff",
      phase,
      customDataId
    );
  }, [id, dimension, type, phase, customDataId]);

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
      title="Non-educational support staff and services"
    />
  );
};
