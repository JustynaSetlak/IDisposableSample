﻿using System;
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
        private bool _isPlayerInitted;

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
            _isPlayerInitted = true;
        }

        public void PlayMusicFromBytes(byte[] audiobyte)
        {
            CheckIfDisposed();
            _provider = new RawSourceWaveStream(
                new MemoryStream(audiobyte), new WaveFormat());
            _waveOutEvent.Init(_provider);
            _isPlayerInitted = true;
        }

        public void PlayMusicFromStream(Stream stream)
        {
            CheckIfDisposed();
            _streamProvider = new RawSourceWaveStream(stream, new WaveFormat());
            _waveOutEvent.Init(_streamProvider);
            _isPlayerInitted = true;
        }

        public bool PlayMusic()
        {
            if (_isPlayerInitted)
            {
                CheckIfDisposed();
                _waveOutEvent.Play();
                return true;
            }
            return false;
        }

        public void StopMusic()
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