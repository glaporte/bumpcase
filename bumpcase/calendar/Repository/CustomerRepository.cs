using calendar.Context;
using calendar.Entites;

namespace calendar.Repository
{
    public class CustomerRepository
    {
        public CustomerRepository()
        {
            using (var context = new CustomerContext())
            {
                var customers = new List<Customer>
                {
                    new Customer
                    {
                        Firstname ="customer 1",
                        Lastname ="name",
                        Address = "0 street na",
                    },
                    new Customer
                    {
                        Firstname ="customer 2",
                        Lastname ="bla",
                        Address = "2 street na",
                    },
                };

                context.Customers.AddRange(customers);
                context.SaveChanges();
            }
        }

        public List<Customer> GetCustomers()
        {
            using (var context = new CustomerContext())
            {
                var list = context.Customers
                    .ToList();
                return list;
            }
        }

        public Customer? GetCustomer(int id)
        {
            using (var context = new CustomerContext())
            {
                return context.Customers.Find(id);
            }
        }

        public Customer AddCustomer(Customer customer) 
        {
            using (var context = new CustomerContext())
            {
                context.Customers.Add(customer);
                context.SaveChanges();
            }
            return customer;
        }
    }
}
