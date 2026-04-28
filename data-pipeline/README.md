# Financial Benchmarking and Insights Data Pipeline

This folder contains all of the code, build and terraform required to run and deploy the FBIT data pipelines.

For more information on the FBIT pipelines see either

* [The documentation folder in this repository](https://github.com/DFE-Digital/education-benchmarking-and-insights/tree/main/documentation)
* Or in the FBIT sharepoint technical folder.

Within the FBIT service, there is an [Azure container app](terraform/container_apps.tf) for each type of run in the data pipeline (default/custom).
When a message is placed in a run queue, a worker container is triggered to spawn from the container app. The worker processes one message and if successful places the message in the completed queue.

## Development Standards

* **Pandas-First**: Use vectorized Pandas operations; avoid iterating over rows (`iterrows`) to maintain performance.
* **Parquet Intermediates**: Always save intermediate DataFrames to Blob Storage as Parquet to enable re-runs, debugging, and data lineage.
* **Schema Validation**: All raw data must be validated against `pipeline.input_schemas` before processing to catch source data regressions early.
* **Pure Logic Engines**: Keep calculation logic (RAG ranking, Similarity algorithms) independent of I/O to facilitate unit testing with mocks or local DataFrames.
* **Logging & Stats**: Utilize `setup_logger` and `stats_collector` for consistent observability and performance tracking across all pipeline stages.
* **Local Environment**: Rely on `make install` for setup and `.env` (derived from `.env-example`) for local configuration overrides.

## Anti-Patterns

* **In-Memory SQL Joins**: Avoid pulling massive DB tables to join in Pandas; push joins to the database or use indexed Parquet files from previous stages.
* **Hardcoded Year Logic**: Never hardcode academic or financial years; always derive them from the queue message payload or configuration.
* **Raw SQL Strings**: Do not write manual `INSERT` or `UPDATE` statements; use the existing SQLAlchemy-based abstractions in `pipeline.utils.database`.
* **Mixing I/O in Engines**: The core calculation engines (RAG, Comparators) should never call Azure SDKs or Database engines directly; pass data as DataFrames or Dictionaries.
* **Testing I/O in Unit Tests**: Do not write unit tests that require an active Azure Storage or SQL Server connection. Use the `tests/e2e` folder for integration tests and keep `tests/unit` strictly isolated using mocks or static Pandas DataFrames.

## Developers

### Dependencies

* Python > 3.11
* [Poetry](https://python-poetry.org/docs/)
* A correctly setup .env file in root of the `data-pipeline` folder
* [Pre-commit hooks](../documentation/developers/04_Pre-commit-Hooks.md) installed to ensure Python (Black) and Markdown linting checks run automatically on every commit.

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

1. Start the local dependencies:

    Start the backing services (Azurite and SQL Server) via the centralized Docker Compose setup.
    See the [Local Environment with Docker guide](../documentation/developers/06_Local-Environment-with-Docker.md) for instructions.

1. Add data to Azurite:

    Using Azure Storage Explorer (default settings), connect to the local Azurite emulator and manually upload the `default` folder from one of the live environments to the `raw` container in Azurite, say test.

1. Once the pipeline is running and there's data for the pipeline to process, trigger the pipeline by adding the following message to the `data-pipeline-job-default-start` queue as UTF-8:

    ```json
    {
      "type": "default",
      "runId": <year>,
        "year": {
            "aar": <year>,
            "cfr": <year>,
            "bfr": <year>,
            "s251": <year>
        }
    }
    ```

### Testing locally

Once the environment is set up as above, and you can successfully run the pipeline it is possible to run the unit and e2e tests locally.
To run the unit tests run

```sh
poetry run coverage run --rcfile ./pyproject.toml -m pytest --junitxml=tests/output/test-output.xml ./tests/unit
```

Make:

```sh
make unit-test
```

To run the E2E tests, you first need to have the pipeline running in a docker container.

```sh
make e2e-test-local
```

### Creating and running Docker images

Build images with:

```sh
make build tags="--tag {tag1} --tag {tag2}"
```

We could then get a shell inside the container with:

```sh
docker run -it {tag} bash
```

If you do not specify a target the resulting image will be the last image defined which in our case is the 'production' image.

To manually push to Azure container registry you can use the following command, however this will require a logged in Azure CLI.
Depending on your DfE account type or device type this may not be possible from your local machine; if this is the case then you
can always use the [Azure cloud shell](https://learn.microsoft.com/en-gb/azure/cloud-shell/overview)

```sh
az login
az account set {subscription name}
az acr build --image fbit-data-pipeline:latest --registry {registry name} --file ./pipeline-worker/Dockerfile .
az containerapp up -g {resource group} -n fbit-data-pipeline --logs-workspace-id {log analytics workspace id} --image {registry name}.azurecr.io/fbit-data-pipeline:latest
```
