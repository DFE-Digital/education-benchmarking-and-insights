declare module "govuk-frontend" {
  interface Config {
    accordion: Partial<{
      i18n: {
        hideAllSections: string;
        hideSection: string;
        hideSectionAriaLabel: string;
        showAllSections: string;
        showSection: string;
        showSectionAriaLabel: string;
      };
      rememberExpanded: boolean;
    }>;
  }

  export function initAll(config?: Partial<Config> & { scope?: Element }): void;
}
