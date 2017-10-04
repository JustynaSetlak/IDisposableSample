using System;
using System.Collections.Generic;
using System.Text;

namespace IDisposableSample
{
    public class FruitRepository : IDisposable, IFruitRepository
    {
        private readonly FruitContext _context;
        private bool _isDisposed;

        public FruitRepository(FruitContext context)
        {
            _context = context;
        }

        public void AddFruit(Fruit fruit)
        {
            Console.WriteLine("Adding fruit");
            CheckIfDisposed();
            _context.Fruits.Add(fruit);
            _context.SaveChanges();
            Dispose();
        }

        public void DeleteFruit(int id)
        {
            Console.WriteLine("Delete fruit with id: " + id);
            var fruitToDelete = Get(id);
            CheckIfDisposed();
            _context.Fruits.Remove(fruitToDelete);
            _context.SaveChanges();
            Dispose();
        }

        public Fruit Get(int id)
        {
            Console.WriteLine("Get fruit with id: " + id);
            CheckIfDisposed();
            var fruit =_context.Fruits.Find(id);
            Console.WriteLine("fruit: " + fruit.Name);
            Dispose();
            return fruit;
        }

        public void Update(Fruit fruit)
        {
            Console.WriteLine("Update fruit with id: " + fruit.Id);
            CheckIfDisposed();
            _context.Fruits.Update(fruit);
            _context.SaveChanges();
            Dispose();
        }

        public void Dispose()
        {
            Console.WriteLine("Check if should dispose");
            if (!_isDisposed)
            {
                Console.WriteLine("Disposing");
                _context.Dispose();
                _isDisposed = true;
            }
            else
            {
                Console.WriteLine("Already disposed");
            }
        }

        public void IsDisposed()
        {
            _isDisposed = _context == null;
        }

        public void CheckIfDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException("Object disposed");
            }   
        }

    }
}
