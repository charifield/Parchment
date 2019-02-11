Option Explicit On
Imports Transitions

Public Class frmMain


    'DECLERATIONS AND TITLEBAR
#Region "Alt-Tab Hider and Drop Shadow"
    Public CS_DROPSHADOW As Int32 = &H20000

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            If Not Me.DesignMode Then cp.ExStyle = cp.ExStyle Or &H80

            Dim parameters As CreateParams = MyBase.CreateParams
            parameters.ClassStyle += CS_DROPSHADOW
            Return parameters

            Return cp
        End Get
    End Property
#End Region

#Region "TitleBar Mouse"

    Private Sub pnlTop_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlTop.MouseDown
        MouseDownDrag(Me)
    End Sub

    Private Sub pnlTop_MouseMove(sender As Object, e As MouseEventArgs) Handles pnlTop.MouseMove
        MouseMoveDrag(Me)
    End Sub

    Private Sub pnlTop_MouseUp(sender As Object, e As MouseEventArgs) Handles pnlTop.MouseUp
        MouseUpDrag(Me)
    End Sub
#End Region

#Region "Titlebar Buttons"
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If RichTextBox1.TextLength <> DocLength Then
            frmPrompt.Messagebox("Do you want to save changes to " & fileName)
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnMax_Click(sender As Object, e As EventArgs) Handles btnMax.Click
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        Else
            Me.WindowState = FormWindowState.Maximized
        End If
    End Sub

    Private Sub btnMin_Click(sender As Object, e As EventArgs) Handles btnMin.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
#End Region

#Region "Form Resize"
    Private Sub main_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize

        pnlMenu_home.Left = 0 : pnlMenu_home.Width = Me.Width : pnlMenu_home.Top = pnlTop.Height
        pnlMenu_insert.Size = pnlMenu_home.Size : pnlMenu_insert.Location = pnlMenu_home.Location
        pnlMenu_document.Size = pnlMenu_home.Size : pnlMenu_document.Location = pnlMenu_home.Location

        pnlLoading.Top = 0 : pnlLoading.BringToFront()

        pnlRichTextBox.SetBounds(50, pnlTop.Height, Me.Width - 100, Me.Height)
        'pnlRichTextBox.SetBounds(50, pnlTop.Height + pnlMenu_home.Height + 50, Me.Width - 100, Me.Height)
        RichTextBox1.Location = New Point(50, pnlMenu_home.Height + 15)
        RichTextBox1.Size = New Size(pnlRichTextBox.Width - 100, Me.Height - (RichTextBox1.Top + pnlMenu_home.Height))


        frmBlur.Size = Me.Size : frmBlur.Location = Me.Location
        frmPrompt.CenterMe()
    End Sub
#End Region

#Region "Form Drag"
    Private Sub main_LocationChanged(sender As Object, e As EventArgs) Handles MyBase.LocationChanged
        frmBlur.Size = Me.Size : frmBlur.Location = Me.Location
        frmPrompt.CenterMe()
    End Sub
#End Region



    'FORM LOAD AND ANIMATION
#Region "Declerations"
    Public URL As String = ""
    Public DocLength As Integer = 0
    Public fileName As String = "untitled.rtf"
#End Region

#Region "Form Load"
    Private Sub main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Main.Main()

        runAnimations()
        DocLength = RichTextBox1.TextLength
        pnlLoading.Width = 0

        If URL <> "" Then OpenLocalFile(URL)
    End Sub
#End Region

#Region "Animations"
    Sub runAnimations()
        'Transition.run(pnlTop, "Height", 0, pnlTop.Height, New TransitionType_EaseInEaseOut(500))
        Transition.run(pnlMenu_home, "Height", 0, pnlMenu_home.Height, New TransitionType_EaseInEaseOut(700))
        Transition.run(Me, "Opacity", 0.0, 1.0, New TransitionType_EaseInEaseOut(400))

        Transition.run(pnlLoading, "Width", 0, Me.Width, New TransitionType_EaseInEaseOut(1500))
    End Sub

    Private Sub pnlLoading_SizeChanged(sender As Object, e As EventArgs) Handles pnlLoading.SizeChanged
        If pnlLoading.Width >= Me.Width Then pnlLoading.Width = 0
    End Sub
#End Region



    'BUTTON PRESSES
#Region "Category Change"
    Private Sub ChangeCategory(sender As Object, e As EventArgs)

        pnlMenu_home.Hide() : pnlMenu_insert.Hide() : pnlMenu_document.Hide()

        Dim selectedCategory As gLabel.gLabel = sender
        Dim selectedPanel As Panel = CType(pnlTop.Controls("pnlMenu_" & selectedCategory.Text), Panel)
        Dim neededPanel As Control = Me.Controls.Find("pnlMenu_" & selectedCategory.Text, True)(0)

        If neededPanel.Height = pnlMenu_home.Height Then Transition.run(neededPanel, "Height", 0, neededPanel.Height, New TransitionType_EaseInEaseOut(200))

        neededPanel.Show() : neededPanel.BringToFront()
        pbxSelection.Left = selectedCategory.Left + (selectedCategory.Width / 3)
    End Sub

#End Region



#Region "Document Options"
    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpenFile.Click
        On Error Resume Next
        module_fileOperations.LoadFile()
    End Sub

    Private Sub btnNewFile_Click(sender As Object, e As EventArgs) Handles btnNewFile.Click
        On Error Resume Next
        System.Diagnostics.Process.Start(My.Application.Info.DirectoryPath & "\Parchment.exe")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If pnlLoading.Width = 0 Then module_fileOperations.animateLoading()
    End Sub

#End Region

    

   
End Class
