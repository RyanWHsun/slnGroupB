using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjGroupB.Models
{
    public delegate void D();
    public class CPostPictureManager
    {
        public event D afterImageMoved;
        private int _position = 0;
        private List<byte[]> _listImages;
        private List<byte[]> _tempImages;
        public CPostPictureManager(List<byte[]> listImages)
        {
            _listImages = listImages;
        }
        public int position {  get { return _position; } }
        public byte[] current
        {
            get 
            {
                if (_listImages.Count == 0)
                    return null;
                else
                    return _listImages[_position]; 
            }
        }
        public void removeImage()
        {
            if (_listImages.Count <= 0)
                return;
            int tempPosition = _position;
            _tempImages = new List<byte[]>(_listImages);
            _listImages.RemoveAt(_position);
            MessageBox.Show("照片已刪除");
            _position = tempPosition -1;
            if (_position < 0)
                _position = 0;
            afterImageMoved();
        }
        public void recoverImage()
        {
            _listImages = new List<byte[]>(_listImages);
            _position = 0;
            afterImageMoved();
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
