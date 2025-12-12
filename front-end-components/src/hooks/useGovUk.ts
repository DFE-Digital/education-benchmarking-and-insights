import { useEffect, useState } from "react";
import { initAll } from "govuk-frontend";

// https://frontend.design-system.service.gov.uk/javascript-api-reference/
export function useGovUk(scope?: Element | null) {
  const [isLoaded, setIsLoaded] = useState<boolean>();

  // https://react.dev/reference/react/useEffect#my-effect-runs-twice-when-the-component-mounts
  useEffect(() => {
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
