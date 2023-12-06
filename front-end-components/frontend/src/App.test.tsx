import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';

describe('App component', () => {
  test('renders App component', () => {
    render(<App />);
    const headerElement = screen.getByRole('banner');
    expect(headerElement).toBeInTheDocument();
  });
});
