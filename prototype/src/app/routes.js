//
// For guidance on how to create routes see:
// https://prototype-kit.service.gov.uk/docs/create-routes
//

const govukPrototypeKit = require('govuk-prototype-kit')
const router = govukPrototypeKit.requests.setupRouter()



// ADD SCHOOLS BY CHARACTERISTICS


router.post( '/comparators/create', (req, res) => {

    res.redirect( '/comparators/create/' + req.session.data['compareRoute'] );

})

// ADD SCHOOLS BY NAME

function addSchool (req, res, schoolName) {

    var comparators = req.session.data.comparators || [];
    
    comparatorLocation = schoolName.substring( schoolName.lastIndexOf(' (')+2, schoolName.lastIndexOf(',') );
    comparatorPostcode = schoolName.substring( schoolName.lastIndexOf(', ')+2, schoolName.length-1 );
    schoolName = schoolName.substring( 0, schoolName.lastIndexOf(' (') );
    comparatorPupils = Math.floor(Math.random() * (2782 - 438 + 1) ) + 438;
    comparatorMeals = Math.floor( ( ( Math.random() * (18 - 4.3 + 1) ) + 4.3 )* 10 ) /10;

    comparators.push({'comparatorName': schoolName, 'comparatorLocation': comparatorLocation, 'comparatorPostcode': comparatorPostcode, 'comparatorPupils': comparatorPupils, 'comparatorMeals': comparatorMeals });
    req.session.data['comparators'] = comparators;
    req.session.data['confirmation'] = 'comparator-added';
    req.session.data.school = null;
    
  }


router.get( '/add-comparator-school', (req, res) => {

    var schoolName = req.session.data.school;

    if ( schoolName ) {
        addSchool( req, res, schoolName);
    } else {
        req.session.data['errorThisPage'] = 'true';
        req.session.data['errorNoSchool'] = 'true';
    }

    res.redirect( '/comparators/create/by-name' );

})


router.get( '/comparators/undo-remove', (req, res) => {

    var schoolName = req.query.schoolName;
    addSchool( req, res, schoolName);

    res.redirect( '/comparators/create/by-name' );

})

router.get( '/comparators/create/by-name', (req, res) => {

    var rows = [];
    var comparators = req.session.data.comparators || [];
    
    for ( i=0; i<comparators.length; i++) {
        var nameHtml = "<a href=\"\" class=\"govuk-link\">" + comparators[i].comparatorName +"</a><br><span class=\"govuk-hint\">" + comparators[i].comparatorLocation + ", " + comparators[i].comparatorPostcode + "</span>";
        rows.push( [ {'html':  nameHtml}, {'text': 'Secondary'}, {'text': comparators[i].comparatorPupils.toLocaleString()}, {'text': 'Good'}, {'text': comparators[i].comparatorMeals + '%'}, {'html': '<a href="/comparators/remove?id=' + i + '">Remove</a>' } ] );
    }

    res.render( '/comparators/create/by-name', { rows: rows, confirmation: req.session.data['confirmation'], errorThisPage: req.session.data['errorThisPage'], errorNoSchool: req.session.data['errorNoSchool'] } );

    // clear confirmation/errors
    req.session.data['confirmation'] = '';
    req.session.data['errorThisPage'] = 'false';
    req.session.data['errorNoSchool'] = 'false';

})



router.get( '/comparators/remove', (req, res) => {

    if (req.session.data.comparators ) {
        req.session.data['school-removed'] = req.session.data.comparators[req.query.id].comparatorName;
        
        req.session.data.comparators.splice( req.query.id, 1 );

        req.session.data['confirmation'] = 'comparator-removed';
    }

    res.redirect( '/comparators/create/by-name' );

})


router.get( '/comparators/reset-confirmed', (req, res) => {

    req.session.data.comparators = null;
    req.session.data['confirmation'] = 'comparator-reset';

    res.redirect( '/comparators' );

})


router.get( '/comparators', (req, res) => {

    res.render( '/comparators/index', { confirmation: req.session.data['confirmation'] } );

    // clear confirmation/errors
    req.session.data['confirmation'] = '';

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