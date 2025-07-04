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

    # NOTE:
    # In Ubuntu 24.04+ AppArmor restricts unprivileged user namespaces.
    # This breaks Chromium's internal sandbox used by Mermaid CLI (via Puppeteer).
    # To work around this we pass a Puppeteer config that disables the sandbox.
    # For our use case diagrams are trusted and the environment is ephemeral.
    # See: https://github.com/mermaid-js/mermaid-cli/blob/340561040b6b0621a486e3fc96723139e5718268/docs/linux-sandbox-issue.md
    # https://github.com/mermaid-js/mermaid-cli/issues/730
    mmdc -e png -i $(pwd)/$fname -o work/$fname -p "$SCRIPT_DIR/puppeteer-config.json"
done 

cp -a images/. work/images
cd work
workfiles=$(find . -name '*.md' -and ! -name '*.md.exclude' | sort -V)

echo "[DEBUG] Markdown files passed to Pandoc:"
echo "$workfiles"

pandoc "${@:4}" --verbose  -s $workfiles  -o "$output"
cd ..
rm -rf work