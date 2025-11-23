using UnityEngine;
using UnityEngine.UI;

public class UI_Character : UI_Panel 
{
    [SerializeField] private Button ui_selectButton;
    
    [SerializeField] private Image ui_characterImage;
    [SerializeField] private Image ui_selectImage;
    private Color selectedColor;
    private Color unselectedColor;

    private Player _player;
    private bool isSelect;

    private UI_CharactersList _ui_charactersList;

    public void Initialize(Player player, UI_CharactersList ui_charactersList)
    {
        _player = player;
        _ui_charactersList = ui_charactersList;
        
        selectedColor = new Vector4(255f / 255f, 243f / 255f, 208f / 255f, 1f);
        unselectedColor = new Vector4(32f / 255f, 18f / 255f, 6f / 255f, 1f);

        isSelect = false;
        ui_selectImage.gameObject.SetActive(false);
        ui_characterImage.color = unselectedColor;

        ui_selectButton.onClick.RemoveAllListeners();
        ui_selectButton.onClick.AddListener(() => Select());
    }

    public void Select()
    {
        if(!isSelect)
        {
            ui_selectImage.gameObject.SetActive(true);
            ui_characterImage.color = selectedColor;
            isSelect = true;

            _ui_charactersList.Select(this);
        }
    }

    public void UnSelect()
    {
        if(isSelect)
        {
            ui_selectImage.gameObject.SetActive(false);
            ui_characterImage.color = unselectedColor;
            isSelect = false;
        }
    }

    public Player GetCharacter() => _player;
    public bool IsSelect() => isSelect;
}