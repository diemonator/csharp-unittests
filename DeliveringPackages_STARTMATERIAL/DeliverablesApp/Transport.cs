using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DeliverablesApp
{
    public class Transport
    {
        private List<Deliverable> myDeliverables;

        public Transport()
        {
            myDeliverables = new List<Deliverable>();
        }

        public List<Deliverable> Deliverables { get { return myDeliverables; } }

        public void LoadDeliverablesFromFile(string filename)
        {
            StreamReader sr = null;
            try
            {
                int id, weight;
                string name, street;
                int housenr;
                string postalcode, city;
                sr = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read));
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    id = Convert.ToInt32(s);
                    weight = Convert.ToInt32(sr.ReadLine());
                    name = sr.ReadLine();
                    street = sr.ReadLine();
                    housenr = Convert.ToInt32(sr.ReadLine());
                    postalcode = sr.ReadLine();
                    city = sr.ReadLine();
                    myDeliverables.Add(new Deliverable(id, weight, name, street, housenr, postalcode, city));
                    s = sr.ReadLine();
                }
            }
            catch (IOException) { }
            finally
            {
                if (sr != null) sr.Close();
            }
        }

        public void AddDeliverable(Deliverable d)
        {
            if (FindDeliverable(d.ID) == null)
            {
                myDeliverables.Add(d);
            }
            else
            {
                throw new Exception("Be aware: nothing is added!!!");
            }
        }

        public Deliverable FindDeliverable(int id)
        {
            foreach (Deliverable d in this.myDeliverables)
            {
                if (d.ID == id)
                {
                    return d;
                }
            }
            return null;
        }

        public Deliverable FindHeaviestDeliverable()
        {
            if (myDeliverables.Count == 0)
                throw new Exception("There is no heaviest deliverable in an empty transport!");

            Deliverable heaviest = myDeliverables[0];

            foreach (Deliverable d in myDeliverables)
                if (d.Weight >= heaviest.Weight)
                    heaviest = d;

            // To do:
            // Was the conditional above a good solution?
            // Should it perhaps be instead one of the following:
            //  if (d.Weight >= heaviest.Weight)
            //  if (d.Weight < heaviest.Weight)
            //  if (d.Weight > heaviest.Weight)

            return heaviest;
        }

        /// <summary>
        /// SortByAddress sorts the elements of the deliverables-list "alfabetically by cityname, then by street; then by increasing house-number".
        /// So elements with the same cityname, but a different street, are sorted alfabetically by street.
        /// Elements with the same city and street, are sorted by increasing house-number. 
        /// </summary>
        public void SortByAddress()
        {
            // Not implemented yet
        }

    }
}
