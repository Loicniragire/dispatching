#Docker
Install the latest MS SQL container:
- docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<password>' \
     -p 1433:1433 --name mssqlserver \
     -d mcr.microsoft.com/mssql/server:2019-latest

Assert that container is running:
- docker ps -a

Connnect to SQL using SA and password above.

