using System;
using System.Xml.Linq;

namespace TiendaReparaciones
{
    public class AdapTDT : Aparato
    {
        private const int precioHora = 5;
        public AdapTDT(int nSerie, string modelo, bool graba, TimeSpan tiempoGrabacion)
            :base(precioHora, nSerie, modelo)
        {
            this.Graba=graba;
            this.RecTime=tiempoGrabacion;
        }
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
            return "Adapt. de TDT: " + base.ToString() + "\nGraba: "+this.Graba + "\nTiempo de Grabacion: " + this.RecTime;
        }

        /// <summary>
        /// Esto hereda de <see cref="Aparato"/> con la información
        /// </summary>
        /// <returns></returns>
        public override XElement toXML()
        {
            var raiz = base.toXML();
            raiz.Name = Tag_AdaptTDT;
            raiz.Add(new XElement(Tag_Graba, this.Graba));
            raiz.Add(new XElement(Tag_HorasGrab, this.RecTime.Hours));
            raiz.Add(new XElement(Tag_MinutosGrab, this.RecTime.Minutes));

            return raiz;
        }
    }
}