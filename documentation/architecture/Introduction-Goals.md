# Introduction and Goals

## Introduction

### Background

Currently the Department for Education (DfE) produces two major sets of financial data:

- School Financial Benchmarking (SFB) - publicly available data that meets the DfE's statutory data transparency requirements
- View My Financial Insights (VMFI) - only accessible to schools, academy trusts, and Local Authorities (LAs) as it includes data nmot in the public domain 
Both of these services allow users to compare a selected school with similar schools. They use largely the same dataset, but are independent of each other. This leads to a duplication of effort and incosistencies as each system analyses the data differently.

![High level viewpoint](images/High-Level-Viewpoint.png)

## Goals and Drivers

![Goals and drivers](images/Goals-and-Drivers.png)

| Type      | Description |
|:---------:|:------------|       
| **Goal** | Provide a robust, stable platform that enables the viewing of default benchmarked data |
| **Goal** | Provide a robust, stable platform that enables the creation of custom comparator sets |
| **Goal** | Provide reporting and insights to enable improvement of the service |
| **Driver** | Reduce the total cost of ownership  |
| **Driver** | Reduce the time taken to import new yearly data |

## Requirements Overview

The requirements for a Minimum Viable Product (MVP) are:

- A single service that replaces both SFB and VMFI, whilst retaining all the features of both systems. Where there is a currentl overlap of feature, a decision will be made and logged as to which functionality will be retained
- A reduction and simplicifcation of the infrastructure hosting the service
- A CI/CD pipeline enabling automated testing and deployment of changes to the system in the most effective manner

The project is run in an Agile manner, and the backlog is managed in [Azure DevOps](https://dev.azure.com/dfe-ssp/s198-DfE-Benchmarking-service).

## Stakeholders

TBD

**//TODO: Insert or link to the stakeholder map here**
