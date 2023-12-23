import React, {useLayoutEffect, useCallback, useState, useEffect} from 'react';
import TotalExpenditure, {ExpenditureData} from "../components/total-expenditure";
import ExpenditureAccordion from "../components/expenditure-accordion";
// @ts-ignore
import {initAll} from 'govuk-frontend'
import SchoolApi, {ExpenditureResult} from "../services/school-api";

type ViewProps = {
    urn: string;
};

const CompareYourSchool: React.FC<ViewProps> = ({urn}) => {
    const [expenditureData, setExpenditureData] = useState<ExpenditureResult>();

    useLayoutEffect(() => {
        initAll();
    }, []);

    const getExpenditure = useCallback(async () => {
        const data = await SchoolApi.getSchoolExpenditure(urn);
        setExpenditureData(data);
    }, [urn])

    useEffect(() => {
        getExpenditure()
    }, [getExpenditure])

    return (
        <div>
            <div className="govuk-grid-row">
                <div className="govuk-grid-column-two-thirds">
                    <p className="govuk-body">
                        The data below is from the latest year available, For maintained schools this is [insert text],
                        academies for [insert text]
                    </p>
                </div>
                <div className="govuk-grid-column-one-third">
                    <p className="govuk-body">[View as table]</p>
                </div>
            </div>
            <TotalExpenditure urn={urn} schools={expenditureData ? expenditureData.results : new Array<ExpenditureData>() }/>
            <ExpenditureAccordion/>
        </div>
    )
};

export default CompareYourSchool;
export const CompareYourSchoolElementId = 'compare-your-school';