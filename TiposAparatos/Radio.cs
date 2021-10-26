using System;
using System.Xml.Linq;

namespace TiendaReparaciones
{
    public enum Bandas { AM, FM, ambas }
    public class Radio : Aparato
    {
        private const int precioHora = 5;
        public Radio( int nSerie, string modelo, Bandas banda)
            :base(precioHora, nSerie, modelo)
        {
            this.Frecuencias=banda;
        }
        public Bandas Frecuencias { get; set; }

        public override string ToString()
        {
            return "Radio: " + base.ToString() + " \nBanda: " + this.Frecuencias;
        }

        public override XElement toXML()
        {
            var raiz = base.toXML();
            raiz.Name = Tag_Radio;
            raiz.Add(new XElement(Tag_Banda,this.Frecuencias));
            return raiz;
        }
    }
}