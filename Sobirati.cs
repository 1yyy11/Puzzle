using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pazzlee
{
    public partial class Sobirati : Form
    {

        private int currentImageIndex = 0;
        public Sobirati()
        {
            InitializeComponent();

            // Загрузка изображений из ресурсов в ImageList
            imageList1.Images.Add(Properties.Resources.fon_transformed);
            imageList1.Images.Add(Properties.Resources.fon);
            imageList1.Images.Add(Properties.Resources.general);
            imageList1.Images.Add(Properties.Resources.images);
            imageList1.Images.Add(Properties.Resources.imagess);
            // Установка свойства BackgroundImageLayout для panel1
            panel1.BackgroundImageLayout = ImageLayout.Stretch;

            // Установка первого изображения в panel1
            if (imageList1.Images.Count > 0)
            {
                panel1.BackgroundImage = imageList1.Images[0]; // Установите первое изображение
            }
            else
            {
                // Обработка случая, когда изображения не загружены
                MessageBox.Show("No images loaded into ImageList.");
            }
        }

            private void button1_Click(object sender, EventArgs e)
        {
            currentImageIndex = (currentImageIndex + 1) % imageList1.Images.Count;
            panel1.BackgroundImage = imageList1.Images[currentImageIndex];
            panel1.Invalidate();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentImageIndex = (currentImageIndex - 1 + imageList1.Images.Count) % imageList1.Images.Count;
            panel1.BackgroundImage = imageList1.Images[currentImageIndex];
            panel1.Invalidate();
        }

        private void Sobirati_Load(object sender, EventArgs e)
        {

            button1.BackColor = Color.FromArgb(237, 161, 217); // Устанавливаем цвет фона кнопки на прозрачный
            button1.FlatStyle = FlatStyle.Flat; // Убираем стандартные границы кнопки
            button1.FlatAppearance.BorderSize = 0; // Убираем границы кнопки

            button2.BackColor = Color.FromArgb(237, 161, 217); // Устанавливаем цвет фона кнопки на прозрачный
            button2.FlatStyle = FlatStyle.Flat; // Убираем стандартные границы кнопки
            button2.FlatAppearance.BorderSize = 0; // Убираем границы кнопки

            button3.BackColor = Color.FromArgb(237, 161, 217); // Устанавливаем цвет фона кнопки на прозрачный
            button3.FlatStyle = FlatStyle.Flat; // Убираем стандартные границы кнопки
            button3.FlatAppearance.BorderSize = 0; // Убираем границы кнопки
                        
           
        }

        private Image ScaleImage(Image img, int maxWidth, int maxHeight)
        {
            // Вычисление коэффициента масштабирования
            float ratioX = (float)maxWidth / img.Width;
            float ratioY = (float)maxHeight / img.Height;
            float ratio = Math.Min(ratioX, ratioY);

            // Вычисление нового размера
            int newWidth = (int)(img.Width * ratio);
            int newHeight = (int)(img.Height * ratio);

            // Создание нового изображения
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic; // Высокое качество масштабирования
                g.DrawImage(img, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                panel1.BackgroundImageLayout = ImageLayout.None; // Установите в None

                // Начальная установка изображения
                if (imageList1.Images.Count > 0)
                {
                    panel1.BackgroundImage = imageList1.Images[currentImageIndex];

                    // Переход на следующую форму
                    Sborka sborkaForm = new Sborka(); // Создаем экземпляр формы Sborka
                    sborkaForm.BackgroundImageToShow = imageList1.Images[currentImageIndex]; // Передаем изображение
                    sborkaForm.Show(); // Показываем новую форму
                    this.Hide(); // Скрываем текущую форму
                }
                else
                {
                    // Обработка случая, когда изображения не загружены
                    MessageBox.Show("No images loaded into ImageList.");
                    return; // Завершаем выполнение метода, чтобы не переходить на следующую форму
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
          

        }
    }
}
