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

# Import and escape adaptive cards before laoding into `messageBody` in the logic app action.
# For schema definition and examples see: https://adaptivecards.io/explorer/
# At time of writing, Teams supports up to and including v1.5 of the schema.
locals {
  alert-assigned-adaptive-card         = replace(replace(file("${path.module}/adaptive-cards/alert-assigned.json"), "\"", "\\\""), "\n", "\\n")
  alert-assigned-minimal-adaptive-card = replace(replace(file("${path.module}/adaptive-cards/alert-assigned-minimal.json"), "\"", "\\\""), "\n", "\\n")
  alert-fired-adaptive-card            = replace(replace(file("${path.module}/adaptive-cards/alert-fired.json"), "\"", "\\\""), "\n", "\\n")
  alert-fired-minimal-adaptive-card    = replace(replace(file("${path.module}/adaptive-cards/alert-fired-minimal.json"), "\"", "\\\""), "\n", "\\n")
  alert-resolved-adaptive-card         = replace(replace(file("${path.module}/adaptive-cards/alert-resolved.json"), "\"", "\\\""), "\n", "\\n")
  alert-resolved-minimal-adaptive-card = replace(replace(file("${path.module}/adaptive-cards/alert-resolved-minimal.json"), "\"", "\\\""), "\n", "\\n")
}

resource "azurerm_logic_app_action_custom" "initialise-affected-resource-ids" {
  name         = "Initialize_affected_resource_IDs"
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

resource "azurerm_logic_app_action_custom" "initialise-alert-id" {
  name         = "Initialize_alert_ID"
  logic_app_id = azurerm_logic_app_workflow.alert-teams.id

  depends_on = [
    azurerm_logic_app_action_custom.initialise-affected-resource-ids
  ]

  body = <<BODY
{
    "runAfter": {
        "${azurerm_logic_app_action_custom.initialise-affected-resource-ids.name}": [
            "Succeeded"
        ]
    },
    "type": "InitializeVariable",
    "inputs": {
        "variables": [
            {
                "name": "AlertId",
                "type": "string",
                "value": "@substring(triggerBody()?['data']?['essentials']?['alertId'], add(lastIndexOf(triggerBody()?['data']?['essentials']?['alertId'], '/'), 1))"
            }
        ]
    }
}
BODY

}

resource "azurerm_logic_app_action_custom" "initialise-message-id" {
  name         = "Initialize_message_ID"
  logic_app_id = azurerm_logic_app_workflow.alert-teams.id

  depends_on = [
    azurerm_logic_app_action_custom.initialise-alert-id
  ]

  body = <<BODY
{
    "runAfter": {
        "${azurerm_logic_app_action_custom.initialise-alert-id.name}": [
            "Succeeded"
        ]
    },
    "type": "InitializeVariable",
    "inputs": {
        "variables": [
            {
                "name": "MessageId",
                "type": "string"
            }
        ]
    }
}
BODY

}

resource "azurerm_logic_app_action_custom" "initialise-fired-timestamp" {
  name         = "Initialize_fired_timestamp"
  logic_app_id = azurerm_logic_app_workflow.alert-teams.id

  depends_on = [
    azurerm_logic_app_action_custom.initialise-message-id
  ]

  body = <<BODY
{
    "runAfter": {
        "${azurerm_logic_app_action_custom.initialise-message-id.name}": [
            "Succeeded"
        ]
    },
    "type": "InitializeVariable",
    "inputs": {
        "variables": [
            {
                "name": "FiredDateTime",
                "type": "string",
                "value": "@{formatDateTime(triggerBody()?['data']?['essentials']?['firedDateTime'], 'MM/dd/yyyy HH:mm:ss')}"
            }
        ]
    }
}
BODY

}

# https://learn.microsoft.com/en-us/azure/azure-monitor/alerts/alerts-common-schema#sample-alert-payload
resource "azurerm_logic_app_action_custom" "is-alert-resolved" {
  name         = "Is_alert_resolved"
  logic_app_id = azurerm_logic_app_workflow.alert-teams.id

  depends_on = [
    azurerm_logic_app_action_custom.initialise-fired-timestamp
  ]

  body = <<BODY
{
    "actions": {
        "For_each_message": {
            "foreach": "@body('Get_messages')?['value']",
            "actions": {
                "For_each_attachment": {
                    "foreach": "@items('For_each_message')?['attachments']",
                    "actions": {
                        "Message_content_contains_fired_timestamp": {
                            "actions": {
                                "Set_message_ID": {
                                    "type": "SetVariable",
                                    "inputs": {
                                        "name": "messageId",
                                        "value": "@items('For_each_message')?['id']"
                                    }
                                }
                            },
                            "else": {
                                "actions": {}
                            },
                            "expression": {
                                "and": [
                                    {
                                        "greater": [
                                            "@indexOf(items('For_each_attachment')?['content'], variables('FiredDateTime'))",
                                            0
                                        ]
                                    }
                                ]
                            },
                            "type": "If"
                        }
                    },
                    "type": "Foreach"
                }
            },
            "runAfter": {
                "Get_messages": [
                    "Succeeded"
                ]
            },
            "type": "Foreach"
        },
        "Get_messages": {
            "type": "ApiConnection",
            "inputs": {
                "host": {
                    "connection": {
                        "name": "@parameters('$connections')['teams']['connectionId']"
                    }
                },
                "method": "get",
                "path": "/beta/teams/@{encodeURIComponent(parameters('TeamId'))}/channels/@{encodeURIComponent(parameters('ChannelId'))}/messages"
            },
            "runtimeConfiguration": {
                "paginationPolicy": {
                    "minimumItemCount": 5
                }
            }
        },
        "Is_existing_minimal_alert": {
            "actions": {
                "Update_the_minimal_adaptive_card_to_mark_as_resolved": {
                    "type": "ApiConnection",
                    "inputs": {
                        "host": {
                            "connection": {
                                "name": "@parameters('$connections')['teams']['connectionId']"
                            }
                        },
                        "method": "post",
                        "body": {
                            "messageId": "@variables('MessageId')",
                            "recipient": {
                                "groupId": "@parameters('TeamId')",
                                "channelId": "@parameters('ChannelId')"
                            },
                            "messageBody": "${local.alert-resolved-minimal-adaptive-card}"
                        },
                        "path": "/v1.0/teams/conversation/updateAdaptivecard/poster/Flow bot/location/@{encodeURIComponent('Channel')}"
                    }
                }
            },
            "runAfter": {
                "For_each_message": [
                    "Succeeded"
                ]
            },
            "else": {
                "actions": {
                    "Update_the_adaptive_card_to_mark_as_resolved": {
                        "type": "ApiConnection",
                        "inputs": {
                            "host": {
                                "connection": {
                                    "name": "@parameters('$connections')['teams']['connectionId']"
                                }
                            },
                            "method": "post",
                            "body": {
                                "messageId": "@variables('MessageId')",
                                "recipient": {
                                    "groupId": "@parameters('TeamId')",
                                    "channelId": "@parameters('ChannelId')"
                                },
                                "messageBody": "${local.alert-resolved-adaptive-card}"
                            },
                            "path": "/v1.0/teams/conversation/updateAdaptivecard/poster/Flow bot/location/@{encodeURIComponent('Channel')}"
                        }
                    }
                }
            },
            "expression": {
                "and": [
                    {
                        "lessOrEquals": [
                            "@length(variables('AffectedResourceIds'))",
                            3
                        ]
                    }
                ]
            },
            "type": "If"
        }
    },
    "runAfter": {
        "${azurerm_logic_app_action_custom.initialise-fired-timestamp.name}": [
            "Succeeded"
        ]
    },
    "else": {
        "actions": {
            "Is_new_minimal_alert": {
                "actions": {
                    "Post_minimal_adaptive_card_and_wait_for_a_response": {
                        "limit": {
                            "timeout": "P1D"
                        },
                        "type": "ApiConnectionWebhook",
                        "inputs": {
                            "host": {
                                "connection": {
                                    "name": "@parameters('$connections')['teams']['connectionId']"
                                }
                            },
                            "body": {
                                "notificationUrl": "@listCallbackUrl()",
                                "body": {
                                    "messageBody": "${local.alert-fired-minimal-adaptive-card}",
                                    "updateMessage": "Response sent",
                                    "recipient": {
                                        "groupId": "@parameters('TeamId')",
                                        "channelId": "@parameters('ChannelId')"
                                    }
                                }
                            },
                            "path": "/v1.0/teams/conversation/gatherinput/poster/Flow bot/location/@{encodeURIComponent('Channel')}/$subscriptions"
                        }
                    },
                    "Update_the_minimal_adaptive_card_to_mark_as_assigned": {
                        "runAfter": {
                            "Post_minimal_adaptive_card_and_wait_for_a_response": [
                                "Succeeded"
                            ]
                        },
                        "type": "ApiConnection",
                        "inputs": {
                            "host": {
                                "connection": {
                                    "name": "@parameters('$connections')['teams']['connectionId']"
                                }
                            },
                            "method": "post",
                            "body": {
                                "messageId": "@body('Post_minimal_adaptive_card_and_wait_for_a_response')?['messageId']",
                                "recipient": {
                                    "groupId": "@parameters('TeamId')",
                                    "channelId": "@parameters('ChannelId')"
                                },
                                "messageBody": "${local.alert-assigned-minimal-adaptive-card}"
                            },
                            "path": "/v1.0/teams/conversation/updateAdaptivecard/poster/Flow bot/location/@{encodeURIComponent('Channel')}"
                        }
                    }
                },
                "else": {
                    "actions": {
                        "Post_adaptive_card_and_wait_for_a_response": {
                            "limit": {
                                "timeout": "P1D"
                            },
                            "type": "ApiConnectionWebhook",
                            "inputs": {
                                "host": {
                                    "connection": {
                                        "name": "@parameters('$connections')['teams']['connectionId']"
                                    }
                                },
                                "body": {
                                    "notificationUrl": "@listCallbackUrl()",
                                    "body": {
                                        "messageBody": "${local.alert-fired-adaptive-card}",
                                        "updateMessage": "Response sent",
                                        "recipient": {
                                            "groupId": "@parameters('TeamId')",
                                            "channelId": "@parameters('ChannelId')"
                                        }
                                    }
                                },
                                "path": "/v1.0/teams/conversation/gatherinput/poster/Flow bot/location/@{encodeURIComponent('Channel')}/$subscriptions"
                            }
                        },
                        "Update_the_adaptive_card_to_mark_as_assigned": {
                            "runAfter": {
                                "Post_adaptive_card_and_wait_for_a_response": [
                                    "Succeeded"
                                ]
                            },
                            "type": "ApiConnection",
                            "inputs": {
                                "host": {
                                    "connection": {
                                        "name": "@parameters('$connections')['teams']['connectionId']"
                                    }
                                },
                                "method": "post",
                                "body": {
                                    "messageId": "@body('Post_adaptive_card_and_wait_for_a_response')?['messageId']",
                                    "recipient": {
                                        "groupId": "@parameters('TeamId')",
                                        "channelId": "@parameters('ChannelId')"
                                    },
                                    "messageBody": "${local.alert-assigned-adaptive-card}"
                                },
                                "path": "/v1.0/teams/conversation/updateAdaptivecard/poster/Flow bot/location/@{encodeURIComponent('Channel')}"
                            }
                        }
                    }
                },
                "expression": {
                    "and": [
                        {
                            "lessOrEquals": [
                                "@length(variables('AffectedResourceIds'))",
                                3
                            ]
                        }
                    ]
                },
                "type": "If"
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
#
# NOTE: Changing `display_name` here or in Azure Portal forces replacement on next `apply`.
#       This is not desirable as any previous authorization will also be dropped.
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
