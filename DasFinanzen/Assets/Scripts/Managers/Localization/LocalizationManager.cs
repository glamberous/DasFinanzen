using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Localization;
using System.Text.RegularExpressions;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class LocalizationManager : MonoBehaviour {
    public LocalizationManagerHumble Manager { get; private set; } = new LocalizationManagerHumble();

    private void Awake() {
        SceneManager.sceneLoaded += Manager.OnSceneLoaded;
    }
}

public class LocalizationManagerHumble : IManager {
    private Dictionary<int, string> LocalizedDictionary = null;

    private Regex KeyRegex = new Regex(@"^\[\d+\]$");
    private char[] BracketChars = new char[] { '[', ']' };

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        foreach (TextMeshProUGUI textMesh in Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI)) as TextMeshProUGUI[])
            AssessAndReplaceString(textMesh);
    }

    private void AssessAndReplaceString(TextMeshProUGUI textMesh) {
        if (KeyRegex.IsMatch(textMesh.text))
            textMesh.text = GetString(GetKey(textMesh.text));
    }

    private int GetKey(string input) => Convert.ToInt32(input.Trim(BracketChars));

    public string GetString(int key) {
        if (LocalizedDictionary.ContainsKey(key))
            return LocalizedDictionary[key];
        else {
            Debug.Log("[WARNING] String key [{key}] missing.");
            return $"[{key}] KEY MISSING";
        }
    }

    public Dictionary<int, string> GetStringDict(int[] keys) {
        Dictionary<int, string> newDict = new Dictionary<int, string>();
        foreach (int key in keys)
            newDict[key] = GetString(key);
        return newDict;
    }

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

        foreach (string line in textFileLines) {
            if (KeyRegex.IsMatch(line)) {
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
        //Messenger.Broadcast(Events.LOCALE_CHANGED); // Change this to scene reload.
    }
}