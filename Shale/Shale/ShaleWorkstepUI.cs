using System;
using System.Drawing;
using System.Windows.Forms;

using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel.DomainObject.Well;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.UI.Controls;

namespace Shale
{
    /// <summary>
    /// This class is the user interface which forms the focus for the capabilities offered by the process.  
    /// This often includes UI to set up arguments and interactively run a batch part expressed as a workstep.
    /// </summary>
    partial class ShaleWorkstepUI : UserControl
    {
        private ShaleWorkstep workstep;
        /// <summary>
        /// The argument package instance being edited by the UI.
        /// </summary>
        private ShaleWorkstep.Arguments args;

        //temporary arguments
        private ShaleWorkstep.Arguments tmpargs = new ShaleWorkstep.Arguments();
        /// <summary>
        /// Contains the actual underlaying context.
        /// </summary>
        private WorkflowContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShaleWorkstepUI"/> class.
        /// </summary>
        /// <param name="workstep">the workstep instance</param>
        /// <param name="args">the arguments</param>
        /// <param name="context">the underlying context in which this UI is being used</param>
        public ShaleWorkstepUI(ShaleWorkstep workstep, ShaleWorkstep.Arguments args, WorkflowContext context)
        {
            InitializeComponent();

            this.workstep = workstep;
            this.args = args;
            this.context = context;

            workstep.CopyArgumentPackage(args, tmpargs);
        }
       

        //helper function for logs drag and drop
        private void getData(ref WellLog tmp, DragEventArgs e, ref PresentationBox box)
        {
            var w_log = e.Data.GetData(typeof(object)) as WellLog;
            if (w_log == null)
            {
                PetrelLogger.ErrorBox("Объект не является каротажной кривой!");
                return;
            }
            tmp = w_log;
            IImageInfoFactory f = CoreSystem.GetService<IImageInfoFactory>(tmp);
            box.Text = tmp.Name;
            box.Image = f.GetImageInfo(tmp).TypeImage;
        }

        private void WellLog_DragDrop(object sender, DragEventArgs e)
        {
            var tmp = tmpargs.ShaleWellLog; 
            getData(ref tmp, e, ref boxWellLog);
            tmpargs.ShaleWellLog = tmp;
        }

        private void sonic_DragDrop(object sender, DragEventArgs e)
        {
            var tmp = tmpargs.SonicLog; 
            getData(ref tmp, e, ref boxSonic);
            tmpargs.SonicLog = tmp;
        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            if (context == null)
            {
                return;
            }

            if (context is WorkstepProcessWrapper.Context)
            {
                Executor exec = workstep.GetExecutor(tmpargs,new WorkstepProcessWrapper.RuntimeContext());
                exec.ExecuteSimple();
            }
            workstep.CopyArgumentPackage(tmpargs, args);
            context.OnArgumentPackageChanged(this, new WorkflowContext.ArgumentPackageChangedEventArgs());
        }

        //load wells button
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            // Set filter options and filter index.
            ofd.Filter = "LAS files|*.las|All Files (*.*)|*.*";
            //openFileDialog1.FilterIndex = 1;

            //ofd.Multiselect = true;
            String path, name;
            // Process input if the user clicked OK.
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                name = ofd.FileName;
                path = ofd.SafeFileName;
                PetrelLogger.InfoOutputWindow(string.Format("{0}, {1}",name,path));

            }
        }
	
    }
}
