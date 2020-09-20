using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntimidatorsManagement_API.Models
{
    public interface IJwtToken
    {
        string CreateToken(ApplicationUser user);
    }
}
