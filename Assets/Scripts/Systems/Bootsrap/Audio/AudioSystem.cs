using UnityEngine;
public class AudioSystem : MonoBehaviour 
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;

    [SerializeField] private Clips _musics;
    [SerializeField] private Clips _sounds;

    private MusicAudio _music;
    private SoundAudio _sound;

    public void Initialize()
    {
        DontDestroyOnLoad(gameObject);

        _music = new MusicAudio(_musicSource, _musics);
        _sound = new SoundAudio(_soundSource, _sounds);
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public float MasterVolume => AudioListener.volume;

    public MusicAudio Music => _music;
    public SoundAudio Sound => _sound;
}