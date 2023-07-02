using AutoMapper;
using TestKSK.Data;
using TestKSK.Models;

namespace TestKSK.Mappings
{
    public class VendingMappings : Profile
    {
        public VendingMappings()
        {
            CreateMap<VendingMachine, VendingMachineModel>();
            CreateMap<MoneyUnit, MoneyUnitModel>();
            CreateMap<Beverage, BeverageModel>();
        }
    }
}
