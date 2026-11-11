using System.Collections.ObjectModel;
using MauiEstudiantes.Models;
using Newtonsoft.Json;

namespace MauiEstudiantes.Views;

public partial class vEstudiante : ContentPage
{
    private readonly HttpClient _cliente = new HttpClient();
    private const string Url = "http://192.168.100.17/ws_estudiante/estudiante_ws.php";
    private ObservableCollection<Estudiante> _estudiante = new ObservableCollection<Estudiante>();

    public vEstudiante()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await MostrarEstudiante();
    }

    public async Task MostrarEstudiante()
    {
        var content = await _cliente.GetStringAsync(Url);
        List<Estudiante> _mostrar = JsonConvert.DeserializeObject<List<Estudiante>>(content) ?? new List<Estudiante>();
        _estudiante = new ObservableCollection<Estudiante>(_mostrar);
        listaEstudiante.ItemsSource = _estudiante;
    }

    private async void btnAgregarEstudiante_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vAgregarEstudiante());
    }

    private async void listaEstudiante_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Estudiante estudiante)
        {
            listaEstudiante.SelectedItem = null;
            await Navigation.PushAsync(new vActualizarEliminar(estudiante));
        }
    }
}
