using System.Diagnostics;
namespace ProcessManagement
{
    internal class Program
    {
        public static States state = States.Start;
        static void Main(string[] args)
        {
            while (true)
                switch (state)
                {
                    case States.Start:  //запуск, приветствие
                        Console.WriteLine("С помощью этой программы Вы можете посмотерть системные процессы и отключать любые из них."); 
                        state = States.None;
                        break;
                    case States.None:
                        Console.WriteLine("Список команд: \n1 - вывести все активные процессы на экран \n2 - завершить процесс \n3 - завершение работы");
                        string s = Console.ReadLine();
                        if (s == "2")
                        {
                            Console.WriteLine("Для завершения процесса введите его ID");
                            bool ok = false; 
                            while (!ok)
                            {
                                try
                                {
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    if (id == -1)
                                    {
                                        ok = true;
                                        continue;
                                    }
                                        StopProsess(id);
                                        ok = true;
                                    
                                }
                                catch (FormatException) //Если ввели символ
                                {
                                    Console.WriteLine("Пожалуйста, введите ID процесса - он состоит из цифр, или, если хотите вернуться к списку задач, введите -1");
                                }
                            }
                        }
                        else if (s == "1")
                        {
                            ReturnAllProcesses();
                            state = States.None;
                        }
                        else if (s == "3")
                            Environment.Exit(0);
                        else
                        {
                            Console.WriteLine("Неизвестная команда, попробуйте ещё раз.");
                            state = States.None;

                        }
                        break;
                    case States.Ex:
                        Console.WriteLine("Введите заново ID процесса:");
                        int id2 = Convert.ToInt32(Console.ReadLine());
                        StopProsess(id2);
                        state = States.None;
                        break;
                }

        }
        static void ReturnAllProcesses()
        {
            Process[] process = Process.GetProcesses();
            foreach (Process process2 in process)
            {
                Console.WriteLine($"ID: {process2.Id} Название: {process2.ProcessName}");
            }
        }
        static void StopProsess(int id) 
        {
            try
            {
                Process process = Process.GetProcessById(id);
                process.Kill();
                Console.WriteLine($"Процесс {process.ProcessName} успешно завершен.");
            }
            catch (ArgumentException) {
                Console.WriteLine($"Процесс с ID {id} не найден. Повторить попытку? Напишите 1, если да, и 0, если хотите вернуться к списку задач.");
                string s = Console.ReadLine();
                if( s == "1")
                    state = States.Ex;
                else if(s == "0")
                    state = States.None;
                else
                    Console.WriteLine("Ваша команда не была распознана, Вы вернетесь к списку задач");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при завершении процесса: {ex.Message}");
            }

        }
    }
    enum States
    {
        None,
        Start,
        Ex
    }
}
