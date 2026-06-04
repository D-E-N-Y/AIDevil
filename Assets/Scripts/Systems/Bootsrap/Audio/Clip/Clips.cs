using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clips", menuName = "Clips")]
public class Clips : ScriptableObject 
{
    [SerializeField] private List<Clip> _clips;

    public List<Clip> AllClips => _clips;

    public Clip GetClipByName(string name)
    {
        return _clips.Find(clip => clip.Name == name);
    }
}