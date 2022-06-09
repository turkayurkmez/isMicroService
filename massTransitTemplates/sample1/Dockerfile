FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["sample1.csproj", "sample1/"]
RUN dotnet restore "sample1/sample1.csproj"

COPY . sample1/.
RUN dotnet publish -c Release -o /app sample1/sample1.csproj 

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS publish
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "sample1.dll"]