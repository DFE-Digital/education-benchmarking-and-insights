import { AccessibleAutocompleteProps } from "accessible-autocomplete/react";

export type AutoCompleteProps<T> = Pick<
  AccessibleAutocompleteProps,
  "defaultValue" | "id" | "name" | "minLength" | "required"
> & {
  /**
   * When the user selects an option.
   * @param value The option selected by the user
   */
  onSelected(value: T): void;

  /**
   * Executes a suggester using the supplied query
   * @param query The value entered by the user
   * @returns The suggested matches based on the query
   */
  onSuggest: (query: string) => Promise<T[]>;

  /**
   * Formatter to use when rendering a suggestion to be displayed
   * @param value Suggested value
   * @returns ⚠️ `string` that may contain HTML
   */
  suggestionFormatter: (value: T) => string;

  /**
   * Formatter to use to generate the value to be inserted into the input field
   * @param value Selected value
   * @returns Plain `string` that represents the value
   */
  valueFormatter: (value: T) => string;
};
