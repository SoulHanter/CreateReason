using Ascon.ManagerEdition.Wizard.ViewModel;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ascon.ManagerEdition.Wizard.Views
{
    /// <summary>
    /// Логика взаимодействия для TableRemarksView.xaml
    /// </summary>
    public partial class TableRemarksView : UserControl
    {
        public TableRemarksView()
        {
            InitializeComponent();
        }

        [Inject]
        public TableRemarksViewModel TableRemarksViewModel
        {
            get => RemarksTable.DataContext as TableRemarksViewModel;
            set
            {
                RemarksTable.DataContext = value;
                //if (TableRemarksViewModel.Close == null)
                //    TableRemarksViewModel.Close = this.Close;
            }
        }
    }
}
