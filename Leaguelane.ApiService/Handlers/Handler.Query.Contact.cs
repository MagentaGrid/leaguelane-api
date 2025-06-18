using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public class GetContactsQuery() : IRequest<ContactsResponse>;
    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, ContactsResponse>
    {
        public readonly IContactService _contactService;

        public GetContactsQueryHandler(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<ContactsResponse> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _contactService.GetContactDetails(cancellationToken);
                if (result == null)
                {
                    return new ContactsResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "Contacts not found",
                        Contacts = null
                    };
                }

                return new ContactsResponse
                {
                    IsSuccess = true,
                    ErrorMessage = null,
                    Contacts = ContactMapper.MapContactToDto(result)
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
