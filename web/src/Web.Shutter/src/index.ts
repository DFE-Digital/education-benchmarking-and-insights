import express, { Express } from "express";
import dotenv from "dotenv";
import path from "path";
import nunjucks from "nunjucks";
import { use, parse } from "marked";
import { sanitize } from "isomorphic-dompurify";
import { markdownRenderer } from "./markdown";

dotenv.config();
const app: Express = express();
const port = process.env.PORT || 3000;
const markdownContent = process.env.MARKDOWN_CONTENT ?? undefined;

app.set("views", path.join(__dirname, "/views"));

const loader = new nunjucks.FileSystemLoader([
  "node_modules/govuk-frontend/dist",
  app.get("views"),
]);
const env = new nunjucks.Environment(loader, {
  watch: process.env.NUNJUCKS_LOADER_WATCH === "true",
  noCache: process.env.NUNJUCKS_LOADER_NO_CACHE === "true",
});

env.addGlobal("govukRebrand", false);
env.express(app);
app.set("view engine", "njk");
use({ renderer: markdownRenderer });

app.use("/assets", express.static(path.join(__dirname, "assets")));

// catch-all endpoint
app.get(/(.*)/, (_, res) => {
  // `index` resolves to `views/index.njk`
  res.render("index", {
    markdown_content: markdownContent
      ? sanitize(parse(markdownContent) as string)
      : undefined,
  });
});

app.listen(port, () => {
  console.log(`Server is running at http://localhost:${port}`);
});
