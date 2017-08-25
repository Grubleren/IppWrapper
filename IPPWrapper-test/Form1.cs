using System;
using System.Windows.Forms;

namespace JH.Calculations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int M = 11;
            int N = 1 << M;

            FFT fft = new FFT();
            fft.Init(M,FFTFactor.IPP_FFT_DIV_INV_BY_N);

            Complex[] data = new Complex[N];

            int F = 1;

            for (int i = 0; i < N; i++)
            {
                data[i].re = Math.Sin(F * 2 * Math.PI / N * i);
                data[i].im = 0;
            }

            fft.FftForward(data);

            label1.Text = string.Format("{0:0.0000}", data[F].re);
            label2.Text = string.Format("{0:0.0000}", data[F].im);
            label3.Text = string.Format("{0:0.0000}", data[N-F].re);
            label4.Text = string.Format("{0:0.0000}", data[N-F].im);

            fft.Free();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int noBiQuads = 1;
            BiQuad[] biQuads = new BiQuad[1];
            double K = 0.1;
            biQuads[0].b0 = K;
            biQuads[0].a0 = 1;
            biQuads[0].a1 = K-1;

            IIR iir = new IIR();
            iir.Init(biQuads, noBiQuads);

            int dataLength = 1000;
            double[] inData = new double[dataLength];
            double[] outData = new double[dataLength];

            for (int i= 0; i < inData.Length; i++)
                inData[i] = 1;

            iir.Iir(inData, outData);

            iir.Free();
        }
    }
}