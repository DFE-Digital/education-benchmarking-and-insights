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
                        "rowSpan": 4
                    },
                    "metadata": {
                        "inputs": [],
                        "type": "Extension/HubsExtension/PartType/MarkdownPart",
                        "settings": {
                            "content": {
                                "content": "",
                                "title": "Users and Sessions",
                                "subtitle": "${environment}",
                                "markdownSource": 1,
                                "markdownUri": ""
                            }
                        }
                    }
                },
                "1": {
                    "position": {
                        "x": 3,
                        "y": 0,
                        "colSpan": 4,
                        "rowSpan": 4
                    },
                    "metadata": {
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
                            },
                            {
                                "name": "resourceTypeMode",
                                "isOptional": true
                            },
                            {
                                "name": "ComponentId",
                                "isOptional": true
                            },
                            {
                                "name": "TimeRange",
                                "isOptional": true
                            },
                            {
                                "name": "DashboardId",
                                "isOptional": true
                            },
                            {
                                "name": "DraftRequestParameters",
                                "isOptional": true
                            },
                            {
                                "name": "SpecificChart",
                                "isOptional": true
                            },
                            {
                                "name": "PartSubTitle",
                                "isOptional": true
                            },
                            {
                                "name": "Dimensions",
                                "isOptional": true
                            },
                            {
                                "name": "LegendOptions",
                                "isOptional": true
                            },
                            {
                                "name": "IsQueryContainTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "settings": {},
                        "partHeader": {
                            "title": "${user_counts_table_title}",
                            "subtitle": "${environment}"
                        }
                    }
                },
                "2": {
                    "position": {
                        "x": 7,
                        "y": 0,
                        "colSpan": 7,
                        "rowSpan": 4
                    },
                    "metadata": {
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
                            },
                            {
                                "name": "resourceTypeMode",
                                "isOptional": true
                            },
                            {
                                "name": "ComponentId",
                                "isOptional": true
                            },
                            {
                                "name": "TimeRange",
                                "isOptional": true
                            },
                            {
                                "name": "DashboardId",
                                "isOptional": true
                            },
                            {
                                "name": "DraftRequestParameters",
                                "isOptional": true
                            },
                            {
                                "name": "SpecificChart",
                                "isOptional": true
                            },
                            {
                                "name": "PartSubTitle",
                                "isOptional": true
                            },
                            {
                                "name": "Dimensions",
                                "isOptional": true
                            },
                            {
                                "name": "LegendOptions",
                                "isOptional": true
                            },
                            {
                                "name": "IsQueryContainTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "settings": {
                            "content": {
                                "GridColumnsWidth": {
                                    "Count": "90px",
                                    "Date": "122px",
                                    "Average length in seconds": "234px"
                                }
                            }
                        },
                        "partHeader": {
                            "title": "${user_sessions_table_title}",
                            "subtitle": "${environment}"
                        }
                    }
                },
                "3": {
                    "position": {
                        "x": 14,
                        "y": 0,
                        "colSpan": 7,
                        "rowSpan": 4
                    },
                    "metadata": {
                        "inputs": [
                            {
                                "name": "resourceTypeMode",
                                "isOptional": true
                            },
                            {
                                "name": "ComponentId",
                                "isOptional": true
                            },
                            {
                                "name": "Scope",
                                "value": {
                                    "resourceIds": [
                                        "${app_insights_id}"
                                    ]
                                },
                                "isOptional": true
                            },
                            {
                                "name": "PartId",
                                "value": "${weekly_active_users_id}",
                                "isOptional": true
                            },
                            {
                                "name": "Version",
                                "value": "2.0",
                                "isOptional": true
                            },
                            {
                                "name": "TimeRange",
                                "isOptional": true
                            },
                            {
                                "name": "DashboardId",
                                "isOptional": true
                            },
                            {
                                "name": "DraftRequestParameters",
                                "isOptional": true
                            },
                            {
                                "name": "Query",
                                "value": "${weekly_active_users_query}",
                                "isOptional": true
                            },
                            {
                                "name": "ControlType",
                                "value": "FrameControlChart",
                                "isOptional": true
                            },
                            {
                                "name": "SpecificChart",
                                "value": "UnstackedArea",
                                "isOptional": true
                            },
                            {
                                "name": "PartTitle",
                                "value": "${weekly_active_users_title}",
                                "isOptional": true
                            },
                            {
                                "name": "PartSubTitle",
                                "isOptional": true
                            },
                            {
                                "name": "Dimensions",
                                "value": {
                                    "xAxis": {
                                        "name": "Day",
                                        "type": "datetime"
                                    },
                                    "yAxis": [
                                        {
                                            "name": "Active Users",
                                            "type": "real"
                                        },
                                        {
                                            "name": "Returning Users",
                                            "type": "real"
                                        },
                                        {
                                            "name": "Lost Users",
                                            "type": "real"
                                        },
                                        {
                                            "name": "New Users",
                                            "type": "real"
                                        }
                                    ],
                                    "splitBy": [],
                                    "aggregation": "Sum"
                                },
                                "isOptional": true
                            },
                            {
                                "name": "LegendOptions",
                                "value": {
                                    "isEnabled": true,
                                    "position": "Bottom"
                                },
                                "isOptional": true
                            },
                            {
                                "name": "IsQueryContainTimeRange",
                                "value": true,
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "settings": {},
                        "filters": {
                            "MsPortalFx_TimeRange": {
                                "model": {
                                    "format": "utc",
                                    "granularity": "auto",
                                    "relative": "30d"
                                }
                            }
                        },
                        "partHeader": {
                            "title": "Weekly active users over past 30 days",
                            "subtitle": "${environment}"
                        }
                    }
                },
                "4": {
                    "position": {
                        "x": 0,
                        "y": 4,
                        "colSpan": 3,
                        "rowSpan": 12
                    },
                    "metadata": {
                        "inputs": [],
                        "type": "Extension/HubsExtension/PartType/MarkdownPart",
                        "settings": {
                            "content": {
                                "content": "",
                                "title": "Features",
                                "subtitle": "${environment}",
                                "markdownSource": 1,
                                "markdownUri": ""
                            }
                        }
                    }
                },
                "5": {
                    "position": {
                        "x": 3,
                        "y": 4,
                        "colSpan": 9,
                        "rowSpan": 6
                    },
                    "metadata": {
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
                                "value": "${popular_school_requests_id}"
                            },
                            {
                                "name": "Version",
                                "value": "2.0"
                            },
                            {
                                "name": "Query",
                                "value": "${popular_school_requests_query}"
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
                                "value": "${popular_school_requests_title}"
                            },
                            {
                                "name": "Dimensions",
                                "value": {
                                    "aggregation": "Sum",
                                    "splitBy": [
                                        {
                                            "name": "Feature",
                                            "type": "string"
                                        }
                                    ],
                                    "xAxis": {
                                        "name": "Urn",
                                        "type": "string"
                                    },
                                    "yAxis": [
                                        {
                                            "name": "Count",
                                            "type": "long"
                                        }
                                    ]
                                }
                            },
                            {
                                "name": "LegendOptions",
                                "value": {
                                    "isEnabled": true,
                                    "position": "Bottom"
                                }
                            },
                            {
                                "name": "resourceTypeMode",
                                "isOptional": true
                            },
                            {
                                "name": "ComponentId",
                                "isOptional": true
                            },
                            {
                                "name": "TimeRange",
                                "isOptional": true
                            },
                            {
                                "name": "DashboardId",
                                "isOptional": true
                            },
                            {
                                "name": "DraftRequestParameters",
                                "isOptional": true
                            },
                            {
                                "name": "PartSubTitle",
                                "isOptional": true
                            },
                            {
                                "name": "IsQueryContainTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "settings": {},
                        "partHeader": {
                            "title": "${popular_school_requests_title}",
                            "subtitle": "${environment}"
                        }
                    }
                },
                "6": {
                    "position": {
                        "x": 12,
                        "y": 4,
                        "colSpan": 9,
                        "rowSpan": 6
                    },
                    "metadata": {
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
                                "value": "${popular_trust_requests_id}"
                            },
                            {
                                "name": "Version",
                                "value": "2.0"
                            },
                            {
                                "name": "Query",
                                "value": "${popular_trust_requests_query}"
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
                                "value": "${popular_trust_requests_title}"
                            },
                            {
                                "name": "Dimensions",
                                "value": {
                                    "aggregation": "Sum",
                                    "splitBy": [
                                        {
                                            "name": "Feature",
                                            "type": "string"
                                        }
                                    ],
                                    "xAxis": {
                                        "name": "CompanyNumber",
                                        "type": "string"
                                    },
                                    "yAxis": [
                                        {
                                            "name": "Count",
                                            "type": "long"
                                        }
                                    ]
                                }
                            },
                            {
                                "name": "LegendOptions",
                                "value": {
                                    "isEnabled": true,
                                    "position": "Bottom"
                                }
                            },
                            {
                                "name": "resourceTypeMode",
                                "isOptional": true
                            },
                            {
                                "name": "ComponentId",
                                "isOptional": true
                            },
                            {
                                "name": "TimeRange",
                                "isOptional": true
                            },
                            {
                                "name": "DashboardId",
                                "isOptional": true
                            },
                            {
                                "name": "DraftRequestParameters",
                                "isOptional": true
                            },
                            {
                                "name": "PartSubTitle",
                                "isOptional": true
                            },
                            {
                                "name": "IsQueryContainTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "settings": {},
                        "partHeader": {
                            "title": "${popular_trust_requests_title}",
                            "subtitle": "${environment}"
                        }
                    }
                },
                "7": {
                    "position": {
                        "x": 3,
                        "y": 10,
                        "colSpan": 9,
                        "rowSpan": 6
                    },
                    "metadata": {
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
                                "value": "${popular_local_authority_requests_id}"
                            },
                            {
                                "name": "Version",
                                "value": "2.0"
                            },
                            {
                                "name": "Query",
                                "value": "${popular_local_authority_requests_query}"
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
                                "value": "${popular_local_authority_requests_title}"
                            },
                            {
                                "name": "Dimensions",
                                "value": {
                                    "aggregation": "Sum",
                                    "splitBy": [
                                        {
                                            "name": "Feature",
                                            "type": "string"
                                        }
                                    ],
                                    "xAxis": {
                                        "name": "Code",
                                        "type": "string"
                                    },
                                    "yAxis": [
                                        {
                                            "name": "Count",
                                            "type": "long"
                                        }
                                    ]
                                }
                            },
                            {
                                "name": "LegendOptions",
                                "value": {
                                    "isEnabled": true,
                                    "position": "Bottom"
                                }
                            },
                            {
                                "name": "resourceTypeMode",
                                "isOptional": true
                            },
                            {
                                "name": "ComponentId",
                                "isOptional": true
                            },
                            {
                                "name": "TimeRange",
                                "isOptional": true
                            },
                            {
                                "name": "DashboardId",
                                "isOptional": true
                            },
                            {
                                "name": "DraftRequestParameters",
                                "isOptional": true
                            },
                            {
                                "name": "PartSubTitle",
                                "isOptional": true
                            },
                            {
                                "name": "IsQueryContainTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "settings": {},
                        "partHeader": {
                            "title": "${popular_local_authority_requests_title}",
                            "subtitle": "${environment}"
                        }
                    }
                },
                "8": {
                    "position": {
                        "x": 12,
                        "y": 10,
                        "colSpan": 9,
                        "rowSpan": 6
                    },
                    "metadata": {
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
                                "value": "${feature_requests_id}"
                            },
                            {
                                "name": "Version",
                                "value": "2.0"
                            },
                            {
                                "name": "Query",
                                "value": "${feature_requests_query}"
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
                                "value": "${feature_requests_title}"
                            },
                            {
                                "name": "Dimensions",
                                "value": {
                                    "aggregation": "Sum",
                                    "splitBy": [
                                        {
                                            "name": "Feature",
                                            "type": "string"
                                        }
                                    ],
                                    "xAxis": {
                                        "name": "Establishment",
                                        "type": "string"
                                    },
                                    "yAxis": [
                                        {
                                            "name": "Count",
                                            "type": "long"
                                        }
                                    ]
                                }
                            },
                            {
                                "name": "LegendOptions",
                                "value": {
                                    "isEnabled": true,
                                    "position": "Bottom"
                                }
                            },
                            {
                                "name": "resourceTypeMode",
                                "isOptional": true
                            },
                            {
                                "name": "ComponentId",
                                "isOptional": true
                            },
                            {
                                "name": "TimeRange",
                                "isOptional": true
                            },
                            {
                                "name": "DashboardId",
                                "isOptional": true
                            },
                            {
                                "name": "DraftRequestParameters",
                                "isOptional": true
                            },
                            {
                                "name": "PartSubTitle",
                                "isOptional": true
                            },
                            {
                                "name": "IsQueryContainTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "settings": {},
                        "partHeader": {
                            "title": "${feature_requests_title}",
                            "subtitle": "${environment}"
                        }
                    }
                },
                "9": {
                    "position": {
                        "x": 0,
                        "y": 16,
                        "colSpan": 3,
                        "rowSpan": 3
                    },
                    "metadata": {
                        "inputs": [],
                        "type": "Extension/HubsExtension/PartType/MarkdownPart",
                        "settings": {
                            "content": {
                                "content": "\n",
                                "title": "Journeys",
                                "subtitle": "${environment}",
                                "markdownSource": 1,
                                "markdownUri": ""
                            }
                        }
                    }
                },
                "10": {
                    "position": {
                        "x": 3,
                        "y": 16,
                        "colSpan": 9,
                        "rowSpan": 3
                    },
                    "metadata": {
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
                            },
                            {
                                "name": "resourceTypeMode",
                                "isOptional": true
                            },
                            {
                                "name": "ComponentId",
                                "isOptional": true
                            },
                            {
                                "name": "TimeRange",
                                "isOptional": true
                            },
                            {
                                "name": "DashboardId",
                                "isOptional": true
                            },
                            {
                                "name": "DraftRequestParameters",
                                "isOptional": true
                            },
                            {
                                "name": "SpecificChart",
                                "isOptional": true
                            },
                            {
                                "name": "PartSubTitle",
                                "isOptional": true
                            },
                            {
                                "name": "Dimensions",
                                "isOptional": true
                            },
                            {
                                "name": "LegendOptions",
                                "isOptional": true
                            },
                            {
                                "name": "IsQueryContainTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "settings": {},
                        "partHeader": {
                            "title": "${custom_data_funnel_title}",
                            "subtitle": "${environment}"
                        }
                    }
                },
                "11": {
                    "position": {
                        "x": 0,
                        "y": 19,
                        "colSpan": 3,
                        "rowSpan": 3
                    },
                    "metadata": {
                        "inputs": [],
                        "type": "Extension/HubsExtension/PartType/MarkdownPart",
                        "settings": {
                            "content": {
                                "content": "",
                                "title": "External links",
                                "subtitle": "${environment}",
                                "markdownSource": 1,
                                "markdownUri": ""
                            }
                        }
                    }
                },
                "12": {
                    "position": {
                        "x": 3,
                        "y": 19,
                        "colSpan": 9,
                        "rowSpan": 3
                    },
                    "metadata": {
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
                            },
                            {
                                "name": "resourceTypeMode",
                                "isOptional": true
                            },
                            {
                                "name": "ComponentId",
                                "isOptional": true
                            },
                            {
                                "name": "TimeRange",
                                "isOptional": true
                            },
                            {
                                "name": "DashboardId",
                                "isOptional": true
                            },
                            {
                                "name": "DraftRequestParameters",
                                "isOptional": true
                            },
                            {
                                "name": "SpecificChart",
                                "isOptional": true
                            },
                            {
                                "name": "PartSubTitle",
                                "isOptional": true
                            },
                            {
                                "name": "Dimensions",
                                "isOptional": true
                            },
                            {
                                "name": "LegendOptions",
                                "isOptional": true
                            },
                            {
                                "name": "IsQueryContainTimeRange",
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "settings": {
                            "content": {
                                "GridColumnsWidth": {
                                    "Url": "558px"
                                }
                            }
                        },
                        "partHeader": {
                            "title": "${popular_commercial_resources_title}",
                            "subtitle": "${environment}"
                        }
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
                            "relative": "7d"
                        },
                        "displayCache": {
                            "name": "UTC Time",
                            "value": "Past 7 days"
                        },
                        "filteredPartIds": [
                            "StartboardPart-LogsDashboardPart-fe9e8ce7-58e9-4bcc-aca2-31fdb261b826",
                            "StartboardPart-LogsDashboardPart-fe9e8ce7-58e9-4bcc-aca2-31fdb261b828",
                            "StartboardPart-LogsDashboardPart-fe9e8ce7-58e9-4bcc-aca2-31fdb261b82c",
                            "StartboardPart-LogsDashboardPart-fe9e8ce7-58e9-4bcc-aca2-31fdb261b82e",
                            "StartboardPart-LogsDashboardPart-fe9e8ce7-58e9-4bcc-aca2-31fdb261b830",
                            "StartboardPart-LogsDashboardPart-fe9e8ce7-58e9-4bcc-aca2-31fdb261b832",
                            "StartboardPart-LogsDashboardPart-fe9e8ce7-58e9-4bcc-aca2-31fdb261b836",
                            "StartboardPart-LogsDashboardPart-fe9e8ce7-58e9-4bcc-aca2-31fdb261b83a",
                            "StartboardPart-LogsDashboardPart-fe9e8ce7-58e9-4bcc-aca2-31fdb261b83c"
                        ]
                    }
                }
            }
        }
    }
}