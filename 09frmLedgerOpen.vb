Public Class frmLedgerOpen
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' continnual update time
        LabelTime.Text = DateAndTime.TimeOfDay
    End Sub

    Private Sub frmLedgerOpen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Process1 As New Process
        Process1.StartInfo.FileName = "Explorer.exe"
        Process1.StartInfo.Arguments = """mbp_general_ledger.xlsm"""
        ' begin processs
        Process1.Start()
    End Sub

    Private Sub ButtonBack_Click(sender As Object, e As EventArgs) Handles ButtonBack.Click
        Dim accountingMenu As New frmAccounting
        accountingMenu.Show()
        Close()
    End Sub
End Class