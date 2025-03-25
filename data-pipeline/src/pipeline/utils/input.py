from collections import abc


class MultiKeyDefaultDict(abc.Mapping):
    """
    Variant of `dict` which should:

    - allow multiple keys to mapped to the same value
    - always return a (mandatory) default if a key is missing

    Intended to reduce multiple declarations which occur in a lot of
    input-file schemas.
    """

    def __init__(self, _dict: dict[tuple[int], dict], *, default: dict):
        self._dict = {}
        for key, value in _dict.items():
            for k in key:
                self._dict[k] = value
        self.default = default

    def __getitem__(self, key):
        return self._dict.get(key, self.default)

    def __iter__(self):
        return iter(self._dict)

    def __len__(self):
        return len(self._dict)

    def get(self, key, default=None):
        """
        Prevent any default value being used, save the one passed on
        initialisation.

        Retrieving values via `[key]` should _never_ fail to return a
        value (as it returns either the value corresponding to the key
        or the default passed on initialisation) but this should avoid
        any confusion.

        :param key: to be retrieved
        :param default: prohibited
        :return: value corresponding to key
        """
        if default is not None:
            raise ValueError("Cannot pass default argument.")

        return super().get(key)
