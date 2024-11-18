# Financial Benchmarking and Insights Data Pipeline

This folder contains all of the code, build and terraform required to run and deploy the FBIT data pipelines.

For more information on the FBIT pipelines see either

* [The documentation folder in this repository](https://github.com/DFE-Digital/education-benchmarking-and-insights/tree/main/documentation)
* Or in the FBIT sharepoint technical folder.

## Developers

### Dependencies

* Python > 3.11
* [Poetry](https://python-poetry.org/docs/)
* A correctly setup .env file in root of the `data-pipeline` folder

Once the above dependencies are installed and working correctly we can install the required project dependencies by running:

    make install

This will install the dependencies and allow the project to be run.

> Note: If the dependencies have changed significantly since the last install then peotry will detect this and inform the user. In this case you should run `poetry lock` to generate the lock file. At this point you can re-run the above install command.

#### Installing dependencies on Windows

These steps will avoid SSL errors due to DfE kit/VPN.

1. Open PowerShell terminal as Administrator
1. Install Chocolatey:

    * `Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('<https://community.chocolatey.org/install.ps1>'))`

1. Install Python:

    * `choco install python`

1. Install OpenSSL:

    * `choco install openssl`
    * Close and re-open the terminal once complete

1. [Compile](https://mattferderer.com/fix-git-self-signed-certificate-in-certificate-chain-on-windows) or locate `ca-bundle.crt`, ensuring that the following certificates are included:

    * `decryption.education.gov.uk`
    * `PKI-SUB-CA01`

1. Convert `.crt` to `.pem`:

    * `openssl x509 -in .\ca-bundle.crt -out .\ca-bundle.pem -outform pem`

1. Install Poetry:

    * `(Invoke-WebRequest -Uri https://install.python-poetry.org -UseBasicParsing).Content | py -`

1. Configure the Poetry repository:

    * `poetry config repositories.FPHO https://files.pythonhosted.org`
    * `poetry config certificates.FPHO.cert .\ca-bundle.pem`

1. From data-pipeline folder, perform the installation:

    * `poetry install`

### Running the pipeline

To run the pipeline locally, follow these steps:

1. Set Up an Azurite instance:

    To start an Azurite instance, use the following command:

    ```sh
    docker run \
        --name dfe-azurite  \
        --rm \
        --publish 10000:10000 \
        --publish 10001:10001 \
        --publish 10002:10002 \
        --detach \
        mcr.microsoft.com/azure-storage/azurite
    ```

    For PowerShell:

    ```ps1
    docker run `
        --name dfe-azurite `
        --rm `
        --publish 10000:10000 `
        --publish 10001:10001 `
        --publish 10002:10002 `
        --detach `
        mcr.microsoft.com/azure-storage/azurite
    ```

    Using Azure Storage Explorer (default settings), connect to Azurite and manually create the following resources:

    Containers

    ```
    comparator-sets
    ```

    ```
    metric-rag
    ```

    ```
    pre-processed
    ```

    ```
    raw
    ```

    Queues

    ```
    data-pipeline-job-default-start
    ```

    ```
    data-pipeline-job-custom-start
    ```

    ```
    data-pipeline-job-finished
    ```

    ```
    data-pipeline-job-dlq
    ```

    Upload files into the `raw` container, following this directory structure:

    ```raw/default/<year>```

2. Set Up a SQL Server Instance

    To create a SQL Server instance, use the following command:

    ```sh
    docker run \
        --name dfe-sql-server \
        --rm \
        --publish 1433:1433 \
        --env SA_PASSWORD='mystrong!Pa55word' \
        --env ACCEPT_EULA=Y \
        --detach \
        mcr.microsoft.com/azure-sql-edge
    ```

    For PowerShell:

    ```ps1
    docker run `
        --name dfe-sql-server `
        --rm `
        --publish 1433:1433 `
        --env SA_PASSWORD='mystrong!Pa55word' `
        --env ACCEPT_EULA=Y `
        --detach `
        mcr.microsoft.com/azure-sql-edge
    ```

3. Create the database

    To create the required database, use [`sqlcmd`](https://learn.microsoft.com/en-us/sql/tools/sqlcmd/sqlcmd-utility):

    ```sh
    sqlcmd -S tcp:127.0.0.1,1433 -U sa -P 'mystrong!Pa55word' -Q 'CREATE DATABASE data;'
    ```

    Then, apply migration scripts using the [core-infrastructure project](../core-infrastructure/README.md) to set up the required tables.

    Set the following program arguments to target this instance:

    ```
    -c "Server=localhost,1433;Database=data;User Id=SA;Password=mystrong!Pa55word;Encrypt=False;
    ```

4. Create an `.env` file:

    Configure an `.env` file as follows:

    ```txt
    STORAGE_CONNECTION_STRING="DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127
    .0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;"
    RAW_DATA_CONTAINER="raw"
    DB_NAME="data"
    DB_PORT="1433"
    DB_ARGS="Encrypt=no;TrustServerCertificate=no;Connection Timeout=30"
    DB_HOST="127.0.0.1"
    DB_USER="sa"
    DB_PWD='mystrong!Pa55word'
    ENV="dev"
    ```

5. Install the Microsoft ODBC Driver for SQL Server

    Install [Microsoft ODBC driver 18 for SQL Server](https://learn.microsoft.com/en-us/sql/connect/odbc/download-odbc-driver-for-sql-server?view=sql-server-ver16):

6. Run the Pipeline

    From data-pipeline directory run the pipeline:

    ```sh
    poetry run python -m src.pipeline.main
    ```

    Once the pipeline is running, start processing files placed in the `raw` container by adding the following message to the `data-pipeline-job-default-start` queue:

    ```
    {"type":"default","year":<year>}
    ```

> Note: There is a docker compose script that will start Azurite, SQL server, and the FBIT pipeline that can be run from the `docker` directory using `docker compose up -d`. Docker compose will _not_ rebuild images on code change. So if you change any of the files in the `src` directory then the `fbit-services-pipeline:latest` image will need to be deleted and recreated. However depending on your machine and set up you may have issues running everything within Docker due to memory limitations and SSL errors. Following the steps outlined above is preferred.

### Testing locally

Once the environment is set up as above, and you can successfully run the pipeline it is possible to run the unit and e2e tests locally.
To run the unit tests run

```sh
poetry run coverage run --rcfile ./pyproject.toml -m pytest --junitxml=tests/output/test-output.xml ./tests/unit
```

Make:

```
make unit-test
```

To run the E2E tests, you first need to have the pipeline running in a docker container.

```
make e2e-test-local
```

### Creating and running Docker images

Build images with:

        make build tags="--tag {tag1} --tag {tag2}"

We could then get a shell inside the container with:

        docker run -it {tag} bash

If you do not specify a target the resulting image will be the last image defined which in our case is the 'production' image.

To manually push to Azure container registry you can use the following command, however this will require a logged in Azure CLI. Depending on your DfE account type or device type this may not be possible from your local machine; if this is the case then you can always use the [Azure cloud shell](https://learn.microsoft.com/en-gb/azure/cloud-shell/overview)

    az login
    az account set {subscription name}
    az acr build --image fbit-data-pipeline:latest --registry {registry name} --file ./docker/Dockerfile .
    az containerapp up -g {resource group} -n fbit-data-pipeline --logs-workspace-id {log analytics workspace id} --image {registry name}.azurecr.io/fbit-data-pipeline:latest
