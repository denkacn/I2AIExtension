namespace I2AIExtension.Editor.Models
{
    public class TranslatedPromtData
    {
        public string Term { get; set; }
        public string Language { get; set; }
        public string From { get; set; }
        public string Promt { get; set; }
        
        public TranslatedPromtData(){}

        public TranslatedPromtData(string term, string language, string from, string promt)
        {
            Term = term;
            Language = language;
            From = from;
            Promt = promt;
        }
    }
}