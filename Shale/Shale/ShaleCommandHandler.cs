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
            //TODO: Add command execution logic here
            PetrelLogger.InfoOutputWindow(string.Format("{0} clicked", @"ShaleCommand" ));
            Form a = new Form();
            a.Icon = Icon.FromHandle(PetrelImages.Modules.GetHicon());
            a.Text = "CreateVF";
            a.Width = 550;
            a.Height = 550;
            ShaleWorkstep asad = new ShaleWorkstep();
            ShaleWorkstep.Arguments arg = new ShaleWorkstep.Arguments(); 
            ShaleWorkstepUI aasd = new ShaleWorkstepUI(asad, arg, null);
            aasd.Parent = a;
            aasd.Show();
            
            //a.par
            a.Show();
            //a.ShowDialog();
        }
    
        #endregion
    }
}
