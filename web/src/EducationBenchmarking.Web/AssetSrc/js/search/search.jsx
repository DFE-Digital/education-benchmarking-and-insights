import React, { useRef } from 'react';
import ReactDOM from 'react-dom';

import SearchBox from "./searchbox";

function Search(props) {
    let {
        search,
        label
    } = props;

    const formElem = useRef(null);
    
    function submitForm() {
        setTimeout(() => formElem.current.submit());
    }
    
    return <form action="" method="POST" role="search" ref={formElem}>
        <div className="govuk-form-group search-form-group">
            <label id="search-label" className="govuk-label" htmlFor="search-input">{label}</label>
            <SearchBox search={search} submitForm={submitForm} />
        </div>
    </form>;
}

const searchFormElem = document.getElementById('search');
searchFormElem && ReactDOM.render(<Search {...searchFormElem.dataset} />, searchFormElem);