# Developer Feature Documentation: Number Rounding for Charts and Tables

## Introduction

This document describes the rounding logic used across tables and charts within the service. The goal is to ensure numerical values are displayed consistently and clearly while maintaining accuracy where appropriate.

## Overview

Rounding rules differ depending on:

- Whether the value contains decimals
- Whether the value is **currency**, **percentage**, or a **ratio**
- Whether the value is displayed in a **table** or a **chart**
- Whether the value is **above or below 1000** (currency only)
- Whether the value is a **ratio**, which is rounded to **2 decimal places**

These rules must be applied in the correct order to ensure consistent behaviour.

## Rounding Rules

### Rounding to the Nearest Pound (Currency Only)

Any currency value containing decimals is rounded to the nearest whole pound before any further formatting.

This applies to **both table and chart views**.

### Chart‑Specific Rounding for Currency Values ≥ 1000

After rounding to the nearest pound:

- If the value is **1000 to < 1 million**, charts convert it to a **“k” format**, rounded to **one decimal place**.
- If the value is **1 million or greater**, charts convert it to an **“m” format**, rounded to **one decimal place**.
- If the value is **less than 1000**, charts display the exact rounded value.

The chart format rules are based on [SI prefixes](https://en.wikipedia.org/wiki/Metric_prefix#List_of_SI_prefixes)

### Percentage Rounding

Percentage values follow a different rule:

- Percentages are **always rounded to 1 decimal place**.
- This applies to **both table and chart views**.
- No “k” or “m” formatting is ever applied to percentages.
- The rounded value is displayed exactly as rounded (e.g., 12.345% → 12.3%).

### Ratio Rounding

Ratios follow their own rule:

- Ratios are **always rounded to 2 decimal places**.
- This applies to **both table and chart views**.
- No “k” or “m” formatting is ever applied to ratios.
- The rounded value is displayed exactly as rounded (e.g., 0.12345 → 0.12).

## Summary of Behaviour

### Table View

- Currency values are rounded to the nearest pound.
- Percentage values are rounded to **1 decimal place**.
- Ratio values are rounded to **2 decimal places**.
- No “k” or “m” formatting is used.

### Chart View

- Currency values are rounded to the nearest pound, then:
  - If < 1000 → shown as the rounded value
  - If ≥ 1000 and < 1,000,000 → formatted as “k”
  - If ≥ 1,000,000 → formatted as “m”
- Percentage values are rounded to **1 decimal place** and shown exactly as rounded.
- Ratio values are rounded to **2 decimal places** and shown exactly as rounded.
- Only currency values use SI‑prefix‑based formatting.

## Screenshots

### Currency Rounding (Nearest Pound)

![Currency rounding example](./images/Table-rounded-to-nearest-£.png)

### Currency Rounding in Charts (k Format)

![Currency k-format example](./images/charts-rounded-to-K-format.png)

### Percentage Rounding (1 Decimal Place)

![Percentage rounding example](./images/chart-percentage-rounding.png)

### Ratio Rounding (2 Decimal Places)

![Ratio rounding example](./images/chart-ratio-rounding.png)

## Future Enhancements

### Visual Misalignment for Very Small Currency Values (e.g., 0 → 0.45)

When decimals are rounded:

- 0.45 becomes **0** for display
- But the chart bar height still reflects the original value (0.45)

This can cause misleading visuals:

- School A: £0
- School B: £0.45

Both may display as 0, but the bar for £0.45 will appear longer.

This creates a mismatch between:

- **Displayed label**
- **Visual bar length**

![Small value misalignment screenshot](./images/chart-showing-potential-issue-with-rounding.png)

Potential improvements include:

- Aligning table content with chart formatting to show the same values in both formats
- Using the rounded value (not the raw value) for chart scaling
- Introducing a minimum visible bar height for non‑zero values
- Displaying very small values as “<£1” instead of rounding to 0 or 1
- Enhancing tooltips to show the exact underlying value
- Applying similar visual‑consistency rules to percentage and ratio bars if needed

<!-- Leave the rest of this page blank -->
\newpage
