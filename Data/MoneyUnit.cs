using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TestKSK.Data.Interfaces;
using TestKSK.Data.BaseEnities;

namespace TestKSK.Data
{
    public class MoneyUnit : CommonDataWithId
    {
        public uint Denomination { get; set; }
        public bool IsAvailable { get; set; }
        public Guid VendingMachineId { get; set; }
        public VendingMachine VendingMachine { get; set; }
    }
}
