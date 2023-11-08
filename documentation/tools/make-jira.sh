
SCRIPT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )
${SCRIPT_DIR}/build-docs.sh ${1} "${2}.jira" ${3:-build} --strip-comments --toc -f markdown -t jira --wrap=preserve
