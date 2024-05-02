import React, { useState } from "react";
import { AutoComplete } from "src/components/auto-complete";
import {
  LaDocument,
  LaInputProps,
  SuggestResult,
} from "src/views/find-organisation";
import { v4 as uuidv4 } from "uuid";

const LaInput: React.FunctionComponent<LaInputProps> = (props) => {
  const { input, code } = props;
  const [inputValue, setInputValue] = useState<string>(input);
  const [selectedCode, setSelectedCode] = useState<string>(code);

  const handleSuggest = async (query: string) => {
    try {
      const res = await fetch(
        "/api/suggest?" +
          new URLSearchParams({ type: "local-authority", search: query }),
        {
          redirect: "manual",
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            "X-Correlation-ID": uuidv4(),
          },
        }
      );

      const response = await res.json();
      if (response.error) {
        throw response.error;
      }

      return response;
    } catch {
      // do something?
    }

    return [];
  };

  const handleSelected = (value: SuggestResult<LaDocument>) => {
    if (!value) {
      return;
    }

    setInputValue(value.document.name);
    setSelectedCode(value.document.code);
  };

  return (
    <div className="suggest-input">
      <AutoComplete<SuggestResult<LaDocument>>
        defaultValue={inputValue}
        id="la-input"
        name="laInput"
        minLength={3}
        onSelected={handleSelected}
        onSuggest={handleSuggest}
        suggestionFormatter={(item) =>
          item?.text ? item.text.replace(/\*(.*)\*/, "<b>$1</b>") : ""
        }
        valueFormatter={(item) => item?.document?.name ?? ""}
      />
      <input value={selectedCode} name="code" type="hidden" />
    </div>
  );
};

export default LaInput;
