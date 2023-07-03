using Medication.Domain.Exceptions;

namespace Medication.Domain.Tests
{
    public class MedicationTests
    {
        [Fact]
        public void CreateMedicationWithSucces()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Fake medication name";
            var quantity = 1;
            var createdDate = DateTime.Now;

            // Act
            var sut = new Medication(id, name, quantity, createdDate);

            // Assert
            Assert.Equal(id, sut.Id);
            Assert.Equal(name, sut.Name);
            Assert.Equal(quantity, sut.Quantity);
            Assert.Equal(createdDate, sut.CreatedAt);

        }

        [Fact]
        public void CreateMedicationWithEmptyIdShouldThrowException()
        {
            // Arrange
            var id = Guid.Empty;
            var name = "Fake medication name";
            var quantity = 1;
            var createdDate = DateTime.Now;

            // Act
            // Assert
            Assert.Throws<MedicationException>(() => new Medication(id, name, quantity, createdDate));
        }

        [Fact]
        public void CreateMedicationWithInvalidQuantityShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Fake medication name";
            var quantity = 0;
            var createdDate = DateTime.Now;

            // Act
            // Assert
            Assert.Throws<MedicationException>(() => new Medication(id, name, quantity, createdDate));
        }

        [Fact]
        public void CreateMedicationWithInvalidEmptyNameShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = string.Empty;
            var quantity = 1;
            var createdDate = DateTime.Now;

            // Act
            // Assert
            Assert.Throws<MedicationException>(() => new Medication(id, name, quantity, createdDate));
        }

        [Fact]
        public void CreateMedicationWithInvalidWhiteSpaceNameShouldThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = " ";
            var quantity = 1;
            var createdDate = DateTime.Now;

            // Act
            // Assert
            Assert.Throws<MedicationException>(() => new Medication(id, name, quantity, createdDate));
        }
    }
}
