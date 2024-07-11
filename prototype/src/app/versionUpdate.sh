#!/bin/bash

read -p "

Enter version number to archive the 'latest' build with.
This should be in the format v0.0.0
" versionDot

# convert version number to lowercase
versionDot=$(echo "$versionDot" | tr '[:upper:]' '[:lower:]')

# check version number is valid format
if [ ! ${versionDot:0:1} == "v" ]; then 
    echo "ERROR: ${versionDot} is not in the format 'v1.0.0'"
    exit;
fi

if [[ ! $versionDot == *.* ]] ; then
    echo "ERROR: ${versionDot} is not in the format 'v1.0.0'"
    exit;
fi

version="${versionDot//./_}"
# check folder doesn't already exist
if [ -d "views/${version}" ]; then
    echo "ERROR: ${versionDot} already exists at views/${version}. Try again with a unique version number."
    exit;
fi

echo "
Archiving latest version to ${versionDot}";

# creating files
cp -r views/latest views/${version}; 
cp -r routes/latest.js routes/${version}.js; 
cp -r assets/sass/_latest.scss assets/sass/_${version}.scss; 
cp -r views/layouts/latest.html views/layouts/${version}.html; 

# updating routes file
sed -i '' -e "s/setupRouter('\/latest')/setupRouter('\/${version}')/g" "routes/${version}.js"
sed -i '' -e "s/const version = 'latest'/const version = '${version}'/g" "routes/${version}.js"

# updating scss file
sed -i '' -e "s/.latest {/.${version} {/g" "assets/sass/_${version}.scss"

# updating layout file
sed -i '' -e "s/bodyClasses = \"latest\"/bodyClasses = \"${version}\"/g" "views/layouts/${version}.html"
sed -i '' -e "s/version = \"latest\"/version = \"${version}\"/g" "views/layouts/${version}.html"


# archived version to use new layout file
find "./views/${version}" -name "*.html" -type f -exec sed -i '' -e "s/latest.html/${version}.html/g" {} \;

# import new scss file
sed  -i '' -e "1s/^/@import '_${version}.scss';\n/g" assets/sass/application.scss

# import new routes file
sed  -i '' -e "s/require(\'.\/routes\/latest.js\');/require(\'.\/routes\/${version}.js\');\nrequire(\'.\/routes\/latest.js\');/g" routes.js

# add new version to archive index
sed  -i '' -e "s/  \<\!-- NEW VERSION --\>/  \<\!-- NEW VERSION --\>\n  \<li\>\n    \<h2 class=\"govuk-heading-m\"\>\<a class=\"govuk-link\" href=\"\/${version}\"\>${versionDot}\<\/a\> - $(date +"%d %B %Y")\<\/h2\>\n    \<p class=\"govuk-body\"\>Summary of changes\<\/p\>\n  \<\/li\>/g" views/latest/index.html

# remove archive list from archive index page
sed  -i '' -e "/\<\!-- VERSION HISTORY START --\>/,/\<\!-- VERSION HISTORY END --\>/d" views/${version}/index.html

# update heading on archived page
sed  -i '' -e "/\<\!-- HEADING START --\>/,/\<\!-- HEADING END --\>/d" views/${version}/index.html
sed  -i '' -e "s/  \<\!-- HEADING --\>/  \<\!-- HEADING --\>\n    \<h1 class=\"govuk-heading-l govuk-\!-margin-bottom-0\"\>Archived version - ${versionDot}\<\/h1\>\n    \<p class=\"govuk-hint\"\>Archived $(date +"%d %B %Y")\<\/p\>\n    \<p\>\n      \<a href=\"\/latest\"\>Return to latest version\<\/a\>\n    \<\/p\>    \<h2 class=\"govuk-heading-s\"\>Updates\<\/h2\>    \<ul class=\"govuk-list govuk-list--bullet\"\>      \<li\>Description of changes\<\/li\>    \<\/ul\>    \<h2 class=\"govuk-heading-m\"\>Contents\<\/h2\>/g" views/${version}/index.html