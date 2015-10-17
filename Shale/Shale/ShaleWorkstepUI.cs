using System;
using System.Drawing;
using System.Windows.Forms;

using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel.DomainObject.Well;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.UI.Controls;
using System.Collections.Generic;
using Slb.Ocean.Geometry;

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

        private List<Borehole> bholes = new List<Borehole>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ShaleWorkstepUI"/> class.
        /// </summary>
        /// <param name="workstep">the workstep instance</param>
        /// <param name="args">the arguments</param>
        /// <param name="context">the underlying context in which this UI is being used</param>
        public ShaleWorkstepUI(ShaleWorkstep workstep, ShaleWorkstep.Arguments args, WorkflowContext context)
        {
            InitializeComponent();

            btn_Apply.Image = PetrelImages.Apply;
       
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
            tmpargs.Boreholes = bholes;
            if (context is WorkstepProcessWrapper.Context)
            {
                Executor exec = workstep.GetExecutor(tmpargs,new WorkstepProcessWrapper.RuntimeContext());
                exec.ExecuteSimple();
            }
            workstep.CopyArgumentPackage(tmpargs, args);
            context.OnArgumentPackageChanged(this, new WorkflowContext.ArgumentPackageChangedEventArgs());
        }

     
       
        //delete Borehole button
        private void btn_DelBoreHole_Click(object sender, EventArgs e)
        {
            var selItems = list_Boreholes.SelectedItems;
            var tmp = new List<ListBoxItem>();
            foreach (var cur in selItems)
                tmp.Add(cur);
            foreach (var cur in tmp)
            {
                list_Boreholes.Items.Remove(cur);
                bholes.Remove(cur.Value as Borehole);
                //wells_inter.Remove(cur.Value as Point2);
            }             
        }

        private void updateBoreholeList(Borehole borehole)
        {
            if (!bholes.Contains(borehole))
            {
                IImageInfoFactory f = CoreSystem.GetService<IImageInfoFactory>(borehole);
                var item = new ListBoxItem();
                item.Text = string.Format("{0} (X: {1}; Y: {2})", borehole.Name, borehole.WellHead.X, borehole.WellHead.Y);
                item.Image = f.GetImageInfo(borehole).TypeImage;
                item.Value = borehole;
                list_Boreholes.Items.Add(item);
                //wells_inter.Add(borehole.WellHead);
                bholes.Add(borehole);
            }
        }

        //drag drop for boreholes
        private void drpBorehole_DragDrop(object sender, DragEventArgs e)
        {
            var rawData = e.Data.GetData(typeof(object));
            switch (rawData.GetType().Name)
            {
                case "Borehole":
                    {
                        updateBoreholeList(rawData as Borehole);
                        break;
                    }
                case "BoreholeCollection":
                    {
                        var boreholes = rawData as BoreholeCollection;
                        foreach (var borehole in boreholes)
                            updateBoreholeList(borehole);
                        break;
                    }
                default:
                    PetrelLogger.ErrorBox("Объект не является скважиной!");
                    break;
            }
        }
	
    }
}
