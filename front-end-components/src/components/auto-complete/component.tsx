import AccessibleAutocomplete from "accessible-autocomplete/react";
import { AutoCompleteProps } from "./types";
import "accessible-autocomplete/dist/accessible-autocomplete.min.css";

export function AutoComplete<T>({
  onSelected,
  onSuggest,
  suggestionFormatter,
  valueFormatter,
  ...props
}: AutoCompleteProps<T>) {
  const handleSource = (
    query: string,
    populateResults: (values: T[]) => void
  ) => {
    onSuggest(query)
      .then(populateResults)
      .catch(() => populateResults([]));
  };

  return (
    <AccessibleAutocomplete
      {...props}
      onConfirm={(confirmed) => onSelected(confirmed as T)}
      source={(query, syncResults) =>
        handleSource(query, syncResults as (values: T[]) => void)
      }
      templates={{
        inputValue: (selectedSuggestion: string) =>
          valueFormatter(selectedSuggestion as T),
        suggestion: (suggestion: string) =>
          suggestionFormatter(suggestion as T),
      }}
      tNoResults={() => "No results found"}
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
        ) as unknown as string;
      }}
      tAssistiveHint={() =>
        "When autocomplete results are available use up and down arrows to review and enter to select. Touch device users, explore by touch or with swipe gestures."
      }
    />
  );
}
