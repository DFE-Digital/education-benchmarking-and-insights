import React, {useRef} from 'react';
import { Bar } from 'react-chartjs-2';
import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tick, ChartOptions } from 'chart.js';
import './horizontal-bar-chart.css';
import {ChartDimensions} from "../../chart-dimensions";

ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title
);


const underLinePlugin = {
    id: 'underline',
    afterDraw: (chart: ChartJS) => {
        const ctx = chart.ctx;
        ctx.save();

        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        const yAxis = chart.scales.y as any;

        yAxis.ticks.forEach((tick: Tick, index: number) => {
            const yAxisLabelPadding = yAxis.options.ticks.padding;
            const textWidth = ctx.measureText(tick.label as string).width;
            const yPosition = yAxis.getPixelForTick(index) + (yAxisLabelPadding * 2)

            ctx.strokeStyle = yAxis.options.ticks.color
            ctx.lineWidth = 1.5;
            const xStart = yAxis.right - textWidth - (yAxisLabelPadding * 3)


            ctx.beginPath();
            ctx.moveTo(xStart, yPosition);
            ctx.lineTo(xStart + textWidth, yPosition);
            ctx.stroke();
        });
        ctx.restore();
    }
};

const HorizontalBarChart: React.FC<BarChartProps> = (props) => {
    const {data, chosenSchool, xLabel, heading, fileName, chartDimensions} = props
    const chosenSchoolIndex = chosenSchool ? data.labels.indexOf(chosenSchool) : 0;
    const barBackgroundColors = data.labels.map((_, index) =>
        index === chosenSchoolIndex ? '#12436D' : '#BFBFBF'
    );


    const chartRef = useRef<ChartJS<'bar'>>(null);


    const datasets = [{
        data: data.data,
        backgroundColor: barBackgroundColors,
        barPercentage: 1.09,
    }];

    const dataForChart = {datasets: datasets, labels: data.labels}

    const options: ChartOptions<'bar'> = {
        maintainAspectRatio: false,
        indexAxis: 'y',
        responsive: true,
        scales: {
            x: {
                border: {
                    color: 'black',
                },
                grid: {
                    display: true,
                    drawOnChartArea: false,
                    drawTicks: true,
                    tickLength: 7,
                    color: 'black',
                },
                title: {
                    display: true,
                    text: xLabel,
                    color: 'black'
                },
                ticks: {
                    color: 'black'
                }
            },
            y: {
                border: {
                    color: 'black',
                },
                grid: {
                    offset: false,
                    display: true,
                    drawOnChartArea: false,
                    drawTicks: true,
                    tickLength: 7,
                    color: 'black'
                },
                ticks: {
                    color: '#1D70B8',
                    font: (context) => {
                        const label = context.tick.label;
                        const weight = data.labels[chosenSchoolIndex] === label ? 'bolder' : 'normal';
                        return {
                            weight: weight,
                        };
                    }
                }
            }
        }
    };

    function handleSaveClick() {
        if(chartRef.current) {
            const a = document.createElement('a');
            a.href = chartRef.current.toBase64Image();
            a.download = `${fileName}.png`;
            a.click();
        }
    }

    return (
        <>
            <div className="govuk-grid-row">
                <div className="govuk-grid-column-two-thirds">
                    {heading}
                    {chartDimensions && chartDimensions.dimensions.length > 0 &&
                        <div className="govuk-form-group">
                            <label className="govuk-label" htmlFor="dimension">
                                View graph as
                            </label>
                            <select className="govuk-select" id="dimension" name="dimension" onChange={chartDimensions.handleChange} defaultValue={xLabel}>
                                {chartDimensions.dimensions.map((dimension, idx) => {
                                    return <option key={idx} value={dimension}>{dimension}</option>;
                                })}
                            </select>
                        </div>
                    }
                </div>
                <div className="govuk-grid-column-one-third">
                    <button className="govuk-button govuk-button--secondary" data-module="govuk-button"
                            onClick={handleSaveClick}>
                        Save as image
                    </button>
                </div>
            </div>
            {data.labels.length > 0 &&
                <div className="govuk-grid-row">
                    <div className="govuk-grid-column-full">
                        <div className="chart-container">
                            <Bar data={dataForChart} options={options} plugins={[underLinePlugin]} ref={chartRef}/>
                        </div>
                    </div>
                </div>
            }
        </>
    )
};

export default HorizontalBarChart;

export type BarData = {
    labels: string[]
    data: number[]
}

export type BarChartProps = {
    data: BarData
    chosenSchool: string
    xLabel: string
    heading: React.ReactNode
    fileName: string
    chartDimensions?: ChartDimensions
}