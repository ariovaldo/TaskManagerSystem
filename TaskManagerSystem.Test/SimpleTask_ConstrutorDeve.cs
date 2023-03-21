using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Test
{
    public class SimpleTask_ConstrutorDeve
    {
        [Fact]
        public void CriarStatusComComOValorTodoPorDefault()
        {
            //arrange
            var task = new SimpleTask("task1", "description task1", DateTime.Now);

            //act

            //assert
            Assert.Equal(TaskStatusEnum.Todo, task.Status);
        }


        [Fact]
        public void NotificarErroQuandoTituloForVazio()
        {
            //arrange
            string titulo = string.Empty;

            //act
            var ex = Assert.Throws<ArgumentNullException>(() => new SimpleTask(titulo));

            //assert
            Assert.Equal("Value cannot be null. (Parameter 'title')", ex?.Message ?? string.Empty);
        }
    }
}