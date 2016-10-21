using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Dto;

namespace Hotel.Services.Abstract
{
    public interface IAccountService : IDomainEntityService<AccountDto>
    {
        Guid CreateClient ( string name, string surname, string middlename, byte day, byte month, int year,
            string email, string passwordHash );

        Guid CreateAdmin ( string name, string surname, string middlename, byte day, byte month, int year,
            string email, string passwordHash );

        void ChangeEmail ( Guid id, string email );

        void ChangePassword ( Guid id, string oldPasswordHash, string newPasswordHash );

        void ChangeName ( Guid id, string name, string surname, string middlename );

        AccountDto Indentify ( string password, string email );

        BookingHolderDto GetCartContent ( Guid userId );

        BookingHolderDto GetHistoryContent ( Guid userId );

        void OnPaymentMade ( Guid userId );
    }
}
