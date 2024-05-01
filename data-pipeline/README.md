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

or 
    
    poetry install

This will install the dependencies and allow the project to be run. 

> Note: If the dependencies have changed significantly since the last install then peotry will detect this and inform the user. In this case you should run `poetry lock` to generate the lock file. At this point you can re-run the above install command. 

Finally load up the virtual environment run: 

    poetry shell

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

    make dev

or 

    poetry run python src/main.py

However, this will only run the pipeline and will fail using the above environment parameters, it is trying to connect to a local based storage. To this end there is a docker compose script that will run the following:

* Azurite with default settings
* FBIT data pipeline in - test mode (`make dev-test-mode`)

> Note: `Test mode` - means that rather than checking for a message and then terminating if there are no messages on the queue, the container, will loop, processing messages on the queue one at a time.



### Creating and running Docker images

Build images with:

        docker build --tag {insert the tag of the build} --file docker/Dockerfile . 

or 

        make build tags="--tag {tag1} --tag {tag2}"

We could then get a shell inside the container with:

        docker run -it {tag} bash

If you do not specify a target the resulting image will be the last image defined which in our case is the 'production' image.

To manually push to Azure container registry you can use the following command, however this will require a logged in Azure CLI. Depending on your DfE account type or device type this may not be possible from your local machine; if this is the case then you can always use the [Azure cloud shell](https://learn.microsoft.com/en-gb/azure/cloud-shell/overview)

    az login
    az account set {subscription name}
    az acr build --image fbit-data-pipeline:latest --registry {registry name} --file ./docker/Dockerfile .
    az containerapp up -g {resource group} -n fbit-data-pipeline --logs-workspace-id {log analytics workspace id} --image {registry name}.azurecr.io/fbit-data-pipeline:latest