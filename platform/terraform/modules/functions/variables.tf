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

variable "subnet_id" {
  type = string
}

variable "instrumentation-key" {
  type     = string
  nullable = true
  default  = null
}

variable "instrumentation-conn-string" {
  type     = string
  nullable = true
  default  = null
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
