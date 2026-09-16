FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY PayrollAPI/PayrollAPI.csproj PayrollAPI/
RUN dotnet restore PayrollAPI/PayrollAPI.csproj

COPY PayrollAPI/ PayrollAPI/
RUN dotnet publish PayrollAPI/PayrollAPI.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "PayrollAPI.dll"]
