using GraphQL.Types;

using PizzaOrder.Data.Entities;
using PizzaOrder.GraphQLModels.Enums;

namespace PizzaOrder.GraphQLModels.Types
{
    public class PizzaDetailsType : ObjectGraphType<PizzaDetails>
    {
        public PizzaDetailsType()
        {
            Name = nameof(PizzaDetailsType);

            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.OrderDetailsId);
            Field(x => x.Price);

            Field<StringGraphType>(
                name: "topppings",
                resolve: context => context.Source.Toppings.ToString());

            //Field<ToppingEnumType>(
            //    name: "toppings",
            //    resolve: context => context.Source.Toppings);
        }
    }
}
