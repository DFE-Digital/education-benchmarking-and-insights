import os

from fastapi import FastAPI

app = FastAPI()


@app.get("/")
async def main_route():
    return {"message": f'Hello World! Environment - {os.environ.get("FASTAPI_ENV")}'}
