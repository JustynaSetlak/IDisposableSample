using System;
using System.IO;
using NAudio.Wave;

namespace PlayerSample
{
    public class MusicPlayer : IDisposable
    {
        private readonly WaveOutEvent _waveOutEvent;
        private AudioFileReader _audioFileReader;
        private bool _isDisposedWaveOutEvent;
        private bool _isDisposedAudioFileReader;
        private bool _isPlayingFromFile;
        private bool _isPlayerInitted;

        public MusicPlayer()
        {
            _waveOutEvent = new WaveOutEvent();
        }

        public void Load(string path)
        {
            CheckIfDisposed();
            _isPlayingFromFile = true;
            _audioFileReader = new AudioFileReader(path);
            InitPlayer(_audioFileReader);
        }

        public void Load(byte[] audiobyte)
        {
            CheckIfDisposed();
            var provider = CreateProvider(new MemoryStream(audiobyte));
            InitPlayer(provider);
        }

        public void Load(Stream stream)
        {
            CheckIfDisposed();
            var provider = CreateProvider(stream);
            InitPlayer(provider);
        }

        private RawSourceWaveStream CreateProvider(Stream stream)
        {
            var provider = new RawSourceWaveStream(stream, new WaveFormat());
            return provider;
        }

        private void InitPlayer(IWaveProvider provider)
        {
            _waveOutEvent.Init(provider);
            _isPlayerInitted = true;
        }

        public bool Play()
        {
            if (_isPlayerInitted)
            {
                CheckIfDisposed();
                _waveOutEvent.Play();
                return true;
            }
            return false;
        }

        public void Stop()
        {
            CheckIfDisposed();
            _waveOutEvent.Stop();
        }

        public void Dispose()
        {
            DisposeWaveOutEvent();
            DisposeAudioFileReader();
        }

        private void DisposeWaveOutEvent()
        {
            if (!_isDisposedWaveOutEvent)
            {
                _waveOutEvent.Dispose();
                _isDisposedWaveOutEvent = true;
            }
        }

        private void DisposeAudioFileReader()
        {
            if (!_isDisposedAudioFileReader && _isPlayingFromFile)
            {
                _audioFileReader.Dispose();
                _isDisposedAudioFileReader = true;
            }
        }

        private void CheckIfDisposed()
        {
            if (_isDisposedWaveOutEvent || _isDisposedAudioFileReader)
            {
                throw new ObjectDisposedException("Object disposed");
            }
        }
    }
}