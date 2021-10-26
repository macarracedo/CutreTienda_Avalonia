using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TiendaReparaciones;

namespace CutreTienda_Avalonia
{
    public partial class MessageBox : Window {
        
        public MessageBox(string msg) : this(){
            var btText = this.FindControl<TextBlock>( "TbText" );

            btText.Text = msg;
        }
      
        public MessageBox(){
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            var btAccept = this.FindControl<Button>( "btAccept" );

            btAccept.Click += (_, _) => this.Close();
        }

        void InitializeComponent()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            AvaloniaXamlLoader.Load(this);
        }
    }
}
