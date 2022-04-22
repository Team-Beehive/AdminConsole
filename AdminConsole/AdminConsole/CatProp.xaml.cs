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
    /// Interaction logic for NewCatProp.xaml
    /// </summary>
    public partial class CatProp : UserControl
    {
        Utilities m_util;
        AppData m_data;
        public CatProp(Utilities util, AppData data)
        {
            m_util = util;
            m_data = data;
            InitializeComponent();
        }

        private void Submit(object sender, EventArgs e)
        {
            string name = CatName.Text;
            if (name != "")
            {
                m_data.s_major.CreateMajorCategory(name);
                
                //Find newley added cat and init list
                LinkedList<MajorCategories> travel = m_data.s_major.GetCategories();
                foreach (MajorCategories cat in travel)
                {
                    if (cat.categoryTitle == name)
                    {
                        cat.relatedDegrees = new List<object>();
                        //TODO: run update on category
                    }
                }
            }
        }

        private void Cancel(object sender, EventArgs e)
        {
            m_util.ClearProperties();
        }
    }
}
