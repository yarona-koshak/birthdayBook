using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

class Program
{
  static void Main()
  {
    int port = 5000;

    var server = new Server(port);

    Console.WriteLine("The server is running");
    Console.WriteLine($"Main Page: http://localhost:{port}/website/pages/openingPage.html");

    var database = new Database();
     AddStartPictures(database);

    while (true)
    {
      (var request, var response) = server.WaitForRequest();

      Console.WriteLine($"Recieved a request with the path: {request.Path}");

      if (File.Exists(request.Path))
      {
        var file = new File(request.Path);
        response.Send(file);
      }
      else if (request.ExpectsHtml())
      {
        var file = new File("website/pages/404.html");
        response.SetStatusCode(404);
        response.Send(file);
      }
      else
      {
        try
        {
          if (request.Path == "getPicture")
          {
            var pictures = database.Pictures.ToArray();

            response.Send(pictures);
          }
          response.SetStatusCode(405);

          database.SaveChanges();
        }
        catch (Exception exception)
        {
          Log.WriteException(exception);
        }
      }

      response.Close();
    }
  }
  static void AddStartPictures(Database database)
  {
    if (database.IsNewlyCreated())
    {
      var startArticals = new Picture[] {
        new Picture(
          "https://i.pinimg.com/736x/91/4d/e9/914de96f4d2988f5769411fc0a5adee1.jpg",
          "Нашему пространству меньше всего присущ пафос…В дату тождественную рождению начала,Где материнский инстинкт не понизил градус,Коротким выдохом согласие прозвучало. Значит теперь идти, а затем и брести устало… Галеры за горизонт цеплялись, на спинах блестела соль… - Эй, Капитан, кто там бежит вдоль причала? Машет руками! -Наверное, снова Ассоль! Слышишь, красотка, нужны лишь пиастры с ромом, Девок пощупать, до шеи подальше держать от реи. Что ты нас манишь любовью, супом и домом!? Подонок наш капитан, да и мы не Греи! Перспективочка вырисовывается… к черту книги! Фильмы легче. (Все, как в жизни – не удается заглянуть за fin). Просто отчалим от берега, сбросив вериги, Шепча молитву-считалочку: «Я не одна, ты не один!» Можешь тонуть только в моих озерах На поверхность всплывая за кислородом синий, как кит. ОН разобрался в ангелах-режиссерах, Назначив самим спродюсировать нам свой хит. Мы играем роли отважных, потому что счастливых, Талантливо нежно снимается каждый кадр. Черновики не сожжены, но пылятся давно в архивах, А там драконы, ведьмы и даже один кадавр. Не подошел никакой жанр киношный: Артхаус, чёрно-белое, европейский и голливудский бренд. Реализм порхает бабочкой в районе подвздошной Области.Такой вот ежеминутный, ежедневный, родной хэппи end/"
        ),
           new Picture(
          "https://i.pinimg.com/736x/15/1e/e4/151ee46b478d41530ba53c7251346e80.jpg",
          " Тепло Это твой портсигар Прикрывающий мою сигаретную пачку В мороке ночи От Промозглой влаги  Бумаги Изможденные без числа В темноте Бабочкой полетала И Прилетела к кровати прилегла  Где уже спит исполин в запахах естества  Спасибо успевающий сказать мне Выпить Его бы до последнего Стона Глотка Нагой А после позже не однажды потом века Лежала бы левая твоя нога Оплетенная Моею  такой же Ногой Кстати"
        )
      };
        for (int i = 0; i < startArticals.Length; i++)
      {
        database.Pictures.Add(startArticals[i]);
      }

      database.SaveChanges();
    }
  }
}


class Database() : DbBase("database")
{
  public DbSet<Picture> Pictures { get; set; } = default!;
}

class Picture( string src, string text)
{
  [Key] public int Id { get; set; } = default!;
  public string Src { get; set; } = src;
  public string Text { get; set; } = text;
}