using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TiendaReparaciones;

namespace CutreTienda_Avalonia
{
    public partial class CreaRadio : Window {
        
        public CreaRadio(){
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
            
            Bandas banda = leerBandas();
            

            TimeSpan tiempoReparacion = new TimeSpan(horas, minutos, 0);

            Aparato radio = FachadaAparatos.CrearRadio(numSerie, edModelo.Text.Trim(), banda);
            Reparacion rep = Reparacion.FactoryMethod(radio, tiempoReparacion);
            
            Data.Lr.Add(rep);
            new MessageBox("Reparación agregada exitosamente. \n \n " + rep).ShowDialog( this );
            

        }
        
        private Bandas leerBandas()
        {
            var edBanda = this.FindControl<ComboBox>("edBanda");
            Bandas toRet = Bandas.ambas;
            switch (edBanda.SelectedIndex)
            {
                case 0:     //AM
                    toRet = Bandas.AM;
                    break;
                case 1:     //FM
                    toRet = Bandas.FM;
                    break;
                case 2:     //Ambas
                    toRet = Bandas.ambas;
                    break;
            }
            
            return toRet;
        }

        
    }
}
