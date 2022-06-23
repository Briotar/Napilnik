using System;
using System.IO;

namespace Lesson
{
    interface ILogger
    {
        void Find();
    }

    class ConsoleLogWriter : ILogger
    {
        private ILogger _logger;
        private string _message;

        public ConsoleLogWriter(string message, ILogger logger = null)
        {
            _message = message;
            _logger = logger;
        }

        public void Find()
        {
            Console.WriteLine(_message);

            if(_logger != null)
                _logger.Find();
        }
    }

    class FileLogWriter : ILogger
    {
        private ILogger _logger;
        private string _message;
        private string _filename;

        public FileLogWriter(string message, string filename, ILogger logger = null)
        {
            _message = message;
            _filename = filename;
            _logger = logger;
        }

        public void Find()
        {
            Console.WriteLine(_message);

            File.WriteAllText($"{_filename}.txt", _message);

            if (_logger != null)
                _logger.Find();
        }
    }

    class SecureLogWriter : ILogger
    {
        ILogger _logger;

        public SecureLogWriter(ILogger logger)
        {
            _logger = logger;
        }

        public  void Find()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                _logger.Find();
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            string filename = "File";

            ILogger consoleWriter = new ConsoleLogWriter("Просто написал в консоль");
            ILogger fileWriter = new FileLogWriter("Просто написал в файл", filename);
            ILogger secureConsoleWriter = new SecureLogWriter(new ConsoleLogWriter("Написал в консоль в  пятницу"));
            ILogger secureFileWriter = new SecureLogWriter(new FileLogWriter("Написал в файл в  пятницу", filename));
            ILogger ConsoleWriterAndSecuryFileWriter = new ConsoleLogWriter("Написаль в консоль", new SecureLogWriter(new FileLogWriter("Но сегодня пятница, поэтому и в файл", filename)));

            consoleWriter.Find();
            fileWriter.Find();
            secureConsoleWriter.Find();
            secureFileWriter.Find();
            ConsoleWriterAndSecuryFileWriter.Find();
        }
    }
}