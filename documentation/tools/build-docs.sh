SOURCE_DIR=${1}
PROJECT_NAME=${2}
TARGET_DIR=$(pwd)/${3:-build}
SCRIPT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

rm -rf ${TARGET_DIR}/

mkdir ${TARGET_DIR}

output="$(pwd)/build/$2"
dir="$(pwd)/$1"

cd ${dir}

rm -rf work

input=$(find . -name '*.md' -and ! -name '*.md.exclude' | sort -V)

for i in $input
do
    fname=${i#./}
    dir=$(dirname $i)
    if [ ! -d work/$dir ];then
        echo "Not exists"
        mkdir -p work/$dir
    fi;
    echo $(pwd)/$fname
    mmdc -e png -i $(pwd)/$fname -o work/$fname
done 

cd work
workfiles=$(find . -name '*.md' -and ! -name '*.md.exclude' | sort -V)
pandoc "${@:4}" --verbose  -s $workfiles  -o "$output"
cd ..
rm -rf work