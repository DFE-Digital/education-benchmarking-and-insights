import React, {useRef, useState} from 'react';
import axios from "axios";

function SearchBox(props) {
    let {
        error,
        search,
        submitForm
    } = props;

    const searchInput = useRef(null);

    const [index, setIndex] = useState(-1);
    const [optionsVisible, setOptionsVisible] = useState(false);
    const [optionMode, setOptionMode] = useState(false);
    const [options, setOptions] = useState([]);
    const [searchValue, setSearchValue] = useState(search);
    const [searchUrn, setSearchUrn] = useState();
    const [lastSearch, setLastSearch] = useState('');
    const [cancelToken, setCancelToken] = useState(null);

    function cancelOngoing() {
        if (cancelToken) {
            cancelToken.cancel();
            setCancelToken(null);
        }
    }

    function fillSearch() {
        if (index < 0) return;

        setSearchValue(options[index].text.replace(/(<([^>]+)>)/gi, ""));
        setSearchUrn(options[index].document.urn)
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
        if (searchValue === lastSearch) {
            setOptionsVisible(true);
            return;
        } else if (!optionsVisible) {
            setOptions([]);
            setOptionsVisible(true);
        }

        cancelOngoing();

        if (searchValue.length === 0) {
            setOptions([]);
            return;
        }

        setOptionMode(false);
        setLastSearch(searchValue);
        let ctLocal = axios.CancelToken.source();
        setCancelToken(ctLocal);
        axios.get(window.location.pathname + '/suggest', {
            params: {
                search: searchValue
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
        submitForm();
    }

    function onChange(e) {
        setSearchValue(e.target.value);
    }

    function optionMouseMove(idx) {
        if (idx !== index) {
            setIndex(idx);
        }
    }

    return <div className="govuk-input__wrapper search-input">
        <input id="search-input" ref={searchInput} className="govuk-input" aria-label="search"
               name="search" type="text"
               value={searchValue} onKeyDown={onKeyDown} onKeyUp={onKeyPressed} onBlur={hideOptions} onChange={onChange}
               autoComplete="off"/>
        <input value={searchUrn} name="urn" type="hidden"/>
        <button id="search-btn" type="submit" className="govuk-button">Continue</button>
        {options.length > 0 && optionsVisible && <ul className="govuk-input" id="search-suggestions">
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

export default SearchBox;