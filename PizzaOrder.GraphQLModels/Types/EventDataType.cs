﻿using GraphQL.Types;
using PizzaOrder.Business.Models;
using PizzaOrder.GraphQLModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaOrder.GraphQLModels.Types
{
    public class EventDataType : ObjectGraphType<EventDataModel>
    {
        public EventDataType()
        {
            Name = nameof(EventDataType);

            Field(x => x.OrderId);
            Field<OrderStatusEnumType>("orderStatus", resolve: context => context.Source.OrderStatus);
        }
    }
}
