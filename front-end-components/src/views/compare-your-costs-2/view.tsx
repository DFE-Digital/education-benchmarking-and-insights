import React, { useState } from "react";
import {
  TotalExpenditure,
  ExpenditureAccordion,
} from "src/views/compare-your-costs-2/partials";
import { CompareYourCosts2ViewProps } from "src/views/compare-your-costs-2";
import { ChartModeChart } from "src/components";
import {
  SelectedEstablishmentContext,
  CustomDataContext,
  ChartModeProvider,
  ShareButtonsLayoutContext,
  SuppressNegativeOrZeroContext,
  CostCodeMapProvider,
  ProgressIndicatorsProvider,
} from "src/contexts";
import { useGovUk } from "src/hooks/useGovUk";
import { ChartOptionsProgress } from "src/components/chart-options-progress";
import { PageActions } from "src/components/page-actions";

export const CompareYourCosts2: React.FC<CompareYourCosts2ViewProps> = ({
  costCodeMap,
  customDataId,
  downloadLink,
  saveClassName,
  saveFileName,
  saveModalPortalId,
  saveTitleAttr,
  id,
  suppressNegativeOrZero,
  tags,
  type,
  progressIndicators,
}) => {
  const [fetching, setFetching] = useState(true);
  const handleFetching = (fetching: boolean) => {
    setFetching(fetching);
  };

  const message = "Only displaying schools with positive expenditure.";

  useGovUk();

  return (
    <SelectedEstablishmentContext.Provider value={id}>
      <CustomDataContext.Provider value={customDataId}>
        <SuppressNegativeOrZeroContext.Provider
          value={{ suppressNegativeOrZero, message }}
        >
          <CostCodeMapProvider costCodeMap={costCodeMap} tags={tags}>
            <ChartModeProvider initialValue={ChartModeChart}>
              <ProgressIndicatorsProvider data={progressIndicators} id={id}>
                <ShareButtonsLayoutContext.Provider value="column">
                  <div className="govuk-grid-row">
                    <div className="govuk-grid-column-one-half">
                      <ChartOptionsProgress />
                    </div>
                    <div className="govuk-grid-column-one-half">
                      <PageActions
                        downloadLink={downloadLink}
                        saveClassName={saveClassName}
                        saveDisabled={fetching}
                        saveFileName={saveFileName}
                        saveModalPortalId={saveModalPortalId}
                        saveTitleAttr={saveTitleAttr}
                      />
                    </div>
                  </div>
                  <hr className="govuk-section-break govuk-section-break--l govuk-section-break--visible govuk-!-margin-top-0" />
                  <TotalExpenditure
                    id={id}
                    type={type}
                    onFetching={handleFetching}
                  />
                  <ExpenditureAccordion id={id} type={type} />
                </ShareButtonsLayoutContext.Provider>
              </ProgressIndicatorsProvider>
            </ChartModeProvider>
          </CostCodeMapProvider>
        </SuppressNegativeOrZeroContext.Provider>
      </CustomDataContext.Provider>
    </SelectedEstablishmentContext.Provider>
  );
};
