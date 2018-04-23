using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Filtering;

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
                for (int i = e.Menu.Items.Count - 1; i >= 0; i--) {
                    if (e.Menu.Items[i].Caption == Localizer.Active.GetLocalizedString(StringId.FilterGroupNotAnd) ||
                        e.Menu.Items[i].Caption == Localizer.Active.GetLocalizedString(StringId.FilterGroupNotOr)) {
                        e.Menu.Items.RemoveAt(i);
                    }
                }
            }
        }
    }
}