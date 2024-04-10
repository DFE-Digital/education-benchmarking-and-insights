variable "function-name" {
  type = string
}

variable "app-settings" {
  type = map(string)
}

variable "sku" {
  type = object({
    tier = string
    size = string
  })
  default = {
    tier = "Dynamic"
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

variable "application-insights-connection-string" {
  type = string
}

variable "location" {
  type = string
}

variable "resource-group-name" {
  type = string
}

variable "storage-account-name" {
  type = string
}

variable "storage-account-key" {
  type = string
}

variable "common-tags" {
  type = map(string)
}

variable "enable-restrictions" {
  type = bool
}