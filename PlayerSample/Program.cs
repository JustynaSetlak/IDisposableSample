using System;
using System.Collections.Generic;
using System.IO;
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
            int userInput;
            do
            {
                userInput = ShowMenu();
                var x = userInput == 1;
            } while (userInput != 1 && userInput != 2 && userInput != 3);

            var path = GetPath();

            MusicPlayer musicManager = new MusicPlayer();
            switch (userInput)
            {
                case 1:
                    musicManager.PlayMusicFromPath(path);
                    break;
                case 2:
                    byte[] audiobyte = File.ReadAllBytes(path);
                    musicManager.PlayMusicFromBytes(audiobyte);
                    break;
                case 3:
                    byte[] bytes = File.ReadAllBytes(path);
                    Stream stream = new MemoryStream(bytes);
                    musicManager.PlayMusicFromStream(stream);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
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

        private static int ShowMenu()
        {
            Console.WriteLine("Check your option: ");
            Console.WriteLine("1 - Play from path");
            Console.WriteLine("2 - Play from byte array");
            Console.WriteLine("3 - Play from stream");
            var chosenOption = Console.ReadLine();
            int result;
            var option = Int32.TryParse(chosenOption, out result);
            return result;
        }

        private static string GetPath()
        {
            Console.WriteLine("Tell me the path: ");
            var path = Console.ReadLine();
            return path;
        }
    }
}
