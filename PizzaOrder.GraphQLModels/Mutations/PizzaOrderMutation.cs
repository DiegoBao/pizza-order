using GraphQL;
using GraphQL.Types;

using PizzaOrder.Business.Models;
using PizzaOrder.Business.Services;
using PizzaOrder.Data.Entities;
using PizzaOrder.Data.Enums;
using PizzaOrder.GraphQLModels.Enums;
using PizzaOrder.GraphQLModels.InputTypes;
using PizzaOrder.GraphQLModels.Types;

using System.Linq;

namespace PizzaOrder.GraphQLModels.Mutations
{
    public class PizzaOrderMutation : ObjectGraphType
    {
        public PizzaOrderMutation(IPizzaDetailsService pizzaDetailsService, IOrderDetailsService orderDetailsService)
        {
            Name = nameof(PizzaOrderMutation);

            FieldAsync<OrderDetailsType>(
                name: "createOrder",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<OrderDetailsInputType>> { Name = "orderDetails" }),
                resolve: async context =>
                {
                    OrderDetailsModel order = context.GetArgument<OrderDetailsModel>("orderDetails");

                    var orderDetails = new OrderDetails(order.AddressLine1, order.AddressLine2, order.MobileNo, order.Amount);

                    orderDetails = await orderDetailsService.CreateAsync(orderDetails);

                    System.Collections.Generic.IEnumerable<PizzaDetails> pizzaDetails = order.PizzaDetails.Select(x => new PizzaDetails(x.Name, x.Toppings, x.Price, x.Size, orderDetails.Id));
                    pizzaDetails = await pizzaDetailsService.CreateBulkAsync(pizzaDetails);

                    orderDetails.PizzaDetails = pizzaDetails.ToList();

                    return orderDetails;
                });

            FieldAsync<OrderDetailsType>(
                name: "updateStatus",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<OrderStatusEnumType>> { Name = "orderStatus" }),
                resolve: async context =>
                {
                    var orderId = context.GetArgument<int>("id");
                    OrderStatus orderStatus = context.GetArgument<OrderStatus>("orderStatus");

                    OrderDetails orderDetails = await orderDetailsService.UpdateStatusAsync(orderId, orderStatus);
                    return orderDetails;
                });

            FieldAsync<OrderDetailsType>(
                name: "deletePizzaDetails",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "pizzaDetailsId" }),
                resolve: async context =>
                {
                    int pizzaDetailsId = context.GetArgument<int>("pizzaDetailsId");

                    int orderId = await pizzaDetailsService.DeletePizzaDetailsAsync(pizzaDetailsId);

                    return await orderDetailsService.GetOrderDetailsAsync(orderId);
                });
        }
    }
}
