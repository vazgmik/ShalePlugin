using System;
using Slb.Ocean.Core;
using Slb.Ocean.Petrel;
using Slb.Ocean.Petrel.UI;
using Slb.Ocean.Petrel.Workflow;

namespace Shale
{
    /// <summary>
    /// This class will control the lifecycle of the Module.
    /// The order of the methods are the same as the calling order.
    /// </summary>
    public class ShaleModule : IModule
    {
        #region Private Variables
        private Process m_shaleworkstepInstance;
        #endregion
        public ShaleModule()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region IModule Members

        /// <summary>
        /// This method runs once in the Module life; when it loaded into the petrel.
        /// This method called first.
        /// </summary>
        public void Initialize()
        {
            // TODO:  Add ShaleModule.Initialize implementation
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the not UI related components.
        /// (eg: datasource, plugin)
        /// </summary>
        public void Integrate()
        {                    
            // Register ShaleCommandHandler
            PetrelSystem.CommandManager.CreateCommand(ShaleCommandHandler.ID, new Shale.ShaleCommandHandler());
            
            // Register ShaleWorkstep
            ShaleWorkstep shaleworkstepInstance = new ShaleWorkstep();
            PetrelSystem.WorkflowEditor.AddUIFactory<ShaleWorkstep.Arguments>(new ShaleWorkstep.UIFactory());
            PetrelSystem.WorkflowEditor.Add(shaleworkstepInstance);
            m_shaleworkstepInstance = new Slb.Ocean.Petrel.Workflow.WorkstepProcessWrapper(shaleworkstepInstance);
            PetrelSystem.ProcessDiagram.Add(m_shaleworkstepInstance, "Plug-ins");
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {    
            // Add Ribbon Configuration file
            PetrelSystem.ConfigurationService.AddConfiguration(Shale.Properties.Resources.OceanRibbon);
            // Register Menu Item for Shale.ShaleCommand command. Will be available in Petrel Classic mode.
            Slb.Ocean.Petrel.UI.Tools.WellKnownMenus.Tools.AddTool(new Slb.Ocean.Petrel.UI.Tools.PetrelCommandTool(new Slb.Ocean.Petrel.Commands.CommandItem(ShaleCommandHandler.ID)));
        }

        /// <summary>
        /// This method called once in the life of the module; 
        /// right before the module is unloaded. 
        /// It is usually when the application is closing.
        /// </summary>
        public void Disintegrate()
        {
            // Unregister ShaleWorkstep
            PetrelSystem.WorkflowEditor.RemoveUIFactory<ShaleWorkstep.Arguments>();
            PetrelSystem.ProcessDiagram.Remove(m_shaleworkstepInstance);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // TODO:  Add ShaleModule.Dispose implementation
        }

        #endregion

    }


}