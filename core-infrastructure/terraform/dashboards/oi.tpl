{
    "lenses": {
        "0": {
            "order": 0,
            "parts": {
                "0": {
                    "position": {
                        "x": 0,
                        "y": 0,
                        "colSpan": 3,
                        "rowSpan": 3
                    },
                    "metadata": {
                        "inputs": [],
                        "type": "Extension/HubsExtension/PartType/MarkdownPart",
                        "settings": {
                            "content": {
                                "settings": {
                                    "content": "### Performance",
                                    "title": "",
                                    "subtitle": "",
                                    "markdownSource": 1,
                                    "markdownUri": null
                                }
                            }
                        }
                    }
                },
                "1": {
                    "position": {
                        "x": 3,
                        "y": 0,
                        "colSpan": 5,
                        "rowSpan": 3
                    },
                    "metadata": {
                        "inputs": [
                            {
                                "name": "options",
                                "value": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "availabilityResults/availabilityPercentage",
                                                "aggregationType": 4,
                                                "namespace": "microsoft.insights/components",
                                                "metricVisualization": {
                                                    "resourceDisplayName": "${app_insights_name}",
                                                    "color": "#54A300"
                                                }
                                            }
                                        ],
                                        "title": "Availability",
                                        "titleKind": 2,
                                        "visualization": {
                                            "chartType": 2,
                                            "axisVisualization": {
                                                "y": {
                                                    "isVisible": true,
                                                    "min": 0,
                                                    "max": 100
                                                },
                                                "x": {
                                                    "isVisible": true
                                                }
                                            }
                                        },
                                        "openBladeOnClick": {
                                            "openBlade": true,
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "availability"
                                                },
                                                "extensionName": "HubsExtension",
                                                "options": {
                                                    "parameters": {
                                                        "id": "${app_insights_id}",
                                                        "menuid": "availability"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                "isOptional": true
                            },
                            {
                                "name": "sharedTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/HubsExtension/PartType/MonitorChartPart",
                        "settings": {
                            "content": {
                                "options": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "availabilityResults/availabilityPercentage",
                                                "aggregationType": 4,
                                                "namespace": "microsoft.insights/components",
                                                "metricVisualization": {
                                                    "resourceDisplayName": "${app_insights_name}",
                                                    "color": "#54A300"
                                                }
                                            }
                                        ],
                                        "title": "Availability",
                                        "titleKind": 2,
                                        "visualization": {
                                            "chartType": 2,
                                            "axisVisualization": {
                                                "y": {
                                                    "isVisible": true,
                                                    "min": 0,
                                                    "max": 100
                                                },
                                                "x": {
                                                    "isVisible": true
                                                }
                                            },
                                            "disablePinning": true
                                        },
                                        "openBladeOnClick": {
                                            "openBlade": true,
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "availability"
                                                },
                                                "extensionName": "HubsExtension",
                                                "options": {
                                                    "parameters": {
                                                        "id": "${app_insights_id}",
                                                        "menuid": "availability"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                "2": {
                    "position": {
                        "x": 8,
                        "y": 0,
                        "colSpan": 4,
                        "rowSpan": 3
                    },
                    "metadata": {
                        "inputs": [
                            {
                                "name": "options",
                                "value": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "performanceCounters/requestExecutionTime",
                                                "aggregationType": 4,
                                                "namespace": "microsoft.insights/components",
                                                "metricVisualization": {
                                                    "displayName": "HTTP request execution time"
                                                }
                                            }
                                        ],
                                        "title": "Avg HTTP request execution time for ${app_insights_name}",
                                        "titleKind": 1,
                                        "visualization": {
                                            "chartType": 2,
                                            "legendVisualization": {
                                                "isVisible": true,
                                                "position": 2,
                                                "hideSubtitle": false
                                            },
                                            "axisVisualization": {
                                                "x": {
                                                    "isVisible": true,
                                                    "axisType": 2
                                                },
                                                "y": {
                                                    "isVisible": true,
                                                    "axisType": 1
                                                }
                                            }
                                        },
                                        "timespan": {
                                            "relative": {
                                                "duration": 86400000
                                            },
                                            "showUTCTime": false,
                                            "grain": 1
                                        }
                                    }
                                },
                                "isOptional": true
                            },
                            {
                                "name": "sharedTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/HubsExtension/PartType/MonitorChartPart",
                        "settings": {
                            "content": {
                                "options": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "performanceCounters/requestExecutionTime",
                                                "aggregationType": 4,
                                                "namespace": "microsoft.insights/components",
                                                "metricVisualization": {
                                                    "displayName": "HTTP request execution time"
                                                }
                                            }
                                        ],
                                        "title": "Avg HTTP request execution time for ${app_insights_name}",
                                        "titleKind": 1,
                                        "visualization": {
                                            "chartType": 2,
                                            "legendVisualization": {
                                                "isVisible": true,
                                                "position": 2,
                                                "hideSubtitle": false
                                            },
                                            "axisVisualization": {
                                                "x": {
                                                    "isVisible": true,
                                                    "axisType": 2
                                                },
                                                "y": {
                                                    "isVisible": true,
                                                    "axisType": 1
                                                }
                                            },
                                            "disablePinning": true
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                "3": {
                    "position": {
                        "x": 12,
                        "y": 0,
                        "colSpan": 4,
                        "rowSpan": 3
                    },
                    "metadata": {
                        "inputs": [
                            {
                                "name": "options",
                                "value": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "requests/count",
                                                "aggregationType": 7,
                                                "namespace": "microsoft.insights/components",
                                                "metricVisualization": {
                                                    "resourceDisplayName": "${app_insights_name}",
                                                    "color": "#0078D4"
                                                }
                                            }
                                        ],
                                        "title": "Server requests",
                                        "titleKind": 2,
                                        "visualization": {
                                            "chartType": 3
                                        },
                                        "openBladeOnClick": {
                                            "openBlade": true,
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "performance"
                                                },
                                                "extensionName": "HubsExtension",
                                                "options": {
                                                    "parameters": {
                                                        "id": "${app_insights_id}",
                                                        "menuid": "performance"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                "isOptional": true
                            },
                            {
                                "name": "sharedTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/HubsExtension/PartType/MonitorChartPart",
                        "settings": {
                            "content": {
                                "options": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "requests/count",
                                                "aggregationType": 7,
                                                "namespace": "microsoft.insights/components",
                                                "metricVisualization": {
                                                    "resourceDisplayName": "${app_insights_name}",
                                                    "color": "#0078D4"
                                                }
                                            }
                                        ],
                                        "title": "Server requests",
                                        "titleKind": 2,
                                        "visualization": {
                                            "chartType": 3,
                                            "disablePinning": true
                                        },
                                        "openBladeOnClick": {
                                            "openBlade": true,
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "performance"
                                                },
                                                "extensionName": "HubsExtension",
                                                "options": {
                                                    "parameters": {
                                                        "id": "${app_insights_id}",
                                                        "menuid": "performance"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                "4": {
                    "position": {
                        "x": 16,
                        "y": 0,
                        "colSpan": 4,
                        "rowSpan": 3
                    },
                    "metadata": {
                        "inputs": [
                            {
                                "name": "options",
                                "value": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "requests/duration",
                                                "aggregationType": 4,
                                                "namespace": "microsoft.insights/components",
                                                "metricVisualization": {
                                                    "resourceDisplayName": "${app_insights_name}",
                                                    "color": "#0078D4"
                                                }
                                            }
                                        ],
                                        "title": "Server response time",
                                        "titleKind": 2,
                                        "visualization": {
                                            "chartType": 2
                                        },
                                        "openBladeOnClick": {
                                            "openBlade": true,
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "performance"
                                                },
                                                "extensionName": "HubsExtension",
                                                "options": {
                                                    "parameters": {
                                                        "id": "${app_insights_id}",
                                                        "menuid": "performance"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                "isOptional": true
                            },
                            {
                                "name": "sharedTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/HubsExtension/PartType/MonitorChartPart",
                        "settings": {
                            "content": {
                                "options": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "requests/duration",
                                                "aggregationType": 4,
                                                "namespace": "microsoft.insights/components",
                                                "metricVisualization": {
                                                    "resourceDisplayName": "${app_insights_name}",
                                                    "color": "#0078D4"
                                                }
                                            }
                                        ],
                                        "title": "Server response time",
                                        "titleKind": 2,
                                        "visualization": {
                                            "chartType": 2,
                                            "disablePinning": true
                                        },
                                        "openBladeOnClick": {
                                            "openBlade": true,
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "performance"
                                                },
                                                "extensionName": "HubsExtension",
                                                "options": {
                                                    "parameters": {
                                                        "id": "${app_insights_id}",
                                                        "menuid": "performance"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                "5": {
                    "position": {
                        "x": 0,
                        "y": 3,
                        "colSpan": 3,
                        "rowSpan": 4
                    },
                    "metadata": {
                        "inputs": [],
                        "type": "Extension/HubsExtension/PartType/MarkdownPart",
                        "settings": {
                            "content": {
                                "settings": {
                                    "content": "### Failures",
                                    "title": "",
                                    "subtitle": "",
                                    "markdownSource": 1,
                                    "markdownUri": null
                                }
                            }
                        }
                    }
                },
                "6": {
                    "position": {
                        "x": 3,
                        "y": 3,
                        "colSpan": 6,
                        "rowSpan": 4
                    },
                    "metadata": {
                        "inputs": [
                            {
                                "name": "options",
                                "value": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "requests/failed",
                                                "aggregationType": 7,
                                                "namespace": "microsoft.insights/components",
                                                "metricVisualization": {
                                                    "resourceDisplayName": "${app_insights_name}",
                                                    "color": "#EC008C"
                                                }
                                            }
                                        ],
                                        "title": "Failed requests",
                                        "titleKind": 2,
                                        "visualization": {
                                            "chartType": 3
                                        },
                                        "openBladeOnClick": {
                                            "openBlade": true,
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "failures"
                                                },
                                                "extensionName": "HubsExtension",
                                                "options": {
                                                    "parameters": {
                                                        "id": "${app_insights_id}",
                                                        "menuid": "failures"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                "isOptional": true
                            },
                            {
                                "name": "sharedTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/HubsExtension/PartType/MonitorChartPart",
                        "settings": {
                            "content": {
                                "options": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "requests/failed",
                                                "aggregationType": 7,
                                                "namespace": "microsoft.insights/components",
                                                "metricVisualization": {
                                                    "resourceDisplayName": "${app_insights_name}",
                                                    "color": "#EC008C"
                                                }
                                            }
                                        ],
                                        "title": "Failed requests",
                                        "titleKind": 2,
                                        "visualization": {
                                            "chartType": 3,
                                            "disablePinning": true
                                        },
                                        "openBladeOnClick": {
                                            "openBlade": true,
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "failures"
                                                },
                                                "extensionName": "HubsExtension",
                                                "options": {
                                                    "parameters": {
                                                        "id": "${app_insights_id}",
                                                        "menuid": "failures"
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                "7": {
                    "position": {
                        "x": 9,
                        "y": 3,
                        "colSpan": 6,
                        "rowSpan": 4
                    },
                    "metadata": {
                        "inputs": [
                            {
                                "name": "ResourceId",
                                "value": "${app_insights_id}"
                            },
                            {
                                "name": "ComponentId",
                                "value": {
                                    "SubscriptionId": "${subscription_id}",
                                    "ResourceGroup": "${resource_group_name}",
                                    "Name": "${app_insights_name}",
                                    "LinkedApplicationType": 0,
                                    "ResourceId": "${app_insights_id}",
                                    "ResourceType": "microsoft.insights/components",
                                    "IsAzureFirst": false
                                }
                            },
                            {
                                "name": "TargetBlade",
                                "value": "Failures"
                            },
                            {
                                "name": "DataModel",
                                "value": {
                                    "version": "1.0.0",
                                    "experience": 3,
                                    "clientTypeMode": "Server",
                                    "timeContext": {
                                        "durationMs": 86400000,
                                        "createdTime": "2021-04-06T21:56:10.624Z",
                                        "isInitialTime": false,
                                        "grain": 1,
                                        "useDashboardTimeRange": false
                                    },
                                    "prefix": "let OperationIdsWithExceptionType = (excType: string) { exceptions | where timestamp > ago(1d) \n    | where tobool(iff(excType == \"null\", isempty(type), type == excType)) \n    | distinct operation_ParentId };\nlet OperationIdsWithFailedReqResponseCode = (respCode: string) { requests | where timestamp > ago(1d)\n    | where iff(respCode == \"null\", isempty(resultCode), resultCode == respCode) and success == false \n    | distinct id };\nlet OperationIdsWithFailedDependencyType = (depType: string) { dependencies | where timestamp > ago(1d)\n    | where iff(depType == \"null\", isempty(type), type == depType) and success == false \n    | distinct operation_ParentId };\nlet OperationIdsWithFailedDepResponseCode = (respCode: string) { dependencies | where timestamp > ago(1d)\n    | where iff(respCode == \"null\", isempty(resultCode), resultCode == respCode) and success == false \n    | distinct operation_ParentId };\nlet OperationIdsWithExceptionBrowser = (browser: string) { exceptions | where timestamp > ago(1d)\n    | where tobool(iff(browser == \"null\", isempty(client_Browser), client_Browser == browser)) \n    | distinct operation_ParentId };",
                                    "grain": "5m",
                                    "selectedOperation": null,
                                    "selectedOperationName": null,
                                    "filters": []
                                },
                                "isOptional": true
                            },
                            {
                                "name": "Version",
                                "value": "1.0"
                            }
                        ],
                        "type": "Extension/AppInsightsExtension/PartType/FailuresCuratedPinnedChartPart",
                        "asset": {
                            "idInputName": "ResourceId",
                            "type": "ApplicationInsights"
                        }
                    }
                },
                "8": {
                    "position": {
                        "x": 15,
                        "y": 3,
                        "colSpan": 5,
                        "rowSpan": 4
                    },
                    "metadata": {
                        "inputs": [
                            {
                                "name": "options",
                                "value": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "requests/failed",
                                                "aggregationType": 7,
                                                "namespace": "microsoft.insights/components",
                                                "metricVisualization": {
                                                    "displayName": "Failed requests"
                                                }
                                            }
                                        ],
                                        "title": "Count Failed requests for ${app_insights_name}",
                                        "titleKind": 1,
                                        "visualization": {
                                            "chartType": 2,
                                            "legendVisualization": {
                                                "isVisible": true,
                                                "position": 2,
                                                "hideSubtitle": false
                                            },
                                            "axisVisualization": {
                                                "x": {
                                                    "isVisible": true,
                                                    "axisType": 2
                                                },
                                                "y": {
                                                    "isVisible": true,
                                                    "axisType": 1
                                                }
                                            }
                                        },
                                        "timespan": {
                                            "relative": {
                                                "duration": 86400000
                                            },
                                            "showUTCTime": false,
                                            "grain": 1
                                        }
                                    }
                                },
                                "isOptional": true
                            },
                            {
                                "name": "sharedTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/HubsExtension/PartType/MonitorChartPart",
                        "settings": {
                            "content": {
                                "options": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "requests/failed",
                                                "aggregationType": 7,
                                                "namespace": "microsoft.insights/components",
                                                "metricVisualization": {
                                                    "displayName": "Failed requests"
                                                }
                                            }
                                        ],
                                        "title": "Count Failed requests for ${app_insights_name}",
                                        "titleKind": 1,
                                        "visualization": {
                                            "chartType": 2,
                                            "legendVisualization": {
                                                "isVisible": true,
                                                "position": 2,
                                                "hideSubtitle": false
                                            },
                                            "axisVisualization": {
                                                "x": {
                                                    "isVisible": true,
                                                    "axisType": 2
                                                },
                                                "y": {
                                                    "isVisible": true,
                                                    "axisType": 1
                                                }
                                            },
                                            "disablePinning": true
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                "9": {
                    "position": {
                        "x": 0,
                        "y": 7,
                        "colSpan": 3,
                        "rowSpan": 4
                    },
                    "metadata": {
                        "inputs": [],
                        "type": "Extension/HubsExtension/PartType/MarkdownPart",
                        "settings": {
                            "content": {
                                "settings": {
                                    "content": "### Usage",
                                    "title": "",
                                    "subtitle": "",
                                    "markdownSource": 1,
                                    "markdownUri": null
                                }
                            }
                        }
                    }
                },
                "10": {
                    "position": {
                        "x": 3,
                        "y": 7,
                        "colSpan": 6,
                        "rowSpan": 4
                    },
                    "metadata": {
                        "inputs": [
                            {
                                "name": "options",
                                "value": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "sessions/count",
                                                "aggregationType": 5,
                                                "namespace": "microsoft.insights/components/kusto",
                                                "metricVisualization": {
                                                    "displayName": "Sessions",
                                                    "color": "#47BDF5"
                                                }
                                            },
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "users/count",
                                                "aggregationType": 5,
                                                "namespace": "microsoft.insights/components/kusto",
                                                "metricVisualization": {
                                                    "displayName": "Users",
                                                    "color": "#7E58FF"
                                                }
                                            }
                                        ],
                                        "title": "Unique sessions and users",
                                        "visualization": {
                                            "chartType": 2,
                                            "legendVisualization": {
                                                "isVisible": true,
                                                "position": 2,
                                                "hideSubtitle": false
                                            },
                                            "axisVisualization": {
                                                "x": {
                                                    "isVisible": true,
                                                    "axisType": 2
                                                },
                                                "y": {
                                                    "isVisible": true,
                                                    "axisType": 1
                                                }
                                            }
                                        },
                                        "openBladeOnClick": {
                                            "openBlade": true,
                                            "destinationBlade": {
                                                "extensionName": "HubsExtension",
                                                "bladeName": "ResourceMenuBlade",
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "segmentationUsers"
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            {
                                "name": "sharedTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/HubsExtension/PartType/MonitorChartPart",
                        "settings": {
                            "content": {
                                "options": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "sessions/count",
                                                "aggregationType": 5,
                                                "namespace": "microsoft.insights/components/kusto",
                                                "metricVisualization": {
                                                    "displayName": "Sessions",
                                                    "color": "#47BDF5"
                                                }
                                            },
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "users/count",
                                                "aggregationType": 5,
                                                "namespace": "microsoft.insights/components/kusto",
                                                "metricVisualization": {
                                                    "displayName": "Users",
                                                    "color": "#7E58FF"
                                                }
                                            }
                                        ],
                                        "title": "Unique sessions and users",
                                        "visualization": {
                                            "chartType": 2,
                                            "legendVisualization": {
                                                "isVisible": true,
                                                "position": 2,
                                                "hideSubtitle": false
                                            },
                                            "axisVisualization": {
                                                "x": {
                                                    "isVisible": true,
                                                    "axisType": 2
                                                },
                                                "y": {
                                                    "isVisible": true,
                                                    "axisType": 1
                                                }
                                            },
                                            "disablePinning": true
                                        },
                                        "openBladeOnClick": {
                                            "openBlade": true,
                                            "destinationBlade": {
                                                "extensionName": "HubsExtension",
                                                "bladeName": "ResourceMenuBlade",
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "segmentationUsers"
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                "11": {
                    "position": {
                        "x": 9,
                        "y": 7,
                        "colSpan": 6,
                        "rowSpan": 4
                    },
                    "metadata": {
                        "inputs": [
                            {
                                "name": "options",
                                "value": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "availabilityResults/availabilityPercentage",
                                                "aggregationType": 4,
                                                "namespace": "microsoft.insights/components",
                                                "metricVisualization": {
                                                    "displayName": "Availability",
                                                    "color": "#47BDF5"
                                                }
                                            }
                                        ],
                                        "title": "Average availability",
                                        "visualization": {
                                            "chartType": 3,
                                            "legendVisualization": {
                                                "isVisible": true,
                                                "position": 2,
                                                "hideSubtitle": false
                                            },
                                            "axisVisualization": {
                                                "x": {
                                                    "isVisible": true,
                                                    "axisType": 2
                                                },
                                                "y": {
                                                    "isVisible": true,
                                                    "axisType": 1
                                                }
                                            }
                                        },
                                        "openBladeOnClick": {
                                            "openBlade": true,
                                            "destinationBlade": {
                                                "extensionName": "HubsExtension",
                                                "bladeName": "ResourceMenuBlade",
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "availability"
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            {
                                "name": "sharedTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/HubsExtension/PartType/MonitorChartPart",
                        "settings": {
                            "content": {
                                "options": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "availabilityResults/availabilityPercentage",
                                                "aggregationType": 4,
                                                "namespace": "microsoft.insights/components",
                                                "metricVisualization": {
                                                    "displayName": "Availability",
                                                    "color": "#47BDF5"
                                                }
                                            }
                                        ],
                                        "title": "Average availability",
                                        "visualization": {
                                            "chartType": 3,
                                            "legendVisualization": {
                                                "isVisible": true,
                                                "position": 2,
                                                "hideSubtitle": false
                                            },
                                            "axisVisualization": {
                                                "x": {
                                                    "isVisible": true,
                                                    "axisType": 2
                                                },
                                                "y": {
                                                    "isVisible": true,
                                                    "axisType": 1
                                                }
                                            },
                                            "disablePinning": true
                                        },
                                        "openBladeOnClick": {
                                            "openBlade": true,
                                            "destinationBlade": {
                                                "extensionName": "HubsExtension",
                                                "bladeName": "ResourceMenuBlade",
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "availability"
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                "12": {
                    "position": {
                        "x": 15,
                        "y": 7,
                        "colSpan": 5,
                        "rowSpan": 4
                    },
                    "metadata": {
                        "inputs": [
                            {
                                "name": "options",
                                "value": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "pageViews/count",
                                                "aggregationType": 7,
                                                "namespace": "microsoft.insights/components",
                                                "metricVisualization": {
                                                    "displayName": "Page views"
                                                }
                                            }
                                        ],
                                        "title": "Count Page views for ${app_insights_name}",
                                        "titleKind": 1,
                                        "visualization": {
                                            "chartType": 2,
                                            "legendVisualization": {
                                                "isVisible": true,
                                                "position": 2,
                                                "hideSubtitle": false
                                            },
                                            "axisVisualization": {
                                                "x": {
                                                    "isVisible": true,
                                                    "axisType": 2
                                                },
                                                "y": {
                                                    "isVisible": true,
                                                    "axisType": 1
                                                }
                                            }
                                        },
                                        "timespan": {
                                            "relative": {
                                                "duration": 86400000
                                            },
                                            "showUTCTime": false,
                                            "grain": 1
                                        }
                                    }
                                },
                                "isOptional": true
                            },
                            {
                                "name": "sharedTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/HubsExtension/PartType/MonitorChartPart",
                        "settings": {
                            "content": {
                                "options": {
                                    "chart": {
                                        "metrics": [
                                            {
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                },
                                                "name": "pageViews/count",
                                                "aggregationType": 7,
                                                "namespace": "microsoft.insights/components",
                                                "metricVisualization": {
                                                    "displayName": "Page views"
                                                }
                                            }
                                        ],
                                        "title": "Count Page views for ${app_insights_name}",
                                        "titleKind": 1,
                                        "visualization": {
                                            "chartType": 2,
                                            "legendVisualization": {
                                                "isVisible": true,
                                                "position": 2,
                                                "hideSubtitle": false
                                            },
                                            "axisVisualization": {
                                                "x": {
                                                    "isVisible": true,
                                                    "axisType": 2
                                                },
                                                "y": {
                                                    "isVisible": true,
                                                    "axisType": 1
                                                }
                                            },
                                            "disablePinning": true
                                        }
                                    }
                                }
                            }
                        },
                        "filters": {
                            "MsPortalFx_TimeRange": {
                                "model": {
                                    "format": "local",
                                    "granularity": "auto",
                                    "relative": "1440m"
                                }
                            }
                        }
                    }
                },
                "13": {
                    "position": {
                        "x": 0,
                        "y": 11,
                        "colSpan": 3,
                        "rowSpan": 9
                    },
                    "metadata": {
                        "inputs": [],
                        "type": "Extension/HubsExtension/PartType/MarkdownPart",
                        "settings": {
                            "content": {
                                "settings": {
                                    "content": "# Application map\nSpot performance bottlenecks or failure hotspots across all components of your distributed application.",
                                    "title": "",
                                    "subtitle": "",
                                    "markdownSource": 1,
                                    "markdownUri": null
                                }
                            }
                        }
                    }
                },
                "14": {
                    "position": {
                        "x": 3,
                        "y": 11,
                        "colSpan": 17,
                        "rowSpan": 9
                    },
                    "metadata": {
                        "inputs": [
                            {
                                "name": "ComponentId",
                                "value": "${app_insights_id}"
                            },
                            {
                                "name": "MainResourceId",
                                "isOptional": true
                            },
                            {
                                "name": "ResourceIds",
                                "isOptional": true
                            },
                            {
                                "name": "TimeContext",
                                "value": {
                                    "durationMs": 3600000,
                                    "createdTime": "Tue Apr 06 2021 14:56:16 GMT-0700 (Pacific Daylight Time)",
                                    "isInitialTime": false,
                                    "grain": 1,
                                    "useDashboardTimeRange": false
                                },
                                "isOptional": true
                            },
                            {
                                "name": "ConfigurationId",
                                "value": "f576321d-1bf0-4340-abc2-125d3a1b016e",
                                "isOptional": true
                            },
                            {
                                "name": "DataModel",
                                "value": {
                                    "timeContext": {
                                        "durationMs": 3600000,
                                        "createdTime": "Tue Apr 06 2021 14:56:16 GMT-0700 (Pacific Daylight Time)",
                                        "isInitialTime": false,
                                        "grain": 1,
                                        "useDashboardTimeRange": false
                                    },
                                    "layoutOption": "Organic",
                                    "exclude4xxError": true
                                },
                                "isOptional": true
                            },
                            {
                                "name": "UseCallerTimeContext",
                                "isOptional": true
                            },
                            {
                                "name": "OverrideSettings",
                                "isOptional": true
                            },
                            {
                                "name": "PartId",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/AppInsightsExtension/PartType/ApplicationMapPart",
                        "asset": {
                            "idInputName": "ComponentId",
                            "type": "ApplicationInsights"
                        },
                        "deepLink": "#@platform.education.gov.uk/resource${app_insights_id}/applicationMap"
                    }
                }
            }
        }
    },
    "metadata": {
        "model": {
            "timeRange": {
                "value": {
                    "relative": {
                        "duration": 24,
                        "timeUnit": 1
                    }
                },
                "type": "MsPortalFx.Composition.Configuration.ValueTypes.TimeRange"
            },
            "filterLocale": {
                "value": "en-us"
            },
            "filters": {
                "value": {
                    "MsPortalFx_TimeRange": {
                        "model": {
                            "format": "utc",
                            "granularity": "auto",
                            "relative": "24h"
                        },
                        "displayCache": {
                            "name": "UTC Time",
                            "value": "Past 24 hours"
                        },
                        "filteredPartIds": [
                            "StartboardPart-MonitorChartPart-c45652d9-4625-417a-9595-578d3e10f06a",
                            "StartboardPart-MonitorChartPart-c45652d9-4625-417a-9595-578d3e10f06c",
                            "StartboardPart-MonitorChartPart-c45652d9-4625-417a-9595-578d3e10f06e",
                            "StartboardPart-MonitorChartPart-c45652d9-4625-417a-9595-578d3e10f070",
                            "StartboardPart-MonitorChartPart-c45652d9-4625-417a-9595-578d3e10f07c",
                            "StartboardPart-MonitorChartPart-c45652d9-4625-417a-9595-578d3e10f074",
                            "StartboardPart-MonitorChartPart-c45652d9-4625-417a-9595-578d3e10f078",
                            "StartboardPart-MonitorChartPart-c45652d9-4625-417a-9595-578d3e10f07e",
                            "StartboardPart-MonitorChartPart-c45652d9-4625-417a-9595-578d3e10f080"
                        ]
                    }
                }
            }
        }
    }
}