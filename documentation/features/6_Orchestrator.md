# Developer Feature Documentation: Pipeline Orchestrator

## Introduction

This document provides detailed information for developers about the implementation, usage and integration of the Pipeline Orchestrator feature within the system.

## Overview

//TODO

## Usage

//TODO

```mermaid
sequenceDiagram
    accDescr: Message flow sequence
    Actor Developer
    participant Platform API
    box Purple Storage queue
    participant data-pipeline-job-pending
    end
    participant Orchestrator
    box Purple Storage queue
    participant data-pipeline-job-custom-start
    end
    box Purple Storage queue
    participant data-pipeline-job-default-start
    end
    participant Data pipeline
    box Purple Storage queue    
    participant data-pipeline-job-finished
    end

    Platform API->>data-pipeline-job-pending: Produce
    Note over Platform API,data-pipeline-job-pending: Platform API generated message<br/>with the 'custom' message schema
    data-pipeline-job-pending-->>Orchestrator: Consume
    
    Developer->>data-pipeline-job-pending: Produce
    Note over Developer,data-pipeline-job-pending: Manually generated message <br/>with the 'default' message schema
    data-pipeline-job-pending-->>Orchestrator: Consume

    alt is 'custom' message type
        Orchestrator->>data-pipeline-job-custom-start: Produce
        Data pipeline-->>data-pipeline-job-custom-start: Consume
    else is 'default' message type
        Orchestrator->>data-pipeline-job-default-start: Produce
        Data pipeline-->>data-pipeline-job-default-start: Consume
    else
        Note right of Orchestrator: Exception
    end

    Data pipeline->>data-pipeline-job-finished: Produce
    data-pipeline-job-finished-->>Orchestrator: Consume
    alt is 'custom' message type
        Orchestrator->>Database: Update status
    else is 'default' message type
        Orchestrator->>Azure Search: Run indexers
    end
```

## Configuration

//TODO

## Known Issues

//TODO

<!-- Leave the rest of this page blank -->
\newpage
