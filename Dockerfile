FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /main
COPY ["src/Ozon.MerchandiseService/Ozon.MerchandiseService.csproj", "src/Ozon.MerchandiseService/"]
RUN dotnet restore "src/Ozon.MerchandiseService/Ozon.MerchandiseService.csproj"

COPY . .

WORKDIR "/main/src/Ozon.MerchandiseService"

RUN dotnet build "Ozon.MerchandiseService.csproj" -c Realese -o /app/build

FROM build AS publish
RUN dotnet publish "Ozon.MerchandiseService.csproj" -c Realese -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM runtime AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Ozon.MerchandiseService.dll"]
