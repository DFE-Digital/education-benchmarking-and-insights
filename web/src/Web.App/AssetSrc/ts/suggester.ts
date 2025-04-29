// @ts-ignore
import accessibleAutocomplete from "accessible-autocomplete";
import debounce from "lodash.debounce";

interface SuggestResult<T> {
    text: string;
    document: T;
}

export function suggester<T>(
    type: "school" | "trust" | "local-authority",
    inputElementId: string,
    targetElementId: string,
    documentKey: keyof T): void {

    const handleSuggest = async (query: string, exclude?: string[]): Promise<SuggestResult<T>[]> => {
        const params = new URLSearchParams({
            type,
            search: query,
        });
        if (exclude) {
            exclude.forEach((e) => {
                params.append("exclude", e);
            });
        }

        const res = await fetch("/api/suggest?" + params, {
            redirect: "manual",
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        });

        const response = await res.json();
        if (!response.error) {
            return response;
        }

        return [];
    };

    const templates = {
        inputValue: itemFormatter,
        suggestion: (item: SuggestResult<T>) => item?.text ? item.text.replace(/\*([^\\*]+)\*/g, "<b>$1</b>") : "",
    }

    function itemFormatter(item: SuggestResult<T>): string {
        return item?.text ? item.text.replace(/\*([^\\*]+)\*/g, "$1") : "";
    }

    function valueFormatter(item: SuggestResult<T>): string {
        if (!item?.document) {
            return "";
        }

        return item.document[documentKey] as string ?? "";
    }

    function onConfirm(item: SuggestResult<T>) {
        const targetElement = document.getElementById(targetElementId) as HTMLInputElement;
        if (targetElement) {
            targetElement.value = valueFormatter(item);
        }
        if (inputElement) {
            inputElement.value = itemFormatter(item);
        }
    }

    const source = debounce(
        async (query: string, populateResults: (results: SuggestResult<T>[]) => void) => {
            const results = await handleSuggest(query, []);
            populateResults(results);
        },
        300
    );

    const inputElement = document.getElementById(inputElementId) as HTMLInputElement;
    if (inputElement) {
        const element = document.createElement("div");
        const id = `__${inputElementId}`;
        inputElement.parentNode?.insertBefore(element, inputElement);

        const selectedElement = document.createElement("input");
        selectedElement.id = targetElementId;
        selectedElement.name = targetElementId;
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
            inputClasses: "govuk-input-autocomplete"
        });

        inputElement.type = "hidden";
        selectedElement.type = "hidden";
        element.addEventListener("keydown", e => {
            const target = e.target as HTMLInputElement;

            // enter
            if (e.key === "Enter") {
                inputElement.value = target?.value?.toString() === "0" ? target.innerText : target.value;
                element.closest("form")?.submit();
            }
        });
    }
}