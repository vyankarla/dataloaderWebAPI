using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Enums
    {
        public enum DatasetTypeColumnsEnum
        {
            API = 1,
            WELLNAME = 2,
            D_DATE = 4,
            OIL = 5,
            GAS = 6,
            WATER = 7,
            TubingPsi = 9,
            CasingPsi = 10,
            Downtime = 11,
            DowntimeReason = 12,
            Choke = 13,

            Year = 14,
            Month = 15,
            Oil_Monthly = 19,
            Gas_Monthly = 20,
            Water_Monthly = 21,
            DaysOn = 22,
            API_Monthly = 23
        }

        public enum StatusEnum
        {
            Initiated = 1,
            Pending = 2,
            Completed = 3
        }

        public enum FileTypesEnum
        {
            Excel = 1,
            CSV = 2,
            AccessDB = 3
        }

        public enum DatasetTypesEnum
        {
            DailyProd = 1,
            Monthly = 2
        }

    }
}