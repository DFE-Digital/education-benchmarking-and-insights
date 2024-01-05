import React from "react";
import {WorkforceBenchmark} from "../../services/school-api";

const WorkforceChart: React.FC<WorkforceChartProps> = ({schools}) => {

    return (

        <div className="govuk-grid-row">
            <div className="govuk-grid-column-full">
                <div className="govuk-workforce-chart" data-module="govuk-workforce-chart" id="workforce-chart-default">
                <FinanceType schools={schools}/>
                <LocalAuthority schools={schools}/>
                </div>
            </div>
        </div>
    )
}

export default WorkforceChart

export type WorkforceChartProps = {
    schools: WorkforceBenchmark[]
}