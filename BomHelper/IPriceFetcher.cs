using BomHelper.Dto;
using UtilClasses.Extensions.Strings;

namespace BomHelper;

public interface IPriceFetcher
{
    Task <PriceInfo>GetPrice();
}



