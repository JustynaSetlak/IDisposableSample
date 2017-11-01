using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace PlayerSample
{
    class Program
    {
        static void Main(string[] args)
        {
            MusicManager musicManager = new MusicManager(new WaveOutEvent(), new Mp3FileReader("Mozart.mp3"));
            musicManager.PlayMusic();
            
            Console.WriteLine("if you want to stop music write: stop");
            var command = Console.ReadLine();
            if (command == "stop")
            {
                musicManager.StopMusic();
            }
            Console.WriteLine("Write play to play music");
            var playCommand = Console.ReadLine();
            if (playCommand == "play")
            {
                musicManager.PlayMusic();
            }
            Console.ReadKey();

        }
    }
}
