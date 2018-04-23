Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Filtering

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
				For i As Integer = e.Menu.Items.Count - 1 To 0 Step -1
					If e.Menu.Items(i).Caption = Localizer.Active.GetLocalizedString(StringId.FilterGroupNotAnd) OrElse e.Menu.Items(i).Caption = Localizer.Active.GetLocalizedString(StringId.FilterGroupNotOr) Then
						e.Menu.Items.RemoveAt(i)
					End If
				Next i
			End If
		End Sub
	End Class
End Namespace