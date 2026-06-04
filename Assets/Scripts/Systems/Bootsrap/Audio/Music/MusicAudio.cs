using UnityEngine;

public class MusicAudio : Audio
{
    public MusicAudio(AudioSource source, Clips clips) 
        : base(source, clips)
    {
        _source.loop = true;
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
            _source.clip = clip;
            _source.Play();
        }
        else
        {
            Debug.LogWarning($"Music clip with name {clip} not found.");
        }
    }
}