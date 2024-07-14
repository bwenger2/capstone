Public Class frmInventory
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' continnual update time
        LabelTime.Text = DateAndTime.TimeOfDay
    End Sub

    Private Sub INVENTORYBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles INVENTORYBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.INVENTORYBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.WBPDataSet)

    End Sub

    Private Sub frmInventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'WBPDataSet.INVENTORY' table. You can move, or remove it, as needed.
        Me.INVENTORYTableAdapter.Fill(Me.WBPDataSet.INVENTORY)
        ' helpful instruction
        TextBoxID.Text = "Enter Product ID to search."
        ' set cursor focus
        With TextBoxID
            .Select()
            .SelectAll()
        End With
    End Sub

    Private Sub ButtonBack_Click(sender As Object, e As EventArgs) Handles ButtonBack.Click
        ' navigate to admin menu
        Dim adminMenu As New frmAdmin
        adminMenu.Show()
        Close()
    End Sub

    Private Sub ButtonSearch_Click(sender As Object, e As EventArgs) Handles ButtonSearch.Click
        ' check for value in product ID text box
        If TextBoxID.Text.Equals("") Then
            MessageBox.Show("Please enter a Product ID.", "Required Field")
            TextBoxID.Focus()
        Else
            ' perform search
            ProductSearch()
        End If
    End Sub

    Private Sub ButtonSaveChanges_Click(sender As Object, e As EventArgs) Handles ButtonSaveChanges.Click
        Try
            INVENTORYBindingNavigatorSaveItem.PerformClick()
        Catch ex As Exception
            ' show error
            MessageBox.Show("Data Write Error.", "Error")
        End Try
    End Sub

    Private Sub ButtonDeleteProduct_Click(sender As Object, e As EventArgs) Handles ButtonDeleteProduct.Click
        Dim deleteInt As Integer = MessageBox.Show("Delete Product Record?" + Environment.NewLine +
                                                   "This cannot be undone.", "Question", MessageBoxButtons.OKCancel)
        If deleteInt = DialogResult.OK Then
            INVENTORYBindingNavigator.DeleteItem.PerformClick()
            INVENTORYBindingNavigatorSaveItem.PerformClick()
            ' reset fields
            DisableFields()
            ' reset form
            Me.frmInventory_Load(sender, e)
        End If
    End Sub

    Private Sub ButtonNew_Click(sender As Object, e As EventArgs) Handles ButtonNew.Click
        Dim newProductScreen As New frmNewProduct
        newProductScreen.Show()
        Close()
    End Sub

    Private Sub ProductSearch()
        ' navigate inventory table to first row
        INVENTORYBindingNavigator.MoveFirstItem.PerformClick()
        ' declarations
        Dim navPosition As Double = INVENTORYBindingNavigator.PositionItem.Text
        Dim navText As String = INVENTORYBindingNavigator.CountItem.Text
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
            navPosition = INVENTORYBindingNavigator.PositionItem.Text
            If TextBoxID.Text.Equals(ProductIDTextBox.Text) Then
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
            INVENTORYBindingNavigator.MoveNextItem.PerformClick()
        End While
        ' continue or retry
        If goBool = True Then
            ' grab data
            TextBoxName.Text = ProductNameTextBox.Text
            TextBoxDescription.Text = ProductDescriptionTextBox.Text
            TextBoxOunces.Text = ProductOuncesTextBox.Text
            TextBoxInStock.Text = InStockTextBox.Text
            TextBoxOnOrder.Text = OnOrderTextBox.Text
            TextBoxVendorID.Text = ProductVendorIDTextBox.Text
            ' parse finance values for formatting
            Dim newWholesale As Double = Double.Parse(WholesalePriceTextBox.Text)
            Dim newRetail As Double = Double.Parse(RetailPriceTextBox.Text)
            TextBoxWholesale.Text = newWholesale.ToString("C")
            TextBoxRetail.Text = newRetail.ToString("C")
            ' enable fields
            EnableFields()
        Else
            ' show error
            MessageBox.Show("Product ID not found." +
                                Environment.NewLine +
                                "Please try again.", "Error")
            DisableFields()
            ' reset text box focus
            With TextBoxID
                .Select()
                .SelectAll()
            End With
        End If
    End Sub

    ' disable fields subroutine
    Private Sub DisableFields()
        ' clear
        TextBoxName.Clear()
        TextBoxDescription.Clear()
        TextBoxOunces.Clear()
        TextBoxInStock.Clear()
        TextBoxOnOrder.Clear()
        TextBoxVendorID.Clear()
        TextBoxWholesale.Clear()
        TextBoxRetail.Clear()
        ' disable
        TextBoxName.Enabled = False
        TextBoxDescription.Enabled = False
        TextBoxOunces.Enabled = False
        TextBoxInStock.Enabled = False
        TextBoxOnOrder.Enabled = False
        TextBoxVendorID.Enabled = False
        TextBoxWholesale.Enabled = False
        TextBoxRetail.Enabled = False
        ButtonDeleteProduct.Enabled = False
        ButtonSaveChanges.Enabled = False
    End Sub

    ' enable fields subroutine
    Private Sub EnableFields()
        TextBoxName.Enabled = True
        TextBoxDescription.Enabled = True
        TextBoxOunces.Enabled = True
        TextBoxInStock.Enabled = True
        TextBoxOnOrder.Enabled = True
        TextBoxVendorID.Enabled = True
        TextBoxWholesale.Enabled = True
        TextBoxRetail.Enabled = True
        ButtonDeleteProduct.Enabled = True
        ButtonSaveChanges.Enabled = True
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