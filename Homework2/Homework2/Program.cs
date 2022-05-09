using System;
using System.Collections.Generic;

namespace Homework2
{
    public class Good
    {
        private string _label;

        public string Label => _label;

        public Good(string label)
        {
            _label = label;
        }
    }

    public class Cart
    {
        private Warehouse _warehouse;
        private List<GoodsCell> _cells = new List<GoodsCell>();

        public Cart(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }

        private void CreateNewCell(Good good, int goodsCount)
        {
            GoodsCell newCell = new GoodsCell(good, goodsCount);
            _cells.Add(newCell);
        }

        public void Add(Good good, int goodsCount)
        {
            for (int i = 0; i < _warehouse.Cells.Count; i++)
            {
                if (_warehouse.Cells[i].Good.Label == good.Label)
                {
                    if (_warehouse.Cells[i].GoodsCount >= goodsCount)
                    {
                        CreateNewCell(good, goodsCount);
                        _warehouse.Cells[i].DecreaseGoodsCount(goodsCount);
                    }
                    else
                    {
                        Console.WriteLine("Столько товара нет!");
                    }
                }
            }
        }

        public void Order()
        {
            Console.WriteLine("\n Корзина:");

            for (int i = 0; i < _cells.Count; i++)
            {
                Console.Write($"Название - {_cells[i].Good.Label}");
                Console.WriteLine($" количество - {_cells[i].GoodsCount}");
            }
        }
    }

    public class GoodsCell
    {
        private Good _good;
        private int _goodsCount;

        public Good Good => _good;
        public int GoodsCount => _goodsCount;

        public GoodsCell(Good good, int goodsCount)
        {
            if(goodsCount <= 0)
            {
                throw new InvalidOperationException();
            }
            else
            {
                _good = good;
                _goodsCount = goodsCount;
            }
        }

        public void AddGoodsCount(int goodsCount)
        {
            if (goodsCount <= 0)
                throw new InvalidOperationException();
            else
                _goodsCount += goodsCount;
        }

        public void DecreaseGoodsCount(int goodsCount)
        {
            _goodsCount -= goodsCount;
        }
    }

    public class Warehouse
    {
        private List<GoodsCell> _cells = new List<GoodsCell>();
        private bool IsNeedCreateNewCell = true;

        public IReadOnlyList<GoodsCell> Cells => _cells;

        private void CreateNewCell(Good good, int goodsCount)
        {
            GoodsCell newCell = new GoodsCell(good, goodsCount);
            _cells.Add(newCell);
        }

        private int CheckGoodAvailability(Good good)
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                if (_cells[i].Good.Label == good.Label)
                {
                    return i;
                }
            }

            return -1;
        }

        public void Delive(Good good, int goodsCount)
        {
            int index = CheckGoodAvailability(good);

            if (index >= 0)
            {
                _cells[index].AddGoodsCount(goodsCount);
                IsNeedCreateNewCell = false;
            }

            if(IsNeedCreateNewCell)
            {
                CreateNewCell(good, goodsCount);
            }

            IsNeedCreateNewCell = true;
        }

        public void ShowInfo()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                Console.Write($"Название - {_cells[i].Good.Label}");
                Console.WriteLine($" количество - {_cells[i].GoodsCount}");
            }
        }
    }

    public class Shop
    {
        private Warehouse _warehouse;

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
        static void Main(string[] args)
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
            cart.Order();
            cart.Add(iPhone12, 1);
            cart.Order();
        }
    }
}