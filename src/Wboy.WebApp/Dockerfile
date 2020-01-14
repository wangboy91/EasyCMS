FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Wboy.WebApp/Wboy.WebApp.csproj", "Wboy.WebApp/"]
COPY ["Wboy.Infrastructure.Core/Wboy.Infrastructure.Core.csproj", "Wboy.Infrastructure.Core/"]
COPY ["Wboy.Application.Service/Wboy.Application.Service.csproj", "Wboy.Application.Service/"]
COPY ["Wboy.Domain/Wboy.Domain.csproj", "Wboy.Domain/"]
COPY ["Wboy.Application.Dto/Wboy.Application.Dto.csproj", "Wboy.Application.Dto/"]
COPY ["Wboy.MySql.EfCore/Wboy.MySql.EfCore.csproj", "Wboy.MySql.EfCore/"]
RUN dotnet restore "Wboy.WebApp/Wboy.WebApp.csproj"
COPY . .
WORKDIR "/src/Wboy.WebApp"
RUN dotnet build "Wboy.WebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Wboy.WebApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Wboy.WebApp.dll"]