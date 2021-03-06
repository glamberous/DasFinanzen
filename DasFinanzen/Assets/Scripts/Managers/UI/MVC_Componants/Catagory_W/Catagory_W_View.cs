﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI {
    public class Catagory_W_View : MonoBehaviour, IView {
        [SerializeField] private TextMeshProUGUI TitleText = null;
        [SerializeField] private Button_Void BackButton = null;
        [SerializeField] private Button_Void AddExpense = null;

    private Catagory_W_HumbleView HumbleView = null;
        public void Awake() {
            HumbleView = new Catagory_W_HumbleView();

            BackButton.SetAction(Controller.Instance.Pop, null);
            AddExpense.SetAction(Controller.Instance.PushAddExpenseWindow, null);
        }

        public void Activate() {
            HumbleView.ConstructView(new Catagory_W_ModelCollection(), TitleText);
            //Add any Listeners needed here.
            Debug.Log("Catagory_W_View Activated.");
        }

        public void Refresh() => HumbleView.RefreshView(new Catagory_W_ModelCollection());

        public void Deactivate() {
            HumbleView.DeconstructView();
            //Remove any Listeners needed here.
            Debug.Log("Catagory_W_View Deactivated.");
        }
    }

    public class Catagory_W_HumbleView {
        private TextMeshProUGUI Title = null;

        public void ConstructView(Catagory_W_ModelCollection modelCollection, TextMeshProUGUI titleText) {
            Title = titleText;
            RefreshView(modelCollection);
        }

        public void RefreshView(Catagory_W_ModelCollection modelCollection) {
            Title.text = modelCollection.SelectedCatagory.NameText;
        }

        public void DeconstructView() {

        }
    }

    public class Catagory_W_ModelCollection {
        // Put Model Collections Here
        public CatagoryModel SelectedCatagory = DataQueries.GetCatagoryModel(Managers.Data.FileData.CatagoryModels, Managers.Data.Runtime.CurrentCatagoryID);
    }
}
