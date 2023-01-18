using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;
using System.IO.Pipes;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Human[] humans_array = new Human[10];
        Doctor[] doctors_array = new Doctor[10];
        Model m = new Model(50, 50, 5);
        int CounterD = 0;
        int CounterH = 0;

        public Form1()
        {
            InitializeComponent();
            try
            {
                //string[] file_human = File.ReadAllLines("human.json");
                /*if (file_human.Length > 0)
                {
                    DataContractSerializer jsonF1 = new DataContractSerializer(typeof(Human[]));
                    using (FileStream fileStream1 = new FileStream("human.json", FileMode.OpenOrCreate))
                    {
                        Human[] loadpeople1 = (Human[])jsonF1.ReadObject(fileStream1);
                        foreach (Human h in loadpeople1)
                        {
                            humans_array[CounterH] = new Human(h.X, h.Y, h.SpeedOX, h.SpeedOY, h.Condition);
                            m.add_human(humans_array[CounterH]);
                            CounterH++;
                            listBox2.Items.Add($"{h.X}, {h.Y}, {h.SpeedOX}, {h.SpeedOY}, {h.Condition}");
                        }
                    }
                    
                }
                string[] file_doctor = File.ReadAllLines("doctor.json");
                if (file_doctor.Length > 0)
                {
                    DataContractSerializer jsonF2 = new DataContractSerializer(typeof(Doctor[]));
                    using (FileStream fileStream2 = new FileStream("doctor.json", FileMode.OpenOrCreate))
                    {
                        Doctor[] loadpeople2 = (Doctor[])jsonF2.ReadObject(fileStream2);
                        foreach (Doctor d in loadpeople2)
                        {
                            doctors_array[CounterD] = new Doctor(d.X, d.Y, d.SpeedOX, d.SpeedOY);
                            m.add_doctor(doctors_array[CounterD]);
                            CounterD++;
                        }
                    }
                }*/
                
                
            }
            catch { }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e) // апдейт
        {
            m.update(Convert.ToInt32(textBox7.Text));
            listBox1.Items.Clear();
            string[] mas = m.PrintH();
            for (int i = 0; i < Convert.ToInt32(mas.Last()); i++)
            {
                listBox1.Items.Add(mas[i]);
            }
            mas = m.PrintD();
            for (int i = 0; i < Convert.ToInt32(mas.Last()); i++)
            {
                listBox1.Items.Add(mas[i]);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Здоров");
            comboBox1.Items.Add("Болен");
            comboBox1.Enabled = false;
            comboBox1.Enabled = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e) //отобразить модель
        {
            listBox1.Items.Clear();
            string[] mas = m.PrintH();
            for (int i = 0; i < Convert.ToInt32(mas.Last()); i++)
            {
                listBox1.Items.Add(mas[i]);
            }
            mas = m.PrintD();
            for (int i = 0; i < Convert.ToInt32(mas.Last()); i++)
            {
                listBox1.Items.Add(mas[i]);
            }
        }

        private void button4_Click(object sender, EventArgs e)//очистить окно отображения
        {
            listBox1.Items.Clear();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e) //добавление человека
        {
            for (; CounterH < 10;)
            {
                if (Convert.ToInt32(textBox1.Text) >= 0 && Convert.ToInt32(textBox2.Text) >= 0 && Convert.ToInt32(textBox3.Text) >= 0 && Convert.ToInt32(textBox4.Text) >= 0 && (comboBox1.Text == "Здоров" || comboBox1.Text == "Болен"))
                {
                    humans_array[CounterH] = new Human(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text), comboBox1.Text); //textBox5.Text);
                    m.add_human(humans_array[CounterH]);
                    listBox2.Items.Add($"{textBox1.Text}, {textBox2.Text}, {textBox3.Text}, {textBox4.Text}, {comboBox1.Text}");
                    CounterH++;
                }
                else MessageBox.Show("Координаты и скорость человека не должны быть отрицательными, состояние должно быть Болен или Здоров");
                break;
            }
        }
        private void button6_Click(object sender, EventArgs e) //добавление доктора
        {
            try
            {
                for (; CounterD < 10;)
                {
                    if (Convert.ToInt32(textBox1.Text) >= 0 && Convert.ToInt32(textBox2.Text) >= 0 && Convert.ToInt32(textBox3.Text) >= 0 && Convert.ToInt32(textBox4.Text) >= 0)
                    {
                        doctors_array[CounterD] = new Doctor(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text));
                        m.add_doctor(doctors_array[CounterD]);
                        listBox3.Items.Add($"{textBox1.Text}, {textBox2.Text}, {textBox3.Text}, {textBox4.Text}");
                        CounterD++;
                    }
                    else MessageBox.Show("Координаты и скорость доктора не должны быть отрицательными");
                    break;
                }
            }
            catch
            {
                MessageBox.Show("Введите координаты или скорость доктора");
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e) //удаление человека
        {
            try
            {
                if (CounterH > 0)
                {
                    m.delete_h(listBox2.SelectedIndex + 1);
                    listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                    CounterH--;
                }
                else MessageBox.Show("no Humans");
            }
            catch
            {

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (CounterD > 0)
                {
                    m.delete_d(listBox3.SelectedIndex + 1);
                    listBox3.Items.RemoveAt(listBox3.SelectedIndex);
                    CounterD--;
                }
                else MessageBox.Show("no Doctors");
            }
            catch
            {

            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m.Save();
            DataContractSerializer jsonF1 = new DataContractSerializer(typeof(Model));
            using (FileStream fileStream1 = new FileStream("ModelSave.json", FileMode.OpenOrCreate))
            {
                jsonF1.WriteObject(fileStream1, m);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            m.Load();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            for (int i=0; i<m.Humans.Count; i++)
            {
                listBox2.Items.Add($"{m.Humans[i].X}, {m.Humans[i].Y}, {m.Humans[i].SpeedOX}, {m.Humans[i].SpeedOY}, {m.Humans[i].Condition}");
                CounterH++;
            }
            for (int i = 0; i < m.Doctors.Count; i++)
            {
                listBox3.Items.Add($"{m.Doctors[i].X}, {m.Doctors[i].Y}, {m.Doctors[i].SpeedOX}, {m.Doctors[i].SpeedOY}");
                CounterD++;
            }
            
        }
    }
    [DataContract]
    public class People
    {
        [DataMember]
        private int x;
        [DataMember]
        private int y;
        [DataMember]
        private int s;
        [DataMember]
        private int speedOX;
        [DataMember]
        private int speedOY;


        public int X
        {
            get { return x; }
            set
            {
                if (value < 0) { throw new Exception("Error x"); }
                x = value;
            }
        }
        public int Y
        {
            get { return y; }
            set
            {
                if (value < 0) { throw new Exception("Error y"); }
                y = value;
            }
        }
        public int SpeedOX
        {
            get { return speedOX; }
            set
            {
                if (value < 0) { throw new Exception("Error speedOX"); }
                speedOX = value;
            }
        }
        public int SpeedOY
        {
            get { return speedOY; }
            set
            {
                if (value < 0) { throw new Exception("Error speedOY"); }
                speedOY = value;
            }
        }
        public int S
        {
            get
            {
                return s;
            }
            set
            {
                if (value == 0 || value == 1) { s = value; }
            }
        }
        public virtual void Update(int t, int h, int w)
        {
            int a = x + t * speedOX; // получение новой координаты x
            int b = y + t * speedOY; // получение новой координаты y
            if (0 < a && a < w)
                x = a;
            else
                Console.WriteLine("Выход за пределы поля");
            if (0 < b && b < h)
                y = b;
            else
                Console.WriteLine("Выход за пределы поля");
        }
    };
    [DataContract]
    public class Human : People
    {
        [DataMember] private string condition;
        public string Condition
        {
            get { return condition; }
            set { condition = value; }
        }
        public Human(int xn = 0, int yn = 0, int speedX = 5, int speedY = 5, string c = "Здоров")
        {
            try
            {
                this.X = xn;

            }
            catch
            {
                this.X = 0;
            }

            try
            {
                this.Y = yn;
            }
            catch
            {
                this.Y = 0;
            }

            try
            {
                this.SpeedOX = speedX;

            }
            catch
            {
                this.SpeedOX = 5;
            }

            try
            {
                this.SpeedOY = speedY;

            }
            catch
            {
                this.SpeedOY = 5;
            }
            try
            {
                this.Condition = c;
            }
            catch
            {
                this.Condition = "Здоров";
            }
        }

        public Human() { }
        public override void Update(int t, int h, int w)
        {
            if (S == 1) // Проверка инкубационного периода 
            {
                S = 0;
                Condition = "Болен";
            }
            int a = X + t * SpeedOX; // получение новой координаты x
            int b = Y + t * SpeedOY; // получение новой координаты y
            if (0 < a && a < w)
                X = a;
            else
                Console.WriteLine("Выход за пределы поля");
            if (0 < b && b < h)
                Y = b;
            else
                Console.WriteLine("Выход за пределы поля");
        }
        public Human cure(Human h, int xd, int yd, int rast)
        {
            int a = Convert.ToInt32(Math.Sqrt(Math.Pow(X - xd, 2) + Math.Pow(Y - yd, 2)));
            if (a <= rast)
                h.Condition = "Здоров"; // излечение человека
            return h;
        }
        public Human infection(Human h, int r)
        {
            int a = Convert.ToInt32(Math.Sqrt(Math.Pow(X - h.X, 2) + Math.Pow(Y - h.Y, 2)));
            if (Condition == "Болен" && a <= r) // проверка возможности заражения
                h.S = 1; // начинается инкубационный период
            return h;
        }
    };
    [DataContract]
    public class Doctor : People
    {
        public Doctor(int xn = 0, int yn = 0, int speedX = 5, int speedY = 5)
        {
            try
            {
                this.X = xn;
            }
            catch
            {
                this.X = 0;
            }
            try
            {
                this.Y = yn;
            }
            catch
            {
                this.Y = 0;
            }
            try
            {
                this.SpeedOX = speedX;

            }
            catch
            {
                this.SpeedOX = 0;
            }
            try
            {
                this.SpeedOY = speedY;
            }
            catch
            {
                this.SpeedOY = 0;
            }
        }
        public Human cure(Human h, int r)
        {
            return h.cure(h, X, Y, r);
        }
    };
    [DataContract]
    public class Model
    {
        public List<Human> Humans { get;set; }
        public List<Doctor> Doctors { get; set; }

        [DataMember] private int height = 100;
        [DataMember] private int width = 100;
        [DataMember] private int rast = 10;
        private string[] ret;

        public Model(int h = 100, int w = 100, int r = 10)
        {
            rast = r;
            height = h;
            width = w;

            Humans = new List<Human>();
            Doctors = new List<Doctor>();

        }
        public int Rast
        {
            set
            {
                if (value > 0) rast = value;
            }
        }
        public int Height
        {
            set
            {
                if (value > 0) height = value;
            }
        }
        public int Width
        {
            set
            {
                if (value > 0) width = value;
            }
        }
        public void Save()
        {
            string humanSave = JsonConvert.SerializeObject(Humans);
            string doctorSave = JsonConvert.SerializeObject(Doctors);

            DataContractSerializer jsonF = new DataContractSerializer(typeof(Model));
            using (FileStream fileStream = new FileStream("HumasSave.json", FileMode.Create))
                jsonF.WriteObject(fileStream, humanSave);

            using (FileStream fileStream = new FileStream("DoctorsSave.json", FileMode.Create))
                jsonF.WriteObject(fileStream, doctorSave);

        }
        public void Load()
        {
            DataContractSerializer jsonF2 = new DataContractSerializer(typeof(Model));
            using (FileStream fileStream2 = new FileStream("HumasSave.json", FileMode.Open))
            {
                var loadpeople = JsonConvert.DeserializeObject<List<Human>>((string)jsonF2.ReadObject(fileStream2));
                Humans = loadpeople;
            }
            DataContractSerializer jsonF1 = new DataContractSerializer(typeof(Model));
            using (FileStream fileStream1 = new FileStream("DoctorsSave.json", FileMode.Open))
            {
                var loadpeople = JsonConvert.DeserializeObject<List<Doctor>>((string)jsonF1.ReadObject(fileStream1));
                Doctors = loadpeople;
            }

        }
        public void add_human(Human h)
        {
            Humans.Add(h);
        }

        public int count_h()
        {
            return Humans.Count;
        }
        public Human get_h(int n)
        {
            if (n > Humans.Count || n < 0)
            {
                Console.WriteLine("Недопустимое значение индекса");
                return new Human();
            }
            else return Humans[n];

        }
        public void delete_h(int n)
        {
            if (n - 1 > Humans.Count || n - 1 < 0)
            {
                Console.WriteLine("Недопустимое значение индекса");
            }
            else
                Humans.RemoveAt(n - 1);
        }
        public void add_doctor(Doctor d)
        {
            Doctors.Add(d);
        }
        public int count_d()
        {
            return Doctors.Count;
        }
        public Doctor get_d(int n)
        {
            if (n > Doctors.Count || n < 0)
            {
                Console.WriteLine("Недопустимое значение индекса");
                return new Doctor();
            }
            else return Doctors[n];

        }
        public void delete_d(int n)
        {
            if (n - 1 > Doctors.Count || n - 1 < 0)
            {
                Console.WriteLine("Недопустимое значение индекса");
            }
            else
                Doctors.RemoveAt(n - 1);
        }
        public void update(int t)
        {
            for (int i = 0; i < Humans.Count; i++)
            {
                Humans[i].Update(t, height, width); // обновление положения человека
            }
            for (int i = 0; i < Humans.Count; i++)
            {
                for (int j = 0; j < Humans.Count; j++)
                    if (j != i)
                        Humans[j] = Humans[i].infection(Humans[j], rast); // проверка возможного заражения
            }
            for (int i = 0; i < Doctors.Count; i++)
            {
                Doctors[i].Update(t, height, width); // обновление положения доктора
            }
            for (int i = 0; i < Doctors.Count; i++)
            {
                for (int j = 0; j < Humans.Count; j++)
                    Humans[j] = Doctors[i].cure(Humans[j], rast); // проверка возможности излечения человека
            }
        }
        public string[] PrintH()
        {
            ret = new string[Humans.Count + 1];
            for (int i = 0; i < Humans.Count; i++)
            {
                ret[i] = ($"Координаты {i + 1} человека: {Humans[i].X} {Humans[i].Y}"+ $". Состояние: {Humans[i].Condition}");
            }
            ret[Humans.Count] = Convert.ToString(Humans.Count);
            return ret;
        }
        public string[] PrintD()
        {
            ret = new string[Doctors.Count + 1];
            for (int i = 0; i < Doctors.Count; i++)
            {
                ret[i] = ($"Координаты {i + 1} доктора: {Doctors[i].X} {Doctors[i].Y}");
            }
            ret[Doctors.Count] = Convert.ToString(Doctors.Count);
            return ret;
        }
    };
}
