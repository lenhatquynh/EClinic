cd ..
cd Project.ServiceInformationService
dotnet ef database update --connection "Data Source=localhost,1433;Initial Catalog=ServiceInformationService;Persist Security Info=True;User ID=sa;Password=Eclinic123;"
sqlcmd -S localhost,1433 -U sa -P Eclinic123 -d ServiceInformationService -i "..\Database\ServiceInformationService.sql"
cd ..
cd bash
