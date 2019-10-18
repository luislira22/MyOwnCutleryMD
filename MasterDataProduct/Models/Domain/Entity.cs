using System;

namespace MasterDataProduct.Models.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}