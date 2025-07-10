import express, { Express } from "express";
import path from "path";
import { parse } from "marked";
import { sanitize } from "isomorphic-dompurify";
import nunjucks from "./nunjucks";
import logger from "./logger";

const app: Express = express();
logger.configure(app, process.env.NODE_ENV, process.env.LOG_LEVEL);
nunjucks.configure(app);

const markdownContent = process.env.MARKDOWN_CONTENT ?? undefined;
app.use("/assets", express.static(path.join(__dirname, "assets")));

app.get("/health", (_, res) => {
  res.type("text").status(200).send("Healthy");
});

// catch-all endpoint
app.get(/(.*)/, (_, res) => {
  // `index` resolves to `views/index.njk`
  res.render("index", {
    markdown_content: markdownContent
      ? sanitize(parse(markdownContent) as string)
      : undefined,
  });
});

const port = process.env.PORT || 7777;
app.listen(port, () => {
  logger.log("info", `Server is running at http://localhost:${port}`);
});
