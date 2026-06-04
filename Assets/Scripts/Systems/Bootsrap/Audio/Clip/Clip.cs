using UnityEngine;

[System.Serializable]
public class Clip
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;

    public string Name => _name;
    public AudioClip AudioClip => _clip;
}