import "dotenv/config";
import { Octokit } from "@octokit/core";
import { createAppAuth } from "@octokit/auth-app";
import consoleLogLevel from "console-log-level";

// const octokit = new Octokit({
//     auth: process.env.PRIVATE_KEY,
//     log: consoleLogLevel({ level: "info" })
// });

const octokit = new Octokit({
    authStrategy: createAppAuth,
    auth: {
        appId: process.env.APP_ID,
        privateKey: process.env.PRIVATE_KEY,
        installationId: process.env.INSTALLATION_ID,
    },
    log: consoleLogLevel({ level: process.env.LOG_LEVEL }),
});

// const app = new App({
//     appId: process.env.APP_ID,
//     privateKey: process.env.PRIVATE_KEY,
    
//     log: consoleLogLevel({ level: "info" })
// });

await octokit.request("POST /repos/{owner}/{repo}/pulls", {
    owner: "DFE-Digital",
    repo: "education-benchmarking-and-insights",
    title: process.env.TITLE,
    head: process.env.BRANCH,
    base: "main",
    headers: {
        "X-GitHub-Api-Version": "2022-11-28"
    },
    draft: true
});

// await octokit.request("POST /repos/{owner}/{repo}/pulls", {
//     owner: "DFE-Digital",
//     repo: "education-benchmarking-and-insights",
//     title: process.env.TITLE,
//     head: process.env.BRANCH,
//     base: "main",
//     headers: {
//         "X-GitHub-Api-Version": "2022-11-28"
//     },
//     draft: true
// });
