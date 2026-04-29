#!/bin/bash

set -e

mkdir -p "./preDockerBuild"

cp *.sln* "./preDockerBuild/"

find src -name "*.csproj" | while read file; do
  dir=$(dirname "$file")
  mkdir -p "./preDockerBuild/$dir"
  cp "$file" "./preDockerBuild/$dir/"
done