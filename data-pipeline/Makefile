install:
	poetry install
	
<<<<<<< HEAD
dev:
	poetry run python src/main.py
=======
run-pipeline:
	poetry run python -m src.pipeline.main

run-pipeline-test-mode:
	poetry run python -m src.pipeline.main test
>>>>>>> d6f264ffa350a8ff4b76b5603924abe6419a5f04

build: 
	docker build $(tags) --file docker/pipeline-worker/Dockerfile .

<<<<<<< HEAD
test:
	poetry run coverage run --rcfile ./pyproject.toml -m pytest --junitxml=tests/output/test-output.xml ./tests;\
=======
unit-test:
	poetry run coverage run --rcfile ./pyproject.toml -m pytest --junitxml=tests/output/test-output.xml ./tests/unit;\
>>>>>>> d6f264ffa350a8ff4b76b5603924abe6419a5f04
	poetry run coverage report --fail-under 95 ;\
	poetry run coverage html ;\
	poetry run coverage xml

<<<<<<< HEAD
=======
e2e-test:
	poetry run coverage run --rcfile ./pyproject.toml -m pytest --junitxml=tests/output/test-output.xml ./tests/e2e;\

>>>>>>> d6f264ffa350a8ff4b76b5603924abe6419a5f04
lint:
	poetry run black --config ./pyproject.toml src tests;\
	poetry run isort --settings-path ./pyproject.toml **/*.py

lint-tf: 
	terraform fmt --diff --recursive

checkov:
	checkov -d terraform/

run-docker:
	docker run -d -p 8000:8000 --name fbit-data-pipeline  fbit-data-pipeline:latest

<<<<<<< HEAD
=======
clear-notebooks:
	poetry run jupyter nbconvert --clear-output --inplace workbooks/*.ipynb

>>>>>>> d6f264ffa350a8ff4b76b5603924abe6419a5f04
