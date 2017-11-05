using System;
using NAudio.Wave;

namespace PlayerSample
{
    public class MusicPlayer : IDisposable
    {
        private readonly WaveOutEvent _waveOutEvent;
        private readonly Mp3FileReader _mp3FileReader;
        private bool _isDisposedWaveOutEvent;
        private bool _isDisposedMp3FileReader;

        public MusicPlayer()
        {
            _waveOutEvent = new WaveOutEvent();
            _mp3FileReader = new Mp3FileReader("Mozart.mp3");
        }

        public void CreatePlayer()
        {
            CheckIfDisposed();
            _waveOutEvent.Init(_mp3FileReader);
        }

        public void PlayMusic()
        {
            CheckIfDisposed();
            _waveOutEvent.Play();
        }

        public void StopMusic()
        {
            CheckIfDisposed();
            _waveOutEvent.Stop();
            _waveOutEvent.Dispose();
        }

        public void Dispose()
        {
            DisposeWaveOutEvent();
            DisposeMp3FileReader();
        }

        private void DisposeWaveOutEvent()
        {
            if (!_isDisposedWaveOutEvent)
            {
                _waveOutEvent.Dispose();
                _isDisposedWaveOutEvent = true;
            }
        }

        private void DisposeMp3FileReader()
        {
            if (!_isDisposedMp3FileReader)
            {
                _mp3FileReader.Dispose();
                _isDisposedMp3FileReader = true;
            }
        }

        private void CheckIfDisposed()
        {
            if (_isDisposedWaveOutEvent || _isDisposedMp3FileReader)
            {
                throw new ObjectDisposedException("Object disposed");
            }
        }
    }
}