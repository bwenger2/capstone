Public Class frmUser
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' continnual update time
        LabelTime.Text = DateAndTime.TimeOfDay
    End Sub

    Private Sub INVENTORYBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles INVENTORYBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.INVENTORYBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.WBPDataSet)

    End Sub

    Private Sub frmUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'WBPDataSet.SALES' table. You can move, or remove it, as needed.
        Me.SALESTableAdapter.Fill(Me.WBPDataSet.SALES)
        'TODO: This line of code loads data into the 'WBPDataSet.INVENTORY' table. You can move, or remove it, as needed.
        Me.INVENTORYTableAdapter.Fill(Me.WBPDataSet.INVENTORY)
        ' default text field settings
        TextBoxProductID_Search.Text = "Enter Product ID to Search."
        TextBoxTransactionID_Search.Text = "Enter Transaction ID to Search."
        TextBoxProductID_Search.Select()
        TextBoxProductID_Search.Focus()
    End Sub

    Private Sub ButtonLogOut_Click(sender As Object, e As EventArgs) Handles ButtonLogOut.Click
        Dim loginScreen As New frmLogin
        loginScreen.Show()
        Close()
    End Sub

    Private Sub SALESBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles SALESBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.SALESBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.WBPDataSet)
    End Sub

    Private Sub ButtonNewSale_Click(sender As Object, e As EventArgs) Handles ButtonNewSale.Click
        Dim newSaleScreen As New frmNewSale
        newSaleScreen.Show()
        Close()
    End Sub

    Private Sub ButtonSearchInventory_Click(sender As Object, e As EventArgs) Handles ButtonSearchInventory.Click
        ' check for required field
        If TextBoxProductID_Search.Text.Equals("") Then
            MessageBox.Show("Please enter a Product ID.", "Required Field")
            TextBoxProductID_Search.Focus()
        Else
            ' search for product
            AttemptInventorySearch()
        End If
    End Sub

    Private Sub ButtonSearchSales_Click(sender As Object, e As EventArgs) Handles ButtonSearchSales.Click
        ' check for required field
        If TextBoxTransactionID_Search.Text.Equals("") Then
            MessageBox.Show("Please enter a Transaction ID.", "Required Field")
            TextBoxTransactionID_Search.Focus()
        Else
            ' search for product
            AttemptSalesSearch()
        End If
    End Sub

    Private Sub AttemptInventorySearch()
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
            If TextBoxProductID_Search.Text.Equals(ProductIDTextBox.Text) Then
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
            Dim aProductID As String = ProductIDTextBox.Text
            Dim aProductName As String = ProductNameTextBox.Text
            Dim aProductDescription As String = ProductDescriptionTextBox.Text
            Dim aProductVolume As String = ProductOuncesTextBox.Text
            Dim aProductInStock As String = InStockTextBox.Text
            Dim aProductPrice As Double = Double.Parse(RetailPriceTextBox.Text)
            ' display product info
            MessageBox.Show("Product ID: " + aProductID + Environment.NewLine + Environment.NewLine +
                            "Name: " + aProductName + Environment.NewLine + Environment.NewLine +
                            "Description: " + aProductDescription + Environment.NewLine + Environment.NewLine +
                            "Volume: " + aProductVolume + "oz." + Environment.NewLine + Environment.NewLine +
                            "Price: " + aProductPrice.ToString("C") + Environment.NewLine + Environment.NewLine +
                            "Number In Stock: " + aProductInStock, "Product Information")
            ' reset text box focus
            With TextBoxProductID_Search
                .Clear()
                .Focus()
            End With
        Else
            ' show error
            MessageBox.Show("Product ID not found." +
                                Environment.NewLine +
                                "Please try again.", "Error")
            ' reset text box focus
            With TextBoxProductID_Search
                .Select()
                .SelectAll()
            End With
        End If
    End Sub

    Private Sub AttemptSalesSearch()
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
            If TextBoxTransactionID_Search.Text.Equals(TransactionIDTextBox.Text) Then
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
            ' grab data
            Dim aSaleID As String = TransactionIDTextBox.Text
            Dim aSaleTotal As Double = Double.Parse(TransactionTotalTextBox.Text)
            Dim aSaleDate As String = TransactionDateTextBox.Text
            ' display product info
            MessageBox.Show("Transaction ID: " + aSaleID + Environment.NewLine + Environment.NewLine +
                            "Date of Sale: " + aSaleDate + Environment.NewLine + Environment.NewLine +
                            "Total: " + aSaleTotal.ToString("C") + Environment.NewLine +
                            Environment.NewLine, "Transaction Information")
            ' reset text box focus
            With TextBoxTransactionID_Search
                .Clear()
                .Focus()
            End With
        Else
            ' show error
            MessageBox.Show("Transaction ID not found." +
                                Environment.NewLine +
                                "Please try again.", "Error")
            ' reset text box focus
            With TextBoxTransactionID_Search
                .Select()
                .SelectAll()
            End With
        End If
    End Sub

    Private Sub TextBoxProductID_Search_GotFocus(sender As Object, e As EventArgs) Handles TextBoxProductID_Search.GotFocus
        ActiveForm.AcceptButton = ButtonSearchInventory
    End Sub

    Private Sub TextBoxTransactionID_Search_GotFocus(sender As Object, e As EventArgs) Handles TextBoxTransactionID_Search.GotFocus
        ActiveForm.AcceptButton = ButtonSearchSales
    End Sub
End Class