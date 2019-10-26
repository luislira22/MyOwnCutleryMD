using System;

namespace MasterDataProduct.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}