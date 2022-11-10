using System.Diagnostics;

namespace DevotedDatabase
{

    public class Setter
    {
        public Setter(Database inMemoryDB, string names, string values)
        {
            string[] commandNames = names.Split(",");
            string[] commandValues = values.Split(",");


            foreach (string command in commandNames)
            {
                string tableName = command.Split(",")[0];
                string columnName = command.Split(",")[1];
                
                if(!inMemoryDB.Table.TableName.Contains(tableName))
                {
                    Table newTable = new Table();
                    newTable.TableName = tableName;
                }
                
                Row newRow = new Row();
                newRow.RowId = Guid.NewGuid();
                newRow.DateCreated = DateTime.Today;
                newRow.DateUpdated = DateTime.Today;
                //newRow.RecordValues = 
                
            }

            Console.WriteLine(names);
            Console.WriteLine(values);
        }
    }
}