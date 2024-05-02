import React, { useState } from "react";
import { AutoComplete } from "src/components/auto-complete";
import {
  SuggestResult,
  TrustDocument,
  TrustInputProps,
} from "src/views/find-organisation";
import { v4 as uuidv4 } from "uuid";

const TrustInput: React.FunctionComponent<TrustInputProps> = (props) => {
  const { input, companyNumber } = props;
  const [inputValue, setInputValue] = useState<string>(input);
  const [selectedCompanyNumber, setSelectedCompanyNumber] =
    useState<string>(companyNumber);

  const handleSuggest = async (query: string) => {
    try {
      const res = await fetch(
        "/api/suggest?" + new URLSearchParams({ type: "trust", search: query }),
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

  const handleSelected = (value: SuggestResult<TrustDocument>) => {
    if (!value) {
      return;
    }

    setInputValue(value.document.name);
    setSelectedCompanyNumber(value.document.companyNumber);
  };

  return (
    <div className="suggest-input">
      <AutoComplete<SuggestResult<TrustDocument>>
        defaultValue={inputValue}
        id="trust-input"
        name="trustInput"
        minLength={3}
        onSelected={handleSelected}
        onSuggest={handleSuggest}
        suggestionFormatter={(item) =>
          item?.text ? item.text.replace(/\*(.*)\*/, "<b>$1</b>") : ""
        }
        valueFormatter={(item) => item?.document?.name ?? ""}
      />
      <input value={selectedCompanyNumber} name="companyNumber" type="hidden" />
    </div>
  );
};

export default TrustInput;
