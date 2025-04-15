namespace ImplicitOperatorAsMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class AdresseDataExtensions
{
    internal static IEnumerable<AddressDto> ToDtos( this IEnumerable<AddressData> addressData )
    {
        return addressData.Select( address => (AddressDto)address );
    }
}
