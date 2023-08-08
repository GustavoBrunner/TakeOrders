namespace Controller.Observer
{
    /// <summary>
    /// Interface observer, com ela podemos notificar os observadores de cada objeto
    /// como ela recebe como parâmetro um notification Type, pode ser enviado qualquer
    /// tipo de notificação, para ser filtrada dentro do método.
    /// </summary>
    public interface IObserver
    {
        void OnNotify<T>(NotificationTypes type, T value);
    }
}