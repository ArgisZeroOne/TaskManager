using System.Diagnostics;

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
		
	}
	public void UserForm(User user)
	{
        Enter.IsVisible = false;
        Enter.IsEnabled = false;
        UserData.IsEnabled = true;
        UserData.IsVisible = true;
        usernametxt.Text = user.Username();
		firstname.Text = user.FirstName();
		lastname.Text =  user.LastName();
		
		

    }
    public void InitUser()
	{
		
		User user = UsersDatabase.ReadLocalUserSettings();
        UsersDatabase UserDB = new UsersDatabase();
		var username = user.Username();
		var password = user.Password();
		User userInDB = null;

        try
		{
            userInDB = UserDB.ChooseUser(username, password);
        } catch (Exception ex) { Debug.WriteLine(ex); }
        if (userInDB != null) UserForm(userInDB);
        else EnterForm();

    }
	private void OnEnterBtnClicked(object sender, EventArgs e)
	{
		UsersDatabase UserDB = new UsersDatabase();
		
		User user = UserDB.ChooseUser(usernameInput.Text,PasswordInput.Text);
		
		UsersDatabase.WriteLocalUserSettings(user);
		InitUser();
    }

    private void exit_Clicked(object sender, EventArgs e)
    {
		Preferences.Default.Clear();
		InitUser();
    }

  
}