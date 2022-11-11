namespace DevotedDatabase
{

    public class Instantiator
    {
        public Database inMemoryDB {get; set;}
        public string databaseName {get; set;}
        public string tableName{get; set;}

        public Instantiator(string databaseName, string tableName)
        {   
            this.databaseName = databaseName;
            this.tableName = tableName;
        }
        public Database CreateDatabase()
        {
            Database inMemoryDB = new Database();
            inMemoryDB.DatabaseName = databaseName;
            // Instantiate Datatable Object
            inMemoryDB.Table = new List<Table>();
            Table newTable = new Table();
            newTable.TableName = tableName;
            // Instantiate Row Object
            newTable.Row = new List<Row>();
            // Add Table Object to Database Object
            inMemoryDB.Table.Add(newTable);
            return inMemoryDB;
        }
    }
}