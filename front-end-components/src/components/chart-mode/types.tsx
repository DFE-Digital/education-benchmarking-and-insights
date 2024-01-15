export type ChartModeProps = {
    displayMode: ChartModes
    handleChange(): void
}

export enum ChartModes {
    CHART = 'View as chart',
    TABLE = 'View as table'
}