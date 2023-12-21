import React, { useState, useEffect } from 'react';
// import ReactDOM from 'react-dom';
import HBarChart from '../../components/HorizontalBarChart/HorizontalBarChart';

type PageProps = {
    urn: string;
}
const ComparisonCharts: React.FC<PageProps> = ({urn}) => {

    const [data, setData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(`api/school/${urn}/expenditure`);
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

    const barData = {
        labels: data.results.map(result => result.schoolName),
        data: data.results.map(result => result.totalExpenditurePerPupil),
    }
    const chosenSchoolName = data.results.find(school => school.urn === urn)?.schoolName || null;

    return(
        <div>
            <h1>Total Expenditure</h1>
            <HBarChart data={barData} chosenSchool={chosenSchoolName} xLabel='per pupil'/>
        </div>
    )
  };
  
// const comparisonCharts = document.getElementById('comparisonCharts');
// comparisonCharts && ReactDOM.render(<ComparisonCharts {...comparisonCharts.dataset} />, comparisonCharts);
export default ComparisonCharts;
