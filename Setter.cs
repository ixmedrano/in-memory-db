using System.Diagnostics;

namespace DevotedDatabase
{

    public class Setter
    {
        public Setter(Database inMemoryDB, string names, string values, int lineIndex)
        {
            string[] commandNames = names.Split(",");
            string[] commandValues = values.Split(",");
            Dictionary<string,string> tempDict = new Dictionary<string,string>();
            int iterator = 0;

            if (commandNames.Length != commandValues.Length)
            {
                Console.WriteLine("Mismatch in name and value pairs. Skipping line number {0}.", lineIndex);
                return;
            }

            while (iterator < commandNames.Length)
            {
                tempDict.Add(commandNames[iterator], commandValues[iterator]);
                iterator ++;
            }

            foreach (KeyValuePair<string,string> row in tempDict)
            {
                string tableName = row.Key.Split(".")[0];
                string columnName = row.Key.Split(".")[1];
                
                if(inMemoryDB.Table == null || !inMemoryDB.Table.Where(i => i.TableName == tableName).Any())
                {
                    Table newTable = new Table();
                    newTable.Row = new List<Row>();
                    newTable.TableName = tableName;
                    inMemoryDB.Table.Add(newTable);
                }

                if(inMemoryDB.Table!.Where(i => i.TableName == tableName).Any())
                {
                    Table table = inMemoryDB.Table.Find(i => i.TableName == tableName);
                    Row newRow = new Row();
                    newRow.RowId = Guid.NewGuid();
                    newRow.DateCreated = DateTime.Today;
                    newRow.DateUpdated = DateTime.Today;
                    newRow.Column = columnName;
                    newRow.Value = row.Value;

                    table.Row.Add(newRow);
                    
                }
                               
            }


        }
    }
}