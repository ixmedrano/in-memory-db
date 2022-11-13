namespace DevotedDatabase
{
    public class Instantiator
    {
        public Database inMemoryDB {get; set;}
        public string databaseName {get; set;}
        public string tableName{get; set;}
        public string tempTableName {get;set;}

        public Instantiator(string databaseName, string tableName, string tempTableName)
        {   
            this.databaseName = databaseName;
            this.tableName = tableName;
            this.tempTableName = tempTableName;
        }
        public Database CreateDatabase()
        {
            // Create new instance of the database with the database name
            Database inMemoryDB = new Database();
            inMemoryDB.DatabaseName = databaseName;
            // Instantiate Datatable Objects
            inMemoryDB.Table = new List<Table>();
            Table newTable = new Table();
            Table tempTable = new Table();
            // Set table name properties
            newTable.TableName = tableName;
            tempTable.TableName = tempTableName;
            // Instantiate Row Object
            newTable.Row = new List<Row>();
            tempTable.Row = new List<Row>();
            // Add Table Object to Database Object
            inMemoryDB.Table.Add(newTable);
            inMemoryDB.Table.Add(tempTable);
            return inMemoryDB;
        }
    }
}