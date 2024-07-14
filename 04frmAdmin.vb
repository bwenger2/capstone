Public Class frmAdmin
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' continnual update time
        LabelTime.Text = DateAndTime.TimeOfDay
    End Sub

    Private Sub ButtonLogOut_Click(sender As Object, e As EventArgs) Handles ButtonLogOut.Click
        Dim loginScreen As New frmLogin
        loginScreen.Show()
        Close()
    End Sub

    Private Sub ButtonInventory_Click(sender As Object, e As EventArgs) Handles ButtonInventory.Click
        ' navigate to inventory menu
        Dim invMenu As New frmInventory
        invMenu.Show()
        Close()
    End Sub

    Private Sub ButtonEmployees_Click(sender As Object, e As EventArgs) Handles ButtonEmployees.Click
        ' navigate to employee window
        Dim employeeWindow As New frmEmployees
        employeeWindow.Show()
        Close()
    End Sub

    Private Sub ButtonAccounting_Click(sender As Object, e As EventArgs) Handles ButtonAccounting.Click
        ' navigate to accounting menu
        Dim accountingMenu As New frmAccounting
        accountingMenu.Show()
        Close()
    End Sub

    Private Sub frmAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' get current month
        Dim monthInt As Int32 = DateAndTime.Now.Month
        If monthInt >= 3 And monthInt <= 5 Then
            Dim currentMonth As String = DateAndTime.MonthName(monthInt)
            TextBoxReminder.Visible = True
            TextBoxReminder.Text = "It's " + currentMonth + "," + Environment.NewLine +
                "and that's Springtime!" + Environment.NewLine +
                "So don't forget to check your inventory."
        End If
    End Sub
End Class