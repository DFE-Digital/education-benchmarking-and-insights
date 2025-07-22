# Rendering of Charts (0006)

Date: 2025-01-27

## Status

Accepted

## Context

We need to decide how charts will be rendered in our application. The charts are essential for visualising data
insights, and the chosen approach should balance performance, scalability, maintainability, accessibility, and cost.
The solution will be integrated into a .NET-based application hosted in Azure App Service.

### Requirements

- Charts should be visually appealing and responsive.
- The solution must support multiple chart types (e.g., bar, line, pie).
- Accessibility for users (e.g., screen readers) is essential.
- The rendering approach must handle large datasets efficiently.
- Minimize server load and ensure scalability for high traffic.
- Compatibility with the .NET ecosystem.
- Support for **progressive enhancement** to ensure functionality for all users, even in environments with limited capabilities.

## Options

### Option 1: Client-Side Rendering with Recharts (React-based Library)

**Description:**
Charts are rendered directly in the user's browser using the [Recharts](https://recharts.org/) library,
a React-based charting solution. Data is fetched from APIs and rendered dynamically on the client side.

#### Pros

- Fully interactive charts (e.g., tooltips, zooming).
- Offloads computation to the client, reducing server load.
- Highly customizable and responsive.
- Extensive community support and documentation.
- No additional server infrastructure required.

#### Cons

- High client-side computation may affect performance for large datasets or low-powered devices.
- Requires a React-based frontend.
- Potentially longer load times for large datasets.
- Progressive enhancement: Limited support, as rendering is entirely dependent on client-side JavaScript. Non-JS users will not see charts.

### Option 2: Server-Side Rendering Using Node Client Embedded in .NET App Service (NodeReact)

**Description:**
Charts are pre-rendered on the server using a Node.js charting library (e.g., Chart.js, D3.js) wrapped in a
NodeReact integration within the .NET application. These pre-rendered charts are served as static assets or
embedded directly in the HTML.

#### Pros

- Reduces client-side processing, improving performance on low-powered devices.
- Compatible with existing .NET application infrastructure.
- Pre-rendering ensures faster load times for charts.
- NodeReact enables seamless integration with .NET.
- Progressive enhancement: Strong support, as pre-rendered charts ensure basic functionality even without client-side JavaScript.

#### Cons

- Additional complexity in the application setup (Node.js runtime within .NET).
- Increased server resource usage for rendering charts, which may affect scalability.
- Limited interactivity for pre-rendered charts.

#### Notes

As part of an earlier [spike](https://github.com/DFE-Digital/education-benchmarking-and-insights/tree/spike/react-node-client), a successful
implementation was completed. This included installing Node.js in the App service alongside .NET; generating a Vite SSR bundle alongside
client side elements; and explitly importing React components from Razor views using NodeReact v2, along with extended CSP support. It also
considerably tidied up the entry point into the front end components which helped to drastically slim down the Vite build outputs.

### Option 3: Separate Service for Chart Rendering (Serving SVG or Images)

**Description:**
A separate microservice renders charts as SVG or image files using a charting library (e.g., Highcharts, Chart.js).
The .NET app consumes these rendered charts as static assets.

#### Pros

- Offloads chart rendering to a dedicated service, improving scalability.
- Reduces client-side and main application server load.
- Charts are lightweight (SVG/image format) and quick to load.
- Allows use of specialised charting libraries for optimized rendering.
- Progressive enhancement: Excellent support, as SVG or image formats are fully functional regardless of client-side JavaScript availability.

#### Cons

- Additional infrastructure and maintenance for the separate service.
- Limited interactivity for static charts (unless using hybrid solutions like interactive SVGs).
- Potential latency due to network calls between services.

### Option 4: .NET Charting Library

**Description:**
Charts are rendered server-side using a .NET-native charting library and served to the client as static images or
interactive content.

#### Pros

- Seamless integration with the .NET ecosystem.
- Reduces complexity by avoiding additional runtimes (Node.js or external services).
- Mature libraries with enterprise-grade features (e.g., Syncfusion).
- Progressive enhancement: Strong support, as server-rendered charts (images) provide basic functionality in all environments.

#### Cons

- Limited interactivity compared to client-side solutions.
- High server load for rendering charts, especially with large datasets or frequent requests.
- Some libraries require licensing (e.g., Syncfusion).

## Decision

**Chosen Option:** Separate Service for Chart Rendering (Option 3)

### Rationale

1. Scalability: Offloading chart rendering to a dedicated service ensures scalability as the application grows.
2. Performance: Lightweight SVG or image formats reduce client-side processing and improve loading times.
3. Flexibility: A separate service allows us to leverage specialized charting libraries optimized for rendering.
4. Maintainability: Isolating chart rendering simplifies the core .NET application while enabling independent updates or scaling of the rendering service.
5. Progressive Enhancement: SVG or image-based charts ensure full functionality in environments with limited or no client-side JavaScript support.

### Trade-Offs

- Requires additional infrastructure and maintenance for the microservice.
- Limited interactivity compared to client-side solutions, but this can be partially mitigated using interactive SVGs where needed.

## Future Considerations

If the application's requirements for interactivity increase significantly, we may explore hybrid solutions combining
server-rendered SVGs with client-side enhancements for greater flexibility. Additionally, fragment caching using the
cache tag helper built into .NET view fragments could be leveraged to improve performance by caching commonly accessed
chart components, reducing redundant server-side processing.

<!-- Leave the rest of this page blank -->
\newpage
