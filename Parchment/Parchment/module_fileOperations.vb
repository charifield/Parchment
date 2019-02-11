Imports Transitions

Module module_fileOperations

#Region "Animations"
    Sub animateLoading()
        Transition.run(frmMain.pnlLoading, "Width", 0, frmMain.Width, New TransitionType_EaseInEaseOut(1000))
    End Sub
#End Region


#Region "Open File"
    Sub LoadFile()

        If frmMain.RichTextBox1.TextLength <> frmMain.DocLength Then
            frmPrompt.Messagebox("Do you want to save changes to " & frmMain.fileName)
            frmPrompt.ShowDialog(frmBlur)
            If frmPrompt.Response = "Discard" Then GoTo Proceed
            If frmPrompt.Response = "Cancel" Then Exit Sub
        End If



Proceed:
        Dim OpenFile As New OpenFileDialog

        OpenFile.ShowDialog()
        If OpenFile.FileName <> "" Then
            OpenLocalFile(OpenFile.FileName.ToLower)
        End If
    End Sub


    Function OpenLocalFile(ByVal fileName As String)
        If IO.File.Exists(fileName) Then

            frmMain.RichTextBox1.Clear()

            If fileName.EndsWith("rtf") Then
                frmMain.RichTextBox1.LoadFile(fileName)
            Else
                frmMain.RichTextBox1.Text = IO.File.ReadAllText(fileName)
            End If

            frmMain.fileName = IO.Path.GetFileName(fileName)
            frmMain.URL = fileName

            frmMain.DocLength = frmMain.RichTextBox1.TextLength
            frmMain.Text = frmMain.fileName & " - Parchment"
            animateLoading()
        End If

        Return True
    End Function

#End Region

End Module
