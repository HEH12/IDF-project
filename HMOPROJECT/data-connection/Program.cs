using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_connection
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("enter new user-1 \nview table-2\nExit-3");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case ("1"):
                        AddUser();
                        break;
                    case ("2"):
                        showTable();
                        break;
                    case ("3"):
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
                Console.ReadLine();
                Console.Clear();
            }

        }
        static void showTable()
        {
            foreach (var item in DataAccess.getUSERES_HMO())
            {
                Console.WriteLine(item.ID + " | " + item.First_name + " " + item.Last_name + " | " + item.City + " " + item.Street + " " + item.building_number + " | " + item.cellphone + " | " + item.phone);
            }
        }
        static void AddUser()
        {
            Console.WriteLine("ID: ");
            string MyID = Console.ReadLine();
            Console.WriteLine("First Name: ");
            var Myfirst_name = Console.ReadLine();
            Console.WriteLine("Last Name: ");
            var Mylast_name = Console.ReadLine();
            Console.WriteLine("dd/MM/yyyy: ");
            string inputDate = Console.ReadLine();
            DateTime MyBirth_date;
            if (!DateTime.TryParseExact(inputDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out MyBirth_date))
            {
                Console.WriteLine("invalid date");
            }
            Console.WriteLine("City: ");
            var MyCity = Console.ReadLine();
            Console.WriteLine("Street: ");
            var MyStreet = Console.ReadLine();
            Console.WriteLine("building number: ");
            int Mybuilding_number = int.Parse(Console.ReadLine());
            Console.WriteLine("cellphone: ");
            var Mycellphone = Console.ReadLine();
            Console.WriteLine("phone: ");
            var Myphone = Console.ReadLine();

            DataAccess.addUser(MyID, Myfirst_name, Mylast_name, MyBirth_date, MyCity, MyStreet, Mybuilding_number, Mycellphone, Myphone);
        }
    }
}
