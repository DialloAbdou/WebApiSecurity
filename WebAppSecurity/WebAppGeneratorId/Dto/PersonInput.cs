namespace WebAppGeneratorId.Dto
{
    public class PersonInput
    {
        public string Name { get; set; } = string.Empty; 
        public string LastName { get; set; } = string.Empty;
        public DateTime ? Birthday { get; set; }
        public string Adresse { get; set; } = string.Empty;
    }
}
