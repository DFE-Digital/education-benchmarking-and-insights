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

### Setting up .env file

In the route of the `data-pipelines` repository there is an `.env-example` folder which shows the parameters that are required.

Ensure you have created a copy of this file named `.env` and filled the parameter value placeholders with the required values. These values will be available from the azure portal.

However for local development assuming azurite, you can use the following values: 



### Running the pipeline   

To running the API in Dev Mode:

    make dev

or 

    poetry run python src/main.py


### Createing and running Docker images

Build images with:

        docker build --tag fbit-data-pipeline --file docker/Dockerfile . 

or 

        make build 

The Dockerfile uses multi-stage builds to run lint and test stages before building the production stage.  If linting or testing fails the build will fail.

You can stop the build at specific stages with the `--target` option:

        docker build --name fbit-data-pipeline --file docker/Dockerfile . --target <stage>

For example we wanted to stop at the **test** stage:

        docker build --tag fbit-data-pipeline --file docker/Dockerfile --target test .

We could then get a shell inside the container with:

        docker run -it fbit-data-pipeline:latest bash

If you do not specify a target the resulting image will be the last image defined which in our case is the 'production' image.

To manually push to Azure container registry you can use the following command

    az login
    az account set {subscription name}
    az acr build --image fbit-data-pipeline:latest --registry {registry name} --file ./docker/Dockerfile .
    az containerapp up -g {resource group} -n fbit-data-pipeline --logs-workspace-id {log analytics workspace id} --image {registry name}.azurecr.io/fbit-data-pipeline:latest