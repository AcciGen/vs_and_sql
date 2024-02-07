using Npgsql;
class Program
{
    static void Main()
    {
        Console.Write("Enter table name >> ");
        string tableName = Console.ReadLine()!;
        CreateTable(tableName);

        string fullname;
        short age;
        bool row = true;
        while (row)
        {
            Console.Write("Enter fullname >> ");
            fullname = Console.ReadLine()!;

            Console.Write("Enter age >> ");
            age = short.Parse(Console.ReadLine()!);

            Insert(tableName, fullname, age);

            Console.Write("One more insertion? (Y/n)");
            if (Console.ReadLine()!.ToUpper() == "N")
            {
                row = false;
            }
        }
        GetAll(tableName);

        string oldname;
        row = true;
        while (row)
        {
            Console.Write("Enter old fullname >> ");
            oldname = Console.ReadLine()!;

            Console.Write("Enter new fullname >> ");
            fullname = Console.ReadLine()!;

            Console.Write("Enter new age >> ");
            age = short.Parse(Console.ReadLine()!);

            Update(tableName, oldname, fullname, age);

            Console.Write("One more update? (Y/n)");
            if (Console.ReadLine()!.ToUpper() == "N")
            {
                row = false;
            }
        }
        GetAll(tableName);

        row = true;
        while (row)
        {
            Console.Write("Enter fullname >> ");
            fullname = Console.ReadLine()!;

            Delete(tableName, fullname);

            Console.Write("One more deletion? (Y/n)");
            if (Console.ReadLine()!.ToUpper() == "N")
            {
                row = false;
            }
        }
        GetAll(tableName);

    }

    public static void GetAll(string tableName)
    {
        string connectionString = "Host=localhost; Port=5432; Database=Demo; User Id=postgres; Password=135;";
    
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
    
            string query = $"select * from {tableName};";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
    
            var result = cmd.ExecuteReader();
    
            while (result.Read())
            {
                Console.Write($"{result[0]} {result[1]} {result[2]}");
            }
        }
    }

    public static void CreateTable(string tableName)
    {
        string connectionString = "Host=localhost; Port=5432; Database=Demo; User Id=postgres; Password=135;";

        using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();

        string query = $"create table if not exists {tableName}(id bigserial primary key, fullname varchar(40), age smallint, job varchar(50));";
        using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

        var countRow = cmd.ExecuteNonQuery();

        Console.WriteLine("Table was created successfully!");
    }

    public static void Insert(string tableName, string fullname, short age)
    {
        string connectionString = "Host=localhost; Port=5432; Database=Demo; User Id=postgres; Password=135;";
    
        using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
    
        string query = $"insert into {tableName}(fullname, age) values('{fullname}', '{age}');";
        using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
    
        var countRow = cmd.ExecuteNonQuery();
    
        Console.WriteLine(countRow + " inserted successfully!");
    }

    public static void Update(string tableName, string oldName, string fullname, short age)
    {
        string connectionString = "Host=localhost; Port=5432; Database=Demo; User Id=postgres; Password=135;";

        using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();

        string query = $"update {tableName} set fullname = '{fullname}', age = {age} where fullname = '{oldName}';";
        using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

        var countRow = cmd.ExecuteNonQuery();

        Console.WriteLine(countRow + " updated successfully!");
    }

    public static void Delete(string tableName, string fullname)
    {
        string connectionString = "Host=localhost; Port=5432; Database=Demo; User Id=postgres; Password=135;";
    
        using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
    
        string query = $"delete from {tableName} where fullname = '{fullname}';";
        using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
    
        var rowCount = cmd.ExecuteNonQuery();
    
        Console.WriteLine(rowCount + " deleted successfully!");
    }
}