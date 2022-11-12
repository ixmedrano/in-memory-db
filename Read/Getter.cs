namespace DevotedDatabase
{

    public class Getter
    {

        public Getter(Database inMemoryDB, string column, Table table)
        {   
            if (table.Row == null || table.Row.Find(i => i.Column == column) == null)
            {
                Console.WriteLine("NULL");
            }
            else
            {
                Row currentRow = table.Row.Find(i => i.Column == column);
                Console.WriteLine("{0}",currentRow.Value);
            }
        }
    }
}


//        public Row getRow {get;}
//        this.getRow = currentRow;