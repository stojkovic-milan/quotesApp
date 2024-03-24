ECHO Creating and starting database...
sqllocaldb create "QuotesDB3"
sleep 1
sqllocaldb start "QuotesDB3"
sleep 2
cd QuotesApi
ECHO Applying migrations...
dotnet ef database update
sleep 2
ECHO Seeding database with data...
cd ..
sqlcmd -S "(localdb)\QuotesDB3" -i ./SeedDB.sql

echo "Initialization successful"
echo "Press any key to exit"
read -n 1 key  # Read a single character within a 5-second
