# Transparency File: Current (As-Is) Process

## Purpose

This document records the current, end-to-end process for producing and publishing the FBIT transparency files, as it worked before the data pipeline began producing them. It also captures why the process is shaped the way it is, so that it can serve as the agreed baseline for any future change.

## What it is

The transparency file is a published Excel workbook that exposes the financial data behind FBIT in a downloadable form. There is one for CFR (maintained schools) and one for AAR (academies and trusts). Each carries an index tab that records the file version and a summary of what changed between versions.

It contains:

* raw data points from the financial returns,
* ancillary (contextual) data about each school, some of which is not held in FBIT,
* some FBIT roll-ups.

Assembling the file involves federation and DNS (Did Not Submit) handling, ancillary joins, and roll-ups. Where columns overlap with FBIT, the figures reconcile with the service.

CFR and AAR run as two separate annual cycles. Each file follows its own financial return (the CFR return becomes available around September and the AAR return around January) and is published later, once the national statistics publication allows.

The primary users are DfE analysts, but because publishing the file is a legal requirement it is available to any concerned party.

## Why the process is shaped this way

These are the drivers behind the process, not incidental details. They are not an exhaustive list; the aim is to understand the process holistically, and further drivers should be added as they surface.

* **Statutory transparency.** Publishing the underlying financial data in a downloadable form is a legal requirement. The transparency file is how that obligation is met, which is why it is governed far more tightly than an ordinary service download.
* **More than an FBIT export.** The file includes ancillary data that the service does not surface, so it is a statutory publication in its own right rather than a copy of FBIT. Where the data does overlap, the two must reconcile.
* **Separate from, and released later than, the FBIT data.** The transparency file has never been released alongside the FBIT data. Publication is gated by the national statistics publication and cannot happen before it, so the two have always run on separate timelines. A file is often generated, checked, and signed off well before it can be published, and is then held until release is allowed. As a result, the service data and the published file are frequently out of step for a period.
* **Independently assured and human-gated.** Because errors in a statutory publication carry external, legal, and reputational consequences, the file is verified independently before it goes live, through internal QA, DfE review, and DfE sign-off. It does not pass through the lower environments; it is produced and checked independently, and is only updated in pre-production and production.
* **Versioned.** The file is updated once for each release, and after that only on a need basis, when there is a reason to update it. For example, when a data fix is made after release, the same fix is applied to the file. Each update produces a new version, carrying a version number and an index tab telling consumers which cut of the data they hold and what has changed. Populating the index tab (version and change summary) is a shared responsibility, and QA confirms it has been added before the file is considered done.

## Roles

The process is a single, end-to-end flow that spans the FBIT team and DfE. DfE are participants in the flow, providing assurance and the statutory sign-off, not simply an external gate.

| Role          | Responsibility                                                                                          |
|:--------------|:-------------------------------------------------------------------------------------------------------|
| Product Owner | Gives the go-ahead to start, monitors the national statistics publication, and gives the go/no-go to publish. |
| Data Analyst  | Sources the return and ancillary data, and produces the file from the SQL scripts.                     |
| QA            | Runs the internal checks before release and the final check after publish, including the index tab.     |
| DfE           | Review the file and the numbers, and provide the statutory sign-off.                                    |
| Data Engineer | Publishes the approved file to the service.                                                             |

## The process

### 1. Go-ahead and data cut

Once the financial return data (CFR or AAR) is available, the Product Owner gives the go-ahead to start. The Product Owner does not provide the data itself; the team sources the cut of return data from source, and that cut is the basis for the release.

### 2. Source the ancillary data

All ancillary sources (for example GIAS, Census, SEN, PRU, and hospital schools) are sourced and collected into the local database. These provide the contextual data about each school that aligns with the return, and must be in place before the file is generated.

### 3. Generate the file

The CFR and AAR transparency files are produced by running their respective SQL scripts against the return and ancillary data. The output is the transparency workbook for that release.

### 4. Internal QA

The file is checked internally before release. Checks include reconciling the totals against FBIT, confirming federation and DNS handling so schools are not double-counted, and checking completeness against the previous year. Manual corrections are applied at this stage when something is found to be wrong, or when a requirement comes from the client (DfE) to update a field to align with FBIT. If issues are found, the file is corrected, regenerated, and re-checked.

### 5. DfE review

The file is passed to DfE, who verify it independently through two activities: DfE review the file, and then data analysts within DfE review the numbers. Any issues raised are fed back, fixed, and the file is regenerated and re-checked.

### 6. Sign-off

Once satisfied, DfE sign off the file. This authorises publication on content grounds, but publication itself is still gated by the national statistics publication. The Product Owner keeps an eye on the national statistics publication and gives the go/no-go call for when the file can be published.

### 7. Publish the file

On the Product Owner's go, the Data Engineer publishes the approved file to the service, following the standard process for updating files on the service. The version and change summary are added to the index tab as part of this.

### 8. Final QA check

QA does a final check to confirm the correct file is now available on the service, and that the version and change summary have been added to the index tab.

## Corrections after publication

The file is not frozen once published; it has been revised after release on more than one occasion. When a correction is needed, the file is checked internally, the version number is updated, and the corrected file is made live. This is a lighter-weight internal update and does not go back through the full DfE review and sign-off.

<!-- Leave the rest of this page blank -->
\newpage
