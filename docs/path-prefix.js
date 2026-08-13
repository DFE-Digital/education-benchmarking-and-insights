export const pathPrefix =
    process.env.ELEVENTY_ENV === "production"
        ? "/education-benchmarking-and-insights/"
        : "/";
