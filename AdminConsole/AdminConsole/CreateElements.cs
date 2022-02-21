using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AdminDatabaseFramework;



namespace AdminConsole
{
    /*
     * Author: Destiny (Destin) Dahlgren
     * Purpose: This class is responsible for generating UI elements
     */

    static class CreateElements
    {
        public static Grid CreateProfButtons()
        {
            Grid grid = new Grid();

            int btnPos = 0;
            foreach (ProfessorData p in AppData.s_professorList)
            {
                RowDefinition rd = new RowDefinition();
                grid.RowDefinitions.Add(rd);
                string cleanName = Utilities.cleanString(p.professorName);

                ProfessorButton btn = new ProfessorButton();
                btn.Content = p.professorName;
                btn.Name = cleanName;
                btn.data = p;
                btn.Click += AppEvents.ButtonPressProf;
                ContextMenu deleteMenu = new ContextMenu();
                ProfMenuItem deleteItem = new ProfMenuItem();
                deleteItem.data = p;
                deleteItem.Click += AppEvents.ContextItemRemoveProf;
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
            add.data = Utilities.NewProf();
            add.Click += AppEvents.ButtonPressAddProf;
            Grid.SetRow(add, btnPos);
            grid.Children.Add(add);
            return grid;
  
        }

        public static Grid CreateBuildingButtons()
        {
            Grid grid = new Grid();

            int btnPos = 0;
            foreach (BuildingData building in AppData.s_buildingList)
            {
                RowDefinition rd = new RowDefinition();
                grid.RowDefinitions.Add(rd);
                string cleanName = Utilities.cleanString(building.BuildingName);

                BuildingButton btn = new BuildingButton();
                btn.Content = building.BuildingName;
                btn.Name = cleanName;
                btn.Click += AppEvents.ButtonPressBuildingSelect;
                btn.data = building;
                ContextMenu deleteMenu = new ContextMenu();
                BuildingMenuItem deleteItem = new BuildingMenuItem();
                deleteItem.data = building;
                deleteItem.Header = "Delete";
                deleteItem.Click += AppEvents.ContextItemRemoveBuilding;
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
            add.data = Utilities.NewBuilding();
            add.Click += AppEvents.ButtonPressAddBuilding;
            Grid.SetRow(add, btnPos);
            grid.Children.Add(add);

            return grid;
        }

        public static void AddCatButtons(LinkedList<MajorCategories> catList)
        {
            if (AppData.s_listPanel.Children.Count > 1)
            {
                AppData.s_listPanel.Children.RemoveAt(1);
            }

            Grid grid = new Grid();

            int btnPos = 0;

            foreach (MajorCategories cat in catList)
            {
                RowDefinition rd = new RowDefinition();
                grid.RowDefinitions.Add(rd);
                string cleanName = Utilities.cleanString(cat.categoryTitle);

                //Button btn = new Button();
                CatButton btn = new CatButton();
                btn.category = cat;
                btn.Content = cat.categoryTitle;
                btn.Name = cleanName;
                btn.Click += AppEvents.ButtonPressCatProp;

                Grid.SetRow(btn, btnPos);
                grid.Children.Add(btn);
                btnPos++;
            }

            grid.RowDefinitions.Add(new RowDefinition());

            Button btnAdd = new Button();
            btnAdd.Content = "Add category";
            btnAdd.Name = "addCat";
            btnAdd.Click += AppEvents.ButtonPressNewCat;
            Grid.SetRow(btnAdd, btnPos);
            grid.Children.Add(btnAdd);

            AppData.s_listPanel.Children.Add(grid);
        }

        public static void AddCatProp()
        {
            if (AppData.s_propertiesPanel.Children.Count > 1)
            {
                AppData.s_propertiesPanel.Children.RemoveAt(1);
            }

            Grid grid = new Grid();
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
            AppData.s_propertiesPanel.Children.Add(grid);
        }

        //Create a list of buttons for every major available
        //TODO: Add a way to generate the buttons for a major that is only under a certain category
        //      Add a back button to get back to a "higher level" of the ui (Major list -> Major categories)
        public static Grid CreateMajorButtons(LinkedList<MajorData> majors)
        {
            if (AppData.s_listPanel.Children.Count > 1)
            {
                AppData.s_listPanel.Children.RemoveAt(1);
            }

            Grid grid = new Grid();

            int btnPos = 0;
            foreach (MajorData major in majors)
            {
                RowDefinition rd = new RowDefinition();
                grid.RowDefinitions.Add(rd);
                string cleanName = Utilities.cleanString(major.MajorName);

                MajorButton btn = new MajorButton();
                btn.major = major;
                btn.Content = major.MajorName;
                btn.Name = cleanName;
                btn.Click += AppEvents.ButtonPressPage;
                Grid.SetRow(btn, btnPos);
                grid.Children.Add(btn);
                btnPos++;
            }

            return grid;
        }
        
        public static void ShowPreview()
        {
            UserControl test = new MajorPreviewControl();
            AppData.s_previewPanel.Children.Add(test);
        }
    }
}
