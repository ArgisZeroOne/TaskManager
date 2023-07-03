namespace Something;

public partial class EditTask : ContentPage
{
    public Task inputTask { get; set; }
    private bool NewTask = false;
    public EditTask(Task inputTask)
    {
        InitializeComponent();
        this.inputTask = inputTask;
        FillEntryes(inputTask);
    }
    void FillEntryes(Task input)
    {
        OrderNumberInput.Text = input.OrderNumber;
        OrderNumberInput.IsEnabled = false;
        ModelInput.Text = input.Model;
        NotesInput.Text = input.Notes;

        DateInput.Date = DateTime.ParseExact(input.Date, "dd/MM/yyyy", null);
        TimeInput.Time = TimeSpan.Parse(input.Time);
        AddressInput.Text = input.Address;
    }
    public EditTask()
    {
        NewTask = true;
        InitializeComponent();
       
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        
        var ordernumber = OrderNumberInput.Text;
        if(ordernumber == null) {
            await DisplayAlert("Ошибка", "Введите номер заказа", "ОK");
        }
        else if(!NewTask)
        {
            TaskDatabase.SetModelToTask(ordernumber, ModelInput.Text);
            TaskDatabase.SetNoteToTask(ordernumber, NotesInput.Text);
            TaskDatabase.SetDateToTask(ordernumber, DateInput.Date.ToString("yyyy-MM-dd"));
            TaskDatabase.SetTimeToTask(ordernumber, TimeInput.Time.ToString());
            TaskDatabase.SetAddressToTask(ordernumber, AddressInput.Text);
            await Navigation.PopAsync();

        } else
        {
            TaskDatabase.CreateTask(new Task(ordernumber, AddressInput.Text, DateInput.Date.ToString("yyyy-MM-dd"), ModelInput.Text, NotesInput.Text, TimeInput.Time.ToString()));
            await Navigation.PopAsync();

        }

    }
}