using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimePlanner.App.ViewModels.ActivityTypes;

namespace TimePlanner.App.Views.ActivityTypes;

public partial class ActivityTypeEditView 
{
    public ActivityTypeEditView(ActivityTypeEditViewModel viewModel)
        :base(viewModel)
    {
        InitializeComponent();
    }
}