import React from "react";

export type HorizontalBarChartWrapperProps = {
    elementId: string
    children?: React.ReactNode[] | React.ReactNode
    data: HorizontalBarChartWrapperData
}

export type HorizontalBarChartWrapperData = {
    tableHeadings: string[]
    dataPoints:  ChartDataPoint[]
}

export type ChartDataPoint = {
    school: string
    urn: string
    value: number
    additionalData?: (string | bigint)[]
}