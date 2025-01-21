import React, {
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";
import {
  CateringStaffServicesData,
  CompareYourCostsProps,
} from "src/views/compare-your-costs/partials/accordion-sections/types";
import {
  CostCategories,
  PoundsPerPupil,
  ChartDimensions,
} from "src/components";
import { CustomDataContext, PhaseContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import {
  ExpenditureApi,
  CateringStaffServicesExpenditure,
  TotalCateringCostsField,
} from "src/services";
import { TotalCateringCostsType } from "src/components/total-catering-costs-type";
import { AccordionSection } from "./accordion-section";

export const CateringStaffServices: React.FC<CompareYourCostsProps> = ({
  type,
  id,
}) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const phase = useContext(PhaseContext);
  const customDataId = useContext(CustomDataContext);
  const [data, setData] = useState<CateringStaffServicesExpenditure[] | null>();
  const [totalCateringCostsField, setTotalCateringCostsField] =
    useState<TotalCateringCostsField>("totalGrossCateringCosts");
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.query<CateringStaffServicesExpenditure>(
      type,
      id,
      dimension.value,
      "CateringStaffServices",
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

  const handleSelectChange: React.ChangeEventHandler<HTMLSelectElement> = (
    event
  ) => {
    const dimension =
      CostCategories.find((x) => x.value === event.target.value) ??
      PoundsPerPupil;
    setDimension(dimension);
  };

  const totalCateringBarData: HorizontalBarChartWrapperData<CateringStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value:
                (totalCateringCostsField == "totalGrossCateringCosts"
                  ? school.totalGrossCateringCosts
                  : school.totalNetCateringCosts) ?? 0,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings, totalCateringCostsField]);

  const cateringStaffBarData: HorizontalBarChartWrapperData<CateringStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.cateringStaffCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const cateringSuppliesBarData: HorizontalBarChartWrapperData<CateringStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data?.map((school) => {
            return {
              ...school,
              value: school.cateringSuppliesCosts,
            };
          }) ?? [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  return (
    <AccordionSection
      charts={[
        {
          data: totalCateringBarData,
          title: `Total catering costs (${totalCateringCostsField === "totalGrossCateringCosts" ? "gross" : "net"})`,
        },
        {
          data: cateringStaffBarData,
          title: "Catering staff costs",
        },
        {
          data: cateringSuppliesBarData,
          title: "Catering supplies costs",
        },
      ]}
      dimension={dimension}
      hasNoData={data?.length === 0}
      index={8}
      options={
        <div className="govuk-grid-row">
          <div className="govuk-grid-column-one-half">
            <ChartDimensions
              dimensions={CostCategories}
              handleChange={handleSelectChange}
              elementId="total-catering-costs"
              value={dimension.value}
            />
          </div>
          <div className="govuk-grid-column-one-half">
            <TotalCateringCostsType
              field={totalCateringCostsField}
              onChange={setTotalCateringCostsField}
            />
          </div>
        </div>
      }
      title="Catering staff and supplies"
    />
  );
};
