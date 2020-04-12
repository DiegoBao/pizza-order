﻿using GraphQL.Types;
using PizzaOrder.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaOrder.GraphQLModels.InputTypes
{
    public class OrderDetailsInputType : InputObjectGraphType<OrderDetailsModel>
    {
        public OrderDetailsInputType()
        {
            Name = nameof(OrderDetailsInputType);

            Field(x => x.AddressLine1);
            Field(x => x.AddressLine2, nullable: true);
            Field(x => x.MobileNo);
            Field(x => x.Amount);

            Field<ListGraphType<PizzaDetailsInputType>>("pizzaDetails");

        }
    }
}
