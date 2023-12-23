import React, {useLayoutEffect, useCallback, useState, useEffect} from 'react';
import TotalExpenditure from "../components/school-expenditure/total-expenditure.tsx";
import ExpenditureAccordion from "../components/school-expenditure/expenditure-accordion.tsx";
import SchoolApi, {ExpenditureResult, SchoolExpenditure} from "../services/school-api";

// @ts-ignore
import {initAll} from 'govuk-frontend'

type CompareYourSchoolViewProps = {
    urn: string
    academyYear: string
    maintainedYear: string
};

const CompareYourSchool: React.FC<CompareYourSchoolViewProps> = ({urn, academyYear, maintainedYear}) => {
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
                        The data below is from the latest year available, For maintained schools this is {maintainedYear},
                        academies for {academyYear}
                    </p>
                </div>
                <div className="govuk-grid-column-one-third">
                    <p className="govuk-body">[View as table]</p>
                </div>
            </div>
            <TotalExpenditure urn={urn}
                              schools={expenditureData ? expenditureData.results : new Array<SchoolExpenditure>()}/>
            <ExpenditureAccordion urn={urn}
                                  schools={expenditureData ? expenditureData.results : new Array<SchoolExpenditure>()}/>
        </div>
    )
};

export default CompareYourSchool;
export const CompareYourSchoolElementId = 'compare-your-school';