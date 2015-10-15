﻿using System;

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

namespace Shale
{
    /// <summary>
    /// This class contains all the methods and subclasses of the ShaleWorkstep.
    /// Worksteps are displayed in the workflow editor.
    /// </summary>
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
            public void UpdateCorrelationData2D(WellLog log1, WellLog log2, CorrelationData2D cor)
            {

                /*Шаг 1:Calculation of LogR of all Resistivity logs 
                        Исходные данные:каротажная кривая сопротивления (LLD) 
                        Результат: логарифм кривой сопротивления                 */
                using (ITransaction trans = DataManager.NewTransaction())
                {
                    trans.Lock(cor);
                    List<Point2> pointList = new List<Point2>();
                    for (int sampleIndex = 0; sampleIndex < log1.SampleCount;sampleIndex++)
                    {
                        if (!float.IsNaN(log1[sampleIndex].Value) && !float.IsNaN(log2[sampleIndex].Value) && log1[sampleIndex].Value > 0)
                        {
                            double db_log = Convert.ToSingle(Math.Log(Convert.ToDouble(log1[sampleIndex].Value)));
                            pointList.Add(new Point2(db_log, log2[sampleIndex].Value));
                        }
                    }

                    cor.Name = "LogR vs Sonic";
                    cor.NameX = log1.WellLogVersion.Name;
                    cor.NameY = log2.WellLogVersion.Name;
                    cor.TemplateX = log1.WellLogVersion.Template;
                    cor.TemplateY = log2.WellLogVersion.Template;
                    cor.Points = pointList;        
                    trans.Commit();
                }
            }
            public override void ExecuteSimple()
            {
                CorrelationData2D cor;
                using (ITransaction trans = DataManager.NewTransaction())
                {
                    /*Шаг 2:CreatingCross‐PlotLogRvs.Sonic (DT) 
                      Исходные данные:  
                      Данные кривой сопротивления,  
                      Каротажная кривая акустики 
                      Результат:  
                      Кросс плот (ось X- сопротивление, ось Y- акустика)                     */            
                    trans.Lock(PetrelProject.PrimaryProject);
                    Collection col = PetrelProject.PrimaryProject.CreateCollection("Correlation collection");
                    
                    cor = col.CreateCorrelationData2D("Cross plot");
                    
                    trans.Commit();              
                }
                UpdateCorrelationData2D(arguments.ShaleWellLog, arguments.SonicLog, cor);

                // window for crossplot 
                ToggleWindow window = PetrelProject.ToggleWindows.Add(WellKnownWindows.Function);
                window.ShowObject(cor);
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

            public Slb.Ocean.Petrel.DomainObject.Well.WellLog SonicLog
            {
                get { return sonicLog; }
                set { sonicLog = value; }
            }
            public Slb.Ocean.Petrel.DomainObject.Well.WellLog ShaleWellLog
            {
                internal get { return this.shaleWellLog; }
                set { this.shaleWellLog = value; }
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