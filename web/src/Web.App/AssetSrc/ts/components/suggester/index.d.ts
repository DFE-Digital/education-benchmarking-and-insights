interface SuggesterProps<T> {
  documentKey: keyof T;
  exclude?: string[];
  inputElementId: string;
  targetElementId: string;
  type: "school" | "trust" | "local-authority";
}

interface SuggestResult<T> {
  text: string;
  document: T;
}
