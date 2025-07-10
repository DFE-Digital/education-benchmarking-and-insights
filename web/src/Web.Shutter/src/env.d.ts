declare global {
  namespace NodeJS {
    interface ProcessEnv {
      APPLICATIONINSIGHTS_CONNECTION_STRING?: string;
      AZURE_LOG_LEVEL?: string;
      LOG_LEVEL?: string;
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
