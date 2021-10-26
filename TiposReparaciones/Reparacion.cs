using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace TiendaReparaciones
{
    public abstract class Reparacion : Object
    {
        
        public const string Tag_HorasRep = "horas_rep";
        public const string Tag_MinutosRep = "minutos_rep";
        public const string Tag_Coste = "coste";
        public const string Tag_Reparacion = "reparacion";
        public const string Tag_Compleja = "compleja";
        public const string Tag_SustPiezas = "sust_piezas";

        
        protected Reparacion(Aparato a, TimeSpan t)
        {
            this.Duracion = t;
            this.Aparato = a;
        }

        public static Reparacion FactoryMethod(Aparato aparato, TimeSpan duracion)
        {
            if (duracion.CompareTo(new TimeSpan(1, 0, 0))<1)
            {
                return new SustPiezas(aparato, duracion);
            }
            else
            {
                return new Compleja(aparato, duracion);
            }
        }
        public Aparato Aparato { get; set; }
        public TimeSpan Duracion { get; set; }
        
        public override String ToString()
        {
            return "\nAparato: " + Aparato.ToString() + " \n \nDuracion: " + Duracion.ToString();
        }

        public virtual XElement toXML()
        {
            var raiz = new XElement(Tag_Reparacion);
            raiz.Add(new XElement(this.Aparato.toXML()));
            raiz.Add(new XElement(Tag_HorasRep, this.Duracion.Hours));
            raiz.Add(new XElement(Tag_MinutosRep, this.Duracion.Minutes));


            return raiz;
        }
    }
}