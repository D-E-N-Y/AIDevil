using UnityEngine;

public class SoundAudio : Audio
{
    public SoundAudio(AudioSource source, Clips clips) 
        : base(source, clips)
    {
        _source.loop = false;
    }

    public override void PlayClip(string name)
    {
        Clip clip = _clips.GetClipByName(name);
        PlayClip(clip.AudioClip);
    }

    public override void PlayClip(AudioClip clip)
    {
        if (clip != null)
        {
            _source.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"Sound clip with name {clip} not found.");
        }
    }
}