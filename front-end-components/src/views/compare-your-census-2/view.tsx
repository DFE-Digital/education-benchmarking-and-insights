import React, { useState } from "react";
import { CompareYourCensus2ViewProps } from "src/views";
import { ChartModeChart, ChartOptions, PageActions } from "src/components";
import {
  SelectedEstablishmentContext,
  PhaseContext,
  ChartModeProvider,
  CustomDataContext,
  ProgressIndicatorsProvider,
  ShareButtonsLayoutContext,
} from "src/contexts";
import {
  AuxiliaryStaff,
  Headcount,
  NonClassroomSupport,
  SchoolWorkforce,
  SeniorLeadership,
  TeachingAssistants,
  TotalTeachers,
  TotalTeachersQualified,
} from "src/views/compare-your-census-2/partials";

export const CompareYourCensus2: React.FC<CompareYourCensus2ViewProps> = (
  props
) => {
  const { customDataId, downloadLink, id, phases, progressIndicators, type } =
    props;
  const [phase, setPhase] = useState<string | undefined>(
    phases ? phases[0] : undefined
  );

  return (
    <SelectedEstablishmentContext.Provider value={id}>
      <PhaseContext.Provider value={phase}>
        <CustomDataContext.Provider value={customDataId}>
          <ChartModeProvider initialValue={ChartModeChart}>
            <ProgressIndicatorsProvider data={progressIndicators} id={id}>
              <ShareButtonsLayoutContext.Provider value="column">
                <div className="govuk-grid-row">
                  <div className="govuk-grid-column-one-half">
                    <ChartOptions
                      phases={phases}
                      handlePhaseChange={setPhase}
                      stacked
                    />
                  </div>
                  <div className="govuk-grid-column-one-half">
                    <PageActions
                      costCodesAttr="data-cost-codes"
                      costCodesLabel="Cost category codes:"
                      downloadLink={downloadLink}
                    />
                  </div>
                </div>
                <hr className="govuk-section-break govuk-section-break--l govuk-section-break--visible govuk-!-margin-top-0" />
                <SchoolWorkforce id={id} type={type} />
                <TotalTeachers id={id} type={type} />
                <TotalTeachersQualified id={id} type={type} />
                <SeniorLeadership id={id} type={type} />
                <TeachingAssistants id={id} type={type} />
                <NonClassroomSupport id={id} type={type} />
                <AuxiliaryStaff id={id} type={type} />
                <Headcount id={id} type={type} />
              </ShareButtonsLayoutContext.Provider>
            </ProgressIndicatorsProvider>
          </ChartModeProvider>
        </CustomDataContext.Provider>
      </PhaseContext.Provider>
    </SelectedEstablishmentContext.Provider>
  );
};
