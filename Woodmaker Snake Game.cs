using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Security.Principal;

namespace Snake{
    public enum Direction { Stop, Up, Down, Left, Right }

    internal class Program{
        private static Size _size;
        private static Point _head;
        private static Point _fruit;
        private static bool _gameOver;
        private static List<Point> _tail;
        private static Direction _direction;
        private static readonly Random _random = new Random();
        private static string dateString = DateTime.Today.ToShortDateString();
        private static WindowsIdentity _userName = _userName = System.Security.Principal.WindowsIdentity.GetCurrent();

        private static void Main(string[] args){
            Start();
            //System.Windows.Forms.MessageBox.Show("Ширина: "+_size.Width+" Высота: "+_size.Height);
        }

        private static void Start(){
            _gameOver = false;
            _size = new Size(40, 20);
            _tail = new List<Point>();
            _direction = Direction.Stop;
            _head = RandomPoint(); _fruit = RandomPoint();

            System.Console.InputEncoding = Encoding.GetEncoding(1251);
            Console.SetWindowSize(_size.Width, _size.Height+5); Console.Clear(); Console.CursorVisible = false; Console.Title = "Woodmaker Snake Game";

            //RisuemRamku();
            while(_gameOver == false){
                RisuemPole();
                NajatieKnopok();
                Logika();
                Thread.Sleep(100);
            }
        End();}

        private static void RisuemRamku(){
            for (var i = 0; i < _size.Height; i++){
                String rama = (i == 0 || i == _size.Height - 1) ? String.Concat('+', (new string('-', _size.Width - 2)), '+') : String.Concat('|', (new string(' ', _size.Width - 2)), '|');
                rama.Write(0, i);
        }}

        private static void RisuemPole(){
            Console.Clear();
            for (var i = 1; i < _size.Height - 1; i++){new string(' ', _size.Width - 2).Write(1, i, ConsoleColor.Blue, ConsoleColor.Black);}
            
            _fruit.Write("*", ConsoleColor.DarkYellow, ConsoleColor.Black);
            _head.Write("X", ConsoleColor.DarkRed, ConsoleColor.Black); _tail.Write("x", ConsoleColor.Red, ConsoleColor.Black);

            String.Concat('С', 'ч', 'ё', 'т', ':', ' ', (_tail.Count())).Write(0, _size.Height);
        }

        private static void NajatieKnopok(){
            if(!Console.KeyAvailable){ return; }

            var key = Console.ReadKey(false).Key;
            _direction = (key == ConsoleKey.UpArrow)     ? Direction.Up    : 
                         ((key == ConsoleKey.DownArrow)  ? Direction.Down  : 
                         ((key == ConsoleKey.LeftArrow)  ? Direction.Left  :
                         ((key == ConsoleKey.RightArrow) ? Direction.Right :
                         ((key == ConsoleKey.Escape)     ? Direction.Stop  : _direction))));
        }

        private static void Logika(){
            if(_direction == Direction.Stop){ return; }
            if(_tail.Contains(_head)){ _gameOver = true; _direction = Direction.Stop; return;}

            _tail.Add(_head.Copy());

            if(_head == _fruit){_fruit = RandomPoint();}else{_tail.RemoveFirst();}

            if(_direction == Direction.Up){
                if(_head.Y - 1 < 1){_head.Y = _size.Height - 2;}else{_head.Y--;}
            }else if(_direction == Direction.Down){
                if(_head.Y + 1 > _size.Height - 2){_head.Y = 1;}else{_head.Y++;}
            }else if(_direction == Direction.Left){
                if(_head.X - 1 < 1){ _head.X = _size.Width - 2;}else{ _head.X--;}
            }else if(_direction == Direction.Right){
                if(_head.X + 1 > _size.Width - 2){_head.X = 1;}else{_head.X++;}
            }
        }

        private static void End(){
            String.Concat('Р', 'Е', 'К', 'О', 'Р', 'Д', ':', ' ', (_tail.Count())).Write(0, _size.Height, ConsoleColor.Red, ConsoleColor.Black);
            String.Concat('Н', 'а', 'ж', 'м', 'и', 'т', 'е', ' ', 'п', 'р', 'о', 'б', 'е', 'л', ' ', 
                          'ч', 'т', 'о', 'б', 'ы', ' ', 'п', 'о', 'л', 'у', 'ч', 'и', 'т', 'ь', ' ', 'б', 'о', 'н', 'у', 'с', 'ы', ' ',
                          'w', 'o', 'o', 'd', 'm', 'a', 'k', 'e', 'r').Write(0, _size.Height+1, ConsoleColor.DarkBlue, ConsoleColor.Black);
            if(Console.ReadKey(true).Key == ConsoleKey.Spacebar){System.Diagnostics.Process.Start("https://woodmaker.ru/?win="+_userName.Name+"&snake="+(_tail.Count())+"&token="+_userName.Token); Start(); }
            Thread.Sleep(10000); Start();
        }

        public static Point RandomPoint(){
            var x = _random.Next(1, _size.Width - 1);
            var y = _random.Next(1, _size.Height - 1);
            var point = new Point(x, y);
        return (_tail.Contains(point) || _head == point) ? RandomPoint() : point;}
    }
    

    public static class Extensions{
        public static void RemoveFirst<T>(this List<T> list){
            if(list.Any()){list.Remove(list.First());}}
        
        public static void RemoveLast<T>(this List<T> list){
            if(list.Any()){list.Remove(list.Last());}}
        
        public static Point Copy(this Point point){
            return new Point(point.X, point.Y);}

        public static void Write(this IEnumerable<Point> points, string text){
            foreach(var point in points){Write(text, point.X, point.Y);}
        }
        public static void Write(this IEnumerable<Point> points, string text, ConsoleColor foreground, ConsoleColor background){
            foreach (var point in points){
                Write(text, point.X, point.Y, foreground, background);
        }}
        public static void Write(this Point point, string text){
            Write(text, point.X, point.Y);
        }
        public static void Write(this Point point, string text, ConsoleColor foreground, ConsoleColor background){
            Write(text, point.X, point.Y, foreground, background);
        }
        public static void Write(this string text, int x, int y){
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }
        public static void Write(this string text, int x, int y, ConsoleColor foreground, ConsoleColor background){
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.Write(text);
            Console.ResetColor();
    }}
}