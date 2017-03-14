using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using refactor_me.Models;

namespace refactor_me.Api
{
    interface IProductOptionService
    {
        ProductOption GetOption(Guid productId, Guid id);

        ProductOptions GetOptions(Guid productId);

        void CreateOption(Guid id, ProductOption option);

        void UpdateOption(Guid id, ProductOption option);

        void DeleteOption(Guid id);
    }
}
