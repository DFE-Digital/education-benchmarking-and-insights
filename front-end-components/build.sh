#!/bin/bash

npm install

npm test
npm run build

mkdir -p ../web/src/EducationBenchmarking.Web/wwwroot/js
cp dist/front-end.js ../web/src/EducationBenchmarking.Web/wwwroot/js
