# User Testing - options & dependencies

This overview is intended to help us choose the most appropriate approach based on the goals and risk profile of the
work, and to understand when engineering involvement is needed.

**TL;DR:**

* Options 1 & 2: Have little or no dependencies on Engineering. These are design-led and can be executed independently by the research/design team.
* Option 3: Engineering is on the critical path. Developers must build and implement these changes; therefore, capacity must be planned.
* Consultation: The Tech Lead must be consulted as soon as a Build option (Option 3) is being considered.
* Governance: The final decision on which technical implementation to use (sub-options 3.1, 3.2, or 3.3) is made by the Tech Lead.

## 1. Figma prototype

This approach is best for early discovery and concept validation. It is the fastest method to create and iterate on
ideas, making it highly suitable for testing content, flow, and high-level usability. The primary pro is that it
requires no engineering effort, allowing the development team to remain focused on current delivery. However, the con
is that it lacks functional depth; it cannot simulate complex interactions or real-world data handling, which may lead
to "happy path" bias during testing.

## 2. GDS prototype

The GDS prototype is useful for validating journeys and interactions that need to feel closer to a production
environment. It supports realistic behaviour without requiring a full software build. The main pro of this option is
its balance of speed versus fidelity, offering a more immersive experience for the user than a flat design. The
significant con is that it requires moderate effort to set up and maintain the prototyping kit, and it still lacks the
ability to test against live backend services

## 3. Build

This option is reserved for research goals that require absolute realism, specifically for testing with real services
and data patterns. The pro is that it offers the highest level of feedback accuracy with no impact on live users.
The primary con is that it is a high-effort undertaking for the engineering team.

Tech Lead decision & requirements - The choice between the technical implementation paths below sits with the Tech
Lead. To enable this decision and subsequent engineering work:

* Risk articulation: Design/Product must articulate the design/user risks (the impact on the user if the change is implemented).
* Refined user stories: Engineering cannot build or implement these changes without fully refined stories that include clear acceptance criteria and edge-case definitions.

### 3.1. Feature flag (pre-prod)

This involves the feature being enabled in a pre-production environment while remaining disabled in production.

### 3.2. Feature flag (dedicated environment)

This is reserved for work with a high degree of risk where the feature is isolated in its own environment. Provides a
safe space for complex or potentially disruptive changes

### 3.3. Feature branch + dedicated environment

This is also reserved for high-risk or experimental work. Offers full isolation from main code paths, providing maximum
safety and control.
