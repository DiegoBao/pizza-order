using GraphQL.Types;
using PizzaOrder.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaOrder.GraphQLModels.Enums
{
    public class ToppingEnumType: EnumerationGraphType<Toppings>
    {
        public ToppingEnumType()
        {
            Name = nameof(ToppingEnumType);
        }
    }
}
