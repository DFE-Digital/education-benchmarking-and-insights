import Header from './components/Header/Header'; // Assuming Header is also a TypeScript component
import HBarChart from './components/HorizontalBarChart/HorizontalBarChart';

function App(): JSX.Element {

  const labels = ['test1', 'test2', 'test3']
  return (
    <div className="App">
      <header className="App-header">
        <Header />
        <HBarChart title={'Test Chart'} data={{labels, datasets: [{data: [123, 456, 789]}]}} />
      </header>
    </div>
  );
}

export default App;
