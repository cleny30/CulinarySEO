import { defineConfig } from "vite";
import path from "path";
import react from "@vitejs/plugin-react";
import tailwindcss from "@tailwindcss/vite";
import fs from 'fs';

// Đường dẫn tới 2 file cert và key
const certPath = path.resolve(__dirname, 'localhost.pem');
const keyPath = path.resolve(__dirname, 'localhost-key.pem');
// https://vite.dev/config/
export default defineConfig({
  plugins: [react(), tailwindcss()],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
    },
  },
  server: {
    https: {
      key: fs.readFileSync(keyPath),
      cert: fs.readFileSync(certPath),
    },
    port: 5173,
    host: 'localhost'
  },
});
