cd ..
cd Project.PaymentService
dotnet ef database update --connection "Data Source=localhost,1433;Initial Catalog=PaymentService;Persist Security Info=True;User ID=sa;Password=Eclinic123;"
sqlcmd -S localhost,1433 -U sa -d PaymentService -i "../Database/PaymentService.sql"
cd ..
cd bash
