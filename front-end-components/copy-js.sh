#!/bin/bash

npm run lint
npm test
npm run build:dev

mkdir -p ../web/src/Web.App/wwwroot/js
mkdir -p ../web/src/Web.App/wwwroot/css

cp -v dist/front-end.js* ../web/src/Web.App/wwwroot/js
cp -v dist/front-end.css ../web/src/Web.App/wwwroot/css
