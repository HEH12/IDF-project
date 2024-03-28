using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace data_connection
{
    public static class DataAccess
    {
        public static hmoDataBaseEntities hmo_ctx = new hmoDataBaseEntities();
        static int hmo_ctx_countr = 0;

        public static DateTime? NULL { get; private set; }

        public static string addUser(string MyID, string MyFirst_name, string MyLast_name, System.DateTime MyBirth_date, string MyCity, string MyStreet, int MyBuildingNumber, string Mycellphone, string Myphone)
        {

            if (hmo_ctx.USERES_HMO.Any(u => u.ID == MyID))
            {
                return "A user with this ID number is already in the system";
            }
            if (checkFN(MyFirst_name) != " ")
                return checkFN(MyFirst_name);
            if (checkLN(MyLast_name) != " ")
                return checkFN(MyLast_name);
            if (checkCity(MyCity) != " ")
                return checkCity(MyCity);
            if (checkStreet(MyStreet) != " ")
                return checkStreet(MyStreet);
            if (checkBN(MyBuildingNumber) != " ")
                return checkFN(MyFirst_name);
            if (checkphone(Myphone) != " ")
                return checkphone(Myphone);
            if (checkcell(Mycellphone) != " ")
                return checkcell(Mycellphone);

            var user = new USERES_HMO()
            {
                ID = MyID,
                First_name = MyFirst_name,
                Last_name = MyLast_name,
                Birth_date = MyBirth_date,
                City = MyCity,
                Street = MyStreet,
                building_number = MyBuildingNumber,
                cellphone = Mycellphone,
                phone = Myphone
            };
            var Corona = new CoronaDetails()
            {
                ID = MyID,
                Vaccine1Date = null,
                Vaccine2Date = null,
                Vaccine3Date = null,
                Vaccine4Date = null,
                Vaccine1Manufacturer = null,
                Vaccine2Manufacturer = null,
                Vaccine3Manufacturer = null,
                Vaccine4Manufacturer = null,
                PositiveResultDate = null,
                RecoveryDate = null

            };
            hmo_ctx_countr++;
            hmo_ctx.USERES_HMO.Add(user);
            hmo_ctx.CoronaDetails.Add(Corona);
            hmo_ctx.SaveChanges();
            return " ";

        }

       

        public static List<USERES_HMO> getUSERES_HMO()
        {
            return hmo_ctx.USERES_HMO.ToList<USERES_HMO>();
        }
        //public static CoronaDetails getByIdUSERES_HMO(string id)
        //{
        //    return hmo_ctx.CoronaDetails.Find(id);
        //}
        public static string getCor(string id)
        {
            string a = "326133180";
             hmo_ctx.USERES_HMO.Find(a);
            return "pppp";
            
        }
        public static string deleteUser(string MyID)
        {
            Regex idRegex = new Regex(@"^\d{9}$");
            if (!idRegex.IsMatch(MyID))
            {
                return "ID number must be exactly 9 digits";
            }
            var user=hmo_ctx.USERES_HMO.Find(MyID);
            if (user!=null)
            {
                hmo_ctx.USERES_HMO.Remove(user);
                var cor = hmo_ctx.CoronaDetails.Find(MyID);
                hmo_ctx.CoronaDetails.Remove(cor);
                hmo_ctx.SaveChanges();
                return " ";
            }
            else
                return "There is no user with such an ID in the system";
        }
        
            public static string updateUser(string MyID, string MyFirst_name, string MyLast_name, System.DateTime MyBirth_date, string MyCity, string MyStreet, int MyBuildingNumber, string Mycellphone, string Myphone)
        {

            if (!(hmo_ctx.USERES_HMO.Any(u => u.ID == MyID)))
            {
                return "A user with this ID number is not exist in the system";
            }
            USERES_HMO user= hmo_ctx.USERES_HMO.Find(MyID);
            if (MyFirst_name != null)
            {
                string ans = checkFN(MyFirst_name);
                if (ans != " ")
                    return ans;
                else
                    user.First_name = MyFirst_name;
            }
            if(MyLast_name !=null)
            {
                string ans = checkLN(MyLast_name);
                if (ans != " ")
                    return ans;
                else
                    user.Last_name = MyLast_name;
            }
            if(MyBirth_date !=null)
            {
                user.Birth_date = MyBirth_date;
            }
            if (MyCity != null)
            {
                string ans = checkCity(MyCity);
                if (ans != " ")
                    return ans;
                else
                    user.City = MyCity;
            }
            if (MyStreet != null)
            {
                string ans = checkStreet(MyStreet);
                if (ans != " ")
                    return ans;
                else
                    user.Street = MyStreet;
            }
            if (MyBuildingNumber!=0)
            {
                string ans = checkBN(MyBuildingNumber);
                if (ans != " ")
                    return ans;
                else
                    user.building_number = MyBuildingNumber;
            }
            if (Myphone != null)
            {
                string ans = checkphone(Myphone);
                if (ans != " ")
                    return ans;
                else
                    user.phone = Myphone;
            }
            if (Mycellphone != null)
            {
                string ans = checkcell(Mycellphone);
                if (ans != " ")
                    return ans;
                else
                    user.cellphone = Mycellphone;
            }

            hmo_ctx.SaveChanges();
            return " ";

        }

        private static string checkcell(string mycellphone)
        {
            Regex cellphoneRegex = new Regex(@"^\d{10}$");
            if (!cellphoneRegex.IsMatch(mycellphone))
            {
                return "Cellphone number must be exactly 10 digits";
            }
            else
                return " ";
        }

        private static string checkphone(string myphone)
        {
            Regex phoneRegex = new Regex(@"^\d{9}$");
            if (!phoneRegex.IsMatch(myphone))
            {
                return "Phone number must be exactly 9 digits";
            }
            else
                return " ";
        }

        private static string checkBN(int myBuildingNumber)
        {
            Regex buildingNumberRegex = new Regex(@"^[1-9]\d*$");
            if (!buildingNumberRegex.IsMatch(myBuildingNumber.ToString()))
            {
                return "Building number should be a positive number";
            }
            else
                return " ";
        }

        private static string checkStreet(string myStreet)
        {
            Regex streetRegex = new Regex(@"^[A-Za-z]+$");
            if (!streetRegex.IsMatch(myStreet))
            {
                return "Street name should contain only letters";
            }
            else
                return " ";
          
        }

        private static string checkCity(string myCity)
        {
            List<string> validCities = new List<string>
    {
        "Jerusalem", "Tel Aviv", "Haifa", "Rishon LeZion", "Petah Tikva", "Ashdod", "Netanya", "Beer Sheva",
        "Holon", "Bnei Brak", "Bat Yam", "Rehovot", "Herzliya", "Kfar Saba", "Ramla", "Lod", "Nahariya",
        "Hadera", "Modiin-Maccabim-Reut", "Nazareth", "Beit Shemesh", "Lakhish", "Eilat", "Tiberias",
        "Safed", "Acre", "Dimona", "Beersheba", "Hod HaSharon", "Kiryat Gat", "Kiryat Malakhi",
        "Kiryat Motzkin", "Kiryat Yam", "Maalot-Tarshiha", "Migdal HaEmek", "Nesher", "Or Akiva",
        "Or Yehuda", "Ramat HaSharon", "Ramat-Gan", "Sderot", "Tamra", "Yavne", "Yehud-Monosson",
        "Yokneam"
    };

            if (!validCities.Contains(myCity))
            {
                return "City name is not valid";
            }
            else
                return " ";
        }

        private static string checkLN(string myLast_name)
        {
            Regex nameRegex = new Regex(@"^[A-Za-z]+$");
            if (!nameRegex.IsMatch(myLast_name))
            {
                return "Last name should contain only letters";
            }
            else return " ";
        }

        private static string checkFN(string myFirst_name)
        {
            Regex nameRegex = new Regex(@"^[A-Za-z]+$");
            if (!nameRegex.IsMatch(myFirst_name))
            {
                return "First name should contain only letters";
            }
            else return " ";
        }
    }
}
