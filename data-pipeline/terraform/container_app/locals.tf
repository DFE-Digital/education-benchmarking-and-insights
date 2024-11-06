locals {
  container-app-name = substr("${var.environment-prefix}-ebis-pipeline-${var.container-app-name-suffix}", 0, 32)
}