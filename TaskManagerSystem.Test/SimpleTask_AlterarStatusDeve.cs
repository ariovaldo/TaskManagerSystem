using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Test
{
    public class SimpleTask_AlterarStatusDeve
    {
        [Theory]
        [InlineData(TaskStatusEnum.Todo, TaskStatusEnum.Todo)]
        [InlineData(TaskStatusEnum.Doing, TaskStatusEnum.Doing)]
        [InlineData(TaskStatusEnum.Done, TaskStatusEnum.Done)]
        public void AtualizaStatusParaOsValoresCorretos(TaskStatusEnum statusDesejado, TaskStatusEnum statusAlterado)
        {
            //arrange
            var task = new SimpleTask("task1", "description task1", DateTime.Now);

            //act
            task.AlterarStatus(statusAlterado);

            //assert
            Assert.Equal(statusDesejado, task.Status);
        }
    }
}