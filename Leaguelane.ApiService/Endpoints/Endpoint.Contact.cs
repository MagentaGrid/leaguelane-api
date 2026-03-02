using Leaguelane.Api.Handlers;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Leaguelane.Api.Endpoints
{
    public static class ContactEndpoint
    {
        public static RouteGroupBuilder AddContactRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetContacts).WithName("contacts").Produces<object>(StatusCodes.Status200OK);

            group.MapPost("", CreateContact).WithName("contact-create").Produces<object>(StatusCodes.Status200OK)
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPut("", UpdateContact).WithName("contact-update").Produces<object>(StatusCodes.Status200OK)
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            return group;
        }

        /// <summary>
        /// Get contacts.
        /// Returns a list of contact entries.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> GetContacts(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetContactsQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Create contact.
        /// Creates a new contact entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="contact"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> CreateContact(ISender sender, [FromBody] ContactDto contact, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CreateContactCommand(contact), cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Update contact.
        /// Updates an existing contact entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="contact"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateContact(ISender sender, [FromBody] ContactDto contact, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateContactCommand(contact), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
