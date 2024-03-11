#!/usr/bin/env node

//Workaround for Azure App Service to prevent decoding url

require('govuk-prototype-kit/lib/build.js').generateAssetsSync()
require('./listen-on-port.js')