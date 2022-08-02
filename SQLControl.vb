Imports System.Data.SqlClient
Public Class SQLControl
    Private DBCon As New SqlConnection("Server=10.20.0.98\IR_INTEGRITI;Database=IntegReview__2022_Q3_0;User=readonlyuser;Pwd=readonlyuserpw;")
    Private DBCmd As SqlCommand
    ' DB DATA
    Public DBDA As SqlDataAdapter
    Public DBDT As DataTable

    ' QUERY PARAMETERS
    Public Params As New List(Of SqlParameter)

    ' QUERY STATS
    Public RecordCount As Integer
    Public Exception As String

    Public Sub New()

    End Sub

    ' ALLOW CONNECTION STRING OVERRIDE

    Public Sub New(ConnectionString As String)
        DBCon = New SqlConnection(ConnectionString)
    End Sub

    ' QUERY EXECUTION SUB
    Public Sub ExecQuery(Query As String)
        ' Reset Query Stats
        RecordCount = 0
        Exception = ""

        Try
            DBCon.Open()
            ' query

            'CREATE DB COMMAND
            DBCmd = New SqlCommand(Query, DBCon)

            ' LOAD PARAMS INTO THE DB COMMAND
            Params.ForEach(Sub(p) DBCmd.Parameters.Add(p))

            ' CLEAR PARAM LIST
            Params.Clear()

            ' EXECUTE COMMAND & FILL DATASET
            DBDT = New DataTable
            DBDA = New SqlDataAdapter(DBCmd)
            RecordCount = DBDA.Fill(DBDT)


        Catch ex As Exception
            ' CAPTURE ERROR
            Exception = "ExecQuery Error: " & System.Environment.NewLine & ex.Message

        Finally
            ' CLOSE CONNECTION
            If DBCon.State = ConnectionState.Open Then DBCon.Close()
        End Try
    End Sub

    ' ADD PARAMS
    Public Sub AddParam(Name As String, Value As Object)
        Dim NewParam As New SqlParameter(Name, Value)
        Params.Add(NewParam)

    End Sub

    ' ERROR CHECKING
    Public Function HasException(Optional Report As Boolean = False) As Boolean
        If String.IsNullOrEmpty(Exception) Then Return False
        If Report = True Then MsgBox(Exception, MsgBoxStyle.Critical, "Exception:")
        Return True
    End Function

End Class
