import express from "express";
import winston from "winston";

let logger: winston.Logger | undefined;

const configure = (
  app: express.Express,
  nodeEnv?: string,
  logLevel?: string,
) => {
  if (logger) {
    return;
  }

  // use dynamic import to ensure that `winston` imported _after_ app insights has loaded
  import("winston")
    .then(winston => {
      logger = winston.createLogger({
        level: logLevel ?? "info",
        format: winston.format.json(),
      });

      // console logging in dev
      if (nodeEnv !== "production") {
        logger.add(
          new winston.transports.Console({
            format: winston.format.simple(),
          }),
        );
      }

      log("debug", "winston configured successfully");
    })
    .catch(e => {
      log("warn", "Unable to configure winston", e);
    });
};

const log = (
  level: "debug" | "info" | "warn",
  message: string,
  ...meta: unknown[]
) => {
  if (logger) {
    logger.log(level, message, meta);
    return;
  }

  // fall back to console logging if winston not ready
  // eslint-disable-next-line no-console
  console.log(
    `${level}: ${message} ${meta?.length ? JSON.stringify({ ...meta }) : ""}`,
  );
};

export default {
  configure,
  log,
};
