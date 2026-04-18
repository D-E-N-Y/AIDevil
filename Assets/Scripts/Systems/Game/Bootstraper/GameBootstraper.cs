using UnityEngine;

public class GameBootstraper : MonoBehaviour
{
    [SerializeField] private GameUICanvas gameUICanvas;
    [SerializeField] private SessionSystem sessionSystem;
    
    [SerializeField] private CameraOrigin cameraOrigin;
    private PlayerCharacter playerCharacter;

    private GameInstance _gameInstance;

    private void Start()
    {
        _gameInstance = GameInstance.current;

        InitializePlayer();

        gameUICanvas.Initialize(playerCharacter);

        sessionSystem.Initialize(_gameInstance, gameUICanvas, playerCharacter);
        sessionSystem.StartSession();
    }

    private void InitializePlayer()
    {
        playerCharacter = Instantiate(
            _gameInstance.DataBase.Characters.GetCharacterByID(
                _gameInstance.ProfileManager.CurrentProfile.CharacterManager.Character_ID
            )
        );
        playerCharacter.transform.position = new Vector3(0f, 1f, 0f);
        
        playerCharacter.Initialize(gameUICanvas.UIGameplay.UIJoystick); 
        
        playerCharacter.UpgradesManager.ApplyUpgrades(
            _gameInstance.ProfileManager.CurrentProfile.CharacterManager.UpgradeContainer.Upgrades_ID, 
            _gameInstance.DataBase.UpgradeTrees
        );

        cameraOrigin.Initialize(playerCharacter.transform);
    }
}