using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AdminDatabaseFramework;
using Google.Cloud.Firestore;

namespace AdminConsole
{
    /*
     * Author: Destiny (Destin) Dahlgren
     * Purpose: This class is responsible for generating UI elements
     */

    public class CreateElements
    {
        private Utilities m_util;
        private AppEvents m_events;
        private AppData m_data;
        
        public CreateElements(Utilities util, AppData data) 
        {
            m_util = util;
            m_data = data;
        }
        public void SetEvents(AppEvents events)
        {
            m_events = events;
        }
        public Grid CreateProfButtons(LinkedList<ProfessorData> professors)
        {
            Grid grid = new Grid();

            int btnPos = 0;
            foreach (ProfessorData p in professors)
            {
                RowDefinition rd = new RowDefinition();
                grid.RowDefinitions.Add(rd);
                string cleanName = m_util.cleanString(p.professorName);

                ProfessorButton btn = new ProfessorButton();
                btn.Content = p.professorName;
                btn.Name = cleanName;
                btn.data = p;
                btn.Click += m_events.ButtonPressProf;
                ContextMenu deleteMenu = new ContextMenu();
                ProfMenuItem deleteItem = new ProfMenuItem();
                deleteItem.data = p;
                deleteItem.Click += m_events.ContextItemRemoveProf;
                deleteItem.Header = "Delete";
                deleteMenu.Items.Add(deleteItem);
                btn.ContextMenu = deleteMenu;

                Grid.SetRow(btn, btnPos);
                btnPos++;
                grid.Children.Add(btn);
            }
  
            RowDefinition rd1 = new RowDefinition();
            grid.RowDefinitions.Add(rd1);
            ProfessorButton add = new ProfessorButton();
            add.Content = "Add professor";
            add.Name = "Add";
            add.data = m_util.NewProf();
            add.Click += m_events.ButtonPressAddProf;
            Grid.SetRow(add, btnPos);
            grid.Children.Add(add);
            return grid;
  
        }

        public Grid CreateBuildingButtons(LinkedList<BuildingData> buildings)
        {
            Grid grid = new Grid();

            int btnPos = 0;
            foreach (BuildingData building in buildings)
            {
                RowDefinition rd = new RowDefinition();
                grid.RowDefinitions.Add(rd);
                string cleanName = m_util.cleanString(building.BuildingName);

                BuildingButton btn = new BuildingButton();
                btn.Content = building.BuildingName;
                btn.Name = cleanName;
                btn.Click += m_events.ButtonPressBuildingSelect;
                btn.data = building;
                ContextMenu deleteMenu = new ContextMenu();
                BuildingMenuItem deleteItem = new BuildingMenuItem();
                deleteItem.data = building;
                deleteItem.Header = "Delete";
                deleteItem.Click += m_events.ContextItemRemoveBuilding;
                deleteMenu.Items.Add(deleteItem);
                btn.ContextMenu = deleteMenu;

                Grid.SetRow(btn, btnPos);
                btnPos++;
                grid.Children.Add(btn);
            }

            grid.RowDefinitions.Add(new RowDefinition());
            BuildingButton add = new BuildingButton();
            add.Content = "Add Building";
            add.Name = "add";
            add.data = m_util.NewBuilding();
            add.Click += m_events.ButtonPressAddBuilding;
            Grid.SetRow(add, btnPos);
            grid.Children.Add(add);

            return grid;
        }

        public void AddCatButtons(LinkedList<MajorCategories> catList)
        {
            /*if (AppData.s_listPanel.Children.Count > 1)
            {
                AppData.s_listPanel.Children.RemoveAt(1);
            }*/

            m_util.ClearList();

            Grid grid = new Grid();

            int btnPos = 0;

            foreach (MajorCategories cat in catList)
            {
                RowDefinition rd = new RowDefinition();
                grid.RowDefinitions.Add(rd);
                string cleanName = m_util.cleanString(cat.categoryTitle);

                //Button btn = new Button();
                CatButton btn = new CatButton();
                btn.category = cat;
                btn.Content = cat.categoryTitle;
                btn.Name = cleanName;
                btn.Click += m_events.ButtonPressCatProp;

                ContextMenu deleteMenu = new ContextMenu();
                CatMenuItem deleteItem = new CatMenuItem();
                deleteItem.data = cat;
                //deleteItem.Click +=;
                deleteItem.Header = "Delete";
                deleteMenu.Items.Add(deleteItem);

                btn.ContextMenu = deleteMenu;

                Grid.SetRow(btn, btnPos);
                grid.Children.Add(btn);
                btnPos++;
            }

            grid.RowDefinitions.Add(new RowDefinition());

            Button btnAdd = new Button();
            btnAdd.Content = "Add category";
            btnAdd.Name = "addCat";
            btnAdd.Click += m_events.ButtonPressNewCat;
            Grid.SetRow(btnAdd, btnPos);
            grid.Children.Add(btnAdd);

            //AppData.s_listPanel.Children.Add(grid);
            m_util.SetList(grid);
        }

        public Grid CreateMajorButtons(LinkedList<MajorData> majors)
        {
            /*if (AppData.s_listPanel.Children.Count > 1)
            {
                AppData.s_listPanel.Children.RemoveAt(1);
            }*/

            m_util.ClearList();

            Grid grid = new Grid();

            int btnPos = 0;
            foreach (MajorData major in majors)
            {
                RowDefinition rd = new RowDefinition();
                grid.RowDefinitions.Add(rd);
                string cleanName = m_util.cleanString(major.MajorName);

                MajorButton btn = new MajorButton();
                btn.major = major;
                btn.Content = major.MajorName;
                btn.Name = cleanName;
                btn.Click += m_events.ButtonPressPage;
                Grid.SetRow(btn, btnPos);
                grid.Children.Add(btn);
                btnPos++;
            }

            return grid;
        }
        
        public void AddCatProp()
        {
            /*if (AppData.s_propertiesPanel.Children.Count > 1)
            {
                AppData.s_propertiesPanel.Children.RemoveAt(1);
            }*/

            m_util.SetProperties(new CatProp(m_util, m_data));

            /*Grid grid = new Grid();
            RowDefinition rd0 = new RowDefinition();
            RowDefinition rd1 = new RowDefinition();
            grid.RowDefinitions.Add(rd0);
            grid.RowDefinitions.Add(rd1);
            TextBox tb = new TextBox();
            tb.Height = 80;
            tb.TextWrapping = TextWrapping.Wrap;
            tb.TextChanged += AppEvents.TextBoxCatName;
            AppData.s_properties = tb;
            Grid.SetRow(tb, 0);
            grid.Children.Add(tb);

            Button btn = new Button();
            btn.Content = "Update";
            btn.Click += AppEvents.ButtonPressUpdateCat;
            Grid.SetRow(btn, 1);
            grid.Children.Add(btn);

            //Properties.Children.Add(grid);
            AppData.s_propertiesPanel.Children.Add(grid);*/
        }

        public Grid CreateMajorButtonsByCat(Dictionary<string, MajorData> majors, LinkedList<MajorCategories> cats, 
            Majors majorFunc, Dictionary<string, List<MajorCategories>> relatedCat)
        {
            //copy major list
            //for each category
            //add cat button for calapsing
            //for each major
            //make button
            //remove major from copied list
            //for each remainging major in copied list
            //TODO: add unassigned button

            m_util.ClearList();
            Grid grid = new Grid();
            int expPos = 0;
            Dictionary<string, MajorData> unassignedMajors = new Dictionary<string, MajorData>(majors);

            foreach (MajorCategories cat in cats)
            {
                List<Object> related = cat.relatedDegrees;
                

                RowDefinition rde = new RowDefinition();
                grid.RowDefinitions.Add(rde);
                Expander categoryExp = new Expander();
                categoryExp.Header = cat.categoryTitle;
                Grid.SetRow(categoryExp, expPos);
                grid.Children.Add(categoryExp);
                expPos++;

                //categoryExp.Content = new Grid();

                Grid catGrid = new Grid();
                int btnPos = 0;

                foreach (DocumentReference doc in related)
                {
                    MajorData major = majorFunc.toMajorData(doc);

                    if (!relatedCat.ContainsKey(major.MajorName))
                    {
                        relatedCat.Add(major.MajorName, new List<MajorCategories>());
                    }
                    relatedCat[major.MajorName].Add(cat);
                    
                    RowDefinition rd = new RowDefinition();
                    //grid.RowDefinitions.Add(rd);
                    catGrid.RowDefinitions.Add(rd);
                    string cleanName = m_util.cleanString(major.MajorName);

                    MajorButton btn = new MajorButton();
                    btn.major = major;
                    btn.Content = major.MajorName;
                    btn.Name = cleanName;
                    btn.Click += m_events.ButtonPressPage;
                    Grid.SetRow(btn, btnPos);
                    catGrid.Children.Add(btn);
                    btnPos++;

                    unassignedMajors.Remove(major.MajorName);
                    
                }
                categoryExp.Content = catGrid;

            }

            //needs to be var because idk why
            /*foreach (var major in unassignedMajors)
            {
                RowDefinition rd = new RowDefinition();
                grid.RowDefinitions.Add(rd);
                string cleanName = m_util.cleanString(major.Key);

                MajorButton btn = new MajorButton();
                btn.major = major.Value;
                btn.Content = major.Key;
                btn.Name = cleanName;
                btn.Click += m_events.ButtonPressPage;
                Grid.SetRow(btn, btnPos);
                grid.Children.Add(btn);
                btnPos++;
            }*/

            return grid;
        }
    }
}
