//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Data.SqlClient;
//using Emgu.CV;
//using Emgu.CV.Structure;
//using System.Configuration;

//namespace CW_OOP_Test1
//{
//    public partial class Form2 : Form
//    {
//        #region Variable
//        MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_TRIPLEX, 0.6d, 0.6d);
//        HaarCascade faceDetected;

//        Image<Gray, byte> result;
//        Image<Gray, byte> TrainedFace = null;
//        Image<Gray, byte> grayFace = null;

//        List<Image<Gray, byte>> trainingImages = Form1.TrainingImages;
//        List<string> labelsNames = Form1.LabelsNames;
//        List<string> labelsImages = Form1.LabelsImages;

//        private SqlConnection sqlConnection = null;
//        #endregion

//        public Form2()
//        {
//            InitializeComponent();
//        }

//        private void brwsBttn_Click(object sender, EventArgs e)
//        {
//            OpenFileDialog ofd = new OpenFileDialog();
//            ofd.Filter = "(*.jpg, *.png) | *.jpg, *.png";
//            if(ofd.ShowDialog() == DialogResult.OK)
//            {
//                try
//                {
//                    previewBox.Image = new Bitmap(ofd.FileName);
//                }
//                catch (Exception) 
//                {
//                    MessageBox.Show("File hasn't inserted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void addPerson_Click(object sender, EventArgs e)
//        {
//            if(previewBox.Image != null && textBox1.Text != null) 
//            {
//                MCvAvgComp[][] DetectedFaces = grayFace.DetectHaarCascade(faceDetected, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));
//                foreach (MCvAvgComp f in DetectedFaces[0])
//                {
//                    TrainedFace = previewBox.Image.Copy(f.rect).Convert<Gray, byte>();
//                    break;
//                }
//                TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
//                trainingImages.Add(TrainedFace);
//                labelsNames.Add(textBox1.Text);

//                //File.WriteAllText(Application.StartupPath + "/Faces/Faces.txt", trainingImages.ToArray().Length.ToString() + ",");
//                for (int i = 0; i < trainingImages.ToArray().Length; i++)
//                {
//                    trainingImages.ToArray()[i].Save(Application.StartupPath + "/Faces/face" + i + ".bmp");
//                    //File.AppendAllText(Application.StartupPath + "/Faces/Faces.txt", labelsNames.ToArray()[i] + ",");

//                }
//                using (sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB_CW"].ConnectionString))
//                {
//                    sqlConnection.Open();

//                    SqlCommand command = new SqlCommand(
//                    $"INSERT INTO [Table] (Name, ImagePath) VALUES (@Name, @ImagePath)",
//                    sqlConnection);
//                    command.Parameters.AddWithValue("Name", textBox1.Text);
//                    command.Parameters.AddWithValue("ImagePath", Application.StartupPath + @"\Faces\face" + (trainingImages.ToArray().Length - 1) + ".bmp");

//                    command.ExecuteNonQuery();
//                }
//                MessageBox.Show(textBox1.Text + " Added Successfully");

//            }
//        }
//    }
//}
