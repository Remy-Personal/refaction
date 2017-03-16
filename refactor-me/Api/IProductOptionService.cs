using System;
using System.Collections.Generic;
using refactor_me.Models;

namespace refactor_me.Api
{
    public interface IProductOptionsService
    {
        ProductOption GetOption(Guid productId, Guid id);

        List<ProductOption> GetOptions(Guid productId);

        void CreateOption(Guid id, ProductOption option);

        void UpdateOption(Guid id, ProductOption option);

        void DeleteOption(Guid id);
    }
}
