using Project.Models.Domain.OrderManagement.Clients;

namespace Project.Models.Domain.OrderManagement.Orders
{
    public class Order
    {
        private OrderId id;
        private OrderQuantity quantity;
        private Client client;
    }
}