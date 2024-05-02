install:
	poetry install
	
dev:
	poetry run python src/pipeline/main.py

dev-test-mode:
	poetry run python src/pipeline/main.py test

build: 
	docker build $(tags) --file docker/pipeline-worker/Dockerfile .

unit-test:
	poetry run coverage run --rcfile ./pyproject.toml -m pytest --junitxml=tests/output/test-output.xml ./tests/unit;\
	poetry run coverage report --fail-under 95 ;\
	poetry run coverage html ;\
	poetry run coverage xml

e2e-test:
	poetry run coverage run --rcfile ./pyproject.toml -m pytest --junitxml=tests/output/test-output.xml ./tests/e2e;\

lint:
	poetry run black --config ./pyproject.toml src tests;\
	poetry run isort --settings-path ./pyproject.toml **/*.py

lint-tf: 
	terraform fmt --diff --recursive

checkov:
	checkov -d terraform/

run-docker:
	docker run -d -p 8000:8000 --name fbit-data-pipeline  fbit-data-pipeline:latest

