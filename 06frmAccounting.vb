Public Class frmAccounting
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' continnual update time
        LabelTime.Text = DateAndTime.TimeOfDay
    End Sub

    Private Sub ButtonBack_Click(sender As Object, e As EventArgs) Handles ButtonBack.Click
        ' return to admin menu
        Dim adminMenu As New frmAdmin
        adminMenu.Show()
        Close()
    End Sub

    Private Sub ButtonLedger_Click(sender As Object, e As EventArgs) Handles ButtonLedger.Click
        ' open general ledger in Excel
        Dim openExcel As New frmLedgerOpen
        openExcel.Show()
        Close()
    End Sub

    Private Sub ButtonReceivable_Click(sender As Object, e As EventArgs) Handles ButtonReceivable.Click
        ' open receivable report
        Dim newReport As New frmReceivable
        newReport.Show()
        Close()
    End Sub

    Private Sub ButtonPayable_Click(sender As Object, e As EventArgs) Handles ButtonPayable.Click
        ' open payable report
        Dim newReport As New frmPayable
        newReport.Show()
        Close()
    End Sub

    Private Sub ButtonSales_Click(sender As Object, e As EventArgs) Handles ButtonSales.Click
        ' open sales window
        Dim salesWindow As New frmSales
        salesWindow.Show()
        Close()
    End Sub

    Private Sub ButtonOrders_Click(sender As Object, e As EventArgs) Handles ButtonOrders.Click
        ' navigate to orders menu
        Dim ordersMenu As New frmOrders
        ordersMenu.Show()
        Close()
    End Sub
End Class