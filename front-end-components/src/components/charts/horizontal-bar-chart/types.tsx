export type BarChartProps = {
    chartId: string
    data: BarDataPoint[]
    ref?: DownloadHandle
}

type BarDataPoint = {
    school: string
    urn: string
    value: number
}

export type DownloadHandle = {
    download: () => void;
};
