using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NESTExportaDB;

namespace NESTDataBaseTool
{
	public partial class cFormTool : Form
	{
		public cFormTool()
		{
			InitializeComponent();
		}

		public void cFormTool_Load(object sender, EventArgs e)
		{
			LerPropriedades();
		}

		public void cButtonGerarScripts_Click(object sender, EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				cButtonGerarScripts.Enabled = false;
				cBottonMigra.Enabled = false;
				ClienteBanco client;
				SalvaPropriedades();
				string mServe = cTextHostOrigem.Text;
				int mPort = int.Parse(cTextPortaOrigem.Text);
				string mUser = cTextUsuarioOrigem.Text;
				string mPass = cTextSenhaOrigem.Text;
				string mDBName = cTextDBOrigem.Text;
				string mDir = cTextDir.Text;

				CriaDiretorio(mDir + "Dados\\");

				//client = new ClienteBanco(DbTypes.PgSql, "201.20.7.33", 5890, "postgres", "TjYz3m", null, "DES_NESTRIS");
				client = new ClienteBanco(DbTypes.PgSql, mServe, mPort, mUser, mPass, null, mDBName);

				client.ScriptBanco(@mDir);
				client.ScriptProcedures(@mDir);
				client.ScriptDados(@mDir + "Dados\\");

				mServe = cTextHostDestino.Text;
				mPort = int.Parse(cTextPortaDestino.Text);
				mUser = cTextUsuarioDestino.Text;
				mPass = cTextSenhaDestino.Text;
				mDBName = cTextDBDestino.Text;

				//client = new ClienteBanco(DbTypes.PgSql, "201.20.7.33", 5890, "postgres", "TjYz3m", null, "DES_NESTRIS");
				client = new ClienteBanco(DbTypes.PgSql, mServe, mPort, mUser, mPass, null, mDBName);
				client.ScriptBanco(@mDir);
				client.ScriptProcedures(@mDir);
				cButtonGerarScripts.Enabled = true;
				cBottonMigra.Enabled = true;
				this.Cursor = Cursors.Default;

			}
			catch (Exception ex)
			{
				MessageBox.Show("Erro ao Construir Scripts: " + Environment.NewLine + ex.ToString());
				cButtonGerarScripts.Enabled = true;
				cBottonMigra.Enabled = true;
				this.Cursor = Cursors.Default;
			}
		}

		public void cButtonMigra_Click(object sender, EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				cButtonGerarScripts.Enabled = false;
				cBottonMigra.Enabled = false;
				ClienteBanco client;
				SalvaPropriedades();

				string mServe = cTextHostDestino.Text;
				int mPort = int.Parse(cTextPortaDestino.Text);
				string mUser = cTextUsuarioDestino.Text;
				string mPass = cTextSenhaDestino.Text;
				string mDBName = cTextDBDestino.Text;
				string mDir = cTextDir.Text;

				CriaDiretorio(mDir + "Dados\\");

				//client = new ClienteBanco(DbTypes.PgSql, "201.20.7.33", 5890, "postgres", "TjYz3m", null, "DES_NESTRIS");
				client = new ClienteBanco(DbTypes.PgSql, mServe, mPort, mUser, mPass, null, mDBName);
				client.ImportaDados(@mDir  + "Dados\\");

				cButtonGerarScripts.Enabled = true;
				cBottonMigra.Enabled = true;
				this.Cursor = Cursors.Default;

			}
			catch (Exception ex)
			{
				MessageBox.Show("Erro ao Importar Dados " + Environment.NewLine + ex.ToString());
				cButtonGerarScripts.Enabled = true;
				cBottonMigra.Enabled = true;
				this.Cursor = Cursors.Default;
			}
		}
		private void SalvaPropriedades()
		{
			Settings.Default.HostOrigem = cTextHostOrigem.Text;
			Settings.Default.HostDestino = cTextHostDestino.Text;
			Settings.Default.UsuarioOrigem = cTextUsuarioOrigem.Text;
			Settings.Default.UsuarioDestino = cTextUsuarioDestino.Text;
			Settings.Default.SenhaOrigem = cTextSenhaOrigem.Text;
			Settings.Default.SenhaDestino = cTextSenhaDestino.Text;
			Settings.Default.PortaOrigem = cTextPortaOrigem.Text;
			Settings.Default.PortaDestino = cTextPortaDestino.Text;
			Settings.Default.DBOrigem = cTextDBOrigem.Text;
			Settings.Default.DBDestino = cTextDBDestino.Text;
			Settings.Default.Diretorio = cTextDir.Text;

			Settings.Default.Save();

		}

		private void LerPropriedades()
		{

			cTextHostOrigem.Text = Settings.Default.HostOrigem;
			cTextHostDestino.Text = Settings.Default.HostDestino;
			cTextUsuarioOrigem.Text = Settings.Default.UsuarioOrigem;
			cTextUsuarioDestino.Text = Settings.Default.UsuarioDestino;
			cTextSenhaOrigem.Text = Settings.Default.SenhaOrigem;
			cTextSenhaDestino.Text = Settings.Default.SenhaDestino;
			cTextPortaOrigem.Text = Settings.Default.PortaOrigem;
			cTextPortaDestino.Text = Settings.Default.PortaDestino;
			cTextDBOrigem.Text = Settings.Default.DBOrigem;
			cTextDBDestino.Text = Settings.Default.DBDestino;
			cTextDir.Text = Settings.Default.Diretorio;


		}

		private void CriaDiretorio(string path)
		{
			bool folderExists = System.IO.Directory.Exists(path);
			if (!folderExists)
				System.IO.Directory.CreateDirectory(path);
		}
	}
}
