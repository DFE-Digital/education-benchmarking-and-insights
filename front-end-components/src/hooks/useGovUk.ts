import { useEffect, useLayoutEffect, useState } from "react";
import { initAll } from "govuk-frontend";
import { useSessionStorage } from "usehooks-ts";

interface InitConfig {
  accordionSection: Element | null;
}

// https://frontend.design-system.service.gov.uk/javascript-api-reference/
export default function useGovUk(config?: Partial<InitConfig>) {
  const [isLoaded, setIsLoaded] = useState<boolean>();
  const [accordionId, setAccordionId] = useState<string>();
  const [
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    _accordionSessionValue,
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    _setAccordionSessionValue,
    removeAccordionSessionValue,
  ] = useSessionStorage<string>(accordionId || "undefined", "");

  useLayoutEffect(() => {
    if (config?.accordionSection) {
      const region =
        config?.accordionSection.querySelector("div[role='region']");
      setAccordionId(region?.id);
    } else {
      initAll();
      setIsLoaded(true);
    }
    // no - will init multiple times which borks the UI
  }, [config]);

  const reInit = (scope: Element) => {
    if (scope) {
      initAll({ scope });
    }
  };

  useEffect(() => {
    if (accordionId) {
      removeAccordionSessionValue();
      initAll();
      setIsLoaded(true);
    }
  }, [accordionId, removeAccordionSessionValue]);

  return {
    isLoaded,
    reInit,
  };
}
