import {render} from '@testing-library/react';
import '@testing-library/jest-dom';
import HorizontalBarChart from './horizontal-bar-chart';
import {BarChartProps} from '../../types';
import {ChartDimensionContext} from '../../contexts';

jest.mock('react-chartjs-2', () => ({
    Bar: (props: BarChartProps) => <div data-testid="mock-bar">{JSON.stringify(props)}</div>
}));

describe('HBarChart Component', () => {
    const mockData = [
        {school: 'School A', urn: "1", value: 20},
        {school: 'School B', urn: "2", value: 30},
        {school: 'School C', urn: "3", value: 40}
    ];
    const mockChosenSchool = 'School B';
    const mockXLabel = 'Test X Label';
    const mockHeading = <h2>Some heading</h2>;
    const mockFileName = 'file-name';

    it('renders without crashing', () => {
        const {container} = render(<ChartDimensionContext.Provider value={mockXLabel}><HorizontalBarChart
            heading={mockHeading} data={mockData} chosenSchool={mockChosenSchool}
            fileName={mockFileName}/></ChartDimensionContext.Provider>);
        expect(container).toBeInTheDocument();
    });

    it('displays correct xLabel', () => {
        const {getByTestId} = render(<ChartDimensionContext.Provider value={mockXLabel}><HorizontalBarChart
            heading={mockHeading} data={mockData} chosenSchool={mockChosenSchool}
            fileName={mockFileName}/></ChartDimensionContext.Provider>);
        const barProps = JSON.parse(getByTestId('mock-bar').textContent ?? '');
        expect(barProps.options.scales.x.title.text).toBe(mockXLabel);
    });

    it('passes correct data to the chart', () => {
        const {getByTestId} = render(<ChartDimensionContext.Provider value={mockXLabel}><HorizontalBarChart
            heading={mockHeading} data={mockData} chosenSchool={mockChosenSchool}
            fileName={mockFileName}/></ChartDimensionContext.Provider>);
        const barProps = JSON.parse(getByTestId('mock-bar').textContent ?? '');
        expect(barProps.data).toEqual({
            labels: mockData.map(data => data.school),
            datasets: [{
                data: mockData.map(data => data.value),
                backgroundColor: expect.any(Array),
                barPercentage: 1.09,
            }]
        });
    });

    it('highlights the chosen school correctly', () => {
        const {getByTestId} = render(<ChartDimensionContext.Provider value={mockXLabel}><HorizontalBarChart
            heading={mockHeading} data={mockData} chosenSchool={mockChosenSchool}
            fileName={mockFileName}/></ChartDimensionContext.Provider>);
        const barProps = JSON.parse(getByTestId('mock-bar').textContent ?? '');
        const chosenSchoolIndex = mockData.findIndex(data => data.school === mockChosenSchool);
        expect(barProps.data.datasets[0].backgroundColor[chosenSchoolIndex]).toBe('#12436D');
    });

    it('sets correct chart options', () => {
        const {getByTestId} = render(<ChartDimensionContext.Provider value={mockXLabel}><HorizontalBarChart
            heading={mockHeading} data={mockData} chosenSchool={mockChosenSchool}
            fileName={mockFileName}/></ChartDimensionContext.Provider>);
        const barProps = JSON.parse(getByTestId('mock-bar').textContent ?? '');
        // Main options
        expect(barProps.options.maintainAspectRatio).toBe(false);
        expect(barProps.options.indexAxis).toBe('y');
        expect(barProps.options.responsive).toBe(true);

        // X-axis options
        expect(barProps.options.scales.x.border.color).toBe('black');
        expect(barProps.options.scales.x.grid.display).toBe(true);
        expect(barProps.options.scales.x.grid.drawOnChartArea).toBe(false);
        expect(barProps.options.scales.x.grid.drawTicks).toBe(true);
        expect(barProps.options.scales.x.grid.tickLength).toBe(7);
        expect(barProps.options.scales.x.grid.color).toBe('black');
        expect(barProps.options.scales.x.title.display).toBe(true);
        expect(barProps.options.scales.x.title.text).toBe(mockXLabel);
        expect(barProps.options.scales.x.title.color).toBe('black');
        expect(barProps.options.scales.x.ticks.color).toBe('black');

        // Y-axis options
        expect(barProps.options.scales.y.border.color).toBe('black');
        expect(barProps.options.scales.y.grid.offset).toBe(false);
        expect(barProps.options.scales.y.grid.display).toBe(true);
        expect(barProps.options.scales.y.grid.drawOnChartArea).toBe(false);
        expect(barProps.options.scales.y.grid.drawTicks).toBe(true);
        expect(barProps.options.scales.y.grid.tickLength).toBe(7);
        expect(barProps.options.scales.y.grid.color).toBe('black');
        expect(barProps.options.scales.y.ticks.color).toBe('#1D70B8');
    });


    it('includes custom underline plugin', () => {
        const {getByTestId} = render(<ChartDimensionContext.Provider value={mockXLabel}><HorizontalBarChart
            heading={mockHeading} data={mockData} chosenSchool={mockChosenSchool}
            fileName={mockFileName}/></ChartDimensionContext.Provider>);
        const barProps = JSON.parse(getByTestId('mock-bar').textContent ?? '');
        expect(barProps.plugins).toEqual(expect.arrayContaining([{id: 'underline'}]));
    });
});
