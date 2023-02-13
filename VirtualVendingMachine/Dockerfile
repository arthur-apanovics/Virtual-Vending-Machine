FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VirtualVendingMachine/VirtualVendingMachine.csproj", "VirtualVendingMachine/"]
RUN dotnet restore "VirtualVendingMachine/VirtualVendingMachine.csproj"
COPY . .
WORKDIR "/src/VirtualVendingMachine"
RUN dotnet build "VirtualVendingMachine.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VirtualVendingMachine.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VirtualVendingMachine.dll"]
