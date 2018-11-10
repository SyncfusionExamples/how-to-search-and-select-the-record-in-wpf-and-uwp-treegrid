using Syncfusion.Data;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Parsing;
using Syncfusion.UI.Xaml.Grid;

using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.UI.Xaml.ScrollAxis;
using Syncfusion.UI.Xaml.TreeGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Key = Windows.System.VirtualKey;
using Syncfusion.UI.Xaml.TreeGrid;
using Syncfusion.UI.Xaml.TreeGrid.Helpers;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SfTreeGridDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.textbox.TextChanged += Textbox_TextChanged;
        }

        IPropertyAccessProvider Provider;
        PersonInfo dataRow = new PersonInfo();
        private void Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            
            if (textBox.Text == "")
                treeGrid.SelectedItems.Clear();

            for (int i = 0; i < treeGrid.View.Nodes.Count; i++)
            {
                if (Provider == null)
                    Provider = treeGrid.View.GetPropertyAccessProvider();

                if (treeGrid.View.Nodes[i].HasChildNodes && treeGrid.View.Nodes[i].ChildNodes.Count == 0)
                {
                    treeGrid.ExpandNode(treeGrid.View.Nodes[i]);
                    treeGrid.CollapseNode(treeGrid.View.Nodes[i]);
                }
                else if (treeGrid.View.Nodes[i].HasChildNodes)
                {
                    dataRow = (treeGrid.View.Nodes[i].Item as PersonInfo);
                    FindMatchText(dataRow);
                    GetChildNodes(treeGrid.View.Nodes[i]);
                }
                else
                {
                    dataRow = (treeGrid.View.Nodes[i].Item as PersonInfo);
                    FindMatchText(dataRow);
                }
            }
        }
        /// <summary>
        /// Find the match text
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private bool FindMatchText(PersonInfo dataRow)
        {
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
                dataRow = (childNode.Item as PersonInfo);
                bool foundMatchText = FindMatchText(dataRow);
                if (foundMatchText)
                   treeGrid.ExpandNode(node);
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
            if (string.IsNullOrEmpty(textbox.Text))
                return false;

            var cellvalue = Provider.GetFormattedValue(record, treeGridColumn.MappingName);

            return cellvalue != null && cellvalue.ToString().ToLower().Equals(textbox.Text.ToString().ToLower());
        }
    }
}
