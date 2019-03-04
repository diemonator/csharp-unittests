using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeliverablesApp;

namespace MyFirstTestProject
{
    [TestClass]
    public class MyFirstUnitTest
    {

        [TestMethod]
        public void TestCreateADeliverable()
        {// testing to create a deliverable
            Deliverable d;
            d = new Deliverable(22, 350, "John", "Mainstreet", 2, "5688GE", "Eindhoven");

            Assert.AreEqual(d.ID, 22);
            Assert.AreEqual(d.Name, "John");
            Assert.AreEqual(d.Street, "Mainstreet");
            Assert.AreEqual(d.Housenumber, 2);
            Assert.AreEqual(d.Postalcode, "5688GE");
            Assert.AreEqual(d.City, "Eindhoven");
        }

        [TestMethod]
        public void TestCreateAnEmptyTransport()
        {// testing to create an empty transport (a transport with 0 deliverables)
            Transport t;
            t = new Transport();

            Assert.AreEqual(t.Deliverables.Count, 0);
        }

        [TestMethod]
        public void TestAddADeliverableToATransport()
        {// testing to  add a deliverablen to a transport
            Deliverable d1;
            d1 = new Deliverable(22, 350, "Josephine", "Nieuwstraat", 2, "5688GE", "Eindhoven");
            Transport t;
            t = new Transport();
            t.AddDeliverable(d1);

            Deliverable d2 = t.FindDeliverable(22);
            Assert.AreEqual(d2.Name, "Josephine");
            Assert.AreEqual(d2.Street, "Nieuwstraat");
        }

        [TestMethod]
        public void TestAdd2DeliverablesToATransport()
        {
            // arraing
            Deliverable d1 = new Deliverable(22, 350, "Josephine", "Nieuwstraat", 2, "5688GE", "Eindhoven");
            Deliverable d2 = new Deliverable(23, 450, "Josephine XX", "Oudestraat", 2, "5688HE", "Eindhoven");
            Transport transport = new Transport();
            // act
            transport.AddDeliverable(d1);
            transport.AddDeliverable(d2);
            // assert
            Assert.AreEqual(d1, transport.FindDeliverable(22));
            Assert.AreEqual(d2, transport.FindDeliverable(23));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestAddDeliverablesWithDuplicateIDToATransport()
        {
            // arraing
            Deliverable d1 = new Deliverable(22, 350, "Josephine", "Nieuwstraat", 2, "5688GE", "Eindhoven");
            Deliverable d2 = new Deliverable(22, 450, "Josephine XX", "Oudestraat", 2, "5688HE", "Eindhoven");
            Transport transport = new Transport();
            // act
            transport.AddDeliverable(d1);
            transport.AddDeliverable(d2);
        }

        [TestMethod]
        public void TestAddDeliverablesFromFile()
        {// testing to add deliverables from a file
            Transport t = new Transport();
            t.LoadDeliverablesFromFile("../../../data/deliverables.txt");

            Assert.AreEqual(t.Deliverables.Count, 15);

            Deliverable d; // test for some of the deliverables

            d = t.Deliverables[0];
            Assert.AreEqual(d.ID, 1);
            Assert.AreEqual(d.Weight, 350);
            Assert.AreEqual(d.Name, "Sven Kramer");

            d = t.Deliverables[1];
            Assert.AreEqual(d.ID, 2);
            Assert.AreEqual(d.Weight, 700);
            Assert.AreEqual(d.Name, "Adele");

            d = t.Deliverables[14];
            Assert.AreEqual(d.ID, 15);
            Assert.AreEqual(d.Weight, 600);
            Assert.AreEqual(d.Name, "Irene Wust");
        }

        [TestMethod]
        public void TestFindHeaviestInATransportOf1()
        {// testing to find heaviest deliverable in a transport of size 1
            Transport t;
            t = new Transport();

            Deliverable d;

            d = new Deliverable(22, 350, "Josephine", "Nieuwstraat", 2, "5688GE", "Eindhoven");
            t.AddDeliverable(d);

            d = t.FindHeaviestDeliverable();
            Assert.AreEqual(d.Weight, 350);
            Assert.AreEqual(d.ID, 22);
        }

        [TestMethod]
        public void TestFindHeaviestInATransportOf2()
        {// testing to find heaviest deliverable in a transport of size 1
            Transport t;
            t = new Transport();

            Deliverable d;

            d = new Deliverable(22, 350, "Josephine", "Nieuwstraat", 2, "5688GE", "Eindhoven");
            t.AddDeliverable(d);
            d = new Deliverable(35, 750, "Cleopatra", "Klaverstraat", 2, "5372MX", "Eindhoven");
            t.AddDeliverable(d);

            d = t.FindHeaviestDeliverable();
            Assert.AreEqual(d.Weight, 750);
            Assert.AreEqual(d.ID, 35);
        }

        [TestMethod]
        public void TestFindHeaviestInATransportOf2WithEqualWeights()
        {// testing to find heaviest deliverable in a transport of size 1
            Transport t = new Transport();
            int[] deliverables = new int[2] { 22, 35 };
            Deliverable d;

            d = new Deliverable(22, 350, "Josephine", "Nieuwstraat", 2, "5688GE", "Eindhoven");
            t.AddDeliverable(d);
            d = new Deliverable(35, 350, "Cleopatra", "Klaverstraat", 2, "5372MX", "Eindhoven");
            t.AddDeliverable(d);

            d = t.FindHeaviestDeliverable();
            List<int> ids = new List<int>();
            int max = 0;
            foreach (var item in t.Deliverables)
            {
                if (item.Weight >= max)
                { 
                    ids.Add(item.ID);
                    max = item.Weight;
                }
            }
            Assert.AreEqual(d.Weight, max);
            CollectionAssert.AreEquivalent(deliverables, ids);
        }

        [TestMethod]
        public void TestFindHeaviestDeliverableFromFile()
        {// testing to add deliverables from a file
            Transport t = new Transport();
            t.LoadDeliverablesFromFile("../../../data/deliverables.txt");

            Assert.AreEqual(t.Deliverables.Count, 15);

            Deliverable d = t.FindHeaviestDeliverable();
            t.Deliverables.Sort(new SortingClass());
            List<int> ids = new List<int>();
            int max = t.Deliverables[0].Weight;
            foreach (var item in t.Deliverables)
            {
                if (item.Weight == max)
                    ids.Add(item.ID);
            }
            Assert.AreEqual(d.Weight, max);
            CollectionAssert.Contains(ids,d.ID);
        }

        [TestMethod]
        public void TestSortByAddress()
        {// testing the SortByAddress-method
            Transport t = new Transport();
            t.LoadDeliverablesFromFile("../../../data/deliverables.txt");

            Assert.AreEqual(t.Deliverables.Count, 15);
            t.SortByAddress();
            Assert.AreEqual(t.Deliverables.Count, 15);

            int[] expectedids = { 3, 12, 10, 13, 11, 2, 15, 6, 4, 9, 14, 1, 5, 7, 8 };
            //for (int i = 0; i < 15; i++)
            //{
            //    // possible but harder
            //    Assert.AreEqual(expectedids[i], t.Deliverables[i].ID);
            //}
            List<int> ids = new List<int>();
            foreach (var item in t.Deliverables)
            {
                ids.Add(item.ID);
            }
            CollectionAssert.AreEquivalent(ids, expectedids);
        }
    }
}
