
using UnityEngine;
using UnityEngine.UI;

public class LanguageFlagSelector : MonoBehaviour
{
    public Image flag;
    public Sprite[] languageSprites;
    private void OnEnable()
    {
        if (EncryptedPlayerPrefs.GetInt("LanguageSelected") >= 0)
        flag.sprite = languageSprites[EncryptedPlayerPrefs.GetInt("LanguageSelected")];
    }
}
