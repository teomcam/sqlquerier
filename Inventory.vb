Public Class Inventory
    Public SQL As New SQLControl()
    Public Sub LoadGrid(Optional Query As String = "")
        If Query = "" Then
            SQL.ExecQuery("SELECT TOP 100 * FROM VReview WHERE (Text LIKE '%Help%' or Text LIKE '%Access%') Order By [UTCTimeInserted] Desc;")
        Else
            SQL.ExecQuery(Query)
        End If
        ' ERROR HANDLING
        If SQL.HasException(True) Then Exit Sub

        dgvData.DataSource = SQL.DBDT
    End Sub

    Private Sub Inventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = Form1
        LoadGrid()
    End Sub

End Class