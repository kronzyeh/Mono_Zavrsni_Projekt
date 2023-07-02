using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public class Helper
    {
        public static string connectionString = Environment.GetEnvironmentVariable("LibraryConnectionString");

        public static List<Guid> ToGuidList(string listOfStrings)
        {
            List<Guid> listOfGuids = new List<Guid>();
            string[] arrayOfStrings = listOfStrings.Split(',');
            for (int i = 0; i < arrayOfStrings.Length; i++)
            {
                listOfGuids.Add(new Guid(arrayOfStrings[i]));
            }
            return listOfGuids;
        }

    }
}
