using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Order testOrder = new Order(1234, 200);

        IPaymentSystem firstSystem = new SimpleMD5PaymentSystem();
        IPaymentSystem secondSystem = new ComplexMD5PaymentSystem();
        IPaymentSystem thirdSystem = new SHA1PaymentSystem();

        Console.WriteLine($"pay.system1.ru / order ? amount = 12000RUB & hash ={firstSystem.GetPayingLink(testOrder)}");
        Console.WriteLine($"order.system2.ru / pay ? hash ={secondSystem.GetPayingLink(testOrder)}");
        Console.WriteLine($"system3.com / pay ? amount = 12000 & curency = RUB & hash ={thirdSystem.GetPayingLink(testOrder)}");
    }
}

public class Order
{
    public readonly int Id;
    public readonly int Amount;

    public Order(int id, int amount) => (Id, Amount) = (id, amount);
}

public interface IPaymentSystem
{
    public string GetPayingLink(Order order);

    public string GetStringHash(HashAlgorithm hashAlgorithm, int input);
}

public abstract class CanCreateStringHash
{
    public string GetStringHash(HashAlgorithm hashAlgorithm, int input)
    {
        byte[] hashResult = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input.ToString()));
        var sBuilder = new StringBuilder();

        for (int i = 0; i < hashResult.Length; i++)
        {
            sBuilder.Append(hashResult[i].ToString("x2"));
        }

        string result = sBuilder.ToString();

        return result;
    }
}

public class SimpleMD5PaymentSystem : CanCreateStringHash, IPaymentSystem
{
    public string GetPayingLink(Order order)
    {
        string payLink = GetStringHash(MD5.Create(), order.Id);

        return payLink;
    }
}

public class ComplexMD5PaymentSystem : CanCreateStringHash, IPaymentSystem
{
    public string GetPayingLink(Order order)
    {
        string payLink = GetStringHash(MD5.Create(), order.Id + order.Amount);

        return payLink;
    }
}

public class SHA1PaymentSystem : CanCreateStringHash, IPaymentSystem
{
    private int _secretKey = 111;

    public string GetPayingLink(Order order)
    {
        string payLink = GetStringHash(SHA1.Create(), order.Id + order.Amount + _secretKey);

        return payLink;
    }
}
