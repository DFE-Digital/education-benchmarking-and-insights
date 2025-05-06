import { useState, useCallback } from "react";

// wrapper around `AbortController` to re-initialise after each `abort()` called
export function useAbort() {
  const [controller, setController] = useState<AbortController>(
    new AbortController()
  );

  const abort = useCallback(() => {
    controller.abort();
    setController(new AbortController());
  }, [controller]);

  const signal = controller.signal;
  return { abort, signal };
}
