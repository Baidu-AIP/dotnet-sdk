/*
 * Copyright 2017 Baidu, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
 * an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations under the License.
 */

using System;

namespace Baidu.Aip
{
	/// <summary>
	///     百度AI异常类
	/// </summary>
	[Serializable]
    public class AipException : Exception
    {
        public AipException()
        {
            Code = -1;
        }

        public AipException(string message) : base(message)
        {
        }

        public AipException(int code, string message) : base(message)
        {
            Code = code;
        }

        public int Code { get; set; }

        public static AipException TokenException(string message)
        {
            return new AipException("Token request failed! " + message);
        }
    }
}