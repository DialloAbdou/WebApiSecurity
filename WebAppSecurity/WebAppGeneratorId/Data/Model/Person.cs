namespace WebAppGeneratorId.Data.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime ? Date  { get; set; }
        public string Adresse { get; set; } = string.Empty;
        public string DisplayId { get; set; } = string.Empty;
    }
}
