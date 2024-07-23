namespace Can.Helper
{
    public static class StuctExtension
    {
        /// <summary>
        /// 高低字节反转
        /// <para/>
        /// 按byte反转
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] Reversal(this byte[] bytes)
        {
            byte[] command = new byte[bytes.Length];

            for (int i = 0; i < bytes.Length; i++)
            {
                command[i] = bytes[bytes.Length - 1 - i];
            }

            return command;
        }

        /// <summary>
        /// 数组合并
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="afters"></param>
        /// <returns></returns>
        public static byte[] Merge(this byte[] bytes, IEnumerable<byte> afters)
        {
            return [.. bytes, .. afters];
        }

        public static byte[] UsbToCan(this byte[] bytes)
        {
            uint ccc = 0;
            ccc += bytes[0];
            ccc <<= 8;
            ccc += bytes[1];
            ccc <<= 8;
            ccc += bytes[2];
            ccc <<= 8;
            ccc += bytes[3];

            ccc <<= 3;
            ccc += 4;

            return ToBytes(ccc).Reversal();
        }

        public static byte[] ToBytes(this ushort value)
        {
            return BitConverter.GetBytes(value);
        }
        public static byte[] ToBytes(this uint value)
        {
            return BitConverter.GetBytes(value);
        }
        public static byte[] ToBytes(this float value)
        {
            return BitConverter.GetBytes(value);
        }

    }
}
