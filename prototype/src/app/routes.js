//
// For guidance on how to create routes see:
// https://prototype-kit.service.gov.uk/docs/create-routes
//

const govukPrototypeKit = require('govuk-prototype-kit')
const router = govukPrototypeKit.requests.setupRouter()


// FIND SCHOOLS

router.get( '/find-school', (req, res) => {

    var rows = getSchoolList();
    res.render( '/find-school', { rows: rows } );
})

router.get( '/comparators/create/local-authority', (req, res) => {

    var rows = getLocalAuthorityList();
    res.render( '/comparators/create/local-authority', { rows: rows } );
})



// ADD SCHOOLS BY CHARACTERISTICS

router.post( '/comparators/create/review', (req, res) => {

    var comparators = generateComparators();
    comparators.sort((a, b) => a.comparatorName > b.comparatorName ? 1 : -1);

    req.session.data['comparators'] = comparators;
    req.session.data['confirmation'] = 'comparator-generated';
    req.session.data['comparatorSetType'] = 'generated';

    res.redirect( '/comparators/create/review' );

})

router.get( '/comparators/pupil', (req, res) => {
    var rows = [];
    var comparators;
    
    if ( !req.session.data['pupilComparators'] ) {
        req.session.data['pupilComparators']  = generateComparators();
    }
    comparators = req.session.data['pupilComparators'];
    
    if ( comparators ) {
        comparators.sort((a, b) => a.comparatorName > b.comparatorName ? 1 : -1);

        for ( i=0; i<comparators.length; i++) {
            var nameHtml = "<a href=\"\" class=\"govuk-link\">" + comparators[i].comparatorName +"</a><br><span class=\"govuk-hint\">" + comparators[i].comparatorLocation + ", " + comparators[i].comparatorPostcode + "</span>";
            rows.push( [ {'html':  nameHtml}, {'text': 'Secondary'}, {'text': comparators[i].comparatorPupils.toLocaleString()}, {'text': 'Good'}, {'text': comparators[i].comparatorMeals + '%'} ] );
        }
    }

    res.render( '/comparators/pupil', { rows: rows } );
})

router.get( '/comparators/building', (req, res) => {
    var rows = [];
    var comparators;
    
    if ( !req.session.data['buildingComparators'] ) {
        req.session.data['buildingComparators']  = generateComparators();
    }
    comparators = req.session.data['buildingComparators'];
    
    if ( comparators ) {
        comparators.sort((a, b) => a.comparatorName > b.comparatorName ? 1 : -1);

        for ( i=0; i<comparators.length; i++) {
            var nameHtml = "<a href=\"\" class=\"govuk-link\">" + comparators[i].comparatorName +"</a><br><span class=\"govuk-hint\">" + comparators[i].comparatorLocation + ", " + comparators[i].comparatorPostcode + "</span>";
            rows.push( [ {'html':  nameHtml}, {'text': 'Secondary'}, {'text': comparators[i].comparatorPupils.toLocaleString()}, {'text': 'Good'}, {'text': comparators[i].comparatorMeals + '%'} ] );
        }
    }

    res.render( '/comparators/building', { rows: rows } );
})

router.post( '/comparators/create', (req, res) => {
    
    var compareRoute = 'review';
    if ( req.session.data['compareRoute'] ) {
        compareRoute = req.session.data['compareRoute'];
    }

    res.redirect( '/comparators/create/' + compareRoute );

})

// ADD SCHOOLS BY NAME

function addSchool (req, res, schoolName) {

    var comparators = req.session.data.comparators || [];

    if (schoolName.lastIndexOf(' (') > 1 ) {
    
        comparatorLocation = schoolName.substring( schoolName.lastIndexOf(' (')+2, schoolName.lastIndexOf(',') );
        comparatorPostcode = schoolName.substring( schoolName.lastIndexOf(', ')+2, schoolName.length-1 );
        schoolName = schoolName.substring( 0, schoolName.lastIndexOf(' (') );
    } else {
        comparatorLocation = 'London';
        comparatorPostcode = 'SW1A 1AA';
    }

    comparatorPupils = Math.floor(Math.random() * (2782 - 438 + 1) ) + 438;
    comparatorMeals = Math.floor( ( ( Math.random() * (18 - 4.3 + 1) ) + 4.3 )* 10 ) /10;

    comparators.unshift({'comparatorName': schoolName, 'comparatorLocation': comparatorLocation, 'comparatorPostcode': comparatorPostcode, 'comparatorPupils': comparatorPupils, 'comparatorMeals': comparatorMeals });
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

    res.redirect( '/comparators/create/review' );

})


router.get( '/comparators/undo-remove', (req, res) => {

    var schoolName = req.query.schoolName;
    addSchool( req, res, schoolName);

    res.redirect( '/comparators/create/review' );

})

router.get( '/comparators/create/review', (req, res) => {

    var rows = [];
    var schoolRows = getSchoolList();
    var comparators = req.session.data.comparators || [];
    
    for ( i=0; i<comparators.length; i++) {
        var nameHtml = "<a href=\"\" class=\"govuk-link\">" + comparators[i].comparatorName +"</a><br><span class=\"govuk-hint\">" + comparators[i].comparatorLocation + ", " + comparators[i].comparatorPostcode + "</span>";
        rows.push( [ {'html':  nameHtml}, {'text': 'Secondary'}, {'text': comparators[i].comparatorPupils.toLocaleString()}, {'text': 'Good'}, {'text': comparators[i].comparatorMeals + '%'}, {'html': '<a href="/comparators/remove?id=' + i + '">Remove</a>' } ] );
    }

    res.render( '/comparators/create/review', { schoolRows: schoolRows, rows: rows, confirmation: req.session.data['confirmation'], comparatorSetType: req.session.data['comparatorSetType'], errorThisPage: req.session.data['errorThisPage'], errorNoSchool: req.session.data['errorNoSchool'] } );

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

    res.redirect( '/comparators/create/review' );

})


router.get( '/comparators/reset-confirmed', (req, res) => {

    req.session.data.comparators = null;
    req.session.data.comparatorSetType = null;

    res.render( '/school-homepage', {confirmation: 'comparator-reset' } );


})


router.get( '/comparators', (req, res) => {

    res.render( '/comparators/index', { confirmation: req.session.data['confirmation'] } );

    // clear confirmation/errors
    req.session.data['confirmation'] = '';

})


router.get( '/comparators/create', (req, res) => {

    if (req.session.data.comparators ) {
        res.redirect( '/comparators/create/review' );
    } else {
        res.render( '/comparators/index' );
    }
})


router.get( '/set-school', (req, res) => {

    var schoolName = req.session.data.school;

    if (schoolName) {
        req.session.data['school-name'] = schoolName.substring( 0, schoolName.lastIndexOf(' (') );
    }

    res.redirect( '/school-homepage' );

})

// DUMMY DATA GENERATION

function getSchoolList() {

    var objSchoolsFile = require('../app/data/schools.json');
    var objSchools = objSchoolsFile.schools;
    var schools = [];

    for (i=0; i<objSchools.length; i++ ) {
        objSchool = objSchools[ i ];

        schools.push({'text':  objSchool.schoolName + " (" + objSchool.schoolLocation + ", " + objSchool.schoolPostcode + ")" });
    }

    return schools;
}

function getLocalAuthorityList() {

    var objAuthorityFile = require('../app/data/authorities.json');
    var objAuthorities = objAuthorityFile.localAuthorities;
    var lcoalAuthorities = [];

    for (i=0; i<objAuthorities.length; i++ ) {
        objAuthority = objAuthorities[ i ];

        lcoalAuthorities.push({'text':  objAuthority.authorityName + " (" + objAuthority.authorityCode + ")" });
    }

    return lcoalAuthorities;
}

function generateComparators() {

    var objSchoolsFile = require('../app/data/schools.json');
    var objSchools = objSchoolsFile.schools;
    var comparators = [];

    for (i=0; i<30; i++ ) {
        objSchool = objSchools[ Math.floor( Math.random() * objSchools.length-1 ) ];
        comparatorPupils = Math.floor(Math.random() * (2782 - 438 + 1) ) + 438;
        comparatorMeals = Math.floor( ( ( Math.random() * (18 - 4.3 + 1) ) + 4.3 )* 10 ) /10;

        comparators.push({'comparatorName':  objSchool.schoolName, 'comparatorLocation':  objSchool.schoolLocation, 'comparatorPostcode': objSchool.schoolPostcode, 'comparatorPupils': comparatorPupils, 'comparatorMeals': comparatorMeals });
    }

    return comparators;
}


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