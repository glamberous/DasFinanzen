using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Localization;
using System.Text.RegularExpressions;
using System;
using TMPro;

public class LocalizationManager : MonoBehaviour {

    public void Awake() {
        Manager = new LocalizationManagerHumble();
    }
    public LocalizationManagerHumble Manager { get; private set; }
}

public struct LocaleObject {
    public LocaleObject(string input1, TextMeshProUGUI input2) {
        String = input1;
        Mesh = input2;
    }

    public string String;
    public TextMeshProUGUI Mesh;
}

public class LocalizationManagerHumble : IManager {
    private Dictionary<int, LocaleObject> LocaleDict = new Dictionary<int, LocaleObject>();
    //private Dictionary<int, string> LocaleDict = null;
    //private Dictionary<int, TextMeshProUGUI> TextMeshDict = null;

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Localization Manager starting...");

        LoadLocaleDictStrings(Managers.Data.FileData.Locale, Resources.FindObjectsOfTypeAll<TextMeshProUGUI>());
        LoadTextMeshDict();
        Refresh();

        status = ManagerStatus.Started;
        Debug.Log("Localization Manager started.");
    }

    private void LoadLocaleDictStrings(LOCALE locale) {
        switch(locale) {
            case LOCALE.EN: LocaleDict = GetLocaleDictFromFile("EN"); break;
            default: LocaleDict = GetLocaleDictFromFile("EN"); break;
        }
    }

    private Dictionary<int, string> GetLocaleDictFromFile(string LocaleCode) {
        string[] textFileLines = Resources.Load<TextAsset>($"Localization/{LocaleCode}_Locale").text.Split(new char[] { '\n', '\r' });
        LocaleDict = new Dictionary<int, LocaleObject>();

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
        LocaleDict[tempDictKey] = new LocaleObject(tempDictValue.TrimEnd('\n'), null);
    }

    public void SetLocale(LOCALE locale) {
        Managers.Data.FileData.Locale = locale;
        LoadLocaleDict(locale);
        Managers.Data.Save();
        Refresh();
    }

    private void LoadTextMeshDict(TextMeshProUGUI[] textMeshes) {
        foreach (TextMeshProUGUI textMesh in textMeshes)

    }

    public void Refresh() {
        TextMeshProUGUI[] TextMeshes = GameObject.FindObjectsOfTypeAll<TextMeshProUGUI>();
        for (int index = 0; index < TextMeshes.Length; index++)
            TextMeshes[index].text = GetString(TextMeshes[index].text);
    }

    public string GetString(string text) {
        if (Regex.IsMatch(text, @"^\[\d+\]$")) {
            int key = Int32.Parse(Regex.Match(text, @"^\[\d+\]$").Value);
            if (LocaleDict.ContainsKey(key))
                return LocaleDict[key];
            else {
                Debug.Log("[WARNING] String key [{key}] missing.");
                return $"[{key}] KEY MISSING";
            }
        }
        return text;
    }
}
