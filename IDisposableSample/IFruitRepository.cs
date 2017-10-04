using System;
using System.Collections.Generic;
using System.Text;

namespace IDisposableSample
{
    public interface IFruitRepository
    {
        Fruit Get(int id);
        void AddFruit(Fruit fruit);
        void DeleteFruit(int id);
        void Update(Fruit fruit);
    }
}
