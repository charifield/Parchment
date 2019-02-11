Module module_formControl


#Region "Declerations"
    Dim CheckMaximized As Boolean = False
    Dim dragform As Boolean = False
    Dim mousex, mousey As Integer
#End Region


    'MOVE/DRAG APP
#Region "Drag Forms"
    Public Function MouseDownDrag(ByVal neededForm As Form)
        On Error Resume Next

        If CheckMaximized = False Then
            dragform = True 'Sets the variable drag to true.
            mousex = Windows.Forms.Cursor.Position.X - neededForm.Left 'Sets variable mousex
            mousey = Windows.Forms.Cursor.Position.Y - neededForm.Top 'Sets variable mousey
            neededForm.Cursor = Cursors.SizeAll
        End If

        Return True
    End Function

    Public Function MouseMoveDrag(ByVal neededForm As Form)
        On Error Resume Next

        If dragform Then
            neededForm.Top = Windows.Forms.Cursor.Position.Y - mousey : neededForm.Left = Windows.Forms.Cursor.Position.X - mousex
            neededForm.Opacity = 0.85 : neededForm.Refresh()
        End If

        Return True
    End Function

    Public Function MouseUpDrag(ByVal neededForm As Form)
        dragform = False
        Dim neededFormControl As Form = neededForm
        neededForm.Cursor = Cursors.Default
        neededForm.Opacity = 1 : neededForm.Refresh()
        Return True
    End Function
#End Region

    'RESIZE APPS
#Region "Form Resize"
    'Public Function MouseDownResize(ByVal neededForm)
    '    On Error Resume Next

    '    If CheckMaximized(neededForm) = False And dragform = False Then
    '        dragform = True : OriginalSize.Height = neededForm.Height : OriginalSize.Width = neededForm.Width 'Sets the variable drag to true.
    '        mousex = Windows.Forms.Cursor.Position.X 'Sets variable mousex
    '        mousey = Windows.Forms.Cursor.Position.Y 'Sets variable mousey
    '        neededForm.Opacity = 0.8 : neededForm.Refresh()
    '    End If
    '    Return True
    'End Function

    'Public Function MouseMoveResize(ByVal neededForm)
    '    On Error Resume Next

    '    If dragform And CheckMaximized(neededForm) = False Then neededForm.SetBounds(neededForm.Location.X, neededForm.Location.Y, OriginalSize.Width + (Windows.Forms.Cursor.Position.X - mousex), OriginalSize.Height + (Windows.Forms.Cursor.Position.Y - mousey))
    '    Return True
    'End Function

    'Public Function MouseUpResize(ByVal neededForm)
    '    On Error Resume Next

    '    neededForm.Opacity = 1 : neededForm.Refresh() : dragform = False
    '    Return True
    'End Function
#End Region



End Module
