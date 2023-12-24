import React from "react";
import HorizontalBarChart, {BarData} from "./horizontal-bar-chart/horizontal-bar-chart";
import {ChartMode} from "../constants";

const ChartWrapper: React.FC<ChartWrapperProps> = ({heading, chosenSchoolName, data, mode}) => {
    const renderView = (displayMode: ChartMode) => {
        switch (displayMode) {
            case ChartMode.CHART:
                return <HorizontalBarChart data={data} chosenSchool={chosenSchoolName} xLabel='per pupil'
                                           heading={heading}/>
            case ChartMode.TABLE:
                return <>
                    <div className="govuk-grid-row">
                        <div className="govuk-grid-column-two-thirds">
                            {heading}
                        </div>
                    </div>
                    <div className="govuk-grid-row">
                        <div className="govuk-grid-column-two-thirds">
                            <p className="govuk-body">[Table goes here]</p>
                        </div>
                    </div>
                </>
            default:
                return <></>
        }
    }


    return (<>
            {renderView(mode)}
        </>
    )
};

export default ChartWrapper

export type ChartWrapperProps = {
    heading: React.ReactNode
    chosenSchoolName: string
    data: BarData
    mode: ChartMode
}
