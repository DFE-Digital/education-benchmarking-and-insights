import React from "react";

export type BarChartProps = {
    data: BarDataPoint[]
    children?: React.ReactNode[] | React.ReactNode
}

type BarDataPoint = {
    school: string
    urn: string
    value: number
}