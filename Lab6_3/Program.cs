using System;
using System.Net;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string login = "is-11-22"; //Логін у Moodle
        string name = "Olexandr"; // Ім'я
        string surname = "Poberezhniy"; // Прізвище
        int course = 2; // Курс
        string group = "IS-11"; //Група

        // Створення HTTPListener для прийому запитів
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/"); // Порт та шлях до сервера
        listener.Start();
        Console.WriteLine("HTTP Server started. Listening for requests...");

        // Обробка запитів
        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            // Перевірка запиту нашого логіну
            if (request.RawUrl.Contains(login))
            {
                // Формування відповіді
                string responseData = $"Name: {name}\nSurname: {surname}\nCourse: {course}\nGroup: {group}";
                byte[] buffer = Encoding.UTF8.GetBytes(responseData);

                // Встановлення заголовків відповіді
                response.ContentLength64 = buffer.Length;
                response.ContentType = "text/plain";
                response.StatusCode = (int)HttpStatusCode.OK;

                // Відправлення відповіді
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
            else
            {
                // Відповідь на інші запити
                string responseData = "Invalid login";
                byte[] buffer = Encoding.UTF8.GetBytes(responseData);

                response.ContentLength64 = buffer.Length;
                response.ContentType = "text/plain";
                response.StatusCode = (int)HttpStatusCode.NotFound;

                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
        }
    }
}
