---
id: ASSUME-002
title: "Pagefind Optional Dependencies Compatibility with npm ci --ignore-scripts"
component: "docs/ci"
domain: "documentation"
tags: ["pagefind", "github-actions", "npm", "security"]
status: "validated"
created: 2026-11-17
updated: 2026-11-17
resolved: 2026-11-17
---

## Assumption

Installing `pagefind` via `npm ci --ignore-scripts` in `.github/workflows/docs-ci-cd.yml` provides the functional Linux x64 Pagefind binary without enabling npm lifecycle install scripts.

## Context

`.github/workflows/docs-ci-cd.yml` runs `npm ci --ignore-scripts` on `ubuntu-latest` for supply-chain security. Pagefind v1.6+ distributes platform binaries through npm `optionalDependencies` (`@pagefind/linux-x64`, `@pagefind/darwin-arm64`, and others) rather than a `postinstall` download script. Preserving `--ignore-scripts` avoids weakening CI security controls.

## Resolution

Validated on 2026-11-17 by confirming `docs/package-lock.json` locks platform-specific `@pagefind/*` optional dependencies (`@pagefind/linux-x64`, `@pagefind/darwin-arm64`, and others) with `--ignore-scripts` and builds the Pagefind index without lifecycle install scripts.
