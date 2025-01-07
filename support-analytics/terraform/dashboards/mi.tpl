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
                        "rowSpan": 5
                    },
                    "metadata": {
                        "inputs": [],
                        "type": "Extension/HubsExtension/PartType/MarkdownPart",
                        "settings": {
                            "content": {
                                "content": "### Users and Sessions\r\n\r\nUser and Session metrics are only gathered for those who have accepted optional analytics cookies. \r\n\r\nThe `Anonymous Requests` metric represents page requests by those users who did not accept the analytics cookies. \r\n\r\n`Authenticated Users` are only correlated with a User/Session if the user accepted analytics cookies.",
                                "markdownSource": 1,
                                "markdownUri": "",
                                "subtitle": "",
                                "title": ""
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
                        "rowSpan": 5
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
                                    "Average length in seconds": "234px",
                                    "Count": "90px",
                                    "Date": "122px"
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
                        "rowSpan": 5
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
                                    "aggregation": "Sum",
                                    "splitBy": [],
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
                                    ]
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
                        "x": 3,
                        "y": 3,
                        "colSpan": 4,
                        "rowSpan": 2
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
                                "value": "${request_counts_table_id}"
                            },
                            {
                                "name": "Version",
                                "value": "2.0"
                            },
                            {
                                "name": "Query",
                                "value": "${request_counts_table_query}"
                            },
                            {
                                "name": "ControlType",
                                "value": "AnalyticsGrid"
                            },
                            {
                                "name": "PartTitle",
                                "value": "${request_counts_table_title}"
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
                            "title": "${request_counts_table_title}",
                            "subtitle": "${environment}"
                        }
                    }
                },
                "5": {
                    "position": {
                        "x": 0,
                        "y": 5,
                        "colSpan": 3,
                        "rowSpan": 18
                    },
                    "metadata": {
                        "inputs": [],
                        "type": "Extension/HubsExtension/PartType/MarkdownPart",
                        "settings": {
                            "content": {
                                "content": "### Features",
                                "markdownSource": 1,
                                "markdownUri": "",
                                "subtitle": "",
                                "title": ""
                            }
                        }
                    }
                },
                "6": {
                    "position": {
                        "x": 3,
                        "y": 5,
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
                "7": {
                    "position": {
                        "x": 12,
                        "y": 5,
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
                                },
                                "isOptional": true
                            },
                            {
                                "name": "PartId",
                                "value": "${most_popular_recent_schools_id}",
                                "isOptional": true
                            },
                            {
                                "name": "Version",
                                "value": "2.0",
                                "isOptional": true
                            },
                            {
                                "name": "Query",
                                "value": "${most_popular_recent_schools_query}",
                                "isOptional": true
                            },
                            {
                                "name": "ControlType",
                                "value": "FrameControlChart",
                                "isOptional": true
                            },
                            {
                                "name": "SpecificChart",
                                "value": "StackedColumn",
                                "isOptional": true
                            },
                            {
                                "name": "PartTitle",
                                "value": "${most_popular_recent_schools_title}"
                            },
                            {
                                "name": "PartSubTitle",
                                "isOptional": true
                            },
                            {
                                "name": "Dimensions",
                                "value": {
                                    "aggregation": "Sum",
                                    "splitBy": [],
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
                                },
                                "isOptional": true
                            },
                            {
                                "name": "LegendOptions",
                                "value": {
                                    "isEnabled": false
                                },
                                "isOptional": true
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
                                "value": true,
                                "isOptional": true
                            }
                        ],
                        "type": "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart",
                        "settings": {}
                    }
                },
                "8": {
                    "position": {
                        "x": 3,
                        "y": 11,
                        "colSpan": 9,
                        "rowSpan": 6
                    },
                    "metadata": {
                        "inputs": [
                            {
                                "name": "Scope",
                                "value": {
                                    "resourceIds": [
                                        "/subscriptions/f155d7d4-a235-4563-b077-c19686c7c92a/resourceGroups/s198d11-ebis-core/providers/Microsoft.OperationalInsights/workspaces/s198d11-ebis-aiw"
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
                "9": {
                    "position": {
                        "x": 12,
                        "y": 11,
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
                "10": {
                    "position": {
                        "x": 3,
                        "y": 17,
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
                "11": {
                    "position": {
                        "x": 0,
                        "y": 23,
                        "colSpan": 3,
                        "rowSpan": 3
                    },
                    "metadata": {
                        "inputs": [],
                        "type": "Extension/HubsExtension/PartType/MarkdownPart",
                        "settings": {
                            "content": {
                                "content": "### Journeys",
                                "markdownSource": 1,
                                "markdownUri": "",
                                "subtitle": "",
                                "title": ""
                            }
                        }
                    }
                },
                "12": {
                    "position": {
                        "x": 3,
                        "y": 23,
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
                "13": {
                    "position": {
                        "x": 0,
                        "y": 26,
                        "colSpan": 3,
                        "rowSpan": 3
                    },
                    "metadata": {
                        "inputs": [],
                        "type": "Extension/HubsExtension/PartType/MarkdownPart",
                        "settings": {
                            "content": {
                                "content": "### External links",
                                "markdownSource": 1,
                                "markdownUri": "",
                                "subtitle": "",
                                "title": ""
                            }
                        }
                    }
                },
                "14": {
                    "position": {
                        "x": 3,
                        "y": 26,
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
                            "StartboardPart-LogsDashboardPart-0a88422f-f530-4210-886d-6bcd743ac011",
                            "StartboardPart-LogsDashboardPart-0a88422f-f530-4210-886d-6bcd743ac01d",
                            "StartboardPart-LogsDashboardPart-0a88422f-f530-4210-886d-6bcd743ac01f",
                            "StartboardPart-LogsDashboardPart-0a88422f-f530-4210-886d-6bcd743ac021",
                            "StartboardPart-LogsDashboardPart-0a88422f-f530-4210-886d-6bcd743ac025",
                            "StartboardPart-LogsDashboardPart-0a88422f-f530-4210-886d-6bcd743ac027",
                            "StartboardPart-LogsDashboardPart-0a88422f-f530-4210-886d-6bcd743ac02b",
                            "StartboardPart-LogsDashboardPart-0a88422f-f530-4210-886d-6bcd743ac013",
                            "StartboardPart-LogsDashboardPart-0a88422f-f530-4210-886d-6bcd743ac017",
                            "StartboardPart-LogsDashboardPart-0a88422f-f530-4210-886d-6bcd743ac01b",
                            "StartboardPart-LogsDashboardPart-0a88422f-f530-4210-886d-6bcd743ac029"
                        ]
                    }
                }
            }
        }
    }
}