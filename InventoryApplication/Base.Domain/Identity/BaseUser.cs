using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Base.Domain.Identity;

public class BaseUser : BaseUser<Guid>
{
}

public class BaseUser<TKey> : IdentityUser<TKey>, IDomainEntityId<TKey> where TKey : IEquatable<TKey>
{
    public BaseUser()
    {
    }

    public BaseUser(string userName) : base(userName)
    {
    }
}