import React from 'react';
import { Bar } from 'react-chartjs-2';
import { ChartData, ChartOptions } from 'chart.js';
import {Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title} from 'chart.js';
import {GridRow, GridCol} from "govuk-react";
import './HorizontalBarChart.css';
  
ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
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

const HBarChart: React.FC<BarChartProps> = ({ data, chosenSchool, xLabel }) => {
    const chosenSchoolIndex = data.labels.indexOf(chosenSchool);
    const barBackgroundColors = data.labels.map((label, index) => 
        index === chosenSchoolIndex ? '#12436D' : '#BFBFBF'
    );

    const datasets = [{
      data: data.data,
      backgroundColor: barBackgroundColors
    }];

    const dataForChart = {datasets:datasets, labels:data.labels}

    const options = {
      maintainAspectRatio: false,
      barPercentage: 1.09,
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
            font: data.labels.map((l, i) => ({
              weight: i == chosenSchoolIndex ? 'bolder' : 'normal',
            }))
          }
        }
      }
    };
    return(
      <>
        <GridRow>
          <GridCol setWidth="full">
            <div className="chart-container">
              <Bar data={dataForChart} options={options as ChartOptions}  />
            </div>
          </GridCol>
        </GridRow>
      </>
    )
  };
  
  export default HBarChart;
