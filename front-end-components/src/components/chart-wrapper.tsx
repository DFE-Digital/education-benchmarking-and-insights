import React, {useContext} from "react";
import HorizontalBarChart from "./horizontal-bar-chart/horizontal-bar-chart";
import {ChartMode} from "../chart-mode";
import TableChart from "./table-chart/table-chart";
import {ChartModeContext} from "../contexts";
import {ChartWrapperProps} from "../types";

const ChartWrapper: React.FC<ChartWrapperProps> = (props) => {
    const {heading, chosenSchoolName, fileName, chartDimensions, data} = props
    const mode = useContext(ChartModeContext);

    const renderView = (displayMode: ChartMode) => {
        switch (displayMode) {
            case ChartMode.CHART:
                return <HorizontalBarChart data={data.dataPoints}
                                           chosenSchool={chosenSchoolName}
                                           heading={heading}
                                           fileName={fileName}
                                           chartDimensions={chartDimensions}
                />
            case ChartMode.TABLE:
                return <TableChart heading={heading}
                                   tableHeadings={data.tableHeadings}
                                   data={data.dataPoints}
                                   chartDimensions={chartDimensions}
                />
            default:
                return null
        }
    }

    return (<>
            {renderView(mode)}
        </>
    )
};

export default ChartWrapper
