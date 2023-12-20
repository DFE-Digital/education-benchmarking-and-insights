import React, {useRef, useState} from 'react';
import axios from "axios";

function SchoolInput(props) {
    let { input, urn } = props;

    const schoolInput = useRef(null);

    const [index, setIndex] = useState(-1);
    const [optionsVisible, setOptionsVisible] = useState(false);
    const [optionMode, setOptionMode] = useState(false);
    const [options, setOptions] = useState([]);
    const [inputValue, setInputValue] = useState(input);
    const [selectedUrn, setSelectedUrn] = useState(urn);
    const [lastInput, setLastInput] = useState('');
    const [cancelToken, setCancelToken] = useState(null);

    function cancelOngoing() {
        if (cancelToken) {
            cancelToken.cancel();
            setCancelToken(null);
        }
    }

    function fillSearch() {
        if (index < 0) return;

        setInputValue(options[index].text.replace(/(<([^>]+)>)/gi, ""));
        setSelectedUrn(options[index].document.urn)
        setOptionsVisible(false);
    }

    function onKeyDown(event) {
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
        if (inputValue === lastInput) {
            setOptionsVisible(true);
            return;
        } else if (!optionsVisible) {
            setOptions([]);
            setOptionsVisible(true);
        }

        cancelOngoing();

        if (inputValue.length === 0) {
            setOptions([]);
            return;
        }

        setOptionMode(false);
        setLastInput(inputValue);
        let ctLocal = axios.CancelToken.source();
        setCancelToken(ctLocal);
        axios.get( '/api/establishments/suggest', {
            params: {
                type: 'school',
                search: inputValue
            }, cancelToken: ctLocal.token
        }).then(response => {
            let optsLocal = response.data;
            setIndex(-1);
            optsLocal.forEach(d => {
                d.text = d.text.replace(/\*(.*)\*/, "<b>$1</b>");
            });
            setOptions(optsLocal);
        }).catch(() => {
        });
    }

    function hideOptions() {
        setOptionsVisible(false);
    }
    function searchThis(e) {
        if (e.button !== 0) return;

        fillSearch();
    }

    function onChange(e) {
        setInputValue(e.target.value);
    }

    function optionMouseMove(idx) {
        if (idx !== index) {
            setIndex(idx);
        }
    }

    return <div className="suggest-input">
        <input id="school-input" ref={schoolInput} className="govuk-input" aria-label="schoolInput"
               name="schoolInput" type="text"
               value={inputValue} onKeyDown={onKeyDown} onKeyUp={onKeyPressed} onBlur={hideOptions} onChange={onChange}
               autoComplete="off"/>
        <input value={selectedUrn} name="urn" type="hidden"/>
        {options.length > 0 && optionsVisible && <ul className="govuk-input" id="school-suggestions">
            {options.map((item, idx) => {
                return <li key={item.id}>
                    <a onMouseMove={() => {
                        optionMouseMove(idx)
                    }} className={index === idx ? "active" : ""} onMouseDown={searchThis} href="#"
                       dangerouslySetInnerHTML={{__html: item.text}}/>
                </li>;
            })}
        </ul>}
    </div>;
}

export default SchoolInput;