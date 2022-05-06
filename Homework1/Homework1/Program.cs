using System;

namespace Homework1
{
    class Weapon
    {
        private int _damage;
        private int _bullets;

        public void Fire(Player player)
        {
            if(_bullets >= 1)
            {
                player.TakeDamage(_damage);
                _bullets -= 1;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    class Player
    {
        private int _health;

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }
    }

    class Bot
    {
        private Weapon _weapon;

        public void OnSeePlayer(Player player)
        {
            _weapon.Fire(player);
        }
    }
}
