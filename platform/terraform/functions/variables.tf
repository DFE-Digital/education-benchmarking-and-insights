variable "function-name" {
  type = string
}

variable "app-settings" {
  type = map(string)
}

variable "sku" {
  type=object({
    tier=string
    size=string
  })
  default = {
    tier = "Dynamic"
    size = "Y1"
  }
}

variable "requires-keys" {
  type = bool
  default = true
}

variable "always-on" {
  type = bool
  default = false
}

variable "cors" {
  type=list(string)
  default = ["*"]
}

variable "environment-prefix" {
  type = string
}

variable "key-vault-id" {
  type = string
}

variable "application-insights-key" {
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
  type = string
}