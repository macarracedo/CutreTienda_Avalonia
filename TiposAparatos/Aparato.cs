using System.Runtime.CompilerServices;
using System.Xml.Linq;
using CutreTienda_Avalonia;

namespace TiendaReparaciones
{
    
    public abstract class Aparato
    {
        public const string Tag_Radio = "radio";
        public const string Tag_Televisor = "televisor";
        public const string Tag_AdaptTDT = "adapt_tdt";
        public const string Tag_RepDVD = "rep_dvd";
        public const string Tag_Pulgadas = "pulgadas";
        public const string Tag_Banda = "banda";
        public const string Tag_NumSerie = "num_serie";
        public const string Tag_Modelo = "modelo";
        public const string Tag_Graba = "graba";
        public const string Tag_BlueRay = "blueray";
        public const string Tag_HorasGrab = "horas_grab";
        public const string Tag_MinutosGrab = "minutos_grab";
        public const string Tag_Aparato = "aparato"; 
        
        /// <summary>
     /// Constructor de Aparato
     /// </summary>
     /// <param name="precioHora"> Registra el precio por hora</param>
     /// <param name="nSerie"> Registra el numero de serie</param>
     /// <param name="modelo"> Registra el modelo</param>
        public Aparato (int precioHora, int nSerie, string modelo)
            {
                this.PrecioHora=precioHora;
                this.NSerie=nSerie;
                this.Modelo=modelo;
            }
            public int PrecioHora { get; set; }
            public int NSerie { get; }
            public string Modelo { get; }

            public override string ToString()
            {
                return " \nNum. de serie: "+NSerie+" \nModelo: "+Modelo ;
            }

            public virtual XElement toXML()
            {
                var raiz = new XElement(Tag_Aparato);
                raiz.Add(new XElement(Tag_NumSerie,this.NSerie));
                raiz.Add(new XElement(Tag_Modelo,this.Modelo));

                return raiz;
            }
    }
}