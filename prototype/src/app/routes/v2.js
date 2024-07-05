const govukPrototypeKit = require('govuk-prototype-kit')
const router = govukPrototypeKit.requests.setupRouter('/v2')

const version = 'v2'

router.get('/test2', function(request, response) {
    response.send('test2')
})