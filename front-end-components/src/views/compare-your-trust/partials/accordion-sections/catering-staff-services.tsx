import React, { useCallback, useEffect, useMemo, useState } from "react";
import { CateringStaffServicesData } from "src/views/compare-your-trust/partials/accordion-sections/types";
import {
  CostCategories,
  PoundsPerPupil,
  ChartDimensions,
} from "src/components";
import { useCentralServicesBreakdownContext } from "src/contexts";
import { HorizontalBarChartWrapperData } from "src/composed/horizontal-bar-chart-wrapper";
import {
  ExpenditureApi,
  CateringStaffServicesTrustExpenditure,
  TotalCateringCostsField,
} from "src/services";
import {
  BreakdownExclude,
  BreakdownInclude,
} from "src/components/central-services-breakdown";
import { TotalCateringCostsType } from "src/components/total-catering-costs-type";
import { AccordionSection } from "src/composed/accordion-section";

export const CateringStaffServices: React.FC<{
  id: string;
}> = ({ id }) => {
  const [dimension, setDimension] = useState(PoundsPerPupil);
  const { breakdown } = useCentralServicesBreakdownContext(true);
  const [data, setData] = useState<
    CateringStaffServicesTrustExpenditure[] | null
  >();
  const [totalCateringCostsField, setTotalCateringCostsField] =
    useState<TotalCateringCostsField>("totalGrossCateringCosts");
  const getData = useCallback(async () => {
    setData(null);
    return await ExpenditureApi.trust<CateringStaffServicesTrustExpenditure>(
      id,
      dimension.value,
      "CateringStaffServices",
      breakdown === BreakdownExclude
    );
  }, [id, dimension, breakdown]);

  useEffect(() => {
    getData().then((result) => {
      setData(result);
    });
  }, [getData]);

  const tableHeadings = useMemo(() => {
    const headings = ["Trust name", `Total ${dimension.heading}`];
    if (breakdown === BreakdownInclude) {
      headings.push(
        `School ${dimension.heading}`,
        `Central ${dimension.heading}`
      );
    }
    return headings;
  }, [dimension, breakdown]);

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
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue:
                    (totalCateringCostsField == "totalGrossCateringCosts"
                      ? trust.totalGrossCateringCosts
                      : trust.totalNetCateringCosts) ?? 0,
                  schoolValue:
                    (totalCateringCostsField == "totalGrossCateringCosts"
                      ? trust.schoolTotalGrossCateringCosts
                      : trust.schoolTotalNetCateringCosts) ?? 0,
                  centralValue:
                    (totalCateringCostsField == "totalGrossCateringCosts"
                      ? trust.centralTotalGrossCateringCosts
                      : trust.centralTotalNetCateringCosts) ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings, totalCateringCostsField]);

  const cateringStaffBarData: HorizontalBarChartWrapperData<CateringStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.cateringStaffCosts ?? 0,
                  schoolValue: trust.schoolCateringStaffCosts ?? 0,
                  centralValue: trust.centralCateringStaffCosts ?? 0,
                };
              })
            : [],
        tableHeadings,
      };
    }, [data, tableHeadings]);

  const cateringSuppliesBarData: HorizontalBarChartWrapperData<CateringStaffServicesData> =
    useMemo(() => {
      return {
        dataPoints:
          data && Array.isArray(data)
            ? data.map((trust) => {
                return {
                  ...trust,
                  totalValue: trust.cateringSuppliesCosts ?? 0,
                  schoolValue: trust.schoolCateringSuppliesCosts ?? 0,
                  centralValue: trust.centralCateringSuppliesCosts ?? 0,
                };
              })
            : [],
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
