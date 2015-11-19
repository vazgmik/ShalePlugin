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
using System.Data;
using Slb.Ocean.Petrel.DomainObject;
using Slb.Ocean.Petrel.DomainObject.Basics;
using System.Collections;
using Slb.Ocean.Petrel.UI.WellSection;
using System.Linq;

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
        private FormatTemplate wsTemplate;
        private List<MyData> res = new List<MyData>();
        private List<MyData> vitr = new List<MyData>();
        private List<MyData> son = new List<MyData>();
        private List<MyData> den = new List<MyData>();
        private List<MyData> por = new List<MyData>();

        //temporary arguments
        private ShaleWorkstep.Arguments tmpargs = new ShaleWorkstep.Arguments();
        /// <summary>
        /// Contains the actual underlaying context.
        /// </summary>
        private WorkflowContext context;

        //boreholes collection
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
            btn_Apply.Image = Shale.Properties.Resources.IDB_BUTTON_APPLY;
            btn_DelBoreHole.Image = PetrelImages.RowDelete;

            this.workstep = workstep;
            this.args = args;
            this.context = context;
            
            workstep.CopyArgumentPackage(args, tmpargs);
            AddMdRange();
        }


        //create template for wellsection
        private void CreateFormatTemplate()
        {    
            string templateName = "TOCWSTemplate";
            string collName = "TOCWSTemplates";
            //WellSectionService 

            FormatTemplateCollection root = WellSectionProject.FormatTemplateCollection;

            using (ITransaction trans = DataManager.NewTransaction())
            {
                trans.Lock(root);
                collName = FormatTemplateCollection.GetUniqueFormatTemplateCollectionName(collName);
                FormatTemplateCollection subColl;
                subColl = root.CreateFormatTemplateCollection(collName);
                templateName = FormatTemplateCollection.GetUniqueFormatTemplateName(templateName);
                wsTemplate = subColl.CreateFormatTemplate(templateName);
                trans.Commit();
            }
        }


        //add vals to dictionary for MD range proccessing
        public void AddMdRange() {
            string [] labels = {"res","son","den","por"};
            foreach(var s in  labels){
                tmpargs.Md_range.Add( s,new double[2]);
            }
        }

        public void FillDic(string str, double start, double end)
        {
            tmpargs.Md_range[str][0] = start;
            tmpargs.Md_range[str][1] = end;
        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            FillDic("res", Convert.ToDouble(MD_st_res.Text), Convert.ToDouble(MD_en_res.Text));
            FillDic("son", Convert.ToDouble(MD_st_son.Text), Convert.ToDouble(MD_en_son.Text));
            FillDic("den", Convert.ToDouble(MD_st_dens.Text), Convert.ToDouble(MD_en_dens.Text));
            FillDic("por", Convert.ToDouble(MD_st_por.Text), Convert.ToDouble(MD_en_por.Text));

            tmpargs.Boreholes = bholes;
            tmpargs.List_den = den;
            tmpargs.List_por = por;
            tmpargs.List_res = res;
            tmpargs.List_son = son;
            tmpargs.List_vitr = vitr;

            if (context is WorkstepProcessWrapper.Context)
            {
                Executor exec = workstep.GetExecutor(tmpargs, new WorkstepProcessWrapper.RuntimeContext());
                exec.ExecuteSimple();
            }

            workstep.CopyArgumentPackage(tmpargs, args);
            context.OnArgumentPackageChanged(this, new WorkflowContext.ArgumentPackageChangedEventArgs());

            if (chbWellSection.Checked)
            {
                //Creating WellSection using Template
                CreateFormatTemplate();
                WellSectionWindow wsw = WellSectionWindow.CreateWindow(wsTemplate);         
                Helper.SetNameWindow(wsw, "TOC_Analysis_WellSection");
                wsw.Domain = Slb.Ocean.Petrel.DomainObject.Domain.MD;
                foreach (Borehole bhole in tmpargs.Boreholes)
                {
                    wsw.ShowObject(bhole);
                }

                var well_ver = args.Boreholes[0].LogCollection.LogVersionCollection.WellLogVersions.ToList();
                for (int start = well_ver.Count-1; start >= well_ver.Count - args.Log_count; --start)
                {
                    wsw.ShowObject(well_ver[start]);
                }

            }

        }
        public void InitForm() {
            Set_MD_StartEnd("Resistivity",false);
            Set_MD_StartEnd("Sonic",false);
            Set_MD_StartEnd("Density",false);
            Set_MD_StartEnd("Porosity", false);

           /* MD_en_dens.ResetText();
            MD_en_por.ResetText();
            MD_en_son.ResetText();
            MD_st_dens.ResetText();
            MD_st_por.ResetText();
            MD_st_son.ResetText();
            MD_st_res.ResetText();
            MD_en_res.ResetText();*/


            chbPorosity.Text = "Porosity";
            chbPorosity.Checked = false;
            cmbPorosity.Enabled = false;
            chbPorosity.Enabled = false;
            WARNINGPOR.Image = null;

            chbSonic.Text = "Sonic";
            cmbSonic.Enabled = false;
            chbSonic.Checked = false;
            chbSonic.Enabled = false;
            WARNINGSON.Image = null;

            chbDensity.Text = "Density";
            cmbDensity.Enabled = false;
            chbDensity.Checked = false;
            chbDensity.Enabled = false;
            WARNINGDEN.Image = null;

            cmbResistivity.Enabled = false;
            res_Label.Text = "ResistivityLog";
            WARNINGRES.Image = null;

            cmbVitrinite.Enabled = false;
            vitr_Label.Text = "VitriniteLog";
            WARNINGVITR.Image = null;          
        }
    
        private bool OneSelectedItem()
        {
            var sel_items = list_Boreholes.SelectedItems;
            int count = 0;
            foreach (var cur in sel_items)
            {
                count++;
                if (count > 1) 
                    return false;
            }
            return true;
        }

        private void clearCombos()
        {
            cmbResistivity.Items.Clear();
            cmbVitrinite.Items.Clear();
            cmbDensity.Items.Clear();
            cmbPorosity.Items.Clear();
            cmbSonic.Items.Clear();
            cmbResistivity.Text = "";
            cmbVitrinite.Text = "";
            cmbDensity.Text = "";
            cmbPorosity.Text = "";
            cmbSonic.Text = "";
        }


        //delete Borehole button
        private void btn_DelBoreHole_Click(object sender, EventArgs e)
        {
            clearCombos();
            var selItems = list_Boreholes.SelectedItems.ToList();
            int index = 0; 
            foreach (var cur in selItems)
            {
                list_Boreholes.Items.Remove(cur);
                index = bholes.IndexOf(cur.Value as Borehole);
                RemoveData(index);
                bholes.RemoveAt(index);           
            }

        }

        //remove wellogs using index of borehole
        private void RemoveData(int index)
        {
            res.RemoveAt(index);
            vitr.RemoveAt(index);
            den.RemoveAt(index);
            son.RemoveAt(index);
            por.RemoveAt(index);
        }


        //add,check type welllogs
        private List<WellLog> GetCorrectType(Borehole borehole,TemplateType type)
        {
            return borehole.LogCollection.WellLogs.Where(p => p.WellLogVersion.Template.TemplateType == type).Select(p => p).ToList();           
        }

        // update and collect welllogs in arguments collections(res,vitr,son,den,por);
        private void AddData(Borehole borehole)
        {    
            var tmp_res = GetCorrectType(borehole,TemplateType.ResistivityDeep);
            var tmp_vitr = GetCorrectType(borehole, TemplateType.VitriniteReflectance);
            var tmp_son = GetCorrectType(borehole, TemplateType.Psonic);
            var tmp_den = GetCorrectType(borehole, TemplateType.DensityCompensatedBulk);
            var tmp_por = GetCorrectType(borehole, TemplateType.Porosity);

            res.Add(new MyData(tmp_res, TemplateType.ResistivityDeep));
            vitr.Add(new MyData(tmp_vitr, TemplateType.VitriniteReflectance));
            son.Add(new MyData(tmp_son, TemplateType.Psonic));
            den.Add(new MyData(tmp_den, TemplateType.Density));
            por.Add(new MyData(tmp_por, TemplateType.Porosity));
        }

        //add data to list and show it
        void AddListData(Borehole borehole, int index)
        {
            //adding data to wellog collections
            AddData(borehole);

            String resist_ = res[index].Log.Count == 0? "0" : res[index].Log.Count.ToString();
            String vitrin_ = vitr[index].Log.Count == 0 ? "0" : vitr[index].Log.Count.ToString();
            String den_ = den[index].Log.Count == 0 ? "0" : den[index].Log.Count.ToString();
            String por_ = por[index].Log.Count == 0 ? "0" : por[index].Log.Count.ToString();
            String son_ = son[index].Log.Count == 0 ? "0" : son[index].Log.Count.ToString();

            IImageInfoFactory f = CoreSystem.GetService<IImageInfoFactory>(borehole);
            var item = new ListBoxItem();
            item.Text = string.Format("{0} (Resistivity: {1}; Vitrinite: {2},Density:{3},Porosity:{4},Sonic:{5})", borehole.Name, resist_, vitrin_, den_, por_, son_);

            if (res[index].Log.Count == 0 || vitr[index].Log.Count == 0 || res[index].Log.Count > 1 || vitr[index].Log.Count > 1)
                item.Image = PetrelImages.Warning;
            else if (den[index].Log.Count != 0 || por[index].Log.Count != 0 || son[index].Log.Count != 0)
                item.Image = f.GetImageInfo(borehole).TypeImage;
            else
                item.Image = PetrelImages.Warning;

            item.Value = borehole;
            list_Boreholes.Items.Add(item);
        }

        //adding boreholes to borehole list,collecting data(welllogs) for computing and show results in list
        private void UpdateBoreholeList(Borehole borehole, int index)
        {
            if (!bholes.Contains(borehole))
            {             
                AddListData(borehole, index);
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
                        UpdateBoreholeList(rawData as Borehole, bholes.Count);
                        break;
                    }
                case "BoreholeCollection":
                    {
                        var boreholes = rawData as BoreholeCollection;
                        foreach (var borehole in boreholes)
                            UpdateBoreholeList(borehole, bholes.Count);
                        break;
                    }
                default:
                    PetrelLogger.ErrorBox("Object is not a Borehole!");
                    break;
            }
        }


        private void Set_MD_StartEnd(string type,bool stat)
        {
            switch(type)
            {
                case "Resistivity":
                    MD_st_res.Enabled = stat;
                    MD_en_res.Enabled = stat;                    
                    MD_st_res.Value = 0;
                    MD_en_res.Value = 0;
                    break;
                case "Sonic":
                    MD_st_son.Enabled = stat;
                    MD_en_son.Enabled = stat;
                    MD_st_son.Value = 0;
                    MD_en_son.Value = 0;
                    break;
                case "Density":
                    MD_st_dens.Enabled = stat;
                    MD_en_dens.Enabled = stat;
                    MD_st_dens.Value = 0;
                    MD_en_dens.Value = 0;
                    break;
                case "Porosity":
                    MD_st_por.Enabled = stat;
                    MD_en_por.Enabled = stat;
                    MD_st_por.Value = 0;
                    MD_en_por.Value = 0;
                    break;
            }
        }


        // check warning for resistivity,vitrinite
        private void WarningCheck(ref List<MyData> data, ref Label t_warn, ref Slb.Ocean.Petrel.UI.Controls.ComboBox cmb, ref Label type_log, string type,int index)
        {
            if (data[index].Log.Count == 0)
            {
                t_warn.Image = Shale.Properties.Resources.IDB_BUTTON_MS_ERROR;
                cmb.Enabled = false;
               // MD_st_res.ResetText();      
               // MD_en_res.ResetText();
                Set_MD_StartEnd("Resistivity", false);
            }
            else
            {
                cmb.Enabled = true;

                if (data[index].Select_index == -1)
                {
                    cmb.SelectedIndex = 0;
                    data[index].Select_index = 0;
                    Set_MD_StartEnd("Resistivity", true);
                    type_log.Text = type;
                    if (data[index].Log.Count > 1)
                    {
                        t_warn.Image = PetrelImages.Warning;
                        type_log.Text += " (Default)";
                    }
                    else
                    {
                        t_warn.Image = Shale.Properties.Resources.IDB_BUTTON_APPLY;
                    }

                    data[index].Img = t_warn.Image;
                }
                else
                {                 
                    cmb.Enabled = true;
                    cmb.SelectedIndex = data[index].Select_index;
                    t_warn.Image = data[index].Img;
                    if (type == "Resistivity Log")
                    {
                        Set_MD_StartEnd("Resistivity", true);
                        MD_st_res.Value = data[index].Md_start;
                        MD_en_res.Value = data[index].Md_end;
                    }
                }


               
            }       
        }

        // check warning for sonic,dens,por
        private void WarningCheck(ref List<MyData> data, ref Label t_warn, ref Slb.Ocean.Petrel.UI.Controls.ComboBox cmb, ref CheckBox chb, string type, ref NumericUpDown start, ref NumericUpDown end, int index)
        {
            if (data[index].Log.Count == 0)
            {
                t_warn.Image = Shale.Properties.Resources.IDB_BUTTON_MS_ERROR;
                cmb.Enabled = false;
               // start.ResetText();
                start.Enabled = false;
               // end.ResetText();
                end.Enabled = false;
            }
            else
            {
                cmb.Enabled = true;
                start.Enabled = true;
                end.Enabled = true;
                if (data[index].Select_index == -1)
                {
                    cmb.SelectedIndex = 0;
                    data[index].Select_index = 0;
                    chb.Text = type;
                    if (data[index].Log.Count > 1)
                    {
                        t_warn.Image = PetrelImages.Warning;
                        chb.Text += " (Default)";
                    }
                    else
                    {
                        t_warn.Image = Shale.Properties.Resources.IDB_BUTTON_APPLY;
                    }
                    data[index].Img = t_warn.Image;
                }
                else
                {
                    cmb.SelectedIndex = data[index].Select_index;
                    t_warn.Image = data[index].Img;
                    switch(type)
                    {
                        case "Sonic":
                            Set_MD_StartEnd("Sonic", true);
                            MD_st_son.Value = data[index].Md_start;
                            MD_en_son.Value = data[index].Md_end;

                            break;
                        case "Density":
                            Set_MD_StartEnd("Density", true);
                            MD_st_dens.Value = data[index].Md_start;
                            MD_en_dens.Value = data[index].Md_end;
                            break;
                        case "Porosity":
                            Set_MD_StartEnd("Porosity", true);
                            MD_st_por.Value = data[index].Md_start;
                            MD_en_por.Value = data[index].Md_end;
                            break;
                    }

                }
            }

        }

        private void LoadCmb(ref Slb.Ocean.Petrel.UI.Controls.ComboBox cmb,MyData data)
        {  
            foreach(var val in data.Log)
            {
                IImageInfoFactory f = CoreSystem.GetService<IImageInfoFactory>(val);
                var cmbitem = new ComboBoxItem();
                cmbitem.Text = string.Format("{0} ({1})", val.Name, data.Type);
                cmbitem.Image = f.GetImageInfo(val).TypeImage;
                cmbitem.Value = val;      
                cmb.Items.Add(cmbitem);
            }
        }

        private void CheckBoxEnabled(ref CheckBox chb,int count)
        {
            chb.Enabled = false;
            if (cmbResistivity.Enabled && cmbVitrinite.Enabled && count >0)
                    chb.Enabled = true;                   
        }

        private void SelItems_Change(object sender, EventArgs e)
        {
            //can process only one selected well
            if (OneSelectedItem())
            {
                InitForm();
                clearCombos();
                foreach (var cur in list_Boreholes.SelectedItems)
                {
                    int index = bholes.IndexOf(cur.Value as Borehole);
                    LoadCmb(ref cmbResistivity, res[index]);
                    LoadCmb(ref cmbVitrinite, vitr[index]);
                    LoadCmb(ref cmbSonic, son[index]);
                    LoadCmb(ref cmbDensity, den[index]);
                    LoadCmb(ref cmbPorosity, por[index]);

                    WarningCheck(ref res,ref WARNINGRES, ref cmbResistivity, ref res_Label,"Resistivity Log",index);
                    WarningCheck(ref vitr, ref WARNINGVITR, ref cmbVitrinite, ref vitr_Label, "Vitrinite Log",index);
                    chbSonic.Checked = son[index].Check;
            
                }
            }
          
            CheckBoxEnabled(ref chbSonic, cmbSonic.Items.Count);
            CheckBoxEnabled(ref chbDensity, cmbDensity.Items.Count);
            CheckBoxEnabled(ref chbPorosity, cmbPorosity.Items.Count);  
        }

        private void cmbResistivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OneSelectedItem())
                foreach (var cur in list_Boreholes.SelectedItems)
                {
                    int index = bholes.IndexOf(cur.Value as Borehole);
                    res_Label.Text = "Resistivity Log";
                    res[index].Select_index = cmbResistivity.SelectedIndex;
                }           
        }

        private void MD_st_res_ValueChanged(object sender, EventArgs e)
        {
            if (OneSelectedItem())
                foreach (var cur in list_Boreholes.SelectedItems)
                {
                    int index = bholes.IndexOf(cur.Value as Borehole);
                    res[index].Md_start = (int)MD_st_res.Value;
                }
        }

        private void MD_en_res_ValueChanged(object sender, EventArgs e)
        {
            if (OneSelectedItem())
                foreach (var cur in list_Boreholes.SelectedItems)
                {
                    int index = bholes.IndexOf(cur.Value as Borehole);
                    res[index].Md_end = (int)MD_en_res.Value;
                }
        }

        private void cmbVitrinite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OneSelectedItem())
                foreach (var cur in list_Boreholes.SelectedItems)
                {
                    int index = bholes.IndexOf(cur.Value as Borehole);
                    vitr_Label.Text = "Vitrinite Log";
                    vitr[index].Select_index = cmbVitrinite.SelectedIndex;
                }    
           
        }

        private void ClearSonicRaw()
        {
            chbSonic.Text = "Sonic";
            cmbSonic.SelectedIndex = -1;
            cmbSonic.Enabled = false;
            MD_st_son.Enabled = false;
            MD_en_son.Enabled = false;
            MD_st_son.Value = 0;
            MD_en_son.Value = 0;
            WARNINGSON.Image = null;    
        }
        private void chbSonic_CheckStateChanged(object sender, EventArgs e)
        {    
            if (OneSelectedItem())
            {
                foreach (var cur in list_Boreholes.SelectedItems)
                {
                    int index = bholes.IndexOf(cur.Value as Borehole);
                    if (chbSonic.Checked)
                    {
                        WarningCheck(ref son, ref WARNINGSON, ref cmbSonic, ref chbSonic, "Sonic", ref MD_st_son, ref MD_en_son,index);
                        son[index].Check = true;
                    }
                    else
                    {
                        son[index].Check = false;
                        son[index].Select_index = -1;
                        son[index].Img = null;
                        son[index].Md_start = 0;
                        son[index].Md_end = 0;
                        ClearSonicRaw();
                    }
                }
            }
                 
        }


        private void cmbSonic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OneSelectedItem())
                foreach (var cur in list_Boreholes.SelectedItems)
                {
                    int index = bholes.IndexOf(cur.Value as Borehole);
                    chbSonic.Text = "Sonic";
                    son[index].Select_index = cmbSonic.SelectedIndex;
                }                   
        }

        private void MD_st_son_ValueChanged(object sender, EventArgs e)
        {
            if (OneSelectedItem())
                foreach (var cur in list_Boreholes.SelectedItems)
                {
                    int index = bholes.IndexOf(cur.Value as Borehole);
                    son[index].Md_start = (int)MD_st_son.Value;
                }
        }

        private void MD_en_son_ValueChanged(object sender, EventArgs e)
        {
            if (OneSelectedItem())
                foreach (var cur in list_Boreholes.SelectedItems)
                {
                    int index = bholes.IndexOf(cur.Value as Borehole);
                    son[index].Md_end = (int)MD_en_son.Value;
                }
        }

        private void ClearDensityRaw()
        {
            chbDensity.Text = "Density";
            cmbDensity.SelectedIndex = -1;
            cmbDensity.Enabled = false;
            MD_st_dens.Enabled = false;
            MD_en_dens.Enabled = false;
            //MD_st_dens.ResetText();
            //MD_en_dens.ResetText();
            WARNINGDEN.Image = null;
        }

        private void chbDensity_CheckStateChanged(object sender, EventArgs e)
        {      
            foreach (var cur in list_Boreholes.SelectedItems)
            {
                int index = bholes.IndexOf(cur.Value as Borehole);
                if (chbDensity.Checked && OneSelectedItem()) 
                {
                    WarningCheck(ref den, ref WARNINGDEN, ref cmbDensity, ref chbDensity, "Density", ref MD_st_dens, ref MD_en_dens, index);
                    den[index].Check = true;
                }
                else
                {
                    den[index].Check = false;
                    den[index].Select_index = -1;
                    den[index].Img = null;
                    den[index].Md_start = 0;
                    den[index].Md_end = 0;
                    ClearDensityRaw();
                }
                
            }
        }

        private void MD_st_dens_ValueChanged(object sender, EventArgs e)
        {
            if (OneSelectedItem())
                foreach (var cur in list_Boreholes.SelectedItems)
                {
                    int index = bholes.IndexOf(cur.Value as Borehole);
                    den[index].Md_start = (int)MD_st_dens.Value;
                }
        }

        private void MD_en_dens_ValueChanged(object sender, EventArgs e)
        {
            if (OneSelectedItem())
                foreach (var cur in list_Boreholes.SelectedItems)
                {
                    int index = bholes.IndexOf(cur.Value as Borehole);
                    den[index].Md_end = (int)MD_en_dens.Value;
                }
        }
        private void ClearPorosityRaw()
        {
            chbPorosity.Text = "Porosity";
            cmbPorosity.SelectedIndex = -1;
            cmbPorosity.Enabled = false;
            MD_st_por.Enabled = false;
            MD_en_por.Enabled = false;
           // MD_st_por.ResetText();
           // MD_en_por.ResetText();
            WARNINGPOR.Image = null;
        }
        private void chbPorosity_CheckStateChanged(object sender, EventArgs e)
        {
            foreach (var cur in list_Boreholes.SelectedItems)
            {
                int index = bholes.IndexOf(cur.Value as Borehole);
                if (chbPorosity.Checked && OneSelectedItem())
                {
                    WarningCheck(ref por, ref WARNINGPOR, ref cmbPorosity, ref chbPorosity, "Porosity", ref MD_st_por, ref MD_en_por, index);
                    por[index].Check = true;
                }
                else
                {
                    por[index].Check = false;
                    por[index].Select_index = -1;
                    por[index].Img = null;
                    por[index].Md_start = 0;
                    por[index].Md_end = 0;
                    ClearPorosityRaw();
                }

            }
        }

        private void MD_st_por_ValueChanged(object sender, EventArgs e)
        {
            if (OneSelectedItem())
                foreach (var cur in list_Boreholes.SelectedItems)
                {
                    int index = bholes.IndexOf(cur.Value as Borehole);
                    por[index].Md_start = (int)MD_st_por.Value;
                }
        }

        private void MD_en_por_ValueChanged(object sender, EventArgs e)
        {
            if (OneSelectedItem())
                foreach (var cur in list_Boreholes.SelectedItems)
                {
                    int index = bholes.IndexOf(cur.Value as Borehole);
                    por[index].Md_end = (int)MD_en_por.Value;
                }
        }

        

    }
            
}