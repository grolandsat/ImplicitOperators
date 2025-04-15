namespace ImplicitOperatorAsMapper;

public record UserDto( int Id, string FullName , IEnumerable<AddressDto> Addresses);