using UnityEngine;
using UnityEngine.UI;

public class UI_Character : UI_Panel 
{
    [SerializeField] private Button ui_selectButton;
    
    [SerializeField] private Image ui_characterImage;
    [SerializeField] private Image ui_selectImage;
    private Color selectedColor;
    private Color unselectedColor;

    private PlayerCharacter _playerCharacter;
    private bool isSelect;

    private UI_CharactersList _ui_charactersList;

    public void Initialize(PlayerCharacter playerCharacter, UI_CharactersList ui_charactersList)
    {
        _playerCharacter = playerCharacter;
        _ui_charactersList = ui_charactersList;
        
        selectedColor = new Vector4(255f / 255f, 243f / 255f, 208f / 255f, 1f);
        unselectedColor = new Vector4(32f / 255f, 18f / 255f, 6f / 255f, 1f);

        ui_selectButton.onClick.RemoveAllListeners();
        ui_selectButton.onClick.AddListener(() => Select());

        UnSelect();
    }

    public void Select()
    {
        isSelect = true;
        ui_selectImage.gameObject.SetActive(isSelect);
        ui_characterImage.color = selectedColor;

        _ui_charactersList.Select(this);
    }

    public void UnSelect()
    {
        isSelect = false;

        ui_selectImage.gameObject.SetActive(isSelect);
        ui_characterImage.color = unselectedColor;
    }

    public PlayerCharacter GetCharacter() => _playerCharacter;
    public bool IsSelect() => isSelect;
}