using System.Diagnostics;


namespace Something;

public partial class List : ContentPage
{
    List<Task> taskArray = new List<Task> { new Task(" ", " ", " ", " ", " ", " ") };
    protected bool UserLogIn = false;
    TaskDatabase db = new TaskDatabase();
    UsersDatabase UserDB = new UsersDatabase();
    public List()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    { 
            base.OnAppearing();
            InitUser();
    }
    public void InitUser()
    {

        try
        {
            UserLogIn = false;
            User user = UsersDatabase.ReadLocalUserSettings();
            var username = user.Username();
            var password = user.Password();
            var userInDB = UserDB.ChooseUser(username, password);
            if (userInDB == null)
            {
                
                taskArray = db.GetTasksFromDBToDate("01-01-1990");
                TasksListView.ItemsSource = taskArray;
                
            }
            else
            {
                UserLogIn = true;
                taskArray = db.GetTasksFromDBToDate(DatePicker.Date.ToString("yyyy-MM-dd"));
                TasksListView.ItemsSource = taskArray;
         
            }
        }
        catch (Exception ex)
        {
            
        }

    }
    private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        InitUser();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditTask());
    }

    private async void TasksListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (UserLogIn)
        {
            var i = e.ItemIndex;

            await Navigation.PushAsync(new EditTask(taskArray[i]));
        }
     
    }
}