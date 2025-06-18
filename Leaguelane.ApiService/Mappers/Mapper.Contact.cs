using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.Api.Mappers
{
    public static class ContactMapper
    {
        public static ContactDto MapContactToDto(Contact contact)
        {
            return new ContactDto
            {
                Address = contact.Address,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                ImageUrl = contact.ImageUrl,
                MapEmbedUrl = contact.MapEmbedUrl
            };
        }

        public static Contact MapContactDtoToContact(ContactDto contactDto)
        {
            return new Contact
            {
                Address = contactDto.Address,
                Email = contactDto.Email,
                PhoneNumber = contactDto.PhoneNumber,
                ImageUrl = contactDto.ImageUrl,
                MapEmbedUrl = contactDto.MapEmbedUrl
            };
        }
    }
}
