# Dockerfile
# Uses multi-stage builds requiring Docker 17.05 or higher
# See https://docs.docker.com/develop/develop-images/multistage-build/
FROM python:3.13-slim AS python-base

ENV PYTHONUNBUFFERED=1 \
    PYTHONDONTWRITEBYTECODE=1 \
    PIP_NO_CACHE_DIR=off \
    PIP_DISABLE_PIP_VERSION_CHECK=on \
    PIP_DEFAULT_TIMEOUT=100 \
    POETRY_HOME="/opt/poetry" \
    POETRY_VIRTUALENVS_IN_PROJECT=true \
    POETRY_NO_INTERACTION=1 \
    PYSETUP_PATH="/opt/pysetup" \
    VENV_PATH="/opt/pysetup/.venv" \
    POETRY_VERSION=1.8.2

ENV PATH="$POETRY_HOME/bin:$VENV_PATH/bin:$PATH"

# builder-base is used to build dependencies
FROM python-base AS builder-base

RUN apt-get update \
    && apt-get install --no-install-recommends -y \
        curl \
        build-essential \
        # libatlas-base-dev \
        gnupg2 \
        g++ && \
        rm -rf /var/lib/apt/lists/*

# Install Poetry - respects $POETRY_VERSION & $POETRY_HOME
RUN curl -sSL https://install.python-poetry.org | python

# Install and cache runtime dependencies with poetry
WORKDIR $PYSETUP_PATH
COPY ./poetry.lock ./pyproject.toml ./
RUN poetry install --only=main --no-root && \
    poetry cache clear pypi --all

# 'production' stage uses the clean 'python-base' stage and copies
# in only our runtime deps that were installed in the 'builder-base'
FROM python-base AS production

RUN apt-get update && \
    apt-get install --no-install-recommends -y \
        curl \
        gnupg2 \
        unixodbc-dev && \
    # Add Microsoft repository
    curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg && \
    curl -fsSL https://packages.microsoft.com/config/debian/12/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    # Install Microsoft SQL Server tools
    DEBIAN_FRONTEND=noninteractive ACCEPT_EULA=Y apt-get install --no-install-recommends -y \
        msodbcsql18 \
        mssql-tools18 && \
    # Clean up package cache to reduce image size
    apt-get clean && \
    rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*

# Add SQL Server tools to PATH for all users
ENV PATH="$PATH:/opt/mssql-tools18/bin"

# Copy virtual environment from builder stage
COPY --from=builder-base $VENV_PATH $VENV_PATH
COPY ./docker/pipeline-worker/docker-entrypoint.sh /docker-entrypoint.sh
COPY ./docker/pipeline-worker/odbc.ini /odbc.ini
COPY ./src/pipeline /pipeline

RUN chmod +x /docker-entrypoint.sh && \
    odbcinst -i -s -f /odbc.ini -l && \
    cat /etc/odbc.ini

ENTRYPOINT [ "/docker-entrypoint.sh" ]
CMD [ "python", "-m", "pipeline.main"]

LABEL description="FBIT data pipeline worker" \
      python.version="${PYTHON_VERSION}" \
      poetry.version="${POETRY_VERSION}"