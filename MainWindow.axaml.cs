using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Xml.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TiendaReparaciones;

namespace CutreTienda_Avalonia
{
    public partial class MainWindow : Window
    {
        
        private const string DefaultPath = "reparaciones.xml";

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var opExit = this.FindControl<MenuItem>( "opExit" );
            var opSave = this.FindControl<MenuItem>("opSave");  
            var opLoad = this.FindControl<MenuItem>("opLoad"); 
            
            
            var btAdd = this.FindControl<Button>("btAdd");
            
            btAdd.Click += (_, _) => this.NuevaFactura();
            opExit.Click += (_, _) => this.Close();
            opSave.Click += (_, _) => this.OnGuardar();
            opLoad.Click += (_, _) => this.OnCargar();
        }
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }


        void OnGuardar()
        {
            if (Data.toXML(DefaultPath))
            {
                new MessageBox("\""+DefaultPath+"\" guardado exitosamente.").ShowDialog( this );
            }
            else
            {
                new MessageBox("Error: No se ha podido guardar \""+DefaultPath+"\".").ShowDialog( this );
            }
        }
        
        void OnCargar()
        {
            if (Data.fromXML(DefaultPath))
            {
                new MessageBox("\""+DefaultPath+"\" cargado exitosamente.").ShowDialog( this );
            }
            else
            {
                new MessageBox("Error: No se ha podido cargar \""+DefaultPath+"\"").ShowDialog( this );
            }
        }
        
        
        void NuevaFactura()
        {
            var cbOps = this.FindControl<ComboBox>( "cbAparato" );  
                
            switch ( cbOps.SelectedIndex ) {
                case 0:
                    new CreaRadio().ShowDialog( this );
                    break;
                case 1:
                    new CreaTV().ShowDialog( this );
                    break;
                case 2:
                    new CreaDVD().ShowDialog( this );
                    break;
                case 3:
                    new CreaTDT().ShowDialog( this );
                    break;
                default:
                    new MessageBox( "Error: Aparato desconocido." ).ShowDialog( this );
                    break;
            }
        }
        
    }
}