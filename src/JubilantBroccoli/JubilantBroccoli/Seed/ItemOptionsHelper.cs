using JubilantBroccoli.Domain.Core.Enums;
using JubilantBroccoli.Domain.Models;

namespace JubilantBroccoli.Seed;

public static class ItemOptionsHelper
{
    public static List<ItemOption> GetItemOptions()
    {
        return new List<ItemOption>
        {
            // ItemType.Burger
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Ломтик сыра", Type = ItemType.Burger},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Бекон", Type = ItemType.Burger},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Карамелизированный лук", Type = ItemType.Burger},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Грибы", Type = ItemType.Burger},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Авокадо", Type = ItemType.Burger},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Салат айсберг", Type = ItemType.Burger},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Соус барбекю", Type = ItemType.Burger},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Огурцы маринованные", Type = ItemType.Burger},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Жареный лук", Type = ItemType.Burger},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Соус тартар", Type = ItemType.Burger},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Ананас", Type = ItemType.Burger},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Соус чесночный", Type = ItemType.Burger},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Помидоры свежие", Type = ItemType.Burger},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Маринованные перцы", Type = ItemType.Burger},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Руккола", Type = ItemType.Burger},
            // ItemType.Pizza
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Экстра сыр", Type = ItemType.Pizza},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Помидоры", Type = ItemType.Pizza},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Базилик", Type = ItemType.Pizza},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Оливки", Type = ItemType.Pizza},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Шампиньоны", Type = ItemType.Pizza},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Ветчина", Type = ItemType.Pizza},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Перец чили", Type = ItemType.Pizza},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Лосось копченый", Type = ItemType.Pizza},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Аншоусы", Type = ItemType.Pizza},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Свежий шпинат", Type = ItemType.Pizza},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Картофель фри", Type = ItemType.Pizza},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Куриная грудка", Type = ItemType.Pizza},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Фета", Type = ItemType.Pizza},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Креветки", Type = ItemType.Pizza},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Каперсы", Type = ItemType.Pizza},
            // ItemType.Sushi
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Имбирь", Type = ItemType.Sushi},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Васаби", Type = ItemType.Sushi},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Соевый соус", Type = ItemType.Sushi},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Тобико", Type = ItemType.Sushi},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Лосось", Type = ItemType.Sushi},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Угорь копченый", Type = ItemType.Sushi},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Тунец", Type = ItemType.Sushi},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Креветки коктейльные", Type = ItemType.Sushi},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Кунжут белый", Type = ItemType.Sushi},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Спайси краб", Type = ItemType.Sushi},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Огурец", Type = ItemType.Sushi},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Лук зеленый", Type = ItemType.Sushi},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Соус унаги", Type = ItemType.Sushi},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Соус спайси майо", Type = ItemType.Sushi},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Тобико черный", Type = ItemType.Sushi},
            // ItemType.Wok
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Острый соус", Type = ItemType.Wok},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Арахисы", Type = ItemType.Wok},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Чеснок", Type = ItemType.Wok},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Брокколи", Type = ItemType.Wok},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Кунжут", Type = ItemType.Wok},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Морковь", Type = ItemType.Wok},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Болгарский перец", Type = ItemType.Wok},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Цуккини", Type = ItemType.Wok},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Ананас консервированный", Type = ItemType.Wok},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Соус хойсин", Type = ItemType.Wok},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Лапша удон", Type = ItemType.Wok},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Корень имбиря", Type = ItemType.Wok},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Кунжутное масло", Type = ItemType.Wok},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Арахисовая паста", Type = ItemType.Wok},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Специи тайские", Type = ItemType.Wok},
            // ItemType.Kebab
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Соус тахини", Type = ItemType.Kebab},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Свежие овощи", Type = ItemType.Kebab},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Луковые кольца", Type = ItemType.Kebab},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Халапеньо", Type = ItemType.Kebab},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Сметана", Type = ItemType.Kebab},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Лаваш", Type = ItemType.Kebab},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Сыр Фета", Type = ItemType.Kebab},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Маринованный перец", Type = ItemType.Kebab},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Шампиньоны гриль", Type = ItemType.Kebab},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Соус чесночный", Type = ItemType.Kebab},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Мясной соус", Type = ItemType.Kebab},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Картофель по-деревенски", Type = ItemType.Kebab},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Капуста маринованная", Type = ItemType.Kebab},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Томаты свежие", Type = ItemType.Kebab},
            new ItemOption { Id = Guid.NewGuid().ToString(), Name = "Лук красный", Type = ItemType.Kebab}
        };
    }
}