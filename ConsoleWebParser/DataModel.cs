using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser
{
    public class DataException : Exception
    {
        public DataException(string msg) : base(msg) { }
    }

    public class DataModel
    {
        public const string InvalidValue = "N/A";
        public int id = -1;
        public string name = InvalidValue;
        public string VIN = InvalidValue;
        public double price = 0.0;
        public string URL = InvalidValue;

        public DataModel()
        {
            id = GetHashCode(); // ...
        }

        public DataModel(string name_, string vin_, string url_, double price_) : base()
        {
            name = name_;
            VIN = vin_;
            URL = url_;
            price = price_;
        }

        public void Validate()
        {
            if (name == InvalidValue || VIN == InvalidValue || URL == InvalidValue || price <= 0) throw new DataException("parce results invalid");
        }
        
        public void Output()
        {
            Console.WriteLine($"Entry {id} ({name}): vin = {VIN}, url = {URL}, price = {price}");
        }
    }
}
