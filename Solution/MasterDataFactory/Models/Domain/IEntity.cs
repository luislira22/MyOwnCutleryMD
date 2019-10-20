using System;

namespace MasterDataFactory.Models.Domain
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}