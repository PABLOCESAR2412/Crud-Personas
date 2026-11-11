#pragma warning disable SYSLIB0014 // WebClient obsoleto: se usa por estilo del ejercicio
using System.Collections.Specialized;
using System.Net;

namespace MauiEstudiantes.Views;

public partial class vAgregarEstudiante : ContentPage
{
    private const string Url = "http://192.168.100.17/ws_estudiante/estudiante_ws.php";

    public vAgregarEstudiante()
    {
        InitializeComponent();
    }

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new NameValueCollection();
            parametros.Add("nombre", txtNombre.Text);
            parametros.Add("apellido", txtApellido.Text);
            parametros.Add("edad", txtEdad.Text);
            await cliente.UploadValuesTaskAsync(Url, "POST", parametros);
            await Navigation.PushAsync(new vEstudiante());
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Alerta", "Error " + ex.Message, "OK");
        }
    }

    private async void btnCancelar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vEstudiante());
    }
}
