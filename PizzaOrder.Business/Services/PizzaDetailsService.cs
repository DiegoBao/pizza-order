using PizzaOrder.Data;
using PizzaOrder.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrder.Business.Services
{
    public interface IPizzaDetailsService
    {
        Task<IEnumerable<PizzaDetails>> CreateBulkAsync(IEnumerable<PizzaDetails> pizzaDetails);
        Task<int> DeletePizzaDetailsAsync(int pizzaDetailsId);
        IEnumerable<PizzaDetails> GetAllPizzaDetailsForOrder(int orderId);
        Task<PizzaDetails> GetPizzaDetailsAsync(int pizzaDetailsId);
    }

    public class PizzaDetailsService : IPizzaDetailsService
    {
        private readonly PizzaDBContext dBContext;

        public PizzaDetailsService(PizzaDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<PizzaDetails> GetPizzaDetailsAsync(int pizzaDetailsId)
        {
            return await dBContext.PizzaDetails
                .FindAsync(pizzaDetailsId).ConfigureAwait(false);
        }

        public IEnumerable<PizzaDetails> GetAllPizzaDetailsForOrder(int orderId)
        {
            return dBContext.PizzaDetails.Where(p => p.OrderDetailsId == orderId).ToList();
        }

        public async Task<IEnumerable<PizzaDetails>> CreateBulkAsync(IEnumerable<PizzaDetails> pizzaDetails)
        {
            dBContext.PizzaDetails.AddRange(pizzaDetails);
            await dBContext.SaveChangesAsync();
            return pizzaDetails;
        }

        public async Task<int> DeletePizzaDetailsAsync(int pizzaDetailsId)
        {
            var pizzaDetails = await dBContext.PizzaDetails.FindAsync(pizzaDetailsId);
            if (pizzaDetails != null)
            {
                int orderid = pizzaDetails.OrderDetailsId;
                dBContext.PizzaDetails.Remove(pizzaDetails);
                await dBContext.SaveChangesAsync();
                return orderid;
            }

            return 0;
        }
    }
}
