using System;
using System.Xml.Linq;
using Avalonia.Controls;

namespace TiendaReparaciones
{
    public class RepDVD : Aparato
    {
        private const int precioHora = 10;
        public RepDVD( int nSerie, string modelo, bool blueRay, bool graba, TimeSpan tiempoGrabacion)
            :base(precioHora, nSerie, modelo)
        {
            this.BlueRay=blueRay;
            this.Graba=graba;
            this.RecTime=tiempoGrabacion;
        }
        public bool BlueRay { get; set; }
        public bool Graba { get; set; }

        private TimeSpan recTime;
        public TimeSpan RecTime
        {
            get
            {
                return this.recTime;
            }
            set
            {
                if (Graba)
                {
                    this.recTime = value;
                }
                else
                {
                    this.recTime = new TimeSpan(0, 0, 0);
                }
            }
        }

        public override string ToString()
        {
            return "Rep. de DVD: " + base.ToString() + " \nBlueray: "+this.BlueRay+" \nGraba:"+this.Graba+
                   "\nTiempo de grabacion:"+this.RecTime;
        }

        public override XElement toXML()
        {
            var raiz = base.toXML();
            raiz.Name = Tag_RepDVD;
            raiz.Add(new XElement(Tag_BlueRay, this.BlueRay));
            raiz.Add(new XElement(Tag_Graba, this.Graba));
            raiz.Add(new XElement(Tag_HorasGrab, this.RecTime.Hours));
            raiz.Add(new XElement(Tag_MinutosGrab, this.RecTime.Minutes));

            return raiz;
        }
    }
}