using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
//using System.Diagnostics;

public class EncryptedPlayerPrefs : MonoBehaviour
{
    [SerializeField]
    private string key;

    private static string encryptionKey;


    private static EncryptedPlayerPrefs Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (!Instance)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        encryptionKey = key;
    }
    public static void SetInt(string key, int value)
    {
        //Debug.Log("Setting int " + key);
        var desEncryption = new DESEncryption();
        PlayerPrefs.SetString(GenerateMD5(key), desEncryption.Encrypt(value.ToString(), encryptionKey));
    }
    public static int GetInt(string key)
    {
        //Debug.Log("Getting int " + key);
        string hashedKey = GenerateMD5(key);
        if (PlayerPrefs.HasKey(hashedKey))
        {
            var desEncryption = new DESEncryption();
            string decryptedValue;
            desEncryption.TryDecrypt(PlayerPrefs.GetString(hashedKey), encryptionKey, out decryptedValue);
            return int.Parse(decryptedValue);
        }
        else
        {
            SetInt(key, 0);
            return 0;
        }
    }
    public static int GetInt(string key, int defaultValue)
    {
        //Debug.Log("Getting int 1 " + key);
        if (PlayerPrefs.HasKey(GenerateMD5(key)))
        {
            return GetInt(key);
        }
        else
        {
            SetInt(key, defaultValue);
            return defaultValue;
        }
    }
    public static void SetFloat(string key, float value)
    {
        //Debug.Log("Setting Float " + key);
        var desEncryption = new DESEncryption();
        PlayerPrefs.SetString(GenerateMD5(key), desEncryption.Encrypt(value.ToString(), encryptionKey));
    }
    public static float GetFloat(string key)
    {
        //Debug.Log("Getting Float " + key);
        string hashedKey = GenerateMD5(key);
        if (PlayerPrefs.HasKey(hashedKey))
        {
            var desEncryption = new DESEncryption();
            string decryptedValue;
            desEncryption.TryDecrypt(PlayerPrefs.GetString(hashedKey), encryptionKey, out decryptedValue);
            return float.Parse(decryptedValue);
        }
        else
        {
            SetFloat(key, 0);
            return 0;
        }
    }
    public static float GetFloat(string key, float defaultValue)
    {
        //Debug.Log("Getting Float 1 " + key);
        string hashedKey = GenerateMD5(key);
        if (PlayerPrefs.HasKey(GenerateMD5(key)))
        {
            return GetFloat(key);
        }
        else
        {
            SetFloat(key, defaultValue);
            return defaultValue;
        }
    }
    public static void SetString(string key, string value)
    {
        //Debug.Log("Setting String " + key);
        var desEncryption = new DESEncryption();
        PlayerPrefs.SetString(GenerateMD5(key), desEncryption.Encrypt(value, encryptionKey));
    }
    public static string GetString(string key)
    {
        //Debug.Log("Getting String " + key);
        string hashedKey = GenerateMD5(key);
        if (PlayerPrefs.HasKey(hashedKey))
        {
            var desEncryption = new DESEncryption();
            string decryptedValue;
            desEncryption.TryDecrypt(PlayerPrefs.GetString(hashedKey), encryptionKey, out decryptedValue);
            return decryptedValue;
        }
        else
        {
            SetString(key, "");
            return "";
        }
    }
    public static string GetString(string key, string defaultValue)
    {
        //Debug.Log("Getting String 1 " + key);
        string hashedKey = GenerateMD5(key);
        if (PlayerPrefs.HasKey(GenerateMD5(key)))
        {
            return GetString(key);
        }
        else
        {
            SetString(key, defaultValue);
            return defaultValue;
        }
    }
    public static bool HasKey(string key)
    {
        //Debug.Log("Checking Key " + key);
        return PlayerPrefs.HasKey(GenerateMD5(key));
    }
    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(GenerateMD5(key));
    }
    /// <summary>
     /// Generates an MD5 hash of the given text.
     /// WARNING. Not safe for storing passwords
     /// </summary>
     /// <returns>MD5 Hashed string</returns>
     /// <param name="text">The text to hash</param>
    static string GenerateMD5(string text)
    {
        var md5 = MD5.Create();
        byte[] inputBytes = Encoding.UTF8.GetBytes(text);
        byte[] hash = md5.ComputeHash(inputBytes);

        // step 2, convert byte array to hex string
        var sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }
}