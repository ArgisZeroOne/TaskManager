using System.Diagnostics;
using System.Security.AccessControl;
using System.Xml;

namespace Something;

public partial class TaskList : ContentPage
{
    List<Task> taskArray;

    public TaskList()
    {
        InitializeComponent();
    }
    public async void InitUser()
    {
    
        try
        {
            User user = UsersDatabase.ReadLocalUserSettings();
            UsersDatabase UserDB = new UsersDatabase();
            var username = user.Username();
            var password = user.Password();
            var userInDB = UserDB.ChooseUser(username, password);
            if (userInDB == null)
            {
                //await DisplayAlert("������", "������� � ������� ������", "�K");
            }
            else
            {


                TasksListView.ItemsSource = taskArray;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }

    }

  

    private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        taskArray = TaskDatabase.GetTasksFromDBToDate(DatePicker.Date.ToString("yyyy-MM-dd"));
       // InitUser();
        
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditTask());
    }

    private async void TasksListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var i = e.ItemIndex;

        await Navigation.PushAsync(new EditTask(taskArray[i]));
    }

    protected override void OnAppearing()
    {
        try
        {
            base.OnAppearing();

            taskArray = TaskDatabase.GetTasksFromDBToDate(DatePicker.Date.ToString("yyyy-MM-dd"));
            //  TasksListView.ItemsSource = taskArray;
            // InitUser();
        }
        catch (Exception ex)
        {

        }

    }

}