using UnityEngine;
using UnityEngine.UI;

public class UI_Character : UI_Panel 
{
    [SerializeField] private Button ui_selectButton;
    
    [SerializeField] private Image ui_characterImage;
    [SerializeField] private Image ui_selectImage;
    [SerializeField] private Image ui_unselectImage;
    private PlayerCharacter _playerCharacter;
    private bool isSelect;

    private UI_CharactersList _ui_charactersList;

    public void Initialize(PlayerCharacter playerCharacter, UI_CharactersList ui_charactersList)
    {
        _playerCharacter = playerCharacter;
        _ui_charactersList = ui_charactersList;

        ui_selectButton.onClick.RemoveAllListeners();
        ui_selectButton.onClick.AddListener(() => Select());

        UnSelect();
    }

    public void Select()
    {
        isSelect = true;
        
        ui_selectImage.gameObject.SetActive(isSelect);
        ui_unselectImage.gameObject.SetActive(!isSelect);

        _ui_charactersList.Select(this);
    }

    public void SelectWithoutMessage()
    {
        isSelect = true;
        
        ui_selectImage.gameObject.SetActive(isSelect);
        ui_unselectImage.gameObject.SetActive(!isSelect);

        _ui_charactersList.Select(this);
    }

    public void UnSelect()
    {
        isSelect = false;

        ui_selectImage.gameObject.SetActive(isSelect);
        ui_unselectImage.gameObject.SetActive(!isSelect);
    }

    public PlayerCharacter GetCharacter() => _playerCharacter;
    public bool IsSelect() => isSelect;
}