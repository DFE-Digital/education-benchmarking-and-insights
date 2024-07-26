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
                                    "markdownSource": 1,
                                    "markdownUri": null,
                                    "subtitle": "",
                                    "title": ""
                                }
                            }
                        }
                    }
                },
                "1": {
                    "position": {
                        "x": 3,
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
                                                "aggregationType": 4,
                                                "metricVisualization": {
                                                    "color": "#54A300",
                                                    "resourceDisplayName": "${app_insights_name}"
                                                },
                                                "name": "availabilityResults/availabilityPercentage",
                                                "namespace": "microsoft.insights/components",
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                }
                                            }
                                        ],
                                        "openBladeOnClick": {
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "extensionName": "HubsExtension",
                                                "options": {
                                                    "parameters": {
                                                        "id": "${app_insights_id}",
                                                        "menuid": "availability"
                                                    }
                                                },
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "availability"
                                                }
                                            },
                                            "openBlade": true
                                        },
                                        "title": "Availability",
                                        "titleKind": 2,
                                        "visualization": {
                                            "axisVisualization": {
                                                "x": {
                                                    "isVisible": true
                                                },
                                                "y": {
                                                    "isVisible": true,
                                                    "max": 100,
                                                    "min": 0
                                                }
                                            },
                                            "chartType": 2
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
                                                "aggregationType": 4,
                                                "metricVisualization": {
                                                    "color": "#54A300",
                                                    "resourceDisplayName": "${app_insights_name}"
                                                },
                                                "name": "availabilityResults/availabilityPercentage",
                                                "namespace": "microsoft.insights/components",
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                }
                                            }
                                        ],
                                        "openBladeOnClick": {
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "extensionName": "HubsExtension",
                                                "options": {
                                                    "parameters": {
                                                        "id": "${app_insights_id}",
                                                        "menuid": "availability"
                                                    }
                                                },
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "availability"
                                                }
                                            },
                                            "openBlade": true
                                        },
                                        "title": "Availability",
                                        "titleKind": 2,
                                        "visualization": {
                                            "axisVisualization": {
                                                "x": {
                                                    "isVisible": true
                                                },
                                                "y": {
                                                    "isVisible": true,
                                                    "max": 100,
                                                    "min": 0
                                                }
                                            },
                                            "chartType": 2,
                                            "disablePinning": true
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                "2": {
                    "position": {
                        "x": 7,
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
                                                "aggregationType": 4,
                                                "metricVisualization": {
                                                    "displayName": "HTTP request execution time"
                                                },
                                                "name": "performanceCounters/requestExecutionTime",
                                                "namespace": "microsoft.insights/components",
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                }
                                            }
                                        ],
                                        "timespan": {
                                            "grain": 1,
                                            "relative": {
                                                "duration": 86400000
                                            },
                                            "showUTCTime": false
                                        },
                                        "title": "Avg HTTP request execution time for ${app_insights_name}",
                                        "titleKind": 1,
                                        "visualization": {
                                            "axisVisualization": {
                                                "x": {
                                                    "axisType": 2,
                                                    "isVisible": true
                                                },
                                                "y": {
                                                    "axisType": 1,
                                                    "isVisible": true
                                                }
                                            },
                                            "chartType": 2,
                                            "legendVisualization": {
                                                "hideSubtitle": false,
                                                "isVisible": true,
                                                "position": 2
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
                                                "aggregationType": 4,
                                                "metricVisualization": {
                                                    "displayName": "HTTP request execution time"
                                                },
                                                "name": "performanceCounters/requestExecutionTime",
                                                "namespace": "microsoft.insights/components",
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                }
                                            }
                                        ],
                                        "title": "Avg HTTP request execution time for ${app_insights_name}",
                                        "titleKind": 1,
                                        "visualization": {
                                            "axisVisualization": {
                                                "x": {
                                                    "axisType": 2,
                                                    "isVisible": true
                                                },
                                                "y": {
                                                    "axisType": 1,
                                                    "isVisible": true
                                                }
                                            },
                                            "chartType": 2,
                                            "disablePinning": true,
                                            "legendVisualization": {
                                                "hideSubtitle": false,
                                                "isVisible": true,
                                                "position": 2
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                "3": {
                    "position": {
                        "x": 11,
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
                                                "aggregationType": 7,
                                                "metricVisualization": {
                                                    "color": "#0078D4",
                                                    "resourceDisplayName": "${app_insights_name}"
                                                },
                                                "name": "requests/count",
                                                "namespace": "microsoft.insights/components",
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                }
                                            }
                                        ],
                                        "openBladeOnClick": {
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "extensionName": "HubsExtension",
                                                "options": {
                                                    "parameters": {
                                                        "id": "${app_insights_id}",
                                                        "menuid": "performance"
                                                    }
                                                },
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "performance"
                                                }
                                            },
                                            "openBlade": true
                                        },
                                        "title": "Server requests",
                                        "titleKind": 2,
                                        "visualization": {
                                            "chartType": 3
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
                                                "aggregationType": 7,
                                                "metricVisualization": {
                                                    "color": "#0078D4",
                                                    "resourceDisplayName": "${app_insights_name}"
                                                },
                                                "name": "requests/count",
                                                "namespace": "microsoft.insights/components",
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                }
                                            }
                                        ],
                                        "openBladeOnClick": {
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "extensionName": "HubsExtension",
                                                "options": {
                                                    "parameters": {
                                                        "id": "${app_insights_id}",
                                                        "menuid": "performance"
                                                    }
                                                },
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "performance"
                                                }
                                            },
                                            "openBlade": true
                                        },
                                        "title": "Server requests",
                                        "titleKind": 2,
                                        "visualization": {
                                            "chartType": 3,
                                            "disablePinning": true
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                "4": {
                    "position": {
                        "x": 15,
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
                                                "aggregationType": 4,
                                                "metricVisualization": {
                                                    "color": "#0078D4",
                                                    "resourceDisplayName": "${app_insights_name}"
                                                },
                                                "name": "requests/duration",
                                                "namespace": "microsoft.insights/components",
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                }
                                            }
                                        ],
                                        "openBladeOnClick": {
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "extensionName": "HubsExtension",
                                                "options": {
                                                    "parameters": {
                                                        "id": "${app_insights_id}",
                                                        "menuid": "performance"
                                                    }
                                                },
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "performance"
                                                }
                                            },
                                            "openBlade": true
                                        },
                                        "title": "Server response time",
                                        "titleKind": 2,
                                        "visualization": {
                                            "chartType": 2
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
                                                "aggregationType": 4,
                                                "metricVisualization": {
                                                    "color": "#0078D4",
                                                    "resourceDisplayName": "${app_insights_name}"
                                                },
                                                "name": "requests/duration",
                                                "namespace": "microsoft.insights/components",
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                }
                                            }
                                        ],
                                        "openBladeOnClick": {
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "extensionName": "HubsExtension",
                                                "options": {
                                                    "parameters": {
                                                        "id": "${app_insights_id}",
                                                        "menuid": "performance"
                                                    }
                                                },
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "performance"
                                                }
                                            },
                                            "openBlade": true
                                        },
                                        "title": "Server response time",
                                        "titleKind": 2,
                                        "visualization": {
                                            "chartType": 2,
                                            "disablePinning": true
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
                                    "markdownSource": 1,
                                    "markdownUri": null,
                                    "subtitle": "",
                                    "title": ""
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
                                                "aggregationType": 7,
                                                "metricVisualization": {
                                                    "color": "#EC008C",
                                                    "resourceDisplayName": "${app_insights_name}"
                                                },
                                                "name": "requests/failed",
                                                "namespace": "microsoft.insights/components",
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                }
                                            }
                                        ],
                                        "openBladeOnClick": {
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "extensionName": "HubsExtension",
                                                "options": {
                                                    "parameters": {
                                                        "id": "${app_insights_id}",
                                                        "menuid": "failures"
                                                    }
                                                },
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "failures"
                                                }
                                            },
                                            "openBlade": true
                                        },
                                        "title": "Failed requests",
                                        "titleKind": 2,
                                        "visualization": {
                                            "chartType": 3
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
                                                "aggregationType": 7,
                                                "metricVisualization": {
                                                    "color": "#EC008C",
                                                    "resourceDisplayName": "${app_insights_name}"
                                                },
                                                "name": "requests/failed",
                                                "namespace": "microsoft.insights/components",
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                }
                                            }
                                        ],
                                        "openBladeOnClick": {
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "extensionName": "HubsExtension",
                                                "options": {
                                                    "parameters": {
                                                        "id": "${app_insights_id}",
                                                        "menuid": "failures"
                                                    }
                                                },
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "failures"
                                                }
                                            },
                                            "openBlade": true
                                        },
                                        "title": "Failed requests",
                                        "titleKind": 2,
                                        "visualization": {
                                            "chartType": 3,
                                            "disablePinning": true
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
                                    "IsAzureFirst": false,
                                    "LinkedApplicationType": 0,
                                    "Name": "${app_insights_name}",
                                    "ResourceGroup": "${resource_group_name}",
                                    "ResourceId": "${app_insights_id}",
                                    "ResourceType": "microsoft.insights/components",
                                    "SubscriptionId": "${subscription_id}"
                                }
                            },
                            {
                                "name": "TargetBlade",
                                "value": "Failures"
                            },
                            {
                                "name": "DataModel",
                                "value": {
                                    "clientTypeMode": "Server",
                                    "experience": 3,
                                    "filters": [],
                                    "grain": "5m",
                                    "prefix": "${failures_query_prefix}",
                                    "selectedOperation": null,
                                    "selectedOperationName": null,
                                    "timeContext": {
                                        "createdTime": "2021-04-06T21:56:10.624Z",
                                        "durationMs": 86400000,
                                        "grain": 1,
                                        "isInitialTime": false,
                                        "useDashboardTimeRange": false
                                    },
                                    "version": "1.0.0"
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
                                "content": "### Usage",
                                "title": "",
                                "subtitle": "",
                                "markdownSource": 1,
                                "markdownUri": {}
                            }
                        }
                    }
                },
                "9": {
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
                                                "aggregationType": 5,
                                                "metricVisualization": {
                                                    "color": "#47BDF5",
                                                    "displayName": "Sessions"
                                                },
                                                "name": "sessions/count",
                                                "namespace": "microsoft.insights/components/kusto",
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                }
                                            },
                                            {
                                                "aggregationType": 5,
                                                "metricVisualization": {
                                                    "color": "#7E58FF",
                                                    "displayName": "Users"
                                                },
                                                "name": "users/count",
                                                "namespace": "microsoft.insights/components/kusto",
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                }
                                            }
                                        ],
                                        "openBladeOnClick": {
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "extensionName": "HubsExtension",
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "segmentationUsers"
                                                }
                                            },
                                            "openBlade": true
                                        },
                                        "title": "Unique sessions and users",
                                        "visualization": {
                                            "axisVisualization": {
                                                "x": {
                                                    "axisType": 2,
                                                    "isVisible": true
                                                },
                                                "y": {
                                                    "axisType": 1,
                                                    "isVisible": true
                                                }
                                            },
                                            "chartType": 2,
                                            "legendVisualization": {
                                                "hideSubtitle": false,
                                                "isVisible": true,
                                                "position": 2
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
                                                "aggregationType": 5,
                                                "metricVisualization": {
                                                    "color": "#47BDF5",
                                                    "displayName": "Sessions"
                                                },
                                                "name": "sessions/count",
                                                "namespace": "microsoft.insights/components/kusto",
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                }
                                            },
                                            {
                                                "aggregationType": 5,
                                                "metricVisualization": {
                                                    "color": "#7E58FF",
                                                    "displayName": "Users"
                                                },
                                                "name": "users/count",
                                                "namespace": "microsoft.insights/components/kusto",
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                }
                                            }
                                        ],
                                        "openBladeOnClick": {
                                            "destinationBlade": {
                                                "bladeName": "ResourceMenuBlade",
                                                "extensionName": "HubsExtension",
                                                "parameters": {
                                                    "id": "${app_insights_id}",
                                                    "menuid": "segmentationUsers"
                                                }
                                            },
                                            "openBlade": true
                                        },
                                        "title": "Unique sessions and users",
                                        "visualization": {
                                            "axisVisualization": {
                                                "x": {
                                                    "axisType": 2,
                                                    "isVisible": true
                                                },
                                                "y": {
                                                    "axisType": 1,
                                                    "isVisible": true
                                                }
                                            },
                                            "chartType": 2,
                                            "disablePinning": true,
                                            "legendVisualization": {
                                                "hideSubtitle": false,
                                                "isVisible": true,
                                                "position": 2
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                "10": {
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
                                                "aggregationType": 7,
                                                "metricVisualization": {
                                                    "displayName": "Page views"
                                                },
                                                "name": "pageViews/count",
                                                "namespace": "microsoft.insights/components",
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                }
                                            }
                                        ],
                                        "timespan": {
                                            "grain": 1,
                                            "relative": {
                                                "duration": 86400000
                                            },
                                            "showUTCTime": false
                                        },
                                        "title": "Count Page views for ${app_insights_name}",
                                        "titleKind": 1,
                                        "visualization": {
                                            "axisVisualization": {
                                                "x": {
                                                    "axisType": 2,
                                                    "isVisible": true
                                                },
                                                "y": {
                                                    "axisType": 1,
                                                    "isVisible": true
                                                }
                                            },
                                            "chartType": 2,
                                            "legendVisualization": {
                                                "hideSubtitle": false,
                                                "isVisible": true,
                                                "position": 2
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
                                                "aggregationType": 7,
                                                "metricVisualization": {
                                                    "displayName": "Page views"
                                                },
                                                "name": "pageViews/count",
                                                "namespace": "microsoft.insights/components",
                                                "resourceMetadata": {
                                                    "id": "${app_insights_id}"
                                                }
                                            }
                                        ],
                                        "title": "Count Page views for ${app_insights_name}",
                                        "titleKind": 1,
                                        "visualization": {
                                            "axisVisualization": {
                                                "x": {
                                                    "axisType": 2,
                                                    "isVisible": true
                                                },
                                                "y": {
                                                    "axisType": 1,
                                                    "isVisible": true
                                                }
                                            },
                                            "chartType": 2,
                                            "disablePinning": true,
                                            "legendVisualization": {
                                                "hideSubtitle": false,
                                                "isVisible": true,
                                                "position": 2
                                            }
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
                "11": {
                    "position": {
                        "x": 0,
                        "y": 11,
                        "colSpan": 3,
                        "rowSpan": 4
                    },
                    "metadata": {
                        "inputs": [],
                        "type": "Extension/HubsExtension/PartType/MarkdownPart",
                        "settings": {
                            "content": {
                                "content": "### Firewall",
                                "title": "",
                                "subtitle": "",
                                "markdownSource": 1,
                                "markdownUri": ""
                            }
                        }
                    }
                },
                "12": {
                    "position": {
                        "x": 3,
                        "y": 11,
                        "colSpan": 6,
                        "rowSpan": 4
                    },
                    "metadata": {
                        "inputs": [],
                        "type": "Extension/HubsExtension/PartType/MarkdownPart",
                        "settings": {
                            "content": {
                                "content": "",
                                "title": "Placeholder",
                                "subtitle": "",
                                "markdownSource": 1,
                                "markdownUri": ""
                            }
                        }
                    }
                },
                "13": {
                    "position": {
                        "x": 9,
                        "y": 11,
                        "colSpan": 6,
                        "rowSpan": 4
                    },
                    "metadata": {
                        "inputs": [],
                        "type": "Extension/HubsExtension/PartType/MarkdownPart",
                        "settings": {
                            "content": {
                                "content": "",
                                "title": "Placeholder 2",
                                "subtitle": "",
                                "markdownSource": 1,
                                "markdownUri": ""
                            }
                        }
                    }
                },
                "14": {
                    "position": {
                        "x": 0,
                        "y": 15,
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
                                    "markdownSource": 1,
                                    "markdownUri": null,
                                    "subtitle": "",
                                    "title": ""
                                }
                            }
                        }
                    }
                },
                "15": {
                    "position": {
                        "x": 3,
                        "y": 15,
                        "colSpan": 16,
                        "rowSpan": 8
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
                                    "createdTime": "Tue Apr 06 2021 14:56:16 GMT-0700 (Pacific Daylight Time)",
                                    "durationMs": 3600000,
                                    "grain": 1,
                                    "isInitialTime": false,
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
                                    "exclude4xxError": true,
                                    "layoutOption": "Organic",
                                    "timeContext": {
                                        "createdTime": "Tue Apr 06 2021 14:56:16 GMT-0700 (Pacific Daylight Time)",
                                        "durationMs": 3600000,
                                        "grain": 1,
                                        "isInitialTime": false,
                                        "useDashboardTimeRange": false
                                    }
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
                        "settings": {},
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
                            "StartboardPart-MonitorChartPart-601c0e92-821c-4e93-945d-13a0211991a8",
                            "StartboardPart-MonitorChartPart-601c0e92-821c-4e93-945d-13a0211991aa",
                            "StartboardPart-MonitorChartPart-601c0e92-821c-4e93-945d-13a0211991ac",
                            "StartboardPart-MonitorChartPart-601c0e92-821c-4e93-945d-13a0211991ae",
                            "StartboardPart-MonitorChartPart-601c0e92-821c-4e93-945d-13a0211991b2",
                            "StartboardPart-MonitorChartPart-601c0e92-821c-4e93-945d-13a0211991ba",
                            "StartboardPart-MonitorChartPart-601c0e92-821c-4e93-945d-13a0211991be",
                            "StartboardPart-ApplicationMapPart-601c0e92-821c-4e93-945d-13a0211991c2"
                        ]
                    }
                }
            }
        }
    }
}