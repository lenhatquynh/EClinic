cd ..
cd Project.CommunicateService
dotnet ef database update --connection "Data Source=localhost,1433;Initial Catalog=CommunicateService;Persist Security Info=True;User ID=sa;Password=Eclinic123;"
sqlcmd -S localhost,1433 -U sa -P Eclinic123 -d CommunicateService -i "..\Database\CommunicateService.sql"
cd ..
cd bash
