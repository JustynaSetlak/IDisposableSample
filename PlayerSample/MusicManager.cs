using System;
using NAudio.Wave;

namespace PlayerSample
{
    public class MusicManager : IDisposable
    {
        private readonly WaveOutEvent _waveOutEvent;
        private readonly Mp3FileReader _mp3FileReader;
        private bool _isDisposedWaveOutEvent;
        private bool _isDisposedMp3FileReader;

        public MusicManager(WaveOutEvent waveOutEvent, Mp3FileReader mp3FileReader)
        {
            _waveOutEvent = waveOutEvent;
            _mp3FileReader = mp3FileReader;
        }

        public void PlayMusic()
        {
            Console.WriteLine("Playing music");
            CheckIfDisposed();
            _waveOutEvent.Init(_mp3FileReader);
            _waveOutEvent.Play();
        }

        public void StopMusic()
        {
            Console.WriteLine("Stopping music");
            _waveOutEvent.Stop();
            Dispose();
        }

        public void Dispose()
        {
            DisposeWaveOutEvent();
            DisposeMp3FileReader();
        }

        private void DisposeWaveOutEvent()
        {
            Console.WriteLine("Check if should dispose WaveOutEvent");
            if (!_isDisposedWaveOutEvent)
            {
                Console.WriteLine("Disposing WaveOutEvent");
                _waveOutEvent.Dispose();
                _isDisposedWaveOutEvent = true;
            }
            else
            {
                Console.WriteLine("Already disposed WaveOutEvent");
            }
        }

        private void DisposeMp3FileReader()
        {
            Console.WriteLine("Check if should dispose Mp3FileReader");
            if (!_isDisposedMp3FileReader)
            {
                Console.WriteLine("Disposing Mp3FileReader");
                _mp3FileReader.Dispose();
                _isDisposedMp3FileReader = true;
            }
            else
            {
                Console.WriteLine("Already disposed Mp3FileReader");
            }
        }

        private void CheckIfDisposed()
        {
            Console.WriteLine("Checking if any object disposed");
            if (_isDisposedWaveOutEvent || _isDisposedMp3FileReader)
            {
                throw new ObjectDisposedException("Object disposed");
            }
        }
    }
}