import React, { useState } from "react";
import { AutoComplete } from "src/components/auto-complete";
import {
  SchoolDocument,
  SchoolInputProps,
  SuggestResult,
} from "src/views/find-organisation";
import { v4 as uuidv4 } from "uuid";

const SchoolInput: React.FunctionComponent<SchoolInputProps> = (props) => {
  const { input, urn } = props; // input = defaultValue?

  const [inputValue, setInputValue] = useState<string>(input);
  const [selectedUrn, setSelectedUrn] = useState<string>(urn);

  let controller = new AbortController();

  const handleSuggest = async (query: string) => {
    controller.abort();
    controller = new AbortController();

    try {
      const res = await fetch(
        "/api/suggest?" +
          new URLSearchParams({ type: "school", search: query }),
        {
          signal: controller.signal,
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

  const handleSelected = (value: SuggestResult<SchoolDocument>) => {
    controller.abort();
    controller = new AbortController();
    setInputValue(value.text.replace(/\*(.*)\*/, "$1"));
    setSelectedUrn(value.document.urn);
  };

  return (
    <div className="suggest-input">
      <AutoComplete<SuggestResult<SchoolDocument>>
        defaultValue={inputValue}
        id="school-input"
        name="schoolInput"
        minLength={3}
        onSelected={handleSelected}
        onSuggest={handleSuggest}
        suggestionFormatter={(item) =>
          item ? item.text.replace(/\*(.*)\*/, "<b>$1</b>") : ""
        }
        valueFormatter={(item) =>
          item ? item.text.replace(/\*(.*)\*/, "$1") : ""
        }
      />
      <input value={selectedUrn} name="urn" type="hidden" />
    </div>
  );
};

export default SchoolInput;
