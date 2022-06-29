public class Player
{
    private readonly Weapon _weapon;
    private readonly Movement _movement;

    public string Name { get; private set; }
    public int Age { get; private set; }

    public void Move()
    {
        Vector2 direction = _movement.CalculateVector2();
        //Do move
    }

    public void Attack()
    {
        //attack
        
        if (_weapon.IsReloading())
            Console.WriteLine("Reloading...");]
        else
            _weapon.Damage();
    }
}

public class Weapon
{
    private readonly float _cooldown;
    private readonly int _damage;

    public void Damage() { }

    public bool IsReloading()
    {
        throw new NotImplementedException();
    }
}

public class Movement
{
    private float _directionX;
    private float _directionY;
    private readonly float _speed;

    public void CalculateVector2() { }
}