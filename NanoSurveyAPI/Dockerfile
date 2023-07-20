FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY init-db.sh /app/init-db.sh
RUN chmod +x init-db.sh

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY --from=build-env /app/out .

COPY --from=build-env /app/init-db.sh .
COPY SQL/DatabaseCreationScript.sql /app/SQL/DatabaseCreationScript.sql

RUN chmod +x init-db.sh
RUN chmod +x /app/SQL/DatabaseCreationScript.sql

RUN apt-get update && apt-get install -y postgresql-client

ADD https://raw.githubusercontent.com/vishnubob/wait-for-it/master/wait-for-it.sh .
RUN chmod +x wait-for-it.sh

ENTRYPOINT ["./wait-for-it.sh", "nanosurvey_database:5432", "--", "./init-db.sh"]