import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import path from 'path';


// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      '@govuk-react/icon-crown': path.resolve(__dirname, 'node_modules/@govuk-react/icon-crown'),
    },
  },

})
