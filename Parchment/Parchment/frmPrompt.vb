Imports Transitions

Public Class frmPrompt

    Public Response As String = ""

    Public Function Messagebox(ByVal message As String) As String
        frmBlur.Show(frmMain)
        lblMessage.Text = message
        Response = ""
    End Function

    Sub CenterMe()
        Me.CenterToParent()
    End Sub


    Private Sub frmPrompt_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged

        If Me.Visible Then
            Transition.run(Me, "Opacity", 0.0, 1.0, New TransitionType_EaseInEaseOut(600))
            Me.CenterToParent()
        End If

    End Sub

    Private Sub SelectOption(sender As Object, e As EventArgs) Handles btnCancel.Click, btnDiscard.Click
        Dim TaskButton As Label = sender
        Response = TaskButton.Text
        Me.Hide() : frmBlur.Hide()
    End Sub
End Class