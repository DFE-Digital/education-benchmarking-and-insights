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

### Setting up .env file

In the route of the `data-pipelines` repository there is an `.env-example` folder which shows the parameters that are required.

Ensure you have created a copy of this file named `.env` and filled the parameter value placeholders with the required values. These values will be available from the azure portal.

However, for local development assuming azurite, you can use the following values:

    STORAGE_CONNECTION_STRING=DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;
    WORKER_QUEUE_NAME=data-pipeline-job-start
    COMPLETE_QUEUE_NAME=data-pipeline-job-finished
    RAW_DATA_CONTAINER=raw

### Running the pipeline

To running the API in Dev Mode:

    make run-pipeline

However, this will only run the pipeline and will fail using the above environment parameters, it is trying to connect to a local based storage. To this end there is a docker compose script that will run the following:

* Azurite with default settings
* FBIT data pipeline in - test mode (`make run-pipeline-dev-mode`)

> Note: `Dev mode` - means that rather than checking for a message and then terminating if there are no messages on the queue, the container, will loop, processing messages on the queue one at a time.

### Testing locally

Once the environment is set up as above, and you can successfully run the pipeline it is possible to run the unit and e2e tests locally.
To run the unit tests run

    make unit-test

To run the E2E tests, you first need to have the pipeline running in a docker container. The required containers can be run using `docker compose`

The docker compose file is located in the `docker` directory. Navigate to this directory and run

    docker compose up -d

this will start 3 docker containers. `Azurite`, `Sql Server` and the `pipeline` containers. Once the containers are up and running the end to end tests can be run using

    make e2e-test-local

> Note: Docker compose will *not* rebuild images on code change. So if you change any of the files in the `src` directory then the `fbit-services-pipeline:latest` image will need to be deleted and recreated.

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
