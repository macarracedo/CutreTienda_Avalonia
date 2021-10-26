using System;

namespace TiendaReparaciones
{
    public static class FachadaAparatos
    {
        public static Radio CrearRadio(int nSerie, string modelo, Bandas banda)
        {
            return new Radio(nSerie, modelo, banda);
        }

        public static Televisor CrearTV(int nSerie, string modelo, int pulgadas)
        {
            return new Televisor(nSerie, modelo, pulgadas);
        }
        
        public static RepDVD CrearDVD(int nSerie, string modelo, bool blueray, bool graba, TimeSpan tiempoGrabacion)
        {
            return new RepDVD(nSerie, modelo, blueray, graba,tiempoGrabacion);
        }
        
        public static AdapTDT CrearTDT(int nSerie, string modelo, bool graba, TimeSpan tiempoGrabacion)
        {
            var tdt =  new AdapTDT(nSerie, modelo, graba, tiempoGrabacion);
            return tdt;
        }
    }
}