using System;
using System.IO;
using NAudio.Wave;

namespace PlayerSample
{
    public class MusicPlayer : IDisposable
    {
        private readonly WaveOutEvent _waveOutEvent;
        private IWaveProvider _provider;
        private IWaveProvider _streamProvider;
        private AudioFileReader _audioFileReader;
        private bool _isDisposedWaveOutEvent;
        private bool _isDisposedAudioFileReader;
        private bool _isPlayingFromFile;

        public MusicPlayer()
        {
            _waveOutEvent = new WaveOutEvent();
        }

        public void PlayMusicFromPath(string path)
        {
            CheckIfDisposed();
            _audioFileReader = new AudioFileReader(path);
            _isPlayingFromFile = true;
            _waveOutEvent.Init(_audioFileReader);
        }

        public void PlayMusicFromBytes(byte[] audiobyte)
        {
            CheckIfDisposed();
            _provider = new RawSourceWaveStream(
                new MemoryStream(audiobyte), new WaveFormat());
            _waveOutEvent.Init(_provider);
        }

        public void PlayMusicFromStream(Stream stream)
        {
            CheckIfDisposed();
            _streamProvider = new RawSourceWaveStream(stream, new WaveFormat());
            _waveOutEvent.Init(_streamProvider);
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
            Dispose();
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