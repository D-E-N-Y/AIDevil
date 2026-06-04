using UnityEngine;

public abstract class Audio
{
    protected Clips _clips;
    protected AudioSource _source;
    private bool _isMuted;

    public Audio(AudioSource source, Clips clips)
    {
        _isMuted = false;

        _clips = clips;

        _source = source;
        _source.mute = _isMuted;
    }

    public void Play()
    {
        _source.Play();
    }

    public void Stop()
    {
        _source.Stop();
    }

    public void SetVolume(float volume)
    {
        _source.volume = volume;
    }

    public void ToggleMute()
    {
        _isMuted = !_isMuted;
        _source.mute = _isMuted;
    }

    public abstract void PlayClip(string name);
    public abstract void PlayClip(AudioClip clip);

    public float Volume => _source.volume;
    public bool IsMuted => _isMuted;
}