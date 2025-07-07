#!/bin/bash
 
echo "Checking for UTF-8 BOMs..."
 
files_with_bom=$(find . -type f \( -name "*.groovy" -o -name "Jenkinsfile" -o -name "*.sh" \) -exec grep -l $'\xEF\xBB\xBF' {} +)
 
if [ -n "$files_with_bom" ]; then
    echo "BOM found in the following files:"
    echo "$files_with_bom"
    exit 1
else
    echo "No BOMs found!"
fi