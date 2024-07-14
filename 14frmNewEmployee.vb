Public Class frmNewEmployee
    ' declarations
    Dim saveBool As Boolean = True
    Dim tempIntphone As Integer = 0
    Dim phoneLength As Integer = 0

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' continnual update time
        LabelTime.Text = DateAndTime.TimeOfDay
    End Sub

    Private Sub EMPLOYEESBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles EMPLOYEESBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.EMPLOYEESBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.WBPDataSet)

    End Sub

    Private Sub frmNewEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'WBPDataSet.EMPLOYEES' table. You can move, or remove it, as needed.
        Me.EMPLOYEESTableAdapter.Fill(Me.WBPDataSet.EMPLOYEES)
        ' generate next employee id
        EMPLOYEESBindingNavigator.MoveLastItem.PerformClick()
        Dim tempInt As Int32 = Int32.Parse(EmployeeIDTextBox.Text)
        tempInt += 1
        ' generate new field in data set
        EMPLOYEESBindingNavigator.AddNewItem.PerformClick()
        ' enable fields
        EnableFields()
        ' set product id
        EmployeeIDTextBox.Text = tempInt.ToString
        ' default values
        LastNameTextBox.Text = "last name"
        FirstNameTextBox.Text = "first name"
        AddressTextBox.Text = "address"
        CityTextBox.Text = "city"
        StateTextBox.Text = "OH"
        ZipTextBox.Text = "55555"
        PhoneTextBox.Text = "6145555555"
        EmailTextBox.Text = "email address"
        StartDateTextBox.Text = DateAndTime.Today
        UserNameTextBox.Text = "username"
        PasswordTextBox.Text = "password"
        AdminCheckBox.Checked = False
        FullTimeCheckBox.Checked = False
        ' helpful instruction
        TextBoxFirstName.Text = "Enter first name"
        TextBoxLastName.Text = "Enter last name"
        TextBoxAddress.Text = "Enter address"
        TextBoxCity.Text = "Enter city"
        ComboBoxState.SelectedText = "OH"
        TextBoxZipCode.Text = "55555"
        TextBoxPhone.Text = "6147725555"
        TextBoxEmail.Text = "user@email.net"
        TextBoxPassword.Text = "Enter password"
        TextBoxUserName.Text = "Enter user name"
        ' focus on textbox
        With TextBoxFirstName
            .Select()
            .SelectAll()
        End With
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        ' delete created database content
        EMPLOYEESBindingNavigator.DeleteItem.PerformClick()
        ' navigate to employee window
        Dim employeeWindow As New frmEmployees
        employeeWindow.Show()
        Close()
    End Sub

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        Try
            EMPLOYEESBindingNavigatorSaveItem.PerformClick()
        Catch ex As Exception
            ' show error
            MessageBox.Show("Data Write Error.", "Error")
        End Try
        Dim employeeWindow As New frmEmployees
        employeeWindow.Show()
        Close()
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
End Class