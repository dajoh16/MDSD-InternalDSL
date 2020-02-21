namespace LossDataExtractor.MetaModel
{
    public class Header
    {
        public EntityObject RootObject { get; set; }
        
        public string FileName { get; set; }

        public Header(string fileName)
        {
            FileName = fileName;
        }

        public override string ToString()
        {
            return $"Csv Meta Model \n FileName: {FileName} \n {RootObject.ToString()}";
        }
    }
}