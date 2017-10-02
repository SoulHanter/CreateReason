using Ascon.ManagerEdition.Wizard.Utils.Storage.StorageInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.ManagerEdition.Wizard.Models;
using Ascon.Pilot.SDK;
using Ascon.ManagerEdition.Common.DICommon;
using Ninject;
using System.Runtime.InteropServices;

namespace Ascon.ManagerEdition.Wizard.Utils.Storage.StorageImplementation
{
    public class SelectObject : ISelectObject
    {
        private IPilotDialogService _dialogService = NinjectCommon.Kernel.Get<IPilotDialogService>();

        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        public List<ProjectSection> SelectedItems(string caption)
        {
            var dialogOptions = _dialogService.NewOptions()
                                              .WithParentWindow(GetActiveWindow())
                                              .WithCaption(caption)
                                              .WithAllowChecking(false)
                                              .WithAllowMultiSelect(true)
                                              .WithOkButtonCaption("Выбрать");

            var selection = _dialogService.ShowDocumentsSelectorDialog(dialogOptions).ToList();

            return selection.Any() ? selection.Select(x => x.MapToProjectSection()).ToList() :
                                     new List<ProjectSection>();
        }
    }
}
