using Microsoft.EntityFrameworkCore;
using PizzaOrder.Data;
using PizzaOrder.Data.Entities;
using PizzaOrder.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrder.Business.Services
{
    public interface IOrderDetailsService
    {
        Task<OrderDetails> CreateAsync(OrderDetails orderDetails);
        Task<IEnumerable<OrderDetails>> GetAllNewOrdersAsync();
        Task<OrderDetails> GetOrderDetailsAsync(int orderId);
        Task<OrderDetails> UpdateStatusAsync(int orderId, OrderStatus orderStatus);
    }

    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly PizzaDBContext dBContext;
        private readonly IEventService eventService;

        public OrderDetailsService(PizzaDBContext dBContext, IEventService eventService)
        {
            this.dBContext = dBContext;
            this.eventService = eventService;
        }

        public async Task<IEnumerable<OrderDetails>> GetAllNewOrdersAsync()
        {
            return await dBContext.OrderDetails
                .Where(x => x.OrderStatus == Data.Enums.OrderStatus.Created)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<OrderDetails> GetOrderDetailsAsync(int orderId)
        {
            return await dBContext.OrderDetails
                .FindAsync(orderId).ConfigureAwait(false);
        }

        public async Task<OrderDetails> CreateAsync(OrderDetails orderDetails)
        {
            dBContext.OrderDetails.Add(orderDetails);
            await dBContext.SaveChangesAsync().ConfigureAwait(false);
            eventService.CreateOrderEvent(new Models.EventDataModel(orderDetails.Id));
            return orderDetails;
        }

        public async Task<OrderDetails> UpdateStatusAsync(int orderId, OrderStatus orderStatus)
        {
            var orderDetails = await dBContext.OrderDetails.FindAsync(orderId);

            if (orderDetails != null)
            {
                orderDetails.OrderStatus = orderStatus;
                dBContext.Update(orderDetails);
                await dBContext.SaveChangesAsync();
                eventService.StatusUpdateEvent(new Models.EventDataModel(orderDetails.Id, orderDetails.OrderStatus));
            }

            return orderDetails;
        }
    }
}
