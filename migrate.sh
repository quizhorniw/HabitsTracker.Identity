#!/bin/bash

if [ -z "$1" ]; then
  echo "Usage: $0 <MigrationName>"
  exit 1
fi

NAME=$1

dotnet ef migrations add $NAME \
  --output-dir Migrations