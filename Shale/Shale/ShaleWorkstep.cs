using System;

using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;
using Slb.Ocean.Petrel.DomainObject.Well;
using System.Collections.Generic;
using Slb.Ocean.Petrel.DomainObject.Analysis;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Geometry;
using Slb.Ocean.Petrel.UI.Internal;
using System.Linq;

namespace Shale
{
    /// <summary>
    /// This class contains all the methods and subclasses of the ShaleWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
    /// 


    // Wellog - Count ;MyData class
    class MyData
    {
        // This is the custom constructor.
        public MyData(int count, WellLog w)
        {
            this.count = count;
            this.log = w;
        }
        // These statements declare the struct fields and set the default values.
        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private WellLog log;

        public WellLog Log
        {
            get { return log; }
            set { log = value; }
        }

        // Other methods, fields, properties, and events.
    }

    class ShaleWorkstep : Workstep<ShaleWorkstep.Arguments>, IExecutorSource, IAppearance, IDescriptionSource
    {
        #region Overridden Workstep methods

        /// <summary>
        /// Creates an empty Argument instance
        /// </summary>
        /// <returns>New Argument instance.</returns>

        protected override ShaleWorkstep.Arguments CreateArgumentPackageCore(IDataSourceManager dataSourceManager)
        {
            return new Arguments(dataSourceManager);
        }
        /// <summary>
        /// Copies the Arguments instance.
        /// </summary>
        /// <param name="fromArgumentPackage">the source Arguments instance</param>
        /// <param name="toArgumentPackage">the target Arguments instance</param>
        protected override void CopyArgumentPackageCore(Arguments fromArgumentPackage, Arguments toArgumentPackage)
        {
            DescribedArgumentsHelper.Copy(fromArgumentPackage, toArgumentPackage);
        }

        /// <summary>
        /// Gets the unique identifier for this Workstep.
        /// </summary>
        protected override string UniqueIdCore
        {
            get
            {
                return "3f967142-ab70-493f-8072-cdfa4e925511";
            }
        }
        #endregion

        #region IExecutorSource Members and Executor class

        /// <summary>
        /// Creates the Executor instance for this workstep. This class will do the work of the Workstep.
        /// </summary>
        /// <param name="argumentPackage">the argumentpackage to pass to the Executor</param>
        /// <param name="workflowRuntimeContext">the context to pass to the Executor</param>
        /// <returns>The Executor instance.</returns>
        public Slb.Ocean.Petrel.Workflow.Executor GetExecutor(object argumentPackage, WorkflowRuntimeContext workflowRuntimeContext)
        {
            return new Executor(argumentPackage as Arguments, workflowRuntimeContext);
        }

        public class Executor : Slb.Ocean.Petrel.Workflow.Executor
        {
            Arguments arguments;
            WorkflowRuntimeContext context;

            public Executor(Arguments arguments, WorkflowRuntimeContext context)
            {
                this.arguments = arguments;
                this.context = context;
            }

            public float FindMaxVal(List<WellLogSample> list)
            {
                if (list.Count == 0)
                    return 0;
                
                float maxVal = float.MinValue;
                foreach (WellLogSample type in list)
                    if (type.Value > maxVal)
                        maxVal = type.Value;

                return maxVal;
            }

            public WellLog CreateWellLog(Borehole bh, string name, Template t, List<WellLogSample> tsamples)
            {
                //function to create welllog
                WellLog log;
                using (ITransaction trans = DataManager.NewTransaction())
                {
                    WellLogVersion wlVersion = WellLogVersion.NullObject;
                    //Template t = PetrelProject.WellKnownTemplates.PetrophysicalGroup.Porosity;
                    WellRoot wellRoot = WellRoot.Get(PetrelProject.PrimaryProject);
                    LogVersionCollection rootLVColl = wellRoot.LogVersionCollection;
                    trans.Lock(rootLVColl);
                    wlVersion = rootLVColl.CreateWellLogVersion(name, t);
                    trans.Lock(bh);
                    log = bh.Logs.CreateWellLog(wlVersion);
                    log.Samples = tsamples;
                    trans.Commit();
                }
                return log;
            } 

            
            public void SetNameWindow(ToggleWindow window,String name)
            {
                // function to set window name in petrel

               
                INameInfoFactory nameFactory = (null != window) ? CoreSystem.GetService<INameInfoFactory>(window) : null;
                var nameInfo = (null != nameFactory) ? nameFactory.GetNameInfo(window) : null;
                if (null != nameInfo && nameInfo.CanChangeName)
                    nameInfo.Name = name;              
            }


            public void CalculateTOC(MyData data, ref List<WellLogSample> tmpsamples, List<WellLogSample> res_samples, int index, double k, string name1, string name2, Borehole bhole)
            {
                // function to calculate TOC
                if (data != null)
                {
                    tmpsamples.Clear();
                    List<WellLogSample> type_samples = data.Log.Samples.ToList();
                    float max_resval = FindMaxVal(res_samples);
                    float max_dtcval = FindMaxVal(type_samples);

                    List<WellLogSample> tocs_samples = new List<WellLogSample>();
                    int size = Math.Min(arguments.List_res[index].Log.SampleCount, data.Log.SampleCount);
                    double power = 0.297 - 0.1688 * 13;//LOM  надо подставить здесь 13
                    for (int i = 0; i < size; ++i)
                    {
                        if (!res_samples[i].Value.Equals(float.NaN) && !type_samples[i].Value.Equals(float.NaN))
                        {
                            double part1 = Math.Log((double)(res_samples[i].Value / max_resval));
                            double part2 = k * (type_samples[i].Value - max_dtcval);
                            float fl_typelog = (float)(part1 + part2);
                            tmpsamples.Add(new WellLogSample(res_samples[i].MD, fl_typelog));

                            float fl_typetoc = fl_typelog * (float)Math.Pow(10, power);
                            tocs_samples.Add(new WellLogSample(res_samples[i].MD, fl_typetoc));
                        }
                    }

                    var typeLogR = CreateWellLog(bhole, name1, arguments.List_son[index].Log.WellLogVersion.Template, tmpsamples);
                    var typeTOC = CreateWellLog(bhole, name2, arguments.List_son[index].Log.WellLogVersion.Template, tocs_samples);

                }

            }

            public void UpdateDrawCorrelationData2D(WellLog log1, WellLog log2, CorrelationData2D cor,String name)
            {
                //Draw Crossplot function 
                using (ITransaction trans = DataManager.NewTransaction())
                {
                    trans.Lock(cor);
                    List<Point2> pointList = new List<Point2>();
                    for (int sampleIndex = 0; sampleIndex < log1.SampleCount;sampleIndex++)
                    {
                        if (!float.IsNaN(log1[sampleIndex].Value) && !float.IsNaN(log2[sampleIndex].Value))
                        {
                            if (log1[sampleIndex].Value > 0 && log2[sampleIndex].Value >0)
                                pointList.Add(new Point2(log1[sampleIndex].Value, log2[sampleIndex].Value));
                        }
                    }
                    cor.Name = name;
                    cor.NameX = log1.WellLogVersion.Name;
                    cor.NameY = log2.WellLogVersion.Name;
                    cor.TemplateX = log1.WellLogVersion.Template;
                    cor.TemplateY = log2.WellLogVersion.Template;
                    cor.Points = pointList;        
                    trans.Commit();
                }

                ToggleWindow window = PetrelProject.ToggleWindows.Add(WellKnownWindows.Function);
                SetNameWindow(window, name);
                window.ShowObject(cor);
            }
            public override void ExecuteSimple()
            {
                /*Шаг 1:Calculation of LogR of all Resistivity logs 
                        Исходные данные:каротажная кривая сопротивления (LLD) 
                        Результат: логарифм кривой сопротивления                 */
                if (arguments.Boreholes.Count == 0 || arguments.List_res == null || arguments.List_vitr == null) 
                    return;

                int index = 0;
                foreach (Borehole bhole in arguments.Boreholes)
                {
                    //check if no resistivity data or vitrinite when continue
                    if (arguments.List_res[index] == null || arguments.List_vitr[index] == null)
                    {
                        index++;
                        continue;
                    }
                    //resistivity samples
                    var samples = arguments.List_res[index].Log.Samples;
                    
                    //create list res_samples,contains samples of resistivity log 
                    List<WellLogSample> res_samples = samples.ToList();
                    //tmp contains Math.Log(values) of samples
                    List<WellLogSample> tmpsamples = new List<WellLogSample>();
                    
                    foreach (WellLogSample sample in samples)
                    {
                        if (sample.Value > 0 && !sample.Value.Equals(float.NaN))
                        {
                            float fl_log =(float)Math.Log((double)sample.Value );
                            tmpsamples.Add(new WellLogSample(sample.MD, fl_log));
                        }
                    }

                    if (arguments.List_son[index] != null)
                    {
                        var LogR = CreateWellLog(bhole, "LogR", arguments.List_res[index].Log.WellLogVersion.Template, tmpsamples);

                        using (ITransaction trans = DataManager.NewTransaction())
                        {
                            /*Шаг 2:CreatingCross‐PlotLogRvs.Sonic (DT) 
                              Исходные данные:  
                              Данные кривой сопротивления,  
                              Каротажная кривая акустики 
                              Результат:  
                              Кросс плот (ось X- сопротивление, ось Y- акустика)                             */
                            trans.Lock(PetrelProject.PrimaryProject);
                            Collection col = PetrelProject.PrimaryProject.CreateCollection("Correlation collection");
                            CorrelationData2D cor = col.CreateCorrelationData2D("Cross plot");
                            UpdateDrawCorrelationData2D(LogR, arguments.List_son[index].Log, cor, bhole.Name + ":LogR vs Sonic");
                            trans.Commit();
                        }
                    }

                    //create SLogR and TOCs
                    CalculateTOC(arguments.List_son[index], ref tmpsamples, res_samples, index, 0.02, "SlogR", "TOCs", bhole);
                    //create DlogR and TOCd
                    CalculateTOC(arguments.List_den[index], ref tmpsamples, res_samples, index, -2.5, "DlogR", "TOCd", bhole);
                    //create DlogR and TOCd
                    CalculateTOC(arguments.List_por[index], ref tmpsamples, res_samples, index, 4.0, "NlogR", "TOCn", bhole);

                    // create SLogR and TOCs
                    //if(arguments.List_son[index]!=null)
                    //{
                    //    tmpsamples.Clear();
                    //    List<WellLogSample> son_samples = arguments.List_son[index].Log.Samples.ToList();
                    //    float max_resval = FindMaxVal(res_samples);
                    //    float max_dtcval = FindMaxVal(son_samples);

                    //    List<WellLogSample> tocs_samples = new List<WellLogSample>();
                    //    int size = Math.Min(arguments.List_res[index].Log.SampleCount, arguments.List_son[index].Log.SampleCount);
                    //    double power = 0.297-0.1688*13;//LOM  надо подставить
                    //    for(int i=0;i<size;++i)
                    //    {
                    //        if (!res_samples[i].Value.Equals(float.NaN) && !son_samples[i].Value.Equals(float.NaN))
                    //        {
                    //            double part1 = Math.Log((double)(res_samples[i].Value / max_resval));
                    //            double part2 = 0.02 * (son_samples[i].Value - max_dtcval);
                    //            float fl_log = (float)(part1 + part2);
                    //            tmpsamples.Add(new WellLogSample(res_samples[i].MD, fl_log));

                    //            float fl_tocs = fl_log * (float)Math.Pow(10, power);
                    //            tocs_samples.Add(new WellLogSample(res_samples[i].MD, fl_tocs));
                    //        }
                    //    }

                    //    var SLogR = CreateWellLog(bhole, "SlogR", arguments.List_son[index].Log.WellLogVersion.Template, tmpsamples);
                    //    var TOCs = CreateWellLog(bhole, "TOCs", arguments.List_son[index].Log.WellLogVersion.Template, tocs_samples);

                    //}
                    
                    index++;
                }




            }
        }

        #endregion

        /// <summary>
        /// ArgumentPackage class for ShaleWorkstep.
        /// Each public property is an argument in the package.  The name, type and
        /// input/output role are taken from the property and modified by any
        /// attributes applied.
        /// </summary>
        public class Arguments : DescribedArgumentsByReflection
        {
            public Arguments()
                : this(DataManager.DataSourceManager)
            {                
            }

            public Arguments(IDataSourceManager dataSourceManager)
            {
            }
            
            private Slb.Ocean.Petrel.DomainObject.Well.WellLog shaleWellLog;
            private Slb.Ocean.Petrel.DomainObject.Well.WellLog sonicLog;
            private List<Borehole> boreholes = new List<Borehole>();

            //version 1
            //private List<WellLog> list_res = new List<WellLog>();

            //public List<WellLog> List_res
            //{
            //    get { return list_res; }
            //    set { list_res = value; }
            //}
            //private List<WellLog> list_son = new List<WellLog>();

            //public List<WellLog> List_son
            //{
            //    get { return list_son; }
            //    set { list_son = value; }
            //}
            //private List<WellLog> list_den = new List<WellLog>();

            //public List<WellLog> List_den
            //{
            //    get { return list_den; }
            //    set { list_den = value; }
            //}
            //private List<WellLog> list_por = new List<WellLog>();

            //public List<WellLog> List_por
            //{
            //    get { return list_por; }
            //    set { list_por = value; }
            //} 

            //version 2
            private List<MyData> list_res = new List<MyData>();

            public List<MyData> List_res
            {
                get { return list_res; }
                set { list_res = value; }
            }
            private List<MyData> list_son = new List<MyData>();

            public List<MyData> List_son
            {
                get { return list_son; }
                set { list_son = value; }
            }
            private List<MyData> list_den = new List<MyData>();

            public List<MyData> List_den
            {
                get { return list_den; }
                set { list_den = value; }
            }
            private List<MyData> list_por = new List<MyData>();

            public List<MyData> List_por
            {
                get { return list_por; }
                set { list_por = value; }
            }

            private List<MyData> list_vitr = new List<MyData>();

            internal List<MyData> List_vitr
            {
                get { return list_vitr; }
                set { list_vitr = value; }
            }

            public List<Borehole> Boreholes
            {
                get { return boreholes; }
                set { boreholes = value; }
            }

        }
    
        #region IAppearance Members
        public event EventHandler<TextChangedEventArgs> TextChanged;
        protected void RaiseTextChanged()
        {
            if (this.TextChanged != null)
                this.TextChanged(this, new TextChangedEventArgs(this));
        }

        public string Text
        {
            get { return Description.Name; }
            private set 
            {
                // TODO: implement set
                this.RaiseTextChanged();
            }
        }

        public event EventHandler<ImageChangedEventArgs> ImageChanged;
        protected void RaiseImageChanged()
        {
            if (this.ImageChanged != null)
                this.ImageChanged(this, new ImageChangedEventArgs(this));
        }

        public System.Drawing.Bitmap Image
        {
            get { return PetrelImages.Modules; }
            private set 
            {
                // TODO: implement set
                this.RaiseImageChanged();
            }
        }
        #endregion

        #region IDescriptionSource Members

        /// <summary>
        /// Gets the description of the ShaleWorkstep
        /// </summary>
        public IDescription Description
        {
            get { return ShaleWorkstepDescription.Instance; }
        }

        /// <summary>
        /// This singleton class contains the description of the ShaleWorkstep.
        /// Contains Name, Shorter description and detailed description.
        /// </summary>
        public class ShaleWorkstepDescription : IDescription
        {
            /// <summary>
            /// Contains the singleton instance.
            /// </summary>
            private  static ShaleWorkstepDescription instance = new ShaleWorkstepDescription();
            /// <summary>
            /// Gets the singleton instance of this Description class
            /// </summary>
            public static ShaleWorkstepDescription Instance
            {
                get { return instance; }
            }

            #region IDescription Members

            /// <summary>
            /// Gets the name of ShaleWorkstep
            /// </summary>
            public string Name
            {
                get { return "ShaleWorkstep"; }
            }
            /// <summary>
            /// Gets the short description of ShaleWorkstep
            /// </summary>
            public string ShortDescription
            {
                get { return "TOC Analysis"; }
            }
            /// <summary>
            /// Gets the detailed description of ShaleWorkstep
            /// </summary>
            public string Description
            {
                get { return ""; }
            }

            #endregion
        }
        #endregion

        public class UIFactory : WorkflowEditorUIFactory
        {
            /// <summary>
            /// This method creates the dialog UI for the given workstep, arguments
            /// and context.
            /// </summary>
            /// <param name="workstep">the workstep instance</param>
            /// <param name="argumentPackage">the arguments to pass to the UI</param>
            /// <param name="context">the underlying context in which the UI is being used</param>
            /// <returns>a Windows.Forms.Control to edit the argument package with</returns>
            protected override System.Windows.Forms.Control CreateDialogUICore(Workstep workstep, object argumentPackage, WorkflowContext context)
            {
               
                return new ShaleWorkstepUI((ShaleWorkstep)workstep, (Arguments)argumentPackage, context);
               
            }
        }
    }
}