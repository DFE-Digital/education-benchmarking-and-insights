import "src/index.css";
import React from "react";
import ReactDOM from "react-dom";
import { components } from "./index";

window.React = React;
window.ReactDOM = ReactDOM;
window.Components = components;

// eslint-disable-next-line @typescript-eslint/no-explicit-any
(window as any)?.ReactJsAsyncInit();
