// See https://aka.ms/new-console-template for more information

using ImplicitOperatorAsMapper;
IEnumerable<AddressData> addressDatas = [
    new(1, "Rue de la Paix", "Paris", "France","95000"),
    new(2, "Avenue des Champs-Élysées", "Paris", "France","95000"),
    ];
Userdata userData = new (1,"Alex", "Terrieur", addressDatas );
UserDto userDto = userData;
Console.WriteLine( $"User ID: {userDto.Id}, Full Name: {userDto.FullName}" );
foreach ( var address in userDto.Addresses )
{
    Console.WriteLine( $"Adress Id: {address.Id} , Full: {address.FullAddress}" );
}
