Public Class frmSales
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' continnual update time
        LabelTime.Text = DateAndTime.TimeOfDay
    End Sub

    Private Sub ButtonBack_Click(sender As Object, e As EventArgs) Handles ButtonBack.Click
        ' navigate to accounting menu
        Dim accountingMenu As New frmAccounting
        accountingMenu.Show()
        Close()
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        ' check for value in product ID text box
        If TextBoxTransactionID.Text.Equals("") Then
            MessageBox.Show("Please enter a Transaction ID.", "Required Field")
            TextBoxTransactionID.Focus()
        Else
            ' perform search
            TransactionSearch()
        End If
    End Sub

    Private Sub ButtonSaveChanges_Click(sender As Object, e As EventArgs) Handles ButtonSaveChanges.Click
        Try
            SALESBindingNavigatorSaveItem.PerformClick()
        Catch ex As Exception
            ' show error
            MessageBox.Show("Data Write Error.", "Error")
        End Try
    End Sub

    Private Sub ButtonDeleteTransaction_Click(sender As Object, e As EventArgs) Handles ButtonDeleteTransaction.Click
        Dim deleteInt As Integer = MessageBox.Show("Delete Transaction Record?" + Environment.NewLine +
                                                   "This cannot be undone.", "Question", MessageBoxButtons.OKCancel)
        If deleteInt = DialogResult.OK Then
            SALESBindingNavigator.DeleteItem.PerformClick()
            SALESBindingNavigatorSaveItem.PerformClick()
            ' reset fields
            DisableFields()
            ' reset form
            Me.frmSales_Load(sender, e)
        End If
    End Sub

    Private Sub ButtonNew_Click(sender As Object, e As EventArgs) Handles ButtonNew.Click
        Dim newTrans As New frmNewTransaction
        newTrans.Show()
        Close()
    End Sub

    Private Sub SALESBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles SALESBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.SALESBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.WBPDataSet)

    End Sub

    Private Sub frmSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'WBPDataSet.SALES' table. You can move, or remove it, as needed.
        Me.SALESTableAdapter.Fill(Me.WBPDataSet.SALES)
        ' helpful instruction
        TextBoxTransactionID.Text = "Enter Transaction ID to search."
        ' set cursor focus
        With TextBoxTransactionID
            .Select()
            .SelectAll()
        End With
    End Sub

    Private Sub TransactionSearch()
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
        ' loop to search for matches
        While continueBool
            ' reset variables
            navPosition = SALESBindingNavigator.PositionItem.Text
            If TextBoxTransactionID.Text.Equals(TransactionIDTextBox.Text) Then
                goBool = True
                Exit While
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
            SALESBindingNavigator.MoveNextItem.PerformClick()
        End While
        ' continue or retry
        If goBool = True Then
            ' grab date
            DateTimePickerTransactionDate.Text = TransactionDateTextBox.Text
            ' parse finance values for formatting
            Dim newTotal As Double = Double.Parse(TransactionTotalTextBox.Text)
            TextBoxTransactionTotal.Text = newTotal.ToString("C")
            ' enable fields
            EnableFields()
        Else
            ' show error
            MessageBox.Show("Transaction ID not found." +
                                Environment.NewLine +
                                "Please try again.", "Error")
            DisableFields()
            ' reset text box focus
            With TextBoxTransactionID
                .Select()
                .SelectAll()
            End With
        End If
    End Sub

    Private Sub EnableFields()
        DateTimePickerTransactionDate.Enabled = True
        TextBoxTransactionTotal.Enabled = True
        ButtonDeleteTransaction.Enabled = True
        ButtonSaveChanges.Enabled = True
    End Sub

    Private Sub DisableFields()
        DateTimePickerTransactionDate.Text = ""
        TextBoxTransactionTotal.Text = ""
        DateTimePickerTransactionDate.Enabled = False
        TextBoxTransactionTotal.Enabled = False
        ButtonDeleteTransaction.Enabled = False
        ButtonSaveChanges.Enabled = False
    End Sub

    Private Sub DateTimePickerTransactionDate_LostFocus(sender As Object, e As EventArgs) Handles DateTimePickerTransactionDate.LostFocus
        Try
            TransactionDateTextBox.Text = DateTimePickerTransactionDate.Text
        Catch ex As Exception
            MessageBox.Show("Transaction Date Error.", "Error")
        End Try
    End Sub

    Private Sub TextBoxTransactionTotal_LostFocus(sender As Object, e As EventArgs) Handles TextBoxTransactionTotal.LostFocus
        Dim tempDouble As New Double
        If TextBoxTransactionTotal.Text.Equals("") Then
            MessageBox.Show("Please Enter the Transaction Total.", "Required Field")
            TextBoxTransactionTotal.Focus()
        Else
            Try
                tempDouble = Double.Parse(TextBoxTransactionTotal.Text.Trim("$"))
                TransactionTotalTextBox.Text = tempDouble.ToString
            Catch ex As Exception
                ' show error
                MessageBox.Show("Transaction Total must be a monetary value.", "Error")
                TextBoxTransactionTotal.Focus()
            End Try
        End If
    End Sub
End Class