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
    participant data-pipeline-job-start
    end
    participant Data pipeline
    box Purple Storage queue    
    participant data-pipeline-job-finished
    end
    Platform API->>data-pipeline-job-pending: Produce
    Note over Platform API,data-pipeline-job-pending: Platform API generated message
    data-pipeline-job-pending-->>Orchestrator: Consume
    Orchestrator->>data-pipeline-job-start: Produce
    Developer->>data-pipeline-job-start: Produce
    Note over Developer,data-pipeline-job-start: Mnanually generated message 
    data-pipeline-job-start-->>Data pipeline: Consume
    Data pipeline->>data-pipeline-job-finished: Produce
    data-pipeline-job-finished-->>Orchestrator: Consume
    Orchestrator->>Orchestrator: Log completion
```


## Configuration
//TODO

## Known Issues
//TODO


<!-- Leave the rest of this page blank -->
\newpage