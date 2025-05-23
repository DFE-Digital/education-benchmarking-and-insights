variable "function-name" {
  type = string
}

variable "app-settings" {
  type = map(string)
}

variable "sku" {
  type = object({
    size = string
  })
  default = {
    size = "Y1"
  }
}

variable "requires-keys" {
  type    = bool
  default = true
}

variable "always-on" {
  type    = bool
  default = false
}

variable "environment-prefix" {
  type = string
}

variable "key-vault-id" {
  type = string
}

variable "subnet_ids" {
  type = list(string)
}

variable "instrumentation-conn-string" {
  type     = string
  nullable = true
  default  = null
}

variable "log-analytics-id" {
  type = string
}

variable "location" {
  type = string
}

variable "resource-group-name" {
  type = string
}

variable "storage-account-id" {
  type = string
}

variable "storage-account-name" {
  type = string
}

variable "storage-account-key" {
  type     = string
  nullable = true
  default  = null
}

variable "common-tags" {
  type = map(string)
}

variable "enable-restrictions" {
  type = bool
}

variable "sql-server-fqdn" {
  type = string
}

variable "sql-server-username" {
  type = string
}

variable "sql-server-password" {
  type      = string
  sensitive = true
}

variable "dotnet-version" {
  type    = string
  default = "v8.0"
}

variable "use-32-bit-worker" {
  type    = bool
  default = false
}

variable "worker-runtime" {
  type    = string
  default = "dotnet-isolated"
}

variable "use-isolated-runtime" {
  type    = bool
  default = true
}

variable "cache-contributor" {
  type    = bool
  default = false
}

variable "cache-owner" {
  type    = bool
  default = false
}

variable "redis-cache-id" {
  type    = string
  default = null
}

variable "os-type" {
  type    = string
  default = "Windows"
}

variable "node-version" {
  type    = string
  default = "22"
}

variable "maximum-elastic-worker-count" {
  type    = number
  default = null
}

variable "minimum-elastic-instance-count" {
  type    = number
  default = null
}