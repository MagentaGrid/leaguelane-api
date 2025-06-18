using Leaguelane.Api.Handlers;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.Api.Endpoints
{
    public static class ContactEndpoint
    {
        public static RouteGroupBuilder AddContactRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetContacts).WithName("contacts");

            group.MapPost("", CreateContact).WithName("contact-create")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPut("", UpdateContact).WithName("contact-update")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            return group;
        }

        public static async Task<IResult> GetContacts(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetContactsQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> CreateContact(ISender sender, [FromBody] ContactDto contact, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CreateContactCommand(contact), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> UpdateContact(ISender sender, [FromBody] ContactDto contact, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateContactCommand(contact), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
