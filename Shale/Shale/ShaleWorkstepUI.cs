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

            btn_Apply.Image = PetrelImages.Apply;
            btn_DelBoreHole.Image = PetrelImages.RowDelete;

            this.workstep = workstep;
            this.args = args;
            this.context = context;
            
            workstep.CopyArgumentPackage(args, tmpargs);
        }


        //create template for wellsection
        private void CreateFormatTemplate()
        {
            FormatTemplate wsTemplate;
            string templateName = "My WSTemplate";
            string collName = "My WSTemplates";
            //WellSectionService 
            
            //FormatTemplateCollection root = WellSectionService.FormatTemplateCollection;

            //using (ITransaction trans = DataManager.NewTransaction())
            //{
            //    trans.Lock(root);
            //    collName = FormatTemplateCollection.GetUniqueFormatTemplateCollectionName(collName);
            //    FormatTemplateCollection subColl;
            //    subColl = root.CreateFormatTemplateCollection(collName);
            //    templateName = FormatTemplateCollection.GetUniqueFormatTemplateName(templateName);
            //    wsTemplate = subColl.CreateFormatTemplate(templateName);
            //    trans.Commit();
            //}
        }
        private void btn_Apply_Click(object sender, EventArgs e)
        {
            //dictionaty indices
            //List<KeyValuePair<int, String>> List_ind = new List<KeyValuePair<int, String>>();
            //for (int i = 0; i < bholes.Count; i++)
            //{
            //    if (!(chbSonic.Checked && List_son[i] != null ))
            //        List_ind.Add(new KeyValuePair<int, string>(i, "sonic"));
            //    if (!(chbDensity.Checked && List_den[i]!=null ))
            //        List_ind.Add(new KeyValuePair<int, string>(i, "density"));
            //    if (!(chbPorosity.Checked && List_por[i]!=null))
            //        List_ind.Add(new KeyValuePair<int, string>(i, "porosity"));
            //}
          
            
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
                //Create WellSection
                //CreateFormatTemplate();
                //WellSectionWindow wsw = WellSectionWindow.CreateWindow(wsTemplate);

                //wsw.Domain = Slb.Ocean.Petrel.DomainObject.Domain.MD;

                //wsw.ShowObject(tmpargs.Well);
                //wsw.ShowObject(tmpargs.Hfrac);
                //wsw.ShowObject(tmpargs.Well.Completions.CasingStrings);
                //wsw.ShowObject(tmpargs.Well.Completions.Perforations);
            }

        }

        //delete Borehole button
        private void btn_DelBoreHole_Click(object sender, EventArgs e)
        {
            clearCombos();
            var selItems = list_Boreholes.SelectedItems;
            var tmp = new List<ListBoxItem>();
            int index = 0;
            foreach (var cur in selItems)
                tmp.Add(cur);
            foreach (var cur in tmp)
            {
                list_Boreholes.Items.Remove(cur);
                index = bholes.IndexOf(cur.Value as Borehole);

                res.RemoveAt(index);
                vitr.RemoveAt(index);
                den.RemoveAt(index);
                son.RemoveAt(index);
                por.RemoveAt(index);

                bholes.Remove(cur.Value as Borehole);           
            }

            if(list_Boreholes.Items.Count == 0)
            {
                res.Clear();
                vitr.Clear();
                den.Clear();
                son.Clear();
                por.Clear();
            }
        }


        private void CorrectType(Template templ, TemplateType type, ref List<MyData> list, WellLog wl, int index)
        {
            if (templ.TemplateType.Equals(type))
            {
                if (list.Count == index)
                    list.Add(new MyData(1, wl));
                else if (list.Count == index+1)
                {
                    int count = list[index].Count;
                    list[index] = new MyData(++count, list[index].Log);
                }
            }

        }

        private void CheckAddNull(ref List<MyData> list, int index)
        {
            if (list.Count == index)
                list.Add(null);
        }

        private void updateBoreholeList(Borehole borehole, int index)
        {
            if (!bholes.Contains(borehole))
            {             
                foreach (WellLog wl in borehole.LogCollection.WellLogs)
                {
                    var templ = wl.WellLogVersion.Template;
                    CorrectType(templ, TemplateType.ResistivityDeep, ref res, wl, index);
                    CorrectType(templ, TemplateType.Ssonic, ref son, wl, index);
                    CorrectType(templ, TemplateType.Density, ref den, wl, index);
                    CorrectType(templ, TemplateType.Porosity, ref por, wl, index);
                    CorrectType(templ, TemplateType.VitriniteReflectance, ref vitr, wl, index);

                }

                CheckAddNull(ref res, index);
                CheckAddNull(ref son, index);
                CheckAddNull(ref den, index);
                CheckAddNull(ref por, index);
                CheckAddNull(ref vitr, index);

                String resist = res[index] == null ? "0" : res[index].Count.ToString();
                String vitrin = vitr[index] == null ? "0" : vitr[index].Count.ToString();
                String dens = den[index] == null ? "0" : den[index].Count.ToString();
                String por_ = por[index] == null ? "0" : por[index].Count.ToString();
                String sonn = son[index] == null ? "0" : son[index].Count.ToString();

                IImageInfoFactory f = CoreSystem.GetService<IImageInfoFactory>(borehole);
                var item = new ListBoxItem();
                item.Text = string.Format("{0} (LLD: {1}; Vitrinite: {2},DENS:{3},POR:{4},SONIC:{5})", borehole.Name, resist, vitrin,dens,por_,sonn);

                /*if (res[index] == null || vitr[index] == null || res[index].Count > 1 || vitr[index].Count > 1  )
                {
                    item.Image = PetrelImages.Warning;
                }
                else if (den[index] != null && den[index].Count == 1 || por[index] != null && por[index].Count == 1 || son[index] != null && son[index].Count == 1)
                    item.Image = PetrelImages.Apply;//f.GetImageInfo(borehole).TypeImage;
                else
                    item.Image = PetrelImages.Warning;
                */
                item.Value = borehole;
                list_Boreholes.Items.Add(item);
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
                        updateBoreholeList(rawData as Borehole, bholes.Count);
                        break;
                    }
                case "BoreholeCollection":
                    {
                        var boreholes = rawData as BoreholeCollection;
                        foreach (var borehole in boreholes)
                            updateBoreholeList(borehole, bholes.Count);
                        break;
                    }
                default:
                    PetrelLogger.ErrorBox("Object is not a Borehole!");
                    break;
            }
        }

        private void warning_check(int index, List<MyData> rr,ref Label war) {
            war.Text = "       ";
            if (rr[index] == null )
                war.Image = PetrelImages.Cancel;
            else if (rr[index].Count > 1)
                war.Image = PetrelImages.Warning;
            else
                war.Image = PetrelImages.Apply;

        
        }
        private void SelItems_Change(object sender, EventArgs e)
        {
            
            clearCombos();
            var lst_box = sender as Slb.Ocean.Petrel.UI.Controls.ListBox;
            var sel_items = lst_box.SelectedItems;

            if (!OneSelectedItem())
            {
                cmbResistivity.Enabled = false;
                cmbVitrinite.Enabled = false;
                cmbDensity.Enabled = false;
                cmbPorosity.Enabled = false;
                cmbSonic.Enabled = false;
            }
            else
            {
                foreach (var cur in sel_items)
                {
                    if (cur.Selected)
                    {
                        Borehole borehole = cur.Value as Borehole;
                        var logs = borehole.LogCollection.WellLogs;
                        foreach (WellLog wl in logs)
                        {
                            var templ = wl.WellLogVersion.Template;
                            if (templ.TemplateType.Equals(TemplateType.ResistivityDeep) || templ.TemplateType.Equals(TemplateType.VitriniteReflectance) ||
                                templ.TemplateType.Equals(TemplateType.Density) || templ.TemplateType.Equals(TemplateType.Ssonic) || templ.TemplateType.Equals(TemplateType.Porosity))
                            {
                                IImageInfoFactory f = CoreSystem.GetService<IImageInfoFactory>(wl);
                                var cmbitem = new ComboBoxItem();
                                cmbitem.Text = string.Format("{0} ({1})", wl.Name, templ.TemplateType);
                                cmbitem.Image = f.GetImageInfo(wl).TypeImage;
                                cmbitem.Value = wl;

                                if (templ.TemplateType.Equals(TemplateType.ResistivityDeep))
                                    cmbResistivity.Items.Add(cmbitem);
                                else if (templ.TemplateType.Equals(TemplateType.VitriniteReflectance))
                                    cmbVitrinite.Items.Add(cmbitem);
                                else if (templ.TemplateType.Equals(TemplateType.Density))
                                    cmbDensity.Items.Add((cmbitem));
                                else if (templ.TemplateType.Equals(TemplateType.Ssonic))
                                    cmbSonic.Items.Add(cmbitem);
                                else
                                    cmbPorosity.Items.Add(cmbitem);
                            }
                        }
                        var index = bholes.IndexOf(cur.Value as Borehole);
                        warning_check(index, res, ref WARNINRES);
                        warning_check(index, vitr, ref WARNINGVITR);
                        warning_check(index, den, ref WARNINGDEN);
                        warning_check(index, son, ref WARNINGSON);
                        warning_check(index, por, ref WARNINGPOR);
                       

                        if (cmbResistivity.Items.Count > 1)
                        {
                            cmbResistivity.Text = "Choose wellog";
                            cmbResistivity.Enabled = true;
                        }
                        if (cmbVitrinite.Items.Count > 1)
                            cmbVitrinite.Text = "Choose wellog";

                        if (cmbSonic.Items.Count > 1)
                            cmbSonic.Text = "Choose wellog";

                        if (cmbDensity.Items.Count > 1)
                            cmbDensity.Text = "Choose wellog";

                        if (cmbPorosity.Items.Count > 1)
                            cmbPorosity.Text = "Choose wellog";

                    }
                }
            }
        }

        private void cmbResistivity_SelectedValueChanged(object sender, EventArgs e)
        {
            var combo = sender as Slb.Ocean.Petrel.UI.Controls.ComboBox;
            combo.Enabled = false;
            var sel_items = list_Boreholes.SelectedItems;
            foreach (var cur in sel_items)
            {
                if (cur.Selected)
                {
                    int index = bholes.IndexOf(cur.Value as Borehole);
                   
                    if (res[index] != null)
                    {
                        String resist = res[index] == null ? "0" : res[index].Count.ToString();
                        String vitrin = vitr[index] == null ? "0" : vitr[index].Count.ToString();
                        String dens = den[index] == null ? "0" : den[index].Count.ToString();
                        String por_ = por[index] == null ? "0" : por[index].Count.ToString();
                        String sonn = son[index] == null ? "0" : son[index].Count.ToString();
                        Borehole bhole = list_Boreholes.Items[index].Value as Borehole;
                        res[index] = new MyData(1,combo.SelectedValue as WellLog);
                        list_Boreholes.Items[index].Text = string.Format("{0} (LLD: {1}; Vitrinite: {2},DENS:{3},POR:{4},SONIC:{5})", bhole.Name, resist, vitrin, dens, por_, sonn); 
                        /*if (vitr[index] != null && vitr[index].Count==1)
                            list_Boreholes.Items[index].Image = PetrelImages.Apply;
*/                    }
                }
            }
                         
        }

        //checkbox Sonic


        //combobox Sonic
        private void cmbSonic_SelectedValueChanged(object sender, EventArgs e)
        {
            if (OneSelectedItem())
            {
                var sel_items = list_Boreholes.SelectedItems;
                foreach (var cur in sel_items)
                {
                    if (cur.Selected)
                    {
                        int index = bholes.IndexOf(cur.Value as Borehole);
                        son[index] = new MyData(1,cmbSonic.SelectedValue as WellLog);
                        cmbSonic.Enabled = false;
                    }
                }     
            }
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

        private void chbSonic_CheckStateChanged(object sender, EventArgs e)
        {
            chbSonic.CheckState = CheckState.Indeterminate;
            if (chbSonic.Checked && OneSelectedItem())
            {
                var sel_items = list_Boreholes.SelectedItems;
                foreach (var cur in sel_items)
                {
                    int index = bholes.IndexOf(cur.Value as Borehole);
                    if (cur.Selected && son[index].Count > 1)
                        cmbSonic.Enabled = true;
                }
            }
            else
                cmbSonic.Enabled = false;
        }

    }
}
