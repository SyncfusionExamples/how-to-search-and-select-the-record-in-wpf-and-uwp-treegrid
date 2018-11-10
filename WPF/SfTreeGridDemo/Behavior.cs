using Syncfusion.UI.Xaml.TreeGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Syncfusion.UI.Xaml.TreeGrid;
using System.Windows;
using Syncfusion.Data;

namespace SfTreeGridDemo
{
    class Behavior:Behavior<MainWindow>
    {
        protected internal IPropertyAccessProvider Provider = null;

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AssociatedObject.textBox.TextChanged += TextBox_TextChanged;
        }

        EmployeeInfo dataRow = new EmployeeInfo();
        /// <summary>
        /// Perform the search operaion for iterating the treegrid nodes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            var treeGrid = this.AssociatedObject.treeGrid;
            if (textBox.Text == "")
                treeGrid.SelectedItems.Clear();

            for (int i = 0; i < treeGrid.View.Nodes.Count; i++)
            {
                if (Provider == null)
                    Provider = treeGrid.View.GetPropertyAccessProvider();

                if (treeGrid.View.Nodes[i].HasChildNodes && treeGrid.View.Nodes[i].ChildNodes.Count == 0)
                {
                    treeGrid.BeginInit();
                    treeGrid.ExpandNode(treeGrid.View.Nodes[i]);
                    treeGrid.CollapseNode(treeGrid.View.Nodes[i]);
                    treeGrid.EndInit();
                }
                else if (treeGrid.View.Nodes[i].HasChildNodes)
                {                    
                    dataRow = (treeGrid.View.Nodes[i].Item as EmployeeInfo);
                    FindMatchText(dataRow);
                    GetChildNodes(treeGrid.View.Nodes[i]);
                }
                else
                {
                    dataRow=(treeGrid.View.Nodes[i].Item as EmployeeInfo);
                    FindMatchText(dataRow);
                }
            }
        }

        /// <summary>
        /// Find the match text
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private bool FindMatchText(EmployeeInfo dataRow)
        {
            var treeGrid = this.AssociatedObject.treeGrid;
            if (MatchSearchText(treeGrid.Columns[0], dataRow))
            {
                treeGrid.SelectedItems.Add(dataRow);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get the child nodes
        /// </summary>
        /// <param name="node"></param>
        private void GetChildNodes(TreeNode node)
        {
            foreach (var childNode in node.ChildNodes)
            {
                dataRow=(childNode.Item as EmployeeInfo);
                bool foundMatchText = FindMatchText(dataRow);
                if (foundMatchText)
                    AssociatedObject.treeGrid.ExpandNode(node);
                GetChildNodes(childNode);
            }
        }

        /// <summary>
        /// Return the cell value that matches with the serched text 
        /// </summary>
        /// <param name="treeGridColumn"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        private bool MatchSearchText(TreeGridColumn treeGridColumn, object record)
        {
            if (string.IsNullOrEmpty(this.AssociatedObject.textBox.Text))
                return false;

            var cellvalue = Provider.GetFormattedValue(record, treeGridColumn.MappingName);

            return cellvalue != null && cellvalue.ToString().ToLower().Equals(this.AssociatedObject.textBox.Text.ToString().ToLower());
        }
    }
}
