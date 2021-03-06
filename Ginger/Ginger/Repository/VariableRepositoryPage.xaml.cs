#region License
/*
Copyright © 2014-2018 European Support Limited

Licensed under the Apache License, Version 2.0 (the "License")
you may not use this file except in compliance with the License.
You may obtain a copy of the License at 

http://www.apache.org/licenses/LICENSE-2.0 

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS, 
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
See the License for the specific language governing permissions and 
limitations under the License. 
*/
#endregion

using Amdocs.Ginger.Common;
using GingerWPF.DragDropLib;
using Ginger.UserControls;
using Ginger.Variables;
using GingerCore;
using GingerCore.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Ginger.Repository
{
    /// <summary>
    /// InterVariableBaseion logic for VariablesRepositoryPage.xaml
    /// </summary>    
    public partial class VariablesRepositoryPage : Page
    {
        public VariablesRepositoryPage(string Path)
        {
            InitializeComponent();

            SetVariablesGridView();

            App.LocalRepository.AttachHandlerToSolutionRepoVariablesChange(RefreshVariables);
        }

        private void SetVariablesGridView()
        {                        
            GridViewDef view = new GridViewDef(GridViewDef.DefaultViewName);
            ObservableList<GridColView> viewCols = new ObservableList<GridColView>();
            view.GridColsView = viewCols;
            viewCols.Add(new GridColView() { Field = VariableBase.Fields.Name, WidthWeight = 50, AllowSorting = true });
            viewCols.Add(new GridColView() { Field = VariableBase.Fields.Description, WidthWeight = 35, AllowSorting = true });
            view.GridColsView.Add(new GridColView() { Field = "Inst.", WidthWeight = 15, StyleType = GridColView.eGridColStyleType.Template, CellTemplate = (DataTemplate)this.pageGrid.Resources["ViewInstancesButton"] });           
            grdVariables.SetAllColumnsDefaultView(view);
            grdVariables.InitViewItems();

            grdVariables.btnRefresh.AddHandler(Button.ClickEvent, new RoutedEventHandler(RefreshVariables));
            grdVariables.AddToolbarTool("@LeftArrow_16x16.png", "Add to " + GingerDicser.GetTermResValue(eTermResKey.Variables), new RoutedEventHandler(AddFromRepository));
            grdVariables.AddToolbarTool("@Edit_16x16.png", "Edit Item", new RoutedEventHandler(EditVar));
            grdVariables.ShowTagsFilter = Visibility.Visible;
            
            grdVariables.RowDoubleClick += grdVariables_grdMain_MouseDoubleClick;
            grdVariables.ItemDropped += grdVariables_ItemDropped;
            grdVariables.PreviewDragItem += grdVariables_PreviewDragItem;

            grdVariables.DataSourceList = App.LocalRepository.GetSolutionRepoVariables();
        }


        private void RefreshVariables(object sender, RoutedEventArgs e)
        {
            App.LocalRepository.RefreshSolutionRepoVariables();
            grdVariables.DataSourceList = App.LocalRepository.GetSolutionRepoVariables();
        }

        private void RefreshVariables(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            grdVariables.DataSourceList = App.LocalRepository.GetSolutionRepoVariables();
        }

        private void AddFromRepository(object sender, RoutedEventArgs e)
        {
            if (grdVariables.Grid.SelectedItems != null && grdVariables.Grid.SelectedItems.Count > 0)
            {
                foreach (VariableBase selectedItem in grdVariables.Grid.SelectedItems)
                    App.BusinessFlow.AddVariable((VariableBase)selectedItem.CreateInstance(true));
                
                int selectedActIndex = -1;
                ObservableList<VariableBase> varList = App.BusinessFlow.Variables;
                if (varList.CurrentItem != null)
                {
                    selectedActIndex = varList.IndexOf((VariableBase)varList.CurrentItem);
                }
                if (selectedActIndex >= 0)
                {
                    varList.Move(varList.Count - 1, selectedActIndex + 1);
                }
            }
            else
                Reporter.ToUser(eUserMsgKeys.NoItemWasSelected);
        }

        private void EditVar(object sender, RoutedEventArgs e)
        {
            if (grdVariables.CurrentItem != null && grdVariables.CurrentItem.ToString() != "{NewItemPlaceholder}")
            {
                VariableBase selectedVarb = (VariableBase)grdVariables.CurrentItem;
                selectedVarb.NameBeforeEdit = selectedVarb.Name;

                VariableEditPage w = new VariableEditPage(selectedVarb, false, VariableEditPage.eEditMode.SharedRepository);
                w.ShowAsWindow(eWindowShowStyle.Dialog);

            }
            else
            {
                Reporter.ToUser(eUserMsgKeys.AskToSelectVariable);
            }
        }

        private void ViewRepositoryItemUsage(object sender, RoutedEventArgs e)
        {
            if (grdVariables.Grid.SelectedItem != null)
            {
                RepositoryItemUsagePage usagePage = new RepositoryItemUsagePage((RepositoryItem)grdVariables.Grid.SelectedItem);
                usagePage.ShowAsWindow();
            }
            else
                Reporter.ToUser(eUserMsgKeys.NoItemWasSelected);
        }

        private void grdVariables_PreviewDragItem(object sender, EventArgs e)
        {
            if (DragDrop2.DragInfo.DataIsAssignableToType(typeof(VariableBase)))
            {
                // OK to drop                         
                DragDrop2.DragInfo.DragIcon = GingerWPF.DragDropLib.DragInfo.eDragIcon.Copy;
            }
        }

        private void grdVariables_ItemDropped(object sender, EventArgs e)
        {
            VariableBase dragedItem = (VariableBase)((DragInfo)sender).Data;
            if (dragedItem != null)
            {
                //App.LocalRepository.AddItemToRepositoryWithPreChecks(dragedItem, null);

                SharedRepositoryOperations.AddItemToRepository(dragedItem);

                //refresh and select the item
                try
                {
                    VariableBase dragedItemInGrid = ((IEnumerable<VariableBase>)grdVariables.DataSourceList).Where(x => x.Guid == dragedItem.Guid).FirstOrDefault();
                    if (dragedItemInGrid != null)
                        grdVariables.Grid.SelectedItem = dragedItemInGrid;
                }
                catch (Exception ex) { Reporter.ToLog(eLogLevel.ERROR, $"Method - {MethodBase.GetCurrentMethod().Name}, Error - {ex.Message}"); }
            }
        }

        private void grdVariables_grdMain_MouseDoubleClick(object sender, EventArgs e)
        {
            EditVar(sender, new RoutedEventArgs());
        }
    }
}
