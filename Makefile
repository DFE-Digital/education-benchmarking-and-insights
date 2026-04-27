# This is the default target when running 'make' without arguments
.PHONY: help up down build-pipeline lint-md kill-dotnet tf-check set-env

# Capture any arguments passed after the target
# This allows 'make set-env all local' instead of 'make set-env ARGS="all local"'
ifeq (set-env,$(firstword $(MAKECMDGOALS)))
  RUN_ARGS := $(wordlist 2,$(words $(MAKECMDGOALS)),$(MAKECMDGOALS))
  $(eval $(RUN_ARGS):;@:)
endif

help: ## Show this help
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-30s\033[0m %s\n", $$1, $$2}'

up: ## Start local Docker dependencies (Azurite, SQL Server, Redis)
	docker-compose -f docker/docker-compose.yml up -d

down: ## Stop local Docker dependencies
	docker-compose -f docker/docker-compose.yml down

build-pipeline: ## Force rebuild of the data-pipeline container
	docker-compose -f docker/docker-compose.yml build pipeline

lint-md: ## Lint and fix markdown files
	npx markdownlint-cli2 --fix "**/*.md" --verbose

kill-dotnet: ## Kill all running dotnet processes
	@echo "Stopping dotnet processes..."
	-@pkill -f dotnet 2>/dev/null || taskkill //IM dotnet.exe //F //T 2>/dev/null || true

tf-check: ## Run the terraform helper tool (format, validate, lint, docs)
	dotnet run scripts/terraform-tool/app.cs

set-env: ## Switch environment (usage: make set-env all local)
	dotnet run scripts/env-tool/app.cs $(RUN_ARGS)
