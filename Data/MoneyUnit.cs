using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TestKSK.Data.Interfaces;

namespace TestKSK.Data
{
    public class MoneyUnit : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint Denomination { get; set; }
        public bool IsAvailable { get; set; }
        public Guid VendingMachineId { get; set; }
        public VendingMachine VendingMachine { get; set; }
    }
}
