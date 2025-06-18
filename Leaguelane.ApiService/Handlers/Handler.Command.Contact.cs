using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record CreateContactCommand(ContactDto contact) : IRequest<ContactsResponse>;
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ContactsResponse>
    {
        private readonly LeaguelaneDbContext _context;
        private readonly IContactService _contactService;
        public CreateContactCommandHandler(LeaguelaneDbContext context, IContactService contactService)
        {
            _context = context;
            _contactService = contactService;
        }

        public async Task<ContactsResponse> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contact = await _contactService.GetContactDetails(cancellationToken);
                
                if (contact != null)
                {
                    return new ContactsResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "Contact already exists",
                        Contacts = null
                    };
                }

                var result = await _contactService.CreateContactDetails(ContactMapper.MapContactDtoToContact(request.contact), cancellationToken);

                return new ContactsResponse
                {
                    IsSuccess = true,
                    Contacts = ContactMapper.MapContactToDto(result),
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
