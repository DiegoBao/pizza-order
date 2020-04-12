using GraphQL.Types;
using PizzaOrder.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaOrder.GraphQLModels.Enums
{
    public class OrderStatusEnumType: EnumerationGraphType
    {
        public OrderStatusEnumType()
        {
            Name = "orderStatus";

            AddValue(OrderStatus.Created.ToString(), "Order was created.", (int)OrderStatus.Created);
            AddValue(OrderStatus.InKitchen.ToString(), "Order is preparing.", (int)OrderStatus.InKitchen);
            AddValue(OrderStatus.OnTheWay.ToString(), "Order is on the way.", (int)OrderStatus.OnTheWay);
            AddValue(OrderStatus.Delivered.ToString(), "Order was delivered.", (int)OrderStatus.Delivered);
            AddValue(OrderStatus.Canceled.ToString(), "Order was canceled.", (int)OrderStatus.Canceled);
        }
    }
}
