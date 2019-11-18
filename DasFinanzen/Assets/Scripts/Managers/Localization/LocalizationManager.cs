using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Localization;
using System.Text.RegularExpressions;
using System;

public class LocalizationManager : MonoBehaviour {
    public LocalizationManagerHumble Manager { get; private set; } = new LocalizationManagerHumble();
}

public class LocalizationManagerHumble : IManager {
    private Dictionary<int, string> LocalizedDictionary = null;

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Localization Manager starting...");

        LoadLocale(Managers.Data.FileData.Locale);

        status = ManagerStatus.Started;
        Debug.Log("Localization Manager started.");
    }

    private void LoadLocale(LOCALE locale) {
        switch (locale) {
            case LOCALE.EN: LocalizedDictionary = GetLocaleDictFromFile("EN"); break;
            default: LocalizedDictionary = GetLocaleDictFromFile("EN"); break;
        }
    }

    private Dictionary<int, string> GetLocaleDictFromFile(string LocaleCode) {
        string[] textFileLines = Resources.Load<TextAsset>($"Localization/{LocaleCode}_Locale").text.Split(new char[] { '\n', '\r' });

        Dictionary<int, string> LocaleDict = new Dictionary<int, string>();
        int tempDictKey = -1;
        string tempDictValue = "";

        Regex regexObject = new Regex(@"^\[\d+\]$");
        char[] BracketChars = new char[] { '[', ']' };

        foreach (string line in textFileLines) {
            if (regexObject.IsMatch(line)) {
                LocaleDict[tempDictKey] = tempDictValue.TrimEnd('\n');
                tempDictValue = "";
                tempDictKey = Convert.ToInt32(line.Trim(BracketChars));
            } else
                tempDictValue += line;
        }
        // Ensure final string key in file is added to the dictionary.
        LocaleDict[tempDictKey] = tempDictValue.TrimEnd('\n');
        return LocaleDict;
    }

    public void SetLocale(LOCALE locale) {
        Managers.Data.FileData.Locale = locale;
        LoadLocale(locale);
        Managers.Data.Save();
        Messenger.Broadcast(Events.LOCALE_CHANGED);
    }

    public Dictionary<int, string> GetStringDict(int[] keys) {
        Dictionary<int, string> newDict = new Dictionary<int, string>();
        foreach (int key in keys)
            newDict[key] = GetString(key);
        return newDict;
    }

    private string GetString(int key) {
        if (LocalizedDictionary.ContainsKey(key))
            return LocalizedDictionary[key];
        else {
            Debug.Log("[WARNING] String key [{key}] missing.");
            return $"[{key}] KEY MISSING";
        }
    }
}