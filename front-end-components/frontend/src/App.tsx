import React from 'react';
import Header from './components/Header/Header'; // Assuming Header is also a TypeScript component

function App(): JSX.Element {
  return (
    <div className="App">
      <header className="App-header">
        <Header />
      </header>
    </div>
  );
}

export default App;
