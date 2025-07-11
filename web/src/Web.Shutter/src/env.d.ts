declare global {
  namespace NodeJS {
    interface ProcessEnv {
      APPLICATIONINSIGHTS_CONNECTION_STRING?: string;
      AZURE_LOG_LEVEL?: "verbose" | "info" | "warning" | "error";
      LOG_LEVEL?:
        | "silly"
        | "debug"
        | "verbose"
        | "http"
        | "info"
        | "warn"
        | "error";
      MARKDOWN_CONTENT?: string;
      NODE_ENV: "development" | "production";
      NUNJUCKS_LOADER_NO_CACHE?: "true";
      NUNJUCKS_LOADER_WATCH?: "true";
      PORT?: string;
      ROLE_NAME?: string;
    }
  }
}

export {};
