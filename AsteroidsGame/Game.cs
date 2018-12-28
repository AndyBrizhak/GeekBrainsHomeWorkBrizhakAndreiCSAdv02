using System;
using System.Windows.Forms;
using System.Drawing;
//Брижак Андрей Домашнее задание по курсу C# уровень 2 урок 2

namespace AsteroidsGame
{
    /// <summary>
    /// класс, где будут происходить все действия игры
    /// </summary>
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;

        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        /// <summary>
        /// Максимальная высота и ширина экрана
        /// </summary>
        const int MaxH = 1000, MaxW = 1000;

        // <summary>
        /// массив объектов BaseObject
        /// </summary>
        public static BaseObject[] _objs;

        /// <summary>
        /// Обьект типа Bullet
        /// </summary>
        private static Bullet _bullet;

        /// <summary>
        /// массив объектов Asteroid
        /// </summary>
        private static Asteroid[] _asteroids;

        static Game()
        {
        }

        /// <summary>
        /// Инициализация сцены и обьектов
        /// </summary>
        /// <param name="form"></param>
        public static void Init(Form form)
        {
            


              // Графическое устройство для вывода графики
              Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics(); // Создаём объект - поверхность рисования и связываем его с формой
            // Запоминаем размеры формы
            Width = form.Width;
            Height = form.Height;

            //  Задача 4 ДЗ Проверка на задание размера экрана. Если высота или ширина(Width,
            //      Height) больше 1000 или принимает отрицательное значение, то выбросить исключение
            //  ArgumentOutOfRangeException().
            CheckSizeScreen(Width, Height);

            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Обработчик таймера в котором вызываются Draw () и Update();
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// вывод графики
        /// </summary>
        public static void Draw()
        {
            // Проверяем вывод графики
           // Buffer.Graphics.Clear(Color.Black);
           // Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
           // Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
           // Buffer.Render();

            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid obj in _asteroids)
                obj.Draw();
            _bullet.Draw();
            Buffer.Render();
        }

        /// <summary>
        /// инициализация  объектов
        /// </summary>
        public static void Load()
        {
            _objs = new BaseObject[30];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[3];
            var rnd = new Random();
            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(800, rnd.Next(0, Game.Height)), new Point(-r, r), new Size(3, 3));
            }
            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(800, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new
                    Size(r, r));
            }

            // for (int i = 0; i < _objs.Length / 3; i++)
            //    _objs[i] = new Planet(new Point(600, i * 20), new Point(-i, -i), new Size(10, 10));
            //for (int i = _objs.Length / 3; i < _objs.Length / 3 * 2; i++)
            //    _objs[i] = new Star(new Point(600, i * 20), new Point(-i, 0), new Size(10, 10));
           // for (int i = _objs.Length / 3 * 2; i < _objs.Length; i++)
           //     _objs[i] = new SmallRedStar(new Point(600, i * 20), new Point(-i, 0), new Size(5, 5));
        }

        /// <summary>
        /// изменения состояния объектов
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            foreach (Asteroid a in _asteroids)
            {

                if (a.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play(); 
                  // a.;
                }
                a.Update();
            }
            _bullet.Update();
        }


        /// <summary>
        /// Проверка на задание размера экрана
        /// </summary>
        /// <param name="width">фактическая ширина экрана</param>
        /// <param name="height">фактическая высота экрана</param>
        public static void CheckSizeScreen(int width, int height)
        {
            if (width <= 0 || height <= 0 || width > MaxW || height > MaxH)
            {
                throw new ArgumentOutOfRangeException();   //выбросить исключение

            }

           
        }
        
    }
}
