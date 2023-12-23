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
            <TotalExpenditure/>
            <ExpenditureAccordion/>
        </div>
    )
};

export default CompareYourSchool;
export const CompareYourSchoolElementId = 'compare-your-school';