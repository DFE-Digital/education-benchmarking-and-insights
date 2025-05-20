import accessibleAutocomplete from "accessible-autocomplete";
import debounce from "lodash.debounce";

export function Suggester<T>({
  type,
  inputElementId,
  targetElementId,
  documentKey,
  exclude,
}: SuggesterProps<T>): void {
  let abortController = new AbortController();

  const handleSuggest = async (query: string): Promise<SuggestResult<T>[]> => {
    const params = new URLSearchParams({
      type,
      search: query,
    });
    if (exclude) {
      exclude.forEach((e) => {
        params.append("exclude", e);
      });
    }

    const res = await fetch("/api/suggest?" + params.toString(), {
      redirect: "manual",
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
      signal: abortController.signal,
    });

    const response = (await res.json()) as ApiResponse<SuggestResult<T>>;
    if (!(response as ApiError).error) {
      return response as SuggestResult<T>[];
    }

    return [];
  };

  const suggestHighlightRegex = /\*([^\\*]+)\*/g;

  const templates = {
    inputValue: itemFormatter,
    suggestion: (item: SuggestResult<T>) =>
      item?.text ? item.text.replace(suggestHighlightRegex, "<b>$1</b>") : "",
  };

  function itemFormatter(item: SuggestResult<T>): string {
    return item?.text ? item.text.replace(suggestHighlightRegex, "$1") : "";
  }

  function valueFormatter(item: SuggestResult<T>): string {
    if (!item?.document) {
      return "";
    }

    return (item.document[documentKey] as string) ?? "";
  }

  function onConfirm(item: SuggestResult<T>) {
    const targetElement = document.getElementById(targetElementId) as HTMLInputElement;

    // set the output (e.g. EstablishmentId) field to the selected item value
    if (targetElement) {
      targetElement.value = valueFormatter(item);
    }

    // set the input (e.g. Term) field to the selected item text
    if (inputElement) {
      inputElement.value = itemFormatter(item);
    }
  }

  // debounce and/or cancel ongoing suggestions to prevent excessive requests
  let suggesting = false;
  const source = debounce(
    (query: string, populateResults: (results: SuggestResult<T>[]) => void) => {
      if (suggesting) {
        abortController.abort("Query updated");
        abortController = new AbortController();
      }

      suggesting = true;
      handleSuggest(query)
        .then((results) => {
          populateResults(results);
        })
        .catch((e: Error) => {
          if (e.name !== "AbortError") {
            throw e;
          }
        })
        .finally(() => {
          suggesting = false;
        });
    },
    500
  ) as SourceFunction;

  const inputElement = document.getElementById(inputElementId) as HTMLInputElement;
  if (inputElement) {
    // create placeholder element to render the autocomplete within
    const element = document.createElement("div");
    const id = `__${inputElementId}`;
    inputElement.parentNode?.insertBefore(element, inputElement);

    // create hidden element to store the selected item ID
    const selectedElement = document.createElement("input");
    selectedElement.id = targetElementId;
    selectedElement.name = targetElementId;
    selectedElement.type = "hidden";
    inputElement.parentNode?.insertBefore(selectedElement, inputElement);

    accessibleAutocomplete({
      element,
      id,
      name: id,
      defaultValue: inputElement.value ?? "",
      autoselect: false,
      displayMenu: "overlay",
      minLength: 3,
      showAllValues: false,
      showNoOptionsFound: false,
      confirmOnBlur: false,
      source,
      templates,
      onConfirm,
      inputClasses: "govuk-input-autocomplete",
    });

    inputElement.type = "hidden";

    element.addEventListener("keydown", (e) => {
      // submit form automatically on enter press, whether an item has been selected or not
      if (e.key === "Enter") {
        element.closest("form")?.submit();
      }
    });

    element.addEventListener("keyup", (e) => {
      // keep original input in sync with autocomplete input
      const target = e.target as HTMLInputElement;
      let value = target.value?.toString();
      if (value === "0") {
        value = target.innerText;
      }

      inputElement.value = value;

      // clear any previously selected establishment
      if (e.key !== "Enter") {
        selectedElement.value = "";
      }
    });
  }
}
