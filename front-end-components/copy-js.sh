#!/bin/bash

npm run lint
npm test
npm run build

mkdir -p ../web/src/EducationBenchmarking.Web/wwwroot/js
mkdir -p ../web/src/EducationBenchmarking.Web/wwwroot/css

cp dist/front-end.js ../web/src/EducationBenchmarking.Web/wwwroot/js
cp dist/front-end.css ../web/src/EducationBenchmarking.Web/wwwroot/css