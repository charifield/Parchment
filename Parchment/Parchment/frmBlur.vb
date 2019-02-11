Imports Transitions

Public Class frmBlur

    Private Sub frmBlur_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        MouseMoveDrag(frmMain)
    End Sub

    Private Sub frmBlur_VisibleChanged(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        Me.Size = frmMain.Size : Me.Location = frmMain.Location
        If Me.Visible Then Transition.run(Me, "Opacity", 0.0, Me.Opacity, New TransitionType_EaseInEaseOut(400))
    End Sub

    Private Sub frmBlur_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        MouseDownDrag(frmMain)
    End Sub

    Private Sub frmBlur_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        MouseUpDrag(frmMain)
    End Sub
End Class