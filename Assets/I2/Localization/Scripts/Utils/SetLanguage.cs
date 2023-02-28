using UnityEngine;
namespace I2.Loc
{
    [AddComponentMenu("I2/Localization/SetLanguage Button")]
    public class SetLanguage : MonoBehaviour
    {
        private enum LanguagesString
        {
            English,
            Chinese,
            Russian,
            Spanish,
            Arabic,
            Turkish,
            Italian,
            Portuguese,
            Indonesian,
            German,
            Japanese
        }
        public string _Language;
        public int index;
#if UNITY_EDITOR
        public LanguageSource mSource;
#endif
        void OnClick()
        {
            ApplyLanguage();
        }
        private void OnEnable()
        {
            if (GetComponent<UnityEngine.UI.Dropdown>())
            {
                GetComponent<UnityEngine.UI.Dropdown>().value = EncryptedPlayerPrefs.GetInt("LanguageSelected");
            }
        }
        public void ApplyLanguage()
        {
            if (LocalizationManager.HasLanguage(_Language))
            {
                LocalizationManager.CurrentLanguage = _Language;
            }
            if (MainMenuUI.Instance)
            {
                MainMenuUI.Instance.CheckForLanguageInputFirstTime(index);
                Utility.MakeClickSound();
            }
        }
   public void ApplyLanguageFromSettings()
		{
            if (GetComponent<UnityEngine.UI.Dropdown>())
            {
                int i = GetComponent<UnityEngine.UI.Dropdown>().value;
                LanguagesString val = (LanguagesString)i;
                if (i != EncryptedPlayerPrefs.GetInt("LanguageSelected"))
                {
                    if (LocalizationManager.HasLanguage(val.ToString()))
                    {
                        LocalizationManager.CurrentLanguage = val.ToString();
                    }
                    if (MainMenuUI.Instance)
                    {
                        EncryptedPlayerPrefs.SetInt("LanguageSelected", i);
                    }
                }
            }
        }
    }
}