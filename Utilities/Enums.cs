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
            Choke = 12
        }

        public enum StatusEnum
        {
            Initiated = 1,
            Pending = 2,
            Completed = 3
        }
    }
}