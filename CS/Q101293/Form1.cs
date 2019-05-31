using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Utils.Menu;

namespace Q101293 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("ID", typeof(int));
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("Check", typeof(bool));
            for(int i = 1; i < 10; i++)
                tbl.Rows.Add(i, string.Format("Item {0}", i), i % 2 == 0);
            gridControl1.DataSource = tbl;
            gridView1.ActiveFilterString = "[Check] = false";
        }

        private void gridView1_FilterEditorCreated(object sender, DevExpress.XtraGrid.Views.Base.FilterControlEventArgs e) {
            e.FilterControl.PopupMenuShowing += OnFilterControlPopupMenuShowing;
        }

        void OnFilterControlPopupMenuShowing(object sender, PopupMenuShowingEventArgs e) {
            if (e.MenuType == FilterControlMenuType.Group) {
                // Hide commands
                e.Menu.Remove(GroupType.NotAnd);
                e.Menu.Remove(GroupType.NotOr);
                //Locate and then disable and rename commands.
                e.Menu.Find(StringId.FilterMenuGroupAdd).Enabled = false;
                e.Menu.Find(StringId.FilterMenuClearAll).Caption = "Remove All";
            }
            // Hide all operators except Equals and DoesNotEqual for the "ID" field
            if (e.MenuType == FilterControlMenuType.Clause) {
                ClauseNode node = e.CurrentNode as ClauseNode;
                if (node.Property.Name == "ID") {
                    DXMenuItem itemEqual = e.Menu.Find(ClauseType.Equals);
                    DXMenuItem itemNotEqual = e.Menu.Find(ClauseType.DoesNotEqual);
                    for (int i = e.Menu.Items.Count - 1; i >= 0; i--) {
                        DXMenuItem item = e.Menu.Items[i];
                        if (!item.Equals(itemEqual) && !item.Equals(itemNotEqual))
                            item.Visible = false;
                    }
                }
            }
        }
    }
}