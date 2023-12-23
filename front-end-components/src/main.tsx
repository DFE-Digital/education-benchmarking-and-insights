import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import CompareYourSchool, {CompareYourSchoolElementId} from './views/compare-your-school'

const compareYourSchoolElement = document.getElementById(CompareYourSchoolElementId);

if (compareYourSchoolElement) {
    const {urn} = compareYourSchoolElement.dataset;
    if( urn != undefined){
        const root = ReactDOM.createRoot(compareYourSchoolElement);

        root.render(
            <React.StrictMode>
                <CompareYourSchool urn={urn}/>
            </React.StrictMode>
        );
    }
}