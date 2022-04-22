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

    public class AppEvents
    {
        private AppData m_data;
        private Utilities m_util;
        private CreateElements m_elements;
        public AppEvents(AppData data, Utilities util)
        {
            m_data = data;
            m_util = util;
        }

        public void SetCreateElements(CreateElements elements)
        {
            m_elements = elements;
        }

        public void ContextItemRemoveBuilding(object sender, EventArgs e)
        {
            BuildingMenuItem temp = sender as BuildingMenuItem;
            m_data.s_building.RemoveBuilding(temp.data);
            m_data.s_buildingList.Remove(temp.data);
            m_util.ClearList();
            m_util.ClearPreview();
            m_data.buildingButtonList = m_elements.CreateBuildingButtons(m_data.s_buildingList);
            m_util.SetList(m_data.buildingButtonList);
            /*AppData.s_building.RemoveBuilding(temp.data);
            AppData.s_buildingList.Remove(temp.data);
            AppData.s_listPanel.Children.RemoveAt(1);
            AppData.s_previewPanel.Children.RemoveAt(1);
            AppData.s_listPanel.Children.Add(CreateElements.CreateBuildingButtons());*/
        }
        public void ContextItemRemoveProf(object sender, EventArgs e)
        {
            ProfMenuItem temp = sender as ProfMenuItem;
            m_data.s_professors.RemoveProfessor(temp.data);
            m_data.s_professorList.Remove(temp.data);
            m_util.ClearList();
            m_util.ClearPreview();
            m_data.profButtonList = m_elements.CreateProfButtons(m_data.s_professorList);
            m_util.SetList(m_data.profButtonList);
            /*AppData.s_professors.RemoveProfessor(temp.data);
            AppData.s_professorList.Remove(temp.data);
            AppData.s_listPanel.Children.RemoveAt(1);
            AppData.s_previewPanel.Children.RemoveAt(1);
            AppData.s_listPanel.Children.Add(CreateElements.CreateProfButtons());*/

        }

        public void ContextItemRemoveCat(object sender, EventArgs e)
        {
            CatMenuItem temp = sender as CatMenuItem;
            m_data.s_major.DeleteMajorCategory(temp.data);
            m_data.s_catList.Remove(temp.data);
            m_util.ClearList();
            m_elements.AddCatButtons(m_data.s_catList);
        }

        public void ButtonPressAddBuilding(object sender, EventArgs e)
        {
            BuildingButton temp = sender as BuildingButton;
            m_data.s_activeBuilding = temp.data;
            m_util.ClearPreview();
            m_util.SetPreview(new BuildingPreviewControl(m_data, m_util));
            m_data.s_buildingList.AddLast(temp.data);
            m_util.ClearList();
            m_data.buildingButtonList = m_elements.CreateBuildingButtons(m_data.s_buildingList);
            m_util.SetList(m_data.buildingButtonList);
            m_data.s_addedBuildingList.Add(temp.data);
            //m_data.s_building.CreateBuilding(temp.data);
            /*AppData.s_activeBuilding = temp.data;
            if (AppData.s_previewPanel.Children.Count > 1)
            {
                AppData.s_previewPanel.Children.RemoveAt(1);
            }
            AppData.s_previewPanel.Children.Add(new BuildingPreviewControl());
            AppData.s_buildingList.AddLast(temp.data);
            AppData.s_listPanel.Children.RemoveAt(1);
            AppData.s_listPanel.Children.Add(CreateElements.CreateBuildingButtons());
            AppData.s_building.CreateBuilding(temp.data);*/
        }
        public void ButtonPressAddProf(object sender, EventArgs e)
        {
            ProfessorButton temp = sender as ProfessorButton;
            m_data.s_activeProfessor = temp.data;
            m_util.ClearPreview();
            m_util.SetPreview(new ProfessorsPreviewControl(m_data, m_util));
            m_data.s_professorList.AddLast(temp.data);
            m_util.ClearList();
            m_data.profButtonList = m_elements.CreateProfButtons(m_data.s_professorList);
            m_util.SetList(m_data.profButtonList);
            m_data.s_professors.CreateProfessor(temp.data);
            /*AppData.s_activeProfessor = temp.data;
            if (AppData.s_previewPanel.Children.Count > 1)
            {
                AppData.s_previewPanel.Children.RemoveAt(1);
            }
            AppData.s_previewPanel.Children.Add(new ProfessorsPreviewControl());
            AppData.s_professorList.AddLast(temp.data);
            AppData.s_listPanel.Children.RemoveAt(1);
            AppData.s_listPanel.Children.Add(CreateElements.CreateProfButtons());
            AppData.s_professors.CreateProfessor(temp.data);*/
        }
        public void ButtonPressNewCat(object sender, EventArgs e)
        {
            m_util.ClearProperties();
            m_util.SetProperties(new CatProp(m_util, m_data));
            /*if (AppData.s_propertiesPanel.Children.Count > 1)
            {
                AppData.s_propertiesPanel.Children.RemoveAt(1);
            }
            AppData.s_propertiesPanel.Children.Add(new NewCatProp());*/
        }
        public void ButtonPressProf(object sender, EventArgs e)
        {
            ProfessorButton temp = sender as ProfessorButton;
            m_data.s_activeProfessor = temp.data;
            m_util.ClearProperties();
            m_util.ClearPreview();
            m_data.s_activeProfessor = temp.data;
            m_util.SetPreview(new ProfessorsPreviewControl(m_data, m_util));
            /*AppData.s_activeProfessor = temp.data;
            if (AppData.s_previewPanel.Children.Count > 1)
            {
                AppData.s_previewPanel.Children.RemoveAt(1);
            }
            AppData.s_previewPanel.Children.Add(new ProfessorsPreviewControl());*/
        }    
        public void ButtonPressBuildingSelect(object sender, EventArgs e)
        {
            BuildingButton temp = sender as BuildingButton;
            m_data.s_activeBuilding = temp.data;
            m_util.ClearProperties();
            m_util.ClearPreview();
            m_data.s_building.updateLocalBuildings();
            m_data.s_activeBuilding = temp.data;
            m_util.SetPreview(new BuildingPreviewControl(m_data, m_util));

            /*AppData.s_activeBuilding = temp.data;
            if (AppData.s_previewPanel.Children.Count > 1)
            {
                AppData.s_previewPanel.Children.RemoveAt(1);
            }
            AppData.s_building.updateLocalBuildings();
            AppData.s_previewPanel.Children.Add(new BuildingPreviewControl());*/
        }
        public void ButtonPressCatProp(object sender, EventArgs e)
        {
            CatButton temp = sender as CatButton;
            m_data.s_activeCat = temp.category;
            m_util.SetProperties(new CatProp(m_util, m_data));
            /*AppData.s_activeCat = temp.category;
            CreateElements.AddCatProp();
            AppData.s_properties.Text = temp.category.categoryTitle;*/
        }
        public void ButtonPressUpdateCat(object sender, EventArgs e)
        {
            m_data.s_major.EditMajorCatagoryTitle(m_data.s_activeCat);
            m_elements.AddCatButtons(m_data.s_catList);
            /*AppData.s_major.EditMajorCatagoryTitle(AppData.s_activeCat);
            CreateElements.AddCatButtons(AppData.s_catList);*/
        }
        public void TextBoxCatName(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            m_data.s_activeCat.categoryTitle = tb.Text;
            //AppData.s_activeCat.categoryTitle = tb.Text;
        }
        public void ButtonPressPage(object sender, EventArgs e)
        {
            MajorButton page = sender as MajorButton;
            m_util.ClearProperties();
            m_util.ClearPreview();
            m_data.s_major.UpdateLocal();
            m_data.s_activeData = page.major;
            m_util.SetPreview(new MajorPreviewControl(m_data, m_util));
            m_util.SetProperties(new CategorySelect(m_data, page.major));

            /*AppData.s_activeData = page.major;
            if (AppData.s_previewPanel.Children.Count > 1)
            {
                AppData.s_previewPanel.Children.RemoveAt(1);
            }
            AppData.s_major.UpdateLocal();
            AppData.s_previewPanel.Children.Add(new MajorPreviewControl());*/
        }
    }
}   
