import React from "react";
import {ChartMode} from "./chart-mode.tsx";

export type CompareYourSchoolViewProps = {
    urn: string
    academyYear: string
    maintainedYear: string
};

export type ChartWrapperProps = {
    elementId: string
    heading: React.ReactNode
    data: ChartWrapperData
    chartDimensions?: ChartDimensions
}

export type BarChartProps = {
    data: BarDataPoint[]
    heading: React.ReactNode
    elementId: string
    chartDimensions?: ChartDimensions
}

export type TableChartProps = {
    tableHeadings: string[]
    data?: ChartDataPoint[]
    heading: React.ReactNode
    chartDimensions?: ChartDimensions
    elementId: string
}

export type ToggleChartModeProps = {
    displayMode: ChartMode
    handleChange(): void
}

export type ChartWrapperData = {
    tableHeadings: string[]
    dataPoints:  ChartDataPoint[]
}

export type ChartDimensions = {
    dimensions: string[]
    handleChange: React.ChangeEventHandler<HTMLSelectElement>
}

export type CostValue = {
    dimension: string
    totalExpenditure: number
    totalIncome: number
    numberOfPupils: bigint
    value: number
}

export type PremisesValue = {
    dimension: string
    totalExpenditure: number
    totalIncome: number
    value: number
}

export type WorkforceValue = {
    dimension: string
    value: number
}

export type SelectedSchool = {
    urn: string
    name: string
}

type BarDataPoint = {
    school: string
    urn: string
    value: number
}

export type ChartDataPoint = {
    school: string
    urn: string
    value: number
    additionalData?: (string | bigint)[]
}

