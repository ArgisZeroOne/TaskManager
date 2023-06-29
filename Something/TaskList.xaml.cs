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

        //Task task = new Task("4", "NSK,Klubnaya", "25-06-2023", "Xiaomi Washmachine", "Тестовая запись");
        //TaskDatabase.CreateTask(task);
        taskArray = TaskDatabase.GetTodayTasksFromDB();
       InitUser();
       // FormingTaskList();
        
    }
    public async void InitUser()
    {
        User user = UsersDatabase.ReadLocalUserSettings();
        UsersDatabase UserDB = new UsersDatabase();
        var username = user.Username();
        var password = user.Password();
        var userInDB = UserDB.ChooseUser(username, password);

        if (userInDB == null)
        {
            await DisplayAlert("Ошибка", "Войдите в учетную запись", "ОK");
        } else
        {

       
            TasksListView.ItemsSource = taskArray;
        }
        
    }

        //public void FormingTaskList()
        //{

        //    List<String> OrderNumbers = new List<String>();
        //    List<String> Addresses = new List<String>();
        //    List<String> Date = new List<String>();
        //    List<String> Model = new List<String>();
        //    List<String> Notes = new List<String>();
        //    List<String> Time = new List<String>();
        //    foreach (var item in taskArray)
        //    {
        //        OrderNumbers.Add(item.OrderNumber);
        //        Addresses.Add(item.Address);
        //        Date.Add(item.Date);
        //        Model.Add(item.Model);
        //        Notes.Add(item.Notes);
        //        Time.Add(item.Time);
        //    }
        //    OrderNumberList.ItemsSource = OrderNumbers;
        //    AddressList.ItemsSource = Addresses;
        //    DateList.ItemsSource = Date;
        //    ModelList.ItemsSource = Model;
        //    NotesList.ItemsSource = Notes;
        //    TimesList.ItemsSource = Time;
        //}

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        taskArray = TaskDatabase.GetTasksFromDBToDate(DatePicker.Date.ToString());
        InitUser();
        //FormingTaskList();
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
        base.OnAppearing();
        taskArray = TaskDatabase.GetTasksFromDBToDate(DatePicker.Date.ToString());
        InitUser();
    }

    //private async void DateList_ItemTapped(object sender, ItemTappedEventArgs e)
    //{
    //    var i = e.ItemIndex;
    //    List<string> dates = (List<string>)DateList.ItemsSource;
    //    List<string> ordernumbers = (List<string>)OrderNumberList.ItemsSource;

    //    var newDate = await DisplayPromptAsync("Дата", "Введите новую дату(дд-мм-гггг):", "OK", "Отмена", null, -1, null, dates[i]);

    //    TaskDatabase.SetDateToTask(ordernumbers[i], newDate);
    //    taskArray = TaskDatabase.GetTasksFromDBToDate(DatePicker.Date.ToString());
    //    FormingTaskList();
    //}
    //private async void ModelList_ItemTapped(object sender, ItemTappedEventArgs e)
    //{
    //    var i = e.ItemIndex;
    //    List<string> models = (List<string>)ModelList.ItemsSource;
    //    List<string> ordernumbers = (List<string>)OrderNumberList.ItemsSource;

    //    var newModel = await DisplayPromptAsync("Модель", "Введите новую модель:", "OK", "Отмена", null, -1, null, models[i]);

    //    TaskDatabase.SetModelToTask(ordernumbers[i], newModel);
    //    taskArray = TaskDatabase.GetTasksFromDBToDate(DatePicker.Date.ToString());
    //    FormingTaskList();
    //}
    //private async void NoteList_ItemTapped(object sender, ItemTappedEventArgs e)
    //{
    //    var i = e.ItemIndex;
    //    List<string> notes = (List<string>)NotesList.ItemsSource;
    //    List<string> ordernumbers = (List<string>)OrderNumberList.ItemsSource;

    //    var newNote = await DisplayPromptAsync("Модель", "Введите новую модель:", "OK", "Отмена", null, -1, null, notes[i]);
    //    Debug.WriteLine(newNote);
    //    TaskDatabase.SetNoteToTask(ordernumbers[i], newNote);
    //    taskArray = TaskDatabase.GetTasksFromDBToDate(DatePicker.Date.ToString());
    //    FormingTaskList();
    //}
    //private async void AddressList_ItemTapped(object sender, ItemTappedEventArgs e)
    //{
    //    var i = e.ItemIndex;
    //    List<string> addresses = (List<string>)AddressList.ItemsSource;
    //    List<string> ordernumber = (List<string>)OrderNumberList.ItemsSource;

    //    var newAddress = await DisplayPromptAsync("Адрес", "Введите новый адрес:", "OK", "Отмена", null, -1, null, addresses[i]);

    //    TaskDatabase.SetAddressToTask(ordernumber[i], newAddress);
    //    taskArray = TaskDatabase.GetTasksFromDBToDate(DatePicker.Date.ToString());
    //    FormingTaskList();
    //}
}