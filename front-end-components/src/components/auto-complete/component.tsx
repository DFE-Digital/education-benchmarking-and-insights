import { useState } from "react";
import AccessibleAutocomplete from "accessible-autocomplete/react";
import { useDebounceCallback } from "usehooks-ts";
import { AutoCompleteProps } from "./types";
import "accessible-autocomplete/dist/accessible-autocomplete.min.css";

export function AutoComplete<T>({
  onSelected,
  onSuggest,
  suggestionFormatter,
  valueFormatter,
  ...props
}: AutoCompleteProps<T>) {
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const suggest = async (
    query: string,
    populateResults: (values: T[]) => void
  ) => {
    setIsLoading(true);

    try {
      const results = await onSuggest(query);
      if (results && Array.isArray(results)) {
        populateResults(results);
        return;
      }

      throw new Error("Unexpected results returned from suggester");
    } catch {
      // todo: report failure back to component
      populateResults([]);
    } finally {
      setIsLoading(false);
    }
  };

  const debouncedSource = useDebounceCallback(
    async (query, populateResults) => {
      await suggest(query, populateResults as (values: T[]) => void);
    },
    300
  );

  return (
    <AccessibleAutocomplete
      {...props}
      onConfirm={(confirmed) => onSelected(confirmed as T)}
      showNoOptionsFound={false}
      source={debouncedSource}
      templates={{
        inputValue: (selectedSuggestion: string) =>
          valueFormatter(selectedSuggestion as T),
        suggestion: (suggestion: string) =>
          suggestionFormatter(suggestion as T),
      }}
      tNoResults={() => (isLoading ? "Searching..." : "No results found")}
      tStatusQueryTooShort={(minQueryLength: number) =>
        `Type in ${minQueryLength} or more characters for results`
      }
      tStatusNoResults={() => "No search results"}
      tStatusSelectedOption={(
        selectedOption: string,
        length: number,
        index: number
      ) => `${selectedOption} ${index + 1} of ${length} is highlighted`}
      tStatusResults={(length: number, contentSelectedOption: number) => {
        const words = {
          result: length === 1 ? "result" : "results",
          is: length === 1 ? "is" : "are",
        };
        return (
          <span>
            {length} {words.result} {words.is} available.{" "}
            {contentSelectedOption}
          </span>
        );
      }}
      tAssistiveHint={() =>
        "When autocomplete results are available use up and down arrows to review and enter to select. Touch device users, explore by touch or with swipe gestures."
      }
    />
  );
}
