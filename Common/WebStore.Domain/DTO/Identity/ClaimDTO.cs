using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace WebStore.Domain.DTO.Identity
{
    public abstract class ClaimDTO : UserDTO
    {
        public IEnumerable<Claim> Claims { get; set; }
    }

    public class AddClaimDTO : ClaimDTO { }
    public class RemoveClaimDTO : ClaimDTO { }
    public class ReplaceClaimDTO : ClaimDTO
    {
        public Claim claim { get; set; }
        public Claim newClaim { get; set; }
    }
}
