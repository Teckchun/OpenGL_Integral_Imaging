using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Utilities;
using OpenCvSharp.Extensions;
using System.IO;

namespace ConvertDepthData
{
    public partial class Form1 : Form
    {
        public IplImage mappingimage;

        public List<CvPoint3D32f> PointArray = new List<CvPoint3D32f>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenMappingImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            DialogResult Dr = OFD.ShowDialog();
            if (Dr == System.Windows.Forms.DialogResult.OK)
            {
                //ColorImage.SetZero();
                mappingimage = IplImage.FromFile(OFD.FileName, LoadMode.AnyColor);

                pictureBox1.Image = mappingimage.ToBitmap();
            }
        }

        private void btnMappingDepth_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            DialogResult Dr = OFD.ShowDialog();
            if (Dr == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader Sreader = new StreamReader(OFD.FileName, System.Text.Encoding.ASCII);

                while (!Sreader.EndOfStream)
                {
                    String[] templine = Sreader.ReadLine().Split(new char[] { ' ', ',' });
                    if (templine.Length > 0 && templine.Length <= 4)
                    {
                        int index_x = int.Parse(templine[1]);
                        int index_y = int.Parse(templine[0]);
                        if (index_x > 0 && index_x < mappingimage.Width && index_y > 0 && index_y < mappingimage.Height)
                        {
                            float realdepth = float.Parse(templine[2]);


                            PointArray.Add(new CvPoint3D32f(index_x, index_y, realdepth));
                        }
                    }
                }
            }
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            DialogResult DR = SFD.ShowDialog();
            if(DR == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter Polygonswriter = new StreamWriter(SFD.FileName, false);

                Polygonswriter.WriteLine("" + mappingimage.Width + "," + mappingimage.Height);

                for(int i =0;i<PointArray.Count;i++)
                {

                }
            }
        }
    }
}
