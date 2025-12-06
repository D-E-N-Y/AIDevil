using UnityEngine;

public class UI_Panel : MonoBehaviour
{
    public bool isShow { get; protected set; }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        isShow = true;
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        isShow = false;
    }

    protected virtual void ClearSubscriptions()
    {
        
    }

    protected virtual void AddSubscriptions()
    {
        
    }
}