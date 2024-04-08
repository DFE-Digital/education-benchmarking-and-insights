import { useLayoutEffect, useState } from "react";
import { initAll } from "govuk-frontend";

// https://frontend.design-system.service.gov.uk/javascript-api-reference/
export function useGovUk() {
  const [isLoaded, setIsLoaded] = useState<boolean>();

  useLayoutEffect(() => {
    if (!isLoaded) {
      initAll();
    }

    setIsLoaded(true);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  /**
   * See https://frontend.design-system.service.gov.uk/configure-components/#initialise-only-part-of-a-page
   */
  const reInit = (scope: Element) => {
    if (scope) {
      initAll({ scope });
    }
  };

  return {
    isLoaded,
    reInit,
  };
}
