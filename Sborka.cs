using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Pazzlee
{
    public partial class Sborka : Form
    {
        public Image BackgroundImageToShow { get; set; } // Свойство для передачи изображения
        private List<PictureBox> puzzlePieces = new List<PictureBox>();
        private Random random = new Random();
        private const int PieceSize = 150; // Новый размер кусочка
        public Sborka()
        {
            InitializeComponent();
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void Sborka_Load(object sender, EventArgs e)
        {
            if (BackgroundImageToShow != null)
            {
                SplitImageIntoPuzzlePieces(BackgroundImageToShow);
            }
        }

        private void SplitImageIntoPuzzlePieces(Image image)
        {
            int pieceWidth = image.Width / 4;
            int pieceHeight = image.Height / 4;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PictureBox piece = new PictureBox
                    {
                        Size = new Size(pieceWidth, pieceHeight),
                        Image = CropImage(image, new Rectangle(j * pieceWidth, i * pieceHeight, pieceWidth, pieceHeight)),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    piece.MouseDown += Piece_MouseDown;
                    pictureBox1.Controls.Add(piece);
                    puzzlePieces.Add(piece);
                }
            }

            ShufflePuzzlePieces();
        }

        private Image CropImage(Image image, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(image);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        private void ShufflePuzzlePieces()
        {
            foreach (var piece in puzzlePieces)
            {
                pictureBox1.Controls.Remove(piece);
            }

            foreach (var piece in puzzlePieces.OrderBy(p => random.Next()))
            {
                pictureBox1.Controls.Add(piece);
                piece.Location = new Point(random.Next(pictureBox1.Width - piece.Width), random.Next(pictureBox1.Height - piece.Height));
            }
        }

        private void Piece_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox piece = sender as PictureBox;
            piece.DoDragDrop(piece, DragDropEffects.Move);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PictureBox)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox piece = (PictureBox)e.Data.GetData(typeof(PictureBox));
            Point point = panel1.PointToClient(new Point(e.X, e.Y));
            piece.Location = point;
            pictureBox1.Controls.Remove(piece);
            panel1.Controls.Add(piece);
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
    }
}
