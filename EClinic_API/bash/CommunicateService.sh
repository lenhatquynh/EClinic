cd ..
cd Project.CommunicateService
dotnet ef database update --connection "Data Source=localhost,1444;Initial Catalog=CommunicateService;Persist Security Info=True;User ID=sa;Password=Eclinic123;"
sqlcmd -S localhost,1444 -U sa -d CommunicateService -i "../Database/CommunicateService.sql"
cd ..
cd bash
