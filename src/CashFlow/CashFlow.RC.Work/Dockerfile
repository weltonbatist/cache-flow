#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CashFlow.RC.Work/CashFlow.RC.Work.csproj", "CashFlow.RC.Work/"]
COPY ["CashFlow.CD.Domain/CashFlow.CD.Domain.csproj", "CashFlow.CD.Domain/"]
COPY ["CashFlow.RC.Domain/CashFlow.RC.Domain.csproj", "CashFlow.RC.Domain/"]
COPY ["CashFlow.RC.Common/CashFlow.RC.Common.csproj", "CashFlow.RC.Common/"]
COPY ["CashFlow.NATS/CashFlow.NATS.csproj", "CashFlow.NATS/"]
COPY ["CashFlow.RC.Infra/CashFlow.RC.Infra.csproj", "CashFlow.RC.Infra/"]
RUN dotnet restore "CashFlow.RC.Work/CashFlow.RC.Work.csproj"
COPY . .
WORKDIR "/src/CashFlow.RC.Work"
RUN dotnet build "CashFlow.RC.Work.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CashFlow.RC.Work.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CashFlow.RC.Work.dll"]