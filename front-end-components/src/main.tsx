import React from 'react';
import ReactDOM from 'react-dom/client';
import 'src/index.css';
import {CompareYourSchool, CompareYourWorkforce} from 'src/views'
import {CompareWorkforceElementId, CompareYourSchoolElementId} from "src/constants";

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

const compareWorkforceElement = document.getElementById(CompareWorkforceElementId);

if (compareWorkforceElement) {
    const {urn, academyYear, maintainedYear} = compareWorkforceElement.dataset;
    if (urn && academyYear && maintainedYear) {
        const root = ReactDOM.createRoot(compareWorkforceElement);

        root.render(
            <React.StrictMode>
                <CompareYourWorkforce urn={urn} maintainedYear={maintainedYear} academyYear={academyYear}/>
            </React.StrictMode>
        );
    }
}