SOURCE_DIR=${1}
PROJECT_NAME=${2}
TARGET_DIR=$(pwd)/${3:-build}
SCRIPT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

rm -rf ${TARGET_DIR}/
mkdir ${TARGET_DIR}

output="$(pwd)/build/$2"
dir="$(pwd)/$1"

cd ${dir}
input=$(find . -name '*.md' -and ! -name '*.md.exclude' | sort -V)
pandoc "${@:4}" --verbose  -s $input  -o "$output"
