import React, { useState } from "react";
import { AutoComplete } from "src/components/auto-complete";
import {
  SchoolDocument,
  SchoolInputProps,
  SuggestResult,
} from "src/views/find-organisation";
import { v4 as uuidv4 } from "uuid";

const SchoolInput: React.FunctionComponent<SchoolInputProps> = (props) => {
  const { input, urn, exclude } = props;
  const [inputValue, setInputValue] = useState<string>(input);
  const [selectedUrn, setSelectedUrn] = useState<string>(urn);

  const handleSuggest = async (query: string) => {
    const params = new URLSearchParams({
      type: "school",
      search: query,
    });
    if (exclude) {
      exclude.forEach((e) => {
        params.append("exclude", e);
      });
    }

    try {
      const res = await fetch("/api/suggest?" + params, {
        redirect: "manual",
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "X-Correlation-ID": uuidv4(),
        },
      });

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
    if (!value) {
      return;
    }

    setInputValue(value.document.schoolName);
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
          item?.text ? item.text.replace(/\*(.*)\*/, "<b>$1</b>") : ""
        }
        valueFormatter={(item) => item?.document?.schoolName ?? ""}
      />
      <input value={selectedUrn} name="urn" type="hidden" />
    </div>
  );
};

export default SchoolInput;
