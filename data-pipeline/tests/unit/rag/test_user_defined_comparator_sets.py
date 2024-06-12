import pathlib

import pandas as pd
import pytest

from src.pipeline.comparator_sets import prepare_data
from src.pipeline.rag import compute_user_defined_rag


msg_payload = {
    "jobId": "ba4f078e-f32f-467f-a848-09cf9d58c8df",
    "type": "comparator-set",
    "runType": "default",
    "runId": "b643f547-5d8b-401e-862d-265543786a41",
    "year": 2023,
    "urn": "145799",
    "payload": {
        "kind": "ComparatorSetPayload",
        "set": [
            "143058",
            "132247",
            "144798",
            "147637",
            "144241",
            "111180",
            "111385",
            "111299",
            "111363",
            "111376",
            "148456",
            "145389",
            "145710",
            "146417",
            "147636",
            "143072",
            "111298",
            "111315",
            "111367",
            "111370",
            "111309",
            "145387",
            "145388",
            "111318",
            "111369",
            "131349",
            "111305",
            "111307",
            "111308",
            "145799",
        ],
    },
}


@pytest.mark.skip(reason="requires representative data")
def test_run_user_defined_rag():
    """
    TODO: this requires a representative, pre-processed
    `all_schools.parquet`.
    """
    all_schools = (
        pathlib.Path(__file__).parent.parent.parent.parent / "all_schools.parquet"
    )
    all_schools = prepare_data(pd.read_parquet(all_schools))

    result = list(
        compute_user_defined_rag(
            data=all_schools,
            target_urn=int(msg_payload["urn"]),
            set_urns=list(map(int, msg_payload["payload"]["set"])),
        )
    )

    assert len(result) == 42
