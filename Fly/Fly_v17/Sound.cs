using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.DirectSound;
using System.IO;

namespace DraGonQuest
{
    public class Sound:IDisposable
    {
        private SecondaryBuffer m_SecondaryBuffer = null;

        public Sound(string filePath,MainForm form)
        {
            BufferDescription desc = new BufferDescription();
            desc.StaticBuffer = true;

            Device d = new Device();
            d.SetCooperativeLevel(form,CooperativeLevel.Normal);
            m_SecondaryBuffer = new SecondaryBuffer(filePath, desc, d);
        }

        public void Play()
        {
            m_SecondaryBuffer.Play(0,BufferPlayFlags.Default);
        }

        public void Dispose()
        {
            m_SecondaryBuffer.Dispose();
            m_SecondaryBuffer = null;
        }
    }
}
