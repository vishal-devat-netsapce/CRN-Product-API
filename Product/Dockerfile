FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Product/API.csproj", "Product/"]
COPY ["DTOModel/DTOModel.csproj", "DTOModel/"]
COPY ["Model/Model.csproj", "Model/"]
COPY ["Operation/Operation.csproj", "Operation/"]
COPY ["Repository/Repository.csproj", "Repository/"]

RUN dotnet restore "Product/API.csproj"

COPY . .

WORKDIR "/src/Product"

RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "API.dll"]