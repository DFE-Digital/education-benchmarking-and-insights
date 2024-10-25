resource "azurerm_logic_app_workflow" "alert-teams" {
  name                = "${var.environment-prefix}-ebis-alert-teams"
  location            = azurerm_resource_group.resource-group.location
  resource_group_name = azurerm_resource_group.resource-group.name
  tags                = local.common-tags

  identity {
    type = "SystemAssigned"
  }

  workflow_parameters = {
    Environment = jsonencode({
      type         = "String"
      defaultValue = ""
      description  = "The environment from which the alert has been triggered"
    })
    TeamId = jsonencode({
      type         = "String"
      defaultValue = ""
      description  = "The GUID corresponding to the Team to post the alert to in Teams"
    })
    ChannelId = jsonencode({
      type         = "String"
      defaultValue = ""
      description  = "The GUID corresponding to the Channel to post the alert to in Teams"
    })
    "$connections" = jsonencode({
      type         = "Object"
      defaultValue = {}
    })
  }

  parameters = {
    Environment = "${var.environment-prefix}"
    TeamId      = "${var.teams-team-id}"
    ChannelId   = "${var.teams-channel-id}"

    "$connections" = jsonencode({
      teams = {
        id             = data.azurerm_managed_api.teams-api.id
        connectionId   = azurerm_api_connection.teams-api-connection.id
        connectionName = "teams"
      }
    })
  }
}

# https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-logic-apps?tabs=send-teams-message
resource "azurerm_logic_app_trigger_http_request" "http-trigger" {
  name         = "When_an_alert_HTTP_request_is_received"
  logic_app_id = azurerm_logic_app_workflow.alert-teams.id

  schema = <<SCHEMA
{
    "type": "object",
    "properties": {
        "schemaId": {
            "type": "string"
        },
        "data": {
            "type": "object",
            "properties": {
                "essentials": {
                    "type": "object",
                    "properties": {
                        "alertId": {
                            "type": "string"
                        },
                        "alertRule": {
                            "type": "string"
                        },
                        "severity": {
                            "type": "string"
                        },
                        "signalType": {
                            "type": "string"
                        },
                        "monitorCondition": {
                            "type": "string"
                        },
                        "monitoringService": {
                            "type": "string"
                        },
                        "alertTargetIDs": {
                            "type": "array",
                            "items": {
                                "type": "string"
                            }
                        },
                        "originAlertId": {
                            "type": "string"
                        },
                        "firedDateTime": {
                            "type": "string"
                        },
                        "resolvedDateTime": {
                            "type": "string"
                        },
                        "description": {
                            "type": "string"
                        },
                        "essentialsVersion": {
                            "type": "string"
                        },
                        "alertContextVersion": {
                            "type": "string"
                        }
                    }
                },
                "alertContext": {
                    "type": "object",
                    "properties": {}
                }
            }
        }
    }
}
SCHEMA

}

resource "azurerm_logic_app_action_custom" "initialise-affected-resource-ids" {
  name         = "Initialize_variable"
  logic_app_id = azurerm_logic_app_workflow.alert-teams.id

  body = <<BODY
{
    "runAfter": {},
    "type": "InitializeVariable",
    "inputs": {
        "variables": [
            {
                "name": "AffectedResourceIds",
                "type": "array",
                "value": "@split(triggerBody()?['data']?['essentials']?['alertTargetIDs'][0], '/')"
            }
        ]
    }
}
BODY

}

# https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-common-schema#sample-alert-payload
resource "azurerm_logic_app_action_custom" "condition-post-teams" {
  name         = "Condition"
  logic_app_id = azurerm_logic_app_workflow.alert-teams.id

  depends_on = [
    azurerm_logic_app_action_custom.initialise-affected-resource-ids
  ]

  body = <<BODY
{
    "actions": {
        "Post_resolved_message_in_a_channel": {
            "type": "ApiConnection",
            "inputs": {
                "host": {
                    "connection": {
                        "name": "@parameters('$connections')['teams']['connectionId']"
                    }
                },
                "method": "post",
                "body": {
                    "recipient": {
                        "groupId": "@{parameters('TeamId')}",
                        "channelId": "@{parameters('ChannelId')}"
                    },
                    "messageBody": "<p class=\"editor-paragraph\">✅ Alert <strong>@{triggerBody()?['data']?['essentials']?['alertRule']}</strong> was <strong>@{triggerBody()?['data']?['essentials']?['monitorCondition']}</strong> at @{triggerBody()?['data']?['essentials']?['firedDateTime']} in <strong>@{parameters('Environment')}</strong></p><br><p class=\"editor-paragraph\"><a href=\"https://portal.azure.com/#blade/Microsoft_Azure_Monitoring_Alerts/AlertDetails.ReactView/alertId/%2fsubscriptions%2f@{variables('AffectedResourceIds')[2]}%2fresourceGroups%2f@{variables('AffectedResourceIds')[4]}%2fproviders%2fMicrosoft.AlertsManagement%2falerts%2f@{triggerBody()?['data']?['essentials']?['alertId']}\" class=\"editor-link\">View the alert in Azure Monitor</a></p>"
                },
                "path": "/beta/teams/conversation/message/poster/Flow bot/location/@{encodeURIComponent('Channel')}"
            }
        }
    },
    "runAfter": {
        "${azurerm_logic_app_action_custom.initialise-affected-resource-ids.name}": [
            "Succeeded"
        ]
    },
    "else": {
        "actions": {
            "Post_fired_message_in_a_channel": {
                "type": "ApiConnection",
                "inputs": {
                    "host": {
                        "connection": {
                            "name": "@parameters('$connections')['teams']['connectionId']"
                        }
                    },
                    "method": "post",
                    "body": {
                        "recipient": {
                            "groupId": "@{parameters('TeamId')}",
                            "channelId": "@{parameters('ChannelId')}"
                        },
                        "messageBody": "<p class=\"editor-paragraph\">🚨 Alert <strong>@{triggerBody()?['data']?['essentials']?['alertRule']}</strong> with severity <strong>@{triggerBody()?['data']?['essentials']?['severity']}</strong> was <strong>@{triggerBody()?['data']?['essentials']?['monitorCondition']}</strong> at @{triggerBody()?['data']?['essentials']?['firedDateTime']} in <strong>@{parameters('Environment')}</strong></p><br><p class=\"editor-paragraph\"><a href=\"https://portal.azure.com/#blade/Microsoft_Azure_Monitoring_Alerts/AlertDetails.ReactView/alertId/%2fsubscriptions%2f@{variables('AffectedResourceIds')[2]}%2fresourceGroups%2f@{variables('AffectedResourceIds')[4]}%2fproviders%2fMicrosoft.AlertsManagement%2falerts%2f@{triggerBody()?['data']?['essentials']?['alertId']}\" class=\"editor-link\">View the alert in Azure Monitor</a></p>"
                    },
                    "path": "/beta/teams/conversation/message/poster/Flow bot/location/@{encodeURIComponent('Channel')}"
                }
            }
        }
    },
    "expression": {
        "and": [
            {
                "equals": [
                    "@triggerBody()?['data']?['essentials']?['monitorCondition']",
                    "Resolved"
                ]
            }
        ]
    },
    "type": "If"
}

BODY

}

data "azurerm_managed_api" "teams-api" {
  name     = "teams"
  location = azurerm_resource_group.resource-group.location
}

# This will create the API connection, but not actually perform authorisation for the `teams`
# managed API. This must be handled as an interactive event via Azure Portal by selecting:
#
#  General > Edit API connection > Authorize
#
# and then logging in with an Office 365 account with access to the target Team and Channel.
resource "azurerm_api_connection" "teams-api-connection" {
  name                = "${var.environment-prefix}-teams-api"
  resource_group_name = azurerm_resource_group.resource-group.name
  managed_api_id      = data.azurerm_managed_api.teams-api.id
  display_name        = "Teams"
  tags                = local.common-tags

  parameter_values = {}

  lifecycle {
    # NOTE: since the connectionString is a secure value it's not returned from the API
    ignore_changes = [parameter_values]
  }
}
