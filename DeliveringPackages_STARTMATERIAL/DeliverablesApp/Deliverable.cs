using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverablesApp
{

    public class Deliverable
    {
        public Deliverable(int id, int weight, string name, string street, int housenumber, string postalcode, string city)
        {
            ID = id;
            Weight = weight;
            Name = name;
            Street = street;
            Housenumber = housenumber;
            Postalcode = postalcode;
            City = city;
        }

        public int ID { get; }
        public int Weight { get; }
        public string Name { get; }
        public string Street { get; }
        public int Housenumber { get; }
        public string Postalcode { get; }
        public string City { get; }

        public override string ToString()
        {
            return "Id: " + Convert.ToString(ID) + ", weight: " + Convert.ToString(Weight) + ", should be delivered at: " +
                Name + " - " + Street + "  " + Housenumber + " - " + Postalcode + "  " + City;
        }

    }
}
