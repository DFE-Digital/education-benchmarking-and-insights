module "local-authority-fa" {
  source = "./modules/functions"
  app-settings = merge(local.default_app_settings, {
    "Search__Name"                           = local.shared_app_settings.search_name
    "Search__Key"                            = local.shared_app_settings.search_key
    "Sql__ConnectionString"                  = local.shared_app_settings.sql_connection
    "WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED" = local.shared_app_settings.use_dotnet_isolated
  })

  core       = { name = "local-authority", environment_prefix = var.environment-prefix, resource_group_name = azurerm_resource_group.resource-group.name, location = var.location, tags = local.common-tags }
  monitoring = local.shared_monitoring
  key_vault  = local.shared_key_vault
  sql_server = local.shared_sql_server

  storage_account = local.shared_platform_storage
  networking      = local.shared_networking
}

module "school-fa" {
  source = "./modules/functions"
  app-settings = merge(local.default_app_settings, {
    "Search__Name"                           = local.shared_app_settings.search_name
    "Search__Key"                            = local.shared_app_settings.search_key
    "Sql__ConnectionString"                  = local.shared_app_settings.sql_connection
    "Cache__Host"                            = local.shared_app_settings.cache_host
    "Cache__Port"                            = local.shared_app_settings.cache_port
    "WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED" = local.shared_app_settings.use_dotnet_isolated
  })

  core       = { name = "school", environment_prefix = var.environment-prefix, resource_group_name = azurerm_resource_group.resource-group.name, location = var.location, tags = local.common-tags }
  monitoring = local.shared_monitoring
  key_vault  = local.shared_key_vault
  sql_server = local.shared_sql_server

  storage_account = local.shared_platform_storage
  networking      = local.shared_networking

  redis_cache = {
    id          = azurerm_redis_cache.cache.id
    contributor = true
  }
}

module "trust-fa" {
  source = "./modules/functions"
  app-settings = merge(local.default_app_settings, {
    "Search__Name"                           = local.shared_app_settings.search_name
    "Search__Key"                            = local.shared_app_settings.search_key
    "Sql__ConnectionString"                  = local.shared_app_settings.sql_connection
    "WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED" = local.shared_app_settings.use_dotnet_isolated
  })

  core       = { name = "trust", environment_prefix = var.environment-prefix, resource_group_name = azurerm_resource_group.resource-group.name, location = var.location, tags = local.common-tags }
  monitoring = local.shared_monitoring
  key_vault  = local.shared_key_vault
  sql_server = local.shared_sql_server

  storage_account = local.shared_platform_storage
  networking      = local.shared_networking
}

module "benchmark-fa" {
  source = "./modules/functions"
  app-settings = merge(local.default_app_settings, {
    "Search__Name"                           = local.shared_app_settings.search_name
    "Search__Key"                            = local.shared_app_settings.search_key
    "Sql__ConnectionString"                  = local.shared_app_settings.sql_connection
    "PipelineMessageHub__ConnectionString"   = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.pipeline-message-hub-storage-connection-string.versionless_id})"
    "PipelineMessageHub__JobPendingQueue"    = "data-pipeline-job-pending"
    "WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED" = local.shared_app_settings.use_dotnet_isolated
  })

  core       = { name = "benchmark", environment_prefix = var.environment-prefix, resource_group_name = azurerm_resource_group.resource-group.name, location = var.location, tags = local.common-tags }
  monitoring = local.shared_monitoring
  key_vault  = local.shared_key_vault
  sql_server = local.shared_sql_server

  storage_account = local.shared_platform_storage
  networking      = local.shared_networking
}

module "insight-fa" {
  source = "./modules/functions"
  app-settings = merge(local.default_app_settings, {
    "Sql__ConnectionString"                  = local.shared_app_settings.sql_connection
    "WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED" = local.shared_app_settings.use_dotnet_isolated
    "Cache__Host"                            = local.shared_app_settings.cache_host
    "Cache__Port"                            = local.shared_app_settings.cache_port
  })

  core       = { name = "insight", environment_prefix = var.environment-prefix, resource_group_name = azurerm_resource_group.resource-group.name, location = var.location, tags = local.common-tags }
  monitoring = local.shared_monitoring
  key_vault  = local.shared_key_vault
  sql_server = local.shared_sql_server

  storage_account = local.shared_platform_storage
  networking      = local.shared_networking

  redis_cache = {
    id          = azurerm_redis_cache.cache.id
    contributor = true
  }
}

module "chart-rendering-fa" {
  source = "./modules/functions"
  app-settings = merge(local.default_app_settings, {
    "FUNCTIONS_WORKER_PROCESS_COUNT"      = var.configuration[var.environment].ssr_fa_worker_process_count
    "WEBSITES_ENABLE_APP_SERVICE_STORAGE" = true
  })

  core       = { name = "chart-rendering", environment_prefix = var.environment-prefix, resource_group_name = azurerm_resource_group.resource-group.name, location = var.location, tags = local.common-tags }
  monitoring = local.shared_monitoring
  key_vault  = local.shared_key_vault
  sql_server = local.shared_sql_server

  storage_account = local.shared_platform_storage
  networking      = local.shared_networking

  service_plan = {
    os_type                        = "Linux"
    size                           = var.configuration[var.environment].ssr_fa_sku
    maximum_elastic_worker_count   = var.configuration[var.environment].ssr_fa_elastic_max_workers
    minimum_elastic_instance_count = var.configuration[var.environment].ssr_fa_elastic_min_instances
  }

  application_stack = {
    worker_runtime = "node"
    node_version   = "22"
  }
}

module "content-fa" {
  source = "./modules/functions"
  app-settings = merge(local.default_app_settings, {
    "Sql__ConnectionString"                  = local.shared_app_settings.sql_connection
    "WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED" = local.shared_app_settings.use_dotnet_isolated
  })

  core       = { name = "content", environment_prefix = var.environment-prefix, resource_group_name = azurerm_resource_group.resource-group.name, location = var.location, tags = local.common-tags }
  monitoring = local.shared_monitoring
  key_vault  = local.shared_key_vault
  sql_server = local.shared_sql_server

  storage_account = local.shared_platform_storage
  networking      = local.shared_networking
}

module "maintenance-tasks-fa" {
  source = "./modules/functions"
  app-settings = merge(local.default_app_settings, {
    "Sql__ConnectionString" = local.shared_app_settings.sql_connection
  })

  core       = { name = "maintenance-tasks", environment_prefix = var.environment-prefix, resource_group_name = azurerm_resource_group.resource-group.name, location = var.location, tags = local.common-tags }
  monitoring = local.shared_monitoring
  key_vault  = local.shared_key_vault
  sql_server = local.shared_sql_server

  storage_account = local.shared_platform_storage
  networking = {
    enable_restrictions = false
    subnet_ids          = [data.azurerm_subnet.web-app-subnet.id]
  }
}

module "orchestrator-fa" {
  source = "./modules/functions"
  app-settings = merge(local.default_app_settings, {
    "PipelineMessageHub__ConnectionString"     = "@Microsoft.KeyVault(SecretUri=${data.azurerm_key_vault_secret.pipeline-message-hub-storage-connection-string.versionless_id})"
    "PipelineMessageHub__JobFinishedQueue"     = "data-pipeline-job-finished"
    "PipelineMessageHub__JobCustomStartQueue"  = "data-pipeline-job-custom-start"
    "PipelineMessageHub__JobDefaultStartQueue" = "data-pipeline-job-default-start"
    "PipelineMessageHub__JobPendingQueue"      = "data-pipeline-job-pending"
    "Sql__ConnectionString"                    = local.shared_app_settings.sql_connection
    "Search__Name"                             = local.shared_app_settings.search_name
    "Search__Key"                              = local.shared_app_settings.search_key
    "Cache__Host"                              = local.shared_app_settings.cache_host
    "Cache__Port"                              = local.shared_app_settings.cache_port
  })

  core       = { name = "orchestrator", environment_prefix = var.environment-prefix, resource_group_name = azurerm_resource_group.resource-group.name, location = var.location, tags = local.common-tags }
  monitoring = local.shared_monitoring
  key_vault  = local.shared_key_vault
  sql_server = local.shared_sql_server

  storage_account = {
    id   = azurerm_storage_account.orchestrator-storage.id
    name = azurerm_storage_account.orchestrator-storage.name
    key  = azurerm_storage_account.orchestrator-storage.primary_access_key
  }

  networking = {
    enable_restrictions = lower(var.cip-environment) != "dev"
    subnet_ids          = [data.azurerm_subnet.web-app-subnet.id]
  }

  service_plan = {
    size = "P0v3"
  }

  application_stack = {
    always_on = true
  }

  redis_cache = {
    id          = azurerm_redis_cache.cache.id
    contributor = true
  }
}
