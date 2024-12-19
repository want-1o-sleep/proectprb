using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
namespace TestProject1
{
    [TestClass]
    public class UnitTestPage6
    {
        [TestMethod]
        public void TestPage6Initialization()
        {
            // Arrange
            Page6 page = new Page6();

            // Act
            page.Show(); // Показываем страницу
            page.NavigationService.Navigate(new Page7()); // Переход на другую страницу
            page.Close(); // Закрываем страницу

            // Assert
            Assert.IsFalse(page.IsVisible); // Проверяем, что страница больше не отображается
        }
    }
}