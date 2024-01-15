import React from 'react';
import ReactDOM from 'react-dom/client';
import 'src/index.css';
import {CompareYourSchool} from 'src/views'
import {CompareYourSchoolElementId} from "src/constants";

const compareYourSchoolElement = document.getElementById(CompareYourSchoolElementId);

if (compareYourSchoolElement) {
    const {urn, academyYear, maintainedYear} = compareYourSchoolElement.dataset;
    if (urn && academyYear && maintainedYear) {
        const root = ReactDOM.createRoot(compareYourSchoolElement);

        root.render(
            <React.StrictMode>
                <CompareYourSchool urn={urn} maintainedYear={maintainedYear} academyYear={academyYear}/>
            </React.StrictMode>
        );
    }
}