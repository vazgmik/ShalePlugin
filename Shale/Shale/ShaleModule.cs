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
            
            // TODO:  Add ShaleModule.Integrate implementation
            
            // Register ShaleCommandHandler
            PetrelSystem.CommandManager.CreateCommand(ShaleCommandHandler.ID, new Shale.ShaleCommandHandler());
        }

        /// <summary>
        /// This method runs once in the Module life. 
        /// In this method, you can do registrations of the UI related components.
        /// (eg: settingspages, treeextensions)
        /// </summary>
        public void IntegratePresentation()
        {

            // TODO:  Add ShaleModule.IntegratePresentation implementation
            
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
            // TODO:  Add ShaleModule.Disintegrate implementation
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