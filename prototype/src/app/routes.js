//
// For guidance on how to create routes see:
// https://prototype-kit.service.gov.uk/docs/create-routes
//

const govukPrototypeKit = require('govuk-prototype-kit')
const router = govukPrototypeKit.requests.setupRouter()


router.get( '/add-comparator-school', (req, res) => {

    var comparators = req.session.data.comparators || [];
    var schoolName = req.session.data.school;
    comparatorName = schoolName.substring( 0, schoolName.lastIndexOf(' (') );
    comparatorLocation = schoolName.substring( schoolName.lastIndexOf(' (')+2, schoolName.lastIndexOf(',') );
    comparatorPostcode = schoolName.substring( schoolName.lastIndexOf(',')+2, schoolName.length-1 );
    comparatorPupils = Math.floor(Math.random() * (2782 - 438 + 1) ) + 438;
    comparatorMeals = Math.floor( ( ( Math.random() * (18 - 4.3 + 1) ) + 4.3 )* 10 ) /10;

    comparators.push({'comparatorName': comparatorName, 'comparatorLocation': comparatorLocation, 'comparatorPostcode': comparatorPostcode, 'comparatorPupils': comparatorPupils, 'comparatorMeals': comparatorMeals });
 
    req.session.data['comparators'] = comparators;
    
    res.redirect( '/comparators/create' );

})


router.get( '/comparators/create', (req, res) => {

    var rows = [];
    var comparators = req.session.data.comparators || [];
    
    for ( i=0; i<comparators.length; i++) {
        rows.push( [ {'text': comparators[i].comparatorName}, {'text': 'Secondary'}, {'text': comparators[i].comparatorPupils.toLocaleString()}, {'text': 'Good'}, {'text': comparators[i].comparatorMeals + '%'}, {'html': '<a href="/comparators/remove?id=' + i + '">Remove</a>' } ] );
    }

    res.render( '/comparators/create', { rows: rows } );

})


router.get( '/comparators/remove', (req, res) => {

    req.session.data.comparators.splice( req.query.id, 1 );

    res.redirect( '/comparators/create' );

})


router.get( '/set-school', (req, res) => {

    var schoolName = req.session.data.school;

    if (schoolName) {
        req.session.data['school-name'] = schoolName.substring( 0, schoolName.lastIndexOf(' (') );
    }

    res.redirect( '/school-homepage' );

})


// viewing session data
router.get('*/manage-prototype/view-data', function(req, res){

    querystring = '';
    for ( var key in req.session.data )
    {
        querystring += key +'=' + req.session.data[key] + '&';
    }

    res.render('manage-prototype/view-data', { data: JSON.stringify( req.session, null, 2), querystring: querystring } );
})



// Saving the session data to the clipboard using another page

router.get('*/manage-prototype/update-session-data', function(req, res){

    var querystringtemp = '';
    for ( var key in req.session.data )
    {
        querystringtemp += key +'=' + req.session.data[key] + '&';
    }

    req.session.data['theoutputquerystring'] = "";
    req.session.data['theoutputquerystring'] = "?" + querystringtemp;


    res.redirect('../manage-prototype/copy-url-to-clipboard');
})