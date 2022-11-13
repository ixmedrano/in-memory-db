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
            else if (table.Row.Find(i => i.Column == column && i.TransactionId == inMemoryDB.TransactionNumber) != null)
            {
                Row currentRow = table.Row.Find(i => i.Column == column && i.TransactionId == inMemoryDB.TransactionNumber);
                Console.WriteLine("{0}",currentRow.Value);
            }
            else
            {
                Row currentRow = table.Row.FindLast(i => i.Column == column && i.TransactionId == inMemoryDB.TransactionNumber);
                Console.WriteLine("{0}",currentRow.Value);
            }
        }
    }
}


//        public Row getRow {get;}
//        this.getRow = currentRow;