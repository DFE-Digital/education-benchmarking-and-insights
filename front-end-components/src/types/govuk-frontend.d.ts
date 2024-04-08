declare module "govuk-frontend" {
  /**
   * Accordion translations
   *
   * Messages used by the component for the labels of its buttons. This includes
   * the visible text shown on screen, and text to help assistive technology users
   * for the buttons toggling each section.
   * @property {string} [hideAllSections] - The text content for the 'Hide all
   *   sections' button, used when at least one section is expanded.
   * @property {string} [hideSection] - The text content for the 'Hide'
   *   button, used when a section is expanded.
   * @property {string} [hideSectionAriaLabel] - The text content appended to the
   *   'Hide' button's accessible name when a section is expanded.
   * @property {string} [showAllSections] - The text content for the 'Show all
   *   sections' button, used when all sections are collapsed.
   * @property {string} [showSection] - The text content for the 'Show'
   *   button, used when a section is collapsed.
   * @property {string} [showSectionAriaLabel] - The text content appended to the
   *   'Show' button's accessible name when a section is expanded.
   */
  interface AccordionTranslations {
    hideAllSections: string;
    hideSection: string;
    hideSectionAriaLabel: string;
    showAllSections: string;
    showSection: string;
    showSectionAriaLabel: string;
  }

  /**
   * Accordion config
   *
   * @property {AccordionTranslations} [i18n=Accordion.defaults.i18n] - Accordion translations
   * @property {boolean} [rememberExpanded] - Whether the expanded and collapsed
   *   state of each section is remembered and restored when navigating.
   */
  interface AccordionConfig {
    i18n: AccordionTranslations;
    rememberExpanded: boolean;
  }

  /**
   * Config for all components via `initAll()`
   *
   * @property {AccordionConfig} [accordion] - Accordion config
   * @property {ButtonConfig} [button] - Button config
   * @property {CharacterCountConfig} [characterCount] - Character Count config
   * @property {ErrorSummaryConfig} [errorSummary] - Error Summary config
   * @property {ExitThisPageConfig} [exitThisPage] - Exit This Page config
   * @property {NotificationBannerConfig} [notificationBanner] - Notification Banner config
   * @property {PasswordInputConfig} [passwordInput] - Password input config
   */
  interface Config {
    accordion: Partial<AccordionConfig>;
  }

  /**
   * Initialise all components
   *
   * Use the `data-module` attributes to find, instantiate and init all of the
   * components provided as part of GOV.UK Frontend.
   */
  export function initAll(config?: Partial<Config> & { scope?: Element }): void;
}
