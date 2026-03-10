using AspWebApi;
using Microsoft.EntityFrameworkCore;
using WebAppGeneratorId.Data;
using WebAppGeneratorId.Data.Model;
using WebAppGeneratorId.Dto;

namespace WebAppGeneratorId.Services
{
    public class PersonService : IPersonService
    {
        private readonly PersonDbContext  _context;
        public PersonService( PersonDbContext context)
        {
            _context = context;
        }
        public async Task<PersonOutput> AddPerson(PersonInput personInput)
        {
            var _person = GetPerson(personInput);
            _context.persons.Add(_person);
             await _context.SaveChangesAsync();
            return GetPersonOutput(_person);

        }

        public async Task<bool> DeletePerson(string id)
        {
            var _result = await _context.persons
                .Where(p => p.DisplayId == id).ExecuteDeleteAsync();
            return _result > 0;
        }

        public async Task<List<PersonOutput>> GetpersonAll()
        {
            var _persons = ( await _context.persons
                     .ToListAsync()).Select(p=> GetPersonOutput(p)).ToList();
             return _persons;
        }

        public async Task<PersonOutput> GetpersonById(string id)
        {
           var _person = await _context.persons.FirstOrDefaultAsync(p => p.DisplayId == id);
            if (_person is null) return null;
              return GetPersonOutput(_person);
        }

        public  async Task<bool> UpdatePerson(string id, PersonInput personInput)
        {
            var _result =  await _context.persons
                .Where(p => p.DisplayId == id).ExecuteUpdateAsync(p =>
                p.SetProperty(p => p.Name, personInput.Name)
                .SetProperty(p=>p.LastName, personInput.LastName)
                .SetProperty(p=>p.Adresse, personInput.Adresse));
            return _result > 0;
            
            
        }

        private Person GetPerson( PersonInput personInput)
        {
            return new Person
            {
                Name = personInput.Name,
                LastName = personInput.LastName,
                Adresse = personInput.Adresse,
                Date = personInput.Birthday,
                DisplayId = DisplayIdGenerator.GenerateDisplayId("Pe")
            };

        }


        private PersonOutput GetPersonOutput(Person person)
        {
            return new PersonOutput
                (
                   
                   person.DisplayId,$"{person.Name} {person.LastName}",
                   person.Date = person.Date == null ? null : person.Date,
                   person.Adresse
                );
        }
    }
}
