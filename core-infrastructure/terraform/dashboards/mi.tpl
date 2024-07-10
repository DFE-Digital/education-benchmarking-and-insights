{
    "lenses": {
        "0": {
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
                                "filteredPartIds": []
                            }
                        }
                    }
                }
            },
            "order": 0,
            "parts": {
                "0": {
                    "position": {
                        "colSpan": 10,
                        "rowSpan": 6,
                        "x": 0,
                        "y": 0
                    },
                    "metadata": {
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "inputs": [
                            {
                                "name": "Scope",
                                "value": {
                                    "resourceIds": [
                                        "${workspace_id}"
                                    ]
                                }
                            },
                            {
                                "name": "PartId",
                                "value": "${popular_school_requests_chart_id}"
                            },
                            {
                                "name": "Version",
                                "value": "2.0"
                            },
                            {
                                "name": "Query",
                                "value": "${popular_school_requests_chart_query}"
                            },
                            {
                                "name": "ControlType",
                                "value": "FrameControlChart"
                            },
                            {
                                "name": "SpecificChart",
                                "value": "StackedColumn"
                            },
                            {
                                "name": "PartTitle",
                                "value": "${popular_school_requests_chart_title}"
                            },
                            {
                                "name": "Dimensions",
                                "value": {
                                    "xAxis": {
                                        "name": "Urn",
                                        "type": "string"
                                    },
                                    "yAxis": [
                                        {
                                            "name": "Count",
                                            "type": "long"
                                        }
                                    ],
                                    "splitBy": [
                                        {
                                            "name": "Feature",
                                            "type": "string"
                                        }
                                    ],
                                    "aggregation": "Sum"
                                }
                            },
                            {
                                "name": "LegendOptions",
                                "value": {
                                    "isEnabled": true,
                                    "position": "Bottom"
                                }
                            }
                        ]
                    }
                },
                "1": {
                    "position": {
                        "colSpan": 10,
                        "rowSpan": 6,
                        "x": 10,
                        "y": 0
                    },
                    "metadata": {
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "inputs": [
                            {
                                "name": "Scope",
                                "value": {
                                    "resourceIds": [
                                        "${workspace_id}"
                                    ]
                                }
                            },
                            {
                                "name": "PartId",
                                "value": "${popular_trust_requests_chart_id}"
                            },
                            {
                                "name": "Version",
                                "value": "2.0"
                            },
                            {
                                "name": "Query",
                                "value": "${popular_trust_requests_chart_query}"
                            },
                            {
                                "name": "ControlType",
                                "value": "FrameControlChart"
                            },
                            {
                                "name": "SpecificChart",
                                "value": "StackedColumn"
                            },
                            {
                                "name": "PartTitle",
                                "value": "${popular_trust_requests_chart_title}"
                            },
                            {
                                "name": "Dimensions",
                                "value": {
                                    "xAxis": {
                                        "name": "CompanyNumber",
                                        "type": "string"
                                    },
                                    "yAxis": [
                                        {
                                            "name": "Count",
                                            "type": "long"
                                        }
                                    ],
                                    "splitBy": [
                                        {
                                            "name": "Feature",
                                            "type": "string"
                                        }
                                    ],
                                    "aggregation": "Sum"
                                }
                            },
                            {
                                "name": "LegendOptions",
                                "value": {
                                    "isEnabled": true,
                                    "position": "Bottom"
                                }
                            }
                        ]
                    }
                },
                "2": {
                    "position": {
                        "colSpan": 10,
                        "rowSpan": 6,
                        "x": 0,
                        "y": 6
                    },
                    "metadata": {
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "inputs": [
                            {
                                "name": "Scope",
                                "value": {
                                    "resourceIds": [
                                        "${workspace_id}"
                                    ]
                                }
                            },
                            {
                                "name": "PartId",
                                "value": "${popular_local_authority_requests_chart_id}"
                            },
                            {
                                "name": "Version",
                                "value": "2.0"
                            },
                            {
                                "name": "Query",
                                "value": "${popular_local_authority_requests_chart_query}"
                            },
                            {
                                "name": "ControlType",
                                "value": "FrameControlChart"
                            },
                            {
                                "name": "SpecificChart",
                                "value": "StackedColumn"
                            },
                            {
                                "name": "PartTitle",
                                "value": "${popular_local_authority_requests_chart_title}"
                            },
                            {
                                "name": "Dimensions",
                                "value": {
                                    "xAxis": {
                                        "name": "Code",
                                        "type": "string"
                                    },
                                    "yAxis": [
                                        {
                                            "name": "Count",
                                            "type": "long"
                                        }
                                    ],
                                    "splitBy": [
                                        {
                                            "name": "Feature",
                                            "type": "string"
                                        }
                                    ],
                                    "aggregation": "Sum"
                                }
                            },
                            {
                                "name": "LegendOptions",
                                "value": {
                                    "isEnabled": true,
                                    "position": "Bottom"
                                }
                            }
                        ]
                    }
                },
                "3": {
                    "position": {
                        "colSpan": 10,
                        "rowSpan": 6,
                        "x": 10,
                        "y": 6
                    },
                    "metadata": {
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "inputs": [
                            {
                                "name": "Scope",
                                "value": {
                                    "resourceIds": [
                                        "${workspace_id}"
                                    ]
                                }
                            },
                            {
                                "name": "PartId",
                                "value": "${feature_requests_chart_id}"
                            },
                            {
                                "name": "Version",
                                "value": "2.0"
                            },
                            {
                                "name": "Query",
                                "value": "${feature_requests_chart_query}"
                            },
                            {
                                "name": "ControlType",
                                "value": "FrameControlChart"
                            },
                            {
                                "name": "SpecificChart",
                                "value": "StackedColumn"
                            },
                            {
                                "name": "PartTitle",
                                "value": "${feature_requests_chart_title}"
                            },
                            {
                                "name": "Dimensions",
                                "value": {
                                    "xAxis": {
                                        "name": "Establishment",
                                        "type": "string"
                                    },
                                    "yAxis": [
                                        {
                                            "name": "Count",
                                            "type": "long"
                                        }
                                    ],
                                    "splitBy": [
                                        {
                                            "name": "Feature",
                                            "type": "string"
                                        }
                                    ],
                                    "aggregation": "Sum"
                                }
                            },
                            {
                                "name": "LegendOptions",
                                "value": {
                                    "isEnabled": true,
                                    "position": "Bottom"
                                }
                            }
                        ]
                    }
                },
                "4": {
                    "position": {
                        "colSpan": 4,
                        "rowSpan": 3,
                        "x": 0,
                        "y": 12
                    },
                    "metadata": {
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "inputs": [
                            {
                                "name": "Scope",
                                "value": {
                                    "resourceIds": [
                                        "${workspace_id}"
                                    ]
                                }
                            },
                            {
                                "name": "PartId",
                                "value": "${user_counts_table_id}"
                            },
                            {
                                "name": "Version",
                                "value": "2.0"
                            },
                            {
                                "name": "Query",
                                "value": "${user_counts_table_query}"
                            },
                            {
                                "name": "ControlType",
                                "value": "AnalyticsGrid"
                            },
                            {
                                "name": "PartTitle",
                                "value": "${user_counts_table_title}"
                            }
                        ]
                    }
                },
                "5": {
                    "position": {
                        "colSpan": 6,
                        "rowSpan": 3,
                        "x": 4,
                        "y": 12
                    },
                    "metadata": {
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "inputs": [
                            {
                                "name": "Scope",
                                "value": {
                                    "resourceIds": [
                                        "${workspace_id}"
                                    ]
                                }
                            },
                            {
                                "name": "PartId",
                                "value": "${user_sessions_table_id}"
                            },
                            {
                                "name": "Version",
                                "value": "2.0"
                            },
                            {
                                "name": "Query",
                                "value": "${user_sessions_table_query}"
                            },
                            {
                                "name": "ControlType",
                                "value": "AnalyticsGrid"
                            },
                            {
                                "name": "PartTitle",
                                "value": "${user_sessions_table_title}"
                            }
                        ]
                    }
                },
                "6": {
                    "position": {
                        "colSpan": 10,
                        "rowSpan": 3,
                        "x": 10,
                        "y": 12
                    },
                    "metadata": {
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "inputs": [
                            {
                                "name": "Scope",
                                "value": {
                                    "resourceIds": [
                                        "${workspace_id}"
                                    ]
                                }
                            },
                            {
                                "name": "PartId",
                                "value": "${popular_commercial_resources_id}"
                            },
                            {
                                "name": "Version",
                                "value": "2.0"
                            },
                            {
                                "name": "Query",
                                "value": "${popular_commercial_resources_query}"
                            },
                            {
                                "name": "ControlType",
                                "value": "AnalyticsGrid"
                            },
                            {
                                "name": "PartTitle",
                                "value": "${popular_commercial_resources_title}"
                            }
                        ]
                    }
                },
                "7": {
                    "position": {
                        "colSpan": 6,
                        "rowSpan": 3,
                        "x": 0,
                        "y": 15
                    },
                    "metadata": {
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "inputs": [
                            {
                                "name": "Scope",
                                "value": {
                                    "resourceIds": [
                                        "${app_insights_id}"
                                    ]
                                }
                            },
                            {
                                "name": "PartId",
                                "value": "${custom_data_funnel_id}"
                            },
                            {
                                "name": "Version",
                                "value": "2.0"
                            },
                            {
                                "name": "Query",
                                "value": "${custom_data_funnel_query}"
                            },
                            {
                                "name": "ControlType",
                                "value": "AnalyticsGrid"
                            },
                            {
                                "name": "PartTitle",
                                "value": "${custom_data_funnel_title}"
                            }
                        ]
                    }
                }
            }
        }
    }
}