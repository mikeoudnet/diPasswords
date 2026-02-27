namespace diPasswords.Application.Interfaces
{
    // Отключение элементов при необходимости и изменение их параметров
    // в зависимости от текущего состояния других элементов
    public interface IElementController<T>
    {
        void Switch(bool? flag = null); // Включить/отключить элемент
        void Retext(bool? flag = null); // Изменить текст элемента
    }
}
