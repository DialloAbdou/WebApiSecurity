using WebAppGeneratorId.Dto;

namespace WebAppGeneratorId.Services
{
    public interface IPersonService
    {
        Task<List<PersonOutput>> GetpersonAll();
        Task<PersonOutput> GetpersonById(string id);
        Task<PersonOutput> AddPerson(PersonInput personInput);
        Task<bool> UpdatePerson( string id, PersonInput personInput ); 
        Task<bool> DeletePerson( string id );

    }
}
