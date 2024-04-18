using EgwarancjeDbLibrary.Models;

namespace Egwarancje.Utils;

public class OrderGenerator
{
    public readonly Random random;
    public readonly int seed;

    readonly string[,] ordersAndMaterials = new string[,]
    {
        // Zamówienie, Realizacja (materiały)
        { "Stół jadalny", "Drewno dębowe - lakierowane" },
        { "Sofa narożna", "Skóra naturalna - czarna" },
        { "Krzesło do jadalni", "Drewno bukowe - malowane" },
        { "Łóżko dwuosobowe", "Drewno sosnowe - olejowane" },
        { "Stolik kawowy", "Drewno orzechowe - ciemne" },
        { "Biurko do pracy", "Metal malowany proszkowo - czarny" },
        { "Regał na książki", "Drewno sosnowe - woskowane" },
        { "Szafa ubraniowa", "Drewno olchowe - bejcowane" },
        { "Komoda", "Drewno mahoniowe - olejowane" },
        { "Szafka nocna", "Drewno dębowe - naturalne" },
        { "Stołek barowy", "Metal chromowany - lśniący" },
        { "Szafka RTV", "Drewno czereśniowe - lakierowane" },
        { "Taboret", "Metal nierdzewny - polerowany" },
        { "Ławka ogrodowa", "Metal miedziany - oksydowany" },
        { "Krzesło biurowe", "Tworzywo sztuczne - matowe" },
        { "Fotel wypoczynkowy", "Skóra syntetyczna - brązowa" },
        { "Biblioteczka", "Drewno brzozowe - malowane" },
        { "Półka wisząca", "Drewno jesionowe - naturalne" },
        { "Półka na buty", "Tworzywo kompozytowe - kolorowe" },
        { "Stołek składany", "Metal cynkowany - galwanizowany" },
        { "Szafka łazienkowa", "Drewno olchowe - lakierowane" },
        { "Wieszak na ubrania", "Metal nierdzewny - polerowany" },
        { "Szafka kuchenna", "Drewno sosnowe - malowane" },
        { "Łóżko dziecięce", "Drewno brzozowe - naturalne" },
        { "Szezlong", "Tkanina bawełniana - beżowa" },
        { "Ława szklana", "Szkło hartowane - przezroczyste" },
        { "Biurko narożne", "Drewno mahoniowe - malowane" },
        { "Półka na wino", "Drewno dębowe - olejowane" },
        { "Witryna na szkło", "Drewno orzechowe - lakierowane" },
        { "Krzesło kuchenne", "Tworzywo sztuczne - kolorowe" },
        { "Regał na płyty", "Metal malowany proszkowo - czarny" },
        { "Stolik barowy", "Metal mosiężny - satynowany" },
        { "Ławka przedpokojowa", "Drewno olchowe - woskowane" },
        { "Szafka na buty", "Drewno sosnowe - olejowane" },
        { "Stolik do kawy", "Drewno czereśniowe - naturalne" },
        { "Szafa garderobiana", "Drewno bukowe - lakierowane" },
        { "Stolik do telewizora", "Drewno mahoniowe - ciemne" },
        { "Szafka na dokumenty", "Metal kuty - miedziany" },
        { "Krzesło ogrodowe", "Tworzywo sztuczne - matowe" },
        { "Krzesło do biurka", "Tkanina poliestrowa - szara" },
        { "Stolik nocny z szufladą", "Drewno dębowe - naturalne" },
        { "Stolik kawowy okrągły", "Drewno jesionowe - woskowane" },
        { "Ławka z oparciem", "Drewno sosnowe - malowane" }
    };

    readonly string[] orderComments =
    [
        "Proszę o dostawę w godzinach wieczornych.",
        "Jeśli to możliwe, proszę o zapakowanie produktów w ekologiczne torby.",
        "Proszę o wcześniejsze powiadomienie przed dostawą.",
        "Wolę produkty o dłuższym terminie ważności.",
        "Jeśli dostępne, proszę o dołączenie próbek nowych produktów.",
        "Proszę o dokładne zabezpieczenie produktów szklanych.",
        "Dostarczcie proszę zamówienie do drzwi mieszkania.",
        "Chciałbym otrzymać produkty bez dodatku cukru.",
        "Proszę o unikanie plastikowych opakowań, jeśli to możliwe.",
        "Proszę o wysłanie faktury elektronicznej.",
        "Wolę produkty bez glutenu.",
        "Proszę o dostawę na parter.",
        "Jeśli produkt jest niedostępny, proszę o kontakt.",
        "Chciałbym zamienić ten produkt na inny, jeśli niedostępny.",
        "Proszę o dostawę w wybranym terminie.",
        "Proszę o pozostawienie paczki u sąsiada, jeśli mnie nie będzie.",
        "Proszę o użycie małych opakowań do łatwiejszego przenoszenia.",
        "Jeśli to możliwe, proszę o dostawę do pracy.",
        "Proszę o kontakt telefoniczny w razie problemów z dostawą.",
        "Proszę o niełączenie produktów spożywczych z chemicznymi.",
        "Proszę o zwrócenie uwagi na delikatne produkty w paczce.",
        "Proszę o wcześniejszą dostawę niż przewidywany termin.",
        "Chciałbym otrzymać potwierdzenie dostawy SMS-em.",
        "Proszę o pozostawienie paczki w miejscu wyznaczonym.",
        "Proszę o dostawę w wybranym przedziale godzinowym.",
        "Chciałbym otrzymać zamówienie w kartonie, nie w torbach.",
        "Proszę o zapakowanie zamówienia w taki sposób, aby było łatwe do otwarcia."
    ];

    public OrderGenerator(int seed)
    {
        this.seed = seed;
        random = new Random(seed);
    }

    public Order GenerateOrder(User user)
    {
        Order order = new()
        {
            UserId = user.Id,
            Comments = orderComments[random.Next(orderComments.Length)],
            OrderSpecs = [],
            OrderDate = DateTime.Now,
            OrderNumber = seed,
            User = user
        };

        return order;
    }

    public OrderSpec[] GenerateOrderSpecs(Order order)
    {
        int specsAmount = random.Next(6);

        OrderSpec[] specs = new OrderSpec[specsAmount];

        for (int i = 0; i < specsAmount; i++)
        {
            int index = random.Next(ordersAndMaterials.GetLength(0));
            string name = ordersAndMaterials[index, 0];
            string realisation = ordersAndMaterials[index, 1];

            OrderSpec spec = new()
            {
                OrderId = order.Id,
                Name = name,
                Realization = realisation,
                ValueNet = (float)(random.NextDouble() * 1000.0D),
                ValueGross = (float)(random.NextDouble() * 1000.0D),
                WarrantyLength = random.Next(100),
                Order = order
            };

            specs[i] = spec;
        }

        return specs;
    }
}
