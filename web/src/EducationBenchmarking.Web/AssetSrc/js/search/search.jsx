import React, { useRef } from 'react';
import ReactDOM from 'react-dom';

import SearchBox from "./searchbox";

function Search(props) {
    let {
        search,
        label,
        error
    } = props;

    const formElem = useRef(null);
    
    function submitForm() {
        setTimeout(() => formElem.current.submit());
    }
    
    return <form action="" method="POST" role="search" ref={formElem}>
        <div
            className={error ? "govuk-form-group search-form-group govuk-form-group--error" : "govuk-form-group search-form-group"}>
            <label id="search-label" className="govuk-label" htmlFor="search-input">{label}</label>
            <p id="search-error" className="govuk-error-message">
                {error}
            </p>
            <SearchBox search={search} error={error} submitForm={submitForm}/>
        </div>
    </form>;
}

const searchFormElem = document.getElementById('search');
searchFormElem && ReactDOM.render(<Search {...searchFormElem.dataset} />, searchFormElem);