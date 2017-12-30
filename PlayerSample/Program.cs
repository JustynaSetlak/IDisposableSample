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
        private const string STOP_COMMAND = "Stop";
        private const string PLAY_COMMAND = "Play";
        private const int MUSIC_FROM_PATH_OPTION = 1;
        private const int MUSIC_FROM_BYTES_OPTION = 2;
        private const int MUSIC_FROM_STREAM_OPTION = 3;

        static void Main(string[] args)
        {
            int userInput;
            do
            {
                userInput = GetChosenPlayerOption();
            } while (userInput != MUSIC_FROM_PATH_OPTION && userInput != MUSIC_FROM_BYTES_OPTION 
                    && userInput != MUSIC_FROM_STREAM_OPTION);

            var path = GetPath();

            MusicPlayer musicManager = new MusicPlayer();
            switch (userInput)
            {
                case MUSIC_FROM_PATH_OPTION:
                    musicManager.PlayMusicFromPath(path);
                    break;
                case MUSIC_FROM_BYTES_OPTION:
                    byte[] audiobyte = File.ReadAllBytes(path);
                    musicManager.PlayMusicFromBytes(audiobyte);
                    break;
                case MUSIC_FROM_STREAM_OPTION:
                    byte[] bytes = File.ReadAllBytes(path);
                    Stream stream = new MemoryStream(bytes);
                    musicManager.PlayMusicFromStream(stream);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            musicManager.PlayMusic();

            Console.WriteLine($"If you want to stop music write: {STOP_COMMAND}");
            var command = Console.ReadLine();
            if (command == STOP_COMMAND)
            {
                musicManager.StopMusic();
            }
            Console.WriteLine($"If you want to play music write: {PLAY_COMMAND}");
            var playCommand = Console.ReadLine();
            if (playCommand == PLAY_COMMAND)
            {
                musicManager.PlayMusic();
            }

            Console.ReadKey();
        }

        private static int GetChosenPlayerOption()
        {
            Console.WriteLine("Check your option: ");
            Console.WriteLine($"{MUSIC_FROM_PATH_OPTION} - Play from path");
            Console.WriteLine($"{MUSIC_FROM_BYTES_OPTION} - Play from byte array");
            Console.WriteLine($"{MUSIC_FROM_STREAM_OPTION} - Play from stream");
            var chosenOption = Console.ReadLine();
            Int32.TryParse(chosenOption, out int result);
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
