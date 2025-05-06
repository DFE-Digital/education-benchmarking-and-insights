type SourceFunction = (query: string, populateResults: (results: T[]) => void) => (Promise<void> | void);

// https://github.com/alphagov/accessible-autocomplete#api-documentation
interface AccessibleAutocompleteOptions<T> {
    // required
    element: HTMLElement,
    id: string,
    source: SourceFunction | T[],

    // optional
    inputClasses?: string,
    hintClasses?: string,
    menuAttributes?: object,
    menuClasses?: string,
    autoselect?: boolean,
    confirmOnBlur?: boolean,
    cssNamespace?: string;
    defaultValue?: string,
    displayMenu?: "inline" | "overlay",
    minLength?: number,
    name?: string,
    onConfirm: (item: T) => void,
    placeholder?: string;
    required?: boolean;
    showAllValues?: boolean,
    showNoOptionsFound?: boolean,
    templates?: {
        inputValue?: (item: T) => string,
        suggestion?: (item: T) => string
    },
    dropdownArrow?: (prop: { className: string }) => string;

    // internationalisation
    tNoResults?: () => string;
    tStatusQueryTooShort?: (minQueryLength: number) => string;
    tStatusNoResults?: () => string;
    tStatusSelectedOption?: (selectedOption: T, length: number, index: number) => string;
    tStatusResults?: (length: number, contentSelectedOption: T) => string;
    tAssistiveHint?: () => string;
}

declare function accessibleAutocomplete(options: AccessibleAutocompleteOptions<T>): void

declare namespace accessibleAutocomplete {
    const enhanceSelectElement: (options: {
        selectElement: HTMLSelectElement
    } & AccessibleAutocompleteOptions<T>) => void
}

declare module "accessible-autocomplete" {
    export = accessibleAutocomplete;
}