using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;//��ӿؼ�������������ռ�
using System.Drawing;//���Point�������ռ�
using System.Collections;//ΪArrayList��������ռ�

namespace MagnetismForm
{
    class FrmClass
    {
        #region  ���Դ���-��������
        //��¼�������������ʾ
        public static bool Example_ListShow = false;
        public static bool Example_LibrettoShow = false;
        public static bool Example_ScreenShow = false;

        //��¼���ĵ�ǰλ��
        public static Point CPoint;  //��������ռ�using System.Drawing;
        public static Point FrmPoint;
        public static int Example_FSpace = 10;//���ô����ľ���

        //Frm_Play�����λ�ü���С
        public static int Example_Play_Top = 0;
        public static int Example_Play_Left = 0;
        public static int Example_Play_Width = 0;
        public static int Example_Play_Height = 0;
        public static bool Example_Assistant_AdhereTo = false;//���������Ƿ������һ��

        //Frm_ListBos�����λ�ü���С
        public static int Example_List_Top = 0;
        public static int Example_List_Left = 0;
        public static int Example_List_Width = 0;
        public static int Example_List_Height = 0;
        public static bool Example_List_AdhereTo = false;//���������Ƿ��������������һ��

        //Frm_Libretto�����λ�ü���С
        public static int Example_Libretto_Top = 0;
        public static int Example_Libretto_Left = 0;
        public static int Example_Libretto_Width = 0;
        public static int Example_Libretto_Height = 0;
        public static bool Example_Libretto_AdhereTo = false;//���������Ƿ��������������һ��

        //����֮��ľ����
        public static int Example_List_space_Top = 0;
        public static int Example_List_space_Left = 0;
        public static int Example_Libretto_space_Top = 0;
        public static int Example_Libretto_space_Left = 0;
        #endregion

        #region  ���������Ƿ�������һ��
        /// <summary>
        /// ���������Ƿ�������һ��
        /// </summary>
        public void FrmBackCheck()
        {
            bool Tem_Magnetism = false;
            //Frm_ListBos��������
            Tem_Magnetism = false;
            if ((Example_Play_Top - Example_List_Top) == 0)
                Tem_Magnetism = true;
            if ((Example_Play_Left - Example_List_Left) == 0)
                Tem_Magnetism = true;
            if ((Example_Play_Left - Example_List_Left - Example_List_Width) == 0)
                Tem_Magnetism = true;
            if ((Example_Play_Left - Example_List_Left + Example_List_Width) == 0)
                Tem_Magnetism = true;
            if ((Example_Play_Top - Example_List_Top - Example_List_Height) == 0)
                Tem_Magnetism = true;
            if ((Example_Play_Top - Example_List_Top + Example_List_Height) == 0)
                Tem_Magnetism = true;
            if (Tem_Magnetism)
                Example_List_AdhereTo = true;

            //Frm_Libretto��������
            Tem_Magnetism = false;
            if ((Example_Play_Top - Example_Libretto_Top) == 0)
                Tem_Magnetism = true;
            if ((Example_Play_Left - Example_Libretto_Left) == 0)
                Tem_Magnetism = true;
            if ((Example_Play_Left - Example_Libretto_Left - Example_Libretto_Width) == 0)
                Tem_Magnetism = true;
            if ((Example_Play_Left - Example_Libretto_Left + Example_Libretto_Width) == 0)
                Tem_Magnetism = true;
            if ((Example_Play_Top - Example_Libretto_Top - Example_Libretto_Height) == 0)
                Tem_Magnetism = true;
            if ((Example_Play_Top - Example_Libretto_Top + Example_Libretto_Height) == 0)
                Tem_Magnetism = true;
            if (Tem_Magnetism)
                Example_Libretto_AdhereTo = true;

            //����������
            Tem_Magnetism = false;
            if ((Example_List_Top - Example_Libretto_Top) == 0)
                Tem_Magnetism = true;
            if ((Example_List_Left - Example_Libretto_Left) == 0)
                Tem_Magnetism = true;
            if ((Example_List_Left - Example_Libretto_Left - Example_Libretto_Width) == 0)
                Tem_Magnetism = true;
            if ((Example_List_Left - Example_Libretto_Left + Example_Libretto_Width) == 0)
                Tem_Magnetism = true;
            if ((Example_List_Top - Example_Libretto_Top - Example_Libretto_Height) == 0)
                Tem_Magnetism = true;
            if ((Example_List_Top - Example_Libretto_Top + Example_Libretto_Height) == 0)
                Tem_Magnetism = true;
            if (Tem_Magnetism)
                Example_Assistant_AdhereTo = true;
        }
        #endregion

        #region  ���ô����ϵĿؼ��ƶ�����
        /// <summary>
        /// ���ÿؼ��ƶ�����
        /// </summary>
        /// <param Frm="Form">����</param>
        /// <param e="MouseEventArgs">�ؼ����ƶ��¼�</param>
        public void FrmMove(Form Frm, MouseEventArgs e)  //Form��MouseEventArgs��������ռ�using System.Windows.Forms;
        {
            if (e.Button == MouseButtons.Left)
            {
                Point myPosittion = Control.MousePosition;//��ȡ��ǰ������Ļ����
                myPosittion.Offset(CPoint.X, CPoint.Y);//���ص�ǰ����λ��
                Frm.DesktopLocation = myPosittion;//���õ�ǰ��������Ļ�ϵ�λ��
            }
        }
        #endregion

        #region  ���㴰��֮��ľ����
        /// <summary>
        /// ���㴰��֮��ľ����
        /// </summary>
        /// <param Frm="Form">����</param>
        /// <param Follow="Form">���洰��</param>
        public void FrmDistanceJob(Form Frm, Form Follow)
        {
            switch (Follow.Name)
            {
                case "Frm_ListBox":
                    {
                        Example_List_space_Top = Follow.Top - Frm.Top;
                        Example_List_space_Left = Follow.Left - Frm.Left;
                        break;
                    }
                case "Frm_Libretto":
                    {
                        Example_Libretto_space_Top = Follow.Top - Frm.Top;
                        Example_Libretto_space_Left = Follow.Left - Frm.Left;
                        break;
                    }
            }
        }
        #endregion

        #region  ���Դ�����ƶ�
        /// <summary>
        /// ���Դ�����ƶ�
        /// </summary>
        /// <param Frm="Form">����</param>
        /// <param e="MouseEventArgs">�ؼ����ƶ��¼�</param>
        /// <param Follow="Form">���洰��</param>
        public void ManyFrmMove(Form Frm, MouseEventArgs e, Form Follow)  //Form��MouseEventArgs��������ռ�using System.Windows.Forms;
        {
            if (e.Button == MouseButtons.Left)
            {
                int Tem_Left = 0;
                int Tem_Top = 0;
                Point myPosittion = Control.MousePosition;//��ȡ��ǰ������Ļ����
                switch (Follow.Name)
                {
                    case "Frm_ListBox":
                        {
                            Tem_Top = Example_List_space_Top - FrmPoint.Y;
                            Tem_Left = Example_List_space_Left - FrmPoint.X;
                            break;
                        }
                    case "Frm_Libretto":
                        {
                            Tem_Top = Example_Libretto_space_Top - FrmPoint.Y;
                            Tem_Left = Example_Libretto_space_Left - FrmPoint.X;
                            break;
                        }
                }
                myPosittion.Offset(Tem_Left, Tem_Top);
                Follow.DesktopLocation = myPosittion;
            }
        }
        #endregion

        #region  �Դ����λ�ý��г�ʼ��
        /// <summary>
        /// �Դ����λ�ý��г�ʼ��
        /// </summary>
        /// <param Frm="Form">����</param>
        public void FrmInitialize(Form Frm)
        {
            switch (Frm.Name)
            {
                case "Frm_Play":
                    {
                        Example_Play_Top = Frm.Top;
                        Example_Play_Left = Frm.Left;
                        Example_Play_Width = Frm.Width;
                        Example_Play_Height = Frm.Height;
                        break;
                    }
                case "Frm_ListBox":
                    {
                        Example_List_Top = Frm.Top;
                        Example_List_Left = Frm.Left;
                        Example_List_Width = Frm.Width;
                        Example_List_Height = Frm.Height;
                        break;
                    }
                case "Frm_Libretto":
                    {
                        Example_Libretto_Top = Frm.Top;
                        Example_Libretto_Left = Frm.Left;
                        Example_Libretto_Width = Frm.Width;
                        Example_Libretto_Height = Frm.Height;
                        break;
                    }
            }

        }
        #endregion

        #region  �洢������ĵ�ǰ��Ϣ
        /// <summary>
        /// �洢������ĵ�ǰ��Ϣ
        /// </summary>
        /// <param Frm="Form">����</param>
        /// <param e="MouseEventArgs">�ؼ����ƶ��¼�</param>
        public void FrmPlace(Form Frm)
        {
            FrmInitialize(Frm);
            FrmMagnetism(Frm);
        }
        #endregion

        #region  ����Ĵ�������
        /// <summary>
        /// ����Ĵ�������
        /// </summary>
        /// <param Frm="Form">����</param>
        public void FrmMagnetism(Form Frm)
        {
            if (Frm.Name != "Frm_Play")
            {
                FrmMagnetismCount(Frm, Example_Play_Top, Example_Play_Left, Example_Play_Width, Example_Play_Height, "Frm_Play");
            }
            if (Frm.Name != "Frm_ListBos")
            {
                FrmMagnetismCount(Frm, Example_List_Top, Example_List_Left, Example_List_Width, Example_List_Height, "Frm_ListBos");
            }
            if (Frm.Name != "Frm_Libratto")
            {
                FrmMagnetismCount(Frm, Example_Libretto_Top, Example_Libretto_Left, Example_Libretto_Width, Example_Libretto_Height, "Frm_Libratto");
            }
            FrmInitialize(Frm);
        }
        #endregion

        #region  ���Դ���ļ���
        /// <summary>
        /// ���Դ���ļ���
        /// </summary>
        /// <param Frm="Form">����</param>
        /// <param e="MouseEventArgs">�ؼ����ƶ��¼�</param>
        public void FrmMagnetismCount(Form Frm, int top, int left, int width, int height, string Mforms)
        {
            bool Tem_Magnetism = false;//�ж��Ƿ��д��Է���
            string Tem_MainForm = "";//��ʱ��¼������
            string Tem_AssistForm = "";//��ʱ��¼������

            //������д��Դ���
            if ((Frm.Top + Frm.Height - top) <= Example_FSpace && (Frm.Top + Frm.Height - top) >= -Example_FSpace)
            {
                //��һ�������岻����������ʱ
                if ((Frm.Left >= left && Frm.Left <= (left + width)) || ((Frm.Left + Frm.Width) >= left && (Frm.Left + Frm.Width) <= (left + width)))
                {
                    Frm.Top = top - Frm.Height;
                    if ((Frm.Left - left) <= Example_FSpace && (Frm.Left - left) >= -Example_FSpace)
                        Frm.Left = left;
                    Tem_Magnetism = true;
                }
                //��һ�����������������ʱ
                if (Frm.Left <= left && (Frm.Left + Frm.Width) >= (left + width))
                {
                    Frm.Top = top - Frm.Height;
                    if ((Frm.Left - left) <= Example_FSpace && (Frm.Left - left) >= -Example_FSpace)
                        Frm.Left = left;
                    Tem_Magnetism = true;
                }

            }

            //������д��Դ���
            if ((Frm.Top - (top + height)) <= Example_FSpace && (Frm.Top - (top + height)) >= -Example_FSpace)
            {
                //��һ�������岻����������ʱ
                if ((Frm.Left >= left && Frm.Left <= (left + width)) || ((Frm.Left + Frm.Width) >= left && (Frm.Left + Frm.Width) <= (left + width)))
                {
                    Frm.Top = top + height;
                    if ((Frm.Left - left) <= Example_FSpace && (Frm.Left - left) >= -Example_FSpace)
                        Frm.Left = left;
                    Tem_Magnetism = true;
                }
                //��һ�����������������ʱ
                if (Frm.Left <= left && (Frm.Left + Frm.Width) >= (left + width))
                {
                    Frm.Top = top + height;
                    if ((Frm.Left - left) <= Example_FSpace && (Frm.Left - left) >= -Example_FSpace)
                        Frm.Left = left;
                    Tem_Magnetism = true;
                }
            }

            //������д��Դ���
            if ((Frm.Left + Frm.Width - left) <= Example_FSpace && (Frm.Left + Frm.Width - left) >= -Example_FSpace)
            {
                //��һ�������岻����������ʱ
                if ((Frm.Top > top && Frm.Top <= (top + height)) || ((Frm.Top + Frm.Height) >= top && (Frm.Top + Frm.Height) <= (top + height)))
                {
                    Frm.Left = left - Frm.Width;
                    if ((Frm.Top - top) <= Example_FSpace && (Frm.Top - top) >= -Example_FSpace)
                        Frm.Top = top;
                    Tem_Magnetism = true;
                }
                //��һ�����������������ʱ
                if (Frm.Top <= top && (Frm.Top + Frm.Height) >= (top + height))
                {
                    Frm.Left = left - Frm.Width;
                    if ((Frm.Top - top) <= Example_FSpace && (Frm.Top - top) >= -Example_FSpace)
                        Frm.Top = top;
                    Tem_Magnetism = true;
                }
            }

            //������д��Դ���
            if ((Frm.Left - (left + width)) <= Example_FSpace && (Frm.Left - (left + width)) >= -Example_FSpace)
            {
                //��һ�������岻����������ʱ
                if ((Frm.Top > top && Frm.Top <= (top + height)) || ((Frm.Top + Frm.Height) >= top && (Frm.Top + Frm.Height) <= (top + height)))
                {
                    Frm.Left = left + width;
                    if ((Frm.Top - top) <= Example_FSpace && (Frm.Top - top) >= -Example_FSpace)
                        Frm.Top = top;
                    Tem_Magnetism = true;
                }
                //��һ�����������������ʱ
                if (Frm.Top <= top && (Frm.Top + Frm.Height) >= (top + height))
                {
                    Frm.Left = left + width;
                    if ((Frm.Top - top) <= Example_FSpace && (Frm.Top - top) >= -Example_FSpace)
                        Frm.Top = top;
                    Tem_Magnetism = true;
                }
            }
            if (Frm.Name == "Frm_Play")
                Tem_MainForm = "Frm_Play";
            else
                Tem_AssistForm = Frm.Name;
            if (Mforms == "Frm_Play")
                Tem_MainForm = "Frm_Play";
            else
                Tem_AssistForm = Mforms;
            if (Tem_MainForm == "")
            {
                Example_Assistant_AdhereTo = Tem_Magnetism;
            }
            else
            {
                switch (Tem_AssistForm)
                {
                    case "Frm_ListBos":
                        Example_List_AdhereTo = Tem_Magnetism;
                        break;
                    case "Frm_Libratto":
                        Example_Libretto_AdhereTo = Tem_Magnetism;
                        break;
                }
            }
        }
        #endregion

        #region  �ָ�����ĳ�ʼ��С
        /// <summary>
        /// �ָ�����ĳ�ʼ��С(���ɿ����ʱ���������Ĵ�СС��300*200,�ָ���ʼ״̬)
        /// </summary>
        /// <param Frm="Form">����</param>
        public void FrmScreen_FormerlySize(Form Frm, int PWidth, int PHeight)
        {
            if (Frm.Width < PWidth || Frm.Height < PHeight)
            {
                Frm.Width = PWidth;
                Frm.Height = PHeight;
                //Example_Size = false;
            }
        }
        #endregion

    }
}
