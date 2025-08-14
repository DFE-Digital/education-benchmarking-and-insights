"""
Purpose: This file exports a txt file of local data pipeline input and outputs hashes to assure data pipeline changes end-to-end.
Partial updates eg 2025 CFR, 2024 BFR mean that multiple years might need to be hashed at once.

Example usage
-------------

* Manually set `data_years`
* bash```
poetry run python tests/e2e/export_data_hashes.py
```
* switch to new implementation, set `new_code_flag` to True
* bash```
poetry run python tests/e2e/export_data_hashes.py
diff e2e_hashes_2024-2025.csv e2e_hashes_2024-2025_new_code.csv
```

"""

import csv
import hashlib
import os
from typing import Dict

from dotenv import load_dotenv

from pipeline.utils.storage import get_blob_service_client

data_years = ["2024", "2025"]
new_code_flag = True

new_code = "_new_code" if new_code_flag else ""
year_slug = "-".join(data_years)
output_file_name = f"e2e_hashes_{year_slug}{new_code}.csv"
blob_folders: list[tuple[str, str]] = []
for year in data_years:
    blob_folders += [
        ("raw", f"default/{year}"),
        ("pre-processed", f"default/{year}"),
        ("comparator-sets", f"default/{year}"),
        ("metric-rag", f"default/{year}"),
    ]


def get_blob_hashes(
    container_name: str,
    prefix: str,
) -> Dict[str, str]:
    """
    Get hashes and filenames for all files in an Azure blob container folder.

    Args:
        container_name: Name of the blob container
        prefix: Folder prefix to filter blobs

    Returns:
        Dictionary mapping blob names to their SHA256 hashes

    Example:
        hashes = get_blob_hashes("raw", "default/2025/")
        # Returns: {"2025/data1.parquet": "abc123...", "2025/data2.parquet": "def456..."}
    """

    blob_service_client = get_blob_service_client()
    container_client = blob_service_client.get_container_client(container_name)
    blob_hashes = {}

    try:
        # List all blobs with the specified prefix
        blob_list = container_client.list_blobs(name_starts_with=prefix)

        for blob in blob_list:
            # Skip directories (blobs ending with '/')
            if blob.name.endswith("/"):
                continue

            try:
                # Download blob content
                blob_client = container_client.get_blob_client(blob.name)
                blob_data = blob_client.download_blob().readall()

                # Calculate SHA256 hash
                hash_sha256 = hashlib.sha256(blob_data).hexdigest()
                dict_key = f"{container_name}/{blob.name}"
                blob_hashes[dict_key] = hash_sha256

            except Exception as e:
                print(f"Error processing blob {blob.name}: {str(e)}")
                # You might want to raise or handle this differently
                continue

    except Exception as e:
        print(f"Error accessing container {container_name}: {str(e)}")
        raise

    return blob_hashes


def dict_to_csv(data_dict, filename):
    """
    Write a dictionary to CSV file with key,value columns.

    Args:
        data_dict: Dictionary to write
        filename: Output CSV filename
    """
    with open(filename, "w", newline="") as f:
        writer = csv.writer(f)
        writer.writerow(["blob_file", "hash"])  # Header
        for key, value in data_dict.items():
            writer.writerow([key, value])


if __name__ == "__main__":
    dotenv_path = os.path.join("tests/e2e/.env.local")
    load_dotenv(dotenv_path)

    all_hashes = {}
    for container, folder in blob_folders:
        print(f"Collecting hashes from {container}/{folder}")
        all_hashes.update(get_blob_hashes(container, folder))
    dict_to_csv(all_hashes, output_file_name)
    print(f"Written to {output_file_name}")
