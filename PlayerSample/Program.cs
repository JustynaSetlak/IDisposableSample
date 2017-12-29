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
        private const string StopCommand = "Stop";
        private const string PlayCommand = "Play";
        private const int MusicFromPathOption = 1;
        private const int MusicFromBytesOption = 2;
        private const int MusicFromStreamOption = 3;

        static void Main(string[] args)
        {
            int userInput;
            do
            {
                userInput = GetChosenPlayerOption();
            } while (userInput != MusicFromPathOption && userInput != MusicFromBytesOption 
                    && userInput != MusicFromStreamOption);

            var path = GetPath();

            MusicPlayer musicManager = new MusicPlayer();
            switch (userInput)
            {
                case MusicFromPathOption:
                    musicManager.PlayMusicFromPath(path);
                    break;
                case MusicFromBytesOption:
                    byte[] audiobyte = File.ReadAllBytes(path);
                    musicManager.PlayMusicFromBytes(audiobyte);
                    break;
                case MusicFromStreamOption:
                    byte[] bytes = File.ReadAllBytes(path);
                    Stream stream = new MemoryStream(bytes);
                    musicManager.PlayMusicFromStream(stream);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            musicManager.PlayMusic();

            Console.WriteLine($"If you want to stop music write: {StopCommand}");
            var command = Console.ReadLine();
            if (command == StopCommand)
            {
                musicManager.StopMusic();
            }
            Console.WriteLine($"If you want to play music write: {PlayCommand}");
            var playCommand = Console.ReadLine();
            if (playCommand == PlayCommand)
            {
                musicManager.PlayMusic();
            }

            Console.ReadKey();
        }

        private static int GetChosenPlayerOption()
        {
            Console.WriteLine("Check your option: ");
            Console.WriteLine($"{MusicFromPathOption} - Play from path");
            Console.WriteLine($"{MusicFromBytesOption} - Play from byte array");
            Console.WriteLine($"{MusicFromStreamOption} - Play from stream");
            var chosenOption = Console.ReadLine();
            int result;
            Int32.TryParse(chosenOption, out result);
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
