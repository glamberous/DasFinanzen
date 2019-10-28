using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DefaultDataGenerator {
    public static void LoadAll() {
        LoadCatagoryModels();
    }

    private static void LoadCatagoryModels() {
        if (Managers.Data.FileData.CatagoryModels.Count == 0) {
            CatagoryModel Catagory1 = new CatagoryModel();
            Catagory1.ColorCode = "e06c75";
            Catagory1.NameText = "Roth IRA";
            Catagory1.Recurring = true;
            Catagory1.Save();

            CatagoryModel Catagory2 = new CatagoryModel();
            Catagory2.ColorCode = "61afef";
            Catagory2.NameText = "Restaurant";
            Catagory2.Recurring = false;
            Catagory2.Save();

            CatagoryModel Catagory3 = new CatagoryModel();
            Catagory3.ColorCode = "ffb86c";
            Catagory3.NameText = "Mortgage";
            Catagory3.Recurring = true;
            Catagory3.Save();

            CatagoryModel Catagory4 = new CatagoryModel();
            Catagory4.ColorCode = "acce1b";
            Catagory4.NameText = "Miscellaneous";
            Catagory4.Recurring = false;
            Catagory4.Save();


            CatagoryModel Catagory5 = new CatagoryModel();
            Catagory5.ColorCode = "c678dd";
            Catagory5.NameText = "HOA";
            Catagory5.Recurring = true;
            Catagory5.Save();

            CatagoryModel Catagory6 = new CatagoryModel();
            Catagory6.ColorCode = "94B57C";
            Catagory6.NameText = "Groceries";
            Catagory6.Recurring = false;
            Catagory6.Save();

            CatagoryModel Catagory7 = new CatagoryModel();
            Catagory7.ColorCode = "d19a66";
            Catagory7.NameText = "Gas";
            Catagory7.Recurring = false;
            Catagory7.Save();

            CatagoryModel Catagory8 = new CatagoryModel();
            Catagory8.ColorCode = "60B9BD";
            Catagory8.NameText = "Entertainment";
            Catagory8.Recurring = false;
            Catagory8.Save();

            CatagoryModel Catagory9 = new CatagoryModel();
            Catagory9.ColorCode = "FF79C6";
            Catagory9.NameText = "Extra Mortgage";
            Catagory9.Recurring = true;
            Catagory9.Save();
        }
    }
}
