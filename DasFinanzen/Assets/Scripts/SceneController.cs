using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneController : MonoBehaviour
{
    public const float CatagoryOffset = 30.0f;

    [SerializeField] private GameObject dailyParent = null;
    [SerializeField] private GameObject monthlyParent = null;
    [SerializeField] private Catagory dailyInitial = null;
    [SerializeField] private Catagory monthlyInitial = null;
    [SerializeField] private CatagoryData[] dailyData = null;
    [SerializeField] private CatagoryData[] monthlyData = null;

    void InitializeCatagories(CatagoryData[] catagoryDatas, Catagory catagoryInitial, RectTransform catagoryMask) {
        Vector3 startPos = catagoryInitial.transform.localPosition;
        for (int i = 0; i < catagoryDatas.Length; i++) {
            Catagory newCatagory;
            if (i == 0)
                newCatagory = catagoryInitial;
            else {
                newCatagory = Instantiate(catagoryInitial) as Catagory;
                catagoryMask.sizeDelta = new Vector2(catagoryMask.sizeDelta.x, catagoryMask.sizeDelta.y + CatagoryOffset);
                newCatagory.transform.localPosition = new Vector3(startPos.x, startPos.y - (CatagoryOffset * i), startPos.z);
            }
            newCatagory.LoadData(catagoryDatas[i]);
        }
    }

    void Start() {
        InitializeCatagories(dailyData, dailyInitial, dailyParent.GetComponent<RectTransform>());
        InitializeCatagories(monthlyData, monthlyInitial, monthlyParent.GetComponent<RectTransform>());
    }
}


