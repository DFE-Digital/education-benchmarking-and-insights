//
// For guidance on how to create routes see:
// https://prototype-kit.service.gov.uk/docs/create-routes
//

const govukPrototypeKit = require('govuk-prototype-kit')
const router = govukPrototypeKit.requests.setupRouter()

require('./routes/latest.js');
require('./routes/v1_0_0.js');

router.all( /^(?!((\/latest|\/v\d+))).*$/, (req, res) => {
    res.redirect('/latest' + req.url );
})
