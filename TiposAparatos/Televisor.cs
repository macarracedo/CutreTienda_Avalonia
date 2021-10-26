using System.Xml.Linq;

namespace TiendaReparaciones
{
    public class Televisor : Aparato
    {
        private const int precioHora = 10;
        public Televisor(int nSerie, string modelo, int pulgadas)
            :base(precioHora, nSerie, modelo)
        {
            this.Pulgadas=pulgadas;
        }
        public int Pulgadas { get; set; }
        
        public override string ToString()
        {
            return "Televeisor. \nPulgadas: "+Pulgadas+ base.ToString();
        }

        public override XElement toXML()
        {
            var raiz = base.toXML();
            raiz.Name = "televisor";
            raiz.Add(new XElement("pulgadas",this.Pulgadas));

            return raiz;
        }
    }
}