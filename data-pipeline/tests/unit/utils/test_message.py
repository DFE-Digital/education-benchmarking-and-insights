import pytest

from pipeline.utils.message import (
    DefaultMessage,
    MessageType,
    PipelineEarlyExit,
    check_run_until_gate,
    get_message_type,
)


@pytest.mark.parametrize(
    "body,expected",
    [
        ({}, MessageType.Default),
        ({"payload": {"kind": "ComparatorSetPayload"}}, MessageType.DefaultUserDefined),
        ({"payload": {"kind": "CustomDataPayload"}}, MessageType.Custom),
    ],
)
def test_message(body: dict, expected: MessageType):
    result = get_message_type(message=body)

    assert result == expected


def test_check_run_until_gate_matches():
    with pytest.raises(PipelineEarlyExit) as exc_info:
        check_run_until_gate("pre-processing", "pre-processing")
    assert "Pipeline stopped after pre-processing as requested by runUntil." in str(
        exc_info.value
    )


def test_check_run_until_gate_different():
    # Should not raise any exception
    check_run_until_gate("pre-processing", "comparators")


def test_check_run_until_gate_none():
    # Should not raise any exception
    check_run_until_gate("pre-processing", None)


def test_default_message_validation_success():
    payload = {
        "runId": 2026,
        "year": {"aar": 2025, "cfr": 2026, "bfr": 2026, "s251": 2025},
        "runUntil": "pre-processing",
        "generateTransparencyFilesAndPrecursorFiles": True,
    }
    msg = DefaultMessage(payload)
    assert msg.run_id == "2026"
    assert msg.aar_year == 2025
    assert msg.cfr_year == 2026
    assert msg.bfr_year == 2026
    assert msg.s251_year == 2025
    assert msg.run_until == "pre-processing"
    assert msg.generate_cfr_transparency_file is True
    assert msg.derive_laa_risk_scores is False


def test_default_message_defaults():
    payload = {
        "runId": "2026-run",
        "year": {"aar": "2025", "cfr": "2026", "bfr": "2026", "s251": "2025"},
    }
    msg = DefaultMessage(payload)
    assert msg.run_id == "2026-run"
    assert msg.generate_cfr_transparency_file is False
    assert msg.run_until is None
    assert msg.derive_laa_risk_scores is False


def test_default_message_missing_run_id():
    payload = {"year": {"aar": 2025, "cfr": 2026, "bfr": 2026, "s251": 2025}}
    with pytest.raises(ValueError) as exc:
        DefaultMessage(payload)
    assert "runId is required" in str(exc.value)


def test_default_message_explicit_none_run_id():
    payload = {
        "runId": None,
        "year": {"aar": 2025, "cfr": 2026, "bfr": 2026, "s251": 2025},
    }
    with pytest.raises(ValueError) as exc:
        DefaultMessage(payload)
    assert "runId is required" in str(exc.value)


def test_default_message_empty_string_run_id():
    payload = {
        "runId": "",
        "year": {"aar": 2025, "cfr": 2026, "bfr": 2026, "s251": 2025},
    }
    with pytest.raises(ValueError) as exc:
        DefaultMessage(payload)
    assert "runId is required" in str(exc.value)


def test_default_message_invalid_year_type():
    payload = {"runId": 2026, "year": "invalid"}
    with pytest.raises(ValueError) as exc:
        DefaultMessage(payload)
    assert "year must be a dictionary" in str(exc.value)


def test_default_message_missing_years():
    payload = {"runId": 2026, "year": {"aar": 2025}}
    with pytest.raises(ValueError) as exc:
        DefaultMessage(payload)
    assert "Missing required years" in str(exc.value)


def test_default_message_invalid_year_values():
    payload = {
        "runId": 2026,
        "year": {"aar": "abc", "cfr": 2026, "bfr": 2026, "s251": 2025},
    }
    with pytest.raises(ValueError) as exc:
        DefaultMessage(payload)
    assert "Year fields must be integers" in str(exc.value)


def test_default_message_invalid_run_until():
    payload = {
        "runId": 2026,
        "year": {"aar": 2025, "cfr": 2026, "bfr": 2026, "s251": 2025},
        "runUntil": "invalid-stage",
    }
    with pytest.raises(ValueError) as exc:
        DefaultMessage(payload)
    assert "Invalid runUntil value" in str(exc.value)


def test_default_message_invalid_generate_cfr():
    payload = {
        "runId": 2026,
        "year": {"aar": 2025, "cfr": 2026, "bfr": 2026, "s251": 2025},
        "generateTransparencyFilesAndPrecursorFiles": "not-a-bool",
    }
    with pytest.raises(ValueError) as exc:
        DefaultMessage(payload)
    assert "generateTransparencyFilesAndPrecursorFiles value" in str(exc.value)


def test_default_message_invalid_derive_laa():
    payload = {
        "runId": 2026,
        "year": {"aar": 2025, "cfr": 2026, "bfr": 2026, "s251": 2025},
        "deriveLaaRiskScores": "not-a-bool",
    }
    with pytest.raises(ValueError) as exc:
        DefaultMessage(payload)
    assert "deriveLaaRiskScores value" in str(exc.value)


def test_default_message_derive_laa_true():
    payload = {
        "runId": 2026,
        "year": {"aar": 2025, "cfr": 2026, "bfr": 2026, "s251": 2025},
        "deriveLaaRiskScores": True,
    }
    msg = DefaultMessage(payload)
    assert msg.derive_laa_risk_scores is True
