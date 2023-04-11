using System.Data.SqlClient;

public class Customer
{
    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public int Age { get; set; }
    public string Email { get; set; }
    public Customer()
    {
        Age = 0;
        Email = "";
    }
}

public class CustomerRepository
{
    public void Save(Customer customer)
    {
        string connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
        string query = "INSERT INTO Customers (Name, Age, Email) VALUES ('" + customer.Name + "', " + customer.Age + ", '" + customer.Email + "')";
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(query, connection);
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void GetCustomer(string name)
    {
        Customer customer;
        string connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
        string query = "SELECT * FROM Customers WHERE Name = '" + name;
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(query, connection);
        connection.Open();
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            customer.CustomerId = reader.GetInt32(0);
            customer.Name = reader.GetString(1);
            customer.Age = reader.GetInt32(2);
            customer.Email = reader.GetString(3);
        }
        connection.Close();
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Customer customer = new Customer();
        Console.WriteLine("Customer Name:");
        customer.Name = Console.ReadLine();
        Console.WriteLine("Email:");
        customer.Email = Console.ReadLine();

        CustomerRepository customerRepository = new CustomerRepository();
        customerRepository.Save(customer);
        var savedCustomer = customerRepository.GetCustomer(customer.Name);
        Console.WriteLine(customer);
    }
}