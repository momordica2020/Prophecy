using System;
using System.Collections.Generic;
using System.Text;

namespace Prophecy.Astronomy
{
    /// <summary>
    /// 坐标类
    /// </summary>
    public class Coordinate
    {

        double _J = 0;
        double _W = 0;
        double _R = 0;

        double _X = 0;
        double _Y = 0;
        double _Z = 0;

        bool _isCartesianConverted = false;
        bool _isSphericalConverted = false;


        public Coordinate()
        {
            J = 0; W = 0; R = 0; X = 0; Y = 0; Z = 0;
            _isCartesianConverted = false;
            _isSphericalConverted = false;
        }
        public Coordinate(Coordinate c)
        {
            J = c.J;
            W = c.W;
            R = c.R;
            X = c.X;
            Y = c.Y;
            Z = c.Z;
            _isCartesianConverted = false;
            _isSphericalConverted = false;
        }
        //public Coordinate(double j, double w, double r)
        //{
        //    J = j;
        //    W = w;
        //    R = r;
        //}

        /// <summary>
        /// 球面坐标转笛卡尔坐标
        /// </summary>
        void ConvertToCartesian()
        {
            // 将角度转换为弧度
            double radJ = _J * Math.PI / 180;  // 经度转弧度
            double radW = _W * Math.PI / 180;  // 纬度转弧度

            _X = _R * Math.Cos(radW) * Math.Cos(radJ);
            _Y = _R * Math.Cos(radW) * Math.Sin(radJ);
            _Z = _R * Math.Sin(radW);
            _isCartesianConverted = true;
        }

        /// <summary>
        /// 笛卡尔坐标转球面坐标
        /// </summary>
        void ConvertToSpherical()
        {
            _R = Math.Sqrt(_X * _X + _Y * _Y + _Z * _Z);

            // 使用反三角函数计算角度
            _J = Math.Atan2(_Y, _X) * 180 / Math.PI;  // 经度
            if (_J < 0) _J += 2 * Math.PI;

            _W = Math.Asin(_Z / _R) * 180 / Math.PI;  // 纬度
            if (_W < 0) _W += 2 * Math.PI;

            _isSphericalConverted = true;

        }

        /// <summary>
        /// 笛卡尔坐标 X
        /// </summary>
        public double X
        {
            set
            {
                _X = value;
                _isCartesianConverted = false;
            }
            get
            {
                if (!_isCartesianConverted)
                {
                    ConvertToCartesian();
                }
                return _X;
            }
        }

        /// <summary>
        /// 笛卡尔坐标 Y
        /// </summary>
        public double Y
        {
            set
            {
                _Y = value;
                _isCartesianConverted = false;
            }
            get
            {
                if (!_isCartesianConverted)
                {
                    ConvertToCartesian();
                }
                return _Y;
            }
        }

        /// <summary>
        /// 笛卡尔坐标 Z
        /// </summary>
        public double Z
        {
            set
            {
                _Z = value;
                _isCartesianConverted = false;
            }
            get
            {
                if (!_isCartesianConverted)
                {
                    ConvertToCartesian();
                }
                return _Z;
            }
        }

        /// <summary>
        /// 球面坐标 R
        /// </summary>
        public double R
        {
            set
            {
                _R = value;
                _isSphericalConverted = false;
            }
            get
            {
                if (!_isSphericalConverted)
                {
                    ConvertToSpherical();
                }
                return _R;
            }
        }

        /// <summary>
        /// 球面坐标 J (Longitude)
        /// </summary>
        public double J
        {
            set
            {
                _J = value;
                _isSphericalConverted = false;
            }
            get
            {
                if (!_isSphericalConverted)
                {
                    ConvertToSpherical();
                }
                return _J;
            }
        }

        /// <summary>
        /// 球面坐标 W (Latitude)
        /// </summary>
        public double W
        {
            set
            {
                _W = value;
                _isSphericalConverted = false;
            }
            get
            {
                if (!_isSphericalConverted)
                {
                    ConvertToSpherical();
                }
                return _W;
            }
        }




    }
}
