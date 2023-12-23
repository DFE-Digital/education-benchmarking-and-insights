import React, { useLayoutEffect } from 'react';
import TotalExpenditure from "../components/total-expenditure";
import ExpenditureAccordion from "../components/expenditure-accordion";
// @ts-ignore
import {initAll} from 'govuk-frontend'

type ViewProps = {
    urn: string;
};

const CompareYourSchool: React.FC<ViewProps> = () => {
    useLayoutEffect(() => {
        initAll();
    }, []);

    return (
        <div>
            <div className="govuk-grid-row">
                <div className="govuk-grid-column-two-thirds">
                    <p className="govuk-body">
                        The data below is from the latest year available, For maintained schools this is [insert text], academies for [insert text]
                    </p>
                </div>
                <div className="govuk-grid-column-one-third">
                    <p className="govuk-body">[View as table]</p>
                </div>
            </div>
            <TotalExpenditure/>
            <ExpenditureAccordion/>
        </div>
    )
};

export default CompareYourSchool;
export const CompareYourSchoolElementId = 'compare-your-school';