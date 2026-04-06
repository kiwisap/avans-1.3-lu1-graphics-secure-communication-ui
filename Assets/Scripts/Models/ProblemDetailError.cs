namespace Assets.Scripts.Models
{
    public class ProblemDetailError
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string Detail { get; set; }
        
        public override string ToString ()
        {
            return $"Error {Status}: {Title} - {Detail}";
        }
    }
}
