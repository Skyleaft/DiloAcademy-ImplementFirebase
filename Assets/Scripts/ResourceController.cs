using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public Button ResourceButton;
    public Image ResourceImage;
    public Text ResourceDescription;
    public Text ResourceUpgradeCost;
    public Text ResourceUnlockCost;
    private ResourceConfig _config;
    private int _level = 1;

    public bool IsUnlocked { get; private set; }

    private void Start()
    {
        ResourceButton.onClick.AddListener(() =>
        {
            if (IsUnlocked)
            {
                UpgradeLevel();
            }
            else
            {
                UnlockResource();
            }
        });
    }


    public void UpgradeLevel()
    {
        double upgradeCost = GetUpgradeCost();
        if (GameManager.Instance.TotalGold < upgradeCost)
        {
            return;
        }
        GameManager.Instance.AddGold(-upgradeCost);
        _level++;
        ResourceUpgradeCost.text = $"Upgrade Cost\n{ AbbrevationUtility.AbbreviateNumber(GetUpgradeCost()) }";
        ResourceDescription.text = $"{ _config.Name } Lv. { _level }\n+{ AbbrevationUtility.AbbreviateNumber(GetOutput()) }";

    }
    public void SetConfig(ResourceConfig config)
    {
        _config = config;
        // tambahan abbrevationutility untuk format gold yang lebih baik
        ResourceDescription.text = $"{ _config.Name } Lv. { _level }\n+{ AbbrevationUtility.AbbreviateNumber(GetOutput()) }";
        ResourceUnlockCost.text = $"Unlock Cost\n{ AbbrevationUtility.AbbreviateNumber(_config.UnlockCost) }";
        ResourceUpgradeCost.text = $"Upgrade Cost\n{ AbbrevationUtility.AbbreviateNumber(GetUpgradeCost()) }";

        SetUnlocked(_config.UnlockCost == 0);
    }

    public double GetOutput()
    {
        return _config.Output * _level;
    }



    public double GetUpgradeCost()
    {
        return _config.UpgradeCost * _level;
    }



    public double GetUnlockCost()
    {
        return _config.UnlockCost;
    }

    public void UnlockResource()
    {
        double unlockCost = GetUnlockCost();
        if (GameManager.Instance.TotalGold < unlockCost)
        {
            return;
        }
        SetUnlocked(true);
        GameManager.Instance.ShowNextResource();
        AchievementController.Instance.UnlockAchievement(AchievementType.UnlockResource, _config.Name);

    }

    public void SetUnlocked(bool unlocked)
    {
        IsUnlocked = unlocked;
        ResourceImage.color = IsUnlocked ? Color.white : Color.grey;
        ResourceUnlockCost.gameObject.SetActive(!unlocked);
        ResourceUpgradeCost.gameObject.SetActive(unlocked);
    }
}
