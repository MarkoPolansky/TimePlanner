using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimePlanner.App.ViewModels.Settings;

namespace TimePlanner.App.Views.Settings;

public partial class SettingsView 
{
    public SettingsView(SettingsViewModel viewModel)
        :base(viewModel)
    {
        InitializeComponent();
    }
}