import React, { useLayoutEffect } from "react";
import { useHash } from "src/hooks/useHash";
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
} from "src/views/compare-your-trust/partials/accordion-sections";

export const ExpenditureAccordion: React.FC<{ id: string }> = ({ id }) => {
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
          <TeachingSupportStaff id={id} />
          <NonEducationalSupportStaff id={id} />
          <EducationalSupplies id={id} />
          <EducationalIct id={id} />
          <PremisesStaffServices id={id} />
          <Utilities id={id} />
          <AdministrativeSupplies id={id} />
          <CateringStaffServices id={id} />
          <OtherCosts id={id} />
        </div>
      </div>
    </div>
  );
};
