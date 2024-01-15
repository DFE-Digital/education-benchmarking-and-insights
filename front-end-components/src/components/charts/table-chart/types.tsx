import React from "react";
import {ChartDataPoint} from "src/components/charts";

export type TableChartProps = {
    tableHeadings: string[]
    data?: ChartDataPoint[]
    children?: React.ReactNode[] | React.ReactNode
}
