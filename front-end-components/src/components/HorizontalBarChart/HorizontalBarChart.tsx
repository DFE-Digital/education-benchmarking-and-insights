import React from 'react';
import { Bar } from 'react-chartjs-2';
import {Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tick, ChartOptions} from 'chart.js';
import './HorizontalBarChart.css';

ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title
  );

interface BarData {
    labels: string[];
    data: number[];
}

interface BarChartProps {
    data: BarData;
    chosenSchool: string;
    xLabel: string;
}

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
      const yPosition = yAxis.getPixelForTick(index) + (yAxisLabelPadding*2)

      ctx.strokeStyle = yAxis.options.ticks.color
      ctx.lineWidth = 1.5;
      const xStart = yAxis.right - textWidth - (yAxisLabelPadding*3)


      ctx.beginPath();
      ctx.moveTo(xStart, yPosition);
      ctx.lineTo(xStart + textWidth, yPosition);
      ctx.stroke();
    });
    ctx.restore();
  }
};

const HBarChart: React.FC<BarChartProps> = ({ data, chosenSchool, xLabel }) => {
    const chosenSchoolIndex = data.labels.indexOf(chosenSchool);
    const barBackgroundColors = data.labels.map((_, index) => 
        index === chosenSchoolIndex ? '#12436D' : '#BFBFBF'
    );

    const datasets = [{
      data: data.data,
      backgroundColor: barBackgroundColors,
      barPercentage: 1.09,
    }];

    const dataForChart = {datasets:datasets, labels:data.labels}

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
          ticks:{
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
    return(
      <>
        <div className="chart-container">
          <Bar data={dataForChart} options={options} plugins={[underLinePlugin]} />
        </div>
      </>
    )
  };
  
  export default HBarChart;
