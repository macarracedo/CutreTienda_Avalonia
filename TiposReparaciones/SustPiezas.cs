using System;
using System.Xml.Linq;

namespace TiendaReparaciones
{
    public class SustPiezas : Reparacion
    {
        public SustPiezas(Aparato a, TimeSpan t) : base(a, t)
        {
            
        }

        public double Coste()
        {
            double toRet = 0;
            //Sustitución de piezas 
            double precio = this.Aparato.PrecioHora;
            int mediasHoras = (int) this.Duracion.TotalMinutes / 30;
            Console.WriteLine("Sust. de piezas. Medias horas: " + mediasHoras);
            toRet += precio * mediasHoras;
            return toRet;
        }

        public override string ToString()
        {
            return "Sustitucion de piezas : " + base.ToString() + " \nCoste: " + this.Coste();
        }

        public override XElement toXML()
        {
            var raiz = base.toXML();
            raiz.Name = Tag_SustPiezas;
            raiz.Add(new XElement(Tag_Coste,this.Coste()));
            
            return raiz;
        }
    }
}