using System;
using System.IO;

namespace Lesson
{
    internal interface ILogger
    {
        void Find();
    }

    internal class ConsoleLogWriter : ILogger
    {
        private readonly ILogger _logger;
        private readonly string _message;

        public ConsoleLogWriter(string message, ILogger logger = null)
        {
            _message = message;
            _logger = logger;
        }

        public void Find()
        {
            Console.WriteLine(_message);

            if (_logger != null)
                _logger.Find();
            else
                throw new NullReferenceException();
        }
    }

    internal class FileLogWriter : ILogger
    {
        private readonly ILogger _logger;
        private readonly string _message;
        private readonly string _filename;

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
            else
                throw new NullReferenceException();
        }
    }

    internal class SecureLogWriter : ILogger
    {
        private readonly ILogger _logger;

        public SecureLogWriter(ILogger logger)
        {
            _logger = logger;
        }

        public void Find()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                _logger.Find();
            }
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            string filename = "File";

            ILogger consoleWriter = new ConsoleLogWriter("Просто написал в консоль");
            ILogger fileWriter = new FileLogWriter("Просто написал в файл", filename);
            ILogger secureConsoleWriter = new SecureLogWriter(new ConsoleLogWriter("Написал в консоль в  пятницу"));
            ILogger secureFileWriter = new SecureLogWriter(new FileLogWriter("Написал в файл в  пятницу", filename));
            ILogger consoleWriterAndSecuryFileWriter = new ConsoleLogWriter("Написаль в консоль", new SecureLogWriter(new FileLogWriter("Но сегодня пятница, поэтому и в файл", filename)));

            consoleWriter.Find();
            fileWriter.Find();
            secureConsoleWriter.Find();
            secureFileWriter.Find();
            consoleWriterAndSecuryFileWriter.Find();
        }
    }
}