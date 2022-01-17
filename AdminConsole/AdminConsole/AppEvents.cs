using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using AdminDatabaseFramework;

namespace AdminConsole
{
    /*
     * Author: Destiny Dahlgren
     * Purpose: Holds most of the interactive events. Mostly for items that are generated at runtime
     */

    /*
     * Functions:
     * ButtonPressCatProp(object sender, EventArgs e)
     * - Creates a list of buttons from the given category list
     * ButtonPressUpdateCat(object sender, EventArgs e)
     * - Updates the category name locally and remakes the buttons to reflect it
     * TextBoxCatName(object sender, TextChangedEventArgs e)
     * - Triggers when the text box changes. Puts the changes into the variables
     * ButtonPressPage(object sender, EventArgs e)
     * - When a major is selected it gets the relivent info and displays the preview
     * ButtonPressPage(object sender, EventArgs e)
     * - When a page element is selected open its properties panel
     * - Currently only set for modifying the text it contains
     * 
     */

    static class AppEvents
    {
        public static void ButtonPressNewCat(object sender, EventArgs e)
        {
            if (AppData.s_propertiesPanel.Children.Count > 1)
            {
                AppData.s_propertiesPanel.Children.RemoveAt(1);
            }
            AppData.s_propertiesPanel.Children.Add(new NewCatProp());
        }
        
        public static void ButtonPressCatProp(object sender, EventArgs e)
        {
            CatButton temp = sender as CatButton;
            AppData.s_activeCat = temp.category;

            //Button temp = sender as Button;

            /*foreach (MajorCategories c in AppData.s_catList)
            {
                if (c.categoryTitle == temp.Content.ToString())
                {
                    //c.oldTitle = c.categoryTitle;
                    AppData.s_activeCat = c;
                    break;
                }
            }*/
            CreateElements.AddCatProp();
            AppData.s_properties.Text = temp.category.categoryTitle;
        }

        public static void ButtonPressUpdateCat(object sender, EventArgs e)
        {
            AppData.s_major.EditMajorCatagoryTitle(AppData.s_activeCat);
            CreateElements.AddCatButtons(AppData.s_catList);
        }

        public static void TextBoxCatName(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            AppData.s_activeCat.categoryTitle = tb.Text;
        }

        public static void ButtonPressPage(object sender, EventArgs e)
        {
            //FrameworkElement page = sender as FrameworkElement;
            MajorButton page = sender as MajorButton;
            if (AppData.s_lastSelected != page.Name)
            {
                if (AppData.s_lastSelected != "n")
                {
                    Utilities.VolitileSave();
                    AppData.s_previewPanel.Children.RemoveAt(1);
                    //clear text in properties
                    if (AppData.s_propertiesPanel.Children.Count > 1)
                    {
                        AppData.s_propertiesPanel.Children.RemoveAt(1);
                    }

                }
                AppData.s_lastSelected = page.Name;
                //Utilities.QueryPageData(page.Name);
                Utilities.QueryPageData(page.major);
                //MajorButton temp = sender as MajorButton;
                //AppData.s_activeData = page.major;
                CreateElements.ShowPreview();
            }
        }

        public static void ButtonPressSelected(object sender, EventArgs e)
        {
            if (AppData.s_propertiesPanel.Children.Count == 1)
            {
                CreateElements.AddProperties();
            }
            AppData.s_activeElement = sender;
            TextBlock temp = sender as TextBlock;
            if (temp != null)
            {
                AppData.s_properties.Text = temp.Text;
            }
        }

        //When the text is changed in the text box
        public static void TextChangedTest(object sender, TextChangedEventArgs e)
        {
            TextBlock temp = AppData.s_activeElement as TextBlock;
            TextBox tb = sender as TextBox;

            AppData.s_hasChanged = true;

            if (temp != null)
            {
                switch (temp.Name)
                {
                    case "title":
                        AppData.s_activeTitle = tb.Text;
                        break;
                    case "type":
                        AppData.s_activeType = tb.Text;
                        break;
                    case "desc":
                        AppData.s_activeDesc = tb.Text;
                        break;
                    case "classes":
                        AppData.s_activeClasses = tb.Text;
                        break;
                    case "prof":
                        //AppData.s_act
                        break;
                    case "camp":
                        AppData.s_activeCampus = tb.Text;
                        break;
                }
                temp.Text = tb.Text;
            }
        }

        //Test function for button presses
        public static void ButtonPressTest(object sender, EventArgs e)
        {
            Debug.WriteLine("Hello world!");
        }
    }
}   
