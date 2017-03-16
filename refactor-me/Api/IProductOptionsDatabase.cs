﻿using System;
using System.Collections.Generic;
using refactor_me.Models;

namespace refactor_me.Api
{
    public interface IProductOptionsDatabase
    {
        ProductOption Get(Guid id);

        void Save(ProductOption productOption);

        void Delete(Guid id);

        List<ProductOption> GetProductOptions(Guid productId);

        void DeleteOptionsGivenProductId(Guid productId);

    }
}
