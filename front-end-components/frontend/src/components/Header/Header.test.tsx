import React from 'react';
import { render, screen } from '@testing-library/react';
import Header from './Header';

describe('Header Component', () => {
  test('renders with correct text', () => {
    render(<Header />);
    const headingElement = screen.getByText(/DfE/i);
    expect(headingElement).toBeInTheDocument();
  });
});
