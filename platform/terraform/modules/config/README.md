<!-- BEGIN_TF_DOCS -->
## Requirements

No requirements.

## Providers

No providers.

## Modules

No modules.

## Resources

No resources.

## Inputs

| Name | Description | Type | Default | Required |
|------|-------------|------|---------|:--------:|
| <a name="input_env_configs"></a> [env\_configs](#input\_env\_configs) | Internal configuration map. Defined as a variable to provide a strict type schema for IDEs. | <pre>map(object({<br>    search = object({<br>      sku           = string<br>      replica_count = number<br>    })<br>    sql = object({<br>      telemetry_enabled = bool<br>    })<br>    cache = object({<br>      sku      = string<br>      capacity = number<br>      family   = string<br>    })<br>    enable_ip_restrictions = bool<br>  }))</pre> | <pre>{<br>  "automated-test": {<br>    "cache": {<br>      "capacity": 1,<br>      "family": "C",<br>      "sku": "Basic"<br>    },<br>    "enable_ip_restrictions": false,<br>    "search": {<br>      "replica_count": 1,<br>      "sku": "basic"<br>    },<br>    "sql": {<br>      "telemetry_enabled": false<br>    }<br>  },<br>  "development": {<br>    "cache": {<br>      "capacity": 1,<br>      "family": "C",<br>      "sku": "Basic"<br>    },<br>    "enable_ip_restrictions": false,<br>    "search": {<br>      "replica_count": 1,<br>      "sku": "basic"<br>    },<br>    "sql": {<br>      "telemetry_enabled": true<br>    }<br>  },<br>  "feature": {<br>    "cache": {<br>      "capacity": 1,<br>      "family": "C",<br>      "sku": "Basic"<br>    },<br>    "enable_ip_restrictions": true,<br>    "search": {<br>      "replica_count": 1,<br>      "sku": "basic"<br>    },<br>    "sql": {<br>      "telemetry_enabled": true<br>    }<br>  },<br>  "pre-production": {<br>    "cache": {<br>      "capacity": 1,<br>      "family": "C",<br>      "sku": "Standard"<br>    },<br>    "enable_ip_restrictions": true,<br>    "search": {<br>      "replica_count": 1,<br>      "sku": "basic"<br>    },<br>    "sql": {<br>      "telemetry_enabled": false<br>    }<br>  },<br>  "production": {<br>    "cache": {<br>      "capacity": 1,<br>      "family": "C",<br>      "sku": "Standard"<br>    },<br>    "enable_ip_restrictions": true,<br>    "search": {<br>      "replica_count": 3,<br>      "sku": "basic"<br>    },<br>    "sql": {<br>      "telemetry_enabled": false<br>    }<br>  },<br>  "test": {<br>    "cache": {<br>      "capacity": 1,<br>      "family": "C",<br>      "sku": "Standard"<br>    },<br>    "enable_ip_restrictions": true,<br>    "search": {<br>      "replica_count": 1,<br>      "sku": "basic"<br>    },<br>    "sql": {<br>      "telemetry_enabled": false<br>    }<br>  }<br>}</pre> | no |
| <a name="input_environment"></a> [environment](#input\_environment) | The target environment. | `string` | n/a | yes |

## Outputs

| Name | Description |
|------|-------------|
| <a name="output_cache"></a> [cache](#output\_cache) | n/a |
| <a name="output_enable_ip_restrictions"></a> [enable\_ip\_restrictions](#output\_enable\_ip\_restrictions) | n/a |
| <a name="output_search"></a> [search](#output\_search) | n/a |
| <a name="output_sql"></a> [sql](#output\_sql) | n/a |
<!-- END_TF_DOCS -->