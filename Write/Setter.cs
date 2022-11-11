using System.Diagnostics;

namespace DevotedDatabase
{

    public class Setter
    {
        public Setter(Database inMemoryDB, string column, string newValue, int lineIndex, Table table)
        {
            // Check if column already exists, if not create it, else update it
            if(table.Row.Find(i => i.Column == column) == null)
            {
                Row newRow = new Row();
                newRow.RowId = Guid.NewGuid();
                newRow.DateCreated = DateTime.Today;
                newRow.DateUpdated = DateTime.Today;
                newRow.Column = column;
                newRow.Value = newValue;
                table.Row.Add(newRow);
            }
            else
            {
                Row currentRow = table.Row.Find(i => i.Column == column);
                currentRow.Value = newValue;
            }

        }
    }
}


//            string[] commandNames = names.Split(",");
//            string[] commandValues = values.Split(",");
//            Dictionary<string,string> tempDict = new Dictionary<string,string>();
//            int iterator = 0;
//            string tableName = row.Key.Split(".")[0];
//            string columnName = row.Key.Split(".")[1];

//            if (commandNames.Length != commandValues.Length)
//            {
//                Console.WriteLine("Mismatch in name and value pairs. Skipping line number {0}.", lineIndex);
//                return;
//            }

//            while (iterator < commandNames.Length)
//            {
//                tempDict.Add(commandNames[iterator], commandValues[iterator]);
//                iterator ++;
//            }