Public Class frmNewOrder
    ' declarations
    Dim totalMoney As New Double
    Dim saveBool As Boolean = True

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' continnual update time
        LabelTime.Text = DateAndTime.TimeOfDay
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        ' delete created database content
        ORDERSBindingNavigator.DeleteItem.PerformClick()
        ' return to main menu
        Dim ordersMenu As New frmOrders
        ordersMenu.Show()
        Close()
    End Sub

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        Try
            ORDERSBindingNavigatorSaveItem.PerformClick()
        Catch ex As Exception
            ' show error
            MessageBox.Show("Data Write Error.", "Error")
        End Try
        Dim ordersMenu As New frmOrders
        ordersMenu.Show()
        Close()
    End Sub

    Private Sub ORDERSBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles ORDERSBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.ORDERSBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.WBPDataSet)
    End Sub

    Private Sub frmNewOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'WBPDataSet.ORDERS' table. You can move, or remove it, as needed.
        Me.ORDERSTableAdapter.Fill(Me.WBPDataSet.ORDERS)
        ' generate next order id
        ORDERSBindingNavigator.MoveLastItem.PerformClick()
        Dim tempInt As Int32 = Int32.Parse(OrderIDTextBox.Text)
        tempInt += 1
        ' generate new field in data set
        ORDERSBindingNavigator.AddNewItem.PerformClick()
        ' default values
        OrderTotalTextBox.Text = "0"
        HowManyTextBox.Text = "0"
        OrderVendorIDTextBox.Text = "1"
        OrderProductIDTextBox.Text = "1"
        ' set order id
        OrderIDTextBox.Text = tempInt.ToString
        ' generate date
        OrderDateTextBox.Text = DateAndTime.Today
        ' helpful instruction
        TextBoxOrderTotal.Text = "0.00"
        TextBoxNumberOrdered.Text = "0"
        TextBoxVendorID.Text = "1"
        TextBoxProductID.Text = "1"
        ' focus on textbox
        TextBoxProductID.Select()
        TextBoxProductID.SelectAll()
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