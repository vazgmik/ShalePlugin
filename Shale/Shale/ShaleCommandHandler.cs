using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Slb.Ocean.Petrel.Commands;
using Slb.Ocean.Petrel;
using System.Windows.Forms;
using Slb.Ocean.Petrel.UI;
using System.Drawing;
using Slb.Ocean.Petrel.Workflow;

namespace Shale
{
    class ShaleCommandHandler : SimpleCommandHandler
    {
        public static string ID = "Shale.ShaleCommand";
        private WorkflowContext ctnxt = new WorkstepProcessWrapper.Context(); 
        #region SimpleCommandHandler Members

        public override bool CanExecute(Slb.Ocean.Petrel.Contexts.Context context)
        { 
            return true;
        }

        public override void Execute(Slb.Ocean.Petrel.Contexts.Context context)
        {          
            PetrelLogger.InfoOutputWindow(string.Format("{0} clicked", @"ShaleCommand" ));
            
            //Create workstep
            ShaleWorkstep tmp_wstep = new ShaleWorkstep();                           
            ShaleWorkstep.Arguments tmp_warg = new ShaleWorkstep.Arguments();
            ShaleWorkstepUI tmp_wstepui = new ShaleWorkstepUI(tmp_wstep, tmp_warg, ctnxt);

            //Create form and add workstep to form
      

           
            Form form_cont = new Form();
            form_cont.Icon = Icon.FromHandle(PetrelImages.Modules.GetHicon());
            form_cont.Text = "ShaleWorkstep";
            tmp_wstepui.Parent = form_cont;

            form_cont.Width = tmp_wstepui.Width;
            form_cont.Height = tmp_wstepui.Height;
            //form_cont.TopMost = true;
            //form_cont.Show();
        }
    
        #endregion
    }
}
