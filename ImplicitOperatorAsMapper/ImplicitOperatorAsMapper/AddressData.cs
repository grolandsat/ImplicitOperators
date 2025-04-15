namespace ImplicitOperatorAsMapper;

internal record AddressData(
    int Id,
    string Street,
    string City,
    string State,
    string ZipCode
)
{
    public static implicit operator AddressDto( AddressData addressData )
    {
        return new AddressDto(
            addressData.Id,$"{addressData.Street}, {addressData.City}, {addressData.State} {addressData.ZipCode}"
        );
    }
}