using System.Linq;
using System.Threading;

namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Loader : IBuffer
    {
        private List<IEntity> _entities;

        public Loader()
        {
            this._entities = new List<IEntity>();
        }

        public int EntitiesCount => this._entities.Count;

        public void Add(IEntity entity)
        {
            this._entities.Add(entity);
        }

        public void Clear()
        {
            this._entities.Clear();
        }

        public bool Contains(IEntity entity)
        {
            return this.GetById(entity.Id) != null;
        }

        public IEntity Extract(int id)
        {
            IEntity fount = this.GetById(id);

            if (fount != null)
            {
                this._entities.Remove(fount);
            }

            return fount;

        }

        public IEntity Find(IEntity entity)
        {
            return this.GetById(entity.Id);
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(this._entities);
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            return this._entities.GetEnumerator();
        }

        public void RemoveSold()
        {
            this._entities.RemoveAll(x => x.Status == BaseEntityStatus.Sold);
        }

        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
            int indexOfEntity = this._entities.IndexOf(oldEntity);
            this.ValidateEntity(indexOfEntity);

            this._entities[indexOfEntity] = newEntity;
        }


        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            int lowerBoundIndex = (int)lowerBound;
            int upperBoundIndex = (int)upperBound;
            var result = new List<IEntity>(this.EntitiesCount);

            for (int i = 0; i < this.EntitiesCount; i++)
            {
                var currentEntity = this._entities[i];
                int entityStatusIndex = (int)currentEntity.Status;

                if (entityStatusIndex >= lowerBoundIndex
                    && entityStatusIndex <= upperBoundIndex)
                {
                    result.Add(currentEntity);
                }
            }

            return result;
        }

        public void Swap(IEntity first, IEntity second)
        {
            int indexFirst = this._entities.IndexOf(first);
            this.ValidateEntity(indexFirst);
            int indexSecond = this._entities.IndexOf(second);
            this.ValidateEntity(indexSecond);

            var temp = this._entities[indexFirst];
            this._entities[indexFirst] = this._entities[indexSecond];
            this._entities[indexSecond] = temp;


        }

        public IEntity[] ToArray()
        {
            return this._entities.ToArray();
        }

        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            for (int i = 0; i < this.EntitiesCount; i++)
            {
                if (this._entities[i].Status.Equals(oldStatus))
                {
                    this._entities[i].Status = newStatus;
                }
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEntity GetById(int id)
        {
            for (int i = 0; i < this.EntitiesCount; i++)
            {
                var currentEntity = this._entities[i];

                if (currentEntity.Id == id)
                {
                    return currentEntity;
                }
            }

            return null;
        }

        private void ValidateEntity(int index)
        {
            if (index == -1)
            {
                throw new InvalidOperationException();
            }
        }
    }
}