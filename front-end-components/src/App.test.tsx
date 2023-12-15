import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';

import App from './App';

describe('App component', () => {
  test('renders App component', () => {
    render(<App />);
    const headerElement = screen.getByRole('banner');
    expect(headerElement).toBeInTheDocument();
  });
});
