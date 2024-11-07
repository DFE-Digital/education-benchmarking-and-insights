variable "common-tags" {
  description = "Tags to be applied to all resources."
  type        = map(string)
}

variable "image-name" {
  description = "Container image to be used for each worker."
  type        = string
}

variable "environment-prefix" {
  description = "Prefix to be used for resources in the current environment."
  type        = string
}

variable "worker-queue-name" {
  description = "Name of the queue which will trigger the worker."
  type        = string
}

variable "container-app-environment-id" {
  description = "Container App Environment ID"
  type        = string
}

variable "container-app-resource-group-name" {
  description = "Name of the Azure Resource group in which Container App resources are to be created."
  type        = string
}

variable "container-app-name-suffix" {
  description = "Unique suffix for the Container App name."
  type        = string
}

variable "core-resource-group-name" {
  description = "Name of the core Azure Resource group from which various data sources are referenced."
  type        = string
}

variable "registry-name" {
  description = "Azure Container Registry name."
  type        = string
}

variable "storage-account-name" {
  description = "Azure Storage Account name."
  type        = string
}

variable "key-vault-name" {
  description = "Azure Key Vault name."
  type        = string
}

variable "core-db-domain-name-secret-name" {
  description = "Name of the Azure Key Vault Secret for the DB host."
  type        = string
  default     = "core-sql-domain-name"
}
variable "core-db-name-secret-name" {
  description = "Name of the Azure Key Vault Secret for the DB name."
  type        = string
  default     = "core-sql-db-name"
}

variable "core-db-user-name-secret-name" {
  description = "Name of the Azure Key Vault Secret for the DB username."
  type        = string
  default     = "core-sql-user-name"
}

variable "core-db-password-secret-name" {
  description = "Name of the Azure Key Vault Secret for the DB password."
  type        = string
  default     = "core-sql-password"
}

variable "max-replicas" {
  description = "The max. number of Container App replicas to launch."
  type        = number
}
