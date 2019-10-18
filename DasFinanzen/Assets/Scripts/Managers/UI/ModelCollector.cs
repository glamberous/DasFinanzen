using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class ModelCollector {
        private FileDataQueries Queries;
        public ModelCollector(FileDataQueries queries) => Queries = queries;

        public Catagory_ModelCollection GetCatagory(string month = "CurrentMonth") {
            Catagory_ModelCollection ModelCollection = new Catagory_ModelCollection();
            ModelCollection.CatagoryModels = Queries.GetCatagories();
            ModelCollection.ExpenseModels = Queries.GetExpenses();
            return ModelCollection;
        }

        public ColorBar_ModelCollection GetColorBar() {
            ColorBar_ModelCollection ModelCollection = new ColorBar_ModelCollection();
            ModelCollection.CatagoryModels = Queries.GetCatagories();
            ModelCollection.ExpenseModels = Queries.GetExpenses();
            ModelCollection.Goal = Queries.GetGoal();
            return new UI.ColorBar_ModelCollection();
        }

        public Goal_ModelCollection GetGoal() {
            Goal_ModelCollection ModelCollection = new Goal_ModelCollection();
            ModelCollection.GoalModel = Queries.GetGoal();
            ModelCollection.CurrentMonthExpenses = Queries.GetExpenses();
            return ModelCollection;
        }
    }
}

