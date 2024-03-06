cd ..
cd Project.BookingService
dotnet ef database update --connection "Data Source=localhost,1444;Initial Catalog=BookingService;Persist Security Info=True;User ID=sa;Password=Eclinic123;"
sqlcmd -S localhost,1444 -U sa -d BookingService -i "../Database/BookingService.sql"
cd ..
cd bash
