import { HttpRequest, HttpResponseInit } from "@azure/functions";
import spec from "../../assets/openapi.json" with { type: "json" };
import fs from "fs/promises";

export async function openApi(): Promise<HttpResponseInit> {
  return { jsonBody: spec };
}

type SupportedFileExtension = ".css" | ".html" | ".js" | ".png";
const supportedMimeTypes: Record<SupportedFileExtension, string> = {
  ".css": "text/css",
  ".html": "text/html",
  ".js": "application/javascript",
  ".png": "image/png",
};

export async function swagger(request: HttpRequest): Promise<HttpResponseInit> {
  const path = request.params.swaggerAsset ?? "index.html";
  const extension = path.substring(
    path.lastIndexOf("."),
  ) as SupportedFileExtension;

  if (!Object.keys(supportedMimeTypes).includes(extension)) {
    return {
      status: 404,
      body: "Not found",
    };
  }

  let fileData: string | Buffer | undefined;
  try {
    const sanitisedPath = path
      .replace(/^\.\//, "")
      .replace(/[^a-zA-Z0-9\\.\\-]/g, "");
    fileData = await fs.readFile(
      `./node_modules/swagger-ui-dist/${sanitisedPath}`,
      extension === ".png" ? null : { encoding: "utf-8" },
    );

    if (path.endsWith("swagger-initializer.js")) {
      fileData = (fileData as string).replace(
        "https://petstore.swagger.io/v2/swagger.json",
        request.url.replace("swagger/swagger-initializer.js", "openapi.json"),
      );
    }
  } catch {
    return {
      status: 404,
      body: "Not found",
    };
  }

  return {
    body: fileData,
    headers: {
      "Content-Type": supportedMimeTypes[extension],
    },
  };
}
