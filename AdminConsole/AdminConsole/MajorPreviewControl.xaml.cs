using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AdminDatabaseFramework;

namespace AdminConsole
{
    /// <summary>
    /// Interaction logic for MajorPreviewControl.xaml
    /// </summary>
    /// 

    public partial class MajorPreviewControl : UserControl
    {
        AppData m_data;
        Utilities m_util;
        public MajorPreviewControl(AppData data, Utilities util)
        {
            m_data = data;
            m_util = util;
            InitializeComponent();
            BindElements();           
        }

        private void ElementSelect(object sender, EventArgs e)
        {
            /*if (AppData.s_propertiesPanel.Children.Count > 1)
            {
                AppData.s_propertiesPanel.Children.RemoveAt(1);
            }*/
            //m_util.ClearProperties();
            m_util.ClearPropertiesKeepCat();
            Label lElement = sender as Label;
            TextBlock tbElement = sender as TextBlock;
            if (lElement != null)
            {
                //AppData.s_propertiesPanel.Children.Add(new MajorPropControl(lElement.Name));
                string target = lElement.Name;
                if (target == "Classes" || target == "campuses")
                {
                    //target += "[0]";
                }
                //m_util.SetProperties(new MajorPropControl(lElement.Name, m_data), false);
                m_util.SetProperties(new MajorPropControl(target, m_data), false);
            } 
            else if (tbElement != null)
            {
                //AppData.s_propertiesPanel.Children.Add(new MajorPropControl(tbElement.Name));
                string target = tbElement.Name;
                if (target == "Classes" || target == "campuses")
                {
                    //target += "[0]";
                }
                //m_util.SetProperties(new MajorPropControl(tbElement.Name, m_data), false);
                m_util.SetProperties(new MajorPropControl(target, m_data), false);
            }
        }

        //TODO: Find a better way to bind this data
        private void BindElements()
        {
            Binding b_title = new Binding("MajorName");
            b_title.Source = m_data.s_activeData;
            //b_title.Source = AppData.s_activeData;
            if (m_data.s_activeData.MajorName == null)
            {
                m_data.s_activeData.MajorName = "No Name";
            }
            BindingOperations.SetBinding(MajorName, Label.ContentProperty, b_title);

            Binding b_classes = new Binding("classes[0]");
            b_classes.Source = m_data.s_activeData;
            if (m_data.s_activeData.Classes == null)
            {
                m_data.s_activeData.Classes = new List<string>();
                m_data.s_activeData.Classes.Add("No Classes");
            }
            //b_classes.Source = AppData.s_activeData;
            BindingOperations.SetBinding(Classes, TextBlock.TextProperty, b_classes);

            Binding b_camp = new Binding("campuses[0]");
            b_camp.Source = m_data.s_activeData;
            if (m_data.s_activeData.campuses == null)
            {
                m_data.s_activeData.campuses = new List<string>();
                m_data.s_activeData.campuses.Add("No campuses");
            }
            //b_camp.Source = AppData.s_activeData;
            BindingOperations.SetBinding(campuses, TextBlock.TextProperty, b_camp);

            Binding b_type = new Binding("type[0]");
            b_type.Source = m_data.s_activeData;
            if (m_data.s_activeData.type == null)
            {
                m_data.s_activeData.type = new List<string>();
                m_data.s_activeData.type.Add("No Type");
            }
            //b_type.Source = AppData.s_activeData;
            BindingOperations.SetBinding(type, Label.ContentProperty, b_type);

            Binding b_desc = new Binding("about[0]");
            b_desc.Source = m_data.s_activeData;
            //b_desc.Source = AppData.s_activeData;
            BindingOperations.SetBinding(about, TextBlock.TextProperty, b_desc);
        }
    }
}
