using Application.DTO;
using Application.Interfaces;
using Domain.EntityModels;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TaskManagerPortfolio.Controllers;
using WebApi.Handlers;
using Xunit;

namespace WebApi.Tests
{
    public class EndpointTests
    {
        [Fact]
        public async Task CanAddToDoTask()
        {

            // Arrange
            var serviceMock = new Mock<IToDoTaskService>();
            serviceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                       .ReturnsAsync((ToDoTaskDTO)null);

            var task = new ToDoTaskDTO
            {
                Title = "Test task",
                Description = "Description test",
                Status = Status.Pending                
            };


            var controller = new ToDoTasksController(serviceMock.Object);

            // Act
            var result = await controller.Add(task);

            // Assert
            var created = Assert.IsType<CreatedAtActionResult>(result);
            var user = Assert.IsType<ToDoTaskDTO>(created.Value);
            Assert.Equal("Test task", user.Title);

        }
    }
}
