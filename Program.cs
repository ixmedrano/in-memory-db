using System.Diagnostics;

namespace DevotedDatabase
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Database Starting");
            // Variables
            string databaseName = "DevotedDataBase";
            string tableName = "DevotedDataTable";
            string tempTableName = String.Concat(tableName,"Temporary");
            string delimiter = " ";
            bool transactIndicator = false;
            bool activeIndicator = true;
            // Instantiate Database Object
            Database inMemoryDB = new Instantiator(databaseName, tableName).CreateDatabase();
            Console.WriteLine("Enter a command:");
            while (activeIndicator)
            {
                string line = Console.ReadLine();
                string cleanedLine = line.ToUpper();
                if (cleanedLine.Split(" ")[0] == "END")
                {
                    Console.WriteLine("Exiting Database");
                    activeIndicator = false;
                    break;
                }
                TransactionHandler(cleanedLine, inMemoryDB, tableName, tempTableName, transactIndicator, delimiter, activeIndicator);
            }
            
        }
        static void TransactionHandler(string line, Database inMemoryDB, string tableName, string tempTableName
                                       ,bool transactIndicator, string delimiter, bool activeIndicator)
        {

            // Locate tables
            Table livetable = inMemoryDB.Table.Find(i => i.TableName == tableName);
            Table tempTable = inMemoryDB.Table.Find(i => i.TableName == tempTableName);
            
            string commandType = line.Split(" ")[0];
            // Update indicator if transaction has started
            if (commandType == "BEGIN")
            {
                transactIndicator = true;
                
            }
            // For roll back command empty object and instantiate a new object
            if (commandType == "ROLLBACK")
            {
                tempTable.Row.Clear();
                
            }
            if (commandType == "COMMIT")
            {
                MergeTables(inMemoryDB, tempTable, livetable );
                tempTable.Row.Clear();
                transactIndicator = false;      
            }
            // If its not a transaction use live table for updates and gets
            if (!transactIndicator)
            {
                string commandName = line.Split(delimiter)[1];
                Crud(commandType, line, delimiter, inMemoryDB, commandName, livetable);
                
            }
            // If its a transaction use temp table
            if (transactIndicator)
            {
                string commandName = line.Split(delimiter)[1];
                Crud(commandType, line, delimiter, inMemoryDB, commandName, tempTable);
                
            }
            

        }
        static void Crud(string commandType, string line, string delimiter, Database inMemoryDB, string commandName 
                           ,Table table)
        {
            switch (commandType)
                {
                    case "SET":
                    string setCommandValue = line.Split(delimiter)[2];
                    Setter setter = new Setter(inMemoryDB, commandName, setCommandValue, table);
                    break;
                    case "GET":
                    Getter getter = new Getter(inMemoryDB, commandName, table);                 
                    break;
                    case "DELETE":
                    Deleter deleter = new Deleter(inMemoryDB, commandName, table);
                    break;
                    case "COUNT":
                    // Known as value in the test case but can re-use commandName variable as input
                    Counter counter = new Counter(inMemoryDB, commandName, table);
                    break;
                    default: Console.WriteLine("Unrecognized Command");
                    break;
                }

        }
        static void MergeTables(Database inMemoryDB,Table source, Table target)
        {
            List<Row> rowsInSource = source.Row;
            foreach(Row row in rowsInSource)
            {
                Setter mergeTables = new Setter(inMemoryDB, row.Column, row.Value, target);
            }

        }
    }
    
}
/*
                string fileName = Path.GetFileName(entry);
                string rejectedFile = System.IO.Path.Combine(rejectedFilePath, fileName);
                string ext = Path.GetExtension(entry);
                string[] fileEntries = Directory.GetFiles(incomingFilePath);
                string rejectedFilePath = @"F:\Storage\TestingFiles\rejected";
*/


/*          
            string rowName = "DevotedDataRow";
            Row newRow = new Row();
            newRow.Column = rowName;
            newRow.RowId = Guid.NewGuid();
            newRow.DateCreated = DateTime.Today;
            newRow.DateUpdated = DateTime.Today;
            // Add Row Object to Table Object
            newTable.Row.Add(newRow);
*/