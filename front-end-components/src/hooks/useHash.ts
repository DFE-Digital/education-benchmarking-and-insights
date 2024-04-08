import { useState, useCallback } from "react";
import { useEventListener } from "usehooks-ts";

// get and set the 'hash' value from the browser URL
export function useHash() {
  const [hash, setHash] = useState(() => window.location.hash);

  const hashChangeHandler = useCallback(() => {
    setHash(window.location.hash);
  }, []);

  useEventListener("hashchange", hashChangeHandler);

  const updateHash = useCallback(
    (newHash: string) => {
      if (newHash !== hash) {
        window.location.hash = newHash;
      }
    },
    [hash]
  );

  return [hash, updateHash];
}
