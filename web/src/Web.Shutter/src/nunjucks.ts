import express from "express";
import path from "path";
import nunjucks from "nunjucks";
import { use } from "marked";
import { markdownRenderer } from "./markdown";

const configure = (app: express.Express) => {
  app.set("views", path.join(__dirname, "/views"));

  const loader = new nunjucks.FileSystemLoader([
    "node_modules/govuk-frontend/dist",
    app.get("views"),
  ]);

  const options: nunjucks.ConfigureOptions = {
    watch: process.env.NUNJUCKS_LOADER_WATCH === "true",
    noCache: process.env.NUNJUCKS_LOADER_NO_CACHE === "true",
  };

  const env = new nunjucks.Environment(loader, options);
  env.addGlobal("govukRebrand", false);
  env.express(app);
  app.set("view engine", "njk");
  use({ renderer: markdownRenderer });
};

export default {
  configure,
};
