FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build

RUN apt-get update && apt-get upgrade -y


WORKDIR /app

COPY CC.csproj .
COPY *.cs .
COPY GUI .
COPY Model .
COPY Podman .


RUN dotnet publish -c Release
ENTRYPOINT ["dotnet", "run"]

FROM mcr.microsoft.com/dotnet/runtime:8.0-jammy AS runtime

RUN apt-get update && apt-get upgrade -y
RUN apt-get install podman -y

COPY --from=build /app/bin /home
WORKDIR /home/Release/net8.0/
ENTRYPOINT ["dotnet", "CC.dll"]

