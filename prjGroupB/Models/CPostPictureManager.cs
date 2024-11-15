using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace prjGroupB.Models
{
    public delegate void D();
    public class CPostPictureManager
    {
        public event D afterImageMoved;
        private int _position = 0;
        private List<byte[]> _listImages;
        public CPostPictureManager(List<byte[]> listImages)
        {
            _listImages = listImages;
        }
        public byte[] current
        {
            get { return _listImages[_position]; }
        }
        public void moveFirst()
        {
            _position = 0;
            if (afterImageMoved != null)
                afterImageMoved();
        }
        public void movePrevious()
        {
            _position--;
            if (_position < 0)
                _position = 0;
            if (afterImageMoved != null)
                afterImageMoved();
        }
        public void moveNext()
        {
            _position++;
            if (_position >= _listImages.Count)
                _position = _listImages.Count - 1;
            if (afterImageMoved != null)
                afterImageMoved();
        }
        public void moveLast()
        {
            _position = _listImages.Count - 1;
            if (afterImageMoved != null)
                afterImageMoved();
        }
        public void moveTo(int to)
        {
            _position = to;
            if (afterImageMoved != null)
                afterImageMoved();
        }
    }
}
