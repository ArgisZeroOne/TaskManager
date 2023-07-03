namespace Something;

public partial class EditTask : ContentPage
{
    public Task inputTask { get; set; }
    private bool NewTask = false;
    private TaskDatabase db = new TaskDatabase();
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
            db.SetModelToTask(ordernumber, ModelInput.Text);
            db.SetNoteToTask(ordernumber, NotesInput.Text);
            db.SetDateToTask(ordernumber, DateInput.Date.ToString("yyyy-MM-dd"));
            db.SetTimeToTask(ordernumber, TimeInput.Time.ToString());
            db.SetAddressToTask(ordernumber, AddressInput.Text);
            await Navigation.PopAsync();

        } else
        {
            db.CreateTask(new Task(ordernumber, AddressInput.Text, DateInput.Date.ToString("yyyy-MM-dd"), ModelInput.Text, NotesInput.Text, TimeInput.Time.ToString()));
            await Navigation.PopAsync();

        }

    }
}