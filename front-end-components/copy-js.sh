#!/bin/bash

npm run lint
npm test
npm run build

mkdir -p ../web/src/Web.App/wwwroot/js
mkdir -p ../web/src/Web.App/wwwroot/css

cp --verbose dist/front-end.js ../web/src/Web.App/wwwroot/js
cp --verbose dist/front-end.css ../web/src/Web.App/wwwroot/css