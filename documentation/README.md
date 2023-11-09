# Documentation Templates

This repository contains a set of documentation templates that cover 

1. Architecture
2. Testing artefacts

## Running scripts via MAKE

You can create a document for a folder using the following make commands. 

For architecture output 

    make architecture-pdf

    make architecure-docx

    make architecure-jira

For testing output 

    make testing-pdf

    make testing-docx

    make testing-jira

By default this will create a build directory and output the document to there hover this can be controlled by setting the `OUTPUT` variable

    make architecture-pdf OUTPUT=out

the above command would output the document to a folder called `out`

## Running scripts directly

The documents can be output in a variety of formats (JIRA, docx and PDF). This is acheived by running one of 

`make-docx`, `make-jira`, `make-pdf` 

Each script all accepts the same arguments

- **First argument**: The folder (relative) that you want to generate the documents for

- **Second argument**: The name of the file that you wish to be output.

So to build the architecture documentation as a PDF you might run: 

```bash
make-pdf architecture my-project
```

The above command would output a PDF `my-project.pdf` based on the `architecture` folder.
