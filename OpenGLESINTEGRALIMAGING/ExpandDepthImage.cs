using System;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;
using System.Windows.Media;

using Microsoft.Kinect;
using System.Drawing.Drawing2D;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections.Generic;
using System.Drawing.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenCvSharp.Extensions;
using System.Drawing.Imaging;

namespace OpenGLESINTEGRALIMAGING
{
    public partial class ExpandDepthImage : Form
    {

        public IplImage ColorImage;
        public int ImageWidth = 1920;
        public int ImageHeight = 1080;

        public int DepthWidth = 512;
        public int DepthHeight = 424;

        public Bitmap ColorImg;
        public Bitmap DepthImg;

        public OpenFileDialog ofd;

        public double pixelDepth;
        public Bitmap expandImg;

        // coordinate mapper
        private KinectSensor kinectSensor = null;
        private FrameDescription colorFrameDescription ;
        private FrameDescription depthFrameDescription;
        private CoordinateMapper mapper = null;
        private FrameDescription frameDescription = null;

        Rectangle rect;


        public ExpandDepthImage()
        {
            // one sensor is currently supported
            this.kinectSensor = KinectSensor.GetDefault();

            // get the coordinate mapper
            this.mapper = this.kinectSensor.CoordinateMapper;

            // get the depth (display) extents
            colorFrameDescription = this.kinectSensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Bgra);

            depthFrameDescription = this.kinectSensor.DepthFrameSource.FrameDescription;
            frameDescription = this.kinectSensor.DepthFrameSource.FrameDescription;

            InitializeComponent();
            
            
        }

     



        // event button method to load color data
        private void btnColorData_Click(object sender, EventArgs e)
        {
            Console.WriteLine("LOAD ORIGINAL COLOR IMAGE");
            ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ColorImg = (Bitmap)Image.FromFile(ofd.FileName);
                
                pictureBox1.Image = ColorImg;
                label1.Visible = true;
            }
        }

        private void btnDepth_Click(object sender, EventArgs e)
        {
            Console.WriteLine("LOAD ORIGINAL DEPTH IMAGE");
            ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DepthImg = (Bitmap)Image.FromFile(ofd.FileName);
                //Console.WriteLine(DepthImg);
                pictureBox3.Image = DepthImg;
                label3.Visible = true;
            }
        }

        private void btnGrayscaleImage_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < ColorImg.Height; y++)
            {
                for (int x = 0; x < ColorImg.Width; x++)
                {
                    System.Drawing.Color c = ColorImg.GetPixel(x, y);

                    int r = c.R;
                    int g = c.G;
                    int b = c.B;
                    int avg = (r + g + b) / 3;
                    ColorImg.SetPixel(x, y, System.Drawing.Color.FromArgb(avg, avg, avg));
                }
            }
            pictureBox2.Image = ColorImg;
            label2.Visible = true;
        }

        private void btnExpandDepthData_Click(object sender, EventArgs e)
        {
            //BitmapPalette myPalette;
            //int width = 128;
            //int height = width;
            //int stride = width / 8;
            //byte[] pixels = new byte[height * stride];

            // Try creating a new image with a custom palette.
            //List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
            //colors.Add(System.Windows.Media.Colors.Red);
            //colors.Add(System.Windows.Media.Colors.Blue);
            //colors.Add(System.Windows.Media.Colors.Green);
            //myPalette = new BitmapPalette(colors);

            // Creates a new empty image with the pre-defined palette
            //BitmapSource image = BitmapSource.Create(width, height, 96, 96, myPalette, pixels, stride);

            //pictureBox4.Image = expandImg;
            depthExtend();
        }

        //TODO: Test resize image
        public Image ResizeImage(Image source, RectangleF destinationBounds)
        {
            Console.WriteLine(source.Width + "x" + source.Height);
            
            RectangleF sourceBounds = new RectangleF(0.0f, 0.0f, (float)source.Width, (float)source.Height);
            RectangleF scaleBounds = new RectangleF();

            Image destinationImage = new Bitmap((int)destinationBounds.Width, (int)destinationBounds.Height);
            Graphics graph = Graphics.FromImage(destinationImage);
            graph.InterpolationMode =
                System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            // Fill with background color
            graph.FillRectangle(new SolidBrush(System.Drawing.Color.White), destinationBounds);

            float resizeRatio, sourceRatio;
            float scaleWidth, scaleHeight;

            sourceRatio = (float)source.Width / (float)source.Height;

            if (sourceRatio >= 1.0f)
            {
                //landscape
                resizeRatio = destinationBounds.Width / sourceBounds.Width;
                scaleWidth = destinationBounds.Width;
                scaleHeight = sourceBounds.Height * resizeRatio;
                float trimValue = destinationBounds.Height - scaleHeight;
                graph.DrawImage(source, 0, (trimValue / 2), destinationBounds.Width, scaleHeight);
            }
            else
            {
                //portrait
                resizeRatio = destinationBounds.Height / sourceBounds.Height;
                scaleWidth = sourceBounds.Width * resizeRatio;
                scaleHeight = destinationBounds.Height;
                float trimValue = destinationBounds.Width - scaleWidth;
                graph.DrawImage(source, (trimValue / 2), 0, scaleWidth, destinationBounds.Height);
            }
            Console.WriteLine(destinationImage.Width + "x" + destinationImage.Height);

            pictureBox5.Image = destinationImage;
            return destinationImage;

        }

       
       
    
        void depthExtend()
        {

            var brush = new SolidBrush(System.Drawing.Color.Black);

            //DepthImg = (Bitmap)Image.FromFile(ofd.FileName);
            if (DepthImg != null)
            {
                int scale = Math.Min(ImageWidth / DepthImg.Width, ImageHeight / DepthImg.Height);
                var bmp = new Bitmap(ImageWidth, ImageHeight);
                var graph = Graphics.FromImage(bmp);

                var scaleWidth = (DepthImg.Width * scale);
                var scaleHeight = (DepthImg.Height * scale);

                graph.FillRectangle(brush, new RectangleF(0, 0, ImageWidth, ImageHeight));
                //graph.DrawImage(DepthImg, new Rectangle((ImageWidth - scaleWidth) / 2, (ImageHeight - scaleHeight) / 2, scaleWidth, scaleHeight));

                //bmp.Save(@"D:\\Depth Image\\ResizeColor11.jpg");

                pictureBox4.Image = bmp;
               
                label4.Visible = true;
            }
            else
            {
                MessageBox.Show("No image to convert!", "Source not found",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
                

            


            //OpenFileDialog ofd = new OpenFileDialog();
            //if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    DepthImg = (Bitmap)Image.FromFile(ofd.FileName);

            //    int scale = Math.Min(ImageWidth / DepthImg.Width, ImageHeight / DepthImg.Height);
            //    var bmp = new Bitmap(ImageWidth, ImageHeight);
            //    var graph = Graphics.FromImage(bmp);

            //    var scaleWidth = (DepthImg.Width * scale);
            //    var scaleHeight = (DepthImg.Height * scale);

            //    graph.FillRectangle(brush, new RectangleF(0, 0, ImageWidth, ImageHeight));
            //    //graph.DrawImage(DepthImg, new Rectangle((ImageWidth - scaleWidth) / 2, (ImageHeight - scaleHeight) / 2, scaleWidth, scaleHeight));

            //    //bmp.Save(@"D:\\Depth Image\\ResizeColor11.jpg");

            //    pictureBox4.Image = bmp;
            //    label4.Visible = true;

            //}

            //Console.WriteLine("HELLO DEPTH COLOR IMAGE");
            //try
            //{
            //    // Retrieve the image.
            //    Bitmap image1 = new Bitmap(@"D:\\Depth Image\\depthImage.png", true);

            //    int x, y;

            //    // Loop through the images pixels to reset color.
            //    for (x = 0; x < image1.Width; x++)
            //    {
            //        for (y = 0; y < image1.Height; y++)
            //        {
            //            System.Drawing.Color pixelColor = image1.GetPixel(x, y);
            //            System.Drawing.Color newColor = System.Drawing.Color.FromArgb(pixelColor.R, 0, 0);
            //            image1.SetPixel(x, y, newColor);
                             
            //            //Console.WriteLine("x: " + x + " y: " + y + " newColor: " + newColor);
            //        }
            //    }

            //    // Set the PictureBox to display the image.
            //    pictureBox5.Image = image1;

            //    // Display the pixel format in Label1.
            //    label6.Visible = true;

            //}
            //catch (ArgumentException)
            //{
            //    MessageBox.Show("There was an error." +
            //        "Check the path to the image file.");
            //}
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExpadingDepthValue_Click(object sender, EventArgs e)
        {
            rect = new Rectangle(30, 90,1920, 1080);
            ResizeImage(DepthImg, rect);

        }




        // button event to create matrix array
        private void button1_Click(object sender, EventArgs e)
        {

            int[,] metrix1 = new int[2, 3] { { 2, -2, 0 }, { 1, 0, 2 } };
            int[,] metrix2 = new int[2, 2] { { 2, 1 }, { -1, 2 } };
            int[,] metrixMultplied = new int[2, 3];

            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        metrixMultplied[row, col] = metrixMultplied[row, col] + metrix1[i, col] * metrix2[row, i];

                    }
                    Console.Write(metrixMultplied[row, col] + ", ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();

        }


        private void btnDepthConverter_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            
        }
    }
}
