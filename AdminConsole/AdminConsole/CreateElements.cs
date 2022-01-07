﻿using System;
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
        //Create a set of navigation buttons
        public static Grid CreateSectionButtons()
        {
            //Should load from xaml because this part is NOT dynamic and should rarly change
            //Look into useing a Frame for better navegation

            Grid grid = new Grid();
            RowDefinition rd0 = new RowDefinition();
            RowDefinition rd1 = new RowDefinition();
            RowDefinition rd2 = new RowDefinition();
            grid.RowDefinitions.Add(rd0);
            grid.RowDefinitions.Add(rd1);
            grid.RowDefinitions.Add(rd2);

            //Button for Majors
            Button majors = new Button();
            majors.Name = "editMajors";
            majors.Content = "Edit Majors";
            majors.Click += AppEvents.ButtonPressTest;
            Grid.SetRow(majors, 0);
            grid.Children.Add(majors);

            //Button for professors
            Button prof = new Button();
            prof.Name = "editProf";
            prof.Content = "Edit Professors";
            prof.Click += AppEvents.ButtonPressTest;
            Grid.SetRow(prof, 1);
            grid.Children.Add(prof);

            //Button for buildings
            Button buildings = new Button();
            buildings.Name = "editBuildings";
            buildings.Content = "Edit Buildings";
            buildings.Click += AppEvents.ButtonPressTest;
            Grid.SetRow(buildings, 2);
            grid.Children.Add(buildings);


            return grid;
        }

        //TODO: Generate a list of buttons for all professors

        //TODO: Generate a list of buttons for all buildings (will need to consult with Tevor on how this should work)

        //TODO: Generate a list of buttons for each category

        //Create a list of buttons for every major available
        //TODO: Add a way to generate the buttons for a major that is only under a certain category
        //      Add a back button to get back to a "higher level" of the ui (Major list -> Major categories)
        public static Grid CreateMajorButtons(LinkedList<MajorData> majors)
        {
            Grid grid = new Grid();

            int btnPos = 0;
            foreach (MajorData major in majors)
            {
                RowDefinition rd = new RowDefinition();
                grid.RowDefinitions.Add(rd);
                string cleanName = Utilities.cleanString(major.MajorName);

                Button btn = new Button();
                btn.Content = major.MajorName;
                btn.Name = cleanName;
                btn.Click += AppEvents.ButtonPressPage;
                Grid.SetRow(btn, btnPos);
                grid.Children.Add(btn);
                btnPos++;
            }

            return grid;
        }

        public static void AddProperties()
        {
            //Add logic to determin correct Properties
            //TODO: add logic to first remove any current properties if they exist
            AppData.s_propertiesPanel.Children.Add(AddTextProperties());
        }

        public static Grid AddTextProperties()
        {
            Grid grid = new Grid();
            TextBox tb = new TextBox();

            tb.Height = 80;
            tb.TextWrapping = TextWrapping.Wrap;
            tb.TextChanged += AppEvents.TextChangedTest;
            AppData.s_properties = tb;
            Grid.SetRow(tb, 0);
            grid.Children.Add(tb);


            return grid;
        }

        //This is a temporary Majors Template function
        //TODO: Replace this with a method of reading in xaml code
        public static Grid MajorsTemplate()
        {
            Grid grid = new Grid();

            RowDefinition r1 = new RowDefinition();
            RowDefinition r2 = new RowDefinition();
            RowDefinition r3 = new RowDefinition();
            RowDefinition r4 = new RowDefinition();
            RowDefinition r5 = new RowDefinition();
            RowDefinition r6 = new RowDefinition();
            grid.RowDefinitions.Add(r1);
            grid.RowDefinitions.Add(r2);
            grid.RowDefinitions.Add(r3);
            grid.RowDefinitions.Add(r4);
            grid.RowDefinitions.Add(r5);
            grid.RowDefinitions.Add(r6);

            TextBlock title = new TextBlock();
            title.Text = AppData.s_activeTitle;
            title.Name = "title";
            title.MouseDown += AppEvents.ButtonPressSelected;
            Grid.SetRow(title, 0);

            TextBlock classes = new TextBlock();
            classes.Text = AppData.s_activeClasses;
            classes.Name = "classes";
            classes.MouseDown += AppEvents.ButtonPressSelected;
            Grid.SetRow(classes, 1);

            //TextBlock prof = new TextBlock();
            //prof.Text = m_activeProfessors;
            //prof.Name = "prof";
            //prof.MouseDown += ButtonPressSelected;
            //Grid.SetRow(prof, 2);

            TextBlock camp = new TextBlock();
            camp.Text = AppData.s_activeCampus;
            camp.Name = "camp";
            camp.MouseDown += AppEvents.ButtonPressSelected;
            Grid.SetRow(camp, 2);

            TextBlock type = new TextBlock();
            type.Text = AppData.s_activeType;
            type.Name = "type";
            type.MouseDown += AppEvents.ButtonPressSelected;
            //Grid.SetRow(type, 4);
            Grid.SetRow(type, 3);

            TextBlock desc = new TextBlock();
            desc.Text = AppData.s_activeDesc;
            desc.Name = "desc";
            desc.MouseDown += AppEvents.ButtonPressSelected;
            desc.TextWrapping = TextWrapping.Wrap;
            //Grid.SetRow(desc, 5);
            Grid.SetRow(desc, 4);

            grid.Children.Add(title);
            //grid.Children.Add(classes);
            //grid.Children.Add(prof);
            grid.Children.Add(camp);
            grid.Children.Add(type);
            grid.Children.Add(desc);


            return grid;
        }

        public static void ShowPreview()
        {
            //TODO: add logic to select the proper preview
            Grid grid = CreateElements.MajorsTemplate();
            AppData.s_previewPanel.Children.Add(grid);
        }
    }
}