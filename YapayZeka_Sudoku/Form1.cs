using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YapayZeka_Sudoku
{
    public partial class Form1 : Form
    {
        public static int satir, sutun, bolgesatir_min, bolgesatir_max, bolgesutun_min, bolgesutun_max, hucre, hata = 0, sayac, blok = 0;
        public static int[,] degermatrisi = new int[81, 10];
        public static int[,] yedeksudoku = new int[9, 9];
        public static int[,] Deger_Matris_index = new int[81, 3];
        public static int[,] sudoku = new int[9, 9];
        public TextBox[,] SudokuTahtasi { get; set; }
        public Form1()
        {
            
            InitializeComponent();
            this.SudokuArayuz();
            this.CozumButonu = new System.Windows.Forms.Button();
        }

        public void SudokuArayuz()//ARAYUZ OLUSTURULUYOR
        {
           
            int index = 0; int konum_x = 50, konum_y = 30, bosluk = 0, kontrol;
            SudokuTahtasi = new TextBox[9, 9];//
            for (int i = 0; i < 9; i++)
            {
                konum_x = 50;
                for (int j = 0; j < 9; j++)
                {
                    if ((int)(j % 3) == 2)
                    {
                        bosluk = 6;
                    }
                    else
                    {
                        bosluk = 0;
                    }
                    TextBox YeniHucre = new TextBox();
                    YeniHucre.Name = "textbox" + index;
                    YeniHucre.TabIndex = index;
                    YeniHucre.Width = 30;
                    YeniHucre.Height = 20;
                    YeniHucre.TextAlign = HorizontalAlignment.Center;
                    YeniHucre.Location = new Point(konum_x, konum_y);
                    YeniHucre.TextChanged += new EventHandler(this.TextChanged);

                    konum_x = konum_x + 31 + bosluk;
                    this.Controls.Add(YeniHucre);
                    SudokuTahtasi[i, j] = YeniHucre;
                    index++;
                }
                if ((int)(i % 3) == 2)
                {
                    bosluk = 6;
                }
                else
                {
                    bosluk = 0;
                }
                konum_y = konum_y + 21 + bosluk;
            }

        }//SudokuArayuz() bıtıs



        private void TextChanged(object sender, EventArgs e)
        {

            int aktif = 0, i;
            int index = this.Controls.Count;

            for (i = 0; i < index; i++)
            {

                if (this.Controls[i] is TextBox)//TUM TEXTBOXLARI ARA
                {

                    if (this.ActiveControl == this.Controls[i])//AKTİF HUCREY'I BUL
                    {
                        int kontrol = sudokuhatakontrol(this.Controls[i].TabIndex);//AKTIF HUCRE ICINDEKİ DEGER CAKISMAYA NEDEN OLUYORMU?
                        if (kontrol == 1)//CAKISMA VAR.O ZAMAN KIRMIZI RENKLE KULLANICIYI UYAR
                        {
                            this.Controls[i].ForeColor = Color.Red;
                            MessageBox.Show("Girmiş oldugunuz deger kullanilamaz baska bir deger seciniz(SATIR/SUTUN YADA BOLGEDE MEVCUT)");
                            this.Controls[i].Text = "";
                            this.Controls[i].ForeColor = Color.Empty;
                        }
                        else//CAKISMA YOKSA GIRILEN DEGERI KABUL ET
                        {

                        }
                    }


                }

            }


        }

        int sudokuhatakontrol(int indeks)//İNDEX'in bulunduğu SATIR,SUTUN veya BOLGEDE AYNI DEGER VARMI?
        {
            int i, j, k, m;
            hucre = indeks;
            satir = hucre / 9;
            sutun = hucre % 9;
            bolgesatir_min = 3 * (satir / 3);
            bolgesatir_max = bolgesatir_min + 2;
            bolgesutun_min = 3 * (sutun / 3);
            bolgesutun_max = bolgesutun_min + 2;
            hata = 0;
           if (SudokuTahtasi[satir, sutun].Text !="")
            {
                for (j = 0; j < 9; j++)
                {
                    if ((sutun != j) && (SudokuTahtasi[satir, j]).Text == (SudokuTahtasi[satir, sutun].Text))// SATIRDA AYNI DEGERDE OLAN BASKA HUCRE VARMI?VARSA CAKISMA VARDIR
                    {
                        
                        hata = 1;
                    }

                    if ((satir != j) && (SudokuTahtasi[j, sutun].Text) == (SudokuTahtasi[satir, sutun].Text))// SUTUNDA AYNI DEGERDE OLAN BASKA HUCRE VARMI?VARSA CAKISMA VARDIR
                    {
                       
                        hata = 1;
                    }


                }
                for (k = bolgesatir_min; k <= bolgesatir_max; k++)//BOLGE KONTROLU BASLIYOR
                {
                    for (m = bolgesutun_min; m <= bolgesutun_max; m++)
                    {
                        if (satir == k && sutun == m)//AYNI HUCREYSE BIRSEY YAPMA
                        {
                            
                        }
                        else//FARKLI HUCREYLE KARSILASTIRILIYORSA
                        {
                            if ((SudokuTahtasi[k, m].Text == SudokuTahtasi[satir, sutun].Text))//BU DEGERLER AYNIYSA CAKISMA VARDIR
                            {
                              
                                hata = 1;
                            }

                        }

                    }
                }


            }
            
            return hata;
        }//sudokuhatakontrol(int indeks) Bitis noktası



        void BaslangicAyari()
        {
            int i, j, k, m;
            for (i = 0; i < 81; i++)
            {
                degermatrisi[i, 0] = 1;
                degermatrisi[i, 1] = 2;
                degermatrisi[i, 2] = 3;
                degermatrisi[i, 3] = 4;
                degermatrisi[i, 4] = 5;
                degermatrisi[i, 5] = 6;
                degermatrisi[i, 6] = 7;
                degermatrisi[i, 7] = 8;
                degermatrisi[i, 8] = 9;
                degermatrisi[i, 9] = 9;
                hucre = i;
                satir = hucre / 9;
                sutun = hucre % 9;
                if (SudokuTahtasi[satir, sutun].Text!="")
                {
                    sudoku[satir, sutun] = Convert.ToInt32(SudokuTahtasi[satir, sutun].Text);
                }
                else
                {
                    sudoku[satir, sutun] = 0;
                }

                yedeksudoku[satir, sutun] = sudoku[satir, sutun];
            }
            Forward_Checking_DegerKontrolu();
        }





        void Forward_Checking_DegerKontrolu()
        {
            int i, j, k, m, x;
            for (x = 0; x < 81; x++)
            {
                hucre = x;
                satir = hucre / 9;
                sutun = hucre % 9;
                if (sudoku[satir, sutun] != 0)
                {
                    degermatrisi[hucre, 0] = -1;
                    degermatrisi[hucre, 1] = -1;
                    degermatrisi[hucre, 2] = -1;
                    degermatrisi[hucre, 3] = -1;
                    degermatrisi[hucre, 4] = -1;
                    degermatrisi[hucre, 5] = -1;
                    degermatrisi[hucre, 6] = -1;
                    degermatrisi[hucre, 7] = -1;
                    degermatrisi[hucre, 8] = -1;
                    degermatrisi[hucre, 9] = 0;

                }
               // for (i = 0; i < 81; i++)
               // {
                i=x;
                    hucre = i;
                    satir = hucre / 9;
                    sutun = hucre % 9;
                    if (yedeksudoku[satir, sutun] != 0)//ilk deger atanan hücreyse
                    {

                        for (j = 0; j < 9; j++)//
                        {

                            if (degermatrisi[satir * 9 + j, sudoku[satir, sutun] - 1] == sudoku[satir, sutun])//hucreye ait satır kontrolu yap ve degerleri check et
                            {
                                degermatrisi[satir * 9 + j, sudoku[satir, sutun] - 1] = -1;
                                degermatrisi[satir * 9 + j, 9]--;
                            }

                            if (degermatrisi[j * 9 + sutun, sudoku[satir, sutun] - 1] == sudoku[satir, sutun])//hucreye ait sutun kontrolu yap ve degerleri check et
                            {
                                degermatrisi[j * 9 + sutun, sudoku[satir, sutun] - 1] = -1;
                                degermatrisi[j * 9 + sutun, 9]--;
                            }

                        }

                        bolgesatir_min = 3 * (satir / 3);
                        bolgesatir_max = bolgesatir_min + 2;
                        bolgesutun_min = 3 * (sutun / 3);
                        bolgesutun_max = bolgesutun_min + 2;

                        for (k = bolgesatir_min; k <= bolgesatir_max; k++)
                        {
                            for (m = bolgesutun_min; m <= bolgesutun_max; m++)
                            {
                                if (satir == k && sutun == m)//ayny hucredaki de?erse bir?ey yapma
                                {

                                }
                                else//farkly hucredaki de?erse
                                {
                                    //if((sudoku[k,m]==sudoku[satir,sutun]))//bu de?erler aynyysa
                                    if (degermatrisi[k * 9 + m, sudoku[satir, sutun] - 1] == sudoku[satir, sutun])
                                    {
                                        degermatrisi[k * 9 + m, sudoku[satir, sutun] - 1] = -1;
                                        degermatrisi[k * 9 + m, 9]--;
                                    }

                                }

                            }
                        }

                    }
                    Deger_Matris_index[i, 0] = i;//Deger matrisin indisi ve kalan eleman sayısı belirlendi;
                    Deger_Matris_index[i, 1] = degermatrisi[i, 9];



                //}


            }
        }//Forward_Checking_DegerKontrolu() Bitis

        private void CozumButonu_Click(object sender, EventArgs e)
        {
            int i, j,kontrol;
            BaslangicAyari();
            komsulukkontrol();
        dongu2:
            Forward_Checking();
            sayac = 0;
            for (i = 0; i < 9; i++)
            {
               
                for (j = 0; j < 9; j++)
                {
                
                    if (sudoku[i, j] == 0 )
                    {
                        sayac++;
                    }
                }
              
            }

            if (sayac > 0)
            {
                deneme();
                goto dongu2;
            }

            
            for (i = 0; i < 9; i++)
            {
             
                for (j = 0; j < 9; j++)
                {

                    SudokuTahtasi[i, j].Text = Convert.ToString(sudoku[i, j]);
                
                }

            }
        }


        void Forward_Checking_DegerEkleme(int hucre, int deger)
        {


            int i, j, k, m;
            satir = hucre / 9;
            sutun = hucre % 9;
            if (sudoku[satir, sutun] == 0)
            {

                sudoku[satir, sutun] = deger;

                int kontrol = sudokuhatakontrol(hucre);
                if (kontrol == 0)//girilen deger çakışmaya neden olmuyorsa devam et
                {
                    degermatrisi[hucre, 0] = -1;
                    degermatrisi[hucre, 1] = -1;
                    degermatrisi[hucre, 2] = -1;
                    degermatrisi[hucre, 3] = -1;
                    degermatrisi[hucre, 4] = -1;
                    degermatrisi[hucre, 5] = -1;
                    degermatrisi[hucre, 6] = -1;
                    degermatrisi[hucre, 7] = -1;
                    degermatrisi[hucre, 8] = -1;
                    degermatrisi[hucre, 9] = 0;
                    SudokuTahtasi[satir, sutun].Text = Convert.ToString(deger);


                    for (j = 0; j < 9; j++)//
                    {

                        if (degermatrisi[satir * 9 + j, sudoku[satir, sutun] - 1] == sudoku[satir, sutun])//hucreye ait satır kontrolu yap ve degerleri check et
                        {
                            degermatrisi[satir * 9 + j, sudoku[satir, sutun] - 1] = -1;
                            degermatrisi[satir * 9 + j, 9]--;
                        }

                        if (degermatrisi[j * 9 + sutun, sudoku[satir, sutun] - 1] == sudoku[satir, sutun])//hucreye ait sutun kontrolu yap ve degerleri check et
                        {
                            degermatrisi[j * 9 + sutun, sudoku[satir, sutun] - 1] = -1;
                            degermatrisi[j * 9 + sutun, 9]--;
                        }

                    }

                    bolgesatir_min = 3 * (satir / 3);
                    bolgesatir_max = bolgesatir_min + 2;
                    bolgesutun_min = 3 * (sutun / 3);
                    bolgesutun_max = bolgesutun_min + 2;

                    for (k = bolgesatir_min; k <= bolgesatir_max; k++)
                    {
                        for (m = bolgesutun_min; m <= bolgesutun_max; m++)
                        {
                            if (satir == k && sutun == m)//ayny hucredaki de?erse bir?ey yapma
                            {

                            }
                            else//farkly hucredaki de?erse
                            {
                                if (degermatrisi[k * 9 + m, sudoku[satir, sutun] - 1] == sudoku[satir, sutun])
                                {
                                    degermatrisi[k * 9 + m, sudoku[satir, sutun] - 1] = -1;
                                    degermatrisi[k * 9 + m, 9]--;
                                }

                            }

                        }
                    }
                
                }
                else //Girilen değer çakışmaya neden olduysa sil
                {
                    sudoku[satir, sutun] = 0;
                    SudokuTahtasi[satir, sutun].Text = "";
                }
            }
            else
            {
                //OFFprintf("\nDEGERI OLAN BIR HUCREYE YENI DEGER GIRMEYE CALISILIYOR");
            }

            for (i = 0; i < 81; i++)
            {
                if (hucre == Deger_Matris_index[i, 0])
                {
                    Deger_Matris_index[i, 1] = degermatrisi[hucre, 9];
                    //printf("\n%d odadan eksildi %d",hucre,Deger_Matris_indeks[i,1]);
                    break;
                }
            }
            //sirala();
        }




        void komsulukkontrol()
        {
            int i, j, k, m;
            int sayac = 0;
            for (i = 0; i < 81; i++)
            {
                sayac = 0;
                hucre = Deger_Matris_index[i, 0];
                satir = hucre / 9;
                sutun = hucre % 9;
                bolgesatir_min = 3 * (satir / 3);
                bolgesatir_max = bolgesatir_min + 2;
                bolgesutun_min = 3 * (sutun / 3);
                bolgesutun_max = bolgesutun_min + 2;




                for (j = 0; j < 9; j++)
                {
                    if (sutun != j && (sudoku[satir, j]) == 0)// satyrda ayny olan ba?ka bir yer varmy
                    {
                        sayac++;

                    }
                    if (satir != j && (sudoku[j, sutun]) == 0)// sutunda ayny olan ba?ka bir yer varmy
                    {
                        sayac++;
                    }


                }
                for (k = bolgesatir_min; k <= bolgesatir_max; k++)
                {
                    for (m = bolgesutun_min; m <= bolgesutun_max; m++)
                    {
                        if (satir == k && sutun == m)//ayny hucredaki de?erse bir?ey yapma
                        {

                        }
                        else//farkli hucredaki degerse
                        {
                            if ((sudoku[k, m] == 0))//bu degerler aynyysa
                            {
                                sayac++;
                            }

                        }

                    }
                }
                //printf("\n sayac%d",sayac);
                Deger_Matris_index[i, 2] = sayac;

            }

        }




        void sirala()
        {
            int i, j, k, m;
            int[] Yedek = new int[3];
            for (i = 0; i < 81; i++)
            {

                for (j = 0; j < 81; j++)
                {
                    if (Deger_Matris_index[i, 1] < Deger_Matris_index[j, 1])
                    {
                        Yedek[0] = Deger_Matris_index[i, 0];
                        Yedek[1] = Deger_Matris_index[i, 1];
                        Yedek[2] = Deger_Matris_index[i, 2];
                        Deger_Matris_index[i, 0] = Deger_Matris_index[j, 0];//indeks sayisi
                        Deger_Matris_index[i, 1] = Deger_Matris_index[j, 1];//hucrenin alabileceği deger sayisi
                        Deger_Matris_index[i, 2] = Deger_Matris_index[j, 2];//komsuluk sayisi(komşu olup 0'a eşit olanlarin sayisi)
                        Deger_Matris_index[j, 0] = Yedek[0];
                        Deger_Matris_index[j, 1] = Yedek[1];
                        Deger_Matris_index[j, 2] = Yedek[2];
                        // goto basla;
                    }
                }
            }
        }





        void deneme()
        {
            int i, j,k,m;

            for (i = 0; i < 81; i++)//DEGER ATANMAYAN HUCRE VARMI?
            {
                hucre = i;
                satir = hucre / 9;
                sutun = hucre % 9;
                bolgesatir_min = 3 * (satir / 3);
                bolgesatir_max = bolgesatir_min + 2;
                bolgesutun_min = 3 * (sutun / 3);
                bolgesutun_max = bolgesutun_min + 2;
                if (sudoku[satir, sutun] == 0)//EGER DEGER ATANMAMIS HUCRE VARSA ONCELIKLE BUNLARI ANAHTARLA(999 YAPTIM) SONRA BU HUCRENİN SATIRINI SUTUNU SIFIRLA
                {
                    for (j = 0; j < 9; j++)//
                    {
                        if (yedeksudoku[satir, j] == 0)
                        {
                            sudoku[satir, j] = 999;
                            degermatrisi[satir * 9 + j, 0] = 1;
                            degermatrisi[satir * 9 + j, 1] = 2;
                            degermatrisi[satir * 9 + j, 2] = 3;
                            degermatrisi[satir * 9 + j, 3] = 4;
                            degermatrisi[satir * 9 + j, 4] = 5;
                            degermatrisi[satir * 9 + j, 5] = 6;
                            degermatrisi[satir * 9 + j, 6] = 7;
                            degermatrisi[satir * 9 + j, 7] = 8;
                            degermatrisi[satir * 9 + j, 8] = 9;
                            degermatrisi[satir * 9 + j, 9] = 9;
                        }
                        if (yedeksudoku[j, sutun] == 0)
                        {
                            sudoku[j, sutun] = 999;
                            degermatrisi[j * 9 + sutun, 0] = 1;
                            degermatrisi[j * 9 + sutun, 1] = 2;
                            degermatrisi[j * 9 + sutun, 2] = 3;
                            degermatrisi[j * 9 + sutun, 3] = 4;
                            degermatrisi[j * 9 + sutun, 4] = 5;
                            degermatrisi[j * 9 + sutun, 5] = 6;
                            degermatrisi[j * 9 + sutun, 6] = 7;
                            degermatrisi[j * 9 + sutun, 7] = 8;
                            degermatrisi[j * 9 + sutun, 8] = 9;
                            degermatrisi[j * 9 + sutun, 9] = 9;
                        }

                        for (k = bolgesatir_min; k <= bolgesatir_max; k++)
                        {
                            for (m = bolgesutun_min; m <= bolgesutun_max; m++)
                            {

                                sudoku[k, m] = 999;
                                degermatrisi[k * 9 + m, 0] = 1;
                                degermatrisi[k * 9 + m, 1] = 2;
                                degermatrisi[k * 9 + m, 2] = 3;
                                degermatrisi[k * 9 + m, 3] = 4;
                                degermatrisi[k * 9 + m, 4] = 5;
                                degermatrisi[k * 9 + m, 5] = 6;
                                degermatrisi[k * 9 + m, 6] = 7;
                                degermatrisi[k * 9 + m, 7] = 8;
                                degermatrisi[k * 9 + m, 8] = 9;
                                degermatrisi[k * 9 + m, 9] = 9;

                            }
                        }


                    }
                }

            }
            for (i = 0; i < 81; i++)
            {
                hucre = i;
                satir = hucre / 9;
                sutun = hucre % 9;
                if (sudoku[satir, sutun] == 999)
                {
                    sudoku[satir, sutun] = 0;
                    //OFF printf("\n******************************************[%d,%d]=0", satir, sutun);
                }
            }
            Forward_Checking_DegerKontrolu();
        }




        void Forward_Checking()
        {
            int i, j, k, m;
        dongu1:
            int degersec = 0, sayac = 0;
            for (i = 0; i < 81; i++)
            {
                Deger_Matris_index[i, 0] = i;//ILGILI HUCREYI BELIRTIR
                Deger_Matris_index[i, 1] = degermatrisi[i, 9];//HUCREYE AIT 
            }
            sirala();
            komsulukkontrol();


            for (i = 0; i < 81; i++)
            {
                if (Deger_Matris_index[i, 1] != 0)
                {
                    degersec = Deger_Matris_index[i, 0];
                    //********************************** //sirala();
                    break;
                }

            }

            sayac = degermatrisi[degersec, 9];
            if (sayac > 0)
            {
                for (i = 0; i < 9; i++)
                {
                    if (degermatrisi[degersec, i] != -1)
                    {
                        //OFFprintf("\nHUCRE %d den ISTEDIGIM DEGER :%d", degersec, degermatrisi[degersec, i]);
                        hucre = degersec;
                        satir = hucre / 9;
                        sutun = hucre % 9;
                        if (sudoku[satir, sutun] == 0)
                        {

                            Forward_Checking_DegerEkleme(hucre, degermatrisi[degersec, i]);
                            goto dongu1;
                        }


                    }


                }

            }






        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.ActiveForm.Text = comboBox1.Text + "SUDOKUYU SISTEME BASARIYLA YUKLENDI";
            button1.Visible = false;
            if (String.IsNullOrEmpty(comboBox1.Text))
            {

            }
            else
            {
                int i, j;
                for (i = 0; i < 9; i++)
                {
                    // printf("\nHucre:%d\n",i);
                    for (j = 0; j < 9; j++)
                    {
                        SudokuTahtasi[i, j].Text = "";
                    }
                }
                string dosya_yolu = Environment.CurrentDirectory + @"\" + comboBox1.Text + ".txt";
                TxtOku(dosya_yolu);
            }
        }


        void TxtOku(string dosya_yolu)
        {

            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosyanın açılacağını,
            //3.parametre dosyaya erişimin veri okumak için olacağını gösterir.
            StreamReader sw = new StreamReader(fs);
            //Okuma işlemi için bir StreamReader nesnesi oluşturduk.
            int satirsayisi = 0;
            string yazi = sw.ReadLine();
            //satirsayisi++;
            int i = 0;
            while (yazi != null)
            {
                char[] ayrac = { ',' };
                string[] kelimeler = yazi.Split(ayrac);
                int j = 0;
                foreach (string okunan in kelimeler)
                {
                    if (okunan != "0")
                    {
                        SudokuTahtasi[i, j].Text = okunan;
                    }


                    j++;
                }
                i++;
                yazi = sw.ReadLine();
                satirsayisi++;

            }


            sw.Close();
            fs.Close();

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < 81; i++)
            {
               hucre = i;
                satir = hucre / 9;
                sutun = hucre % 9;
                SudokuTahtasi[satir, sutun].Text = "";
                comboBox1.Text = "Sudoku Seç";
            }
        }
      





    }//Form1 Bitis
}//YapayZeka_Sudoku Bitis
