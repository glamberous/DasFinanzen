using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UI {
    public class ModelCollector {
        private FileDataQueries Queries;
        private RuntimeData Runtime;
        public ModelCollector(FileDataQueries queries, RuntimeData runtime) {
            Queries = queries;
            Runtime = runtime;
        }

        public Catagory_ModelCollection GetCatagory() {
            Catagory_ModelCollection ModelCollection = new Catagory_ModelCollection();
            ModelCollection.CatagoryModels = Queries.GetCatagoryModels();
            ModelCollection.ExpenseModels = Queries.GetExpenseModels(Runtime.SelectedTime);
            return ModelCollection;
        }

        public ColorBar_ModelCollection GetColorBar() {
            ColorBar_ModelCollection ModelCollection = new ColorBar_ModelCollection();
            ModelCollection.CatagoryModels = Queries.GetCatagoryModels();
            ModelCollection.ExpenseModels = Queries.GetExpenseModels(Runtime.SelectedTime);
            ModelCollection.Goal = Queries.GetGoalModel();
            return new UI.ColorBar_ModelCollection();
        }

        public Goal_ModelCollection GetGoal() {
            Goal_ModelCollection ModelCollection = new Goal_ModelCollection();
            ModelCollection.GoalModel = Queries.GetGoalModel();
            ModelCollection.CurrentMonthExpenses = Queries.GetExpenseModels(Runtime.SelectedTime);
            return ModelCollection;
        }
    }
}

