using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    // Text Fields
    public Text levelText, hitpointText, coinText, upgradeCostText;

    // Logic
    public Image weaponSprite;
    public Image weaponUpgradeSprite;
    public RectTransform xpBar;

    // Weapon Upgrade
    public void OnUpgradeClick()
    {
        // Update Menu if we upgrade our weapon
        if(GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    // Update the character info
    public void UpdateMenu()
    {
        // Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        weaponUpgradeSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel + 1];
        if (GameManager.instance.GameManagerWeaponLevel == 6)
            upgradeCostText.text = "MAX";
        else 
            upgradeCostText.text = "Upgrade Weapon    ( " + GameManager.instance.weaponPrices[GameManager.instance.GameManagerWeaponLevel].ToString() + " )";

        // Meta
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        coinText.text = GameManager.instance.coins.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        // xp Bar
        int currLevel = GameManager.instance.GetCurrentLevel();
        if(currLevel == GameManager.instance.xpTable.Count)
        {
            xpBar.localScale = Vector3.one;
        } else {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
        }
    }
}
