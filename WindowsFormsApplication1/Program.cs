using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace WindowsFormsApplication1
{
	static class Program
	{

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern ushort GlobalAddAtom(string lpString);

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern ushort GlobalFindAtom(string lpString);

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern ushort GlobalDeleteAtom(ushort atom);


		[STAThread]
		static void Main()
		{
			string atomStr = Application.ProductName + Application.ProductVersion + "1";

			ushort Atom = GlobalFindAtom(atomStr);
			
			//Atom = GlobalDeleteAtom(Atom);
			
			if (Atom > 0)
			{
				MessageBox.Show("You are trying to open second instance of the App! This instance will be closed.");
			}
			else
			{

				Atom = GlobalAddAtom(atomStr);

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new Form1());

				GlobalDeleteAtom(Atom);
			}
		}
	}
	
}

