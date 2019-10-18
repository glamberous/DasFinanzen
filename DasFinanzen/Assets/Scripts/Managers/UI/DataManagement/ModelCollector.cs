using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class ModelCollector {
        private ISaveLoad SaveLoad;
        public ModelCollector(ISaveLoad saveLoad) => SaveLoad = saveLoad;

        public Catagory_ModelCollection GetCatagory(string month = "CurrentMonth") {
            Catagory_ModelCollection ModelCollection = new Catagory_ModelCollection();
            ModelCollection.CatagoryModels = SaveLoad.GetCatagoryModels();
            ModelCollection.ExpenseModels = SaveLoad.GetExpenseModels();
            return ModelCollection;
        }

        public ColorBar_ModelCollection GetColorBar() {
            ColorBar_ModelCollection ModelCollection = new ColorBar_ModelCollection();
            ModelCollection.CatagoryModels = SaveLoad.GetCatagoryModels();
            ModelCollection.ExpenseModels = SaveLoad.GetExpenseModels();
            ModelCollection.Goal = SaveLoad.GetGoal();
            return new UI.ColorBar_ModelCollection();
        }

        public Remaining_ModelCollection GetRemaining() {

            return new UI.Remaining_ModelCollection();
        }
    }
}

