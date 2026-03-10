using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAppGeneratorId.Dto;
using WebAppGeneratorId.Services;

namespace WebAppGeneratorId.EndPoints
{
    public static class PersonEndPoints
    {
        public static IServiceCollection MapServicePersons(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            return services;
        }
        public static RouteGroupBuilder MapPersonEndPoints(this RouteGroupBuilder groupe)
        {
            groupe.MapGet("", GetAllPersonne);
            groupe.MapGet("/{id}", GetPersonneByID);
            groupe.MapPost("", AddPerson);
            groupe.MapPut("/{id}", UpdatePerson);
            groupe.MapDelete("/{id}", DeletePerson);
            return groupe;
        }

        /// <summary>
        /// Elle permet renvoie la liste des personne
        /// </summary>;
        /// <param name="_service"></param>
        /// <returns></returns>
        private static async Task<IResult> GetAllPersonne
            (
               [FromServices] IPersonService _service
            )
        {
            var _personne = await _service.GetpersonAll();
            return Results.Ok(_personne);
        }

        private static async Task<IResult> GetPersonneByID
            (
             [FromServices] IPersonService _service,
             [FromRoute] string id

            )
        {
            var _personne = await _service.GetpersonById(id);
            return Results.Ok(_personne);
        }

        private static async Task<IResult> AddPerson
            (
               [FromBody] PersonInput personInput,
               [FromServices] IPersonService _service,
               [FromServices] IValidator<PersonInput> _validator
            )
        {
            var _result = _validator.Validate(personInput);
            if (!_result.IsValid)
            {
                return Results.BadRequest( _result.Errors.Select(e => new
                {
                        e.PropertyName,
                        e.ErrorMessage
                }));
            }
            var _person = await _service.AddPerson(personInput);
            return Results.Ok(_person);
        }

        private static async Task<IResult> UpdatePerson
            (
               [FromRoute] string id,
               [FromBody] PersonInput personInput,
               [FromServices] IPersonService _service,
               [FromServices ] IValidator<PersonInput> _validator
            )
        {
            var _result = _validator.Validate(personInput);
            if (!_result.IsValid)
            {
                 return Results.BadRequest( _result.Errors.Select(e => new
                {
                    e.PropertyName,
                    e.ErrorMessage
                }));
            }
            var _IsUpdate =  await _service.UpdatePerson(id, personInput);

            return Results.Ok(_IsUpdate);   
        }

        private static async Task<IResult> DeletePerson
            ( 
                [FromRoute] string id,
                [FromServices] IPersonService _service
            )
        {
           var _result =  await _service.DeletePerson(id);
            return Results.Ok(_result);
        }
    }
}
