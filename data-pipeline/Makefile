install:
	poetry install
	
run-pipeline:
	poetry run python -m src.pipeline.main

run-pipeline-dev-mode:
	export ENV=dev;\
    poetry run python -m src.pipeline.main

build: 
	docker build $(tags) --file docker/pipeline-worker/Dockerfile .

unit-test:
	poetry run coverage run --rcfile ./pyproject.toml -m pytest --junitxml=tests/output/test-output.xml ./tests/unit;\
	EXIT_CODE=$$?;\
	poetry run coverage report --fail-under 65 ;\
	EXIT_CODE=$$?;\
	poetry run coverage html ;\
	poetry run coverage xml ;\
	exit $$EXIT_CODE

e2e-test:
	poetry run coverage run --rcfile ./pyproject.toml -m pytest -s --junitxml=tests/output/test-output.xml ./tests/e2e;\

e2e-test-local:
	export ENV=Local;\
	poetry run coverage run --rcfile ./pyproject.toml -m pytest -s --junitxml=tests/output/test-output.xml ./tests/e2e;\

lint:
	poetry run black --config ./pyproject.toml src/ tests/;\
	poetry run isort --settings-path ./pyproject.toml src/ tests/

lint-tf: 
	terraform fmt --diff --recursive

checkov:
	checkov -d terraform/

run-docker:
	docker run -d -p 8000:8000 --name fbit-data-pipeline  fbit-data-pipeline:latest

clear-notebooks:
	poetry run jupyter nbconvert --clear-output --inplace workbooks/*.ipynb

