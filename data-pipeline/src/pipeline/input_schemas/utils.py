from collections.abc import Iterable
from typing import TypeVar

K = TypeVar("K")
V = TypeVar("V")


def evolve_schema(
    base: dict[K, V],
    *,
    removals: Iterable[K] | None = None,
    renames: dict[K, K | tuple[K, V]] | None = None,
    additions: dict[K, V] | None = None,
) -> dict[K, V]:
    """
    Derives an evolved schema dictionary from a base dictionary in a single pass.

    Preserves existing key order, applies key (and optional value) renames,
    filters out removals, and appends additions.
    """
    drop_set = set(removals or ())
    rename_map = renames or {}

    out: dict[K, V] = {}
    for k, v in base.items():
        if k in drop_set:
            continue
        if k in rename_map:
            target = rename_map[k]
            if isinstance(target, tuple):
                new_k, new_v = target
            else:
                new_k, new_v = target, v
            out[new_k] = new_v
        else:
            out[k] = v

    if additions:
        out |= additions

    return out
