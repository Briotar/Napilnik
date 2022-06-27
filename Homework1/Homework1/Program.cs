using System;

namespace Homework1
{
    class Weapon
    {
        private readonly int _damage;
        private readonly int _bulletsPerShot = 1;
        private int _bullets;

        public bool CanFire()
        {
            if (_bullets >= _bulletsPerShot)
                return true;
            else
                return false;
        }

        public void Fire(Player player)
        {
            if (player == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                player.TakeDamage(_damage);
                _bullets -= _bulletsPerShot;
            }
        }
    }

    class Player
    {
        private int _health;

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException();
            else
                _health -= damage;
        }
    }

    class Bot
    {
        private Weapon _weapon;

        public void OnSeePlayer(Player player)
        {
            if (_weapon == null)
                throw new NullReferenceException();
            else if (_weapon.CanFire())
                _weapon.Fire(player);
        }
    }
}
