using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    int width = 25;
    int height = 20;
    char dash = '▲';
    char calle = ' ';
    char personaje = '@';
    int calleStart = 10;
    int personajePos = 12;

    public Program(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void StartGame()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;

        Queue<string> buffer = new Queue<string>();
        Random random = new Random();
        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.LeftArrow)
                {
                    personajePos = Math.Max(0, personajePos - 1);
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    personajePos = Math.Min(width - 1, personajePos + 1);
                }
                
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
            }

            char[] filas = new char[width];
            for (int i = 0; i < width; i++)
            {
                filas[i] = dash;
            }

            for (int i = 0; i < 6; i++)
            {
                int position = Math.Max(0, Math.Min(width - 1, calleStart + i));
                filas[position] = calle;
            }

            int move = random.Next(3) - 1;
            calleStart = Math.Max(0, Math.Min(width - 6, calleStart + move));

            string line = new string(filas);
            buffer.Enqueue(line);

            if (buffer.Count > height)
            {
                buffer.Dequeue();
            }

            Console.SetCursorPosition(0, 0);
            string[] bufferArray = buffer.ToArray();
            for (int i = bufferArray.Length - 1; i >= 0; i--)
            {
                Console.WriteLine(bufferArray[i]);
            }

            Console.SetCursorPosition(personajePos, height - 1);
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.Write(personaje);
            Console.ResetColor();

            if (bufferArray[0][personajePos] == dash)
            {
                Console.SetCursorPosition(0, height);
                Console.WriteLine("¡Juego Terminado!");
                break;
            }

            Thread.Sleep(100);
        }
    }

    static void Main(string[] args)
    {
        Program ventana = new Program(25, 20);
        ventana.StartGame();
    }
}
