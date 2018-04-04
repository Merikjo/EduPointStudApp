using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduPointStudApp.Models
{
    public class KurssiDataModel
    {
        public KurssiDataModel()
        {
            this.Kurssi = new HashSet<Kurssi>();
            this.Opiskelija = new HashSet<Opiskelija>();
        }

        public string Etunimi { get; set; }
        public string Sukunimi { get; set; }
        public string Opiskelijanro { get; set; }
        public int OpiskelijaID { get; set; }
        public string Tutkinto { get; set; }



        public int? OpettajaID { get; set; }
        public string EtunimiOpe { get; set; }
        public string SukunimiOpe { get; set; }
        public string Opettajanro { get; set; }


        public DateTime? KirjattuSisään { get; set; }
        public DateTime? KirjattuUlos { get; set; }
        public string Luokkakoodi { get; set; }

        public int RekisteriID { get; set; }

        public string Kurssinimi { get; set; }
        public string Kurssikoodi { get; set; }
        public string Luokka { get; set; }
        public int? KurssiID { get; set; }


        public int? LuokkaID { get; set; }
        public int? KirjattuID { get; set; }

        public virtual ICollection<Kurssi> Kurssi { get; set; }
        public virtual ICollection<Opiskelija> Opiskelija { get; set; }
        public virtual Opettaja Opettaja { get; set; }
    }
}