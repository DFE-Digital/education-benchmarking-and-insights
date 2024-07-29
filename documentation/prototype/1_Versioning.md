# Prototype versioning instructions

All prototype updates should be made in the following locations assuming you are in `prototype/src/app`.

* Pages - `views/latest/`
* Index page - `views/latest/index.html`
* Routing - `routes/latest.js`
* CSS - `assets/sass/_latest.scss`

This will update the pages running at http://localhost:3000/latest/

Archived versions such as http://localhost:3000/v1_0_0 can be created automatically at suitable points.

## Archiving a version of the prototype

Archiving a version of the prototype allows you still access the prototype as it stood at this point in time while allowing you to continue making/viewing changes in the `latest` folder for your next iteration.


### Automatic archiving (Recommended)
Archiving can be done automatically using a script

1. Open the folder you have installed the repo to in Finder
2. Navigate to `prototype/src`
3. Right click on the `app` folder and click `New Terminal at Folder`
4. In terminal, enter `bash versionUpdate.sh`
5. Enter a version number to archive the current `latest`.
    1. Version numbers should be in the format `v1.2.3`
    2. Numbers should increment suitably based on how minor/major an update it is.
6. Press enter
7. If successful, there will be a new link using the supplied version number at the bottom of the [index page](http://localhost:3000/latest/) to a snapshot of the prototype.
    1. Manually update the content to explain the changes in this update

If this works, you do not need to follow the manual archiving steps below.

### Manual archiving (Fallback)
If for any reason the script doesn't work, follow these steps to manually create an archived version.

1. Choose a suitable version number in the format `v1_2_3` to use in file/folder names
    1. Note this is a different format to the automatic process above
    2. `v1_2_3` will be used in the below examples
2. Navigate to `prototype/src/app`
3. Create a copy of the `views/latest` folder called `views/v1_2_3`
4. Create a copy of `routes/latest.js` called `routes/v1_2_3.js` 
5. Create a copy of `assets/sass/_latest.scss` called `assets/sass/_v1_2_3.scss` - note this begins with an underscore
6. Create a copy of `views/layouts/latest.html` called `views/layouts/v1_2_3.html`
7. In `routes/v1_2_3.js`:
    1. Change `const router = govukPrototypeKit.requests.setupRouter('/latest');` to `const router = govukPrototypeKit.requests.setupRouter('/v1_2_3');`
    2. Change `const version = 'latest';` to `const version = 'v1_2_3';`
8. In `assets/sass/_v1_2_3.scss`
    1. Change `.latest {` on the first line, to `.v1_2_3 {`
9. In `views/layouts/v1_2_3.html`
    1. Change `{% set bodyClasses = "latest" %}` to `{% set bodyClasses = "v1_2_3" %}`
    2. Change `{% set version = "latest" %}` to `{% set version = "v1_2_3" %}`
10. In the `views/v1_2_3` folder, you will need to do a find and replace across all files within than folder and any subfolders to:
    1. Change `{% extends "layouts/latest.html" %}` to `{% extends "layouts/v1_2_3.html" %}`
11. In `assets/sass/application.scss`
    1. Add the line `@import '_v1_2_3.scss';`
12. In `routes.js`
    1. Add the line `require('./routes/v1_2_3.js');`
13. Update the index page of your archived page `views/v1_2_3/index.html` to say this is an archived page
14. Update the main index page `views/latest/index.html` to add your archived version under the Version History section
