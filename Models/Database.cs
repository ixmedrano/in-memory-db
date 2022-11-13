namespace DevotedDatabase
{
    public class Row
    {
        public int  TransactionId {get;set;}
        public string  Column {get;set;}
        public string ? Value {get;set;}
    }
    public class Table
    {
        public string TableName {get;set;}
        public List<Row> ? Row {get;set;}   

    }
    public class Database
    {
        public string DatabaseName {get;set;}
        public List<Table> ? Table {get;set;}
        public int TransactionNumber {get;set;} = 0;
    }

}