
setup:
	npm install -g @mermaid-js/mermaid-cli

architecture-pdf:
	./tools/make-pdf.sh architecture architecture $(OUTPUT)

architecture-docx: 
	./tools/make-docx.sh architecture architecture $(OUTPUT)

architecture-jira: 
	./tools/make-jira.sh architecture architecture $(OUTPUT)

testing-pdf: 
	./tools/make-pdf.sh testing architecture $(OUTPUT)

testing-docx: 
	./tools/make-docx.sh testing architecture $(OUTPUT)

testing-jira: 
	./tools/make-jira.sh testing architecture $(OUTPUT)