using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContex;
        private IOwnerRepository _owner;
        private IAccountRepository _account;
         
        public IOwnerRepository Owner 
        { 
            get
            {
                if(_owner == null)
                {
                    _owner = new OwnerRepository(_repoContex);
                }
                return _owner;
            }
        }
        public IAccountRepository Account
        {
            get
            {
                if (_account == null)
                {
                    _account = new AccountRepository(_repoContex);
                }
                return _account;
            }
        }

        public RepositoryWrapper(RepositoryContext repoContex)
        {
            _repoContex = repoContex;
        }
        public void Save()
        {
            _repoContex.SaveChanges();
        }
    }
}
