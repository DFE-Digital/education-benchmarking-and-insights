import React, {useContext} from "react";
import {ChartModes} from "src/components/chart-mode";
import {HorizontalBarChart} from "src/components/charts/horizontal-bar-chart";
import {TableChart} from "src/components/charts/table-chart";
import {ChartModeContext} from "src/contexts";
import {Loading} from "src/components/loading";
import {HorizontalBarChartWrapperProps} from "src/components/charts";

export const HorizontalBarChartWrapper: React.FC<HorizontalBarChartWrapperProps> = (props) => {
    const { data, children} = props
    const mode = useContext(ChartModeContext);

    const renderView = (displayMode: ChartModes) => {
        switch (displayMode) {
            case ChartModes.CHART:
                return <HorizontalBarChart data={data.dataPoints}>
                    {children}
                </HorizontalBarChart>
            case ChartModes.TABLE:
                return <TableChart tableHeadings={data.tableHeadings} data={data.dataPoints}>
                    {children}
                </TableChart>
            default:
                return <Loading/>
        }
    }

    return (
        <>
            {data.dataPoints.length > 0 ? renderView(mode) : <Loading/>}
        </>
    )
};
