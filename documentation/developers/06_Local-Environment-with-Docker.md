# Local Environment with Docker

To simplify local development, the FBIT repository includes a centralized Docker Compose setup. This allows you to quickly stand up the necessary backing services (Database, Storage, Caching, etc.) required to run the Platform APIs, Data Pipeline, and Web application locally.

## Prerequisites

1. Install [Docker Desktop](https://www.docker.com/products/docker-desktop/) (or Docker Engine if on Linux).
2. Ensure Docker is running.

## Quick Start

The shared Docker Compose configuration is located in the `docker` directory at the root of the repository.

1. Navigate to the `docker` directory:

   ```sh
   cd docker
   ```

2. Set up the Redis password. The compose file expects a `redis.env` file containing a `REDIS_PASSWORD`:

   ```sh
   echo "REDIS_PASSWORD=a_password_of_your_choice" > redis.env
   ```

3. Start the services in detached mode:

   ```sh
   docker-compose up -d
   # Or depending on your Docker version:
   docker compose up -d
   ```

## Services Provided

The `docker-compose.yml` file provisions the following services:

### 1. Azurite (Azure Storage Emulator)

Provides local emulation for Azure Blob Storage, Queue Storage, and Table Storage.

- **Image:** `mcr.microsoft.com/azure-storage/azurite`
- **Ports:**
  - Blob: `10000`
  - Queue: `10001`
  - Table: `10002`
- **Interacting:** Use [Azure Storage Explorer](https://azure.microsoft.com/en-us/products/storage/storage-explorer/) and connect to the local emulator.

### 2. Azure SQL Edge

Provides a lightweight local SQL Server instance.

- **Image:** `mcr.microsoft.com/azure-sql-edge`
- **Port:** `1433`
- **Credentials:**
  - **User ID:** `sa`
  - **Password:** `mystrong!Pa55word`
- **Interacting:** Connect using tools like Azure Data Studio, SQL Server Management Studio (SSMS), DataGrip, or `sqlcmd`.

### 3. Redis

Provides distributed caching for the Platform APIs.

- **Image:** `redis:6.2-alpine`
- **Port:** `6379`
- **Credentials:**
  - **Password:** The password you defined in `redis.env`
- **Debugging Locally:**
  To debug or view Redis commands, you can attach to the container:
  
  ```sh
  # Open redis-cli inside the container
  docker exec -it redis redis-cli -a a_password_of_your_choice
  ```

  Once connected, you can run commands like `GET test:key`, `DEL test:key`, or `MONITOR` to watch incoming traffic.

### 4. Data Pipeline Worker

Runs the Python data pipeline worker locally.

- **Build Context:** Uses the `data-pipeline` folder and builds from `pipeline-worker/Dockerfile`.
- **Note on Code Changes:** Because this service builds an image from the `data-pipeline` source, Docker Compose will *not* automatically rebuild the image on code changes. If you modify files in the `data-pipeline/src` directory, you need to rebuild the service:

  ```sh
  docker compose build pipeline
  docker compose up -d pipeline
  ```

## Teardown

To stop the services and remove the containers, run the following from the `docker` directory:

```sh
docker-compose down
```

*Note: Data volumes for SQL, Azurite, and Redis are mapped to local folders within the `docker` directory (e.g., `docker/sql/data`, `docker/azurite/data`). This means your data will persist between restarts. If you wish to completely wipe your local data, you can delete these mapped directories after stopping the containers.*
