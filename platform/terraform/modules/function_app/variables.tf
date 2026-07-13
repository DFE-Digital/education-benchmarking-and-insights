variable "core" {
  type = object({
    environment_prefix  = string
    name                = string
    location            = string
    resource_group_name = string
    tags                = map(string)
  })
}

variable "monitoring" {
  type = object({
    log_analytics_id                  = string
    instrumentation_connection_string = optional(string)
  })
}

variable "key_vault" {
  type = object({
    id            = string
    requires_keys = optional(bool, true)
  })
}

variable "app-settings" {
  type = map(string)
}

variable "service_plan" {
  type = object({
    os_type                        = optional(string, "Windows")
    size                           = optional(string, "Y1")
    maximum_elastic_worker_count   = optional(number)
    minimum_elastic_instance_count = optional(number)
  })
  default = {}
}

variable "networking" {
  type = object({
    enable_restrictions = bool
    subnet_ids          = list(string)
  })
}

variable "storage_account" {
  type = object({
    id   = string
    name = string
    key  = optional(string)
  })
}

variable "sql_server" {
  type = object({
    fqdn     = string
    username = string
    password = string
  })
}

variable "application_stack" {
  type = object({
    worker_runtime       = optional(string, "dotnet-isolated")
    use_32_bit_worker    = optional(bool, false)
    dotnet_version       = optional(string, "v8.0")
    use_isolated_runtime = optional(bool, true)
    node_version         = optional(string, "22")
    always_on            = optional(bool, false)
  })
  default = {}
}

variable "redis_cache" {
  type = object({
    id          = optional(string)
    contributor = optional(bool, false)
    owner       = optional(bool, false)
  })
  default = {}
}
