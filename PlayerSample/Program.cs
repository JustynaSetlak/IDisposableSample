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
        private const string END_COMMAND = "End";
        private const string DISPOSE_COMMAND = "Dispose";
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

            HandlePlayerSource(userInput, musicManager, path);
            musicManager.Play();

            ShowPlayingPossibilities();
            HandleUserPlayingCommand(musicManager);

            musicManager.Dispose();
            musicManager.Play();

            Console.ReadKey();
        }

        private static void HandlePlayerSource(int userInput, MusicPlayer musicManager, string path)
        {
            switch (userInput)
            {
                case MUSIC_FROM_PATH_OPTION:
                    musicManager.Load(path);
                    break;
                case MUSIC_FROM_BYTES_OPTION:
                    byte[] audiobyte = File.ReadAllBytes(path);
                    musicManager.Load(audiobyte);
                    break;
                case MUSIC_FROM_STREAM_OPTION:
                    byte[] bytes = File.ReadAllBytes(path);
                    Stream stream = new MemoryStream(bytes);
                    musicManager.Load(stream);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        private static void HandleUserPlayingCommand(MusicPlayer musicManager)
        {
            var command = Console.ReadLine();
            do
            {
                switch (command)
                {
                    case PLAY_COMMAND:
                        musicManager.Play();
                        break;
                    case STOP_COMMAND:
                        musicManager.Stop();
                        break;
                    case DISPOSE_COMMAND:
                        musicManager.Dispose();
                        break;
                }
                command = Console.ReadLine();
            } while (command != END_COMMAND);
        }

        private static int GetChosenPlayerOption()
        {
            Console.WriteLine("Check your option: ");
            Console.WriteLine($"{MUSIC_FROM_PATH_OPTION} - Play from path");
            Console.WriteLine($"{MUSIC_FROM_BYTES_OPTION} - Play from byte array");
            Console.WriteLine($"{MUSIC_FROM_STREAM_OPTION} - Play from stream");
            var chosenOption = Console.ReadLine();
            int.TryParse(chosenOption, out int result);
            return result;
        }

        private static string GetPath()
        {
            Console.WriteLine("Tell me the path: ");
            var path = Console.ReadLine();
            return path;
        }

        private static void ShowPlayingPossibilities()
        {
            Console.WriteLine($"If you want to stop music write: {STOP_COMMAND}");
            Console.WriteLine($"If you want to play music write: {PLAY_COMMAND}");
            Console.WriteLine($"If you want to dispose write: {DISPOSE_COMMAND}");
            Console.WriteLine($"If you want to quit write: {END_COMMAND}");
        }
    }
}
