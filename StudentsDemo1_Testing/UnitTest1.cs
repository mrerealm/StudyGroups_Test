using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentsDemo1.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace StudentsDemo1_Testing
{
    [TestClass]
    public class UnitTest1
    {
        private string GetInput1()
        {
            return @"{
                { '','','','',''},
                { '','', '','',''},
                { '','','','',''},
                { '','','','',''},
                { '','','', '',''},
                { '','','','',''}
                }";
        }

        private string GetInput2()
        {
            return @"{
                { '','','Simon','',''},
                { '','Sergey', '','Thomas',''},
                { '','','','',''},
                { '','Chris','','',''},
                { '','Harry','', 'Roger',''},
                { '','','','',''}
                }";
        }

        private string NeighboursOutput1 = "{{'Simon','Sergey','Thomas'},{'Chris','Harry'},{'Roger'}}";

        private List<HashSet<string>> GetGroup1()
        {
            var lists = new List<HashSet<string>>();

            var set1 = new HashSet<string>();
            set1.Add("Simon");
            set1.Add("Sergey");
            set1.Add("Thomas");
            lists.Add(set1);

            var set2 = new HashSet<string>();
            set2.Add("Chris");
            set2.Add("Harry");
            lists.Add(set2);

            var set3 = new HashSet<string>();
            set3.Add("Roger");
            lists.Add(set3);

            return lists;
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Invalid input format.")]
        public void StudentRank_Deserialize_Null()
        {
            // Arrange
            var input = GetInput1();

            // Act
            // deserialise string array into model
            var list = StudentRank.Deserialize(null);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Invalid input format.")]
        public void StudentRank_Deserialize_EmptyString()
        {
            // Arrange
            var input = GetInput1();

            // Act
            // deserialise string array into model
            var list = StudentRank.Deserialize(string.Empty);

            // Assert
        }

        [TestMethod]
        public void StudentRank_Deserialize_EmptyEntries()
        {
            // Arrange
            var input = GetInput1();

            // Act
            // deserialise string array into model
            var list = StudentRank.Deserialize(input);

            // Assert
            Assert.IsNotNull(list);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void StudentRank_Deserialize_Input_Sample2()
        {
            // Arrange
            var input = GetInput2();

            // Act
            // deserialise string array into model
            var list = StudentRank.Deserialize(input);

            // Assert
            Assert.IsNotNull(list);
            Assert.AreEqual(6, list.Count);
        }

        [TestMethod]
        public void StudentRank_Serialize_Group1()
        {
            // Arrange
            var input = GetGroup1();

            // Act
            // deserialise string array into model
            var output = StudentRank.Serialize(input);

            // Assert
            Assert.IsNotNull(output);
            Assert.AreEqual(NeighboursOutput1, output);
        }

        [TestMethod]
        public void StudentRank_IsInGroup()
        {
            // Arrange
            StudentRank.NofCols = 5;
            var simon = new StudentRank { Position = 2, Name = "Simon" };
            var sergey = new StudentRank { Position = 6, Name = "Sergey" };
            var thomas = new StudentRank { Position = 8, Name = "Thomas" };
            var chris = new StudentRank { Position = 16, Name = "Chris" };
            var harry = new StudentRank { Position = 16, Name = "Harry" };

            // Act

            // Assert
            Assert.IsTrue(simon.IsInGroup(sergey));
            Assert.IsTrue(simon.IsInGroup(thomas));
            Assert.IsTrue(chris.IsInGroup(harry));
        }

        [TestMethod]
        public void StudentRank_IsNotInGroup()
        {
            // Arrange
            var simon = new StudentRank { Position = 2, Name = "Simon" };
            var sergey = new StudentRank { Position = 6, Name = "Sergey" };
            var thomas = new StudentRank { Position = 8, Name = "Thomas" };
            var chris = new StudentRank { Position = 16, Name = "Chris" };
            var harry = new StudentRank { Position = 16, Name = "Harry" };
            var roger = new StudentRank { Position = 18, Name = "Roger" };

            // Act

            // Assert
            Assert.IsFalse(simon.IsInGroup(chris));
            Assert.IsFalse(harry.IsInGroup(roger));
        }
    }
}
