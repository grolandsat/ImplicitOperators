namespace ImplicitOperatorAsMapper;

internal record Userdata( int Id, string FirstName, string LastName, IEnumerable<AddressData> Addresses )
{
    public static implicit operator UserDto( Userdata userData )
    {
        return new UserDto( userData.Id, $"{userData.FirstName} {userData.LastName}", userData.Addresses.ToDtos() );
    }
}