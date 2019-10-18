using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class ModelCollector {
        private ISaveLoad SaveLoad;
        public ModelCollector(ISaveLoad saveLoad) => SaveLoad = saveLoad;

        public Catagory_ModelCollection GetCatagory(string month = "CurrentMonth") {
            Catagory_ModelCollection ModelCollection = new Catagory_ModelCollection();
            ModelCollection.CatagoryModels = SaveLoad.LoadCatagories();
            ModelCollection.ExpenseModels = SaveLoad.LoadExpenses();
            return ModelCollection;
        }

        public ColorBar_ModelCollection GetColorBar() {
            ColorBar_ModelCollection ModelCollection = new ColorBar_ModelCollection();
            ModelCollection.CatagoryModels = SaveLoad.LoadCatagories();
            ModelCollection.ExpenseModels = SaveLoad.LoadExpenses();
            ModelCollection.Goal = SaveLoad.LoadGoal();
            return new UI.ColorBar_ModelCollection();
        }

        public Goal_ModelCollection GetRemaining() {
            decimal Goal = SaveLoad.LoadGoal();
            
            return new UI.Goal_ModelCollection();
        }
    }
}

