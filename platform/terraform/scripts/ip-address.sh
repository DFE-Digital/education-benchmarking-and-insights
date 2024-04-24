#!/bin/bash
set -e
ipAddress=$(curl ipinfo.io/ip)
echo "{\"ip\": \"$ipAddress\"}" | jq