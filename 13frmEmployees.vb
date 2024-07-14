Public Class frmEmployees

    'declarations
    Dim tempIntphone As Integer = 0
    Dim phoneLength As Integer = 0

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' continnual update time
        LabelTime.Text = DateAndTime.TimeOfDay
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
            MessageBox.Show("Please enter an Employee ID.", "Required Field")
            TextBoxID.Focus()
        Else
            ' perform search
            EmployeeSearch()
        End If
    End Sub

    Private Sub ButtonSaveChanges_Click(sender As Object, e As EventArgs) Handles ButtonSaveChanges.Click
        Try
            EMPLOYEESBindingNavigatorSaveItem.PerformClick()
        Catch ex As Exception
            ' show error
            MessageBox.Show("Data Write Error.", "Error")
        End Try
    End Sub

    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click
        Dim deleteInt As Integer = MessageBox.Show("Delete Employee Record?" + Environment.NewLine +
                                                   "This cannot be undone.", "Question", MessageBoxButtons.OKCancel)
        If deleteInt = DialogResult.OK Then
            EMPLOYEESBindingNavigator.DeleteItem.PerformClick()
            EMPLOYEESBindingNavigatorSaveItem.PerformClick()
            ' reset fields
            DisableFields()
            ' reset form
            Me.frmEmployees_Load(sender, e)
        End If
    End Sub

    Private Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        ' navigate to add employee window
        Dim addWindow As New frmNewEmployee
        addWindow.Show()
        Close()
    End Sub

    Private Sub EMPLOYEESBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles EMPLOYEESBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.EMPLOYEESBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.WBPDataSet)

    End Sub

    Private Sub frmEmployees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'WBPDataSet.EMPLOYEES' table. You can move, or remove it, as needed.
        Me.EMPLOYEESTableAdapter.Fill(Me.WBPDataSet.EMPLOYEES)
        ' helpful instruction
        TextBoxID.Text = "Enter Employee ID to search."
        ' set cursor focus
        TextBoxID.Select()
        TextBoxID.SelectAll()
    End Sub

    Private Sub EmployeeSearch()
        EnableFields()
        ' navigate inventory table to first row
        EMPLOYEESBindingNavigator.MoveFirstItem.PerformClick()
        ' declarations
        Dim navPosition As Double = EMPLOYEESBindingNavigator.PositionItem.Text
        Dim navText As String = EMPLOYEESBindingNavigator.CountItem.Text
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
            navPosition = EMPLOYEESBindingNavigator.PositionItem.Text
            If TextBoxID.Text.Equals(EmployeeIDTextBox.Text) Then
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
            EMPLOYEESBindingNavigator.MoveNextItem.PerformClick()
        End While
        ' continue or retry
        If goBool = True Then
            ' grab data
            TextBoxFirstName.Text = FirstNameTextBox.Text
            TextBoxLastName.Text = LastNameTextBox.Text
            TextBoxAddress.Text = AddressTextBox.Text
            TextBoxCity.Text = CityTextBox.Text
            ComboBoxState.Text = StateTextBox.Text
            TextBoxZipCode.Text = ZipTextBox.Text
            TextBoxPhone.Text = PhoneTextBox.Text
            TextBoxEmail.Text = EmailTextBox.Text
            TextBoxUserName.Text = UserNameTextBox.Text
            TextBoxPassword.Text = PasswordTextBox.Text
            DateTimePickerStart.Text = StartDateTextBox.Text


            ' enable delete button
            ButtonDelete.Enabled = True
        Else
            ' show error
            MessageBox.Show("Employee ID not found." +
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
        ' reset fields
        TextBoxFirstName.Clear()
        TextBoxLastName.Clear()
        TextBoxAddress.Clear()
        TextBoxCity.Clear()
        ComboBoxState.Text = ""
        TextBoxZipCode.Clear()
        TextBoxPhone.Clear()
        TextBoxEmail.Clear()
        TextBoxUserName.Clear()
        TextBoxPassword.Clear()
        ' disable fields
        TextBoxFirstName.Enabled = False
        TextBoxLastName.Enabled = False
        TextBoxAddress.Enabled = False
        TextBoxCity.Enabled = False
        ComboBoxState.Enabled = False
        TextBoxZipCode.Enabled = False
        TextBoxPhone.Enabled = False
        TextBoxEmail.Enabled = False
        TextBoxUserName.Enabled = False
        TextBoxPassword.Enabled = False
        AdminCheckBox.Enabled = False
        FullTimeCheckBox.Enabled = False
        DateTimePickerStart.Enabled = False

        ButtonDelete.Enabled = False
        ButtonSaveChanges.Enabled = False
    End Sub

    ' enable fields subroutine
    Private Sub EnableFields()
        ' enable text fields
        TextBoxFirstName.Enabled = True
        TextBoxLastName.Enabled = True
        TextBoxAddress.Enabled = True
        TextBoxCity.Enabled = True
        ComboBoxState.Enabled = True
        TextBoxZipCode.Enabled = True
        TextBoxPhone.Enabled = True
        TextBoxEmail.Enabled = True
        TextBoxUserName.Enabled = True
        TextBoxPassword.Enabled = True
        AdminCheckBox.Enabled = True
        FullTimeCheckBox.Enabled = True
        DateTimePickerStart.Enabled = True

        ButtonDelete.Enabled = True
        ButtonSaveChanges.Enabled = True
    End Sub

    Private Sub TextBoxFirstName_LostFocus(sender As Object, e As EventArgs) Handles TextBoxFirstName.LostFocus
        If TextBoxFirstName.Text.Equals("") Then
            MessageBox.Show("Please Enter a First Name.", "Required Field")
            TextBoxFirstName.Focus()
        Else
            FirstNameTextBox.Text = TextBoxFirstName.Text
        End If
    End Sub

    Private Sub TextBoxLastName_LostFocus(sender As Object, e As EventArgs) Handles TextBoxLastName.LostFocus
        If TextBoxLastName.Text.Equals("") Then
            MessageBox.Show("Please Enter a Last Name.", "Required Field")
            TextBoxLastName.Focus()
        Else
            LastNameTextBox.Text = TextBoxLastName.Text
        End If
    End Sub

    Private Sub TextBoxAddress_LostFocus(sender As Object, e As EventArgs) Handles TextBoxAddress.LostFocus
        If TextBoxAddress.Text.Equals("") Then
            MessageBox.Show("Please Enter an Address.", "Required Field")
            TextBoxAddress.Focus()
        Else
            AddressTextBox.Text = TextBoxAddress.Text
        End If
    End Sub

    Private Sub TextBoxCity_LostFocus(sender As Object, e As EventArgs) Handles TextBoxCity.LostFocus
        If TextBoxCity.Text.Equals("") Then
            MessageBox.Show("Please Enter a City.", "Required Field")
            TextBoxCity.Focus()
        Else
            CityTextBox.Text = TextBoxCity.Text
        End If
    End Sub

    Private Sub ComboBoxState_LostFocus(sender As Object, e As EventArgs) Handles ComboBoxState.LostFocus
        StateTextBox.Text = ComboBoxState.Text
    End Sub

    Private Sub TextBoxZipCode_LostFocus(sender As Object, e As EventArgs) Handles TextBoxZipCode.LostFocus
        Dim tempInt As New Int32
        Dim zipLength As Int32 = 0
        If TextBoxZipCode.Text.Equals("") Then
            MessageBox.Show("Please Enter a Zip Code.", "Required Field")
            TextBoxZipCode.Focus()
        Else
            Try
                tempInt = Int32.Parse(TextBoxZipCode.Text)
                For Each c As Char In TextBoxZipCode.Text
                    zipLength += 1
                Next
                If zipLength <> 5 Then
                    Dim ex1 As New Exception
                    Throw ex1
                End If
                ZipTextBox.Text = tempInt.ToString
            Catch ex As Exception
                MessageBox.Show("Zip Code must be a 5 digit integer (12345).", "Error")
                TextBoxZipCode.Focus()
            End Try
        End If
    End Sub

    Private Sub TextBoxPhone_LostFocus(sender As Object, e As EventArgs) Handles TextBoxPhone.LostFocus
        tempIntphone = 0
        phoneLength = 0
        If TextBoxPhone.Text.Equals("") Then
            MessageBox.Show("Please Enter a Phone Number.", "Required Field")
            TextBoxPhone.Focus()
        Else
            Try
                For Each c As Char In TextBoxPhone.Text
                    phoneLength += 1
                Next
                If phoneLength <> 10 Then
                    Dim ex1 As New Exception
                    Throw ex1
                End If
                PhoneTextBox.Text = TextBoxPhone.Text
            Catch ex As Exception
                MessageBox.Show("Phone Number must be a 10 digit integer (1234567890).", "Error")
                'MessageBox.Show(ex.ToString, "Error")
                TextBoxPhone.Focus()
            End Try
        End If
    End Sub

    Private Sub TextBoxEmail_LostFocus(sender As Object, e As EventArgs) Handles TextBoxEmail.LostFocus
        If TextBoxEmail.Text.Equals("") Then
            MessageBox.Show("Please Enter an Email Address.", "Required Field")
            TextBoxEmail.Focus()
        Else
            Try
                ' validate string value as email
                Dim eAddress = New System.Net.Mail.MailAddress(TextBoxEmail.Text)
                ' send value
                EmailTextBox.Text = TextBoxEmail.Text
            Catch ex As Exception
                MessageBox.Show("You must enter a valid email address. (address@domain.net)", "Error")
                TextBoxEmail.Focus()
            End Try
        End If
    End Sub

    Private Sub TextBoxUserName_LostFocus(sender As Object, e As EventArgs) Handles TextBoxUserName.LostFocus
        If TextBoxUserName.Text.Equals("") Then
            MessageBox.Show("Please Enter a User Name.", "Required Field")
            TextBoxUserName.Focus()
        Else
            UserNameTextBox.Text = TextBoxUserName.Text
        End If
    End Sub

    Private Sub TextBoxPassword_LostFocus(sender As Object, e As EventArgs) Handles TextBoxPassword.LostFocus
        If TextBoxPassword.Text.Equals("") Then
            MessageBox.Show("Please Enter a Password.", "Required Field")
            TextBoxPassword.Focus()
        Else
            PasswordTextBox.Text = TextBoxPassword.Text
        End If
    End Sub

    Private Sub DateTimePickerStart_LostFocus(sender As Object, e As EventArgs) Handles DateTimePickerStart.LostFocus
        Try
            StartDateTextBox.Text = DateTimePickerStart.Text
        Catch ex As Exception
            MessageBox.Show("Start Date Error.", "Error")
        End Try
    End Sub
End Class