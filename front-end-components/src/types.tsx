import React from "react";

export type CompareYourSchoolViewProps = {
    urn: string
    academyYear: string
    maintainedYear: string
};

export type ChartWrapperProps = {
    heading: React.ReactNode
    chosenSchoolName: string
    data: ChartWrapperData
    fileName: string
    chartDimensions?: ChartDimensions
}

export type BarChartProps = {
    data: BarDataPoint[]
    chosenSchool: string
    heading: React.ReactNode
    fileName: string
    chartDimensions?: ChartDimensions
}

export type TableChartProps = {
    tableHeadings: string[]
    data?: ChartDataPoint[]
    heading: React.ReactNode
    chartDimensions?: ChartDimensions
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

type BarDataPoint = {
    school: string
    urn: string
    value: number
}

type ChartDataPoint = {
    school: string
    urn: string
    value: number
    additionalData?: (string | bigint)[]
}