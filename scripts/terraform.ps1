#script to format and validate terraform, as well as generate documentation
#need to install terraform and terraform-docs
#https://developer.hashicorp.com/terraform/tutorials/aws-get-started/install-cli
#https://terraform-docs.io/user-guide/installation/

terraform -chdir='../core-infrastructure/terraform' fmt -recursive
terraform -chdir='../data-pipeline/terraform' fmt -recursive
terraform -chdir='../platform/terraform' fmt -recursive
terraform -chdir='../web/terraform' fmt -recursive
terraform -chdir='../prototype/terraform' fmt -recursive
terraform -chdir='../performance-tests/terraform' fmt -recursive
terraform -chdir='../support-analytics/terraform' fmt -recursive

terraform -chdir='../core-infrastructure/terraform' init -backend=false -upgrade
terraform -chdir='../core-infrastructure/terraform' validate

terraform -chdir='../data-pipeline/terraform' init -backend=false -upgrade
terraform -chdir='../data-pipeline/terraform' validate

terraform -chdir='../platform/terraform' init -backend=false -upgrade
terraform -chdir='../platform/terraform' validate

terraform -chdir='../web/terraform' init -backend=false -upgrade
terraform -chdir='../web/terraform' validate

terraform -chdir='../prototype/terraform' init -backend=false -upgrade
terraform -chdir='../prototype/terraform' validate

terraform -chdir='../performance-tests/terraform' init -backend=false -upgrade
terraform -chdir='../performance-tests/terraform' validate

terraform -chdir='../support-analytics/terraform' init -backend=false -upgrade
terraform -chdir='../support-analytics/terraform' validate

terraform-docs markdown table --output-file README.md ../core-infrastructure/terraform
terraform-docs markdown table --output-file README.md ../data-pipeline/terraform
terraform-docs markdown table --recursive --output-file README.md ../platform/terraform
terraform-docs markdown table --output-file README.md ../web/terraform
terraform-docs markdown table --output-file README.md ../prototype/terraform
terraform-docs markdown table --output-file README.md ../performance-tests/terraform
terraform-docs markdown table --output-file README.md ../support-analytics/terraform