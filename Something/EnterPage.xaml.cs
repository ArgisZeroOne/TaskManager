namespace Something;

public partial class EnterPage : ContentPage
{
	public EnterPage()
	{
		InitializeComponent();
        InitUser();
		 
    }
	
	public void EnterForm()
	{
		Enter.IsVisible = true;
		Enter.IsEnabled = true;
		UserData.IsEnabled = false;
		UserData.IsVisible = false;
		MainLabel.Text = "����";
	}
	public void UserForm(User user)
	{
        Enter.IsVisible = false;
        Enter.IsEnabled = false;
        UserData.IsEnabled = true;
        UserData.IsVisible = true;
        username.Text = "��� ������������: " + user.Username();
		firstname.Text = "���: " +  user.FirstName();
		lastname.Text =  "�������: " + user.LastName();
		rights.Text = "�����: " + user.Rights();
		MainLabel.Text = "������������, " + user.FirstName();

    }
    public void InitUser()
	{
		
		User user = UsersDatabase.ReadLocalUserSettings();
        UsersDatabase UserDB = new UsersDatabase();
		var username = user.Username();
		var password = user.Password();
        var userInDB = UserDB.ChooseUser(username,password);

        if (userInDB != null) UserForm(userInDB);
		else EnterForm();
		
    }
	private void OnEnterBtnClicked(object sender, EventArgs e)
	{
		UsersDatabase UserDB = new UsersDatabase();
		
		User user = UserDB.ChooseUser(usernameInput.Text,PasswordInput.Text);
		MainLabel.Text = "������������, " + user.Username();
		UsersDatabase.WriteLocalUserSettings(user);
		InitUser();
    }

    private void exit_Clicked(object sender, EventArgs e)
    {
		Preferences.Default.Clear();
		InitUser();
    }
}