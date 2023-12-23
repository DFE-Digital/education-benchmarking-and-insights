import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import CompareYourSchool, {CompareYourSchoolElementId} from './views/compare-your-school'

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