cd ..
cd Project.ProfileService
dotnet ef database update --connection "Data Source=localhost,1433;Initial Catalog=ProfileService;Persist Security Info=True;User ID=sa;Password=Eclinic123;"
sqlcmd -S localhost,1433 -U sa -d ProfileService -i "../Database/ProfileService.sql"
cd ..
cd bash

