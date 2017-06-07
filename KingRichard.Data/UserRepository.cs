using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace KingRichard.Data
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(DbContext context) : base(context) { }
    }
}
