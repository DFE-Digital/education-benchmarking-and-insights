import React from "react";
import ReactDOM from "react-dom";
import ReactDOMServer from "react-dom/server";
import { components } from "./index";

global.React = React;
global.ReactDOM = ReactDOM;
global.ReactDOMServer = ReactDOMServer;
global.Components = components;
