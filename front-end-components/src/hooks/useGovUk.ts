import { useLayoutEffect, useState } from "react";
import { initAll } from "govuk-frontend";

// https://frontend.design-system.service.gov.uk/javascript-api-reference/
export function useGovUk(scope: Element | null) {
  const [isLoaded, setIsLoaded] = useState<boolean>();

  useLayoutEffect(() => {
    if (!isLoaded) {
      if (scope) {
        initAll({ scope });
      } else {
        initAll();
      }
    }

    setIsLoaded(true);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return {
    isLoaded,
  };
}
