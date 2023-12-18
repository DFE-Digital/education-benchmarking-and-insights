export interface BarData {
    labels: string[];
    data: number[];
}

export interface BarChartProps {
    data: BarData;
    chosenSchool: string;
    xLabel: string;
}
