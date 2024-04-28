install:
	poetry install
	
dev:
	poetry run python src/main.py

build: 
	docker build $(tags) --file docker/pipeline-worker/Dockerfile .

test:
	poetry run coverage run --rcfile ./pyproject.toml -m pytest --junitxml=tests/output/test-output.xml ./tests;\
	poetry run coverage report --fail-under 95 ;\
	poetry run coverage html ;\
	poetry run coverage xml

lint:
	poetry run black --config ./pyproject.toml src tests;\
	poetry run isort --settings-path ./pyproject.toml **/*.py

lint-tf: 
	terraform fmt --diff --recursive

checkov:
	checkov -d terraform/

run-docker:
	docker run -d -p 8000:8000 --name fbit-data-pipeline  fbit-data-pipeline:latest

