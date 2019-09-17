using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ErrorManagerBehaviour : MonoBehaviour {
    // Initialization Variables
    [SerializeField] private Image ErrorImage = null;
    [SerializeField] private TextMeshProUGUI ErrorText = null;

    public ErrorManager Manager { get; private set; }
    private void Awake() => Manager = new ErrorManager(ErrorImage, ErrorText);
}

public class ErrorManager : ManagerInterface {
    private Image ErrorImage = null;
    private TextMeshProUGUI ErrorText = null;

    public ErrorManager(Image errorImage, TextMeshProUGUI errorText) {
        ErrorImage = errorImage;
        ErrorText = errorText;
    }

    public ManagerStatus status { get; private set; }
    public void Startup() {
        Debug.Log("Error manager starting...");

        status = ManagerStatus.Started;
    }

    public Image TryGetImage(Image testForNull) {
        if (testForNull != null)
            return testForNull;
        Debug.Log("ERROR: Could not find instance of Image componant! Using 'ErrorImage' instead.");
        return ErrorImage;
    }

    public TextMeshProUGUI TryGetTextMeshProUGUI(TextMeshProUGUI testForNull) {
        if (testForNull != null)
            return testForNull;
        Debug.Log("ERROR: Could not find instance of TextMeshProUGUI componant! Using 'ErrorText' instead.");
        return ErrorText;
    }
}
