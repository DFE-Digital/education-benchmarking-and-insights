import React from 'react';
import { Bar } from 'react-chartjs-2';
import { ChartData, ChartOptions } from 'chart.js';
import {Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title} from 'chart.js';
import {GridRow, GridCol} from "govuk-react";
  
  ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
  );

interface BarData {
    labels: string[];
    datasets: [{data:number[]}];
}

interface BarChartProps {
    data: BarData;
    title: string;
}

const HBarChart: React.FC<BarChartProps> = ({ data, title }) => {
    const options = {
      indexAxis: 'y',
      elements: {
        bar: {
          borderWidth: 2,
        },
      },
      responsive: true,
      plugins: {
        title: {
          display: true,
          text: title,
        },
      },
    };
    return(
    <>
      <GridRow>
        <GridCol setWidth="full">
          <Bar data={data} options={options as ChartOptions} />;
        </GridCol>
      </GridRow>
    </>)
  };
  
  export default HBarChart;
