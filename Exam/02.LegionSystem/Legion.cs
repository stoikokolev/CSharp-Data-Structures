using System.Linq;
using Wintellect.PowerCollections;

namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using _02.LegionSystem.Interfaces;

    public class Legion : IArmy
    {
        private OrderedSet<IEnemy> _enemies;

        public Legion()
        {
            this._enemies = new OrderedSet<IEnemy>();
        }

        public int Size => this._enemies.Count;

        public bool Contains(IEnemy enemy) => this._enemies.Contains(enemy);

        public void Create(IEnemy enemy) => this._enemies.Add(enemy);


        public IEnemy GetByAttackSpeed(int speed)
        {
            for (int i = 0; i < this.Size; i++)
            {
                if (this._enemies[i].AttackSpeed == speed)
                {
                    return this._enemies[i];
                }
            }

            return null;
        }

        public List<IEnemy> GetFaster(int speed)
        {
            var list = new List<IEnemy>(_enemies.Count);
            for (int i = 0; i < this.Size; i++)
            {
                if (this._enemies[i].AttackSpeed > speed)
                {
                    list.Add(this._enemies[i]);
                }
            }

            return list;
        }

        public IEnemy GetFastest()
        {
            this.CheckIfEmpty();
            return this._enemies.GetFirst();
        }

        public IEnemy[] GetOrderedByHealth()
        {

            return this._enemies.OrderByDescending(x => x.Health).ToArray();

        }

        public List<IEnemy> GetSlower(int speed)
        {

            var list = new List<IEnemy>();
            for (int i = 0; i < this.Size; i++)
            {
                if (this._enemies[i].AttackSpeed < speed)
                {
                    list.Add(this._enemies[i]);
                }
            }

            return list;
        }

        public IEnemy GetSlowest()
        {
            this.CheckIfEmpty();
            return this._enemies.GetLast();
        }

        public void ShootFastest()
        {
            this.CheckIfEmpty();
            this._enemies.RemoveFirst();
        }

        public void ShootSlowest()
        {
            this.CheckIfEmpty();

            this._enemies.RemoveLast();
        }

        private void CheckIfEmpty()
        {
            if (this._enemies.Count == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }
        }
    }
}
