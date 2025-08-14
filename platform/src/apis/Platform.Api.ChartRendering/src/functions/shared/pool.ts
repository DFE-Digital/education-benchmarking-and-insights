import Piscina from "piscina";

const piscina = new Piscina({
  filename: "./dist/src/functions/shared/worker.js",
});

export default piscina;
