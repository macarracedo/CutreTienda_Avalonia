using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using CutreTienda_Avalonia;
using JetBrains.Annotations;

namespace TiendaReparaciones
{
    
    public static class Data
    {
        static Data()
        {
            Lr = new List<Reparacion>();
        }
        public static List<Reparacion> Lr;
        
       [ItemNotNull]
        
        public static Boolean toXML(string path)
        {
            Boolean toRet = false;
            var raiz = new XDocument();
            var reparaciones = new XElement("reparaciones");
            
            foreach (var rep in Lr)
            {
                reparaciones.Add(rep.toXML());
            }
            raiz.Add(reparaciones);
            raiz.Save(path);
            toRet = true;
            return toRet;
        }

        public static Boolean fromXML(string path)
        {
            Boolean toRet = false;
            var doc = XDocument.Load(path);
            var root = doc.Root.Elements();
            IEnumerable<XElement> tipo;
            Aparato ap = null;
            Reparacion rep;
            int numSerie = 0;
            int pulgadas = 0;
            string modelo = "";
            bool graba = false;
            bool blueray = false;
            Bandas banda = Bandas.ambas;
            int horasGrab = 0;
            int minutosGrab = 0;
            int horasRep = 0;
            int minutosRep = 0;
            
            foreach (XElement reparacion in root)
            {
                switch (reparacion.Name.ToString()) //Escoge entre compleja y sustitucion de piezas
                {
                    case Reparacion.Tag_Compleja:
                        foreach (var subNodo in reparacion.Elements())
                        {
                            switch (subNodo.Name.ToString()) //Escoge entre el tipo de aparato, la duracion y el coste
                            {
                                case Aparato.Tag_Radio:
                                    tipo = subNodo.Elements();
                                    foreach (var elem in tipo)
                                    {
                                        switch (elem.Name.ToString())
                                        {
                                            case Aparato.Tag_NumSerie:
                                                int.TryParse(elem.Value, out numSerie);
                                                break;
                                            case Aparato.Tag_Modelo:
                                                modelo = elem.Value;
                                                break;
                                            case Aparato.Tag_Banda:
                                                banda = leerBandas(elem.Value);
                                                break;
                                        }
                                    
                                    }

                                    ap = FachadaAparatos.CrearRadio(numSerie, modelo, banda);
                                    break;
                                case Aparato.Tag_Televisor:
                                    tipo = subNodo.Elements();
                                    foreach (var elem in tipo)
                                    {
                                        switch (elem.Name.ToString())
                                        {
                                            case Aparato.Tag_NumSerie:
                                                int.TryParse(elem.Value, out numSerie);
                                                break;
                                            case Aparato.Tag_Modelo:
                                                modelo = elem.Value;
                                                break;
                                            case Aparato.Tag_Pulgadas:
                                                int.TryParse(elem.Value, out pulgadas);
                                                break;
                                        }
                                    
                                    }

                                    ap = FachadaAparatos.CrearTV(numSerie, modelo, pulgadas);
                                    break;
                                case Aparato.Tag_AdaptTDT:
                                    tipo = subNodo.Elements();
                                    foreach (var elem in tipo)
                                    {
                                        switch (elem.Name.ToString())
                                        {
                                            case Aparato.Tag_NumSerie:
                                                int.TryParse(elem.Value, out numSerie);
                                                break;
                                            case Aparato.Tag_Modelo:
                                                modelo = elem.Value;
                                                break;
                                            case Aparato.Tag_Graba:
                                                bool.TryParse(elem.Value, out graba);
                                                break;
                                            case Aparato.Tag_HorasGrab:
                                                int.TryParse(elem.Value, out horasGrab);
                                                break;
                                            case Aparato.Tag_MinutosGrab:
                                                int.TryParse(elem.Value, out minutosGrab);
                                                break;
                                        }
                                    
                                    }

                                    ap = FachadaAparatos.CrearTDT(numSerie, modelo, graba,
                                        new TimeSpan(horasGrab, minutosGrab, 0));
                                    break;
                                case Aparato.Tag_RepDVD:
                                    tipo = subNodo.Elements();
                                    foreach (var elem in tipo)
                                    {
                                        switch (elem.Name.ToString())
                                        {
                                            case Aparato.Tag_NumSerie:
                                                int.TryParse(elem.Value, out numSerie);
                                                break;
                                            case Aparato.Tag_Modelo:
                                                modelo = elem.Value;
                                                break;
                                            case Aparato.Tag_BlueRay:
                                                bool.TryParse(elem.Value, out blueray);
                                                break;
                                            case Aparato.Tag_Graba:
                                                bool.TryParse(elem.Value, out graba);
                                                break;
                                            case Aparato.Tag_HorasGrab:
                                                int.TryParse(elem.Value, out horasGrab);
                                                break;
                                            case Aparato.Tag_MinutosGrab:
                                                int.TryParse(elem.Value, out minutosGrab);
                                                break;
                                        }
                                    
                                    }
                                    ap = FachadaAparatos.CrearDVD(numSerie, modelo, blueray, graba,
                                        new TimeSpan(horasGrab, minutosGrab, 0));
                                    break;
                                case Reparacion.Tag_HorasRep:
                                    int.TryParse(subNodo.Value, out horasRep);
                                    break;
                                case Reparacion.Tag_MinutosRep:
                                    int.TryParse(subNodo.Value, out minutosRep);
                                    break;
                            }
                            
                        }
                        Lr.Add(new Compleja(ap, new TimeSpan(horasRep, minutosRep, 0)));
                        break;
                    case Reparacion.Tag_SustPiezas:
                        foreach (var subNodo in reparacion.Elements())
                        {
                            switch (subNodo.Name.ToString())
                            {
                                case Aparato.Tag_Radio:
                                    tipo = subNodo.Elements();
                                    foreach (var elem in tipo)
                                    {
                                        switch (elem.Name.ToString())
                                        {
                                            case Aparato.Tag_NumSerie:
                                                int.TryParse(elem.Value, out numSerie);
                                                break;
                                            case Aparato.Tag_Modelo:
                                                modelo = elem.Value;
                                                break;
                                            case Aparato.Tag_Banda:
                                                banda = leerBandas(elem.Value);
                                                break;
                                        }
                                    
                                    }

                                    ap = FachadaAparatos.CrearRadio(numSerie, modelo, banda);
                                    break;
                                case Aparato.Tag_Televisor:
                                    tipo = subNodo.Elements();
                                    foreach (var elem in tipo)
                                    {
                                        switch (elem.Name.ToString())
                                        {
                                            case Aparato.Tag_NumSerie:
                                                int.TryParse(elem.Value, out numSerie);
                                                break;
                                            case Aparato.Tag_Modelo:
                                                modelo = elem.Value;
                                                break;
                                            case Aparato.Tag_Pulgadas:
                                                int.TryParse(elem.Value, out pulgadas);
                                                break;
                                        }
                                    
                                    }

                                    ap = FachadaAparatos.CrearTV(numSerie, modelo, pulgadas);
                                    break;
                                case Aparato.Tag_AdaptTDT:
                                    tipo = subNodo.Elements();
                                    foreach (var elem in tipo)
                                    {
                                        switch (elem.Name.ToString())
                                        {
                                            case Aparato.Tag_NumSerie:
                                                int.TryParse(elem.Value, out numSerie);
                                                break;
                                            case Aparato.Tag_Modelo:
                                                modelo = elem.Value;
                                                break;
                                            case Aparato.Tag_Graba:
                                                bool.TryParse(elem.Value, out graba);
                                                break;
                                            case Aparato.Tag_HorasGrab:
                                                int.TryParse(elem.Value, out horasGrab);
                                                break;
                                            case Aparato.Tag_MinutosGrab:
                                                int.TryParse(elem.Value, out minutosGrab);
                                                break;
                                        }
                                    
                                    }

                                    ap = FachadaAparatos.CrearTDT(numSerie, modelo, graba,
                                        new TimeSpan(horasGrab, minutosGrab, 0));
                                    break;
                                case Aparato.Tag_RepDVD:
                                    tipo = subNodo.Elements();
                                    foreach (var elem in tipo)
                                    {
                                        switch (elem.Name.ToString())
                                        {
                                            case Aparato.Tag_NumSerie:
                                                int.TryParse(elem.Value, out numSerie);
                                                break;
                                            case Aparato.Tag_Modelo:
                                                modelo = elem.Value;
                                                break;
                                            case Aparato.Tag_BlueRay:
                                                bool.TryParse(elem.Value, out blueray);
                                                break;
                                            case Aparato.Tag_Graba:
                                                bool.TryParse(elem.Value, out graba);
                                                break;
                                            case Aparato.Tag_HorasGrab:
                                                int.TryParse(elem.Value, out horasGrab);
                                                break;
                                            case Aparato.Tag_MinutosGrab:
                                                int.TryParse(elem.Value, out minutosGrab);
                                                break;
                                        }
                                    
                                    }
                                    ap = FachadaAparatos.CrearDVD(numSerie, modelo, blueray, graba,
                                        new TimeSpan(horasGrab, minutosGrab, 0));
                                    break;
                                case Reparacion.Tag_HorasRep:
                                    int.TryParse(subNodo.Value, out horasRep);
                                    break;
                                case Reparacion.Tag_MinutosRep:
                                    int.TryParse(subNodo.Value, out minutosRep);
                                    break;
                            }
                            
                        }
                        Lr.Add(new SustPiezas(ap, new TimeSpan(horasRep, minutosRep, 0)));
                        break;
                } 
            }
            toRet = true;

            return toRet;
        }
        private static Bandas leerBandas(string frecuencias)
        {
            Bandas toRet = Bandas.ambas;
            switch (frecuencias.Trim().ToUpper())
            {
                case "AM":     //AM
                    toRet = Bandas.AM;
                    break;
                case "FM":     //FM
                    toRet = Bandas.FM;
                    break;
                case "AMBAS":     //Ambas
                    toRet = Bandas.ambas;
                    break;
            }
            
            return toRet;
        }
        
    }
}