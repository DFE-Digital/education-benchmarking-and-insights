import React, { useLayoutEffect } from "react";
import { useHash } from "src/hooks/useHash";
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
  const [hash] = useHash();

  useLayoutEffect(() => {
    if (!hash) {
      return;
    }

    // scroll section into view if expanded accordion contents have been rendered out of position
    document
      .querySelector(`.govuk-accordion__section${hash}`)
      ?.scrollIntoView();
  }, [hash]);

  return (
    <div className="govuk-grid-row">
      <div className="govuk-grid-column-full">
        <div
          className="govuk-accordion"
          data-module="govuk-accordion"
          data-remember-expanded="false"
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
