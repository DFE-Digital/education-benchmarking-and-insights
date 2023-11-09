
SCRIPT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )
${SCRIPT_DIR}/build-docs.sh ${1} "${2}.pdf" ${3:-build} --strip-comments --toc -f markdown -t pdf --pdf-engine=/Library/TeX/texbin/pdflatex --wrap=preserve --data-dir=${SCRIPT_DIR} --template=pdf-template
