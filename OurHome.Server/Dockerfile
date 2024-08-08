FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OurHome.Server/OurHome.Server.csproj", "OurHome.Server/"]
COPY ["OurHome.DataAccess/OurHome.DataAccess.csproj", "OurHome.DataAccess/"]
COPY ["OurHome.Model/OurHome.Model.csproj", "OurHome.Model/"]
COPY ["OurHome.Shared/OurHome.Shared.csproj", "OurHome.Shared/"]
COPY ["OurHome.Client/OurHome.Client.csproj", "OurHome.Client/"]
RUN dotnet restore "OurHome.Server/OurHome.Server.csproj"
COPY . .
WORKDIR "/src/OurHome.Server"
RUN dotnet build "OurHome.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OurHome.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OurHome.Server.dll"]