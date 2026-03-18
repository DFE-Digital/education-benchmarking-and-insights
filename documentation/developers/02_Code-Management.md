# Code Management & Quality Standards

These are our shared standards for managing our code, from initial repository access to merging into the main branch. Our goal is to maintain a secure, high-quality, and fast-moving codebase while fostering a culture of continuous learning and transparent collaboration.

## Repository Access (Who can change the code)

We follow a strict security process to protect our code. Access is granted based on what is necessary for individual roles, following these standards:

* **Permission Levels:** Most team members are granted **Read** or **Write** access to perform daily tasks. **Admin** access is strictly limited to a small number of team members who manage repository settings.
* **Security & Secrets:** We never store passwords, API keys, or any private data within the code. This prevents sensitive information from being exposed in our history and protects the project from potential leaks.
* **User Access Reviews:** Access is reviewed regularly to ensure our user list is up to date. If a team member leaves the project, their access is removed immediately to maintain the integrity of the repository.

## Branching Policy (Trunk-based)

We use a **trunk-based** approach, a strategy where all developers work on a single branch (the main branch or "trunk") and merge changes frequently.

* **What is Trunk-based?** Instead of working on long-running branches for weeks, we break work into the smallest possible parts to keep the codebase moving. This often involves merging incomplete features hidden behind feature toggles. See the [Feature Flags Management Guide](12_Feature-Flags-Management-Guide.md) for more details.
* **Why we use it:** This approach increases our **speed** by allowing us to release features faster, **reduces conflict** by preventing "merge hell," and improves **team visibility** by keeping everyone aligned in real-time.
* **Short-lived Branches:** We create small branches that are merged back into main as soon as possible, typically within a day or two.
* **Branch Protection:** We never push code directly to the main branch. Everything must go through a Pull Request (PR) to ensure it is tested and reviewed. While rare exceptions may be made (such as urgent emergency fixes), these must be agreed upon by the tech lead first.

## Code Reviews

To ensure high quality and a clear project history, we follow these manual standards during every review:

* **PR Size:** Aim for PRs under 400 lines of code. Small PRs are critical for Trunk-based development and effective, timely code reviews.
* **PR Templates:** We complete the [PR template](../../.github/pull_request_template.md) for every Pull Request to ensure consistency and provide reviewers with necessary context. This serves as our manual checklist to ensure all quality standards are met before a review begins.
* **Capture Review Narrative:** When review discussions or decisions happen offline, we post a short summary in the PR thread. This keeps our decision-making transparent and ensures the full history of the work is documented and accessible to the whole team.
* **Encourage Positive Feedback:** We use reviews as an opportunity to support one another. When we encounter clever solutions or clean implementations, we leave a friendly comment to say so. We avoid empty approvals like "LGTM" and instead aim to highlight at least one specific strength of the code.
* **Quality Gates & Knowledge Sharing:** Pull Requests are our collective quality gate and a vital tool for sharing knowledge. We encourage everyone to perform initial logic checks to learn how the system works. If there is any uncertainty about a change, we bring in others rather than letting it through.

## Automated Checks and Rules

We use automation to handle repetitive tasks, allowing the team to focus on logic rather than formatting. These rules are enforced automatically:

* **Branch Naming Standards:** Branches must follow the pattern `<category>/<backlog-item>/<task>-description-in-kebab-case`. This consistent structure helps us identify the purpose and origin of a branch at a glance.
  * **Categories:** `feature`, `bugfix`, `hotfix`, `exp`, `tech-debt`, `docs`, `prototype`, `dependabot`, or `chore`.
  * **Backlog Item:** The ID for the user story, bug, or spike (e.g., `1234`). If no formal item exists, we omit this section.
  * **Task:** The task ID and a **kebab-case** description (e.g., `5678-add-login-validation`).
  * **Rule:** We create branches **per task** to keep changes focused.
  * **Example:** `feature/1234/5678-add-login-validation`
* **PR Labels:** Every PR must have a label to help us organise our work. Labels allow anyone to see immediately if a PR is a bug fix, a new feature, or a documentation update.
* **Commit Title Standards:** PR titles must follow the [Conventional Commits](https://www.conventionalcommits.org) standard using the pattern `<type>(<optional-scope>): <description>`. This consistent structure allows us to generate automatic changelogs and makes our project history easy to read.
  * **Types:** `feat` (new feature), `fix` (bug fix), `docs`, `style`, `refactor`, `perf`, `test`, or `chore`.
  * **Scope:** An optional noun describing the section of the codebase (e.g., `auth`).
  * **Description:** We use the imperative mood (e.g., "add" instead of "added").
  * **Breaking Changes:** Indicate breaking changes by appending a `!` after the type/scope, or by including a `BREAKING CHANGE:` footer.
  * **Example:** `feat(auth): add login validation`
* **Ticket Linking:** Every PR must be linked to an Azure DevOps (ADO) ticket. This provides "traceability," connecting our code changes directly to the original task or requirement. To automatically link a ticket, include `AB#<ticket-number>` (e.g., `AB#1234`) in the PR description or commit message.
* **Squash and Merge Method:** GitHub is configured to only allow "Squash and merge." This keeps our Git history clean by combining multiple small commits into one polished update upon merging.

<!-- Leave the rest of this page blank -->
\newpage
