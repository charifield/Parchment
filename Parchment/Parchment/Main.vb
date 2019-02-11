Module Main

    Public Sub Main()

        Dim Parchment As New frmMain

        'Check if opening default file
        Try
            If (My.Application.CommandLineArgs.Count > 0) Then

                Dim file As String = My.Application.CommandLineArgs(0).ToLower
                frmMain.URL = file

            End If
        Catch

        End Try
    End Sub


End Module
