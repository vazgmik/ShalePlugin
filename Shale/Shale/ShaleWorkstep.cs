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
using Slb.Ocean.Petrel.DomainObject.Basics;
using System.Drawing;

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
        public MyData(List<WellLog> w,TemplateType t)
        {
            this.log = w;
            this.type = t;
            this.Select_index = -1;
            this.check = false;
        }
        // These statements declare the struct fields and set the default values.
        private TemplateType type;

        public TemplateType Type
        {
            get { return type; }
            set { type = value; }
        }
        

        private List<WellLog> log = new List<WellLog>();

        public List<WellLog> Log
        {
            get { return log; }
            set { log = value; }
        }

 
        private int md_start;

        public int Md_start
        {
            get { return md_start; }
            set { md_start = value; }
        }

        private int md_end;

        public int Md_end
        {
            get { return md_end; }
            set { md_end = value; }
        }

        private int select_index;

        public int Select_index
        {
            get { return select_index; }
            set { select_index = value; }
        }

        private bool check;

        public bool Check
        {
            get { return check; }
            set { check = value; }
        }

        private Image img;

        public Image Img
        {
            get { return img; }
            set { img = value; }
        }

        // Other methods, fields, properties, and events.
    }

    // set name window 
    public static class Helper
    {
        public static void SetNameWindow(ToggleWindow window, String name)
        {
            // function to set window name in petrel
            INameInfoFactory nameFactory = (null != window) ? CoreSystem.GetService<INameInfoFactory>(window) : null;
            var nameInfo = (null != nameFactory) ? nameFactory.GetNameInfo(window) : null;
            if (null != nameInfo && nameInfo.CanChangeName)
                nameInfo.Name = name;
        }

        public static int Partition<T>(this IList<T> list, int start, int end, Random rnd = null) where T : IComparable<T>
        {
            if (rnd != null)
                list.Swap(end, rnd.Next(start, end));

            var pivot = list[end];
            var lastLow = start - 1;
            for (var i = start; i < end; i++)
            {
                if (list[i].CompareTo(pivot) <= 0)
                    list.Swap(i, ++lastLow);
            }
            list.Swap(end, ++lastLow);
            return lastLow;
        }

        /// <summary>
        /// Returns Nth smallest element from the list. Here n starts from 0 so that n=0 returns minimum, n=1 returns 2nd smallest element etc.
        /// Note: specified list would be mutated in the process.
        /// Reference: Introduction to Algorithms 3rd Edition, Corman et al, pp 216
        /// </summary>
        public static T NthOrderStatistic<T>(this IList<T> list, int n, Random rnd = null) where T : IComparable<T>
        {
            return NthOrderStatistic(list, n, 0, list.Count - 1, rnd);
        }
        private static T NthOrderStatistic<T>(this IList<T> list, int n, int start, int end, Random rnd) where T : IComparable<T>
        {
            while (true)
            {
                var pivotIndex = list.Partition(start, end, rnd);
                if (pivotIndex == n)
                    return list[pivotIndex];

                if (n < pivotIndex)
                    end = pivotIndex - 1;
                else
                    start = pivotIndex + 1;
            }
        }

        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            if (i == j)   //This check is not required but Partition function may make many calls so its for perf reason
                return;
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        /// <summary>
        /// Note: specified list would be mutated in the process.
        /// </summary>
        public static T Median<T>(this IList<T> list) where T : IComparable<T>
        {
            return list.NthOrderStatistic((list.Count - 1) / 2);
        }

        public static double Median<T>(this IEnumerable<T> sequence, Func<T, double> getValue)
        {
            var list = sequence.Select(getValue).ToList();
            var mid = (list.Count - 1) / 2;
            return list.NthOrderStatistic(mid);
        }
    };
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
            private int log_counter = 0;
            public Executor(Arguments arguments, WorkflowRuntimeContext context)
            {
                this.arguments = arguments;
                this.context = context;
            }

            /// <summary>
            /// Partitions the given list around a pivot element such that all elements on left of pivot are <= pivot
            /// and the ones at thr right are > pivot. This method can be used for sorting, N-order statistics such as
            /// as median finding algorithms.
            /// Pivot is selected ranodmly if random number generator is supplied else its selected as last element in the list.
            /// Reference: Introduction to Algorithms 3rd Edition, Corman et al, pp 171
            /// </summary>
            

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
                    ++log_counter;
                    log.Samples = tsamples;
                    trans.Commit();
                }
                return log;
            }

            public Template CreateCustomFractionTemplate()
            {
                ITemplateService templateSv = PetrelSystem.TemplateService;
                Template gr;
                gr = PetrelProject.WellKnownTemplates.MiscellaneousGroup.Fraction;
                TemplateCollection templateColl = gr.TemplateCollection;
                string name = templateSv.GetUniqueName(gr.Name);
                Template template;
                using (ITransaction trans = DataManager.NewTransaction())
                {
                    trans.Lock(templateColl);
                    template = templateColl.CreateTemplateFromReference(name, gr);                                                                    
                    trans.Commit();
                }

                return template;
            }

            public float GetMedian(List<WellLogSample> samples, int startRngVal, int endRngVal)
            {
                List<float> _base = new List<float>();
                foreach (WellLogSample sample in samples)
                {
                    if (sample.MD > startRngVal && sample.MD < endRngVal)
                    {
                        _base.Add(sample.Value);
                    }
                }
               return Helper.Median<float>(_base);
            }


            public void CalculateTOC(MyData data, List<WellLogSample> res_samples, int index, double k, string name1, string name2, Borehole bhole)
            {
                // function to calculate TOC
                if (data.Log.Count != 0)
                {

                    //System.IO.StreamWriter file = new System.IO.StreamWriter("d:\\test.txt");
                    List<WellLogSample>tmpsamples = new List<WellLogSample>();
                    List<WellLogSample> type_samples = data.Log[data.Select_index].Samples.ToList();
                    
                    float max_resval = GetMedian(res_samples, arguments.List_res[index].Md_start, arguments.List_res[index].Md_end);//20;
                    float max_dtcval = GetMedian(type_samples, data.Md_start,data.Md_end);//65;

                    List<WellLogSample> tocs_samples = new List<WellLogSample>();
                    int size = Math.Min(arguments.List_res[index].Log[arguments.List_res[index].Select_index].SampleCount, data.Log[data.Select_index].SampleCount);
                    double power = 0.297 - 0.1688 * 13;//LOM  надо подставить здесь 13
                    for (int i = 0; i < size; ++i)
                    {
                        if (!res_samples[i].Value.Equals(float.NaN) && !type_samples[i].Value.Equals(float.NaN))
                        {
                            // log (RESD / RESDbase) + 0.02 * (DTC – DTCbase)
                            double part1 = Math.Log((double)(res_samples[i].Value / max_resval),10);
                            double part2 = 0.02 * (type_samples[i].Value - max_dtcval) / 3.281;//*3.28 * (float)Math.Pow(10,-6);
                            float fl_typelog = (float)(part1 + part2);
                            tmpsamples.Add(new WellLogSample(res_samples[i].MD, fl_typelog));
                            float fl_typetoc = fl_typelog * (float)Math.Pow(10, power);
                            tocs_samples.Add(new WellLogSample(res_samples[i].MD, fl_typetoc));
                           
                          //  file.WriteLine(fl_typetoc);

                        }
                    }
                    
                    Template t = CreateCustomFractionTemplate();
                    var typeLogR = CreateWellLog(bhole, name1, t, tmpsamples);
                    var typeTOC = CreateWellLog(bhole, name2, t, tocs_samples);
                     //   file.Close();
                      
                    
                }

            }


            //Well space - Geomechanical: Poisson ratio, Young Modulus, Average Brittleness estimations
            public void BritleAnalysis()
            {
                /*Poissson_Ratio=0.5*(DTS*DTS-2*DT*DT)/(DTS*DTS-DT*DT)
                    young_modul=2*13475*RHOB_GC*(1+Possson_Ratio)/(DTS*DTS)
                    V_br=(Possson_Ratio-0.15)/(0.15) Possson_Ratio  ?max and ?min are the maximum and minimum values 
                    E_br=(young_modul-0.7)/7.3 young_modul Emin and Emax
                    BA= 100*(E_br+V_br)/2

                    DTS - this is S-sonic vs
                    DT and DTC- these are P-sonic vp
                    RHOB_GC - density */
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
                //SetNameWindow(window, name);
                Helper.SetNameWindow(window, name);
                window.ShowObject(cor);
            }
            public override void ExecuteSimple()
            {
                /*Шаг 1:Calculation of LogR of all Resistivity logs 
                        Исходные данные:каротажная кривая сопротивления (LLD) 
                        Результат: логарифм кривой сопротивления                 */
                if (arguments.Boreholes.Count == 0 || arguments.List_res.Count == 0 || arguments.List_vitr.Count == 0) 
                    return;

                log_counter = 0;
                int index = 0;
                foreach (Borehole bhole in arguments.Boreholes)
                {
                    //check if no resistivity data or vitrinite when continue
                    if (arguments.List_res[index].Log.Count == 0 || arguments.List_vitr[index].Log.Count == 0)
                    {
                        index++;
                        continue;
                    }
                    //resistivity samples
                    var samples = arguments.List_res[index].Log[arguments.List_res[index].Select_index].Samples;
                    
                    //create list res_samples,contains samples of resistivity log 
                    List<WellLogSample> res_samples = samples.ToList();
                    //tmp contains Math.Log(values) of samples
                    List<WellLogSample> tmpsamples = new List<WellLogSample>();
                    
                    foreach (WellLogSample sample in samples)
                    {
                        if (sample.Value > 0 && !sample.Value.Equals(float.NaN))
                        {
                            float fl_log =(float)Math.Log((double)sample.Value,10 );
                            tmpsamples.Add(new WellLogSample(sample.MD, fl_log));
                        }
                    }

                    // if sonic checked then we can draw plot LogR vs Sonic
                    if (arguments.List_son[index].Check && arguments.List_son[index].Log.Count != 0)
                    {
                        var LogR = CreateWellLog(bhole, "LogR", arguments.List_res[index].Log[arguments.List_res[index].Select_index].WellLogVersion.Template, tmpsamples);

                        using (ITransaction trans = DataManager.NewTransaction())
                        {
                            /*
                             usec = микросекунда 10^-6 -- sonic log
                             1 ft = 0,3048 m -- 
                             usec/ft = 3.2808399 × 10-6 s / m
                             gm/cc = 1000 kg / m3
                             */
                            /*Шаг 2:CreatingCross‐PlotLogRvs.Sonic (DT) 
                              Исходные данные:  
                              Данные кривой сопротивления,  
                              Каротажная кривая акустики 
                              Результат:  
                              Кросс плот (ось X- сопротивление, ось Y- акустика)                             */
                            trans.Lock(PetrelProject.PrimaryProject);
                            Collection col = PetrelProject.PrimaryProject.CreateCollection("Correlation collection");
                            CorrelationData2D cor = col.CreateCorrelationData2D("Cross plot");

                            UpdateDrawCorrelationData2D(LogR, arguments.List_son[index].Log[arguments.List_son[index].Select_index], cor, bhole.Name + ":LogR vs Sonic");
                            trans.Commit();
                        }
                    }


                    if (arguments.List_son[index].Check &&  arguments.List_son[index].Select_index >=0 && arguments.List_son[index].Log.Count >0)
                    {
                        //create SLogR and TOCs
                        CalculateTOC(arguments.List_son[index], res_samples, index, 0.02, "SlogR", "TOCs", bhole);
                    }
                    if (arguments.List_den[index].Check &&  arguments.List_den[index].Select_index >=0 && arguments.List_den[index].Log.Count >0 )
                    {
                        //create DlogR and TOCd
                        CalculateTOC(arguments.List_den[index], res_samples, index, -2.5, "DlogR", "TOCd", bhole);
                    }

                    if (arguments.List_por[index].Check && arguments.List_por[index].Select_index >= 0 && arguments.List_por[index].Log.Count > 0)
                    {
                        //create DlogR and TOCd
                        CalculateTOC(arguments.List_por[index],res_samples, index, 4.0, "NlogR", "TOCn", bhole);
                    }

                    index++;
                }
                arguments.Log_count = log_counter;



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

            private int log_count;

            public int Log_count
            {
                get { return log_count; }
                set { log_count = value; }
            }
            private List<Borehole> boreholes = new List<Borehole>();

         
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

            public Dictionary<string,double[]> md_range = new Dictionary<string,double[]>();
            
            public Dictionary<string, double[]> Md_range
            {
                get { return md_range; }
                set { md_range = value; }
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