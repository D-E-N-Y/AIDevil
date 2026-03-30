using System;
using System.Collections.Generic;

public class PurchaseCharacter
{
    public event Action onPurchaseComplete;

    private ProfileManager _profileManager;
    private DB_Characters _db_chracters;

    public PurchaseCharacter(ProfileManager profileManager, DB_Characters db_chracters)
    {
        _profileManager = profileManager;
        _db_chracters = db_chracters;
    }

    public bool CanPurchase(PlayerCharacter character)
    {
        if (character == null) return false;
        
        if (!IsCharacterLocked(character.ID)) return false;
        if (!IsHasEhoughResources(character.Cost)) return false;

        return true;
    }

    private bool IsHasEhoughResources(IReadOnlyList<Cost> costs)
    {
        return _profileManager.CurrentProfile.Wallet.HasEnoughResources(costs);
    }

    private bool IsCharacterLocked(string characterID)
    {
        return !_profileManager.CurrentProfile.CharacterManager.CharacterProgress.IsCharacterUnlocked(characterID);
    }

    public bool CanPurchase(string characterID)
    {
        PlayerCharacter character = _db_chracters.GetCharacterByID(characterID);
        return CanPurchase(character);
    }

    public void Purchase(string characterID)
    {
        PlayerCharacter character = _db_chracters.GetCharacterByID(characterID);

        _profileManager.CurrentProfile.Wallet.RemoveResources(character.Cost);
        _profileManager.CurrentProfile.CharacterManager.CharacterProgress.AddCharacter(characterID);

        onPurchaseComplete?.Invoke();
    }
}