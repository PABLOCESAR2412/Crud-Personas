#pragma warning disable SYSLIB0014 // WebClient obsoleto: se usa por estilo del ejercicio
using System.Collections.Specialized;
using System.Net;
using MauiEstudiantes.Models;

namespace MauiEstudiantes.Views;

public partial class vActualizarEliminar : ContentPage
{
    private const string Url = "http://192.168.100.17/ws_estudiante/estudiante_ws.php";

    public vActualizarEliminar()
    {
        InitializeComponent();
    }

    public vActualizarEliminar(Estudiante estudiante) : this()
    {
        txtCodigo.Text = estudiante.codigo.ToString();
        txtNombre.Text = estudiante.nombre;
        txtApellido.Text = estudiante.apellido;
        txtEdad.Text = estudiante.edad.ToString();
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new NameValueCollection();
            parametros.Add("codigo", txtCodigo.Text);
            parametros.Add("nombre", txtNombre.Text);
            parametros.Add("apellido", txtApellido.Text);
            parametros.Add("edad", txtEdad.Text);
            await cliente.UploadValuesTaskAsync(Url, "PUT", parametros);
            await Navigation.PushAsync(new vEstudiante());
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Alerta", "Error " + ex.Message, "OK");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                await DisplayAlertAsync("Alerta", "Ingresa el código", "OK");
                return;
            }

            WebClient cliente = new WebClient();
            await cliente.UploadValuesTaskAsync($"{Url}?codigo={txtCodigo.Text}", "DELETE", new NameValueCollection());
            await Navigation.PushAsync(new vEstudiante());
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Alerta", "Error " + ex.Message, "OK");
        }
    }
}
