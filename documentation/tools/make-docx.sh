
SCRIPT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )
${SCRIPT_DIR}/build-docs.sh ${1} "${2}.docx" ${3:-build} --strip-comments --toc -f markdown+raw_attribute -t docx --wrap=preserve --reference-doc=${SCRIPT_DIR}/templates/docx-template.docx --lua-filter=${SCRIPT_DIR}/filters/pagebreak.lua
