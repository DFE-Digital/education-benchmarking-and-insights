{
    "lenses": {
        "0": {
            "metadata": {},
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
                                "name": "TimeRange",
                                "value": "P1D"
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
                            },
                            {
                                "name": "IsQueryContainTimeRange",
                                "value": false
                            }
                        ]
                    }
                },
                "1": {
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
                                "value": "${popular_trust_requests_chart_id}"
                            },
                            {
                                "name": "Version",
                                "value": "2.0"
                            },
                            {
                                "name": "TimeRange",
                                "value": "P1D"
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
                            },
                            {
                                "name": "IsQueryContainTimeRange",
                                "value": false
                            }
                        ]
                    }
                },
                "2": {
                    "position": {
                        "colSpan": 10,
                        "rowSpan": 6,
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
                                "value": "${popular_local_authority_requests_chart_id}"
                            },
                            {
                                "name": "Version",
                                "value": "2.0"
                            },
                            {
                                "name": "TimeRange",
                                "value": "P1D"
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
                            },
                            {
                                "name": "IsQueryContainTimeRange",
                                "value": false
                            }
                        ]
                    }
                }
            }
        }
    }
}