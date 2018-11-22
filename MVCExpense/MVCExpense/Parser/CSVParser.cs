namespace MVCExpense.Parser
{
    using Microsoft.VisualBasic.FileIO;

    public class CSVParser : Parser
    {
        protected string Delimiters = ",";

        public CSVParser() : base()
        {

        }

        public override void ParseData(string filePath)
        {
            using (var parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(this.Delimiters);
                while (!parser.EndOfData)
                {
                    this.ParseLine(parser.ReadFields());
                }
            }
        }
    }
}