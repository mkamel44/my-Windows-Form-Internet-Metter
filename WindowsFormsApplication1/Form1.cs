using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Media;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;

namespace WindowsFormsApplication1
{
	public partial class Form1 : Form
	{
		private Form3 aboutus =new Form3();
		
		private NetworkInterface nic;

		private long bytesSentSpeed;

		private long bytesReceivedSpeed;

		private double new_int = 15000;

		private double mycount = 0;
		
		private string soundName = "";

		private bool boo = false;

		private int count = 0;

		private int doh = 0;

		private string connName = "";

		private SoundPlayer simpleSound = new SoundPlayer();
		
		private int proxy_counter = 0;
		
		private int update_counter = 0;
		
		private double new_data = 0;

		public Form1()
		{
			
			InitializeComponent();
			
			kamel_Metter.Properties.Settings.Default.Upgrade();
			
			soundName = Path.GetDirectoryName(Application.ExecutablePath) + "\\alert.wav";

			textBox3.Text = soundName;

			button8_Click(null, null);

			loadData();
			
			getDataGrid();
			
		}
		
		private double ConvertBytesToMegabytes(double bytes)
		{
			return (bytes / (1024 * 1024));
		}

		private double ConvertMegabytesToBytes(double megaBytes)
		{
			return (megaBytes * (1024 * 1024.0));
		}

		
		private void timer1_Tick(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(connName))
			{

				NetworkInterface[] nicArr = NetworkInterface.GetAllNetworkInterfaces();

				bool booo = true;

				for (int i = 0; i < nicArr.Length; i++)
				{
					if (nicArr[i].Name.StartsWith(connName))
					{
						if (!label7.Text.Equals(nicArr[i]))
						{
							nic = nicArr[i];

							label7.Text = nic.Name;

							booo = false;
						}

					}
				}

				if (booo)
				{
					emty();
				}

			}
			else
			{
				emty();
			}
			
			
			if(checkBox1.Checked == true)
			{
				if(proxy_counter == 30000)
				{
					RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
					
					registry.SetValue("ProxyServer", 1);

					registry.SetValue("ProxyEnable", 0);
					
					proxy_counter=0;
				}
				else
				{
					++proxy_counter;
				}
			}
			
			if(checkBox2.Checked == true)
			{
				if(update_counter == 30000)
				{
					RegistryKey registry = Registry.CurrentUser.OpenSubKey("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate\\Auto Update", true);
					
					registry.SetValue("AUOptions", 1);
					
					update_counter=0;
				}
				else
				{
					++update_counter;
				}
			}
			
			
		}

		private void UpdateNetworkInterface()
		{
			try
			{
				if (nic != null)
				{

					IPv4InterfaceStatistics interfaceStats = nic.GetIPv4Statistics();

					bytesSentSpeed = (long)(interfaceStats.BytesSent - double.Parse(lblBytesSent.Text)) / 1024;
					bytesReceivedSpeed = (long)(interfaceStats.BytesReceived - double.Parse(lblBytesReceived.Text)) / 1024;

					lblBytesReceived.Text = interfaceStats.BytesReceived.ToString();
					lblBytesSent.Text = interfaceStats.BytesSent.ToString();
					lblUpload.Text = bytesSentSpeed.ToString();
					lblDownload.Text = bytesReceivedSpeed.ToString();

					
					new_data = mycount + long.Parse(lblBytesSent.Text) + long.Parse(lblBytesReceived.Text);
					
					if(new_data > ((double)kamel_Metter.Properties.Settings.Default["all"]) )
					{
						label28.Text = ConvertBytesToMegabytes(new_data).ToString("00.00");
						
						kamel_Metter.Properties.Settings.Default["all"] = new_data;
						
						kamel_Metter.Properties.Settings.Default.Save();
					}
				}

			}
			catch (Exception e)
			{
				System.Console.WriteLine("a2 " + e);
			}
		}

		public void emty()
		{
			label7.Text = "الرجاء اختيار اسم الاتصال لكي يتم الاتصال معه وتحليل الترفيك";
			lblBytesReceived.Text = "0";
			lblBytesSent.Text = "0";
			lblDownload.Text = "0";
			lblUpload.Text = "0";
			
			label28.Text = "00.00";
			
			mycount = (double)kamel_Metter.Properties.Settings.Default["all"];
			
			label34.Text = ConvertBytesToMegabytes(mycount).ToString("00.00");
			
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			UpdateNetworkInterface();
		}


		public void todo()
		{
			try
			{

				if (count <= 0)
				{
					count = 50;

					notifyIcon1.ShowBalloonTip(50000000);

					try
					{
						simpleSound.SoundLocation = soundName;

						simpleSound.Play();
					}
					catch(Exception eee)
					{
						System.Console.WriteLine("a30 " + eee);
					}
					
					doNow();
				}
				else
				{
					--count;
				}
			}
			catch (Exception e)
			{
				System.Console.WriteLine("a1 " + e);
			}
		}

		private void notifyIcon1_DoubleClick(object sender, EventArgs e)
		{
			if(WindowState != FormWindowState.Normal)
			{
				Show();
				WindowState = FormWindowState.Normal;
			}
			else
			{
				Hide();
				this.WindowState = FormWindowState.Minimized;
			}
		}

		private void Form1_Resize_1(object sender, EventArgs e)
		{
			if (FormWindowState.Minimized == WindowState)
			{
				Hide();
				this.WindowState = FormWindowState.Minimized;
			}
		}


		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				new_int = double.Parse(textBox1.Text.Trim());

				kamel_Metter.Properties.Settings.Default["new_int"] =  new_int;

				connName = comboBox2.Text.ToString();

				label25.Text = connName;

				kamel_Metter.Properties.Settings.Default["connName"] = connName.ToString();

				soundName = textBox3.Text.Trim();

				kamel_Metter.Properties.Settings.Default["soundName"] = soundName.ToString();

				doh = comboBox1.SelectedIndex;

				kamel_Metter.Properties.Settings.Default["doh"] = doh.ToString();

				kamel_Metter.Properties.Settings.Default.Save();

				button2.Enabled = true;

				button1.Enabled = false;
				
			}
			catch (Exception)
			{
				MessageBox.Show("الرجاء التأكد من المعطيات", "خطأ");
			}
		}
		
		public bool check_task_is_here()
		{
			
			TaskService ts = new TaskService();
			
			for(int i=0;i<ts.RootFolder.Tasks.Count;i++)
			{
				if(ts.RootFolder.Tasks[i].Name.Equals("kamel_Metter"))
				{
					return true;
				}
			}
			
			return false;
		}

		public void loadData()
		{
			try
			{
				
				checkBox1.Checked = bool.Parse( kamel_Metter.Properties.Settings.Default["proxy_saved"].ToString());
				
				checkBox2.Checked = bool.Parse( kamel_Metter.Properties.Settings.Default["update_saved"].ToString());
				
				if(check_task_is_here())
				{
					checkBox3.Checked = true;

				}
				
				new_int = (double) kamel_Metter.Properties.Settings.Default["new_int"];

				textBox1.Text = new_int.ToString();

				connName = kamel_Metter.Properties.Settings.Default["connName"].ToString();

				label25.Text = connName;
				
				mycount = (double) kamel_Metter.Properties.Settings.Default["all"];
				
				label34.Text = ConvertBytesToMegabytes(mycount).ToString("00.00");
				
				label28.Text = "00.00";

				comboBox2.Text = connName;

				soundName = kamel_Metter.Properties.Settings.Default["soundName"].ToString();

				textBox3.Text = soundName;

				doh = int.Parse(kamel_Metter.Properties.Settings.Default["doh"].ToString());

				comboBox1.SelectedIndex = doh;

				button2.Enabled = true;

				button1.Enabled = false;

				boo = bool.Parse(kamel_Metter.Properties.Settings.Default["boo"].ToString());

				if(!string.IsNullOrEmpty( label25.Text))
				{
					boo=true;
				}
				
				if (boo == true)
				{
					button2.Text = "ايقاف برنامج التحكم";
				}
				if (boo == false)
				{
					button2.Text = "تشغيل برنامج التحكم";
				}
				

			}
			catch (Exception e)
			{
				System.Console.WriteLine("a3 " + e);
			}
		}

		private void timer3_Tick(object sender, EventArgs e)
		{
			if (boo)
			{
				
				double all = (double)kamel_Metter.Properties.Settings.Default["all"];
				
				double uu = ConvertMegabytesToBytes(new_int);
				
				if (all > uu)
				{
					todo();
				}
			}

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			button1.Enabled = true;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			boo = !boo;

			kamel_Metter.Properties.Settings.Default["boo"] = boo.ToString();

			kamel_Metter.Properties.Settings.Default.Save();

			if (boo == true)
			{
				button2.Text = "ايقاف برنامج التحكم";
			}
			if (boo == false)
			{
				button2.Text = "تشغيل برنامج التحكم";
			}
			
			timer1.Enabled=boo;
			timer2.Enabled=boo;
			timer3.Enabled=boo;
			

		}


		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			button1.Enabled = true;
		}

		private void textBox3_TextChanged(object sender, EventArgs e)
		{
			button1.Enabled = true;
		}

		private void button6_Click(object sender, EventArgs e)
		{
			Hide();

			this.WindowState = FormWindowState.Minimized;
		}

		private void اغلاقالبرنامجToolStripMenuItem_Click(object sender, EventArgs e)
		{
			notifyIcon1_DoubleClick(null, null);
		}

		private void اظهارالبرنامجToolStripMenuItem_Click(object sender, EventArgs e)
		{
			aboutus.ShowDialog();
		}

		private void اغلاقالبرنامجToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			Application.ExitThread();
		}

		private void button7_Click(object sender, EventArgs e)
		{
			aboutus.ShowDialog();
		}

		public void doNow()
		{
			if (doh == 0 || doh == -1)
			{

			}
			else
				if (doh == 1)
			{
				int seconds = 100;
				ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.CreateNoWindow = false;
				startInfo.UseShellExecute = false;
				startInfo.WindowStyle = ProcessWindowStyle.Hidden;
				startInfo.FileName = "shutdown.exe";
				startInfo.Arguments = "/s /f /t " + seconds;
				startInfo.RedirectStandardOutput = true;
				startInfo.RedirectStandardError = true;
				Process p = Process.Start(startInfo);
				string outstring = p.StandardOutput.ReadToEnd();
				string errstring = p.StandardError.ReadToEnd();
				p.WaitForExit();

				doh = 0;

			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			button1.Enabled = true;
		}

		
		private void button8_Click(object sender, EventArgs e)
		{
			comboBox2.Items.Clear();

			NetworkInterface[] nicArr = NetworkInterface.GetAllNetworkInterfaces();


			for (int i = 0; i < nicArr.Length; i++)
			{
				comboBox2.Items.Add(nicArr[i].Name);
			}
		}


		private void button10_Click(object sender, EventArgs e)
		{
			TopMost = !TopMost;
			
			aboutus.TopMost = TopMost;
			
			button10.UseVisualStyleBackColor = !TopMost;
		}

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			button1.Enabled = true;
		}

		
		private void Form1_Shown(object sender, EventArgs e)
		{
			Hide();

			this.WindowState = FormWindowState.Minimized;
			
			dropDownPanel1.SwitchStatus();
			
			dropDownPanel2.SwitchStatus();
			
			dropDownPanel3.SwitchStatus();
			
			dropDownPanel1.StatusChanged += new ScrewTurn.DropDownPanel.DropDownPanelEventHandler(this.DropDownPanelStatusChanged);
			
			dropDownPanel2.StatusChanged += new ScrewTurn.DropDownPanel.DropDownPanelEventHandler(this.DropDownPanelStatusChanged);
			
			dropDownPanel3.StatusChanged += new ScrewTurn.DropDownPanel.DropDownPanelEventHandler(this.DropDownPanelStatusChanged);

		}

		
		void Button11Click(object sender, EventArgs e)
		{
			
			DialogResult res = MessageBox.Show("هل انت متأكد من تصفير الاستهلاك الأخير المخزن","تأكيد التصفير", MessageBoxButtons.YesNo);
			
			if(res == DialogResult.Yes)
			{
				
				addToData(ConvertBytesToMegabytes(mycount).ToString("00.00").ToString());
				
				mycount = 0;
				
				label34.Text = ConvertBytesToMegabytes(mycount).ToString("00.00");
				
				kamel_Metter.Properties.Settings.Default["all"] = 0d;
				
				kamel_Metter.Properties.Settings.Default.Save();
				
				getDataGrid();
				
			}
		}

		public void addToData(string data)
		{
			
			bool addOrNot = true;
			
			for(int i=0;i<((StringCollection)kamel_Metter.Properties.Settings.Default["a1"]).Count;i++)
			{
				string dateme = DateTime.Now.ToString("dd/MM/yyyy");
				
				if(dateme.Equals(((StringCollection)kamel_Metter.Properties.Settings.Default["a1"])[i].ToString()))
				{
					addOrNot = false;
					
					double b = double.Parse(((StringCollection)kamel_Metter.Properties.Settings.Default["a2"])[i].ToString());
					
					double c = double.Parse(data);
					
					((StringCollection)kamel_Metter.Properties.Settings.Default["a2"])[i] = (b + c).ToString();
					
					kamel_Metter.Properties.Settings.Default.Save();
				}
				
			}
			
			if(addOrNot)
			{
				
				((StringCollection)kamel_Metter.Properties.Settings.Default["a1"]).Add(DateTime.Now.ToString("dd/MM/yyyy"));
				
				((StringCollection)kamel_Metter.Properties.Settings.Default["a2"]).Add(data);
				
				((StringCollection)kamel_Metter.Properties.Settings.Default["a3"]).Add(DateTime.Now.DayOfWeek.ToString());

				kamel_Metter.Properties.Settings.Default.Save();
				
			}
			
		}
		
		public void getDataGrid()
		{
			dataGridView1.Rows.Clear();
			
			for(int i=0;i<((StringCollection)kamel_Metter.Properties.Settings.Default["a1"]).Count;i++)
			{
				string a1 = ((StringCollection)kamel_Metter.Properties.Settings.Default["a1"])[i].ToString();
				
				string a2 = ((StringCollection)kamel_Metter.Properties.Settings.Default["a2"])[i].ToString();
				
				string a3 = ((StringCollection)kamel_Metter.Properties.Settings.Default["a3"])[i].ToString();
				
				dataGridView1.Rows.Add(new string[]{ a2 , a1 , a3 });
			}
			
		}
		
		
		void TextBox3MouseEnter(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(textBox3,textBox3.Text);
		}
		
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{

			kamel_Metter.Properties.Settings.Default["proxy_saved"] =  checkBox1.Checked.ToString();

			kamel_Metter.Properties.Settings.Default.Save();
			
			if(checkBox1.Checked == true)
			{
				
				proxy_counter=0;
				
				RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
				
				registry.SetValue("ProxyServer", 1);

				registry.SetValue("ProxyEnable", 0);
			}
		}
		
		void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
			startInfo.FileName = "inetcpl.cpl";
			startInfo.Arguments = " ,4";
			process.StartInfo = startInfo;
			process.Start();
		}
		
		
		
		void Label28MouseEnter(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(label28,"ناتج هذا الرقم مجموع البايت المرسل + البايت المستلم + الاستهلاك الأخير");
		}
		
		void Label34MouseEnter(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(label34,"ناتج هذا الرقم اخر استهلاك للنت بعد اخر اطفاء الجهاز");
		}
		
		void LinkLabel2LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
			startInfo.FileName = "wuapp.exe";
			process.StartInfo = startInfo;
			process.Start();
		}
		
		void CheckBox2CheckedChanged(object sender, EventArgs e)
		{
			kamel_Metter.Properties.Settings.Default["update_saved"] =  checkBox2.Checked.ToString();

			kamel_Metter.Properties.Settings.Default.Save();
			
			if(checkBox2.Checked == true)
			{
				
				update_counter=0;
				
				RegistryKey registry = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate\\Auto Update",true);
				
				registry.SetValue("AUOptions", 1);
			}
		}
		
		void Button11MouseEnter(object sender, EventArgs e)
		{
			toolTip1.SetToolTip(button11,"تصفير الاستهلاك الأخير");
		}
		
		
		void CheckBox3MouseClick(object sender, MouseEventArgs e)
		{
			
			try
			{
				
				TaskService ts = new TaskService();
				
				ts.RootFolder.DeleteTask("kamel_Metter");
				
			}
			catch(Exception)
			{
				
			}
			
			if(checkBox3.Checked==true)
			{
				TaskService ts = new TaskService();

				TaskDefinition td = ts.NewTask();
				
				td.Actions.Add(new ExecAction(Application.ExecutablePath,null,null));
				
				td.Triggers.Add(new LogonTrigger());
				
				td.Principal.RunLevel = TaskRunLevel.Highest;
				
				td.Settings.StopIfGoingOnBatteries=false;
				
				td.Settings.DisallowStartIfOnBatteries=false;
				
				td.Settings.AllowHardTerminate=false;
				
				td.Settings.ExecutionTimeLimit=TimeSpan.Zero;
				
				ts.RootFolder.RegisterTaskDefinition("kamel_Metter", td);
			}
			
			
		}
		
		void Form1FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			
			Hide();

			this.WindowState = FormWindowState.Minimized;
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			DialogResult res = MessageBox.Show("هل انت متأكد من تصفير كامل البيانات المخزنة","تأكيد التصفير", MessageBoxButtons.YesNo);
			
			if(res == DialogResult.Yes)
			{
				
				((StringCollection)kamel_Metter.Properties.Settings.Default["a1"]).Clear();
				
				((StringCollection)kamel_Metter.Properties.Settings.Default["a2"]).Clear();
				
				((StringCollection)kamel_Metter.Properties.Settings.Default["a3"]).Clear();
				
				kamel_Metter.Properties.Settings.Default.Save();
				
				getDataGrid();

			}
		}
		
		
		void DropDownPanelStatusChanged(object sender, ScrewTurn.DropDownPanelEventArgs e)
		{
			
			
		}
	}
}