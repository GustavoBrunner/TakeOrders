namespace Controller.Observer
{
    /// <summary>
    /// Interface observer, com ela podemos notificar os observadores de cada objeto
    /// como ela recebe como par�metro um notification Type, pode ser enviado qualquer
    /// tipo de notifica��o, para ser filtrada dentro do m�todo.
    /// </summary>
    public interface IObserver
    {
        void OnNotify<T>(NotificationTypes type, T value);
    }
}