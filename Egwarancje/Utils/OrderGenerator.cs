using EgwarancjeDbLibrary.Models;
using System.Globalization;

namespace Egwarancje.Utils;

public class OrderGenerator
{
    public readonly Random random;
    public readonly int seed;

    private readonly string[,] ordersAndMaterials = new string[,]
    {
        { "Stol jadalny", "Drewno debowe - lakierowane" },
        { "Sofa narozna", "Skora naturalna - czarna" },
        { "Krzeslo do jadalni", "Drewno bukowe - malowane" },
        { "Lozko dwuosobowe", "Drewno sosnowe - olejowane" },
        { "Stolik kawowy", "Drewno orzechowe - ciemne" },
        { "Biurko do pracy", "Metal malowany proszkowo - czarny" },
        { "Regal na ksiazki", "Drewno sosnowe - woskowane" },
        { "Szafa ubraniowa", "Drewno olchowe - bejcowane" },
        { "Komoda", "Drewno mahoniowe - olejowane" },
        { "Szafka nocna", "Drewno debowe - naturalne" },
        { "Stolek barowy", "Metal chromowany - lsniacy" },
        { "Szafka RTV", "Drewno czeresniowe - lakierowane" },
        { "Taboret", "Metal nierdzewny - polerowany" },
        { "Lawka ogrodowa", "Metal miedziany - oksydowany" },
        { "Krzeslo biurowe", "Tworzywo sztuczne - matowe" },
        { "Fotel wypoczynkowy", "Skora syntetyczna - brazowa" },
        { "Biblioteczka", "Drewno brzozowe - malowane" },
        { "Polka wiszaca", "Drewno jesionowe - naturalne" },
        { "Polka na buty", "Tworzywo kompozytowe - kolorowe" },
        { "Stolek skladany", "Metal cynkowany - galwanizowany" },
        { "Szafka lazienkowa", "Drewno olchowe - lakierowane" },
        { "Wieszak na ubrania", "Metal nierdzewny - polerowany" },
        { "Szafka kuchenna", "Drewno sosnowe - malowane" },
        { "Lozko dzieciece", "Drewno brzozowe - naturalne" },
        { "Szezlong", "Tkanina bawelniana - bezowa" },
        { "Lawa szklana", "Szklo hartowane - przezroczyste" },
        { "Biurko narozne", "Drewno mahoniowe - malowane" },
        { "Polka na wino", "Drewno debowe - olejowane" },
        { "Witryna na szklo", "Drewno orzechowe - lakierowane" },
        { "Krzeslo kuchenne", "Tworzywo sztuczne - kolorowe" },
        { "Regal na plyty", "Metal malowany proszkowo - czarny" },
        { "Stolik barowy", "Metal mosiezny - satynowany" },
        { "Lawka przedpokojowa", "Drewno olchowe - woskowane" },
        { "Szafka na buty", "Drewno sosnowe - olejowane" },
        { "Stolik do kawy", "Drewno czeresniowe - naturalne" },
        { "Szafa garderobiana", "Drewno bukowe - lakierowane" },
        { "Stolik do telewizora", "Drewno mahoniowe - ciemne" },
        { "Szafka na dokumenty", "Metal kuty - miedziany" },
        { "Krzeslo ogrodowe", "Tworzywo sztuczne - matowe" },
        { "Krzeslo do biurka", "Tkanina poliestrowa - szara" },
        { "Stolik nocny z szuflada", "Drewno debowe - naturalne" },
        { "Stolik kawowy okragly", "Drewno jesionowe - woskowane" },
        { "Lawka z oparciem", "Drewno sosnowe - malowane" }
    };

    private readonly string[] orderComments =
    {
        "Prosze o dostawe w godzinach wieczornych.",
        "Jesli to mozliwe, prosze o zapakowanie produktow w ekologiczne torby.",
        "Prosze o wczesniejsze powiadomienie przed dostawa.",
        "Wole produkty o dluzszym terminie waznosci.",
        "Jesli dostepne, prosze o dolaczenie probek nowych produktow.",
        "Prosze o dokladne zabezpieczenie produktow szklanych.",
        "Dostarczcie prosze zamowienie do drzwi mieszkania.",
        "Chcialbym otrzymac produkty bez dodatku cukru.",
        "Prosze o unikanie plastikowych opakowan, jesli to mozliwe.",
        "Prosze o wyslanie faktury elektronicznej.",
        "Wole produkty bez glutenu.",
        "Prosze o dostawe na parter.",
        "Jesli produkt jest niedostepny, prosze o kontakt.",
        "Chcialbym zamienic ten produkt na inny, jesli niedostepny.",
        "Prosze o dostawe w wybranym terminie.",
        "Prosze o pozostawienie paczki u sasiada, jesli mnie nie bedzie.",
        "Prosze o uzycie malych opakowan do latwiejszego przenoszenia.",
        "Jesli to mozliwe, prosze o dostawe do pracy.",
        "Prosze o kontakt telefoniczny w razie problemow z dostawa.",
        "Prosze o nielaczenie produktow spozywczych z chemicznymi.",
        "Prosze o zwrocenie uwagi na delikatne produkty w paczce.",
        "Prosze o wczesniejsza dostawe niz przewidywany termin.",
        "Chcialbym otrzymac potwierdzenie dostawy SMS-em.",
        "Prosze o pozostawienie paczki w miejscu wyznaczonym.",
        "Prosze o dostawe w wybranym przedziale godzinowym.",
        "Chcialbym otrzymac zamowienie w kartonie, nie w torbach.",
        "Prosze o zapakowanie zamowienia w taki sposob, aby bylo latwe do otwarcia."
    };

    public OrderGenerator(int seed)
    {
        this.seed = seed;
        random = new Random(seed);
    }

    public Order GenerateOrder(User user)
    {
        CultureInfo polishCulture = new("pl-PL");
        Order order = new()
        {
            UserId = user.Id,
            Comments = orderComments[random.Next(orderComments.Length)],
            OrderSpecs = [],
            OrderDate = DateTime.Now.ToString("d", polishCulture),
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
                ValueNet = MathF.Round((float)(random.NextDouble() * 1000.0D) * 100f) / 100f,
                ValueGross = MathF.Round((float)(random.NextDouble() * 1000.0D) * 100f) / 100f,
                WarrantyLength = random.Next(100),
                Order = order
            };

            specs[i] = spec;
        }

        return specs;
    }
}
