import React, {useRef} from 'react';
import ReactDOM from 'react-dom';
import OrganisationInput from "./organisation-input";

function FindOrganisationViewV2(props) {
    let {
        identifier,
        kind,
        organisationInput,
        organisationError
    } = props;

    const formElem = useRef(null);

    function submitForm() {
        setTimeout(() => formElem.current.submit());
    }

    return <form action="" method="POST" role="search" ref={formElem}>
        <div className="govuk-form-group">
            <fieldset className="govuk-fieldset" aria-describedby="find-method-hint">
                <legend className="govuk-fieldset__legend govuk-fieldset__legend--m">
                    Name, address or [URN or Companies House number]
                </legend>
                <div
                    className={organisationError ? "govuk-form-group suggest-form-group govuk-form-group--error" : "govuk-form-group suggest-form-group"}>
                    <p id="school-error" className="govuk-error-message">{organisationError}</p>
                    <OrganisationInput input={organisationInput} identifier={identifier} kind={kind} submitForm={submitForm}/>
                </div>
            </fieldset>
        </div>
        <button id="search-btn" type="submit" className="govuk-button">Continue</button>
    </form>;
}

const element = document.getElementById('find-org-view-v2');
element && ReactDOM.render(<FindOrganisationViewV2 {...element.dataset} />, element);