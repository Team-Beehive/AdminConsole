using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections;
using AdminDatabaseFramework;
using System.Collections.Generic;

namespace ProfessorSortingTest
{
    [TestClass]
    public class ProfessorSortTests
    {
        [TestMethod]
        public void CompareTwoStrings()
        {
            string first = "Apple";
            string second = "Pear";
            
            Assert.IsTrue(first.CompareTo(second) < 0);
        }

        [TestMethod]
        public void SplitStringAfterSpace()
        {
            string name = "First Last";
            string expected = "Last";
            string split = name.Split(' ').Last();

            Assert.AreEqual(expected, split);
        }

        [TestMethod]
        public void SortSimpleList()
        {
            ProfessorData Last = new ProfessorData() { professorName = "Test ZZ" };
            ProfessorData First = new ProfessorData() { professorName = "Test AA" };
            ProfessorData Middle = new ProfessorData() { professorName = "Test MM" };

            LinkedList<ProfessorData> testData = new LinkedList<ProfessorData>();

            testData.AddLast(Last);
            testData.AddLast(First);
            testData.AddLast(Middle);

            testData = ObjectFunctions.SortByLastName(testData);


            Assert.AreEqual(testData.ToList()[0].professorName, First.professorName);
            Assert.AreEqual(testData.ToList()[1].professorName, Middle.professorName);
            Assert.AreEqual(testData.ToList()[2].professorName, Last.professorName);
        }
    }
}
