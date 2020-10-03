using System.Linq;
using Wintellect.PowerCollections;

namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Inventory : IHolder
    {
        private List<IWeapon> _weapons;

        public Inventory()
        {
            this._weapons = new List<IWeapon>();
        }

        public int Capacity => this._weapons.Count;

        public void Add(IWeapon weapon)
        {
            if (!this._weapons.Contains(weapon))
            {
                this._weapons.Add(weapon);
            }
        }

        public void Clear()
        {
            this._weapons.Clear();
        }

        public bool Contains(IWeapon weapon)
        {
            return this._weapons.Contains(weapon);
        }

        public void EmptyArsenal(Category category)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                if (this._weapons[i].Category == category)
                {
                    this._weapons[i].Ammunition = 0;
                }
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            if (this._weapons.Contains(weapon))
            {
                if (weapon.Ammunition >= ammunition)
                {
                    weapon.Ammunition -= ammunition;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");

            }


        }

        public IWeapon GetById(int id)
        {
            if (id < 0 || id >= this.Capacity)
            {
                return null;
            }
            else
            {
                return this._weapons[id];
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this._weapons.GetEnumerator();
        }
        // may occur bugs
        public int Refill(IWeapon weapon, int ammunition)
        {
            if (this.Contains(weapon))
            {
                if (weapon.Ammunition + ammunition <= weapon.MaxCapacity)
                {
                    weapon.Ammunition += ammunition;
                }
                else
                {
                    weapon.Ammunition = weapon.MaxCapacity;
                }
            }
            else
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            return weapon.Ammunition;
        }

        public IWeapon RemoveById(int id)
        {
            var weapon = this.GetById(id);
            if (weapon != null)
            {
                this._weapons.Remove(weapon);
            }
            else
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            return weapon;
        }

        public int RemoveHeavy()
        {
            
            var newList = new List<IWeapon>();
            for (int i = 0; i < this.Capacity; i++)
            {
                if (this._weapons[i].Category != Category.Heavy)
                {
                   newList.Add(this._weapons[i]);
                }
            }

            int toReturn = this._weapons.Count - newList.Count;
            this._weapons = newList;
            return toReturn;



        }

        public List<IWeapon> RetrieveAll()
        {
            if (this.Capacity == 0)
            {
                return new List<IWeapon>();
            }
            else
            {
                return new List<IWeapon>(this._weapons);
            }
        }

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            int lowerBoundIndex = (int)lower;
            int upperBoundIndex = (int)upper;
            var result = new List<IWeapon>(this.Capacity);

            for (int i = 0; i < this.Capacity; i++)
            {
                var currentWeapon = this._weapons[i];
                int WeaponStatusIndex = (int)currentWeapon.Category;

                if (WeaponStatusIndex >= lowerBoundIndex
                    && WeaponStatusIndex <= upperBoundIndex)
                {
                    result.Add(currentWeapon);
                }
            }

            return result;
        }

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            if (this.Contains(firstWeapon))
            {

                if (this.Contains(secondWeapon))
                {
                    if (firstWeapon.Category == secondWeapon.Category)
                    {

                        var indexofFirst = this._weapons.IndexOf(firstWeapon);
                        var indexofSecond = this._weapons.IndexOf(secondWeapon);

                        var temp = this._weapons[indexofFirst];
                        this._weapons[indexofFirst] = this._weapons[indexofSecond];
                        this._weapons[indexofSecond] = temp;

                    }

                }
                else
                {
                    throw new InvalidOperationException("Weapon does not exist in inventory!");
                }
            }
            else
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
        }
    }
}
