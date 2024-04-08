import { useLayoutEffect, useState } from "react";
import { initAll } from "govuk-frontend";

// https://frontend.design-system.service.gov.uk/javascript-api-reference/
export default function useGovUk() {
  const [isLoaded, setIsLoaded] = useState<boolean>();

  useLayoutEffect(() => {
    initAll();
    setIsLoaded(true);
  }, []);

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
