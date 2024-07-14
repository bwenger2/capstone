Public Class frmLogin

    ' declarations
    Const TRIES_ALLOWED As Integer = 5
    Dim triesLeft As Integer = TRIES_ALLOWED
    Dim unlockNum As Integer = 0

    Private Sub EMPLOYEESBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles EMPLOYEESBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.EMPLOYEESBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.WBPDataSet)

    End Sub

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'WBPDataSet.EMPLOYEES' table. You can move, or remove it, as needed.
        Me.EMPLOYEESTableAdapter.Fill(Me.WBPDataSet.EMPLOYEES)
        ' set tries left
        triesLeft = TRIES_ALLOWED
        ' update tries remaining label
        LabelTriesLeft.Text = triesLeft.ToString
        ' focus cursor
        TextBoxUserName.Select()
        TextBoxUserName.Focus()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' continnual update time
        LabelTime.Text = DateAndTime.TimeOfDay
    End Sub

    Private Sub ButtonForgotPassword_Click(sender As Object, e As EventArgs) Handles ButtonForgotPassword.Click
        ' friendly message box with admin phone number
        MessageBox.Show("Please call the system administrator:" +
                        Environment.NewLine +
                        "614-555-5559" +
                        Environment.NewLine + Environment.NewLine +
                        "Or Email:" +
                        Environment.NewLine +
                        "sys.admin@wbp.com",
                        "Forgot Password?")
        ' refocus cursor
        ButtonClear.PerformClick()
    End Sub

    Private Sub ButtonClear_Click(sender As Object, e As EventArgs) Handles ButtonClear.Click
        ' reset text boxes
        TextBoxPassword.Clear()
        With TextBoxUserName
            .Clear()
            .Focus()
        End With
    End Sub

    Private Sub ButtonLogIn_Click(sender As Object, e As EventArgs) Handles ButtonLogIn.Click
        ' check for required fields
        If TextBoxUserName.Text.Equals("") Then
            MessageBox.Show("Please enter a user name.", "Required Field")
            ' re-focus on user name text box
            With TextBoxUserName
                .Focus()
            End With
        ElseIf TextBoxPassword.Text.Equals("") Then
            MessageBox.Show("Please enter a password.", "Required Field")
            ' re-focus on password text box
            TextBoxPassword.Focus()
        Else
            ' if both text boxes have content,
            ' attempt login
            AttemptLogin()
            ' update try count
            triesLeft -= 1
            ' update tries remaining label
            LabelTriesLeft.Text = triesLeft.ToString
            ' if try count goes to zero, lock program
            If triesLeft = 0 Then
                TextBoxLockout.Visible = True
                ' blue blocks
                ButtonUnlock1.Enabled = True
                ButtonUnlock2.Enabled = True
                ButtonUnlock3.Enabled = True
                ButtonUnlock4.Enabled = True
                ButtonUnlock1.Visible = True
                ButtonUnlock2.Visible = True
                ButtonUnlock3.Visible = True
                ButtonUnlock4.Visible = True
                TextBoxUserName.Enabled = False
                TextBoxPassword.Enabled = False
                ButtonLogIn.Enabled = False
                ButtonClear.Enabled = False
            End If
        End If
    End Sub

    ' attempt login function
    Private Sub AttemptLogin()
        ' navigate employee table to first row
        EMPLOYEESBindingNavigator.MoveFirstItem.PerformClick()
        ' declarations
        Dim navPosition As Double = EMPLOYEESBindingNavigator.PositionItem.Text
        Dim navText As String = EMPLOYEESBindingNavigator.CountItem.Text
        Dim navCountString As String = ""
        'Dim userTypeString As String = ""
        Dim goBool As Boolean = False
        Dim continueBool As Boolean = True
        Dim count As Double = 0
        ' get the number of fields
        For Each c As Char In navText
            If IsNumeric(c) Then
                navCountString = navCountString & c
            End If
        Next
        ' parse nav count
        Dim navCountInt As Integer = Integer.Parse(navCountString)
        ' test message
        'MessageBox.Show(navCountInt, "Code Test")
        ' loop to search for matches
        While continueBool
            ' reset variables
            navPosition = EMPLOYEESBindingNavigator.PositionItem.Text
            If TextBoxUserName.Text.Equals(UserNameTextBox.Text) Then
                If TextBoxPassword.Text.Equals(PasswordTextBox.Text) Then
                    goBool = True
                    Exit While
                End If
            End If
            ' accumulate count
            count += 1
            ' test message
            'MessageBox.Show(count, "Code Test")
            ' test for end of table
            If navPosition.Equals(navCountInt) Then
                Exit While
            End If
            ' next row in table
            EMPLOYEESBindingNavigator.MoveNextItem.PerformClick()
        End While
        ' continue or retry
        If goBool = True Then
            If AdminCheckBox.Checked Then
                ' navigate to admin menu
                Dim newAdmin As New frmAdmin
                newAdmin.Show()
                Close()
            Else
                ' navigate to normal menu
                Dim newUser As New frmUser
                newUser.Show()
                Close()
            End If
        Else
            ' show error
            MessageBox.Show("Credentials not found." +
                            Environment.NewLine +
                            "Please try again.", "Error")
            ' reset text boxes
            ButtonClear.PerformClick()
        End If
    End Sub

    Private Sub ButtonUnlock1_Click(sender As Object, e As EventArgs) Handles ButtonUnlock1.Click
        unlockNum = 1
    End Sub

    Private Sub ButtonUnlock2_Click(sender As Object, e As EventArgs) Handles ButtonUnlock2.Click
        If unlockNum = 1 Then
            unlockNum = 2
        End If
    End Sub

    Private Sub ButtonUnlock3_Click(sender As Object, e As EventArgs) Handles ButtonUnlock3.Click
        If unlockNum = 2 Then
            unlockNum = 3
        End If
    End Sub

    Private Sub ButtonUnlock4_Click(sender As Object, e As EventArgs) Handles ButtonUnlock4.Click
        If unlockNum = 3 Then
            ' display code input box
            Dim unlockStr As String = InputBox("RESET LOCK", "BACK DOOR", "ENTER UNLOCK CODE")
            If unlockStr = "applesauce" Then
                MessageBox.Show("CODE ACCEPTED", "SUCCESS")
                ' unlock controls
                TextBoxLockout.Visible = False
                ' blue blocks
                ButtonUnlock1.Enabled = False
                ButtonUnlock2.Enabled = False
                ButtonUnlock3.Enabled = False
                ButtonUnlock4.Enabled = False
                ButtonUnlock1.Visible = False
                ButtonUnlock2.Visible = False
                ButtonUnlock3.Visible = False
                ButtonUnlock4.Visible = False
                TextBoxUserName.Enabled = True
                TextBoxPassword.Enabled = True
                ButtonLogIn.Enabled = True
                ButtonClear.Enabled = True
                frmLogin_Load(sender, e)
            Else
                MessageBox.Show("CODE NOT ACCEPTED", "ERROR")
                unlockNum = 0
            End If
        End If
    End Sub
End Class
