//
// For guidance on how to create routes see:
// https://prototype-kit.service.gov.uk/docs/create-routes
//

const govukPrototypeKit = require('govuk-prototype-kit')
const router = govukPrototypeKit.requests.setupRouter()


// FIND SCHOOLS

router.get( '/find-school', (req, res) => {

    var schools = getSchoolList();
    var trusts = getTrustList();
    var authorities = getLocalAuthorityList();

    res.render( '/find-school', { schools: schools, trusts: trusts, authorities: authorities } );
})

router.get( '/authority-homepage', (req, res) => {
    res.render( '/authority-homepage' );
})

router.get( '/trust-homepage', (req, res) => {
    res.render( '/trust-homepage' );
})

router.get( '/school-homepage', (req, res) => {
    res.render( '/school-homepage' );
})

router.get( '/comparators/create/local-authority', (req, res) => {

    var rows = getLocalAuthorityList();
    res.render( '/comparators/create/local-authority', { rows: rows } );
})

// ADD SCHOOLS BY CHARACTERISTICS

router.get( '/comparators/create/characteristics', (req, res) => {

    var rows = getLocalAuthorityList();
    res.render( '/comparators/create/characteristics', { rows: rows } );

})

router.get( '/comparators/create/preview', (req, res) => {

    var rows = [];
    var comparators;

    if ( req.session.data['comparators'] && req.session.data['comparators'].length >= 30 ) {
        comparators = req.session.data['comparators'];
        console.log('new');
    } else {
        comparators = generatePupilComparators();
        console.log('existing');
    }

    console.log(comparators);

    comparators.sort((a, b) => a.comparatorName > b.comparatorName ? 1 : -1);
    req.session.data['comparators'] = comparators;
    
    for ( i=0; i<comparators.length; i++) {
        var nameHtml = "<span class=\"govuk-body govuk-!-font-weight-bold\">" + comparators[i].comparatorName +"</span><br><span class=\"govuk-hint\">" + comparators[i].comparatorLocation + ", " + comparators[i].comparatorPostcode + "</span>";
        var charsHtml = "<p class=\"govuk-body\">Number of pupils</p><p class=\"govuk-body\">Pupils with special educational needs (SEN):</p><p class=\"govuk-body\">Number of schools within trust:</p><p class=\"govuk-body\">Key stage 4 progress:</p>";
        var valuesHtml = "<p class=\"govuk-body\">" + comparators[i].comparatorPupils.toLocaleString() + "</p><p class=\"govuk-body\">" + comparators[i].comparatorSen + "%</p><p class=\"govuk-body\">" + ( Math.floor(Math.random() * (43 - 3 + 1) ) + 3) + "</p><p class=\"govuk-body\">" + Math.floor( (Math.random() * 100 ) ) / 100 + "</p>";
        rows.push( [ {'html':  nameHtml}, {'html': charsHtml}, {'html': valuesHtml} ] );
    }

    console.log(rows);
    res.render( '/comparators/create/preview', { rows: rows, confirmation: req.session.data['confirmation'], comparatorSetType: req.session.data['comparatorSetType'], errorThisPage: req.session.data['errorThisPage'], errorNoSchool: req.session.data['errorNoSchool'] } );

    // clear confirmation/errors
    req.session.data['confirmation'] = '';
    req.session.data['errorThisPage'] = 'false';
    req.session.data['errorNoSchool'] = 'false';

})


/*
router.post( '/comparators/create/generate-comparators', (req, res) => {

    var comparators = generatePupilComparators();
    comparators.sort((a, b) => a.comparatorName > b.comparatorName ? 1 : -1);

    req.session.data['comparators'] = comparators;
    req.session.data['confirmation'] = 'comparator-generated';
    req.session.data['comparatorSetType'] = 'generated';

    res.render( '/comparators/create/preview', { rows: comparators } );

})
*/

router.get( '/comparators/view', (req, res) => {
    var pupilRows = [];
    var pupilComparators;
    var buildingRows = [];
    var buildingComparators;
    
    if ( !req.session.data['pupilComparators'] ) {
        req.session.data['pupilComparators']  = generatePupilComparators();
    }
    if ( !req.session.data['buildingComparators'] ) {
        req.session.data['buildingComparators']  = generateBuildingComparators();
    }
    pupilComparators = req.session.data['pupilComparators'];
    buildingComparators = req.session.data['buildingComparators'];
    
    if ( pupilComparators ) {
        pupilComparators.sort((a, b) => a.comparatorName > b.comparatorName ? 1 : -1);

        for ( i=0; i<pupilComparators.length; i++) {
            var nameHtml = "<a href=\"/comparators/view-school?comparatorType=pupil&comparatorId=" + [i] +"\" class=\"govuk-link\">" + pupilComparators[i].comparatorName +"</a><br><span class=\"govuk-hint\">" + pupilComparators[i].comparatorLocation + ", " + pupilComparators[i].comparatorPostcode + "</span>";
            pupilRows.push( [ {'html':  nameHtml}, {'text': 'Secondary'}, {'text': pupilComparators[i].comparatorPupils.toLocaleString()}, {'text': pupilComparators[i].comparatorSen + '%'}, {'text': pupilComparators[i].comparatorMeals + '%'} ] );
        }
    }

    if ( buildingComparators ) {
        buildingComparators.sort((a, b) => a.buildingComparators > b.buildingComparators ? 1 : -1);

        for ( i=0; i<buildingComparators.length; i++) {
            var nameHtml = "<a href=\"/comparators/view-school?comparatorType=building&comparatorId=" + [i] +"\" class=\"govuk-link\">" + buildingComparators[i].comparatorName +"</a><br><span class=\"govuk-hint\">" + buildingComparators[i].comparatorLocation + ", " + buildingComparators[i].comparatorPostcode + "</span>";
            var buildingRow = [ {'html':  nameHtml}, {'text': 'Secondary'}, {'text': buildingComparators[i].comparatorPupils.toLocaleString()} ];

            if ( req.session.data['signedIn'] == 'true' ) {
                buildingRow.push( {'text': buildingComparators[i].comparatorGifa.toLocaleString() + ' sqm'}, {'text': buildingComparators[i].comparatorAge + ' years'} );
            } else if ( i == 0 ) {
                buildingRow.push( { 'html': "<a href=\"/sign-in?goTo=/comparators/view&hash=\&building\">Sign in</a> to see", colspan: 2, rowspan: buildingComparators.length, attributes: { style: "text-align: center; vertical-align: top;" } } );
            }

            buildingRows.push( buildingRow );
        }
    }

    res.render( '/comparators/view', { pupilRows: pupilRows, buildingRows: buildingRows } );
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
    comparatorSen = Math.floor( ( ( Math.random() * (9 - 0.8 + 1) ) + 2.3 )* 10 ) /10;

    comparators.unshift({'comparatorName': schoolName, 'comparatorLocation': comparatorLocation, 'comparatorPostcode': comparatorPostcode, 'comparatorPupils': comparatorPupils, 'comparatorSen': comparatorSen, 'comparatorMeals': comparatorMeals });
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

    res.redirect( '/comparators/create/view' );

})


router.get( '/comparators/create/by-name', (req, res) => {

    var rows = [];
    var schoolRows = getSchoolList();
    var comparators = req.session.data.comparators || [];
    
    for ( i=0; i<comparators.length; i++) {
        var nameHtml = "<span class=\"govuk-body govuk-!-font-weight-bold\">" + comparators[i].comparatorName + "</span><br><span class=\"govuk-hint\">" + comparators[i].comparatorLocation + ", " + comparators[i].comparatorPostcode + "</span>";
        rows.push( [ {'html':  nameHtml}, {'text': 'Secondary'}, {'text': comparators[i].comparatorPupils.toLocaleString()}, {'text': comparators[i].comparatorSen + '%'}, {'text': comparators[i].comparatorMeals + '%'}, {'html': '<a href="/comparators/remove?id=' + i + '">Remove</a>' } ] );
    }

    res.render( '/comparators/create/by-name', { schoolRows: schoolRows, rows: rows, confirmation: req.session.data['confirmation'], comparatorSetType: req.session.data['comparatorSetType'], errorThisPage: req.session.data['errorThisPage'], errorNoSchool: req.session.data['errorNoSchool'] } );

    // clear confirmation/errors
    req.session.data['confirmation'] = '';
    req.session.data['errorThisPage'] = 'false';
    req.session.data['errorNoSchool'] = 'false';

})


router.get( '/comparators/create/view', (req, res) => {

    var rows = [];
    var schoolRows = getSchoolList();
    var comparators = req.session.data.comparators || [];
    
    for ( i=0; i<comparators.length; i++) {
        var nameHtml = "<a href=\"/comparators/view-school?comparatorType=custom&comparatorId=" + [i] +"\" class=\"govuk-link\">" + comparators[i].comparatorName +"</a><br><span class=\"govuk-hint\">" + comparators[i].comparatorLocation + ", " + comparators[i].comparatorPostcode + "</span>";
        rows.push( [ {'html':  nameHtml}, {'text': 'Secondary'}, {'text': comparators[i].comparatorPupils.toLocaleString()}, {'text': comparators[i].comparatorSen + '%'}, {'text': comparators[i].comparatorMeals + '%'} ] );
    }

    res.render( '/comparators/create/view', { schoolRows: schoolRows, rows: rows, confirmation: req.session.data['confirmation'], comparatorSetType: req.session.data['comparatorSetType'], errorThisPage: req.session.data['errorThisPage'], errorNoSchool: req.session.data['errorNoSchool'] } );

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
    req.session.data.comparatorSetType = null;

    res.render( '/school-homepage', {confirmation: 'comparator-reset' } );


})


router.get( '/comparators', (req, res) => {

    res.render( '/comparators/index', { confirmation: req.session.data['confirmation'] } );

    // clear confirmation/errors
    req.session.data['confirmation'] = '';

})

router.get( '/comparators/view-school', (req, res) => {

    if ( req.session.data.comparatorType == 'pupil' && req.session.data.pupilComparators ) {
        arrComparators = req.session.data.pupilComparators;
    } else if ( req.session.data.comparatorType == 'building' && req.session.data.buildingComparators ) {
        arrComparators = req.session.data.buildingComparators;
    } else if ( req.session.data.comparatorType == 'custom' && req.session.data.comparators) {
        arrComparators = req.session.data.comparators;
    } 
    
    if ( typeof arrComparators !== 'undefined' && arrComparators[req.session.data.comparatorId]) {
        objSchool = arrComparators[req.session.data.comparatorId];
    } else {
        objSchool = { comparatorName: 'Burngreave High School', comparatorLocation: 'Sheffield', comparatorPostcode: 'S13 9ZD', comparatorPupils: 1408, comparatorMeals: 14.7 };
    }

    res.render( '/comparators/view-school', { comparatorName: objSchool.comparatorName, comparatorLocation: objSchool.comparatorLocation, comparatorPostcode: objSchool.comparatorPostcode, comparatorPupils: objSchool.comparatorPupils.toLocaleString(), comparatorMeals: objSchool.comparatorMeals }  );
})

router.post( '/set-school', (req, res) => {

    if ( req.session.data.signIn == 'trust') {
        var trustName = req.session.data.trust;
        if (trustName) {
            req.session.data['trust-name'] = trustName.substring( 0, trustName.lastIndexOf(' (') );
        }
        res.redirect( '/trust-homepage' );
    } else if ( req.session.data.signIn == 'authority') {
        var authorityName = req.session.data.authority;
        if (authorityName) {
            req.session.data['authority-name'] = authorityName.substring( 0, authorityName.lastIndexOf(' (') );
        }
        res.redirect( '/authority-homepage' );
    } else {
        var schoolName = req.session.data.school;
        if (schoolName) {
            req.session.data['school-name'] = schoolName.substring( 0, schoolName.lastIndexOf(' (') );
        }
        res.redirect( '/school-homepage' );
    }

})

// COMPARE TRUSTS

router.post( '/compare-trusts', (req, res) => {
    var compareRoute = 'by-name';
    if ( req.session.data['compareRoute'] ) {
        compareRoute = req.session.data['compareRoute'];
    }
    res.redirect( '/compare-trusts/' + compareRoute );
})

router.get( '/compare-trusts/by-name', (req, res) => {

    var rows = [];
    var trustRows = getTrustList();
    var trusts = req.session.data.trusts || [];
    
    for ( i=0; i<trusts.length; i++) {
        var nameHtml = "<a href=\"#\">" + trusts[i].trustName +"</a></span>";
        rows.push( [ {'html':  nameHtml}, {'text': trusts[i].trustPupils.toLocaleString()}, {'text': trusts[i].trustSchools}, {'text': '£' + trusts[i].trustIncome.toLocaleString() }, {'text': 'Secondary'}, {'html': '<a href="/compare-trusts/remove?id=' + i + '">Remove</a>' } ] );
    }

    res.render( '/compare-trusts/by-name', { trustRows: trustRows, rows: rows, confirmation: req.session.data['confirmation'], trustSetType: req.session.data['trustSetType'], errorThisPage: req.session.data['errorThisPage'], errorNoSchool: req.session.data['errorNoSchool'] } );

    // clear confirmation/errors
    req.session.data['confirmation'] = '';
    req.session.data['errorThisPage'] = 'false';
    req.session.data['errorNoSchool'] = 'false';

})

function addTrust (req, res, trustName) {

    var trusts = req.session.data.trusts || [];

    trustPupils = Math.floor(Math.random() * (2782 - 438 + 1) ) + 438;
    trustSchools = Math.floor( ( ( Math.random() * (18 - 4.3 + 1) ) + 4.3 ));
    trustIncome = Math.floor( ( ( Math.random() * (18 - 4.3 + 1) ) + 4.3 ) * 1000000 );
   
    trusts.unshift({'trustName': trustName, 'trustPupils': trustPupils, 'trustSchools': trustSchools, 'trustIncome': trustIncome });
    req.session.data['trusts'] = trusts;
    req.session.data['confirmation'] = 'trust-added';
    req.session.data.trust = null;
    
  }


router.get( '/compare-trusts/add-trust', (req, res) => {

    var trustName = req.session.data.trust;

    if ( trustName ) {
        addTrust( req, res, trustName);
    } else {
        req.session.data['errorThisPage'] = 'true';
        req.session.data['errorNoSchool'] = 'true';
    }

    res.redirect( '/compare-trusts/by-name' );

})

router.get( '/compare-trusts/remove', (req, res) => {

    if (req.session.data.trusts ) {
        req.session.data['trust-removed'] = req.session.data.trusts[req.query.id].trustName;
        
        req.session.data.trusts.splice( req.query.id, 1 );

        req.session.data['confirmation'] = 'trust-removed';
    }

    res.redirect( '/compare-trusts/by-name' );

})

router.get( '/compare-trusts/undo-remove', (req, res) => {

    var trustName = req.query.trustName;
    addTrust( req, res, trustName);

    res.redirect( '/compare-trusts/by-name' );

})

router.get( '/compare-trusts/reset-confirmed', (req, res) => {

    req.session.data.trusts = null;
    
    res.render( '/compare-trusts/by-name', {confirmation: 'trust-reset' } );

})



// ADD TRUSTS BY CHARACTERISTICS

router.post( '/compare-trusts/by-name', (req, res) => {

    var trusts = generateTrusts();
    trusts.sort((a, b) => a.trustName > b.trustName ? 1 : -1);

    req.session.data['trusts'] = trusts;
    req.session.data['confirmation'] = 'trusts-generated';
    req.session.data['trustSetType'] = 'generated';

    res.redirect( '/compare-trusts/by-name' );

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

function getTrustList() {

    var objTrustsFile = require('../app/data/trusts.json');
    var objTrusts = objTrustsFile.trusts;
    var trusts = [];

    for (i=0; i<objTrusts.length; i++ ) {
        trusts.push({'text':  objTrusts[i].trustName + " (" + objTrusts[i].trustNumber + ")" });
    }

    return trusts;
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

function generatePupilComparators() {

    var objSchoolsFile = require('../app/data/schools.json');
    var objSchools = objSchoolsFile.schools;
    var comparators = [];
    
    for (i=0; i<30; i++ ) {
        intRandom = Math.floor( Math.random() * objSchools.length ) ;
        objSchool = objSchools[ intRandom ];
        
        comparatorPupils = Math.floor(Math.random() * (2782 - 438 + 1) ) + 438;
        comparatorMeals = Math.floor( ( ( Math.random() * (18 - 4.3 + 1) ) + 4.3 )* 10 ) /10;
        comparatorSen = Math.floor( ( ( Math.random() * (9 - 0.8 + 1) ) + 2.3 )* 10 ) /10;

        comparators.push({'comparatorName':  objSchool.schoolName, 'comparatorLocation':  objSchool.schoolLocation, 'comparatorPostcode': objSchool.schoolPostcode, 'comparatorPupils': comparatorPupils, 'comparatorMeals': comparatorMeals, 'comparatorSen': comparatorSen });
    }

    return comparators;
}

function generateBuildingComparators() {

    var objSchoolsFile = require('../app/data/schools.json');
    var objSchools = objSchoolsFile.schools;
    var comparators = [];

    for (i=0; i<30; i++ ) {
        objSchool = objSchools[ Math.floor( Math.random() * objSchools.length ) ];
        comparatorPupils = Math.floor(Math.random() * (2782 - 438 + 1) ) + 438;
        comparatorGifa = comparatorPupils * Math.floor( ( Math.random() * (12 - 10 + 1) ) + 10 );
        comparatorAge = Math.floor( ( ( Math.random() * (87 - 4.3 + 1) ) + 4.3 ) );

        comparators.push({'comparatorName':  objSchool.schoolName, 'comparatorLocation':  objSchool.schoolLocation, 'comparatorPostcode': objSchool.schoolPostcode, 'comparatorPupils': comparatorPupils, 'comparatorAge': comparatorAge, 'comparatorGifa': comparatorGifa });
    }

    return comparators;
}

function generateTrusts() {

    var objTrustsFile = require('../app/data/trusts.json');
    var objTrusts = objTrustsFile.trusts;
    var trusts = [];

    for (i=0; i<10; i++ ) {
        objTrust = objTrusts[ Math.floor( Math.random() * objTrusts.length ) ];

        trustSchools = Math.floor( ( ( Math.random() * (18 - 4.3 + 1) ) + 4.3 ));
        trustPupils = Math.floor(Math.random() * (2782 - 438 + 1) ) + 438 * trustSchools;
        trustIncome = Math.floor( ( ( Math.random() * (18 - 4.3 + 1) ) + 4.3 ) * 1000000 * trustSchools );

        trusts.push({'trustName': objTrust.trustName, 'trustPupils': trustPupils, 'trustSchools': trustSchools, 'trustIncome': trustIncome });
    }

    return trusts;
}


// viewing session data
router.get('*/manage-prototype-data/view-data', function(req, res){

    querystring = '';
    for ( var key in req.session.data )
    {
        querystring += key +'=' + req.session.data[key] + '&';
    }

    res.render('manage-prototype-data/view-data', { data: JSON.stringify( req.session, null, 2), querystring: querystring } );
})



// Saving the session data to the clipboard using another page

router.get('*/manage-prototype-data/update-session-data', function(req, res){

    var querystringtemp = '';
    for ( var key in req.session.data )
    {
        querystringtemp += key +'=' + req.session.data[key] + '&';
    }

    req.session.data['theoutputquerystring'] = "";
    req.session.data['theoutputquerystring'] = "?" + querystringtemp;


    res.redirect('../manage-prototype-data/copy-url-to-clipboard');
})