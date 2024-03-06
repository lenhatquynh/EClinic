cd ..
cd Project.ServiceInformationService
dotnet ef database update --connection "Data Source=localhost,1444;Initial Catalog=ServiceInformationService;Persist Security Info=True;User ID=sa;Password=Eclinic123;"
sqlcmd -S localhost,1444 -U sa -d ServiceInformationService -i "../Database/ServiceInformationService.sql"
cd ..
cd bash
