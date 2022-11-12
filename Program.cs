using System.Diagnostics;

namespace DevotedDatabase
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Database Starting");
            // TODO convert to STDIN input
            string incomingFilePath = @"F:\Storage\TestingFiles\incoming\sample_input.txt";
            // Variables
            string databaseName = "DevotedDataBase";
            string tableName = "DevotedDataTable";
            string tempTableName = String.Concat(tableName,"Temporary");
            string delimiter = " ";
            bool transactIndicator = false;
            // Instantiate Database Object
            Database inMemoryDB = new Instantiator(databaseName, tableName).CreateDatabase();
            
            var lines = File.ReadLines(incomingFilePath);
            int lineIndex = 1;
            foreach (string line in lines)
            {             
                // Locate tables
                Table livetable = inMemoryDB.Table.Find(i => i.TableName == tableName);
                Table tempTable = inMemoryDB.Table.Find(i => i.TableName == tempTableName);   
                lineIndex ++;
                Console.WriteLine(line);
                string commandType = line.Split(" ")[0];
                // Update indicator if transaction has started
                if (commandType == "BEGIN")
                {
                    transactIndicator = true;
                    continue;
                }
                // For roll back command empty object and instantiate a new object
                if (commandType == "ROLLBACK")
                {
                    tempTable.Row.Clear();
                    continue;
                }
                if (commandType == "COMMIT")
                {
                    MergeTables(inMemoryDB, tempTable, livetable, lineIndex);
                    tempTable.Row.Clear();
                    transactIndicator = false;
                    continue;

                }
                // If its not a transaction use live table for updates and gets
                if (!transactIndicator)
                {
                    string commandName = line.Split(delimiter)[1];
                    Action(commandType, line, delimiter, inMemoryDB, commandName, lineIndex, livetable);
                    continue;
                }
                // If its a transaction use temp table
                if (transactIndicator)
                {
                    string commandName = line.Split(delimiter)[1];
                    Action(commandType, line, delimiter, inMemoryDB, commandName, lineIndex, tempTable);
                    continue;
                }
                

            }
        }
        static void Action(string commandType, string line, string delimiter, Database inMemoryDB, string commandName, int lineIndex
                           ,Table table)
        {
            switch (commandType)
                {
                    case "SET":
                    string setCommandValue = line.Split(delimiter)[2];
                    Setter setter = new Setter(inMemoryDB, commandName, setCommandValue, lineIndex, table);
                    break;
                    case "GET":
                    Getter getter = new Getter(inMemoryDB, commandName, lineIndex, table);                 
                    break;
                    case "DELETE":
                    Deleter deleter = new Deleter(inMemoryDB, commandName, lineIndex, table);
                    break;
                    case "COUNT":
                    // Known as value in the test case but can re-use commandName variable as input
                    Counter counter = new Counter(inMemoryDB, commandName, lineIndex, table);
                    break;
                    default: Console.WriteLine("Unrecognized Command");
                    break;
                }

        }
        static void MergeTables(Database inMemoryDB,Table source, Table target, int lineIndex)
        {
            List<Row> rowsInSource = source.Row;
            foreach(Row row in rowsInSource)
            {
                Setter mergeTables = new Setter(inMemoryDB, row.Column, row.Value, lineIndex, target);
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