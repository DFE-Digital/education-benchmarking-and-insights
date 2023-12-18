import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import path from 'path';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [ react()],
  build: {
    lib: {
      entry: path.resolve(__dirname, 'src/main.tsx'),
      name: 'Educational Benchmarking - Front-end components',
      formats: ['es'],
      fileName: `front-end.js`,
    }
  },
  resolve: {
    alias: {
      '@govuk-react/icon-crown': path.resolve(__dirname, 'node_modules/@govuk-react/icon-crown'),
    },
  },
})
