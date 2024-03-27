npm run lint
npm test
npm run build

Copy-Item dist/front-end.js ../web/src/Web.App/wwwroot/js -Verbose
Copy-Item dist/front-end.css ../web/src/Web.App/wwwroot/css -Verbose