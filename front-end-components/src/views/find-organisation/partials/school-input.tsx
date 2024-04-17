import React, { useEffect, useRef, useState } from "react";
import {
  SchoolDocument,
  SchoolInputProps,
  SuggestResult,
} from "src/views/find-organisation";
import { useDebounceCallback } from "usehooks-ts";
import { v4 as uuidv4 } from "uuid";

const SchoolInput: React.FunctionComponent<SchoolInputProps> = (props) => {
  const { input, urn } = props;

  const schoolInput = useRef(null);

  const [index, setIndex] = useState<number>(-1);
  const [optionsVisible, setOptionsVisible] = useState<boolean>(false);
  const [optionMode, setOptionMode] = useState<boolean>(false);
  const [options, setOptions] = useState<SuggestResult<SchoolDocument>[]>([]);
  const [inputValue, setInputValue] = useState<string>(input);
  const [selectedUrn, setSelectedUrn] = useState<string>(urn);
  const [lastInput, setLastInput] = useState<string>("");
  const [fetchValue, setFetchValue] = useState<string>();
  const debounceFetchValue = useDebounceCallback(setFetchValue, 500);

  let controller = new AbortController();

  function fillSearch() {
    if (index < 0) return;

    setInputValue(options[index].text.replace(/(<([^>]+)>)/gi, ""));
    setSelectedUrn(options[index].document.urn);
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

    if (inputValue === lastInput?.trim()) {
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
    debounceFetchValue(inputValue);
  }

  useEffect(() => {
    if (!fetchValue) {
      return;
    }

    fetch(
      "/api/establishments/suggest?" +
        new URLSearchParams({ type: "school", search: fetchValue }),
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
        optsLocal.forEach((d: SuggestResult<SchoolDocument>) => {
          d.text = d.text.replace(/\*(.*)\*/, "<b>$1</b>");
        });
        setOptions(optsLocal);
      })
      .catch(() => {});
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [fetchValue]);

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
        id="school-input"
        ref={schoolInput}
        className="govuk-input"
        aria-label="schoolInput"
        name="schoolInput"
        type="text"
        value={inputValue}
        onKeyDown={onKeyDown}
        onKeyUp={onKeyPressed}
        onBlur={hideOptions}
        onChange={onChange}
        autoComplete="off"
      />
      <input value={selectedUrn} name="urn" type="hidden" />
      {options.length > 0 && optionsVisible && (
        <ul className="govuk-input" id="school-suggestions">
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

export default SchoolInput;
