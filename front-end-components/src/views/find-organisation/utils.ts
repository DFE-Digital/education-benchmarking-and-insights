import { SuggestResult } from "./types";

/**
 * Formatter to use when rendering a suggestion to be displayed
 */
export function suggestionFormatter<T>(item: SuggestResult<T>): string {
  return item?.text ? item.text.replace(/\*([^\\*]+)\*/g, "<b>$1</b>") : "";
}
