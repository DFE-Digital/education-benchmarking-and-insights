import React, { useRef } from 'react';
import ReactDOM from 'react-dom';
import TrustInput from "./trustInput";
import SchoolInput from "./schoolInput";

function FindOrganisationView(props) {
    let {
        findMethod,
        schoolInput,
        schoolError,
        trustInput,
        trustError,
        urn,
        companyNumber
    } = props;

    const formElem = useRef(null);

    function submitForm() {
        setTimeout(() => formElem.current.submit());
    }

    return <form action="" method="POST" role="search" ref={formElem}>
        <div className="govuk-form-group">
            <fieldset className="govuk-fieldset" aria-describedby="find-method-hint">
                <legend className="govuk-fieldset__legend govuk-fieldset__legend--m">
                    Select an organisation type
                </legend>
                <div className="govuk-radios" data-module="govuk-radios">
                    <div className="govuk-radios__item">
                        <input className="govuk-radios__input" id="school" name="findMethod" type="radio" value="school"
                               defaultChecked={findMethod === "school"} data-aria-controls="conditional-school"/>
                        <label className="govuk-label govuk-radios__label" htmlFor="school">
                            Academy or Local Authority maintained school
                        </label>
                    </div>
                    <div className="govuk-radios__conditional" id="conditional-school">
                        <div
                            className={schoolError ? "govuk-form-group suggest-form-group govuk-form-group--error" : "govuk-form-group suggest-form-group"}>
                            <div id="school-hint" className="govuk-hint">
                                Enter a name, URN or address
                            </div>
                            <p id="school-error" className="govuk-error-message">{schoolError}</p>
                            <SchoolInput input={schoolInput} urn={urn} submitForm={submitForm}/>
                        </div>
                    </div>
                    <div className="govuk-radios__item">
                        <input className="govuk-radios__input" id="trust" name="findMethod" type="radio" value="trust"
                               defaultChecked={findMethod === "trust"} data-aria-controls="conditional-trust"/>
                        <label className="govuk-label govuk-radios__label" htmlFor="trust">
                            Single or Multi-Academy Trust
                        </label>
                    </div>
                    <div className="govuk-radios__conditional govuk-radios__conditional--hidden"
                         id="conditional-trust">
                        <div
                            className={trustError ? "govuk-form-group suggest-form-group govuk-form-group--error" : "govuk-form-group suggest-form-group"}>
                            <div id="trust-hint" className="govuk-hint">
                                Enter a name or company number
                            </div>
                            <p id="trust-error" className="govuk-error-message">{trustError}</p>
                            <TrustInput input={trustInput} companyNumber={companyNumber} submitForm={submitForm}/>
                        </div>
                    </div>
                </div>
            </fieldset>
        </div>
        <button id="search-btn" type="submit" className="govuk-button">Continue</button>
    </form>;
}

const findOrganisationViewElement = document.getElementById('find-organisation-view');
findOrganisationViewElement && ReactDOM.render(
    <FindOrganisationView {...findOrganisationViewElement.dataset} />, findOrganisationViewElement);