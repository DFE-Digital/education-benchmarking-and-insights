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
} from "src/views/compare-your-costs-trust/partials/accordion-sections";

export const ExpenditureAccordion: React.FC<{ type: string; id: string }> = ({
  type,
  id,
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
          <TeachingSupportStaff id={id} type={type} />
          <NonEducationalSupportStaff id={id} type={type} />
          <EducationalSupplies id={id} type={type} />
          <EducationalIct id={id} type={type} />
          <PremisesStaffServices id={id} type={type} />
          <Utilities id={id} type={type} />
          <AdministrativeSupplies id={id} type={type} />
          <CateringStaffServices id={id} type={type} />
          <OtherCosts id={id} type={type} />
        </div>
      </div>
    </div>
  );
};
