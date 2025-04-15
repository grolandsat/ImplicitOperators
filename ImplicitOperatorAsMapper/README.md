# Mapping avec Implicit Operator

On a souvent besoin de faire du mapping entre deux classes. Il existe différentes manières de procéder :
- Utilisation de nugget de mapping (type AutoMapper)
- Utilisation de méthodes d’extensions

Cet article vise à proposer une alternative plus légère à destination des mappings simples. Cette solution exploite les `implicit operator` pour le mapping.

## 1er Cas concret : Transformer une classe Data en classe Dto

### Déclaration des classes

```csharp
// Classe Data
internal record Userdata(int Id, string FirstName, string LastName);

// Classe Dto
public record UserDto(int Id, string FullName);
```

### Ajout de l’`implicit operator`

On ajoute à la classe `Userdata` un `implicit operator` pour transformer `Userdata` en `UserDto`. Il fonctionne comme un cast.

```csharp
internal record Userdata(int Id, string FirstName, string LastName)
{
    public static implicit operator UserDto(Userdata userData)
    {
        return new UserDto(userData.Id, $"{userData.FirstName} {userData.LastName}");
    }
}

public record UserDto(int Id, string FullName);
```

### Utilisation

```csharp
Userdata userData = new(1, "Alex", "Terrieur");
UserDto userDto = userData;
Console.WriteLine($"User ID: {userDto.Id}, Full Name: {userDto.FullName}");
```

---

## 2ème Cas : Notre User possède une liste d’adresses

### Déclaration des classes

On déclare les classes `Address` selon nos besoins et selon le format fourni par l’infrastructure (BDD ou API).

```csharp
// Classe Dto
public record AddressDto(int Id, string FullAddress);

// Classe Data avec son implicit operator
internal record AddressData(
    int Id,
    string Street,
    string City,
    string State,
    string ZipCode
)
{
    public static implicit operator AddressDto(AddressData addressData)
    {
        return new AddressDto(
            addressData.Id,
            $"{addressData.Street}, {addressData.City}, {addressData.State} {addressData.ZipCode}"
        );
    }
}
```

### Gestion de la liste

Pour les listes, on utilise une méthode d’extension.

```csharp
internal static class AddressDataExtensions
{
    internal static IEnumerable<AddressDto> ToDtos(this IEnumerable<AddressData> addressData)
    {
        return addressData.Select(address => (AddressDto)address);
    }
}
```

### Modification de l’`implicit operator` de `Userdata`

On adapte la classe `Userdata` pour inclure une liste d’adresses.

```csharp
internal record Userdata(int Id, string FirstName, string LastName, IEnumerable<AddressData> Addresses)
{
    public static implicit operator UserDto(Userdata userData)
    {
        return new UserDto(
            userData.Id,
            $"{userData.FirstName} {userData.LastName}",
            userData.Addresses.ToDtos()
        );
    }
}

// UserDto qui attend sa liste d’adresses
public record UserDto(int Id, string FullName, IEnumerable<AddressDto> Addresses);
```

### Utilisation

```csharp
IEnumerable<AddressData> addressDatas = new[]
{
    new AddressData(1, "Rue de la Paix", "Paris", "France", "95000"),
    new AddressData(2, "Avenue des Champs-Élysées", "Paris", "France", "95000"),
};

Userdata userData = new(1, "Alex", "Terrieur", addressDatas);
UserDto userDto = userData;

Console.WriteLine($"User ID: {userDto.Id}, Full Name: {userDto.FullName}");
foreach (var address in userDto.Addresses)
{
    Console.WriteLine($"Address Id: {address.Id}, Full: {address.FullAddress}");
}
```

---

## Conclusion

L’utilisation des `implicit operator` permet de gérer le mapping simplement et de manière transparente :
- La gestion des sous-classes est facilitée.
- La gestion des listes reste simple.

Toutefois, pour des classes complexes, il serait préférable d’utiliser une autre approche ou d’adapter cette méthode en conséquence.
