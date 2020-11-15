using UnityEngine;

public static class Serialization
{
    public static void Save(string key, int value)
    {
        Log.Message($"Сериализация {typeof(int)} с ключом {key}. Значение: {value}");

        PlayerPrefs.SetInt(key, value);
    }

    public static int Load(string key, int defaultValue)
    {
        if (PlayerPrefs.HasKey(key) == true)
        {
            int value = PlayerPrefs.GetInt(key, defaultValue);

            Log.Message($"Десериализация {typeof(int)} с ключом {key}. Значение: {value}");

            return value;
        }
        else
        {
            Save(key, defaultValue);
            Load(key, defaultValue);

            return defaultValue;
        }
    }
}