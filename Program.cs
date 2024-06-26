﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Threading;

namespace ConsoleGame
{
    public class DevProgram
    {
        private const int ScreenWidth = 100;
        private const int ScreenHeingt = 50;
        private const int MapWidth = 32;
        private const int MapHeing = 32;

        private const double Fov = Math.PI / 3;
        private const double Depth = 16;

        private static double _playerX = 5;
        private static double _playerY = 5;
        private static double _playerA = 0;

        private static string _map = " ";

        private static readonly char[] Screen = new char[ScreenWidth * ScreenHeingt];
       
        public static void DevStart()
        {
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t\t\t\t\t\t" + "Играть  Enter");
            Console.WriteLine("\n\t\t\t\t\t\t" + "Выйти  Esc");
            Console.ForegroundColor = ConsoleColor.White;

            while (true)
            {
                //SoundPlayer sp = new SoundPlayer("\\Путь к музыки");
                if (Console.KeyAvailable)
                {
                    ConsoleKey consolKey1 = Console.ReadKey(true).Key;
                    switch (consolKey1)
                    {

                        case ConsoleKey.Enter:
                            Console.Beep(659, 300);

                            Text();

                            Console.SetWindowSize(ScreenWidth, ScreenHeingt);
                            Console.SetBufferSize(ScreenWidth, ScreenHeingt);

                            Console.CursorVisible = false;

                            _map += "##########################";
                            _map += "#........................#";
                            _map += "#........................#";
                            _map += "#........................#";
                            _map += "#.......#................#";
                            _map += "#.......#................#";
                            _map += "#.......#................#";
                            _map += "#.......#................#";
                            _map += "#.......#................#";
                            _map += "#.......#................#";
                            _map += "#.......############.....#";
                            _map += "#..................#.....#";
                            _map += "#..................#.....#";
                            _map += "#..................#.....#";
                            _map += "#..................#.....#";
                            _map += "#..................#.....#";
                            _map += "#........###########.....#";
                            _map += "#..................#.....#";
                            _map += "#..................#.....#";
                            _map += "#..................#.....#";
                            _map += "#..................#.....#";
                            _map += "#..................#.....#";
                            _map += "#..................#.....#";
                            _map += "#..................#.....#";
                            _map += "#..................#.....#";
                            _map += "#..................#.....#";
                            _map += "##########################";

                            // sp.Play();
                            Start();
                            break;
                    }
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Console.Beep(659, 300);
                        Environment.Exit(0);

                    }
                }
            }
        }
        static void Start()
        {

            DateTime dateTimeFrom = DateTime.Now;
            while (true)
            {
                try
                {
                    DateTime dateTimeTo = DateTime.Now;
                    double elapsedTime = (dateTimeTo - dateTimeFrom).TotalSeconds;
                    dateTimeFrom = DateTime.Now;
                    if (Console.KeyAvailable)
                    {
                        ConsoleKey consolKey2 = Console.ReadKey(true).Key;
                        switch (consolKey2)
                        {
                            case ConsoleKey.A:
                                _playerA += elapsedTime * 10;

                                break;
                            case ConsoleKey.D:
                                _playerA -= elapsedTime * 10;
                                break;
                            case ConsoleKey.S:
                                _playerX += Math.Sin(_playerA) * 10 * elapsedTime;
                                _playerY += Math.Cos(_playerA) * 10 * elapsedTime;


                                if (_map[(int)_playerY * MapWidth + (int)_playerX] == '#')
                                {
                                    _playerX -= Math.Sin(_playerA) * 10 * elapsedTime;
                                    _playerY -= Math.Cos(_playerA) * 10 * elapsedTime;
                                }

                                break;
                            case ConsoleKey.W:
                                _playerX -= Math.Sin(_playerA) * 10 * elapsedTime;
                                _playerY -= Math.Cos(_playerA) * 10 * elapsedTime;


                                if (_map[(int)_playerY * MapWidth + (int)_playerX] == '#')
                                {
                                    _playerX += Math.Sin(_playerA) * 10 * elapsedTime;
                                    _playerY += Math.Cos(_playerA) * 10 * elapsedTime;
                                }
                                break;



                        }

                        var key = Console.ReadKey();
                        if (key.Key == ConsoleKey.Escape)
                        {
                            Console.Beep(659, 300);
                            Environment.Exit(0);

                        }
                    }
               
                    for (int x = 0; x < ScreenWidth; x++)
                    {
                        double rayAngle = _playerA + Fov / 2 - x * Fov / ScreenWidth;
                        double rayX = Math.Sin(rayAngle);
                        double rayY = Math.Cos(rayAngle);

                        double disLanceToWall = 0;
                        bool hirWall = false;

                        while (!hirWall && disLanceToWall < Depth)
                        {
                            disLanceToWall += 0.1;

                            int testX = (int)(_playerX - rayX * disLanceToWall);
                            int testY = (int)(_playerY - rayY * disLanceToWall);

                            if (testX <= 0 || testX >= Depth + _playerX || testY <= 0 || testY >= Depth + _playerY)
                            {
                                hirWall = true;
                                disLanceToWall = Depth;
                            }
                            else
                            {
                                char testCell = _map[testY * MapWidth + testX];

                                if (testCell == '#')
                                {
                                    hirWall = true;
                                }
                            }

                        }
                        int ceilong = (int)(ScreenHeingt / 2d - ScreenHeingt * Fov / disLanceToWall);
                        int floor = ScreenHeingt - ceilong;
                        //Текстура 
                        char wallShade;
                        if (disLanceToWall <= Depth / 4d)
                            wallShade = '\u2588';
                        else if (disLanceToWall <= Depth / 3d)
                            wallShade = '\u2593';
                        else if (disLanceToWall <= Depth / 2d)
                            wallShade = '\u2592';
                        else if (disLanceToWall <= Depth)
                            wallShade = '\u2591';
                        else
                            wallShade = ' ';
                        for (int y = 0; y < ScreenHeingt; y++)
                        {
                            if (y <= ceilong)
                            {
                                // Потолок
                                Screen[y * ScreenWidth + x] = ' ';
                            }
                            else if (y > ceilong && y <= floor)
                            {
                                //Стена
                                Screen[y * ScreenWidth + x] = wallShade;
                            }
                            else
                            {
                                //Пол
                                char floorShade;
                                double b = 1 - (y - ScreenHeingt / 2d) / (ScreenHeingt / 2d);
                                if (b < 0.25)
                                    floorShade = 'X';
                                else if (b < 0.5)
                                    floorShade = '+';
                                else if (b < 0.75)
                                    floorShade = '=';
                                else if (b < 0.9)
                                    floorShade = ':';
                                else
                                    floorShade = '.';
                                Screen[y * ScreenWidth + x] = floorShade;
                            }
                        }
                        ///Статистика
                        char[] stats = $"X: {_playerX}, Y: {_playerY}, A: {_playerA}, FPS: {(int)1 / elapsedTime}".ToCharArray();
                        stats.CopyTo(Screen, 0);
                        Console.SetCursorPosition(0, 0);
                        Console.Write(Screen);
                    }
                }
                catch (Exception ignore)
                {

                }      
               


            }
        }

        static void Text()
        {
           

        }
    }

    class Program
    {

       
        static void Main(string[] args)
        {

           DevProgram devProgram = new DevProgram();
           DevProgram.DevStart();

        }
       
    }
}
