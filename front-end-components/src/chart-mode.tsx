export enum ChartMode {
    CHART = 'View as chart',
    TABLE = 'View as table'
}

export const oppositeMode = (currentMode: ChartMode) => {
    return currentMode == ChartMode.TABLE ? ChartMode.CHART : ChartMode.TABLE
}