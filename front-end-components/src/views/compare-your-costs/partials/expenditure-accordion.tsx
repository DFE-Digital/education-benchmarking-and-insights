import React from "react";
import { ExpenditureAccordionProps } from "src/views/compare-your-costs/partials";
import {
  AdministrativeSupplies,
  CateringStaffServices,
  EducationalIct,
  EducationalSupplies,
  NonEducationalSupportStaff,
  OtherCosts,
  PremisesStaffServices,
  TeachingSupportStaff,
  Utilities,
} from "src/views/compare-your-costs/partials/accordion-sections";

export const ExpenditureAccordion: React.FC<ExpenditureAccordionProps> = ({
  schools,
}) => {
  return (
    <div className="govuk-grid-row">
      <div className="govuk-grid-column-full">
        <div
          className="govuk-accordion"
          data-module="govuk-accordion"
          id="accordion"
        >
          <TeachingSupportStaff schools={schools} />
          <NonEducationalSupportStaff schools={schools} />
          <EducationalSupplies schools={schools} />
          <EducationalIct schools={schools} />
          <PremisesStaffServices schools={schools} />
          <Utilities schools={schools} />
          <AdministrativeSupplies schools={schools} />
          <CateringStaffServices schools={schools} />
          <OtherCosts schools={schools} />
        </div>
      </div>
    </div>
  );
};
