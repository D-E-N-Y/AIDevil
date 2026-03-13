using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource Icons", menuName = "ResourceIcons")]
public class ResourceIcons : ScriptableObject
{
    [SerializeField] private List<ResourceIcon> _resourceIcons;

    public ResourceIcon GetResourceIcon(ResourceType resource)
    {
        foreach (ResourceIcon resourceIcon in _resourceIcons)
        {
            if (resourceIcon.resource == resource)
            {
                return resourceIcon;
            }
        }

        return _resourceIcons.First();
    }
}