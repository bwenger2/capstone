Public Class frmPayable

    ' declarations
    Dim reportString As String = ""
    Dim reportTotal As Double = 0.00

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' update time
        LabelTime.Text = DateAndTime.TimeOfDay
    End Sub

    Private Sub ORDERSBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles ORDERSBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.ORDERSBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.WBPDataSet)

    End Sub

    Private Sub frmPayable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'WBPDataSet.ORDERS' table. You can move, or remove it, as needed.
        Me.ORDERSTableAdapter.Fill(Me.WBPDataSet.ORDERS)
        ' generate new report
        GenerateOrdersReport()
    End Sub

    Private Sub ButtonBack_Click(sender As Object, e As EventArgs) Handles ButtonBack.Click
        ' return to previous menu
        Dim previousMenu As New frmAccounting
        previousMenu.Show()
        Close()
    End Sub

    Private Sub GenerateOrdersReport()
        ' navigate inventory table to first row
        ORDERSBindingNavigator.MoveFirstItem.PerformClick()
        ' declarations
        Dim navPosition As Double = ORDERSBindingNavigator.PositionItem.Text
        Dim navText As String = ORDERSBindingNavigator.CountItem.Text
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
            reportString = "Order ID: " + OrderIDTextBox.Text + Environment.NewLine +
                "Order Date: " + OrderDateTextBox.Text + Environment.NewLine +
                "Product ID: " + OrderProductIDTextBox.Text + Environment.NewLine +
                "Vendor ID: " + OrderVendorIDTextBox.Text + Environment.NewLine +
                "Number of Product: " + HowManyTextBox.Text + Environment.NewLine +
                "Order Amount: " + Double.Parse(OrderTotalTextBox.Text).ToString("C") + Environment.NewLine
            ' increment total
            reportTotal += Double.Parse(OrderTotalTextBox.Text)
            ' send new report string to rich text box
            RichTextBox1.Text += reportString
            ' next row
            ORDERSBindingNavigator.MoveNextItem.PerformClick()
        Next
        ' output total
        RichTextBox1.Text += Environment.NewLine + "Total Cost: " + reportTotal.ToString("C")
    End Sub
End Class