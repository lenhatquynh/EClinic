# Running ASP.NET Core with Docker for EClinic API
This guide will show you how to run an EClinic API built with ASP.NET Core using Docker.

## Prerequisites
* Docker installed on your local machine.
* MongoDb installed on your local machine.
* SQl Server installed on your local machine.
* AWS CLI installed on your local machine.
## Run on Windows
### 1. Generate Https Certificate
* cmd
```sh
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p Eclinic123
  ```
* cmd
```sh
dotnet dev-certs https --trust
  ```
### 2. AWS Local Configuration profile
* Download AWS CLI here: https://docs.aws.amazon.com/cli/latest/userguide/getting-started-install.html
 ```sh
aws configure --profile "eclinic"
  ```
* This is just for our identification. With that, you will be prompted to enter the access id and the secret.
### 3. Run Docker Compose
* Build docker compose.
 ```sh
docker compose build
  ```
* Run docker compose.
 ```sh
docker compose up
  ```
* or  run docker compose in background.
 ```sh
docker compose up -d
  ```
### 4 Install dotnet ef tool
* cmd
```sh
dotnet tool install --global dotnet-ef --version 6.*
  ```
### 5 Instal sqlcmd command
* In windows when you install sql server you will have sqlcmd command
### 6 Instal MongoDB Shell
* Download MongoDB Shell here: https://downloads.mongodb.com/compass/mongosh-1.8.0-x64.msi
### 7 Run file bat
* In the `bash` folder double click on the file `EClinic.bat` or open a terminal there and run the command.
 ```sh
 .\EClinic.bat
  ```
* Running this bat file is to update the database and add data to the tables.

## Run on macOS or Linux
### 1. Generate Https Certificate
* shell
```sh
sudo dotnet dev-certs https -ep .aspnet/https/aspnetapp.pfx -p Eclinic123
  ```
* shell
```sh
dotnet dev-certs https --trust
  ```
### 2. AWS Local Configuration profile
* Download AWS CLI here: https://docs.aws.amazon.com/cli/latest/userguide/getting-started-install.html
 ```sh
aws configure --profile "eclinic"
  ```
* This is just for our identification. With that, you will be prompted to enter the access id and the secret.
### 3. Run Docker Compose
* Build docker compose.
 ```sh
docker compose build
  ```
* Run docker compose.
 ```sh
docker compose up
  ```
* or run docker compose in background.
 ```sh
docker compose up -d
  ```

### 4 Install dotnet ef tool
* shell
```sh
dotnet tool install --global dotnet-ef
  ```
### 5 Instal sqlcmd command
* For mac
 ```
 brew install sqlcmd
  ```
* For Linux
 ```
 sudo apt-get install mssql-tools
  ```
  ```
 sudo ls /opt/mssql-tools/bin/sqlcmd*
  ```
  ```
 sudo ln -sfn /opt/mssql-tools/bin/sqlcmd /usr/bin/sqlcmd
  ```
### 6 Instal MongoDB Shell
* Download MongoDB Shell here: https://www.mongodb.com/try/download/shell
### 7 Run file bat
* In `bash` folder open terminal and run command.
 ```sh
 sh EClinic.sh
  ```
* Running this bat file is to update the database and add data to the tables.
# Api Information
| Service | Url | Description |
| -------- | -------- | -------- |
| API gateway | https://localhost:8888 | This is the API gateway used to run all services in the system |
| Identity Service | https://localhost:1111/swagger/index.html | This is Swagger UI showing you all the Identity Service Api |
| Profile Service | https://localhost:2222/swagger/index.html | This is Swagger UI showing you all the Profile Service Api |
| Forum Service | https://localhost:3333/swagger/index.html | This is Swagger UI showing you all the Forum Service Api |
| Blog Service | https://localhost:5555/swagger/index.html | This is Swagger UI showing you all the Blog Service Api |
| Service Information Service | https://localhost:5432/swagger/index.html | This is Swagger UI showing you all the Service Information Service Api |
| Booking Service | https://localhost:7777/swagger/index.html | This is Swagger UI showing you all the Booking Service Api |
| Communication Service | https://localhost:8686/swagger/index.html | This is Swagger UI showing you all the Communication Service Api |
| Payment Service | https://localhost:9999/swagger/index.html | This is Swagger UI showing you all the Payment Service Api |
