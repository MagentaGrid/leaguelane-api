using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Context;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record UpdateContactCommand(ContactDto contact) : IRequest<ContactsResponse>;
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, ContactsResponse>
    {
        private readonly LeaguelaneDbContext _context;
        private readonly IContactService _contactService;
        public UpdateContactCommandHandler(LeaguelaneDbContext context, IContactService contactService)
        {
            _context = context;
            _contactService = contactService;
        }

        public async Task<ContactsResponse> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contact = await _contactService.GetContactDetails(cancellationToken);

                if (contact == null)
                {
                    return new ContactsResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "Contact not found",
                        Contacts = null
                    };
                }

                contact.Email = request.contact.Email;
                contact.Address = request.contact.Address;
                contact.Updated = DateTime.UtcNow;
                contact.PhoneNumber = request.contact.PhoneNumber;
                contact.MapEmbedUrl = request.contact.MapEmbedUrl;
                contact.ImageUrl = request.contact.ImageUrl;

                var result = await _contactService.UpdateContactDetails(contact, cancellationToken);

                return new ContactsResponse
                {
                    IsSuccess = true,
                    Contacts = request.contact,
                    ErrorMessage = "Contact updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new ContactsResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    Contacts = null
                };
            }
        }
    }
}
