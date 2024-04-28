install:
	poetry install
	
dev:
	poetry run python src/main.py

build: 
	docker build --tag fbit-data-pipeline --file docker/pipeline-worker/Dockerfile .

test:
	coverage run --rcfile ./pyproject.toml -m pytest ./tests;\
	coverage report --fail-under 95

test-ci:
	coverage run --rcfile ./pyproject.toml -m pytest ./tests ;\
	coverage report --fail-under 95 ;\
	coverage html ;\
	coverage xml

lint:
	black --config ./pyproject.toml src tests ;\
	isort --settings-path ./pyproject.toml **/*.py

lint-tf: 
	terraform fmt --diff --recursive

checkov:
	checkov -d terraform/

run-docker:
	docker run -d -p 8000:8000 --name fbit-data-pipeline  fbit-data-pipeline:latest

