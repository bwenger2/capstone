Public Class frmOrders
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' continnual update time
        LabelTime.Text = DateAndTime.TimeOfDay
    End Sub

    Private Sub ORDERSBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles ORDERSBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.ORDERSBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.WBPDataSet)

    End Sub

    Private Sub frmOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'WBPDataSet.ORDERS' table. You can move, or remove it, as needed.
        Me.ORDERSTableAdapter.Fill(Me.WBPDataSet.ORDERS)
        ' helpful instruction
        TextBoxOrderID.Text = "Enter Order ID to search."
        ' set cursor focus
        With TextBoxOrderID
            .Select()
            .SelectAll()
        End With
    End Sub

    Private Sub ButtonBack_Click(sender As Object, e As EventArgs) Handles ButtonBack.Click
        ' navigate to accounting menu
        Dim accountingMenu As New frmAccounting
        accountingMenu.Show()
        Close()
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        ' check for value in order ID text box
        If TextBoxOrderID.Text.Equals("") Then
            MessageBox.Show("Please enter an Order ID.", "Required Field")
            TextBoxOrderID.Focus()
        Else
            ' perform search
            OrderSearch()
        End If
    End Sub

    Private Sub ButtonSaveChanges_Click(sender As Object, e As EventArgs) Handles ButtonSaveChanges.Click
        Try
            ORDERSBindingNavigatorSaveItem.PerformClick()
        Catch ex As Exception
            ' show error
            MessageBox.Show("Data Write Error.", "Error")
        End Try
    End Sub

    Private Sub ButtonDeleteOrder_Click(sender As Object, e As EventArgs) Handles ButtonDeleteOrder.Click
        Dim deleteInt As Integer = MessageBox.Show("Delete Order Record?" + Environment.NewLine +
                                                   "This cannot be undone.", "Question", MessageBoxButtons.OKCancel)
        If deleteInt = DialogResult.OK Then
            ORDERSBindingNavigator.DeleteItem.PerformClick()
            ORDERSBindingNavigatorSaveItem.PerformClick()
            ' reset fields
            DisableFields()
            ' reset form
            Me.frmOrders_Load(sender, e)
        End If
    End Sub

    Private Sub ButtonNew_Click(sender As Object, e As EventArgs) Handles ButtonNew.Click
        Dim orderForm As New frmNewOrder
        orderForm.Show()
        Close()
    End Sub

    Private Sub OrderSearch()
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
        ' loop to search for matches
        While continueBool
            ' reset variables
            navPosition = ORDERSBindingNavigator.PositionItem.Text
            If TextBoxOrderID.Text.Equals(OrderIDTextBox.Text) Then
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
            ORDERSBindingNavigator.MoveNextItem.PerformClick()
        End While
        ' continue or retry
        If goBool = True Then
            ' grab data
            DateTimePickerOrderDate.Text = OrderDateTextBox.Text
            TextBoxProductID.Text = OrderProductIDTextBox.Text
            TextBoxVendorID.Text = OrderVendorIDTextBox.Text
            TextBoxNumberOrdered.Text = HowManyTextBox.Text
            ' parse finance values for formatting
            Dim newTotal As Double = Double.Parse(OrderTotalTextBox.Text)
            TextBoxOrderTotal.Text = newTotal.ToString("C")
            ' enable fields
            EnableFields()
        Else
            ' show error
            MessageBox.Show("Order ID not found." +
                                Environment.NewLine +
                                "Please try again.", "Error")
            DisableFields()
            ' reset text box focus
            With TextBoxOrderID
                .Select()
                .SelectAll()
            End With
        End If
    End Sub

    Private Sub EnableFields()
        DateTimePickerOrderDate.Enabled = True
        TextBoxProductID.Enabled = True
        TextBoxVendorID.Enabled = True
        TextBoxNumberOrdered.Enabled = True
        TextBoxOrderTotal.Enabled = True
        ButtonDeleteOrder.Enabled = True
        ButtonSaveChanges.Enabled = True
    End Sub

    Private Sub DisableFields()
        DateTimePickerOrderDate.Text = ""
        TextBoxProductID.Clear()
        TextBoxVendorID.Clear()
        TextBoxNumberOrdered.Clear()
        TextBoxOrderTotal.Clear()
        DateTimePickerOrderDate.Enabled = False
        TextBoxProductID.Enabled = False
        TextBoxVendorID.Enabled = False
        TextBoxNumberOrdered.Enabled = False
        TextBoxOrderTotal.Enabled = False
        ButtonDeleteOrder.Enabled = False
        ButtonSaveChanges.Enabled = False
    End Sub

    Private Sub DateTimePickerOrderDate_LostFocus(sender As Object, e As EventArgs) Handles DateTimePickerOrderDate.LostFocus
        Try
            OrderDateTextBox.Text = DateTimePickerOrderDate.Text
        Catch ex As Exception
            MessageBox.Show("Order Date Error.", "Error")
        End Try
    End Sub

    Private Sub TextBoxProductID_LostFocus(sender As Object, e As EventArgs) Handles TextBoxProductID.LostFocus
        Dim tempInt As New Int32
        If TextBoxProductID.Text.Equals("") Then
            MessageBox.Show("Please Enter a Product ID.", "Required Field")
            TextBoxProductID.Focus()
        Else
            Try
                tempInt = Int32.Parse(TextBoxProductID.Text)
                OrderProductIDTextBox.Text = tempInt.ToString
            Catch ex As Exception
                ' show error
                MessageBox.Show("Product ID must be an integer.", "Error")
                TextBoxProductID.Focus()
            End Try
        End If
    End Sub

    Private Sub TextBoxVendorID_LostFocus(sender As Object, e As EventArgs) Handles TextBoxVendorID.LostFocus
        Dim tempInt As New Int32
        If TextBoxVendorID.Text.Equals("") Then
            MessageBox.Show("Please Enter a Vendor ID.", "Required Field")
            TextBoxVendorID.Focus()
        Else
            Try
                tempInt = Int32.Parse(TextBoxVendorID.Text)
                OrderVendorIDTextBox.Text = tempInt.ToString
            Catch ex As Exception
                ' show error
                MessageBox.Show("Vendor ID must be an integer.", "Error")
                TextBoxVendorID.Focus()
            End Try
        End If
    End Sub

    Private Sub TextBoxNumberOrdered_LostFocus(sender As Object, e As EventArgs) Handles TextBoxNumberOrdered.LostFocus
        Dim tempInt As New Int32
        If TextBoxNumberOrdered.Text.Equals("") Then
            MessageBox.Show("Please Enter the number ordered.", "Required Field")
            TextBoxNumberOrdered.Focus()
        Else
            Try
                tempInt = Int32.Parse(TextBoxNumberOrdered.Text)
                HowManyTextBox.Text = tempInt.ToString
            Catch ex As Exception
                ' show error
                MessageBox.Show("Number Ordered must be an integer.", "Error")
                TextBoxNumberOrdered.Focus()
            End Try
        End If
    End Sub

    Private Sub TextBoxOrderTotal_LostFocus(sender As Object, e As EventArgs) Handles TextBoxOrderTotal.LostFocus
        Dim tempDouble As New Double
        If TextBoxOrderTotal.Text.Equals("") Then
            MessageBox.Show("Please Enter the Order Total.", "Required Field")
            TextBoxOrderTotal.Focus()
        Else
            Try
                tempDouble = Double.Parse(TextBoxOrderTotal.Text.Trim("$"))
                OrderTotalTextBox.Text = tempDouble.ToString
            Catch ex As Exception
                ' show error
                MessageBox.Show("Order Total must be a monetary value.", "Error")
                TextBoxOrderTotal.Focus()
            End Try
        End If
    End Sub
End Class