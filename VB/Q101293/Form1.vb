Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Filtering
Imports DevExpress.Data.Filtering.Helpers
Imports DevExpress.Utils.Menu

Namespace Q101293
    Partial Public Class Form1
        Inherits Form
        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            Dim tbl As New DataTable()
            tbl.Columns.Add("ID", GetType(Integer))
            tbl.Columns.Add("Name", GetType(String))
            tbl.Columns.Add("Check", GetType(Boolean))
            For i As Integer = 1 To 9
                tbl.Rows.Add(i, String.Format("Item {0}", i), i Mod 2 = 0)
            Next i
            gridControl1.DataSource = tbl
            gridView1.ActiveFilterString = "[Check] = false"
        End Sub

        Private Sub gridView1_FilterEditorCreated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FilterControlEventArgs) Handles gridView1.FilterEditorCreated
            AddHandler e.FilterControl.PopupMenuShowing, AddressOf OnFilterControlPopupMenuShowing
        End Sub

        Private Sub OnFilterControlPopupMenuShowing(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs)
            If e.MenuType = FilterControlMenuType.Group Then
                ' Hide commands
                e.Menu.Remove(GroupType.NotAnd)
                e.Menu.Remove(GroupType.NotOr)
                'Locate and then disable and rename commands.
                e.Menu.Find(StringId.FilterMenuGroupAdd).Enabled = False
                e.Menu.Find(StringId.FilterMenuClearAll).Caption = "Remove All"
            End If
            ' Hide all operators except Equals and DoesNotEqual for the "ID" field
            If e.MenuType = FilterControlMenuType.Clause Then
                Dim node As ClauseNode = TryCast(e.CurrentNode, ClauseNode)
                If node.Property.Name = "ID" Then
                    Dim itemEqual As DXMenuItem = e.Menu.Find(ClauseType.Equals)
                    Dim itemNotEqual As DXMenuItem = e.Menu.Find(ClauseType.DoesNotEqual)
                    For i As Integer = e.Menu.Items.Count - 1 To 0 Step -1
                        Dim item As DXMenuItem = e.Menu.Items(i)
                        If (Not item.Equals(itemEqual)) AndAlso (Not item.Equals(itemNotEqual)) Then
                            item.Visible = False
                        End If
                    Next i
                End If
            End If
        End Sub
    End Class
End Namespace