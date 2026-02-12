# DSI Pre-Production Test Organisations and Account Management

This document provides an overview of the DSI pre-production test organisations and the management of associated accounts used across automated testing

## Ownership and Administration

The **Test Engineer** on the team is the current owner of these organisations and the associated test accounts.

### Responsibilities

**Primary Ownership:** Responsible for the maintenance and oversight of all test organisations.

**Main Approver:** Acting as the primary approver for access requests within these organisations.

**User Management:** Responsible for adding and managing users within these organisations as needed.

**Role Transition:** In the event of a personnel change, the incoming Test Engineer assumes responsibility for creating new test accounts, assigning correct organisations and management of automated test accounts.

## Automated Test Accounts

The following accounts are managed by the Test Engineer and are linked to specific organisations for automated test suites:

| Test Account Email                                     | Linked Organisations |
|:-------------------------------------------------------| :--- |
| `{testEnginnerEmailPrefix}+fbit.e2e@education.gov.uk`  | 002: Local Authority<br>01: Community School<br>010: Multi-Academy Trust<br>013: Single-Academy Trust 1<br>32: Special Post 16 Institution |
| `{testEnginnerEmailPrefix}+fbit.a11y@education.gov.uk` | 05: Foundation School<br>07: Community Special School |

## Test Organisations Overview

The Following organsiations have been set up in pre prod.

| Category              | Name                                         | Identifier                            |
|-----------------------|----------------------------------------------|---------------------------------------|
| Local Authority       | 002 - FBIT TEST - Local Authority            | Establishment Number: 006             |
| Multi-Academy Trust   | 010 - FBIT TEST - Multi-Academy Trust        | Company Registration Number: 00000001 |
| Single- Academy Trust | 013 - FBIT TEST - Single-Academy Trust 1     | Company Registration Number: 00000002 |
|                       | 013 - FBIT TEST - Single-Academy Trust 2     | Company Registration Number: 00000003 |
|                       | 013 - FBIT TEST - Single-Academy Trust 3     | Company Registration Number: 00000004 |
|                       | 013 - FBIT TEST - Single-Academy Trust 4     | Company Registration Number: 00000005 |
|                       | 013 - FBIT TEST - Single-Academy Trust 5     | Company Registration Number: 00000006 |
|                       | 013 - FBIT TEST - Single-Academy Trust 6     | Company Registration Number: 00000007 |
| Schools               | 01 - FBIT TEST - Community School            | URN: 777042                           |
|                       | 05 - FBIT TEST - Foundation School           | URN: 777043                           |
|                       | 07 - FBIT TEST - Community Special School    | URN: 777044                           |
|                       | 14 - FBIT TEST - Pupil Referral Unit         | URN: 777045                           |
|                       | 15 - FBIT TEST - LA Nursery School           | URN: 777046                           |
|                       | 32 - FBIT TEST - Special Post 16 Institution | URN: 777047                           |
