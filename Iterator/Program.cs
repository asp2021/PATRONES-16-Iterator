using System;
using System.Collections.Generic;

namespace Iterator
{
    public class Menu
    {
        public Menu(string name, string description, double price, bool isVegetarian)
        {
            Name = name;
            Description = description;
            Price = price;
            IsVegetarian = isVegetarian;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public bool IsVegetarian { get; private set; }
    }

    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
    }

    public class DinnerMenuIterator : IIterator<Menu>
    {
        Menu[] items;
        int position = 0;

        public DinnerMenuIterator(Menu[] items)
        {
            this.items = items;
        }

        public bool HasNext()
        {
           if( position >= items.Length || items[position]==null)
                return false;
            else
                return true;
        }

        public Menu Next()
        {
            Menu menu = items[position];
            position = position + 1;
            return menu;
        }
    }

    public class BreakfastMenuIterator : IIterator<Menu>
    {
        List<Menu> items;
        int position = 0;

        public BreakfastMenuIterator(List<Menu> items)
        {
            this.items = items;
        }

        public bool HasNext()
        {
            if (position >= items.Count || items[position] == null)
                return false;
            else
                return true;
        }

        public Menu Next()
        {
            Menu menu = items[position];
            position = position + 1;
            return menu;
        }
    }

    public class BreakfastMenuCollection
    {
        private List<Menu> _menuItems;

        public BreakfastMenuCollection()
        {
            _menuItems = new List<Menu>();
            AddItem("Desayuno simple dulce", "Panqueques con manjar + cafe o te", 200, false);
            AddItem("Desayuno medio dulce", "Panqueques con manjar+ medialunas + cafe o te", 400, false);
            AddItem("Desayuno full dulce", "Panqueques con manjar + torta + jugo naranja + cafe o te", 700, false);
            AddItem("Desayuno vegetariano", "Jugo frutas + licuado + frutas", 300, true);
        }

        public void AddItem(string name, string description, double price, bool isVegetarian)
        {
            Menu menuItem = new Menu(name, description, price, isVegetarian);
            _menuItems.Add(menuItem);
        }

        public IIterator<Menu> CreateIterator() => new BreakfastMenuIterator(_menuItems);
    }

    public class DinnerMenuCollection
    {
        private Menu[] _menuItems;
        private int _maxItems = 4;
        private int _numberOfItems;

        public DinnerMenuCollection()
        {
            _menuItems = new Menu[_maxItems];

            AddItem("Cena veggar", "Ensalada + jugo", 250, true);
            AddItem("Caribean sushi", "10 hots rolls + vino", 450, false);
            AddItem("Pizza familiar", "Pizza grande + cerveza", 650, false);
            AddItem("Desayuno vegetariano", "Jugo frutas + licuado + frutas", 300, true);
        }

        public void AddItem(string name, string description, double price, bool isVegetarian)
        {
            Menu menuItem = new Menu(name, description, price, isVegetarian);
            if (_numberOfItems > _maxItems)
            {
                Console.WriteLine("No se permiten añadir más elementos");
            }
            else
            { 
                _menuItems[_numberOfItems] = menuItem;
                _numberOfItems++;
            }
        }

        // public Menu[] GetMenuItems() => _menuItems;
        public IIterator<Menu> CreateIterator() => new DinnerMenuIterator(_menuItems);
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ITERATOR" + "\n");
            Console.WriteLine("Permite desacoplar una iteraccion de una colección." + "\n");


            var breakFastMenu = new BreakfastMenuCollection();
            var dinnerMenu = new DinnerMenuCollection();

            IIterator<Menu> breakfastMenuIterator = breakFastMenu.CreateIterator();
            IIterator<Menu> dinnerMenuIterator = dinnerMenu.CreateIterator();

            Iterate(breakfastMenuIterator);
            Console.WriteLine();
            Iterate(dinnerMenuIterator);
            Console.ReadLine();
            
          
        }

        private static void Iterate(IIterator<Menu> iterator)
        {
            while (iterator.HasNext())
            {
                Menu menu = iterator.Next();
                Console.WriteLine(menu.Name);
                Console.WriteLine(menu.Description);
                Console.WriteLine(menu.Price);
            }
        }
    }
}
