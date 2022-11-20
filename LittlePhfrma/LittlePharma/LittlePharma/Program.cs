using System.Data.SqlClient;
class Program
{
    static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=LittlePharma;Integrated Security=True";
    static void Main()
    {
        ReadCommand();
    }

    private static void ReadCommand()
    {
        Console.WriteLine("Выберите необходимое действие:");
        Console.WriteLine("1. Создать товар");
        Console.WriteLine("2. Удалить товар");
        Console.WriteLine("3. Создать аптеку");
        Console.WriteLine("4. Удалить аптеку");
        Console.WriteLine("5. Создать склад");
        Console.WriteLine("6. Удалить склад");
        Console.WriteLine("7. Создать партию");
        Console.WriteLine("8. Удалить партию");
        Console.WriteLine("9. Вывести список товаров в выбранной аптеке");
        Console.WriteLine("Введите номер необходимого действия:");

        var action = Console.ReadLine();

        SelectAction(action);

    }

    private static void SelectAction(string action)
    {
        if (action == "1")
        { AddProduct(); }
        else if (action == "2")
        { DelProduct(); }
        else if (action == "3")
        { AddPharmacy(); }
        else if (action == "4")
        { DelPharmacy(); }
        else if (action == "5")
        { AddStock(); }
        else if (action == "6")
        { DelStock(); }
        else if (action == "7")
        { AddSupplying(); }
        else if (action == "8")
        { DelSupplying(); }
        else if (action == "9")
        { GetProducts(); }
        else
        { 
            Console.WriteLine("Неверно выбрано действие"); 
            ReadCommand();
        }
    }

    private static void AddProduct()
    {
        Console.WriteLine("Введите наименование товара");

        var product = Console.ReadLine();

        string sqlExpression = "AddProduct";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter productParam = new SqlParameter
            {
                ParameterName = "@name",
                Value = product
            };
            command.Parameters.Add(productParam);

            var result = command.ExecuteScalar();

            Console.WriteLine("Id добавленного объекта: {0}", result);
        }
        ReadCommand();
    }

    private static void DelProduct()
    {
        var sqlExpression = "GetProduct";

        ReadData(sqlExpression);

        Console.WriteLine("Введите ID товара");

        var id = Console.ReadLine();

        sqlExpression = "DelProduct";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter idParam = new SqlParameter
            {
                ParameterName = "@Id",
                Value = id
            };
            command.Parameters.Add(idParam);

            var result = command.ExecuteScalar();

            Console.WriteLine("Удален товар и все партии во всех аптеках, связанные с этим товаром");
        }
        ReadCommand();
    }

    private static void AddPharmacy()
    {
        Console.WriteLine("Введите наименование аптеки");
        var pharmacy = Console.ReadLine();
        Console.WriteLine("Введите адрес аптеки");
        var address = Console.ReadLine();
        Console.WriteLine("Введите телефон аптеки");
        var phone = Console.ReadLine();

        string sqlExpression = "AddPharmacy";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter pharmacyParam = new SqlParameter
            {
                ParameterName = "@name",
                Value = pharmacy
            };
            command.Parameters.Add(pharmacyParam);

            SqlParameter addressParam = new SqlParameter
            {
                ParameterName = "@address",
                Value = address
            };
            command.Parameters.Add(addressParam);

            SqlParameter phoneParam = new SqlParameter
            {
                ParameterName = "@phone",
                Value = phone
            };
            command.Parameters.Add(phoneParam);

            var result = command.ExecuteScalar();

            Console.WriteLine("Id добавленного объекта: {0}", result);
        }
        ReadCommand();
    }

    private static void DelPharmacy()
    {
        var sqlExpression = "GetPharmacy";

        ReadData(sqlExpression);

        Console.WriteLine("Введите ID аптеки");

        var id = Console.ReadLine();

        sqlExpression = "DelPharmacy";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter idParam = new SqlParameter
            {
                ParameterName = "@Id",
                Value = id
            };
            command.Parameters.Add(idParam);

            var result = command.ExecuteScalar();

            Console.WriteLine("Удалена аптека, все склады аптеки и партии в складах");
        }
        ReadCommand();
    }

    private static void AddStock()
    {
        Console.WriteLine("Введите ID аптеки");
        var pharmacyId = Console.ReadLine();
        Console.WriteLine("Введите название склада");
        var name = Console.ReadLine();

        string sqlExpression = "AddStock";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter pharmacyIdParam = new SqlParameter
            {
                ParameterName = "@pharmacyId",
                Value = pharmacyId
            };
            command.Parameters.Add(pharmacyIdParam);

            SqlParameter nameParam = new SqlParameter
            {
                ParameterName = "@name",
                Value = name
            };
            command.Parameters.Add(nameParam);

            var result = command.ExecuteScalar();

            Console.WriteLine("Id добавленного объекта: {0}", result);
        }
        ReadCommand();
    }

    private static void DelStock()
    {
        string sqlExpression = "GetStock";

        ReadData(sqlExpression);

        Console.WriteLine("Введите ID склада");

        var id = Console.ReadLine();

        sqlExpression = "DelStock";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter idParam = new SqlParameter
            {
                ParameterName = "@Id",
                Value = id
            };
            command.Parameters.Add(idParam);

            var result = command.ExecuteScalar();

            Console.WriteLine("Удален склад, все партии в этом складе");
        }
        ReadCommand();
    }

    private static void AddSupplying()
    {
        Console.WriteLine("Введите ID продукта");
        var productId = Console.ReadLine();
        Console.WriteLine("Введите ID склада");
        var stockId = Console.ReadLine();
        Console.WriteLine("Введите количество");
        var amount = Console.ReadLine();

        string sqlExpression = "AddSupplying";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter productIdParam = new SqlParameter
            {
                ParameterName = "@productId",
                Value = productId
            };
            command.Parameters.Add(productIdParam);

            SqlParameter stockIdParam = new SqlParameter
            {
                ParameterName = "@stockId",
                Value = stockId
            };
            command.Parameters.Add(stockIdParam);

            SqlParameter amountParam = new SqlParameter
            {
                ParameterName = "@amount",
                Value = amount
            };
            command.Parameters.Add(amountParam);

            var result = command.ExecuteScalar();

            Console.WriteLine("Id добавленного объекта: {0}", result);
        }
        ReadCommand();
    }

    private static void DelSupplying()
    {
        string sqlExpression = "GetSupplying";

        ReadData(sqlExpression);

        Console.WriteLine("Введите ID поставки");

        var id = Console.ReadLine();

        sqlExpression = "DelSupplying";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter idParam = new SqlParameter
            {
                ParameterName = "@Id",
                Value = id
            };
            command.Parameters.Add(idParam);

            var result = command.ExecuteScalar();

            Console.WriteLine("Партия удалена");
        }
        ReadCommand();
    }

    private static void GetProducts()
    {
        Console.WriteLine("Введите ID аптеки");

        var id = Console.ReadLine();

        string sqlExpression = "GetProducts";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter pharmacyIdParam = new SqlParameter
            {
                ParameterName = "@pharmacyId",
                Value = id
            };
            command.Parameters.Add(pharmacyIdParam);

            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                Console.WriteLine("{0}\t{1}", reader.GetName(0), reader.GetName(1));

                while (reader.Read())
                {
                    var name = reader.GetString(0);
                    var amount = reader.GetInt32(1);
                    Console.WriteLine("{0} \t{1}", name, amount);
                }
            }
            reader.Close();
        }
        ReadCommand();
    }

    private static void ReadData(string sqlExpression)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                Console.WriteLine("{0}\t{1}", reader.GetName(0), reader.GetName(1));

                while (reader.Read())
                {
                    var ID = reader.GetInt32(0);
                    var Name = reader.GetString(1);
                    Console.WriteLine("{0} \t{1}", ID, Name);
                }
            }
            reader.Close();
        }
    }
}
