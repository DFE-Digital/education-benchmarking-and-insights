# JavaScript Library Selection for ASP.NET Core with GOV.UK Standards (0009)

Date: 2025-02-17

## Status

Accepted

## Context

This review of JavaScript libraries is due to the move to **server-side rendering (SSR) of charts** in the application.
We are developing an ASP.NET Core application that follows **GOV.UK design standards** and **progressive enhancement** principles. The application primarily renders **Razor Views**, and JavaScript is used to enhance the user experience rather than being a requirement.

Key considerations:

- **Accessibility & Usability**: Must comply with **WCAG 2.1 AA** and **GOV.UK accessibility guidelines**.
- **Performance & Maintainability**: Minimize dependencies to keep pages lightweight and maintainable.
- **Progressive Enhancement**: Ensure core functionality works without JavaScript.
- **Security**: Avoid introducing unnecessary third-party dependencies that might increase security risks.

## Decision

We will use the following approach for JavaScript in our application:

1. **GOV.UK Frontend Library**
    - **Rationale**: Provides accessible, tested UI components following GOV.UK standards.
    - **Implementation**: Load via CDN or NPM:

      ```html
      <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/govuk-frontend/govuk/all.min.css">
      <script src="https://cdn.jsdelivr.net/npm/govuk-frontend/govuk/all.min.js"></script>
      ```

    - **Usage**: Components like buttons, accordions, and form validation use built-in JavaScript modules.

2. **Vanilla JavaScript (Preferred for Enhancements)**
    - **Rationale**: Keeps dependencies minimal while allowing custom interactions.
    - **Example Implementation**:

      ```html
      <script>
          document.addEventListener("DOMContentLoaded", function() {
              console.log("Enhancements loaded!");
          });
      </script>
      ```

3. **HTMX (For Lightweight AJAX Enhancements, If Needed)**
    - **Rationale**: Allows making server interactions without a full JavaScript framework.
    - **Implementation**:

      ```html
      <script src="https://unpkg.com/htmx.org@1.9.2"></script>
      ```

4. **Alpine.js (For Minimal Reactivity, If Needed)**
    - **Rationale**: A small (10KB) framework for simple interactions like toggles and modals.
    - **Implementation**:

      ```html
      <script defer src="https://cdn.jsdelivr.net/npm/alpinejs@3.x.x/dist/cdn.min.js"></script>
      ```

5. **Vue.js (For Lightweight, Reactive Components, If Needed)**
    - **Rationale**: Can be used for reactive interfaces with a smaller footprint than React.
    - **Implementation**: Can be integrated as Vue components within Razor Views.
    - **Example:**

      ```html
      <div id="app">
          <button v-on:click="count++">Clicked {{ count }} times</button>
      </div>
      <script>
          new Vue({
              el: '#app',
              data: { count: 0 }
          });
      </script>
      ```

## Alternatives Considered

- **Angular**: Not chosen due to unnecessary complexity and deviation from the progressive enhancement approach.
- **jQuery**: Avoided unless required for legacy support, as it adds unnecessary overhead.
- **React (JSX Components)**: Considered for highly interactive elements but introduces additional complexity and is typically overkill for GOV.UK-style applications relying on progressive enhancement.

## Consequences

- **Pros**:
  - Ensures compliance with **GOV.UK accessibility** and **performance guidelines**.
  - Reduces **dependencies**, improving maintainability and security.
  - Allows **progressive enhancment**, ensuring the application works without JavaScript.
  - Provides flexibility with **Vue** for specific use cases requiring interactivity.
- **Cons**:
  - Some dynamic interactions may require more manual JavaScript coding compared to using a framework.
  - Introducing **Vue** may increase complexity if not used selectively.

<!-- Leave the rest of this page blank -->
\newpage
