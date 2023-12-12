import HBarChart from './components/HorizontalBarChart/HorizontalBarChart';

function App(): JSX.Element {

  const labels = ['Red Riding Secondary School', 'Three Bear School', 'Testing McTester School', 'Braidbar Complex', 'Colcota Primary', 'Mearns High School Cru']
  return (
    <div className="App">
      <header className="App-header">
        <HBarChart data={{ labels:labels, data:[123, 456, 789, 354, 265, 654]}} chosenSchool={"Three Bear School"} xLabel={"per pupil"} />
      </header>
    </div>
  );
}

export default App;
