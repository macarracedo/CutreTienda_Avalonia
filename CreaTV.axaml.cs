using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TiendaReparaciones;

namespace CutreTienda_Avalonia
{
    public partial class CreaTV : Window {
        
        public CreaTV(){
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var btAdd = this.FindControl<Button>("btAdd");
            
            btAdd.Click += (_, _) => this.ingresarDatos();
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
            
            var edModelo = this.FindControl<TextBox>("edModelo");
            
            var edHoras = this.FindControl<TextBox>("edHoras");
            int horas;
            int.TryParse(edHoras.Text.Trim(), out horas);
            
            var edMinutos = this.FindControl<TextBox>("edMinutos");
            int minutos;
            int.TryParse(edMinutos.Text.Trim(), out minutos);
            
            var edPulgadas = this.FindControl<TextBox>("edPulgadas");
            int pulgadas;
            int.TryParse(edPulgadas.Text.Trim(), out pulgadas);
            

            TimeSpan tiempoReparacion = new TimeSpan(horas, minutos, 0);
            Aparato televisor = FachadaAparatos.CrearTV(numSerie, edModelo.Text.Trim(), pulgadas);
            Reparacion rep = Reparacion.FactoryMethod(televisor,tiempoReparacion);
            Data.Lr.Add(rep);
            new MessageBox("Reparación agregada exitosamente. \n \n " + rep).ShowDialog( this );

        }
    }
}
