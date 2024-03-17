import React, { useRef, useState } from "react";
import {
  SuggestResult,
  TrustDocument,
  TrustInputProps,
} from "src/views/find-organisation/types";
import { v4 as uuidv4 } from "uuid";

const TrustInput: React.FC<TrustInputProps> = (props) => {
  const { input, companyNumber } = props;

  const trustInput = useRef(null);

  const [index, setIndex] = useState<number>(-1);
  const [optionsVisible, setOptionsVisible] = useState<boolean>(false);
  const [optionMode, setOptionMode] = useState<boolean>(false);
  const [options, setOptions] = useState<SuggestResult<TrustDocument>[]>([]);
  const [inputValue, setInputValue] = useState<string>(input);
  const [selectedCompanyNumber, setSelectedCompanyNumber] =
    useState<string>(companyNumber);
  const [lastInput, setLastInput] = useState<string>("");

  let controller = new AbortController();

  function fillSearch() {
    if (index < 0) return;

    setInputValue(options[index].text.replace(/(<([^>]+)>)/gi, ""));
    setSelectedCompanyNumber(options[index].document.companyNumber);
    setOptionsVisible(false);
  }

  function onKeyDown(event: React.KeyboardEvent<HTMLDivElement>) {
    if (event.key === "Enter" && index > -1 && optionMode) {
      fillSearch();
    } else if (event.key === "ArrowDown") {
      setOptionMode(true);
      if (index + 1 < options.length) {
        setIndex(index + 1);
      }
      event.preventDefault();
    } else if (event.key === "ArrowUp") {
      if (index >= 0) {
        setOptionMode(true);
        setIndex(index - 1);
      }
      event.preventDefault();
    }
  }

  function onKeyPressed() {
    controller.abort();
    controller = new AbortController();

    if (inputValue === lastInput) {
      setOptionsVisible(true);
      return;
    } else if (!optionsVisible) {
      setOptions([]);
      setOptionsVisible(true);
    }

    if (inputValue.length === 0) {
      setOptions([]);
      return;
    }

    setOptionMode(false);
    setLastInput(inputValue);

    fetch(
      "/api/establishments/suggest?" +
        new URLSearchParams({ type: "trust", search: inputValue }),
      {
        signal: controller.signal,
        redirect: "manual",
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "X-Correlation-ID": uuidv4(),
        },
      }
    )
      .then((res) => res.json())
      .then((res) => {
        if (res.error) {
          throw res.error;
        }

        return res;
      })
      .then((response) => {
        const optsLocal = response;
        setIndex(-1);
        optsLocal.forEach((d: SuggestResult<TrustDocument>) => {
          d.text = d.text.replace(/\*(.*)\*/, "<b>$1</b>");
        });
        setOptions(optsLocal);
      })
      .catch(() => {});
  }

  function hideOptions() {
    setOptionsVisible(false);
  }
  function searchThis(e: React.MouseEvent<HTMLAnchorElement>) {
    if (e.button !== 0) return;

    fillSearch();
  }

  function onChange(e: React.ChangeEvent<HTMLInputElement>) {
    setInputValue(e.target.value);
  }

  function optionMouseMove(idx: number) {
    if (idx !== index) {
      setIndex(idx);
    }
  }

  return (
    <div className="suggest-input">
      <input
        id="trust-input"
        ref={trustInput}
        className="govuk-input"
        aria-label="trustInput"
        name="trustInput"
        type="text"
        value={inputValue}
        onKeyDown={onKeyDown}
        onKeyUp={onKeyPressed}
        onBlur={hideOptions}
        onChange={onChange}
        autoComplete="off"
      />
      <input value={selectedCompanyNumber} name="companyNumber" type="hidden" />
      {options.length > 0 && optionsVisible && (
        <ul className="govuk-input" id="trust-suggestions">
          {options.map((item, idx) => {
            return (
              <li key={item.id}>
                <a
                  onMouseMove={() => {
                    optionMouseMove(idx);
                  }}
                  className={index === idx ? "active" : ""}
                  onMouseDown={searchThis}
                  href="#"
                  dangerouslySetInnerHTML={{ __html: item.text }}
                />
              </li>
            );
          })}
        </ul>
      )}
    </div>
  );
};

export default TrustInput;
