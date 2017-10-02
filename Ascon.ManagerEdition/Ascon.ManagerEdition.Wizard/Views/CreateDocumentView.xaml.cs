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
    /// Логика взаимодействия для CreateDocumentView.xaml
    /// </summary>
    public partial class CreateDocumentView : Window
    {
        public CreateDocumentView()
        {
            InitializeComponent();
        }

        [Inject]
        public CreateDocumentViewModel CreateRowViewModel
        {
            get => this.DataContext as CreateDocumentViewModel;
            set
            {
                this.DataContext = value;
                if (CreateRowViewModel.Close == null)
                    CreateRowViewModel.Close = this.Close;
            }
        }
    }
}
