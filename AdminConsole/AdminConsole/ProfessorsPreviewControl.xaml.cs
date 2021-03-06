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

namespace AdminConsole
{
    /// <summary>
    /// Interaction logic for ProfessorsPreviewControl.xaml
    /// </summary>
    public partial class ProfessorsPreviewControl : UserControl
    {
        private AppData m_data;
        private Utilities m_utils;
        public ProfessorsPreviewControl(AppData data, Utilities utils)
        {
            m_data = data;
            m_utils = utils;
            InitializeComponent();
            Binding();
        }

        private void ElementSelect(object sender, EventArgs e)
        {
            /*if (AppData.s_propertiesPanel.Children.Count > 1)
            {
                AppData.s_propertiesPanel.Children.RemoveAt(1);
            }*/
            m_utils.ClearProperties();
            //TextBlock tbElement = sender as TextBlock;
            Label lElement = sender as Label;
            //AppData.s_propertiesPanel.Children.Add(new ProfPropControl(temp.Name));
            //AppData.s_propertiesPanel.Children.Add(new ProfPropControl(lElement.Name));
            m_utils.SetProperties(new ProfPropControl(lElement.Name, m_data));
        }

        private void Binding()
        {
            Binding b_name = new Binding("professorName");
            b_name.Source = m_data.s_activeProfessor;
            //b_name.Source = AppData.s_activeProfessor;
            BindingOperations.SetBinding(professorName, Label.ContentProperty, b_name);

            Binding b_dep = new Binding("professorDepartment");
            b_dep.Source = m_data.s_activeProfessor;
            //b_dep.Source = AppData.s_activeProfessor;
            //BindingOperations.SetBinding(professorDepartment, TextBlock.TextProperty, b_dep);
            BindingOperations.SetBinding(professorDepartment, Label.ContentProperty, b_dep);

            Binding b_office = new Binding("professorOffice");
            b_office.Source = m_data.s_activeProfessor;
            //b_office.Source = AppData.s_activeProfessor;
            //BindingOperations.SetBinding(professorOffice, TextBlock.TextProperty, b_office);
            BindingOperations.SetBinding(professorOffice, Label.ContentProperty, b_office);

            Binding b_email = new Binding("professorEmail");
            b_email.Source = m_data.s_activeProfessor;
            //b_email.Source = AppData.s_activeProfessor;
            //BindingOperations.SetBinding(professorEmail, TextBlock.TextProperty, b_email);
            BindingOperations.SetBinding(professorEmail, Label.ContentProperty, b_email);

            Binding b_phone = new Binding("professorPhoneNumber");
            b_phone.Source = m_data.s_activeProfessor;
            //b_phone.Source = AppData.s_activeProfessor;
            //BindingOperations.SetBinding(professorPhoneNumber, TextBlock.TextProperty, b_phone);
            BindingOperations.SetBinding(professorPhoneNumber, Label.ContentProperty, b_phone);
        }
    }
}
