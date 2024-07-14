Public Class frmNewProduct

    ' declarations
    Dim saveBool As Boolean = True

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' continnual update time
        LabelTime.Text = DateAndTime.TimeOfDay
    End Sub

    Private Sub INVENTORYBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles INVENTORYBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.INVENTORYBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.WBPDataSet)

    End Sub

    Private Sub frmNewProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'WBPDataSet.INVENTORY' table. You can move, or remove it, as needed.
        Me.INVENTORYTableAdapter.Fill(Me.WBPDataSet.INVENTORY)
        ' generate next product id
        INVENTORYBindingNavigator.MoveLastItem.PerformClick()
        Dim tempInt As Int32 = Int32.Parse(ProductIDTextBox.Text)
        tempInt += 1
        ' generate new field in data set
        INVENTORYBindingNavigator.AddNewItem.PerformClick()
        ' set product id
        ProductIDTextBox.Text = tempInt.ToString
        ' default values
        ProductNameTextBox.Text = "product name"
        ProductDescriptionTextBox.Text = "product description"
        ProductOuncesTextBox.Text = "0"
        InStockTextBox.Text = "0"
        OnOrderTextBox.Text = "0"
        ProductVendorIDTextBox.Text = "0"
        WholesalePriceTextBox.Text = "0.00"
        RetailPriceTextBox.Text = "0.00"
        ' helpful instruction
        TextBoxName.Text = "Product Name."
        TextBoxDescription.Text = "Product Description."
        TextBoxOunces.Text = "16"
        TextBoxInStock.Text = "0"
        TextBoxOnOrder.Text = "0"
        TextBoxVendorID.Text = "0"
        TextBoxWholesale.Text = "0.00"
        TextBoxRetail.Text = "0.00"
        ' focus on textbox
        With TextBoxName
            .Select()
            .SelectAll()
        End With
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        ' delete created database content
        INVENTORYBindingNavigator.DeleteItem.PerformClick()
        ' return to main menu
        Dim invMenu As New frmInventory
        invMenu.Show()
        Close()
    End Sub

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        INVENTORYBindingNavigatorSaveItem.PerformClick()
        Dim invMenu As New frmInventory
        invMenu.Show()
        Close()
    End Sub

    Private Sub TextBoxName_LostFocus(sender As Object, e As EventArgs) Handles TextBoxName.LostFocus
        If TextBoxName.Text.Equals("") Then
            MessageBox.Show("Please Enter a Product Name.", "Required Field")
            TextBoxName.Focus()
        Else
            ProductNameTextBox.Text = TextBoxName.Text
        End If
    End Sub

    Private Sub TextBoxDescription_LostFocus(sender As Object, e As EventArgs) Handles TextBoxDescription.LostFocus
        If TextBoxDescription.Text.Equals("") Then
            MessageBox.Show("Please Enter a Product Description.", "Required Field")
            TextBoxDescription.Focus()
        Else
            ProductDescriptionTextBox.Text = TextBoxDescription.Text
        End If
    End Sub

    Private Sub TextBoxOunces_LostFocus(sender As Object, e As EventArgs) Handles TextBoxOunces.LostFocus
        Dim tempInt As New Int32
        If TextBoxOunces.Text.Equals("") Then
            MessageBox.Show("Please Enter a Product Volume in Ounces.", "Required Field")
            TextBoxOunces.Focus()
        Else
            Try
                tempInt = Int32.Parse(TextBoxOunces.Text)
                ProductOuncesTextBox.Text = tempInt.ToString
            Catch ex As Exception
                ' show error
                MessageBox.Show("Ounces must be an integer.", "Error")
                TextBoxOunces.Focus()
            End Try
        End If
    End Sub

    Private Sub TextBoxInStock_LostFocus(sender As Object, e As EventArgs) Handles TextBoxInStock.LostFocus
        Dim tempInt As New Int32
        If TextBoxInStock.Text.Equals("") Then
            MessageBox.Show("Please Enter a Number In Stock.", "Required Field")
            TextBoxInStock.Focus()
        Else
            Try
                tempInt = Int32.Parse(TextBoxInStock.Text)
                InStockTextBox.Text = tempInt.ToString
            Catch ex As Exception
                ' show error
                MessageBox.Show("Number In Stock must be an integer.", "Error")
                TextBoxInStock.Focus()
            End Try
        End If
    End Sub

    Private Sub TextBoxOnOrder_LostFocus(sender As Object, e As EventArgs) Handles TextBoxOnOrder.LostFocus
        Dim tempInt As New Int32
        If TextBoxOnOrder.Text.Equals("") Then
            MessageBox.Show("Please Enter a Number On Order.", "Required Field")
            TextBoxOnOrder.Focus()
        Else
            Try
                tempInt = Int32.Parse(TextBoxOnOrder.Text)
                OnOrderTextBox.Text = tempInt.ToString
            Catch ex As Exception
                ' show error
                MessageBox.Show("Number On Order must be an integer.", "Error")
                TextBoxOnOrder.Focus()
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
                ProductVendorIDTextBox.Text = tempInt.ToString
            Catch ex As Exception
                ' show error
                MessageBox.Show("Vendor ID must be an integer.", "Error")
                TextBoxVendorID.Focus()
            End Try
        End If
    End Sub

    Private Sub TextBoxWholesale_LostFocus(sender As Object, e As EventArgs) Handles TextBoxWholesale.LostFocus
        Dim tempDouble As New Double
        If TextBoxWholesale.Text.Equals("") Then
            MessageBox.Show("Please Enter the Wholesale Price.", "Required Field")
            TextBoxWholesale.Focus()
        Else
            Try
                tempDouble = Double.Parse(TextBoxWholesale.Text.Trim("$"))
                WholesalePriceTextBox.Text = tempDouble.ToString
            Catch ex As Exception
                ' show error
                MessageBox.Show("Wholesale Price must be a monetary value.", "Error")
                TextBoxWholesale.Focus()
            End Try
        End If
    End Sub

    Private Sub TextBoxRetail_LostFocus(sender As Object, e As EventArgs) Handles TextBoxRetail.LostFocus
        Dim tempDouble As New Double
        If TextBoxRetail.Text.Equals("") Then
            MessageBox.Show("Please Enter the Retail Price.", "Required Field")
            TextBoxRetail.Focus()
        Else
            Try
                tempDouble = Double.Parse(TextBoxRetail.Text.Trim("$"))
                RetailPriceTextBox.Text = tempDouble.ToString
            Catch ex As Exception
                ' show error
                MessageBox.Show("Retail Price must be a monetary value.", "Error")
                TextBoxRetail.Focus()
            End Try
        End If
    End Sub
End Class