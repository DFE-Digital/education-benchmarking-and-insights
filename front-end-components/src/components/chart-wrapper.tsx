import React, {useContext} from "react";
import HorizontalBarChart, {BarData} from "./horizontal-bar-chart/horizontal-bar-chart";
import {ChartMode, ChartModeContext} from "../chart-more";
import {ChartDimensions} from "../chart-dimensions";

const ChartWrapper: React.FC<ChartWrapperProps> = (props) => {
    const {heading, chosenSchoolName, data, fileName, chartDimensions, selectedDimension} = props
    const mode = useContext(ChartModeContext);

    const renderView = (displayMode: ChartMode) => {
        switch (displayMode) {
            case ChartMode.CHART:
                return <HorizontalBarChart data={data} chosenSchool={chosenSchoolName} xLabel={selectedDimension}
                                           heading={heading} fileName={fileName} chartDimensions={chartDimensions}/>
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
    fileName: string
    chartDimensions?: ChartDimensions
    selectedDimension: string
}
