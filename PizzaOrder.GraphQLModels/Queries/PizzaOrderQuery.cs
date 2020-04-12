using GraphQL;
using GraphQL.Types;
using PizzaOrder.Business.Services;
using PizzaOrder.GraphQLModels.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaOrder.GraphQLModels.Queries
{
    public class PizzaOrderQuery : ObjectGraphType
    {
        public PizzaOrderQuery(IOrderDetailsService orderDetailsService, IPizzaDetailsService pizzaDetailsService)
        {
            Name = nameof(PizzaOrderQuery);

            FieldAsync<ListGraphType<OrderDetailsType>>(
                name: "newOrders",
                resolve: async context => await orderDetailsService.GetAllNewOrdersAsync());

            FieldAsync<PizzaDetailsType>(
                name: "pizzaDetails",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: async context => await pizzaDetailsService.GetPizzaDetailsAsync(context.GetArgument<int>("id")));

            FieldAsync<OrderDetailsType>(
                name: "orderDetails",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: async context => await orderDetailsService.GetOrderDetailsAsync(context.GetArgument<int>("id")));
        }
    }
}
