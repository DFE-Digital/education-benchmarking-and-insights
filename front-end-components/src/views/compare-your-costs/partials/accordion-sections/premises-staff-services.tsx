import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  CompareYourCostsProps,
  PremisesStaffServicesData,
} from "src/views/compare-your-costs/partials/accordion-sections/types";
import { PoundsPerMetreSq, PremisesCategories } from "src/components";
import { PhaseContext, CustomDataContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import { ExpenditureApi, PremisesStaffServicesExpenditure } from "src/services";
import { AccordionSection } from "src/composed/accordion-section";

export const PremisesStaffServices: React.FC<CompareYourCostsProps> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PoundsPerMetreSq);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<PremisesStaffServicesExpenditure[] | null>();
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<PremisesStaffServicesExpenditure>(
      type,
      id,
      dimension.value,
      "PremisesStaffServices",
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
      PremisesCategories.find((x) => x.value === value) ?? PoundsPerMetreSq;
    setDimension(dimension);
  };

  const totalPremisesStaffServiceCostsBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.totalPremisesStaffServiceCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const cleaningCaretakingBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.cleaningCaretakingCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const maintenanceBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.maintenancePremisesCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const otherOccupationBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.otherOccupationCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const premisesStaffBarData: HorizontalBarChartWrapperData<PremisesStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.premisesStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  return (
    <AccordionSection
      charts={[
        {
          data: totalPremisesStaffServiceCostsBarData,
          title: "Total premises staff and service costs",
          dimensions: PremisesCategories,
        },
        {
          data: cleaningCaretakingBarData,
          title: "Cleaning and caretaking costs",
        },
        { data: maintenanceBarData, title: "Maintenance of premises costs" },
        { data: otherOccupationBarData, title: "Other occupation costs" },
        { data: premisesStaffBarData, title: "Premises staff costs" },
      ]}
      dimension={dimension}
      handleDimensionChange={handleDimensionChange}
      hasNoData={data?.length === 0}
      index={5}
      title="Premises staff and services"
    />
  );
};
