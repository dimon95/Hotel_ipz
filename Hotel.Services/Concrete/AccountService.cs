using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hotel.Model.Entities.Abstract;
using Hotel.Dto;
using Hotel.Services.Abstract;
using Hotel.Repository.Abstract;
using Hotel.Model.Entities.Concrete;

namespace Hotel.Services.Concrete
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepo;
       // private IBookingHolderRepository _bhRepo;
        
        public AccountService ( IAccountRepository aRepo)
        {
            _accountRepo = aRepo;
        }

        public void ChangeEmail ( Guid id, string email )
        {
            _accountRepo.StartTransaction();

            Account ac = ServiceUtils.GetEntity( _accountRepo, id );

            ac.Email = email;

            _accountRepo.Commit();
        }

        public void ChangeName (Guid id, string name, string surname, string middlename )
        {
            _accountRepo.StartTransaction();

            Account ac = ServiceUtils.GetEntity(_accountRepo, id);

            ac.Name = name;
            ac.Surname = surname;
            ac.Middlename = middlename;

            _accountRepo.Commit();
        }

        public void ChangePassword ( Guid id, string oldPasswordHash, string newPasswordHash )
        {
            _accountRepo.StartTransaction();

            Account ac = ServiceUtils.GetEntity(_accountRepo, id);

            if ( ac.PasswordHash != oldPasswordHash )
                throw new ArgumentException("wrong password");

            ac.PasswordHash = newPasswordHash;

            _accountRepo.Commit();
        }

        public Guid CreateAdmin ( string name, string surname, string middlename, 
                                    byte day, byte month, int year, 
                                    string email, string passwordHash )
        {
            if ( _accountRepo.Find( email ) != null )
                throw new ArgumentException( "Account alredy exists" );

            _accountRepo.StartTransaction();

            Admin ad = new Admin( Guid.NewGuid(), name, surname, middlename, email, passwordHash, 
                new Utils.DateOfBirth( day, month, year ) );

            _accountRepo.Add( ad );

            _accountRepo.Commit();

            return ad.Id;
        }

        public Guid CreateClient ( string name, string surname, string middlename, 
                                        byte day, byte month, int year, string email, string passwordHash )
        {
            if ( _accountRepo.Find( email ) != null )
                throw new ArgumentException( "Account alredy exists" );

            _accountRepo.StartTransaction();

            Client cl = new Client( Guid.NewGuid(), name, surname, middlename, email, passwordHash,
                new Utils.DateOfBirth( day, month, year ) );

            _accountRepo.Add( cl );

            _accountRepo.Commit();

            return cl.Id;
        }

        public BookingHolderDto GetCartContent ( Guid userId )
        {
            Account ac = ServiceUtils.GetEntity(_accountRepo, userId);

            return ac.Cart.toDto();
        }

        public BookingHolderDto GetHistoryContent ( Guid userId )
        {
            Account ac = ServiceUtils.GetEntity(_accountRepo, userId);

            return ac.History.toDto();
        }

        public AccountDto Indentify ( string password, string email )
        {
            Account ac = _accountRepo.Find(email, password);

            if ( ac == null )
                return null;

            return ac.toDto();
        }

        public void OnPaymentMade ( Guid userId )
        {
            Account acc = ServiceUtils.GetEntity(_accountRepo, userId);

            _accountRepo.StartTransaction();

            acc.PaymentMade();

            _accountRepo.Commit();
        }

        public AccountDto View ( Guid id )
        {
            return ServiceUtils.GetEntity( _accountRepo, id ).toDto();
        }

        public IList<Guid> ViewAll ()
        {
            return _accountRepo.SelectAllDomainIds().ToList();
        }
    }
}
