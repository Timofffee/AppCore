using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.MetaGame.Models;
using Krem.JetPack.ScriptableORM.Models;
using TMPro;
using UnityEngine;

namespace Krem.JetPack.MetaGame.Components
{
    [NodeGraphGroupName("Jet Pack/Meta Game")]
    [DisallowMultipleComponent]
    public class MetaUpgradeButton : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected IntScriptableModel _currentLevel;
        [SerializeField, NotNull] protected MetaUpgradeSettingsScriptableModel _settings;

        [Header("Components")]
        [SerializeField, NotNull] protected TMP_Text _currentLevelText;
        [SerializeField, NotNull] protected TMP_Text _upgradePriceText;
        [SerializeField, NotNull] protected TMP_Text _enoughMoneyText;
        [SerializeField, NotNull] protected TMP_Text _notEnoughMoneyText;

        [Header("States")]
        [SerializeField] protected bool _isMoneyEnoughState = true;

        public IntScriptableModel CurrentLevel => _currentLevel;
        public MetaUpgradeSettingsScriptableModel Settings => _settings;
        public TMP_Text CurrentLevelText => _currentLevelText;
        public TMP_Text UpgradePriceText => _upgradePriceText;

        public bool IsMoneyEnoughState
        {
            get => _isMoneyEnoughState;
            set
            {
                if (_isMoneyEnoughState == value)
                {
                    return;
                }

                _isMoneyEnoughState = value;

                if (_isMoneyEnoughState)
                {
                    _enoughMoneyText.gameObject.SetActive(true);
                    _notEnoughMoneyText.gameObject.SetActive(false);
                }
                else
                {
                    _enoughMoneyText.gameObject.SetActive(false);
                    _notEnoughMoneyText.gameObject.SetActive(true);
                }
            }
        }

        public int UpgradePrice =>
            Settings.Model.InitialPrice + Settings.Model.PerLevelPrice * CurrentLevel.Model.Value;
    }
}