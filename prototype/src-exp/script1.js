#!/usr/bin/env node

require('govuk-prototype-kit/lib/build.js').generateAssetsSync()
require('./listen-on-port.js')