using System;
using System.Collections.Generic;

namespace Homework2
{
    public class Good
    {
        private readonly string _label;

        public Good(string label)
        {
            _label = label;
        }

        public string Label => _label;
    }

    public class Cart : Warehouse
    {
        private readonly Warehouse _warehouse;

        public Cart(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public void Add(Good good, int goodsCount)
        {
            int index = _warehouse.CheckGoodAvailability(good);

            if (_warehouse.Goods[index].Item2 >= goodsCount)
            {
                AddNewGoods(good, goodsCount);
                _warehouse.DecreaseGoodsCount(goodsCount, index);
            }
            else
            {
               Console.WriteLine("Столько товара нет!");
            }
        }

        public override void ShowInfo()
        {
            Console.WriteLine("\n Корзина:");

            base.ShowInfo();
        }
    }

    public class Warehouse
    {
        private List<(Good, int)> _goods = new List<(Good, int)>();

        public IReadOnlyList<(Good, int)> Goods => _goods;

        public void Delive(Good good, int goodsCount)
        {
            AddNewGoods(good, goodsCount);
        }

        public virtual void ShowInfo()
        {
            for (int i = 0; i < _goods.Count; i++)
            {
                Console.Write($"Название - {_goods[i].Item1.Label}");
                Console.WriteLine($" количество - {_goods[i].Item2}");
            }
        }

        internal int CheckGoodAvailability(Good good)
        {
            for (int i = 0; i < _goods.Count; i++)
            {
                if (_goods[i].Item1.Label == good.Label)
                {
                    return i;
                }
            }

            return -1;
        }

        internal void DecreaseGoodsCount(int goodsCount, int index)
        {
            var myStruct = _goods[index];
            myStruct.Item2 -= goodsCount;
            _goods[index] = myStruct;
        }

        protected void AddNewGoods(Good good, int goodsCount)
        {
            int index = CheckGoodAvailability(good);

            if (index >= 0)
            {
                AddGoodsCount(goodsCount, index);
            }
            else
            {
                CreateNewGoodPosition(good, goodsCount);
            }
        }

        private void AddGoodsCount(int goodsCount, int index)
        {
            if (goodsCount <= 0)
            {
                throw new InvalidOperationException();
            }
            else
            {
                var myStruct = _goods[index];
                myStruct.Item2 += goodsCount;
                _goods[index] = myStruct;
            }
        }

        private void CreateNewGoodPosition(Good good, int goodsCount)
        {
            _goods.Add((good, goodsCount));
        }
    }

    public class Shop
    {
        private readonly Warehouse _warehouse;

        public Shop(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public Cart Cart()
        {
            Cart cart = new Cart(_warehouse);
            return cart;
        }
    }

    public class Program
    {
        private static void Main(string[] args)
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");
            Warehouse warehouse = new Warehouse();
            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 1);
            warehouse.Delive(iPhone11, 3);
            warehouse.ShowInfo();

            Cart cart = shop.Cart();

            cart.Add(iPhone12, 1);
            cart.Add(iPhone11, 2);
            cart.ShowInfo();
            cart.Add(iPhone12, 1);
            cart.ShowInfo();
        }
    }
}