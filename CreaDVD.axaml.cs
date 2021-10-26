using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TiendaReparaciones;

namespace CutreTienda_Avalonia
{
    public partial class CreaDVD : Window {
        
        public CreaDVD(){
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var aceptar = this.FindControl<Button>("aceptar");
            
            aceptar.Click += (_, _) => this.ingresarDatos();
        }

        void InitializeComponent()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            AvaloniaXamlLoader.Load(this);
        }

        void ingresarDatos()
        {
            var edNumSerie = this.FindControl<TextBox>("edNumSerie");
            int numSerie;
            int.TryParse(edNumSerie.Text.Trim(), out numSerie);
            
            var modelo = this.FindControl<TextBox>("modelo");
            
            var horas = this.FindControl<TextBox>("edHoras");
            int Horas;
            int.TryParse(horas.Text.Trim(), out Horas);
            
            var minutos = this.FindControl<TextBox>("edMinutos");
            int Minutos;
            int.TryParse(minutos.Text.Trim(), out Minutos);
            
            var edGraba = this.FindControl<CheckBox>("cbGraba");
            bool graba = edGraba.IsChecked.Value;
            
            var cbBlueRay = this.FindControl<CheckBox>("cbBlueRay");
            bool blueRay = cbBlueRay.IsChecked.Value;
            
            
            var horasG = this.FindControl<TextBox>("horasG");
            int HorasG;
            int.TryParse(horasG.Text.Trim(), out HorasG);
            
            var minutosG = this.FindControl<TextBox>("minutosG");
            int MinutosG;
            int.TryParse(minutosG.Text.Trim(), out MinutosG);

            TimeSpan tiempoReparacion = new TimeSpan(Horas, Minutos, 0);
            TimeSpan tiempoGrabacion = new TimeSpan(HorasG, MinutosG, 0);

            Aparato repDVD = FachadaAparatos.CrearDVD(numSerie, modelo.Text.Trim(), blueRay, graba, tiempoGrabacion);
            Reparacion rep = Reparacion.FactoryMethod(repDVD, tiempoReparacion);
            
            Data.Lr.Add(rep);
            new MessageBox("Reparación agregada exitosamente. \n \n " + rep).ShowDialog( this );

        }
    }
}
