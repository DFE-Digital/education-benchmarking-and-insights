# Progressive Enhancement Work Charter

This charter defines how we collaborate to build digital experiences that are accessible, resilient, and scalable. It ensures that design and technical enhancements enrich the user experience without compromising essential functionality.

**TL;DR:**

* **Core First:** Every feature must work effectively without CSS or JavaScript, relying on semantic HTML and server-side rendering (Razor views).
* **Resilience:** We design for failure. If external assets fail to load, users must still be able to complete their core tasks.
* **Layered Value:** Enhancements (React, JS, CSS, Animations) must be additive and optional.
* **Collaborative:** Designers and Developers work together from the start to map technical feasibility against user needs.

## Principles

* **User-first functionality**: Core features must work effectively for all users, regardless of device, browser, or network conditions. Accessibility and usability are fundamental priorities.
* **Resilient by default**: The web is fragile. Assume JavaScript or CSS might fail to load (e.g., due to corporate firewalls, CDN issues, or network drops). Semantic HTML and server-side logic must provide a robust fallback.
* **Progressive enhancement**: Additional features or styling (e.g., React components, complex interactivity) enhance the experience without interfering with essential functionality. Each enhancement adds value and is optional.
* **Design-led thinking**: Designs reflect technical realities and constraints from the outset. Visual and interaction design clearly communicates hierarchy, affordances, and function in all contexts.
* **Collaboration and empathy**: Design and development teams work together from the earliest stages. Everyone considers both user needs and technical feasibility.
* **Iterative improvement**: Implement features in small, testable increments. Continuously evaluate enhancements for performance, accessibility, and usability.
* **Transparency and documentation**: All design decisions and technical solutions are documented. Rationale for enhancements is clearly communicated to stakeholders.

## How We Work

1. **Planning**: Identify essential functionality for all users. Prioritise accessibility, performance, and compatibility. Map enhancements and additional layers explicitly.
2. **Design**: Start with a "core design" that works universally. Layer richer visual, interactive, or content features progressively. Rely on the built-in progressive enhancement features of `govuk-frontend` components rather than reinventing fallback states.
3. **Development**: Implement the core experience first using **semantic HTML and server-side ASP.NET Core rendering**. Ensure forms, links, and data handling work via standard HTTP requests. Introduce enhancements (React components, client-side validation, advanced interactions) only as non-blocking layers.
4. **Testing**: Test the core experience across all supported platforms and devices. **Crucially, test all user journeys with JavaScript disabled** to verify fallback functionality and resilience. Validate enhancements progressively, ensuring they complement the core experience. Include accessibility, performance, and responsiveness in every test.
5. **Feedback and iteration**: Collect data from real users. Use feedback to refine both core and enhanced experiences. Promote continuous improvement and enrichment.

## Team Commitments

| Role                   | Responsibility                                                                                                                                           |
|------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------|
| **Designers**          | Ensure the core experience is usable and visually coherent before layering enhancements. Communicate opportunities for enriching the experience clearly. |
| **Developers**         | Prioritise a functional core first, then implement enhancements in manageable, testable layers. Share technical possibilities and constraints.           |
| **Product Managers**   | Support a progressive approach by prioritising core functionality and recognising the value of enhancements.                                             |
| **QA / Accessibility** | Ensure both core and enhanced experiences meet accessibility, performance, and usability standards.                                                      |

## Decision-Making Guidelines

* Does this enhancement complement the core experience for users? → **Implement progressively**.
* Does it provide meaningful value for a broad set of users? → **Prioritise accordingly**.
* Can it be implemented while maintaining accessibility, usability, and performance? → **Proceed**.

## Outcome

By following this charter, we build accessible, resilient digital experiences that work for all users. We deliver richer experiences that enhance the user journey without compromising core functionality, ensuring our solutions are sustainable and adaptable.

<!-- Leave the rest of this page blank -->
\newpage
