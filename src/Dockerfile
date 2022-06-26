FROM mcr.microsoft.com/dotnet/sdk:6.0 AS installer-env

# Build requires 3.1 SDK
COPY --from=mcr.microsoft.com/dotnet/core/sdk:3.1 /usr/share/dotnet /usr/share/dotnet
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1

COPY . .

RUN dotnet restore "HelloWorld.sln" --disable-parallel
RUN mkdir -p /home/site/wwwroot
RUN dotnet publish HelloWorld.csproj -c Release --output /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4

ENV LANG de_DE.UTF-8
ENV TZ=Europe/Berlin
ENV LANGUAGE ${LANG}

ENV LC_ALL ${LANG}
ENV AzureWebJobsScriptRoot=/home/site/wwwroot 
ENV AzureFunctionsJobHost__Logging__Console__IsEnabled=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1
ENV FUNCTIONS_WORKER_RUNTIME dotnet-isolated
ENV AzureWebJobsStorage="DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://azurite:10000/devstoreaccount1;QueueEndpoint=http://azurite:10001/devstoreaccount1;TableEndpoint=http://azurite:10002/devstoreaccount1;"

COPY --from=installer-env ["/home/site/wwwroot", "/home/site/wwwroot"]
