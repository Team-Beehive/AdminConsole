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
        public static void ContextItemRemoveBuilding(object sender, EventArgs e)
        {
            BuildingMenuItem temp = sender as BuildingMenuItem;
            AppData.s_building.RemoveBuilding(temp.data);
            AppData.s_buildingList.Remove(temp.data);
            AppData.s_listPanel.Children.RemoveAt(1);
            AppData.s_previewPanel.Children.RemoveAt(1);
            AppData.s_listPanel.Children.Add(CreateElements.CreateBuildingButtons());
        }
        public static void ButtonPressAddBuilding(object sender, EventArgs e)
        {
            BuildingButton temp = sender as BuildingButton;
            AppData.s_activeBuilding = temp.data;
            if (AppData.s_previewPanel.Children.Count > 1)
            {
                AppData.s_previewPanel.Children.RemoveAt(1);
            }
            AppData.s_previewPanel.Children.Add(new BuildingPreviewControl());
            AppData.s_buildingList.AddLast(temp.data);
            AppData.s_listPanel.Children.RemoveAt(1);
            AppData.s_listPanel.Children.Add(CreateElements.CreateBuildingButtons());
            AppData.s_building.CreateBuilding(temp.data);
        }
        public static void ContextItemRemoveProf(object sender, EventArgs e)
        {
            ProfMenuItem temp = sender as ProfMenuItem;
            AppData.s_professors.RemoveProfessor(temp.data);
            AppData.s_professorList.Remove(temp.data);
            AppData.s_listPanel.Children.RemoveAt(1);
            AppData.s_previewPanel.Children.RemoveAt(1);
            AppData.s_listPanel.Children.Add(CreateElements.CreateProfButtons());

        }
        public static void ButtonPressAddProf(object sender, EventArgs e)
        {
            ProfessorButton temp = sender as ProfessorButton;
            AppData.s_activeProfessor = temp.data;
            if (AppData.s_previewPanel.Children.Count > 1)
            {
                AppData.s_previewPanel.Children.RemoveAt(1);
            }
            AppData.s_previewPanel.Children.Add(new ProfessorsPreviewControl());
            AppData.s_professorList.AddLast(temp.data);
            AppData.s_listPanel.Children.RemoveAt(1);
            AppData.s_listPanel.Children.Add(CreateElements.CreateProfButtons());
            AppData.s_professors.CreateProfessor(temp.data);
        }
        public static void ButtonPressProf(object sender, EventArgs e)
        {
            ProfessorButton temp = sender as ProfessorButton;
            AppData.s_activeProfessor = temp.data;
            if (AppData.s_previewPanel.Children.Count > 1)
            {
                AppData.s_previewPanel.Children.RemoveAt(1);
            }
            AppData.s_previewPanel.Children.Add(new ProfessorsPreviewControl());
        }
        
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

                AppData.s_activeData = page.major;

                CreateElements.ShowPreview();
            }
        }

    }
}   
