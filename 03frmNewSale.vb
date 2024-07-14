Public Class frmNewSale

    ' declarations
    Dim totalMoney As New Double
    Dim saveBool As Boolean = True

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' continnual update time
        LabelTime.Text = DateAndTime.TimeOfDay
    End Sub

    Private Sub SALESBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles SALESBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.SALESBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.WBPDataSet)

    End Sub

    Private Sub frmNewSale_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'WBPDataSet.SALES' table. You can move, or remove it, as needed.
        Me.SALESTableAdapter.Fill(Me.WBPDataSet.SALES)
        ' generate next transaction id
        SALESBindingNavigator.MoveLastItem.PerformClick()
        Dim tempInt As Int32 = Int32.Parse(TransactionIDTextBox.Text)
        tempInt += 1
        ' generate new field in data set
        SALESBindingNavigator.AddNewItem.PerformClick()
        ' default value for total
        TransactionTotalTextBox.Text = "0"
        ' set transaction id
        TransactionIDTextBox.Text = tempInt.ToString
        ' generate date
        TransactionDateTextBox.Text = DateAndTime.Today
        ' helpful instruction
        TextBox1.Text = "Enter a Sale Total."
        ' focus on textbox
        TextBox1.Select()
        TextBox1.Focus()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        ' delete created database content
        SALESBindingNavigator.DeleteItem.PerformClick()
        ' return to main menu
        Dim userMenu As New frmUser
        userMenu.Show()
        Close()
    End Sub

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        If TextBox1.Text.Equals("") Then
            MessageBox.Show("Please enter a Transaction Total.", "Required Field")
            TextBox1.Focus()
        Else
            Try
                totalMoney = Double.Parse(TextBox1.Text.Trim("$"))
                saveBool = True
            Catch ex As Exception
                ' show error
                MessageBox.Show("Total must be a monetary value." +
                                Environment.NewLine +
                                "Please try again.", "Error")
                saveBool = False
                TextBox1.Focus()
            End Try
            If saveBool = True Then
                TransactionTotalTextBox.Text = TextBox1.Text
                SALESBindingNavigatorSaveItem.PerformClick()
                Dim userMenu As New frmUser
                userMenu.Show()
                Close()
            End If
        End If
    End Sub
End Class