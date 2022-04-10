# 第一階段，構建和測試
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /app

# COPY ./WebCore31/WebCore31.csproj ./WebCore31/
# COPY ./Core31.Library/Core31.Library.csproj ./Core31.Library/
# COPY ./Core31.Library.Test/Core31.Library.Test.csproj ./Core31.Library.Test/
# COPY ./*.csproj ./


# COPY ./WebCore31/ ./WebCore31/
# COPY ./Core31.Library/ ./Core31.Library/
# COPY ./Core31.Library.Test/ ./Core31.Library.Test/
COPY ./ ./

RUN dotnet restore ./WebCore31/WebCore31.csproj


RUN dotnet test ./Core31.Library.Test/Core31.Library.Test.csproj
RUN dotnet publish ./WebCore31/WebCore31.csproj -c Release -o out

# 第二階段，運行時
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app

COPY --from=build-env /app/out .

EXPOSE 80

ENTRYPOINT ["dotnet", "WebCore31.dll"]