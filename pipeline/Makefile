install:
	poetry install
	
dev:
	poetry run uvicorn src.main:app --reload --host localhost --port 8000 

build: 
	docker build --tag fbit-data-pipeline --file docker/Dockerfile .

run-docker:
	docker run -p 8000:8000 fbit-data-pipeline:latest
	