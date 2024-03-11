// npm dependencies
const { runErrorServer } = require('govuk-prototype-kit/lib/errorServer')
const { verboseLog } = require('govuk-prototype-kit/lib/utils/verboseLogger')

const config = require('govuk-prototype-kit/lib/config.js').getConfig(null, false)

try {
  // local dependencies
  const syncChanges = require('govuk-prototype-kit/lib/sync-changes')
  const server = require('./server.js')
  const { generateAssetsSync } = require('govuk-prototype-kit/lib/build')

  const port = config.port
  const proxyPort = port - 50

  generateAssetsSync()

  if (config.isTest) {
    server.listen()
  } else {
    if (config.isDevelopment) {
      console.log('You can manage your prototype at:')
      console.log(`http://localhost:${port}/manage-prototype`)
      console.log('')
    }
    console.log('The Prototype Kit is now running at:')
    console.log(`http://localhost:${port}`)
    console.log('')

    if (config.isProduction || !config.useBrowserSync) {
      server.listen(port)
    } else {
      server.listen(proxyPort, () => {
        syncChanges.sync({
          port,
          proxyPort,
          files: ['.tmp/public/**/*.*', 'app/views/**/*.*', 'app/assets/**/*.*', 'app/config.json']
        })
      })
    }
  }
} catch (e) {
  if (config.isDevelopment) {
    verboseLog('************************ STARTING ERROR SERVER ***************************')
    runErrorServer(e)
  } else {
    throw e
  }
}
