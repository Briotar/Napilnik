using System;
using System.Collections.Generic;

namespace IMJunior
{
    class Program
    {
        static void Main(string[] args)
        {
            PaymentSystemFactory factory = new PaymentSystemFactory(new Qiwi("QIWI"), new WebMoney("WebMoney"), new Card("Card"));

            var orderForm = new OrderForm();
            var paymentHandler = new PaymentHandler();

            var systemId = orderForm.ShowForm();

            var paymentSystem = factory.GetPaymentSystem(systemId);

            paymentHandler.ShowPaymentResult(paymentSystem, systemId);
        }
    }

    public class OrderForm
    {
        public string ShowForm()
        {
            Console.WriteLine("Мы принимаем: QIWI, WebMoney, Card");

            //симуляция веб интерфейса
            Console.WriteLine("Какой системой вы хотите совершить оплату?");
            return Console.ReadLine();
        }
    }

    public class PaymentHandler
    {
        public void ShowPaymentResult(PaymentSystem paymentSystem, string systemId)
        {
            Console.WriteLine($"Вы оплатили с помощью {systemId}");

            paymentSystem.ShowInfo();

            Console.WriteLine("Оплата прошла успешно!");
        }
    }

    public abstract class PaymentSystem
    {
        public string SystemId { get; private set; }

        public PaymentSystem(string systemId)
        {
            SystemId = systemId;
        }

        public virtual void ShowInfo() { }
    }

    public class Qiwi : PaymentSystem
    {
        public Qiwi(string systemId) : base(systemId) { }

        public override void ShowInfo()
        {
            Console.WriteLine("Перевод на страницу QIWI...");
            Console.WriteLine("Проверка платежа через QIWI...");
        }
    }
    public class WebMoney : PaymentSystem
    {
        public WebMoney(string systemId) : base(systemId) { }

        public override void ShowInfo()
        {
            Console.WriteLine("Вызов API WebMoney...");
            Console.WriteLine("Проверка платежа через WebMoney...");
        }
    }

    public class Card : PaymentSystem
    {
        public Card(string systemId) : base(systemId) { }

        public override void ShowInfo()
        {
            Console.WriteLine("Вызов API банка эмитера карты Card...");
            Console.WriteLine("Проверка платежа через Card..."); 
        }
    }

    public class PaymentSystemFactory
    {
        private List<PaymentSystem> _paymentSystems;

        public PaymentSystemFactory(Qiwi qiwi, WebMoney webMoney, Card card)
        {
            _paymentSystems = new List<PaymentSystem>();

            _paymentSystems.Add(qiwi);
            _paymentSystems.Add(webMoney);
            _paymentSystems.Add(card);
        }

        public PaymentSystem GetPaymentSystem(string systemId)
        {
            for (int i = 0; i < _paymentSystems.Count; i++)
            {
                if (systemId == _paymentSystems[i].SystemId)
                    return _paymentSystems[i];
            }

            throw new InvalidOperationException();
        }
    }
}