using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class SceneController : MonoBehaviour
{
    public const float CatagoryOffset = 30.0f;

    [SerializeField] private GameObject dailyParent = null;
    [SerializeField] private GameObject monthlyParent = null;
    [SerializeField] private Catagory dailyInitial = null;
    [SerializeField] private Catagory monthlyInitial = null;
    [SerializeField] private AppData appData = null;

    void InitializeCatagories(CatagoryData[] catagoryDatas, Catagory catagoryOriginal, GameObject catagoryParent) {
        Vector3 startPos = catagoryOriginal.transform.localPosition;
        RectTransform catagoryMask = catagoryParent.GetComponent<RectTransform>();
        for (int i = 0; i < catagoryDatas.Length; i++) {
            Catagory newCatagory;
            if (i == 0)
                newCatagory = catagoryOriginal;
            else {
                catagoryMask.sizeDelta = new Vector2(catagoryMask.sizeDelta.x, catagoryMask.sizeDelta.y + CatagoryOffset);

                newCatagory = Instantiate(original: catagoryOriginal, parent: catagoryParent.transform) as Catagory;
                newCatagory.transform.localPosition = new Vector3(startPos.x, startPos.y - (CatagoryOffset * i), startPos.z);
            }
            newCatagory.Construct(catagoryDatas[i]);
        }
    }

    void Start() {
        InitializeCatagories(appData.DailyCatagoryData, dailyInitial, dailyParent);
        InitializeCatagories(appData.MonthlyCatagoryData, monthlyInitial, monthlyParent);
    }
}


