cd ..
cd Project.AIService
sqlcmd -S localhost,1433 -U sa -P Eclinic123 -i "..\Database\AIService.sql"
cd ..
docker start ai
cd bash

