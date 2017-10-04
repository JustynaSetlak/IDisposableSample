using System;

namespace IDisposableSample
{
    class Program
    {
        static void Main(string[] args)
        {
            FruitRepository fruitRepository = new FruitRepository(new FruitContext());
            fruitRepository.AddFruit(new Fruit{ Color = "Red", Name = "Apple"});
            try
            {
                fruitRepository.Get(1);
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
