import React, { useState, useEffect } from 'react';
import HBarChart from '../../components/HorizontalBarChart/HorizontalBarChart';

const ComparisonCharts: React.FC = (urn: string) => {

    const [data, setData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(window.location.pathname + `school/${urn}/expenditure`);
                if (!response.ok) {
                    throw new Error(`Response not ok ${response}`);
                }
                const json = await response.json();
                setData(json);
            } catch (error) {
                setError(error);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, [urn]);

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;
    
    return(
        <HBarChart />


        /*

        render bar expenditure bar chart, pass props through


        export interface BarData {
            labels: string[];
            data: number[];
        }

        export interface BarChartProps {
            data: BarData;
            chosenSchool: string;
            xLabel: string;
        }
        */
    )
  };
  
  const comparisonCharts = document.getElementById('comparisonCharts');
  searchFormElem && ReactDOM.render(<ComparisonCharts {...searchFormElem.dataset} />, searchFormElem);
