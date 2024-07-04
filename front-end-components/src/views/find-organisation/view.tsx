import { useRef } from "react";
import SchoolInput from "src/views/find-organisation/partials/school-input";
import { FindOrganisationProps } from "src/views/find-organisation";
import TrustInput from "src/views/find-organisation/partials/trust-input";
import { useGovUk } from "src/hooks/useGovUk";
import LaInput from "src/views/find-organisation/partials/la-input";

export const FindOrganisation: React.FC<FindOrganisationProps> = (props) => {
  const {
    code,
    companyNumber,
    findMethod,
    laError,
    laInput,
    schoolError,
    schoolInput,
    trustInput,
    trustError,
    urn,
  } = props;

  const formElem = useRef(null);

  useGovUk();

  return (
    <form action="" method="POST" role="search" ref={formElem}>
      <div className="govuk-form-group">
        <fieldset
          className="govuk-fieldset"
          aria-describedby="find-method-hint"
        >
          <legend className="govuk-fieldset__legend govuk-fieldset__legend--m">
            What are you looking for?
          </legend>
          <div className="govuk-radios" data-module="govuk-radios">
            <div className="govuk-radios__item">
              <input
                className="govuk-radios__input"
                id="school"
                name="findMethod"
                type="radio"
                value="school"
                defaultChecked={findMethod === "school"}
                data-aria-controls="conditional-school"
              />
              <label
                className="govuk-label govuk-radios__label"
                htmlFor="school"
              >
                School or academy
              </label>
            </div>
            <div className="govuk-radios__conditional" id="conditional-school">
              <div
                className={
                  schoolError
                    ? "govuk-form-group suggest-form-group govuk-form-group--error"
                    : "govuk-form-group suggest-form-group"
                }
              >
                <div id="school-hint" className="govuk-hint">
                  Name, address, postcode or unique reference number (URN)
                </div>
                <p id="school-error" className="govuk-error-message">
                  {schoolError}
                </p>
                <SchoolInput input={schoolInput ?? ""} urn={urn ?? ""} />
              </div>
            </div>

            <div className="govuk-radios__item">
              <input
                className="govuk-radios__input"
                id="trust"
                name="findMethod"
                type="radio"
                value="trust"
                defaultChecked={findMethod === "trust"}
                data-aria-controls="conditional-trust"
              />
              <label
                className="govuk-label govuk-radios__label"
                htmlFor="trust"
              >
                Trust
              </label>
            </div>
            <div
              className="govuk-radios__conditional govuk-radios__conditional--hidden"
              id="conditional-trust"
            >
              <div
                className={
                  trustError
                    ? "govuk-form-group suggest-form-group govuk-form-group--error"
                    : "govuk-form-group suggest-form-group"
                }
              >
                <div id="trust-hint" className="govuk-hint">
                  Name or company number
                </div>
                <p id="trust-error" className="govuk-error-message">
                  {trustError}
                </p>
                <TrustInput
                  input={trustInput ?? ""}
                  companyNumber={companyNumber ?? ""}
                />
              </div>
            </div>

            <div className="govuk-radios__item">
              <input
                className="govuk-radios__input"
                id="local-authority"
                name="findMethod"
                type="radio"
                value="local-authority"
                defaultChecked={findMethod === "local-authority"}
                data-aria-controls="conditional-local-authority"
              />
              <label
                className="govuk-label govuk-radios__label"
                htmlFor="local-authority"
              >
                Local authority
              </label>
            </div>
            <div
              className="govuk-radios__conditional govuk-radios__conditional--hidden"
              id="conditional-local-authority"
            >
              <div
                className={
                  laError
                    ? "govuk-form-group suggest-form-group govuk-form-group--error"
                    : "govuk-form-group suggest-form-group"
                }
              >
                <div id="local-authority-hint" className="govuk-hint">
                  Name or 3 digit code
                </div>
                <p id="local-authority-error" className="govuk-error-message">
                  {laError}
                </p>
                <LaInput input={laInput ?? ""} code={code ?? ""} />
              </div>
            </div>
          </div>
        </fieldset>
      </div>
      <button id="search-btn" type="submit" className="govuk-button">
        Continue
      </button>
    </form>
  );
};
