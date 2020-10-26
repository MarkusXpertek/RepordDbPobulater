# RepordDbPopulater
This project populates the database of the new version of creditdatawebapi with the existing reports.
# Process
In order for the report populater to run the users that own the reports should already be uploaded to the database
This project use the branch folder under witch the reports is saved to identify its owner.

## Cofigiration
In the project is a config.txt file used to configure the file locations of the reports and the connectinstring of the newly created database.
In the cinfig.txt file you will find the following:
#### example
_____
ConnectionString>
ReportsLocation>
_____

Specify the connection string to the database just after "ConnectionString>" 
And the location of the branch folders where the reports are saved just after "ReportsLocation>" 
#### example
_______
ConnectionString>Data Source=localhost,1433;Initial Catalog=CreditDataDb;User ID={User};Password={password}
ReportsLocation>C:\Users\MarkusMadeleyn\source\repos\XpertekAcquire\CreditDataWebAPI\quickstart\docker-compose\reports
__________

Now run the application. If the project runs succesfully the database should be filled with the existing reports related to the users thats owns the report.
