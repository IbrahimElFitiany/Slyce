# Docker Cache Prep

## Why

To cache `dotnet restore` in Docker, you need to copy `.csproj` files before copying source. The naïve approach is listing every project manually in the Dockerfile, which is ugly, error-prone, and breaks silently when you add a new module or layer and forget to update it. With Slyce's structure (multiple bounded contexts, each with Domain/Application/Infrastructure), that list gets out of hand fast.

`prep-docker-cache` automates this by scanning the repo and mirroring all `.csproj` and `.sln` files into `preDockerBuild/`, which the Dockerfile then copies in one shot.

---

## Usage

Always run before building:

```bash
./prep-docker-cache && docker build -t slyce .
```

Re-run whenever you add, remove, or rename a `.csproj`. If `preDockerBuild/` is stale, the restore layer will be out of sync and the build may fail.

---

## How It Works

**`prep-docker-cache`** — copies only what `dotnet restore` needs:

```bash
mkdir -p "./preDockerBuild"
cp *.sln* "./preDockerBuild/"
find src -name "*.csproj" | while read file; do
  dir=$(dirname "$file")
  mkdir -p "./preDockerBuild/$dir"
  cp "$file" "./preDockerBuild/$dir/"
done
```

**Dockerfile** — two-phase build:

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /slyce

COPY preDockerBuild .
RUN dotnet restore "./src/WebAPI"   # cached unless a .csproj/.sln changes

COPY ./src ./src
RUN dotnet publish "./src/WebAPI" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/build .
EXPOSE 8080
ENTRYPOINT ["dotnet", "WebAPI.dll"]
```