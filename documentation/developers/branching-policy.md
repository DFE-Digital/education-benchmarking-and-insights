# Branching Policy

This document is designed to outline the current approach to branching.

## Trunk based development

A trunk-based development workflow has been adopted within this project - trunk-based development workflow is one of the most popular development frameworks among developer teams.

This branching model allows developers to collaborate on code in a single branch called ‘trunk’, resisting any pressure to create other long-lived development branches. They therefore avoid merge hell, do not break the build, and *live happily ever after*.

Fellow developers must then perform a code reviews before merging the checked-out branch with the main branch. The crucial thing about checked-out branches is that they are short-lived, spanning two to three days at most.

The main branch should always be production-ready. That means the team should thoroughly test each code change before pushing them to the main branch. Short development cycles and automated testing enable the team to identify defects and recover from failed builds quickly, reducing the risk.

One of the main benefits of this approach is that it integrates well with existing continuous integration and continuous delivery (CI/CD) services. When the team pushes each commit to the main branch, it runs automated tests (part of CI) to verify that the new changes do not break the main branch. Then, after all the tests successfully complete, the pipelines are configured to create deployments.


## Branch naming

Branches should be named using the following pattern `category/<reference>/description-in-kebab-case`. With categories;
- `feature` is for adding, refactoring or removing a feature
- `bugfix` is for fixing a bug
- `hotfix` is for changing code with a temporary solution and/or without following the usual process (usually because of an emergency)
- `test` is for experimenting outside of an issue/ticket
- `tech-debt` is for technical debt 
- `doc` is for documentation changes