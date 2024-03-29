const requests = require('govuk-prototype-kit/index').requests
const plugins = require('govuk-prototype-kit/lib/plugins/plugins')

// Serve assets from plugins
function setupPathsFor (item) {
  plugins.getPublicUrlAndFileSystemPaths(item)
    .forEach(paths => {
      requests.serveDirectory(paths.publicUrl, paths.fileSystemPath)
      // Keep /extension-assets path for backwards compatibility
      // TODO: Remove in v14
      requests.serveDirectory(
        paths.publicUrl.replace('plugin-assets', 'extension-assets'), paths.fileSystemPath)

      //Fix for azure
      requests.serveDirectory(
        paths.publicUrl.replace('%40x-govuk%2F', '@x-govuk/'), paths.fileSystemPath)
    })
}

setupPathsFor('scripts')
setupPathsFor('stylesheets')
setupPathsFor('assets')
