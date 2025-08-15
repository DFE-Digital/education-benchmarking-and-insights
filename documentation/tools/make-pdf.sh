#!/bin/bash

SCRIPT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

INPUT_FILE=${1}
OUTPUT_FILE="${2}.pdf"
BUILD_DIR=${3:-build}

pandoc "${INPUT_FILE}" \
  -o "${OUTPUT_FILE}" \
  --from=markdown+pipe_tables+grid_tables+multiline_tables \
  --to=pdf \
  --pdf-engine=xelatex \
  --variable=geometry:margin=2.5cm \
  --variable=fontsize=10pt \
  --variable=documentclass=article \
  --variable=classoption=onecolumn \
  --table-of-contents \
  --toc-depth=3 \
  --highlight-style=tango \
  --strip-comments \
  --standalone \
  --filter pandoc-crossref \
  --citeproc \
  -V colorlinks=true \
  -V linkcolor=blue \
  -V urlcolor=blue \
  -V toccolor=gray \
  --data-dir="${SCRIPT_DIR}" \
  --template=pdf-template