resource "random_uuid" "popular-school-requests-chart-id" {}
resource "random_uuid" "popular-trust-requests-chart-id" {}
resource "random_uuid" "popular-local-authority-requests-chart-id" {}

resource "azapi_resource" "mi-dashboard" {
  type                      = "Microsoft.Portal/dashboards@2020-09-01-preview"
  name                      = "${var.environment-prefix}-ebis-dashboard-mi"
  location                  = azurerm_resource_group.resource-group.location
  parent_id                 = azurerm_resource_group.resource-group.id
  schema_validation_enabled = false
  tags = merge(
    local.common-tags,
    {
      "hidden-title" = "Management Information - ${var.environment-prefix}-ebis"
  })

  body = jsonencode({
    properties = {
      lenses = [
        {
          metadata = {}
          order    = 0
          parts = [
            {
              position = {
                colSpan = 10
                rowSpan = 6
                x       = 0
                y       = 0
              }
              metadata = {
                type = "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart"
                inputs = [
                  {
                    name = "Scope"
                    value = {
                      resourceIds = [
                        azurerm_log_analytics_workspace.application-insights-workspace.id
                      ]
                    }
                  },
                  {
                    name  = "PartId"
                    value = random_uuid.popular-school-requests-chart-id.result
                  },
                  {
                    name  = "Version"
                    value = "2.0"
                  },
                  {
                    name  = "TimeRange"
                    value = "P1D"
                  },
                  {
                    name  = "Query"
                    value = file("${path.module}/queries/popular-school-requests-chart.kql")
                  },
                  {
                    name  = "ControlType"
                    value = "FrameControlChart"
                  },
                  {
                    name  = "SpecificChart"
                    value = "StackedColumn"
                  },
                  {
                    name  = "PartTitle"
                    value = azurerm_log_analytics_query_pack_query.popular-school-requests-chart.description
                  },
                  {
                    name : "Dimensions",
                    value : {
                      xAxis = {
                        name = "Urn",
                        type = "string"
                      }
                      yAxis = [
                        {
                          name = "Count"
                          type = "long"
                        }
                      ]
                      splitBy = [
                        {
                          name = "Feature"
                          type = "string"
                        }
                      ]
                      aggregation = "Sum"
                    }
                  },
                  {
                    name = "LegendOptions"
                    value = {
                      isEnabled = true
                      position  = "Bottom"
                    }
                  },
                  {
                    name  = "IsQueryContainTimeRange"
                    value = false
                  }
                ]
              }
            },
            {
              position = {
                colSpan = 10
                rowSpan = 6
                x       = 0
                y       = 6
              }
              metadata = {
                type = "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart"
                inputs = [
                  {
                    name = "Scope"
                    value = {
                      resourceIds = [
                        azurerm_log_analytics_workspace.application-insights-workspace.id
                      ]
                    }
                  },
                  {
                    name  = "PartId"
                    value = random_uuid.popular-trust-requests-chart-id.result
                  },
                  {
                    name  = "Version"
                    value = "2.0"
                  },
                  {
                    name  = "TimeRange"
                    value = "P1D"
                  },
                  {
                    name  = "Query"
                    value = file("${path.module}/queries/popular-trust-requests-chart.kql")
                  },
                  {
                    name  = "ControlType"
                    value = "FrameControlChart"
                  },
                  {
                    name  = "SpecificChart"
                    value = "StackedColumn"
                  },
                  {
                    name  = "PartTitle"
                    value = azurerm_log_analytics_query_pack_query.popular-trust-requests-chart.description
                  },
                  {
                    name : "Dimensions",
                    value : {
                      xAxis = {
                        name = "CompanyNumber",
                        type = "string"
                      }
                      yAxis = [
                        {
                          name = "Count"
                          type = "long"
                        }
                      ]
                      splitBy = [
                        {
                          name = "Feature"
                          type = "string"
                        }
                      ]
                      aggregation = "Sum"
                    }
                  },
                  {
                    name = "LegendOptions"
                    value = {
                      isEnabled = true
                      position  = "Bottom"
                    }
                  },
                  {
                    name  = "IsQueryContainTimeRange"
                    value = false
                  }
                ]
              }
            },
            {
              position = {
                colSpan = 10
                rowSpan = 6
                x       = 0
                y       = 12
              }
              metadata = {
                type = "Extension/Microsoft_OperationsManagementSuite_Workspace/PartType/LogsDashboardPart"
                inputs = [
                  {
                    name = "Scope"
                    value = {
                      resourceIds = [
                        azurerm_log_analytics_workspace.application-insights-workspace.id
                      ]
                    }
                  },
                  {
                    name  = "PartId"
                    value = random_uuid.popular-local-authority-requests-chart-id.result
                  },
                  {
                    name  = "Version"
                    value = "2.0"
                  },
                  {
                    name  = "TimeRange"
                    value = "P1D"
                  },
                  {
                    name  = "Query"
                    value = file("${path.module}/queries/popular-local-authority-requests-chart.kql")
                  },
                  {
                    name  = "ControlType"
                    value = "FrameControlChart"
                  },
                  {
                    name  = "SpecificChart"
                    value = "StackedColumn"
                  },
                  {
                    name  = "PartTitle"
                    value = azurerm_log_analytics_query_pack_query.popular-local-authority-requests-chart.description
                  },
                  {
                    name : "Dimensions",
                    value : {
                      xAxis = {
                        name = "Code",
                        type = "string"
                      }
                      yAxis = [
                        {
                          name = "Count"
                          type = "long"
                        }
                      ]
                      splitBy = [
                        {
                          name = "Feature"
                          type = "string"
                        }
                      ]
                      aggregation = "Sum"
                    }
                  },
                  {
                    name = "LegendOptions"
                    value = {
                      isEnabled = true
                      position  = "Bottom"
                    }
                  },
                  {
                    name  = "IsQueryContainTimeRange"
                    value = false
                  }
                ]
              }
            }
          ]
        }
      ]
      metadata = {}
    }
  })
}