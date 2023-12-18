import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import path from 'path';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [ react()],
  build: {
    lib: {
      entry: path.resolve(__dirname, 'src/main.tsx'),
      name: 'Education Benchmarking - Front-end components',
      formats: ['es'],
      fileName: `front-end`,
    }
  },
  define: { 'process.env.NODE_ENV': '"production"' },
  resolve: {
    alias: {
      '@govuk-react/icon-crown': path.resolve(__dirname, 'node_modules/@govuk-react/icon-crown'),
    },
  },
})
