install:
	poetry install
	
dev:
	poetry run uvicorn src.main:app --reload --host localhost --port 8000 



build: 
	docker build --tag fbit-data-pipeline --file docker/Dockerfile .

test:
	coverage run --rcfile ./pyproject.toml -m pytest ./tests ;\
	coverage report --fail-under 95

lint:
	black --config ./pyproject.toml src tests ;\
	isort --settings-path ./pyproject.toml **/*.py

run-docker:
	docker run -d -p 8000:8000 --name fbit-data-pipeline  fbit-data-pipeline:latest

