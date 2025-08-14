using TMPro;
using UnityEngine;

public class UI_FPS : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ui_fps;
    private float timer;
    private int fps;

    void Start()
    {
        timer = 0f;
        fps = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        fps++;

        if (timer >= 1f)
        {
            ui_fps.text = fps.ToString();
            timer = 0f;
            fps = 0;
        }
    }
}
