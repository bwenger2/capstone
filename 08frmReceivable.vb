Public Class frmReceivable

    ' declarations
    Dim reportString As String = ""
    Dim reportTotal As Double = 0.00

    Private Sub frmReceivable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'WBPDataSet.SALES' table. You can move, or remove it, as needed.
        Me.SALESTableAdapter.Fill(Me.WBPDataSet.SALES)
        ' generate report
        GenerateSalesReport()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' update time
        LabelTime.Text = DateAndTime.TimeOfDay
    End Sub

    Private Sub SALESBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles SALESBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.SALESBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.WBPDataSet)

    End Sub

    Private Sub GenerateSalesReport()
        ' navigate inventory table to first row
        SALESBindingNavigator.MoveFirstItem.PerformClick()
        ' declarations
        Dim navPosition As Double = SALESBindingNavigator.PositionItem.Text
        Dim navText As String = SALESBindingNavigator.CountItem.Text
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

        ' loop to grab values
        For i As Integer = 1 To navCountInt
            RichTextBox1.Text += Environment.NewLine
            reportString = "Transaction ID: " + TransactionIDTextBox.Text + Environment.NewLine +
                "Transaction Date: " + TransactionDateTextBox.Text + Environment.NewLine +
                "Transaction Amount: " + Double.Parse(TransactionTotalTextBox.Text).ToString("C") + Environment.NewLine
            ' increment total
            reportTotal += Double.Parse(TransactionTotalTextBox.Text)
            ' send new report string to rich text box
            RichTextBox1.Text += reportString
            ' next row
            SALESBindingNavigator.MoveNextItem.PerformClick()
        Next
        ' output total
        RichTextBox1.Text += Environment.NewLine + "Total Revenue: " + reportTotal.ToString("C")
    End Sub

    Private Sub ButtonBack_Click(sender As Object, e As EventArgs) Handles ButtonBack.Click
        ' return to previous menu
        Dim previousMenu As New frmAccounting
        previousMenu.Show()
        Close()
    End Sub
End Class