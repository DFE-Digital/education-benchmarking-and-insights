import consoleLogLevel from "console-log-level";

declare global {
    namespace NodeJS {
        interface ProcessEnv {
            NODE_ENV: "development" | "production";
            APP_ID: string;
            BRANCH: string;
            INSTALLATION_ID: number;
            LOG_LEVEL: consoleLogLevel.LogLevelNames | undefined
            PRIVATE_KEY: string;
            TITLE: string;
        }
    }
}

export { }