declare module "query-selector" {
  function querySelector(selectors: string, element: Element): NodeList;

  export default {
    default: querySelector,
  };
}
