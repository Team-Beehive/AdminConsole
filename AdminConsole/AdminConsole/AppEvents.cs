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
    static class AppEvents
    {
        
        public static void ButtonPressBuildingSelect(object sender, EventArgs e)
        {
            BuildingButton temp = sender as BuildingButton;
            AppData.s_activeBuilding = temp.data;
            if (AppData.s_previewPanel.Children.Count > 1)
            {
                AppData.s_previewPanel.Children.RemoveAt(1);
            }
            AppData.s_building.updateLocalBuildings();
            AppData.s_previewPanel.Children.Add(new BuildingPreviewControl());
        }

        //Runs when a major button is pressed
        //Get page info and display a preview
        public static void ButtonPressPage(object sender, EventArgs e)
        {
            FrameworkElement page = sender as FrameworkElement;
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
                Utilities.QueryPageData(page.Name);
                CreateElements.ShowPreview();
            }
        }

        //When a page element is selected open the properties panel for that element
        //Set for modyfying text elements
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
