# Developer Feature Documentation: Pipeline Orchestrator

## Introduction

This document provides detailed information for developers about the implementation, usage and integration of the Pipeline Orchestrator feature within the system.

## Overview

//TODO

## Orchestrations

### PipelineJobOrchestrator

```mermaid
sequenceDiagram
    accDescr: Message flow sequence
    Actor Developer
    participant Platform API
    box Purple Storage queue
    participant data-pipeline-job-pending
    end
    participant PipelineJobOrchestrator
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
    data-pipeline-job-pending-->>PipelineJobOrchestrator: Consume 
    
    Developer->>data-pipeline-job-pending: Produce
    Note over Developer,data-pipeline-job-pending: Manually generated message <br/>with the 'default' message schema
    data-pipeline-job-pending-->>PipelineJobOrchestrator: Consume

    alt is 'custom' message type
        PipelineJobOrchestrator->>data-pipeline-job-custom-start: Produce
        Data pipeline-->>data-pipeline-job-custom-start: Consume
    else is 'default' message type
        PipelineJobOrchestrator->>data-pipeline-job-default-start: Produce
        Data pipeline-->>data-pipeline-job-default-start: Consume
    else
        Note right of PipelineJobOrchestrator: Exception
    end

    Data pipeline->>data-pipeline-job-finished: Produce
    data-pipeline-job-finished-->>PipelineJobOrchestrator: Consume
    alt is 'custom' message type
        PipelineJobOrchestrator->>Database: Update status
    else is 'default' message type
        PipelineJobOrchestrator-->>PipelineJobDefaultFinished: Consume
        Note over PipelineJobDefaultFinished: Sub-orchestration
    end
```

### PipelineJobDefaultFinished

```mermaid
sequenceDiagram   
    External-->>PipelineJobDefaultFinished: Consume

    alt is success status
        par
            PipelineJobDefaultFinished->>Azure Search: Run indexers
        and 
            PipelineJobDefaultFinished->>Distributed Cache: Clear cache
        and 
            PipelineJobDefaultFinished->>Database: De-activate UserData
        end
    end
```

## Configuration

//TODO

## Known Issues

//TODO

<!-- Leave the rest of this page blank -->
\newpage
