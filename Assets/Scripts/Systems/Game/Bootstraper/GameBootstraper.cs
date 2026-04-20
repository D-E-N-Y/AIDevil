using UnityEngine;

public class GameBootstraper : MonoBehaviour
{
    [SerializeField] private GameUICanvas gameUICanvas;
    [SerializeField] private SessionSystem sessionSystem;
    
    [SerializeField] private CameraOrigin cameraOrigin;
    [SerializeField] private Transform playerSpawnPoint;
    private PlayerCharacter playerCharacter;

    private GameInstance _gameInstance;

    private void Start()
    {
        _gameInstance = GameInstance.current;

        InitializePlayer();

        gameUICanvas.Initialize(playerCharacter);
        gameUICanvas.gameObject.SetActive(true);

        sessionSystem.Initialize(_gameInstance, gameUICanvas, playerCharacter);
        sessionSystem.StartSession();
    }

    private void InitializePlayer()
    {
        PlayerCharacter character = _gameInstance.DataBase.Characters.GetCharacterByID(
            _gameInstance.ProfileManager.CurrentProfile.CharacterManager.Character_ID
        );
        
        playerCharacter = Instantiate(character, playerSpawnPoint.position, Quaternion.identity);
        playerCharacter.Initialize(gameUICanvas.UIGameplay.UIJoystick); 
        
        playerCharacter.UpgradesManager.ApplyUpgrades(
            _gameInstance.ProfileManager.CurrentProfile.CharacterManager.UpgradeContainer.Upgrades_ID, 
            _gameInstance.DataBase.UpgradeTrees
        );

        cameraOrigin.Initialize(playerCharacter.transform);
    }
}