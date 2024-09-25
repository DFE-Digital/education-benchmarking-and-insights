#script to format and validate terraform, as well as generate documentation
#need to install terraform and terraform-docs
#https://developer.hashicorp.com/terraform/tutorials/aws-get-started/install-cli
#https://terraform-docs.io/user-guide/installation/

Write-Host "format core-infrastructure"
terraform -chdir='../core-infrastructure/terraform' fmt -recursive
Write-Host "format data-pipeline"
terraform -chdir='../data-pipeline/terraform' fmt -recursive
Write-Host "format platform"
terraform -chdir='../platform/terraform' fmt -recursive
Write-Host "format web"
terraform -chdir='../web/terraform' fmt -recursive
Write-Host "format prototype"
terraform -chdir='../prototype/terraform' fmt -recursive
Write-Host "format performance-tests"
terraform -chdir='../performance-tests/terraform' fmt -recursive
Write-Host "format support-analytics"
terraform -chdir='../support-analytics/terraform' fmt -recursive

Write-Host "validate core-infrastructure"
terraform -chdir='../core-infrastructure/terraform' init -backend=false -upgrade
terraform -chdir='../core-infrastructure/terraform' validate

Write-Host "validate data-pipeline"
terraform -chdir='../data-pipeline/terraform' init -backend=false -upgrade
terraform -chdir='../data-pipeline/terraform' validate

Write-Host "validate platform"
terraform -chdir='../platform/terraform' init -backend=false -upgrade
terraform -chdir='../platform/terraform' validate

Write-Host "validate web"
terraform -chdir='../web/terraform' init -backend=false -upgrade
terraform -chdir='../web/terraform' validate

Write-Host "validate prototype"
terraform -chdir='../prototype/terraform' init -backend=false -upgrade
terraform -chdir='../prototype/terraform' validate

Write-Host "validate performance-tests"
terraform -chdir='../performance-tests/terraform' init -backend=false -upgrade
terraform -chdir='../performance-tests/terraform' validate

Write-Host "validate support-analytics"
terraform -chdir='../support-analytics/terraform' init -backend=false -upgrade
terraform -chdir='../support-analytics/terraform' validate

Write-Host "document core-infrastructure"
terraform-docs markdown table --output-file README.md ../core-infrastructure/terraform
Write-Host "document data-pipeline"
terraform-docs markdown table --output-file README.md ../data-pipeline/terraform
Write-Host "document platform"
terraform-docs markdown table --recursive --output-file README.md ../platform/terraform
Write-Host "document web"
terraform-docs markdown table --output-file README.md ../web/terraform
Write-Host "document prototype"
terraform-docs markdown table --output-file README.md ../prototype/terraform
Write-Host "document performance-tests"
terraform-docs markdown table --output-file README.md ../performance-tests/terraform
Write-Host "document support-analytics"
terraform-docs markdown table --output-file README.md ../support-analytics/terraform