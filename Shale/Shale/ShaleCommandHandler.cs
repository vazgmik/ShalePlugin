using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Slb.Ocean.Petrel.Commands;
using Slb.Ocean.Petrel;
using System.Windows.Forms;
using Slb.Ocean.Petrel.UI;
using System.Drawing;

namespace Shale
{
    class ShaleCommandHandler : SimpleCommandHandler
    {
        public static string ID = "Shale.ShaleCommand";

        #region SimpleCommandHandler Members

        public override bool CanExecute(Slb.Ocean.Petrel.Contexts.Context context)
        { 
            return true;
        }

        public override void Execute(Slb.Ocean.Petrel.Contexts.Context context)
        {          
            PetrelLogger.InfoOutputWindow(string.Format("{0} clicked", @"ShaleCommand" ));

            Form form_cont = new Form();
            form_cont.Icon = Icon.FromHandle(PetrelImages.Modules.GetHicon());
            form_cont.Text = "ShaleWorkstep";
            form_cont.Width = 750;
            form_cont.Height = 550;

            ShaleWorkstep tmp_wstep = new ShaleWorkstep();
            ShaleWorkstep.Arguments tmp_warg = new ShaleWorkstep.Arguments();
            ShaleWorkstepUI tmp_wstepui = new ShaleWorkstepUI(tmp_wstep, tmp_warg, null);

            tmp_wstepui.Parent = form_cont;
            form_cont.TopMost = true;
            form_cont.Show();
        }
    
        #endregion
    }
}
